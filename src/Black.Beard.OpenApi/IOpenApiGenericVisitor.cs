using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace Bb.OpenApi
{

    public interface IOpenApiGenericVisitor<T> : IOpenApi
    {
        T? VisitCallback(KeyValuePair<string, OpenApiCallback> self);
        T? VisitCallback(OpenApiCallback self);
        T? VisitCallbacks(IDictionary<string, OpenApiCallback> self);
        T? VisitComponents(OpenApiComponents self);
        T? VisitContact(OpenApiContact self);
        T? VisitDocument(OpenApiDocument self);
        T? VisitEncoding(KeyValuePair<string, OpenApiEncoding> self);
        T? VisitEncoding(OpenApiEncoding self);
        T? VisitEncodings(IDictionary<string, OpenApiEncoding> self);
        T? VisitEnumPrimitive(IOpenApiPrimitive self);
        T? VisitExample(KeyValuePair<string, OpenApiExample> self);
        T? VisitExample(OpenApiExample self);
        T? VisitExamples(IDictionary<string, OpenApiExample> self);
        T? VisitExtension(KeyValuePair<string, IOpenApiExtension> self);
        T? VisitExtension(IOpenApiExtension self);
        T? VisitExtensions(IDictionary<string, IOpenApiExtension> self);
        T? VisitExternalDocs(OpenApiExternalDocs self);
        T? VisitHeader(KeyValuePair<string, OpenApiHeader> self);
        T? VisitHeader(OpenApiHeader self);
        T? VisitHeaders(IDictionary<string, OpenApiHeader> self);
        T? VisitInfo(OpenApiInfo self);
        T? VisitLicense(OpenApiLicense self);
        T? VisitLink(KeyValuePair<string, OpenApiLink> self);
        T? VisitLink(OpenApiLink self);
        T? VisitLinks(IDictionary<string, OpenApiLink> self);

        T? VisitMediaTypes(IDictionary<string, OpenApiMediaType> self);
        T? VisitMediaType(KeyValuePair<string, OpenApiMediaType> self);
        T? VisitMediaType(OpenApiMediaType self);

        T? VisitOperations(IDictionary<OperationType, OpenApiOperation> self);
        T? VisitOperation(KeyValuePair<OperationType, OpenApiOperation> self);
        T? VisitOperation(OpenApiOperation self);

        T? VisitParameters(IDictionary<string, OpenApiParameter> self);
        T? VisitParameters(IList<OpenApiParameter> self);
        T? VisitParameter(KeyValuePair<string, OpenApiParameter> self);
        T? VisitParameter(OpenApiParameter self);
        
        T? VisitPaths(OpenApiPaths self);


        T? VisitPathItems(Dictionary<RuntimeExpression, OpenApiPathItem> self);
        T? VisitPathItem(KeyValuePair<string, OpenApiPathItem> self);
        T? VisitPathItem(KeyValuePair<RuntimeExpression, OpenApiPathItem> self);
        T? VisitPathItem(OpenApiPathItem self);
        
        T? VisitReference(OpenApiReference self);

        T? VisitRequestBodies(IDictionary<string, OpenApiRequestBody> self);
        T? VisitRequestBody(KeyValuePair<string, OpenApiRequestBody> self);
        T? VisitRequestBody(OpenApiRequestBody self);

        T? VisitResponse(KeyValuePair<string, OpenApiResponse> self);
        T? VisitResponse(OpenApiResponse self);
        T? VisitResponses(IDictionary<string, OpenApiResponse> self);
        T? VisitRuntimeExpression(RuntimeExpression self);
        T? VisitRuntimeExpressionWrapper(KeyValuePair<string, RuntimeExpressionAnyWrapper> self);
        T? VisitRuntimeExpressionWrapper(RuntimeExpressionAnyWrapper self);
        T? VisitRuntimeExpressionWrappers(Dictionary<string, RuntimeExpressionAnyWrapper> self);
        T? VisitSchema(KeyValuePair<string, OpenApiSchema> self);
        T? VisitSchema(OpenApiSchema self);
        T? VisitSchemas(IList<OpenApiSchema> self);
        T? VisitSchemas(IDictionary<string, OpenApiSchema> self);
        T? VisitSecurityRequirement(OpenApiSecurityRequirement self);
        T? VisitSecurityRequirements(IList<OpenApiSecurityRequirement> self);
        T? VisitSecurityScheme(OpenApiSecurityScheme self);
        T? VisitSecurityScheme(KeyValuePair<OpenApiSecurityScheme, IList<string>> self);
        T? VisitSecuritySchemes(IDictionary<string, OpenApiSecurityScheme> self);
        T? VisitServer(OpenApiServer self);
        T? VisitServers(IList<OpenApiServer> self);
        T? VisitTag(OpenApiTag self);
        T? VisitTags(IList<OpenApiTag> self);
        T? VisitVariable(KeyValuePair<string, OpenApiServerVariable> self);
        T? VisitVariable(OpenApiServerVariable self);
        T? VisitVariables(IDictionary<string, OpenApiServerVariable> self);
    }


}
