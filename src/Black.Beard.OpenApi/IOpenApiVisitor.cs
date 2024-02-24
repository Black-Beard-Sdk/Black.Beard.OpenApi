using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace Bb.OpenApi
{

    public interface IOpenApiVisitor : IOpenApi
    {


        /// <summary>
        /// Visits the generic open API element.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        void VisitOpenApiElement(IOpenApiElement self);

        /// <summary>
        /// Visits the <see cref="OpenApiDocument"/>.
        /// </summary>
        /// <param name="self">The self.</param>
        void VisitDocument(OpenApiDocument self);



        void VisitTags(IList<OpenApiTag> self);
        void VisitTag(OpenApiTag self);



        void VisitSecuritySchemes(IDictionary<string, OpenApiSecurityScheme> self);

        void VisitSecurityScheme(KeyValuePair<OpenApiSecurityScheme, IList<string>> self);

        void VisitSecurityScheme(OpenApiSecurityScheme self);


        void VisitSecurityRequirements(IList<OpenApiSecurityRequirement> self);
        void VisitSecurityRequirement(OpenApiSecurityRequirement self);


        void VisitResponses(IDictionary<string, OpenApiResponse> self);
        void VisitResponse(KeyValuePair<string, OpenApiResponse> self);
        void VisitResponse(OpenApiResponse self);


        void VisitInfo(OpenApiInfo self);


        void VisitComponents(OpenApiComponents self);



        void VisitPaths(OpenApiPaths self);


        void VisitOperations(IDictionary<OperationType, OpenApiOperation> self);
        void VisitOperation(KeyValuePair<OperationType, OpenApiOperation> self);
        void VisitOperation(OpenApiOperation self);



        void VisitEnumPrimitive(IOpenApiPrimitive self);



        void VisitSchemas(IList<OpenApiSchema> self);
        void VisitSchemas(IDictionary<string, OpenApiSchema> self);
        void VisitSchema(KeyValuePair<string, OpenApiSchema> self);
        void VisitSchema(OpenApiSchema self);




        void VisitExternalDocs(OpenApiExternalDocs self);




        void VisitCallbacks(IDictionary<string, OpenApiCallback> self);
        void VisitCallback(KeyValuePair<string, OpenApiCallback> self);
        void VisitCallback(OpenApiCallback self);


        void VisitExamples(IDictionary<string, OpenApiExample> self);
        void VisitExample(KeyValuePair<string, OpenApiExample> self);
        void VisitExample(OpenApiExample self);


        void VisitExtensions(IDictionary<string, IOpenApiExtension> self);
        void VisitExtension(KeyValuePair<string, IOpenApiExtension> self);
        void VisitExtension(IOpenApiExtension self);


        void VisitHeaders(IDictionary<string, OpenApiHeader> self);
        void VisitHeader(KeyValuePair<string, OpenApiHeader> self);
        void VisitHeader(OpenApiHeader self);


        void VisitLinks(IDictionary<string, OpenApiLink> self);
        void VisitLink(KeyValuePair<string, OpenApiLink> self);
        void VisitLink(OpenApiLink self);


        void VisitRequestBodies(IDictionary<string, OpenApiRequestBody> self);
        void VisitRequestBody(KeyValuePair<string, OpenApiRequestBody> self);
        void VisitRequestBody(OpenApiRequestBody self);


        void VisitReference(OpenApiReference self);


        void VisitRuntimeExpressionWrappers(Dictionary<string, RuntimeExpressionAnyWrapper> self);
        void VisitRuntimeExpressionWrapper(KeyValuePair<string, RuntimeExpressionAnyWrapper> self);
        void VisitRuntimeExpressionWrapper(RuntimeExpressionAnyWrapper self);


        void VisitContact(OpenApiContact self);


        void VisitLicense(OpenApiLicense self);


        void VisitEncodings(IDictionary<string, OpenApiEncoding> self);
        void VisitEncoding(KeyValuePair<string, OpenApiEncoding> self);
        void VisitEncoding(OpenApiEncoding self);


        void VisitParameters(IList<OpenApiParameter> self);
        void VisitParameters(IDictionary<string, OpenApiParameter> self);
        void VisitParameter(KeyValuePair<string, OpenApiParameter> self);
        void VisitParameter(OpenApiParameter self);


        void VisitMediaTypes(IDictionary<string, OpenApiMediaType> self);
        void VisitMediaType(KeyValuePair<string, OpenApiMediaType> self);
        void VisitMediaType(OpenApiMediaType self);


        void VisitPathItems(Dictionary<RuntimeExpression, OpenApiPathItem> self);
        void VisitPathItem(KeyValuePair<RuntimeExpression, OpenApiPathItem> self);
        void VisitPathItem(OpenApiPathItem self);



        void VisitServers(IList<OpenApiServer> self);
        void VisitServer(OpenApiServer self);


        void VisitVariables(IDictionary<string, OpenApiServerVariable> self);
        void VisitVariable(KeyValuePair<string, OpenApiServerVariable> self);
        void VisitVariable(OpenApiServerVariable self);


        void VisitRuntimeExpression(RuntimeExpression self);
        void VisitPathItem(KeyValuePair<string, OpenApiPathItem> self);
    }


}
