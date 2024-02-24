using Bb.Codings;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using Bb.Extensions;

namespace Bb.OpenApiServices
{

    public abstract class OpenApiGeneratorCSharpBase : DiagnosticGeneratorBase<CSMemberDeclaration>, IServiceGenerator<OpenApiDocument>
    {


        public OpenApiGeneratorCSharpBase(string artifactName, string @namespace, params string[] usings)
        {
            _tree = new DeclarationBloc();
            _namespace = @namespace;
            _artifactName = artifactName;
            _usings = usings;

        }

        public void Parse(OpenApiDocument self, ContextGenerator ctx)
        {
            Initialize(ctx);
            Trace.WriteLine("Generating " + _artifactName);
            self.Accept(this);
        }


        protected CSharpArtifact CreateArtifact(string suffix)
            => new CSharpArtifact(suffix + _artifactName)
                .Usings(_usings);

        protected CSNamespace CreateNamespace(CSharpArtifact cs) => cs.Namespace(_namespace);


    
      

        protected readonly string _namespace;
        private readonly string _artifactName;
        private readonly string[] _usings;
        protected readonly DeclarationBloc _tree;
        protected OpenApiDocument _self;
        protected Data _datas = new Data();

    }




}