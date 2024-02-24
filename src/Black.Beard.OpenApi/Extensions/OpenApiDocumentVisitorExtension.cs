using Bb.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Diagnostics;

namespace Bb.Extensions
{
    public static class OpenApiDocumentVisitorExtension
    {

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiMediaType self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitMediaType(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiMediaType> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitMediaTypes(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiMediaType> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitMediaType(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiDocument self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitDocument(self);
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiTag self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitTag(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IList<OpenApiSchema> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSchemas(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiServer self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitServer(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiSecurityScheme self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSecurityScheme(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiSchema> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitSchema(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiSchema self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSchema(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiInfo self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitInfo(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiComponents self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushPath("components"))
                using (visitor.PushChildren(self))
                    visitor.VisitComponents(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiParameter> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("parameters"))
                    visitor.VisitParameters(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>        
        public static void Accept(this KeyValuePair<string, OpenApiParameter> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitParameter(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IList<OpenApiParameter> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("parameters"))
                    visitor.VisitParameters(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiParameter self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitParameter(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiPaths self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("paths"))
                    visitor.VisitPaths(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiSchema> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSchemas(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IOpenApiPrimitive self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitEnumPrimitive(self);
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiExternalDocs self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitExternalDocs(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiSecurityRequirement self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSecurityRequirement(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IList<OpenApiSecurityRequirement> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSecurityRequirements(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IList<OpenApiServer> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitServers(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IList<OpenApiTag> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitTags(self);
        }



        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiCallback> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitCallbacks(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiCallback> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitCallback(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiCallback self, IOpenApiVisitor visitor)
        {
            visitor.VisitCallback(self);
        }



        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiExample> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitExamples(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiExample> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitExample(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiExample self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitExample(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, IOpenApiExtension> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitExtensions(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, IOpenApiExtension> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitExtension(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IOpenApiExtension self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitExtension(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiHeader> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitHeaders(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiHeader> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitHeader(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiHeader self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitHeader(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiLink> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitLinks(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiLink> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitLink(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiLink self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitLink(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this Dictionary<RuntimeExpression, OpenApiPathItem> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("paths"))
                    visitor.VisitPathItems(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<RuntimeExpression, OpenApiPathItem> self, IOpenApiVisitor visitor)
        {
            if (self.Key != null)
                using (visitor.PushChildren(self))
                    visitor.VisitPathItem(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this RuntimeExpression self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitRuntimeExpression(self);
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this Dictionary<string, RuntimeExpressionAnyWrapper> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitRuntimeExpressionWrappers(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, RuntimeExpressionAnyWrapper> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitRuntimeExpressionWrapper(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this RuntimeExpressionAnyWrapper self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitRuntimeExpressionWrapper(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiServerVariable> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitVariables(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiServerVariable> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitVariable(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiServerVariable self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitVariable(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiPathItem self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitPathItem(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiPathItem> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath($"[{self.Key}]"))
                    visitor.VisitPathItem(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiRequestBody> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitRequestBodies(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiRequestBody> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitRequestBody(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiRequestBody self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitRequestBody(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiResponse> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitResponses(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiResponse> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitResponse(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiResponse self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitResponse(self);
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiSecurityScheme> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("securitySchemes"))
                    visitor.VisitSecuritySchemes(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<OpenApiSecurityScheme, IList<string>> self, IOpenApiVisitor visitor)
        {
            if (self.Key != null)
                using (visitor.PushChildren(self))
                    visitor.VisitSecurityScheme(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiReference self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitReference(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiContact self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitContact(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiLicense self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitLicense(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<string, OpenApiEncoding> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitEncodings(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<string, OpenApiEncoding> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    visitor.VisitEncoding(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiEncoding self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitEncoding(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this IDictionary<OperationType, OpenApiOperation> self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitOperations(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this KeyValuePair<OperationType, OpenApiOperation> self, IOpenApiVisitor visitor)
        {
            if (!string.IsNullOrEmpty(self.Key.ToString()))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key.ToString()))
                    visitor.VisitOperation(self);
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiVisitor"/>.</param>
        public static void Accept(this OpenApiOperation self, IOpenApiVisitor visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    visitor.VisitOperation(self);
        }
                   

    }


}
