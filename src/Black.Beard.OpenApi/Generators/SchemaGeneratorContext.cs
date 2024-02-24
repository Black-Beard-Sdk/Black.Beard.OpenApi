using Microsoft.OpenApi.Models;

namespace Bb
{
    internal class SchemaGeneratorContext
    {

        public SchemaGeneratorContext()
        {
            this._models = new Dictionary<Type, OpenApiSchema>();
        }


        internal bool Exists(Type type)
        {
            return !_models.ContainsKey(type);
        }

        internal void Add(Type type, OpenApiSchema schema)
        {
            this._models.Add(type, schema);
        }

        internal OpenApiSchema Resolve(Type type)
        {
            return this._models[type];
        }

        public OpenApiComponents OpenApiComponents
        {
            get
            {
                
                var component = new OpenApiComponents();

                foreach (var model in this._models)
                {
                    var type = model.Key;
                    string name;
                    if (type.IsGenericType)
                    {

                        name = type.Name.Substring(0, type.Name.IndexOf('`'));

                        string comma = "Of";
                        foreach (var item in model.Key.GetGenericArguments())
                        {
                            name += comma + item.Name;
                            comma = "And";
                        }

                    }
                    else
                        name = type.Name;

                    component.Schemas.Add(name, model.Value);

                }

                return component;

            }
        }

        public OpenApiSchema Root { get; internal set; }

        private Dictionary<Type, OpenApiSchema> _models;

    }

}
