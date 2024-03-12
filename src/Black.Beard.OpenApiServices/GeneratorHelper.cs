using Bb.Codings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Models;

namespace Bb.OpenApiServices
{

    public static class GeneratorHelper
    {

        public static IdentifierNameSyntax CodeHttp(this string codeKey)
        {
                      
            switch (codeKey)
            {
                case "100": return SyntaxFactory.IdentifierName("Status100Continue");
                case "101": return SyntaxFactory.IdentifierName("Status101SwitchingProtocols");
                case "102": return SyntaxFactory.IdentifierName("Status102Processing");

                //  Ok
                case "200": return SyntaxFactory.IdentifierName("Status200OK");
                case "201": return SyntaxFactory.IdentifierName("Status201Created");
                case "202": return SyntaxFactory.IdentifierName("Status202Accepted");
                case "203": return SyntaxFactory.IdentifierName("Status203NonAuthoritative");
                case "204": return SyntaxFactory.IdentifierName("Status204NoContent");
                case "205": return SyntaxFactory.IdentifierName("Status205ResetContent");
                case "206": return SyntaxFactory.IdentifierName("Status206PartialContent");
                case "207": return SyntaxFactory.IdentifierName("Status207MultiStatus");
                case "208": return SyntaxFactory.IdentifierName("Status208AlreadyReported");
                case "226": return SyntaxFactory.IdentifierName("Status226IMUsed");

                // 
                case "300": return SyntaxFactory.IdentifierName("Status300MultipleChoices");
                case "301": return SyntaxFactory.IdentifierName("Status301MovedPermanently");
                case "302": return SyntaxFactory.IdentifierName("Status302Found");
                case "303": return SyntaxFactory.IdentifierName("Status303SeeOther");
                case "304": return SyntaxFactory.IdentifierName("Status304NotModified");
                case "305": return SyntaxFactory.IdentifierName("Status305UseProxy");
                case "306": return SyntaxFactory.IdentifierName("Status306SwitchProxy");                      // RFC 2616, removed
                case "307": return SyntaxFactory.IdentifierName("Status307TemporaryRedirect");
                case "308": return SyntaxFactory.IdentifierName("Status308PermanentRedirect");

                // request Error
                case "400": return SyntaxFactory.IdentifierName("Status400BadRequest");
                case "401": return SyntaxFactory.IdentifierName("Status401Unauthorized");
                case "402": return SyntaxFactory.IdentifierName("Status402PaymentRequired");
                case "403": return SyntaxFactory.IdentifierName("Status403Forbidden");
                case "404": return SyntaxFactory.IdentifierName("Status404NotFound");
                case "405": return SyntaxFactory.IdentifierName("Status405MethodNotAllowed");
                case "406": return SyntaxFactory.IdentifierName("Status406NotAcceptable");
                case "407": return SyntaxFactory.IdentifierName("Status407ProxyAuthenticationRequired");
                case "408": return SyntaxFactory.IdentifierName("Status408RequestTimeout");
                case "409": return SyntaxFactory.IdentifierName("Status409Conflict");
                case "410": return SyntaxFactory.IdentifierName("Status410Gone");
                case "411": return SyntaxFactory.IdentifierName("Status411LengthRequired");
                case "412": return SyntaxFactory.IdentifierName("Status412PreconditionFailed");
                case "413": return SyntaxFactory.IdentifierName("Status413RequestEntityTooLarge");            // RFC 2616, renamed
                //case "413": return SyntaxFactory.IdentifierName("Status413PayloadTooLarge");                  // RFC 7231
                case "414": return SyntaxFactory.IdentifierName("Status414RequestUriTooLong");                // RFC 2616, renamed
                //case "414": return SyntaxFactory.IdentifierName("Status414UriTooLong");                       // RFC 7231
                case "415": return SyntaxFactory.IdentifierName("Status415UnsupportedMediaType");
                case "416": return SyntaxFactory.IdentifierName("Status416RequestedRangeNotSatisfiable");     // RFC 2616, renamed
                //case "416": return SyntaxFactory.IdentifierName("Status416RangeNotSatisfiable");              // RFC 7233
                case "417": return SyntaxFactory.IdentifierName("Status417ExpectationFailed");
                case "418": return SyntaxFactory.IdentifierName("Status418ImATeapot");
                case "419": return SyntaxFactory.IdentifierName("Status419AuthenticationTimeout");            // Not defined in any RFC
                case "421": return SyntaxFactory.IdentifierName("Status421MisdirectedRequest");
                case "422": return SyntaxFactory.IdentifierName("Status422UnprocessableEntity");
                case "423": return SyntaxFactory.IdentifierName("Status423Locked");
                case "424": return SyntaxFactory.IdentifierName("Status424FailedDependency");
                case "426": return SyntaxFactory.IdentifierName("Status426UpgradeRequired");
                case "428": return SyntaxFactory.IdentifierName("Status428PreconditionRequired");
                case "429": return SyntaxFactory.IdentifierName("Status429TooManyRequests");
                case "431": return SyntaxFactory.IdentifierName("Status431RequestHeaderFieldsTooLarge");
                case "451": return SyntaxFactory.IdentifierName("Status451UnavailableForLegalReasons");

                // Internals Error
                case "500": return SyntaxFactory.IdentifierName("Status500InternalServerError");
                case "501": return SyntaxFactory.IdentifierName("Status501NotImplemented");
                case "502": return SyntaxFactory.IdentifierName("Status502BadGateway");
                case "503": return SyntaxFactory.IdentifierName("Status503ServiceUnavailable");
                case "504": return SyntaxFactory.IdentifierName("Status504GatewayTimeout");
                case "505": return SyntaxFactory.IdentifierName("Status505HttpVersionNotsupported");
                case "506": return SyntaxFactory.IdentifierName("Status506VariantAlsoNegotiates");
                case "507": return SyntaxFactory.IdentifierName("Status507InsufficientStorage");
                case "508": return SyntaxFactory.IdentifierName("Status508LoopDetected");
                case "510": return SyntaxFactory.IdentifierName("Status510NotExtended");
                case "511": return SyntaxFactory.IdentifierName("Status511NetworkAuthenticationRequired");

            }

            return null;

        }


