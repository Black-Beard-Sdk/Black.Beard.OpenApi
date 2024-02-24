using Bb.Extensions;
using Bb.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Linq;

namespace Bb.Generators
{

    internal class SchemaIdentifyModels : OpenApiVisitorBase, IOpenApiVisitor
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaIdentifyModels"/> class.
        /// </summary>
        /// <param name="schemaRoot">The schema root.</param>
        public SchemaIdentifyModels(string[] schemaRoots)
        {
            this._schemaRoots = schemaRoots;
            this.Result = new HashSet<string>();
        }


        /// <summary>
        /// Identify all models for the specified schema name.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <returns></returns>
        public static HashSet<string> Get(OpenApiDocument schema, params string[] schemaRoots)
        {
            var visitor = new SchemaIdentifyModels(schemaRoots);
            schema.Accept(visitor);
            return visitor.Result;
        }

        public override void VisitDocument(OpenApiDocument self)
        {

            this._root = self.Components;

            foreach (var schemaRoot in _schemaRoots)
            {

                var root = self.Components.Schemas.FirstOrDefault(c => c.Key == schemaRoot);

                if (root.Key != null)
                    root.Value.Accept(this);

            }

        }

        public override void VisitSchemas(IDictionary<string, OpenApiSchema> self)
        {
            foreach (var item in self)
                item.Value.Accept(this);
        }

        public override void VisitSchema(OpenApiSchema self)
        {
            if (self.Reference != null)
            {
                var name = self.Reference.Id;
                if (!string.IsNullOrEmpty(name) && !_schemaRoots.Contains(name))
                    Result.Add(name);
            }

            self.Items.Accept(this);
            self.OneOf.Accept(this);
            self.AllOf.Accept(this);
            self.AnyOf.Accept(this);
            self.Properties.Accept(this);

        }

        public override void VisitSchemas(IList<OpenApiSchema> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public HashSet<string> Result { get; }


        private readonly string[] _schemaRoots;
        private OpenApiComponents _root;
    }


}
