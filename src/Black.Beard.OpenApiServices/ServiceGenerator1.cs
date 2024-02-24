using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Bb.OpenApiServices
{

    public abstract class ServiceGenerator<T> : ServiceGenerator
        where T : class, new()
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceGenerator{T}"/> class.
        /// </summary>
        public ServiceGenerator()
        {
            this.Configuration = new T();
        }

        /// <summary>
        /// Gets the type of the configuration.
        /// </summary>
        /// <value>
        /// The type of the configuration.
        /// </value>
        public override Type? ConfigurationType => typeof(T);

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public T? Configuration { get; set; }

        public override object? GetConfiguration() => Configuration;

        /// <summary>
        /// Applies the configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public override void ApplyConfiguration(object? configuration)
        {
            this.Configuration = configuration as T;
        }

    }

}