using Bb.OpenApi;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Microsoft.VisualBasic;

namespace Bb
{

    public class OpenApiBase : IOpenApi, IStoreSource
    {


        public OpenApiBase()
        {
            _path = new Stack<string>();
            _context = new Stack<string>();
            _pathObject = new Stack<object>();
            _pathStorages = new Stack<DisposingStorage>();
        }


        #region Paths

        public string LastPath
        {

            get
            {
            
                if (_path.Count >= 1)
                    return _path.Peek();

                return default;
            
            }

        }

        public string GetPath
        {

            get
            {

                StringBuilder sb = new StringBuilder();
                sb.Append("#");

                var l = _path.Reverse().ToList();

                foreach (var item in l)
                {
                    sb.Append("/");
                    sb.Append(item);
                }

                return sb.ToString();

            }
        }

        /// <summary>
        /// clear all segments from current path.
        /// </summary>
        public void ClearPath()
        {
            _path.Clear();
        }

        /// <summary>
        /// Pushes a new segment path.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public IDisposable PushPath(string segment)
        {
            return new _disposePathClass(this, segment);
        }

        #endregion Paths


        #region Children

        /// <summary>
        /// Gets the stacked items.
        /// </summary>
        /// <returns></returns>
        public object[] GetItems
        {
            get
            {
                return _pathObject.ToArray();
            }
        }

        /// <summary>
        /// Clears all stacked children.
        /// </summary>
        public void ClearChildren()
        {
            _pathObject.Clear();
        }
        
        public IDisposable PushChildren(object segment)
        {
            return new _disposeItemClass(this, segment);
        }

        #endregion Children


        #region contexts

        /// <summary>
        /// Gets the current context.
        /// </summary>
        /// <returns></returns>
        public string? ContextName
        {
            get
            {
                
                if (_context.Count >= 1)
                    return _context.Peek();
                
                return null;

            }
        }

        /// <summary>
        /// Gets the path of the current context.
        /// </summary>
        /// <returns></returns>
        public string[] Contexts
        {
            get
            {
                return _context.ToArray();
            }
        }

        /// <summary>
        /// clear all segments from current path.
        /// </summary>
        public void ClearContexts()
        {
            _context.Clear();
        }

        /// <summary>
        /// Pushes a new context.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns></returns>
        public IDisposable PushContext(string context)
        {
            return new _disposeContextClass(this, context);
        }

        #endregion contexts


        #region Stores

        /// <summary>
        /// Get the current store
        /// </summary>
        /// <returns></returns>
        protected IStore CurrentStore()
        {
            return _pathStorages.Peek();
        }

        /// <summary>
        /// Append a new layer for storing data
        /// </summary>
        void IStoreSource.StorePop()
        {
            _pathStorages.Pop();
        }

        /// <summary>
        /// remove the last layer for storing datas
        /// </summary>
        /// <param name="toDispose"></param>
        void IStoreSource.StorePush(object toDispose)
        {
            _pathStorages.Push((DisposingStorage)toDispose);
        }

        protected void Store(string key, object value)
        {
            _pathStorages.Peek().AddInStorage(key, value);
        }

        protected IStore Store()
        {
            return new DisposingStorage(this);
        }

        #endregion Stores


        private class _disposeContextClass : IDisposable
        {

            public _disposeContextClass(OpenApiBase document, string item)
            {
                _document = document;
                _document._context.Push(item);
            }

            public void Dispose()
            {
                _document._context.Pop();
            }

            private readonly OpenApiBase _document;

        }

        private class _disposeItemClass : IDisposable
        {

            public _disposeItemClass(OpenApiBase document, object item)
            {
                _document = document;
                _document._pathObject.Push(item);
            }

            public void Dispose()
            {
                _document._pathObject.Pop();
            }

            private readonly OpenApiBase _document;

        }

        private class _disposePathClass : IDisposable
        {

            public _disposePathClass(OpenApiBase document, string segments)
            {
                _document = document;
                _document._path.Push(segments);
            }

            public void Dispose()
            {
                _document._path.Pop();
            }

            private readonly OpenApiBase _document;

        }


        [DebuggerStepThrough]
        [DebuggerNonUserCode]
        protected void Stop()
        {

            var st = new StackTrace();
            var f = st.GetFrame(1);
            Debug.WriteLine($"{f.ToString().Trim()} try to stop");

            if (Debugger.IsAttached)
                Debugger.Break();

        }


        private Stack<string> _context;
        private Stack<string> _path;
        private Stack<object> _pathObject;
        private Stack<DisposingStorage> _pathStorages;

    }


    /// <summary>
    /// Object for storing data in processing visitor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DisposingStorage : IStore
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposingStorage{T}"/> class.
        /// </summary>
        /// <param name="document"></param>
        public DisposingStorage(IStoreSource document)
        {
            this._dic = new Dictionary<string, object>();
            _documentRoot = document;
            _documentRoot.StorePush(this);
        }

        #region Storing

        public void AddInStorage(string key, object value)
        {
            if (_dic.ContainsKey(key))
                _dic[key] = value;
            else
                _dic.Add(key, value);
        }

        public bool TryGetInStorage(string key, out object? value)
        {
            return _dic.TryGetValue(key, out value);
        }

        public bool ContainsInStorage(string key)
        {
            return _dic.ContainsKey(key);
        }

        public void Dispose()
        {
            _documentRoot.StorePop();
        }

        #endregion Storing

        private readonly IStoreSource _documentRoot;
        private readonly Dictionary<string, object> _dic;
    }

    public interface IStore : IDisposable
    {

        void AddInStorage(string key, object value);


        bool TryGetInStorage(string key, out object value);


        bool ContainsInStorage(string key);

    }

    public interface IStoreSource

    {
        void StorePop();

        void StorePush(object toDispose);

    }

}
