using Bb.Analysis.DiagTraces;
using Bb.Extensions;
using Bb.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Bb.OpenApiServices
{

    public class OpenApiValidator : OpenApiVisitorBase, IOpenApiVisitor, IServiceGenerator<OpenApiDocument>
    {

        public OpenApiValidator()
        {
            this._h = new HashSet<string>();
        }

        protected virtual void Initialize(ContextGenerator ctx)
        {
            _ctx = ctx;
            _diag = ctx.Diagnostics;
        }

        public void Parse(OpenApiDocument self, ContextGenerator ctx)
        {
            this._h.Clear();
            Initialize(ctx);
            self.Accept(this);
        }

        public override void VisitSchemas(IDictionary<string, OpenApiSchema> self)
        {

            foreach (KeyValuePair<string, OpenApiSchema> item in self)
                if (item.Value.IsEmptyType())
                    _diag.AddError(GetLocation, item.Key, $"Should specify a type");

            base.VisitSchemas(self);

        }

        public override void VisitSchema(OpenApiSchema self)
        {

            if (self.Properties.Count == 0)
                CheckType(self);

            else
            {

                using (PushContext("class"))
                {

                    if (self.Reference != null)
                        new KeyValuePair<string, OpenApiSchema>(self.Reference.Id, self).Accept(this);

                    using (PushContext("property"))
                        foreach (var item in self.Properties)
                            item.Accept(this);

                }

            }

            base.VisitSchema(self);

        }

        public override void VisitSchema(KeyValuePair<string, OpenApiSchema> self)
        {

            string key = self.Key;
            OpenApiSchema value = self.Value;
            string kind = this.ContextName;

            switch (kind)
            {

                case "enum":
                    VisitJsonSchemaForEnum(value);
                    break;

                case "class":
                    var typeName = value.Type;
                    if (typeName == null)
                    {

                        if (_h.Add(key))
                        {
                            if (value.Properties.Any())
                                VisitJsonSchemaForClass(self, "object");

                            else if (value.Items != null)
                                VisitJsonSchemaForClass(self, "array");

                            else
                            {
                                Stop();
                            }
                        }
                    }
                    else if (_h.Add(key))
                        VisitJsonSchemaForClass(self, "object");
                    break;

                case "property":
                    VisitJsonSchemaProperty(self);
                    break;

                default:
                    break;

            }

        }

        private void VisitJsonSchemaForClass(KeyValuePair<string, OpenApiSchema> self, string? typeName)
        {
            string key = self.Key;
            OpenApiSchema value = self.Value;

            if (typeName == "array")
            {
                if (value.Items != null)
                {
                    Stop();
                }
                else
                {
                    Stop();
                }
            }
            else if (typeName == "object")
            {

                using (PushContext("property"))
                    foreach (var item in value.Properties)
                        item.Accept(this);

                foreach (string item in value.Required)
                    if (!value.Properties.Any(c => c.Key == item))
                        _diag.AddError(GetLocation, item, $"the required property {item} is not found in the {key} object");

            }
            else
            {
                Stop();
            }
        }

        private void VisitJsonSchemaForEnum(OpenApiSchema self)
        {
            using (PushContext("enum"))
                foreach (IOpenApiAny item in self.Enum)
                    if (item is IOpenApiPrimitive p)
                        p.Accept(this);
                    else
                    {
                        Stop();
                    }
        }

        public void VisitJsonSchemaProperty(KeyValuePair<string, OpenApiSchema> self)
        {

            string propertyName = self.Key;
            OpenApiSchema value = self.Value;

            if (value.Type == null)
            {
                if (value.Reference == null)
                    _diag.AddError(GetLocation, "type", $"type or reference of the property {propertyName} should be specified");

                else if (value.Properties.Count > 0)
                    _diag.AddWarning(GetLocation, "type", $"type of the property {propertyName} should be 'object'");

                else if (value.Enum.Count > 0)
                    _diag.AddError(GetLocation, "type", $"type of the property {propertyName} should be specified");
            }
            else
            {
                var accept = CheckType(value);
                if (!accept.AcceptMinMaxValue)
                {

                    if (value.Maximum.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"Maximum is unexpected for the type {value.Type.ToLower()}");

                    if (value.Minimum.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"minimum is unexpected for the type {value.Type.ToLower()}");
                }
                else if (!accept.AcceptComma)
                {

                    if (value.Maximum.HasValue && HasComma(value.Maximum.Value))
                        _diag.AddError(GetLocation, value.Format, $"Maximum float with decimal is unexpected for the type {value.Type.ToLower()}");

                    if (value.Minimum.HasValue && HasComma(value.Minimum.Value))
                        _diag.AddError(GetLocation, value.Format, $"minimum float with decimal is unexpected for the type {value.Type.ToLower()}");
                }
                if (!accept.AcceptMinMaxItems)
                {
                    if (value.UniqueItems.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"uniqueItems is unexpected for the type {value.Type.ToLower()}");

                    if (value.MaxItems.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"maxItems is unexpected for the type {value.Type.ToLower()}");

                    if (value.MinItems.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"minItems is unexpected for the type {value.Type.ToLower()}");
                }
                if (!accept.AcceptMinMaxLength)
                {
                    if (value.MaxLength.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"maxLength is unexpected for the type {value.Type.ToLower()}");

                    if (value.MinLength.HasValue)
                        _diag.AddError(GetLocation, value.Format, $"minLength is unexpected for the type {value.Type.ToLower()}");
                }
            }
        }

        private TextLocation GetLocation => new TextLocation( new LocationPath(GetPath), null) { Filename = this._ctx.ContractDocumentFilename };



        public override void VisitEnumPrimitive(IOpenApiPrimitive self)
        {

            switch (self.PrimitiveType)
            {
                case PrimitiveType.Integer:
                case PrimitiveType.String:
                case PrimitiveType.Long:
                case PrimitiveType.Float:
                case PrimitiveType.Double:
                case PrimitiveType.Byte:
                case PrimitiveType.Binary:
                case PrimitiveType.Boolean:
                case PrimitiveType.Date:
                case PrimitiveType.DateTime:
                case PrimitiveType.Password:
                    break;
                default:
                    Stop();
                    break;
            }

        }



        private bool HasComma(decimal value)
        {
            return decimal.Round(value) != value;
        }

        private ExceptedProperties CheckType(OpenApiSchema self)
        {

            ExceptedProperties result = new ExceptedProperties();

            if (string.IsNullOrEmpty(self.Type))
                _diag.AddError(GetLocation, string.Empty, $"the type is missing for { this.GetPath}");

            switch (self.Type.ToLower())
            {

                case "boolean":
                    break;

                case "integer":
                    result.AcceptMinMaxValue = true;
                    if (!string.IsNullOrEmpty(self.Format))
                        switch (self.Format)
                        {
                            case "int32":
                            case "int64":
                                break;
                            default:
                                _diag.AddError(GetLocation, self.Format, $"the format {self.Format} is not managed for {self.Type.ToLower()}");
                                break;
                        }
                    break;

                case "number":
                    result.AcceptMinMaxValue = true;
                    result.AcceptComma = true;
                    if (!string.IsNullOrEmpty(self.Format))
                        switch (self.Format)
                        {
                            case "double":
                            case "float":
                                break;
                            default:
                                _diag.AddError(GetLocation, self.Format, $"the format {self.Format} is not managed for {self.Type.ToLower()}");
                                break;
                        }
                    break;

                case "string":
                    result.AcceptMinMaxLength = true;
                    if (!string.IsNullOrEmpty(self.Format))
                        switch (self.Format)
                        {
                            case "email":
                            case "hostname":
                            case "ipv4":
                            case "ipv6":
                            case "uri":
                            case "binary":
                            case "byte":
                            case "password":
                            case "uuid":
                            case "date":
                            case "date-time":
                                break;
                            default:
                                _diag.AddError(GetLocation, self.Format, $"the format {self.Format} is not managed for string");
                                break;
                        }

                    if (self.Enum.Count > 0)
                        foreach (var item in self.Enum)
                            if (item is IOpenApiPrimitive primitive)
                                if (primitive.PrimitiveType != PrimitiveType.String)
                                {
                                    dynamic d = item as dynamic;
                                    _diag.AddError(GetLocation, self.Format, $"the enum value {d.Value} should be {primitive.PrimitiveType} type");
                                }

                    break;

                case "array":
                    result.AcceptMinMaxItems = true;
                    if (self.Items != null)
                    {
                        if (self.Items.Reference != null)
                            self.Items.Accept(this);

                        else if (self.IsEmptyType())
                            _diag.AddError(GetLocation, "", $"Array must specify a type");

                    }
                    else if (self.OneOf != null && self.OneOf.Count > 0)
                    {
                        Stop();
                    }
                    break;

                case "object":
                    break;

                //case JsonObjectType.Null:
                //case JsonObjectType.File:
                //case JsonObjectType.None:
                default:
                    _diag.AddError(GetLocation, self.Format, $"the format {self.Format} is not managed for string");
                    break;
            }

            return result;

        }

        public override void VisitOperation(KeyValuePair<OperationType, OpenApiOperation> self)
        {

            var key = self.Key.ToString();
            var value = self.Value;

            using (PushContext("parameters"))
                foreach (var item in value.Parameters)
                    item.Accept(this);

            if (value.RequestBody != null)
            {

                foreach (var item in value.RequestBody.Content)
                {

                    var t = item.Value.Schema;
                    var t1 = t.ConvertTypeName();

                    if (t1 == typeof(object))
                        using (PushContext("class"))
                            t.Accept(this);

                    else if (t1 == typeof(Array))
                    {
                        if (t.Items != null)
                        {

                            if (t.Items.ResolveName() == null)
                                using (PushContext("class"))
                                    t.Items.Accept(this);

                        }
                        else if (t.Reference != null)
                        {

                        }
                        else
                        {
                            Stop();
                        }
                    }
                    else
                    {
                        Stop();
                    }

                }

            }

            using (PushContext("responses"))
                foreach (var item in value.Responses)
                    item.Accept(this);

        }

        public override void VisitInfo(OpenApiInfo self)
        {

        }

        public override void VisitResponse(KeyValuePair<string, OpenApiResponse> self)
        {

            string key = self.Key;
            OpenApiResponse value = self.Value;

            if (value.Content != null && value.Content.Count > 0)
                foreach (var item in value.Content)
                    item.Accept(this);
            else
                _diag.AddWarning(GetLocation, "content", $"the http result code '{key}' should contains mime result type.");

            if (value.Headers != null)
                foreach (var item in value.Headers)
                {
                    Stop();
                }

        }

        public override void VisitMediaType(KeyValuePair<string, OpenApiMediaType> self)
        {

            if (self.Key == "application/json")
                self.Value.Schema.Accept(this);

            else
            {
                Stop();
            }

        }

        public override void VisitParameter(OpenApiParameter self)
        {

            string key = self.Name;

            self.Schema.Accept(this);

            if (!self.In.HasValue)
                _diag.AddError(GetLocation, "in", $"the parameter '{key}', should specified 'In value' or the value is invalid.");

            else
                switch (self.In.Value)
                {
                    case ParameterLocation.Query:
                        if (self.Style == ParameterStyle.PipeDelimited || self.Style == ParameterStyle.DeepObject)
                            _diag.AddWarning(GetLocation, "style", $"for parameter '{key}', the style should not be '{self.Style}'");
                        if (self.Style == ParameterStyle.SpaceDelimited && !self.Explode)
                            _diag.AddWarning(GetLocation, "style", $"for parameter '{key}', the style should not be '{self.Style.ToString()}'");
                        break;

                    case ParameterLocation.Header:
                        break;

                    case ParameterLocation.Path:
                        if (self.Style != ParameterStyle.Simple)
                            _diag.AddWarning(GetLocation, "style", $"for parameter '{key}', the style should be simple for parameter specified by path");

                        if (!self.Required)
                            _diag.AddWarning(GetLocation, "required", $"for parameter '{key}', the style required");
                        break;

                    case ParameterLocation.Cookie:
                        break;

                    default:
                        break;
                }

        }

        private HashSet<string> _h;

        protected ContextGenerator _ctx;
        protected ScriptDiagnostics _diag;

    }


    public struct ExceptedProperties
    {
        public bool AcceptMinMaxValue { get; set; }
        public bool AcceptMinMaxItems { get; set; }
        public bool AcceptMinMaxLength { get; set; }
        public bool AcceptComma { get; internal set; }
    }

}