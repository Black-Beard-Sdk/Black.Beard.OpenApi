using Bb.Generators;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static System.Text.Json.JsonElement;

namespace Bb.Extensions
{


    /// <summary>
    /// OpenApi extension
    /// </summary>
    public static class OpenApiExtension
    {

        #region I/O

        /// <summary>
        /// Loads Open api contract file path
        /// </summary>
        /// <param name="pathFile">path file</param>
        /// <returns><see cref="OpenApiDocument"/></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static OpenApiDocument LoadOpenApiContract(this string pathFile)
        {

            var file = new FileInfo(pathFile);
            file.Refresh();

            if (!file.Exists)
                throw new FileNotFoundException(file.FullName);

            using (FileStream fs = File.Open(pathFile, FileMode.Open))
            {
                var openApiDocument = new OpenApiStreamReader().Read(fs, out var diagnostic);
                return openApiDocument;
            }

        }


        /// <summary>
        /// Saves the document to specified path file.
        /// </summary>
        /// <param name="document"><see cref="OpenApiDocument"/></param>
        /// <param name="pathFile">path file</param>
        public static void SaveOpenApiContract(this OpenApiDocument document, string pathFile)
        {

            using (FileStream fs = File.Open(pathFile, FileMode.Create))
            using (var writer = new StreamWriter(fs, Encoding.UTF8, bufferSize: 2048, leaveOpen: true))
            {
                var jsonWriter = new OpenApiJsonWriter(writer);
                document.SerializeAsV3(jsonWriter);
                fs.Flush();
            }

        }

        /// <summary>
        /// Saves the document to specified path file.
        /// </summary>
        /// <param name="document"><see cref="OpenApiDocument"/></param>
        /// <param name="pathFile">path file</param>
        public static byte[] SerializeToByte(this OpenApiDocument document)
        {

            if (document == null)
                throw new ArgumentNullException(nameof(document));

            using (MemoryStream memo = new MemoryStream())
            {

                using (var writer = new StreamWriter(memo, Encoding.UTF8, bufferSize: 2048, leaveOpen: true))
                {
                    var jsonWriter = new OpenApiJsonWriter(writer);
                    document.SerializeAsV3(jsonWriter);
                }

                memo.Flush();
                return memo.ToArray();
            }

        }

        /// <summary>
        /// Saves the document to specified path file.
        /// </summary>
        /// <param name="document"><see cref="OpenApiDocument"/></param>
        /// <param name="pathFile">path file</param>
        public static string SerializeToString(this OpenApiDocument document, Encoding encoding = null)
        {

            if (document == null)
                throw new ArgumentNullException(nameof(document));

            using (MemoryStream memo = new MemoryStream())
            {

                using (var writer = new StreamWriter(memo, Encoding.UTF8, bufferSize: 2048, leaveOpen: true))
                {
                    var jsonWriter = new OpenApiJsonWriter(writer);
                    document.SerializeAsV3(jsonWriter);
                }

                memo.Flush();

                if (encoding == null)
                    encoding = Encoding.UTF8;

                return encoding.GetString(memo.ToArray());

            }

        }

        #endregion I/O


        /// <summary>
        /// Collect all types from OpenApiDocument <see cref="OpenApiDocument"/> and generate a contract schema 
        /// </summary>
        /// <param name="componentDatas"><see cref="OpenApiDocument"/></param>
        /// <param name="rootKey">the root key of the contract schema</param>
        /// <returns>string</returns>
        public static JsonObject GenerateSchemaContract(this OpenApiDocument document, string rootKey, string id = null)
        {

            string payload = document.SerializeToString();
            payload = payload.Substring(1).Trim(); // Remove bom

            // Get the list of models
            var doc = JsonDocument.Parse(payload);
            var components = doc.RootElement.GetProperty("components").GetProperty("schemas");
            ObjectEnumerator e = components.EnumerateObject();

            JsonProperty? root = default;
            var definitions = new JsonObject();

            var properties = BuildModel(rootKey , e, definitions, document);

            if (string.IsNullOrEmpty(id))
                id = BuildId(document, root);

            var result = new JsonObject()
            {
                ["$id"] = id,
                ["$schema"] = "http://json-schema.org/draft-04/schema#",
                ["title"] = properties.Key,
                ["type"] = "object",
                ["additionalProperties"] = true,
                ["properties"] = properties.Value,
                ["definitions"] = definitions
            };

            return result;

        }

