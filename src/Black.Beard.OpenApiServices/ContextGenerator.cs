using Bb;
using Bb.Analysis;
using Bb.Analysis.DiagTraces;
using Bb.Codings;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;

namespace Bb.OpenApiServices
{

    public class ContextGenerator
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextGenerator"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ContextGenerator(string path)
        {
            this.TargetPath = path;
            this._datas = new Dictionary<object, Data>();
            this._files = new HashSet<string>();
            this.Diagnostics = new ScriptDiagnostics();
            this._assemblies = new HashSet<Assembly>();
            _assemblyNames = new HashSet<string>();
        }

        #region Append documents

        /// <summary>
        /// create a new document on file system with specified content
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public string AppendDocument(string? path, string filename, string content)
        {

            Trace.WriteLine(" -  " + Path.GetFileNameWithoutExtension(filename) + " has generated.");

            var file = ComputeFullPath(path, filename);
            file.Save(content);
            return file;
        }

        /// <summary>
        /// create a new document on file system with specified content
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public string AppendDocument(string filename, string content)
        {
            var file = ComputeFullPath(string.Empty, filename);
            file.Save(content);
            return file;
        }

        /// <summary>
        /// create a new document on filesystem with specified content
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public string AppendDocument(string? path, string filename, StringBuilder content)
        {
            var file = ComputeFullPath(path, filename);
            file.Save(content.ToString());
            return file;
        }

        /// <summary>
        /// create a new document on filesystem with specified content
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public string AppendDocument(string filename, StringBuilder content)
        {
            var file = ComputeFullPath(string.Empty, filename);
            file.Save(content.ToString());
            return file;
        }

        /// <summary>
        /// Computes the full path for the target file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private string ComputeFullPath(string? path, string filename)
        {

            var _path = ComputeFullPath(path);

            int count = 1;
            var file = Path.Combine(_path, filename);
            while (_files.Contains(file))
            {
                var f = Path.GetFileNameWithoutExtension(filename) + count++.ToString();
                var e = Path.GetExtension(filename);
                file = Path.Combine(_path, f) + e;
            }

            _files.Add(file);

            return file;

        }

        public string GetRelativePath(string path)
        {
            Uri path1 = new Uri(Path.Combine(this.TargetPath, "."));
            Uri path2 = new Uri(path);
            Uri diff = path1.MakeRelativeUri(path2);
            return diff.ToString();
        }


        /// <summary>
        /// Computes the full path for the target file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string ComputeFullPath(string? path)
        {
            var targetDirectory = TargetPath;
            if (!string.IsNullOrEmpty(path))
                targetDirectory = Path.Combine(targetDirectory, path);
            var dir = new DirectoryInfo(targetDirectory);

            if (!dir.Exists)
                dir.Create();

            return dir.FullName;

        }

        #endregion Append documents

        public ScriptDiagnostics Diagnostics { get; }


        public string TargetPath { get; }

        public ContextGenerator AddAssemblyName(string assemblyName)
        {
            _assemblyNames.Add(assemblyName);
            return this;
        }

        public ContextGenerator AddAssembly(Type type)
        {
            _assemblies.Add(type.Assembly);
            return this;
        }

        public ContextGenerator AddAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }

        public Data GetDataFor(object key)
        {

            if (!_datas.TryGetValue(key, out var data))
                _datas.Add(key, data = new Data());

            return data;

        }

        private Dictionary<object, Data> _datas;

        public IEnumerable<string> Files => _files;

        public IEnumerable<Assembly> Assemblies => _assemblies;
        public IEnumerable<string> AssemblyNames => _assemblyNames;

        public string? ContractDocumentFilename { get; set; }

        private readonly HashSet<Assembly> _assemblies;
        private readonly HashSet<string> _assemblyNames;
        private HashSet<string> _files;

    }



}