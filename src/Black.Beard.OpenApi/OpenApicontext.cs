using Bb.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Bb
{

    /// <summary>
    /// context for generate the schema
    /// </summary>
    public class OpenApicontext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApicontext"/> class.
        /// </summary>
        /// <param name="options">The options for generate. if the value is null. a new instance is created with UseOneOfForPolymorphism  activated </param>
        public OpenApicontext(SchemaGeneratorOptions? options = null)
        {

            this._schemas = new Dictionary<string, OpenApiSchema>();
            this._repository = new SchemaRepository();

            _options = options ?? new SchemaGeneratorOptions()
            {
                UseOneOfForPolymorphism = true,
            };

            _serializerOptions = new JsonSerializerOptions()
            {

            };

            _serializer = new JsonSerializerDataContractResolver2(_serializerOptions);

            _schemaGenerator = new SchemaGenerator(_options, _serializer);

        }

        /// <summary>
        /// Gets the schema's list.
        /// </summary>
        /// <value>
        /// The list of schema.
        /// </value>
        public SchemaRepository Repository => _repository;


        /// <summary>
        /// Gets the schema's list.
        /// </summary>
        /// <value>
        /// The list of schema.
        /// </value>
        public Dictionary<string, OpenApiSchema> Schemas => _schemas;

        /// <summary>
        /// Gets the options for generate the schema.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public SchemaGeneratorOptions Options => _options;

        /// <summary>
        /// Gets the json serializer options.
        /// </summary>
        /// <value>
        /// The json serializer options.
        /// </value>
        public JsonSerializerOptions JsonSerializerOptions => _serializerOptions;


        /// <summary>
        /// Appends the specified type in the context.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public OpenApicontext GenerateOpenApiContract(Type type)
        {

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var name = type.Name;
            if (!this._schemas.ContainsKey(name))
            {
                OpenApiSchema openApi = _schemaGenerator.GenerateSchema(type, _repository);
                this._schemas.Add(name, openApi);
            }

            return this;

        }

        /// <summary>
        /// Generates the document
        /// </summary>
        /// <returns></returns>
        public OpenApiDocument Generate()
        {
            
            OpenApiDocument document = new OpenApiDocument()
            {
                Components = new OpenApiComponents()
            };

            foreach (var item in _repository.Schemas)
                document.Components.Schemas.Add(item.Key, item.Value);

            if (_doc != null)
                ApplyDocumentationVisitor.ApplyDocumentation(document, _doc, "");

            return document;

        }

        /// <summary>
        /// Add document for applies the documentation to generation.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        public OpenApicontext ApplyDocumentation(XElement doc)
        {

            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            if (this._doc == null)
                this._doc = doc;
            else
                this._doc = this._doc.Merges(doc);

            return this;
        }


        private SchemaRepository _repository;
        private Dictionary<string, OpenApiSchema> _schemas;
        private XElement? _doc;
        private readonly SchemaGeneratorOptions _options;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly JsonSerializerDataContractResolver2 _serializer;
        private readonly SchemaGenerator _schemaGenerator;


    }
     
}
