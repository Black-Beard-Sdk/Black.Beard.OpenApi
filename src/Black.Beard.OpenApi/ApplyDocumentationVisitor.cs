using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Bb
{

    internal class ApplyDocumentationVisitor : OpenApiVisitorBase
    {

        public static void ApplyDocumentation(OpenApiDocument document, XElement xmlDoc, string @namespace)
        {
            var visitor = new ApplyDocumentationVisitor(xmlDoc, @namespace);
            visitor.VisitDocument(document);
        }

        public ApplyDocumentationVisitor(XElement xmlDoc, string @namespace)
        {
            _xml = xmlDoc;
            _members = _xml.Element(XName.Get("members"));
            _namespace = @namespace;
        }

        public override void VisitDocument(OpenApiDocument self)
        {
            this._document = self;
            base.VisitDocument(self);
        }

        public override void VisitComponents(OpenApiComponents self)
        {
            base.VisitComponents(self);
        }

        public override void VisitOperation(OpenApiOperation self)
        {
            base.VisitOperation(self);
        }

        public override void VisitParameter(OpenApiParameter self)
        {
            base.VisitParameter(self);
        }

        public override void VisitPathItem(OpenApiPathItem self)
        {
            base.VisitPathItem(self);
        }

        public override void VisitSchema(KeyValuePair<string, OpenApiSchema> self)
        {

            StringBuilder sb1 = new StringBuilder(500);
            if (this.ContextName == "class")
                sb1 = ResolveDescriptionForClass(self);

            else if (this.ContextName == "property")
                sb1 = ResolveDescriptionForProperty(self);

            else
            {
                Stop();
            }

            if (sb1.Length > 0)
                self.Value.Description = XmlCommentsTextHelper.Humanize(sb1.ToString());

            base.VisitSchema(self);
        }


        private StringBuilder ResolveDescriptionForProperty(KeyValuePair<string, OpenApiSchema> self)
        {

            StringBuilder sb1 = new StringBuilder(500);

            XElement? element = ResolvePropertyDocumentation(self.Key);
            if (element != null)
            {

                var p = element.Element(XName.Get(SummaryTag));

                if (p != null)
                    sb1.Append(XmlCommentsTextHelper.Humanize(p.Value));

            }

            return sb1;

        }

        private StringBuilder ResolveDescriptionForClass(KeyValuePair<string, OpenApiSchema> self)
        {

            StringBuilder sb1 = new StringBuilder(500);
            StringBuilder sb2 = new StringBuilder(500);

            var element = ResolveTypeDocumentation(self.Key);
            if (element != null)
            {
                var p = element.Element(XName.Get(SummaryTag));
                if (p != null)
                {
                    sb1.Append(XmlCommentsTextHelper.Humanize(p.Value));
                    sb1.AppendLine(string.Empty);
                }
            }

            if (self.Value.Enum.Any())
            {
                var aa = ResolveEnumMemberDocumentation(self.Key);
                if (aa.Any())
                    foreach (var item in aa)
                    {
                        var p = item.Value.Element(XName.Get(SummaryTag));
                        if (p != null)
                        {


                            if (sb2.Length > 0)
                                sb2.AppendLine(string.Empty);

                            sb2.Append(item.Key);

                            var description = XmlCommentsTextHelper.Humanize(p.Value).Trim();

                            if (!string.IsNullOrEmpty(description) && description != item.Key)
                            {
                                sb2.Append(" : ");
                                sb2.Append(description);
                            }
                        }
                    }
            }

            if (sb2.Length > 0)
            {
                if (sb1.Length > 0)
                    sb1.AppendLine(string.Empty);
                sb1.Append(sb2);
            }
            return sb1;

        }

        #region Resolve documentation

        private XElement? ResolveTypeDocumentation(string name)
        {
            return ResolveDocumentation("T", name);
        }

        private XElement? ResolvePropertyDocumentation(string name)
        {
            return ResolveDocumentation("P", name);
        }

        private XElement? ResolveDocumentation(string type, string memberName)
        {

            foreach (var item in _members.Elements())
            {
                var attribute = item.Attribute(XName.Get("name"));
                var v = attribute.Value;
                if (attribute != null)
                {
                    string[] names = GetNames(v);
                    if (v.StartsWith(type + ":") && names[names.Length - 1] == memberName)
                        return item;
                }
            }

            return null;

        }

        private List<KeyValuePair<string, XElement>> ResolveEnumMemberDocumentation(string typeName)
        {

            var result = new List<KeyValuePair<string, XElement>>();

            foreach (var item in _members.Elements())
            {
                var attribute = item.Attribute(XName.Get("name"));
                if (attribute != null)
                {
                    var v = attribute.Value;
                    string[] names = GetNames(v);
                    if (v.StartsWith("F:") && names[names.Length - 2].Contains(typeName))
                        result.Add(new KeyValuePair<string, XElement>(names[names.Length - 1], item));
                }
            }

            return result;

        }

        private static string[] GetNames(string v)
        {
            var lexer = new lexer(v);
            var items = lexer.Read();
            return items.ToArray();
        }


        private class lexer
        {

            public lexer(string value)
            {
                this._value = value;
                this._sb = new StringBuilder(value.Length);
            }


            public List<string> Read()
            {
                List<string> names = new List<string>();
                this._index = 0;
                this._sb.Clear();
                this._current = '\0';

                while (ReadItem())
                {
                    names.Add(this._sb.ToString());
                    this._sb.Clear();
                }

                return names;

            }

            private bool ReadItem()
            {

                while (Next())
                    if (char.IsLetter(this._current))
                        this._sb.Append(this._current);
                    else
                        break;

                return this._sb.Length > 0;

            }

            private bool Next()
            {
                if (this._index < this._value.Length)
                {
                    this._current = this._value[this._index];
                    this._index++;
                    return true;
                }
                else
                {
                    this._current = '\0';
                    return false;
                }
            }

            private readonly string _value;
            private readonly StringBuilder _sb;
            private int _index;
            private char _current;
        }

        #endregion Resolve documentation

        private const string SummaryTag = "summary";
        private readonly XElement _xml;
        private readonly XElement? _members;
        private readonly string _namespace;
        private OpenApiDocument _document;

    }

}