        public static void ApplyHttpMethod(this OperationType operationType, CsMethodDeclaration method)
        {

            switch (operationType)
            {
                case OperationType.Get:
                    method.Attribute("HttpGet");
                    break;

                case OperationType.Put:
                    method.Attribute("HttpPut");
                    break;

                case OperationType.Post:
                    method.Attribute("HttpPost");
                    break;

                case OperationType.Delete:
                    method.Attribute("HttpDelete");
                    break;

                case OperationType.Options:
                    method.Attribute("HttpOptions");
                    break;

                case OperationType.Patch:
                    method.Attribute("Patch");
                    break;

                case OperationType.Head:
                    CodeHelper.Stop();
                    break;

                case OperationType.Trace:
                    CodeHelper.Stop();
                    break;

                default:
                    CodeHelper.Stop();
                    break;
            }

        }

        public static void ApplyHttpMethod(this string keyText, CsMethodDeclaration method)
        {

            var key = keyText.ToLower();
            switch (key)
            {

                case "get":
                    method.Attribute("HttpGet");
                    break;

                case "post":
                    method.Attribute("HttpPost");
                    break;

                case "delete":
                    method.Attribute("HttpDelete");
                    break;

                case "options":
                    method.Attribute("HttpOptions");
                    break;

                case "patch":
                    method.Attribute("Patch");
                    break;

                case "put":
                    method.Attribute("HttpPut");
                    break;

                default:
                    CodeHelper.Stop();
                    break;

            }


        }


        public static bool ApplyAttributes(this CsParameterDeclaration self, OpenApiParameter source)
        {
            if (source.In.HasValue)
                switch (source.In.Value)
                {

                    case ParameterLocation.Query:
                        self.Attribute("FromQuery");
                        return true;

                    case ParameterLocation.Header:
                        self.Attribute("FromHeader");
                        return true;

                    case ParameterLocation.Path:
                        self.Attribute("FromRoute");
                        return true;

                    case ParameterLocation.Cookie:
                        CodeHelper.Stop();
                        return true;

                    default:
                        CodeHelper.Stop();
                        break;

                }

            return false;

        }

        /// <summary>
        ///generate random code
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomCode(int length = 4, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            var stringChars = new char[length];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];
            var code = new string(stringChars);
            return code;
        }

    }


}