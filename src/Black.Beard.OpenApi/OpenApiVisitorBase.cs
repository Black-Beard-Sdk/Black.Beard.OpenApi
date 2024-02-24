using Bb.Extensions;
using Bb.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Xml.Schema;

namespace Bb
{


    public class OpenApiVisitorBase : OpenApiBase, IOpenApiVisitor
    {

        /// <summary>
        /// Visits the <see cref="OpenApiDocument" />.
        /// </summary>
        /// <param name="self">The self.</param>
        public virtual void VisitDocument(OpenApiDocument self)
        {
            self.ExternalDocs.Accept(this);
            self.Info.Accept(this);
            self.Servers.Accept(this);

            using (PushContext("paths"))
                self.Paths.Accept(this);

            self.Components.Accept(this);
            self.SecurityRequirements.Accept(this);
            self.Tags.Accept(this);
        }

        public virtual void VisitComponents(OpenApiComponents self)
        {

            self.Callbacks.Accept(this);
            self.Examples.Accept(this);
            self.Extensions.Accept(this);
            self.Headers.Accept(this);
            self.Links.Accept(this);
            self.Parameters.Accept(this);
            self.RequestBodies.Accept(this);
            self.Responses.Accept(this);

            if (self.Schemas != null && self.Schemas.Any())
                using (PushContext("class"))
                    self.Schemas.Accept(this);

            self.SecuritySchemes.Accept(this);

        }

