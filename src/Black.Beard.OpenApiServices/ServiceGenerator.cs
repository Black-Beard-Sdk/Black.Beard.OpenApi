using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Bb.OpenApiServices
{

  
    public abstract class ServiceGenerator
    {

        static ServiceGenerator()
        {
            jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public abstract object? GetConfiguration();

        public abstract void ApplyConfiguration(object? token);

        public abstract Type? ConfigurationType { get; }

        public abstract void InitializeDatas(string file);

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>
        /// The directory.
        /// </value>
        public string Directory { get => _dir.FullName; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Template { get; internal set; }

        public string Contract { get; internal set; }

        public abstract ContextGenerator Generate();

        protected string Load(params string[] paths)
        {
            var path = Path.Combine(paths);
            return path.LoadFromFile().Map(_objectForMap);
        }

        /// <summary>
        /// Sets the object for map.
        /// </summary>
        /// <param name="mapObject">The map object.</param>
        public void SetObjectForMap (object mapObject)
        {
            _objectForMap = mapObject;
        }

        private object _objectForMap;


        public void SetDirectory(string directory)
        {
            _dir = new DirectoryInfo(directory);
        }

        protected DirectoryInfo _dir;
        protected static readonly JsonSerializerSettings jsonSerializerSettings;

    }


}