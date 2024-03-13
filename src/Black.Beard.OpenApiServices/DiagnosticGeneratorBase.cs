using Bb.Analysis.DiagTraces;

namespace Bb.OpenApiServices
{

    public abstract class DiagnosticGeneratorBase<T> : OpenApiGenericVisitor<T>
    {

        protected virtual void Initialize(ContextGenerator ctx)
        {
            Context = ctx;
            _diag = ctx.Diagnostics;
        }

     
        protected ScriptDiagnostics _diag;

        public ContextGenerator Context { get; private set; }

    }


}