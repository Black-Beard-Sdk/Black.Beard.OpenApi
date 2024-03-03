using Bb.Analysis.DiagTraces;

namespace Bb.OpenApiServices
{

    public abstract class DiagnosticGeneratorBase<T> : OpenApiGenericVisitor<T>
    {

        protected virtual void Initialize(ContextGenerator ctx)
        {
            _ctx = ctx;
            _diag = ctx.Diagnostics;
        }

     
        protected ContextGenerator _ctx;
        protected ScriptDiagnostics _diag;

    }


}