using Bb.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Bb.Extensions;

namespace Bb
{
    public abstract class OpenApiGenericVisitor<T> : OpenApiBase, IOpenApiGenericVisitor<T>
    {

        public virtual T? VisitCallback(KeyValuePair<string, OpenApiCallback> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitCallback(OpenApiCallback self);

        public abstract T? VisitCallbacks(IDictionary<string, OpenApiCallback> self);

        public abstract T? VisitComponents(OpenApiComponents self);

        public abstract T? VisitContact(OpenApiContact self);

        public abstract T? VisitDocument(OpenApiDocument self);

        public virtual T? VisitEncoding(KeyValuePair<string, OpenApiEncoding> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitEncoding(OpenApiEncoding self);

        public abstract T? VisitEncodings(IDictionary<string, OpenApiEncoding> self);

        public abstract T? VisitEnumPrimitive(IOpenApiPrimitive self);

        public virtual T? VisitExample(KeyValuePair<string, OpenApiExample> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitExample(OpenApiExample self);

        public abstract T? VisitExamples(IDictionary<string, OpenApiExample> self);

        public virtual T? VisitExtension(KeyValuePair<string, IOpenApiExtension> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitExtension(IOpenApiExtension self);

        public abstract T? VisitExtensions(IDictionary<string, IOpenApiExtension> self);

        public abstract T? VisitExternalDocs(OpenApiExternalDocs self);

        public virtual T? VisitHeader(KeyValuePair<string, OpenApiHeader> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitHeader(OpenApiHeader self);

        public abstract T? VisitHeaders(IDictionary<string, OpenApiHeader> self);

        public abstract T? VisitInfo(OpenApiInfo self);

        public abstract T? VisitLicense(OpenApiLicense self);

        public virtual T? VisitLink(KeyValuePair<string, OpenApiLink> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitLink(OpenApiLink self);

        public abstract T? VisitLinks(IDictionary<string, OpenApiLink> self);


        public abstract T? VisitMediaTypes(IDictionary<string, OpenApiMediaType> self);

        public virtual T? VisitMediaType(KeyValuePair<string, OpenApiMediaType> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitMediaType(OpenApiMediaType self);




        public abstract T? VisitOperations(IDictionary<OperationType, OpenApiOperation> self);
        public virtual T? VisitOperation(KeyValuePair<OperationType, OpenApiOperation> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitOperation(OpenApiOperation self);

        public virtual T? VisitParameter(KeyValuePair<string, OpenApiParameter> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitParameter(OpenApiParameter self);

        public abstract T? VisitParameters(IDictionary<string, OpenApiParameter> self);

        public abstract T? VisitParameters(IList<OpenApiParameter> self);

        public abstract T? VisitPaths(OpenApiPaths self);

        public virtual T? VisitPathItem(KeyValuePair<string, OpenApiPathItem> self)
        {
            return self.Value.Accept(this);
        }

        public virtual T? VisitPathItem(KeyValuePair<RuntimeExpression, OpenApiPathItem> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitPathItem(OpenApiPathItem self);

        public abstract T? VisitPathItems(Dictionary<RuntimeExpression, OpenApiPathItem> self);

        public abstract T? VisitReference(OpenApiReference self);

        public abstract T? VisitRequestBodies(IDictionary<string, OpenApiRequestBody> self);

        public virtual T? VisitRequestBody(KeyValuePair<string, OpenApiRequestBody> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitRequestBody(OpenApiRequestBody self);

        public virtual T? VisitResponse(KeyValuePair<string, OpenApiResponse> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitResponse(OpenApiResponse self);

        public abstract T? VisitResponses(IDictionary<string, OpenApiResponse> self);

        public abstract T? VisitRuntimeExpression(RuntimeExpression self);

        public virtual T? VisitRuntimeExpressionWrapper(KeyValuePair<string, RuntimeExpressionAnyWrapper> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitRuntimeExpressionWrapper(RuntimeExpressionAnyWrapper self);

        public abstract T? VisitRuntimeExpressionWrappers(Dictionary<string, RuntimeExpressionAnyWrapper> self);


        public abstract T? VisitSchema(OpenApiSchema self);

        public abstract T? VisitSchemas(IList<OpenApiSchema> self);

        public abstract T? VisitSchemas(IDictionary<string, OpenApiSchema> self);

        public virtual T? VisitSchema(KeyValuePair<string, OpenApiSchema> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitSecurityRequirement(OpenApiSecurityRequirement self);

        public abstract T? VisitSecurityRequirements(IList<OpenApiSecurityRequirement> self);

        public abstract T? VisitSecurityScheme(OpenApiSecurityScheme self);

        public abstract T? VisitSecurityScheme(KeyValuePair<OpenApiSecurityScheme, IList<string>> self);

        public abstract T? VisitSecuritySchemes(IDictionary<string, OpenApiSecurityScheme> self);

        public abstract T? VisitServer(OpenApiServer self);

        public abstract T? VisitServers(IList<OpenApiServer> self);

        public abstract T? VisitTag(OpenApiTag self);

        public abstract T? VisitTags(IList<OpenApiTag> self);

        public virtual T? VisitVariable(KeyValuePair<string, OpenApiServerVariable> self)
        {
            return self.Value.Accept(this);
        }

        public abstract T? VisitVariable(OpenApiServerVariable self);

        public abstract T? VisitVariables(IDictionary<string, OpenApiServerVariable> self);


    }

}
