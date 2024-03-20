using Bb.Codings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.RegularExpressions;
using Bb.Extensions;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Expressions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Bb.OpenApiServices
{


    public class OpenApiGenerateServices : OpenApiGeneratorCSharpBase
    {

        public OpenApiGenerateServices(string contract, string artifactName, string @namespace, params string[] usings)
            : base(artifactName, @namespace, usings.Concat(
                    "Microsoft.AspNetCore.Mvc",
                    "Newtonsoft.Json",
                    "Newtonsoft.Json.Linq"))
        {
            this._contract = contract;
        }

        public override CSMemberDeclaration? VisitDocument(OpenApiDocument self)
        {

            Context.AddAssemblyName("System.ComponentModel.Annotations");

            foreach (var item in self.Paths)
                item.Accept(this);

            return null;

        }
        public virtual string GetServiceRoute(KeyValuePair<string, OpenApiPathItem> self)
        {
            string key = self.Key;
            string pathController = $"/api/mock/{_contract}{key}";
            return pathController;
        }

        public override CSMemberDeclaration? VisitPathItem(KeyValuePair<string, OpenApiPathItem> self)
        {

            string key = self.Key;
            string pathController = GetServiceRoute(self);

            OpenApiPathItem value = self.Value;

            var _n = ConvertToClassName(key);
            string name = _n + "Controller";
            string typeLogger = $"ILogger<{name}>";

            CsClassDeclaration controller = new CsClassDeclaration(name)
                .Base("ControllerBase")
                .Ctor(ctor =>
                {

                    ctor.Parameter("logger", typeLogger);
                    //ctor.Parameter("trace", "ServiceTrace");

                    ctor.Body(b =>
                    {
                        b.Set("_logger".Identifier(), "logger".Identifier());
                        //b.Set("_trace".Identifier(), "trace".Identifier());
                    });
                    ;
                })
                ;

            controller.Attribute("ApiController");
            controller.Attribute("Route")
                .Argument(pathController.Literal())
                ;

            foreach (var item in value.Operations)
            {
                var r = item.Accept(this);
                if (r != null)
                    controller.Add(r);
            }

            var cs = CreateArtifact(name);
            var ns = CreateNamespace(cs);
            ns.DisableWarning("CS8618", "CS1591");

            ns.Add(controller);

            controller.Field("_logger", typeLogger, f =>
             {
                 f.IsPrivate()
                 ;
             });

            //controller.Field("_trace", "ServiceTrace", f =>
            //{
            //    f.IsPrivate()
            //    ;
            //});

            Context.AppendDocument("Controllers", name + ".cs", cs.Code().ToString());

            return null;

        }

        public override CSMemberDeclaration? VisitOperation(KeyValuePair<OperationType, OpenApiOperation> self)
        {

            string typeReturn = null;
            OperationType key = self.Key;
            OpenApiOperation value = self.Value;

            var method = new CsMethodDeclaration(key.ToString().ConvertToCharpName());
            key.ToString().ApplyHttpMethod(method);

            if (!string.IsNullOrEmpty(value.Description))
                method.Documentation.Summary(() => value.Description);

            using (var c = _tree.Stack(method))
            {

                if (value.RequestBody != null)
                    foreach (var item2 in value.RequestBody.Content)
                    {
                        var name = item2.Value.Schema.ResolveType(out var valueResult);
                        if (name != null)
                        {
                            if (valueResult != null)
                            {
                                Stop();
                            }

                            var p = new CsParameterDeclaration("queryBody", name);
                            p.Attribute("FromBody");
                            var description = item2.Value.Schema.ResolveDescription();
                            if (!string.IsNullOrEmpty(description))
                                method.Documentation.Parameter(name, () => description);
                            method.Add(p);
                        }

                    }

                foreach (var item1 in value.Parameters)
                {
                    var p = item1.Accept(this);
                    if (p != null)
                        method.Add(p);
                }

                foreach (KeyValuePair<string, OpenApiResponse> item2 in value.Responses.OrderBy(c => c.Key))
                    item2.Value.Accept(this);

                typeReturn = ResolveReturnType(value, method, "2");

                // Attribute ProduceResponse
                foreach (var item2 in value.Responses)
                {

                    var t2 = item2.Value.Content.FirstOrDefault().Value;
                    if (t2 != null)
                    {

                        method.Attribute("ProducesResponseType", a =>
                        {

                            var code = GeneratorHelper.CodeHttp(item2.Key);
                            if (code != null)
                                a.Argument(SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("StatusCodes"), code));
                            
                            else
                            {
                                var value = int.Parse(item2.Key);
                                a.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(value)));
                            }

                            OpenApiSchema t = t2.Schema;
                            OpenApiSchema? schema2 = null;
                            var v = t?.ResolveType(out schema2);
                            if (v != null)
                            {
                                if (schema2 != null)
                                {
                                    Stop();
                                }

                                a.Argument("Type", CodeHelper.TypeOf(v));
                            }
                        });

                    }
                }

            }

            method.Body(code =>
            {
                GenerateMethod(self, code, typeReturn, method);

            });

            return method;

        }

        protected virtual void GenerateMethod(KeyValuePair<OperationType, OpenApiOperation> self, CodeBlock code, string typeReturn, CsMethodDeclaration method)
        {
            code.Thrown("NotImplementedException".NewObject());
        }

        protected string ResolveReturnType(OpenApiOperation self, CsMethodDeclaration method, string code)
        {

            string result = null;

            List<KeyValuePair<string, OpenApiSchema>> _resultTypes = new List<KeyValuePair<string, OpenApiSchema>>();
            foreach (var item2 in self.Responses)
            {

                if (item2.Key.StartsWith(code))
                {
                    var t2 = item2.Value.Content.FirstOrDefault().Value;
                    if (t2 != null)
                    {
                        OpenApiSchema t = t2.Schema;
                        var v = t.ResolveType(out var schema2);
                        if (v != null)
                        {
                            if (schema2 != null)
                            {
                                Stop();
                            }
                            _resultTypes.Add(new KeyValuePair<string, OpenApiSchema>(item2.Key, t));
                        }
                    }
                }

            }

            var item3 = _resultTypes.OrderBy(c => c.Key).FirstOrDefault().Value;
            if (item3 != null)
            {

                result = item3.ResolveType(out var schema2);

                if (code == "2")
                {

                    if (schema2 != null)
                    {
                        Stop();
                    }

                    var typeReturn = CodeHelper.BuildTypename("ActionResult", result).ToString();
                    method.ReturnType(typeReturn);


                    if (item3.IsJson())
                        _tree.Current.Attribute("Produces")
                            .Argument("application/json".Literal());
                }
            }
            else
            {
                var typeReturn = "IActionResult";
                method.ReturnType(typeReturn);
            }

            return result;

        }

        public override CSMemberDeclaration? VisitParameter(OpenApiParameter self)
        {

            var n = self.Name;
            var t = self.Schema.ConvertTypeName();

            //if (_scharpReservedKeyword.Contains(n))
            //    n = "@" + n;

            var p = new CsParameterDeclaration(n, t);

            p.ApplyAttributes(self);

            if (!string.IsNullOrEmpty(self.Description))
            {
                var method = this._tree.Current as CsMethodDeclaration;
                method.Documentation.Parameter(n, () => self.Description);
            }

            return p;

        }

        public override CSMemberDeclaration? VisitResponse(KeyValuePair<string, OpenApiResponse> self)
        {

            var key = self.Key;
            var value = self.Value;

            switch (key[0])
            {

                case '2':
                    break;

                case '4':
                    if (!error400.HasValue)
                        error400 = new KeyValuePair<string, OpenApiResponse>(key, value);
                    else
                    {
                        if (int.Parse(error400.Value.Key) > int.Parse(key))
                            error400 = new KeyValuePair<string, OpenApiResponse>(key, value);
                    }
                    break;

                case '5':
                    if (!error500.HasValue)
                        error500 = new KeyValuePair<string, OpenApiResponse>(key, value);
                    else
                    {
                        if (int.Parse(error500.Value.Key) > int.Parse(key))
                            error500 = new KeyValuePair<string, OpenApiResponse>(key, value);
                    }
                    break;

                default:
                    Stop();
                    break;

            }

            return null;

        }

        public override CSMemberDeclaration? VisitComponents(OpenApiComponents self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitInfo(OpenApiInfo self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitServer(OpenApiServer self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitTag(OpenApiTag self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitEnumPrimitive(IOpenApiPrimitive self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitMediaType(OpenApiMediaType self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitMediaTypes(IDictionary<string, OpenApiMediaType> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitMediaType(KeyValuePair<string, OpenApiMediaType> self)
        {
            return null;
        }

        public virtual string ConvertToClassName(string text)
        {

            var _n = text.Trim('/');
            var regex = new Regex(@"{|}");
            var items = regex.Matches(_n);
            foreach (Match item in items)
                _n = _n.Replace(item.Value, string.Empty);

            _n = _n.Trim('/');

            StringBuilder stringBuilder = new StringBuilder(_n?.Length ?? 0);
            if (text != null)
            {
                char c = '\0';
                for (int i = 0; i < text.Length; i++)
                {
                    char c2 = text[i];
                    if (!char.IsLetterOrDigit(c2))
                    {
                        c2 = '/';
                    }

                    if (stringBuilder.Length == 0)
                    {
                        c2 = char.ToUpper(c2);
                    }
                    else
                    {
                        if (c2 == '/')
                        {
                            c = c2;
                            continue;
                        }

                        c2 = ((c != '/') ? char.ToLower(c2) : char.ToUpper(c2));
                    }

                    stringBuilder.Append(c2);
                    c = c2;
                }
            }

            return stringBuilder.ToString().Trim().Trim('/');
        }

        public override CSMemberDeclaration? VisitCallback(OpenApiCallback self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitCallbacks(IDictionary<string, OpenApiCallback> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitContact(OpenApiContact self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitEncoding(KeyValuePair<string, OpenApiEncoding> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitEncoding(OpenApiEncoding self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitEncodings(IDictionary<string, OpenApiEncoding> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitExample(OpenApiExample self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitExamples(IDictionary<string, OpenApiExample> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitExtension(KeyValuePair<string, IOpenApiExtension> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitExtension(IOpenApiExtension self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitExtensions(IDictionary<string, IOpenApiExtension> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitExternalDocs(OpenApiExternalDocs self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitHeader(KeyValuePair<string, OpenApiHeader> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitHeader(OpenApiHeader self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitHeaders(IDictionary<string, OpenApiHeader> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitLicense(OpenApiLicense self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitLink(KeyValuePair<string, OpenApiLink> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitLink(OpenApiLink self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitLinks(IDictionary<string, OpenApiLink> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitOperation(OpenApiOperation self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitOperations(IDictionary<OperationType, OpenApiOperation> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitParameter(KeyValuePair<string, OpenApiParameter> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitParameters(IDictionary<string, OpenApiParameter> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitParameters(IList<OpenApiParameter> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitPaths(OpenApiPaths self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitPathItem(KeyValuePair<RuntimeExpression, OpenApiPathItem> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitPathItem(OpenApiPathItem self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitPathItems(Dictionary<RuntimeExpression, OpenApiPathItem> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitReference(OpenApiReference self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRequestBodies(IDictionary<string, OpenApiRequestBody> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRequestBody(KeyValuePair<string, OpenApiRequestBody> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRequestBody(OpenApiRequestBody self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitResponse(OpenApiResponse self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitResponses(IDictionary<string, OpenApiResponse> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRuntimeExpression(RuntimeExpression self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRuntimeExpressionWrapper(KeyValuePair<string, RuntimeExpressionAnyWrapper> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRuntimeExpressionWrapper(RuntimeExpressionAnyWrapper self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitRuntimeExpressionWrappers(Dictionary<string, RuntimeExpressionAnyWrapper> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSchema(OpenApiSchema self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSchemas(IList<OpenApiSchema> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSchemas(IDictionary<string, OpenApiSchema> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSchema(KeyValuePair<string, OpenApiSchema> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSecurityRequirement(OpenApiSecurityRequirement self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSecurityRequirements(IList<OpenApiSecurityRequirement> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSecurityScheme(OpenApiSecurityScheme self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSecurityScheme(KeyValuePair<OpenApiSecurityScheme, IList<string>> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitSecuritySchemes(IDictionary<string, OpenApiSecurityScheme> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitServers(IList<OpenApiServer> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitTags(IList<OpenApiTag> self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitVariable(OpenApiServerVariable self)
        {
            return null;
        }

        public override CSMemberDeclaration? VisitVariables(IDictionary<string, OpenApiServerVariable> self)
        {
            return null;
        }

        private KeyValuePair<string, OpenApiResponse>? error500 = null;
        private KeyValuePair<string, OpenApiResponse>? error400 = null;
        protected readonly string _contract;

    }


}