        public virtual void VisitSchemas(IList<OpenApiSchema> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitSchemas(IDictionary<string, OpenApiSchema> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitSchema(KeyValuePair<string, OpenApiSchema> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitSchema(OpenApiSchema self)
        {

            self.AdditionalProperties.Accept(this);

            if (self.AllOf.Any())
                using (PushContext("AllOf"))
                    self.AllOf.Accept(this);

            if (self.AnyOf.Any())
                using (PushContext("AnyOf"))
                    self.AnyOf.Accept(this);

            if (self.OneOf.Any())
                using (PushContext("OneOf"))
                    self.OneOf.Accept(this);

            self.Extensions.Accept(this);
            self.ExternalDocs.Accept(this);

            if (self.Items != null)
                using (PushContext("Items"))
                    self.Items.Accept(this);

            self.Not.Accept(this);

            if (self.Properties.Any())
                using (PushContext("property"))
                    self.Properties.Accept(this);

            self.Reference.Accept(this);

        }

        public virtual void VisitEnumPrimitive(IOpenApiPrimitive self)
        {

        }

        public virtual void VisitInfo(OpenApiInfo self)
        {

            self.Contact.Accept(this);
            self.Extensions.Accept(this);
            self.License.Accept(this);

        }


        public virtual void VisitLinks(IDictionary<string, OpenApiLink> self)
        {
            foreach (var item in self)
            {
                item.Accept(this);
            }
        }

        public virtual void VisitLink(KeyValuePair<string, OpenApiLink> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitLink(OpenApiLink self)
        {
            self.Parameters.Accept(this);
            self.Server.Accept(this);
        }


        public virtual void VisitEncodings(IDictionary<string, OpenApiEncoding> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }
        public virtual void VisitEncoding(KeyValuePair<string, OpenApiEncoding> self)
        {

            self.Value.Accept(this);
        }
        public virtual void VisitEncoding(OpenApiEncoding self)
        {
            self.Headers.Accept(this);
            self.Extensions.Accept(this);
        }




        public virtual void VisitMediaTypes(IDictionary<string, OpenApiMediaType> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitMediaType(KeyValuePair<string, OpenApiMediaType> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitMediaType(OpenApiMediaType self)
        {
            self.Schema.Accept(this);
            self.Examples.Accept(this);
            self.Encoding.Accept(this);
            self.Extensions.Accept(this);
        }

        public virtual void VisitOperations(IDictionary<OperationType, OpenApiOperation> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitOperation(KeyValuePair<OperationType, OpenApiOperation> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitOperation(OpenApiOperation self)
        {
            self.Callbacks.Accept(this);
            self.Extensions.Accept(this);
            self.Parameters.Accept(this);
            self.RequestBody.Accept(this);
            self.Responses.Accept(this);
            self.ExternalDocs.Accept(this);
            self.Security.Accept(this);
            self.Servers.Accept(this);
            self.Tags.Accept(this);
        }

        public virtual void VisitParameters(IList<OpenApiParameter> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitParameter(OpenApiParameter self)
        {
            self.Reference.Accept(this);
        }

        public virtual void VisitParameters(IDictionary<string, OpenApiParameter> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitParameter(KeyValuePair<string, OpenApiParameter> self)
        {
            self.Value.Accept(this);
        }


        public virtual void VisitPaths(OpenApiPaths self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitPathItem(KeyValuePair<string, OpenApiPathItem> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitResponses(IDictionary<string, OpenApiResponse> self)
        {
            foreach (var item in self)
                item.Value.Accept(this);
        }
        public virtual void VisitResponse(KeyValuePair<string, OpenApiResponse> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitResponse(OpenApiResponse self)
        {
            self.Extensions.Accept(this);
            self.Content.Accept(this);
            self.Headers.Accept(this);
            self.Links.Accept(this);
            self.Reference.Accept(this);

        }


        public virtual void VisitServer(OpenApiServer self)
        {
            self.Variables.Accept(this);
            self.Extensions.Accept(this);
        }

        public virtual void VisitTag(OpenApiTag self)
        {
            self.ExternalDocs.Accept(this);
            self.Extensions.Accept(this);
            self.Reference.Accept(this);
        }

        public virtual void VisitExternalDocs(OpenApiExternalDocs self)
        {
            self.Extensions.Accept(this);
        }

        public virtual void VisitSecurityRequirements(IList<OpenApiSecurityRequirement> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitServers(IList<OpenApiServer> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitTags(IList<OpenApiTag> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitCallbacks(IDictionary<string, OpenApiCallback> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitCallback(KeyValuePair<string, OpenApiCallback> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitCallback(OpenApiCallback self)
        {
            self.Extensions.Accept(this);
            self.Reference.Accept(this);
            self.PathItems.Accept(this);
        }


        public virtual void VisitExamples(IDictionary<string, OpenApiExample> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitExample(KeyValuePair<string, OpenApiExample> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitExample(OpenApiExample self)
        {
            self.Extensions.Accept(this);
            self.Reference.Accept(this);
        }

        public virtual void VisitExtensions(IDictionary<string, IOpenApiExtension> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitExtension(KeyValuePair<string, IOpenApiExtension> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitExtension(IOpenApiExtension self)
        {

        }


        public virtual void VisitHeaders(IDictionary<string, OpenApiHeader> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitHeader(KeyValuePair<string, OpenApiHeader> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitHeader(OpenApiHeader self)
        {
            self.Schema.Accept(this);
            self.Example.Accept(this);
            self.Content.Accept(this);
            self.Extensions.Accept(this);
        }

        public virtual void VisitRequestBodies(IDictionary<string, OpenApiRequestBody> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }
        public virtual void VisitRequestBody(KeyValuePair<string, OpenApiRequestBody> self)
        {
            self.Accept(this);
        }

        public virtual void VisitRequestBody(OpenApiRequestBody self)
        {
            self.Extensions.Accept(this);
            self.Content.Accept(this);
            self.Reference.Accept(this);
        }

        public virtual void VisitSecuritySchemes(IDictionary<string, OpenApiSecurityScheme> self)
        {
            foreach (var item in self)
                item.Value.Accept(this);
        }

        public virtual void VisitReference(OpenApiReference self)
        {

        }

        public virtual void VisitOpenApiElement(IOpenApiElement self)
        {

        }

        public virtual void VisitRuntimeExpressionWrappers(Dictionary<string, RuntimeExpressionAnyWrapper> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitRuntimeExpressionWrapper(KeyValuePair<string, RuntimeExpressionAnyWrapper> self)
        {
            self.Value.Accept(this);
        }

        public virtual void VisitRuntimeExpressionWrapper(RuntimeExpressionAnyWrapper self)
        {

        }

        public virtual void VisitContact(OpenApiContact self)
        {
            self.Extensions.Accept(this);
        }

        public virtual void VisitLicense(OpenApiLicense self)
        {
            self.Extensions.Accept(this);
        }

        public virtual void VisitPathItems(Dictionary<RuntimeExpression, OpenApiPathItem> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitPathItem(KeyValuePair<RuntimeExpression, OpenApiPathItem> self)
        {
            self.Key.Accept(this);
            self.Value.Accept(this);
        }

        public virtual void VisitRuntimeExpression(RuntimeExpression self)
        {

        }

        public virtual void VisitPathItem(OpenApiPathItem self)
        {
            self.Extensions.Accept(this);
            self.Reference.Accept(this);
            foreach (var item in self.Operations)
                item.Accept(this);
        }

        public virtual void VisitSecurityRequirement(OpenApiSecurityRequirement self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitSecurityScheme(OpenApiSecurityScheme self)
        {
            self.Extensions.Accept(this);
            self.Reference.Accept(this);
        }

        public virtual void VisitSecurityScheme(KeyValuePair<OpenApiSecurityScheme, IList<string>> self)
        {
            self.Key.Accept(this);
        }

        public virtual void VisitVariables(IDictionary<string, OpenApiServerVariable> self)
        {
            foreach (var item in self)
                item.Accept(this);
        }

        public virtual void VisitVariable(KeyValuePair<string, OpenApiServerVariable> self)
        {

            self.Value.Accept(this);
        }

        public virtual void VisitVariable(OpenApiServerVariable self)
        {
            self.Extensions.Accept(this);
        }


    }

}
