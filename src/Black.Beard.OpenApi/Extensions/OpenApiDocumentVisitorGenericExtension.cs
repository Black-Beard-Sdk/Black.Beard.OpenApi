using Bb.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Expressions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Diagnostics;

namespace Bb.Extensions
{

    /// <summary>
    /// Extension for implement visitor pattern on OpenApiDocument
    /// </summary>
    public static class OpenApiDocumentVisitorGenericExtension
    {

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiMediaType self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitMediaType(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiMediaType> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitMediaTypes(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiMediaType> self, IOpenApiGenericVisitor<T> visitor)
        {

            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitMediaType(self);
            return default;        
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiDocument self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitDocument(self);
            return default;
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiTag self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitTag(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IList<OpenApiSchema> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitSchemas(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiServer self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitServer(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiSecurityScheme self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitSecurityScheme(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiSchema> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitSchema(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiSchema self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitSchema(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiInfo self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitInfo(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiComponents self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("components"))
                    return visitor.VisitComponents(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiParameter> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("parameters"))
                    return visitor.VisitParameters(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>        
        public static T? Accept<T>(this KeyValuePair<string, OpenApiParameter> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitParameter(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IList<OpenApiParameter> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("parameters"))
                    return visitor.VisitParameters(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiParameter self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitParameter(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiPaths self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("paths"))
                    return visitor.VisitPaths(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiSchema> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitSchemas(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IOpenApiPrimitive self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitEnumPrimitive(self);
            return default;
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiExternalDocs self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitExternalDocs(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiSecurityRequirement self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitSecurityRequirement(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IList<OpenApiSecurityRequirement> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitSecurityRequirements(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IList<OpenApiServer> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitServers(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IList<OpenApiTag> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitTags(self);
            return default;
        }



        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiCallback> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitCallbacks(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiCallback> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitCallback(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiCallback self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitCallback(self);
            return default;
        }



        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiExample> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitExamples(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiExample> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitExample(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiExample self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitExample(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, IOpenApiExtension> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitExtensions(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, IOpenApiExtension> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitExtension(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IOpenApiExtension self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitExtension(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiHeader> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitHeaders(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiHeader> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitHeader(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiHeader self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitHeader(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiLink> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitLinks(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiLink> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitLink(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiLink self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitLink(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this Dictionary<RuntimeExpression, OpenApiPathItem> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("paths"))
                    return visitor.VisitPathItems(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiPathItem> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath($"[{self.Key}]"))
                    return visitor.VisitPathItem(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<RuntimeExpression, OpenApiPathItem> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self.Key != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitPathItem(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this RuntimeExpression self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitRuntimeExpression(self);
            return default;
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this Dictionary<string, RuntimeExpressionAnyWrapper> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitRuntimeExpressionWrappers(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, RuntimeExpressionAnyWrapper> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitRuntimeExpressionWrapper(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this RuntimeExpressionAnyWrapper self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitRuntimeExpressionWrapper(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiServerVariable> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitVariables(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiServerVariable> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitVariable(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiServerVariable self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitVariable(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiPathItem self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitPathItem(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiRequestBody> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitRequestBodies(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiRequestBody> self, IOpenApiGenericVisitor<T> visitor)
        {

            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitRequestBody(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiRequestBody self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitRequestBody(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiResponse> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitResponses(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiResponse> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitResponse(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiResponse self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitResponse(self);
            return default;
        }


        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiSecurityScheme> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath("securitySchemes"))
                    return visitor.VisitSecuritySchemes(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<OpenApiSecurityScheme, IList<string>> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key?.Name))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key.Name))
                    return visitor.VisitSecurityScheme(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiReference self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitReference(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiContact self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitContact(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiLicense self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitLicense(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<string, OpenApiEncoding> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitEncodings(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<string, OpenApiEncoding> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (!string.IsNullOrEmpty(self.Key))
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key))
                    return visitor.VisitEncoding(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiEncoding self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitEncoding(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this IDictionary<OperationType, OpenApiOperation> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitOperations(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this KeyValuePair<OperationType, OpenApiOperation> self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self.Value != null)
                using (visitor.PushChildren(self))
                using (visitor.PushPath(self.Key.ToString()))
                    return visitor.VisitOperation(self);
            return default;
        }

        /// <summary>
        /// Accepts to continue to parse the model with the specified visitor.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="visitor">The visitor <see cref="IOpenApiDocumentGenericVisitor<T>"/>.</param>
        public static T? Accept<T>(this OpenApiOperation self, IOpenApiGenericVisitor<T> visitor)
        {
            if (self != null)
                using (visitor.PushChildren(self))
                    return visitor.VisitOperation(self);
            return default;
        }


    }


}
