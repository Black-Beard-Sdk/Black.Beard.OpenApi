using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Bb.Extensions
{

    /// <summary>
    /// Documentation loader extension
    /// </summary>
    public static class LoadDocumentationExtension
    {

        /// <summary>
        /// Converts to <see cref="XPathDocument"/>.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static XPathDocument? ToXPathDocument(this XElement self)
        {
            if (self != null)
            {
                var result = new XPathDocument(self.CreateReader());
                return result;
            }

            return default;
        }

        /// <summary>
        /// Converts to <see cref="XDocument"/>.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static XDocument? ToXDocument(this XElement self)
        {
            if (self != null)
            {
                var result = new XDocument(self.CreateReader());
                return result;
            }
            return default;
        }

        /// <summary>
        /// Loads and concat all XML documentation found files.
        /// </summary>
        /// <param name="directory">The directory where search documents.</param>
        /// <returns></returns>
        public static XElement? LoadXmlFiles(DirectoryInfo? directory = null)
        {

            if (directory == null)
            {
                var assembly = Assembly.GetEntryAssembly();
                if (!string.IsNullOrEmpty(assembly?.Location))
                {
                    var file = new FileInfo(assembly.Location);
                    file.Refresh();
                    directory = file.Directory;
                }
            }

            return LoadXmlFiles("*.xml", directory);

        }

        /// <summary>
        /// Loads and concats all XML documentation found files.
        /// </summary>
        /// <param name="patternGlobing">The pattern globing.</param>
        /// <param name="directory">The directory where search documents.</param>
        /// <returns>if no document are found, the result is null.</returns>
        public static XElement? LoadXmlFiles(this string patternGlobing, DirectoryInfo? directory = null)
        {

            var members = new XElement(XName.Get("members"));
            XElement? xml = new XElement(XName.Get("doc"), members);

            if (directory != null)
            {

                directory.Refresh();

                // Build one large xml with all comments files
                var files = directory.GetFiles(patternGlobing).ToList();
                if (files.Count == 0)
                    Console.WriteLine($"no documentation file found in the folder {directory.FullName}");

                // Build one large xml with all comments files
                foreach (var file in files)
                {

                    try
                    {
                        Append(xml, members, file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to parse '{file}'. maybe the file don't contains documentation. ({e.ToString()})");
                    }

                }

            }

            return xml;

        }

        private static void Append(XElement xml, XElement members, FileInfo file)
        {

            Console.WriteLine($"try to load documentation {file.FullName}");
            XElement dependentXml = XElement.Load(file.FullName);

            if (dependentXml.Name.LocalName == "doc")
            {

                var source = dependentXml.Element(XName.Get("assembly"));
                xml.AddFirst(source);

                source = dependentXml.Element(XName.Get("members"));
                members.Add(source.Elements(XName.Get("member")));

            }

        }

        #region default values

        private static Action<OpenApiInfoGenerator<OpenApiInfo>> GetDefaultBuilder()
        {
            var ass = Assembly.GetEntryAssembly();
            var c = ass.CustomAttributes.ToList();

            string? title = GetServiceName(c);
            string? version = GetVersion(c);
            string? description = GetDescription(c);

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(version))
                version = $"{title} {version}";

            Action<OpenApiInfoGenerator<OpenApiInfo>> builder1 = b =>
            {

                if (!string.IsNullOrEmpty(title))
                    b.Add(c => c.Title = title);

                if (!string.IsNullOrEmpty(version))
                    b.Add(c => c.Version = version);

                if (!string.IsNullOrEmpty(description))
                    b.Add(c => c.Description = description);

            };

            return builder1;

        }

        private static string? GetDescription(List<CustomAttributeData> c)
        {
            return GetValue(c, typeof(AssemblyDescriptionAttribute));
        }

        private static string? GetServiceName(List<CustomAttributeData> c)
        {
            return GetValue(c, typeof(AssemblyTitleAttribute), typeof(AssemblyProductAttribute));
        }

        private static string? GetVersion(List<CustomAttributeData> c)
        {
            return GetValue(c, typeof(AssemblyInformationalVersionAttribute), typeof(AssemblyFileVersionAttribute));
        }

        private static string? GetValue(List<CustomAttributeData> c, params Type[] types)
        {

            foreach (var type in types)
            {
                var o = c.Where(d => type == d.AttributeType).Select(e => e.ConstructorArguments.First().Value?.ToString()).FirstOrDefault();
                if (o != null)
                    return o;
            }

            return null;

        }

        #endregion default values


        /// <summary>
        /// Merges the XML fragments.
        /// </summary>
        /// <param name="baseDocument">The base document.</param>
        /// <param name="nextDocuments">The next documents.</param>
        /// <returns></returns>
        public static XElement Merges(this XElement baseDocument, params XElement[] nextDocuments)
        {

            if (baseDocument == null)
                throw new ArgumentNullException(nameof(baseDocument));

            XElement? xml = baseDocument.Element("doc");

            if (xml != null)
            {
                XElement? members = xml.Element("members");
                foreach (var documentToMerge in nextDocuments)
                {

                    XElement? dependentXml = documentToMerge.Element("doc");
                    if (dependentXml != null && dependentXml.Name.LocalName == "doc")
                    {

                        var source = dependentXml.Element(XName.Get("assembly"));
                        xml.AddFirst(source);

                        source = dependentXml.Element(XName.Get("members"));
                        members.Add(source.Elements(XName.Get("member")));

                    }

                }
            }

            return baseDocument;

        }
   
    }


}