        /// <summary>
        /// Collect all types from OpenApiDocument <see cref="OpenApiDocument"/> and generate a contract schema 
        /// </summary>
        /// <param name="componentDatas"><see cref="OpenApiDocument"/></param>
        /// <param name="rootKey">the root key of the contract schema</param>
        /// <returns>string</returns>
        public static JsonObject GenerateSchemaMultiContract(this OpenApiDocument document, string[] rootKeys, string id = null)
        {

            string payload = document.SerializeToString();
            payload = payload.Substring(1).Trim(); // Remove bom

            // Get the list of models
            var doc = JsonDocument.Parse(payload);
            var components = doc.RootElement.GetProperty("components").GetProperty("schemas");
            ObjectEnumerator e = components.EnumerateObject();

            JsonProperty? root = default;
            var definitions = new JsonObject();

            var properties = BuildMultiModel(rootKeys, e, definitions, document);

            if (string.IsNullOrEmpty(id))
                id = BuildId(document, root);

            var result = new JsonObject()
            {
                ["$id"] = id,
                ["$schema"] = "http://json-schema.org/draft-04/schema#",
                ["title"] = id,
                ["type"] = "object",
                ["additionalProperties"] = true,
                ["properties"] = properties,
                ["definitions"] = definitions
            };

            return result;

        }

        private static string BuildId(OpenApiDocument document, JsonProperty? root)
        {

            string id = document.Info.Title
                          .Replace(":", "")
                          .Replace("  ", " ")
                          .Replace(" ", "-");

            id = "http://local/" + id + "/" + root.Value.Name;

            return id;

        }

        private static JsonObject BuildMultiModel(string[] rootKeys, ObjectEnumerator modelList, JsonObject definitions, OpenApiDocument document)
        {

            
            JsonObject jsonNode = new JsonObject()
            {
                
            };

            List<string> results = new List<string>(rootKeys.Length);

            // Get the list of models
            var _definitions = SchemaIdentifyModels.Get(document, rootKeys);

            while (modelList.MoveNext())
            {

                JsonProperty item = modelList.Current;
                var name = item.Name;

                if (rootKeys.Contains(name))
                {                    
                    var valueDefinition = JsonNode.Parse(ConvertPath(item.Value.GetRawText()));
                    definitions.Add(name, valueDefinition);

                    jsonNode.Add(name, new JsonObject()
                    {
                        ["$ref"] = "#/definitions/" + name
                    });

                    results.Add(name);
                }

                else if (_definitions.Contains(name))
                {
                    var valueDefinition = JsonNode.Parse(ConvertPath(item.Value.GetRawText()));
                    definitions.Add(name, valueDefinition);
                }

            }
            
            return jsonNode;

        }

        private static KeyValuePair<string, JsonNode> BuildModel(string rootKey, ObjectEnumerator modelList, JsonObject definitions, OpenApiDocument document)
        {

            JsonProperty? root = default;

            // Get the list of models
            var _definitions = SchemaIdentifyModels.Get(document, rootKey);

            while (modelList.MoveNext())
            {

                JsonProperty item = modelList.Current;
                var name = item.Name;

                if (rootKey == name)
                    root = item;

                else if (_definitions.Contains(name))
                {
                    var valueDefinition = JsonNode.Parse(ConvertPath(item.Value.GetRawText()));
                    definitions.Add(name, valueDefinition);
                }

            }

            if (root == null)
                throw new Exception($"rootKey '{rootKey}' not found");

            var txt = ConvertPath(root.Value.Value.GetProperty("properties").GetRawText());
            JsonNode properties = JsonNode.Parse(txt);

            return new KeyValuePair<string, JsonNode>(root.Value.Name, properties);

        }

        private static string ConvertPath(string path)
        {
            if (path.Contains("#/components/schemas"))
                path = path.Replace("#/components/schemas", "#/definitions");
            return path;
        }

        /// <summary>
        /// Collect all types from OpenApiDocument <see cref="OpenApiComponents"/> and generate a contract schema 
        /// </summary>
        /// <param name="componentDatas"><see cref="OpenApiComponents"/></param>
        /// <param name="rootKey">the root key of the contract schema</param>
        /// <returns>string</returns>
        public static JsonObject GenerateSchemaContracts(this OpenApiComponents componentDatas, string rootKey)
        {

            var doc = new OpenApiDocument()
            {
                Components = componentDatas
            };

            return doc.GenerateSchemaContract(rootKey);

        }

        /// <summary>
        /// Generate a contract schema <see cref="OpenApiComponents"/> from specified type
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static OpenApicontext GenerateOpenApiContract(this Type self)
        {
            var ctx = new OpenApicontext();
            ctx.GenerateOpenApiContract(self);
            return ctx;
        }


    }

}
