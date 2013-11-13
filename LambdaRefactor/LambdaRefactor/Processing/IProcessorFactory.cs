using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaRefactor.Processing
{
    public interface IProcessorFactory
    {
        IProcessor Create(ProcessorType type, object mandatoryArgument, object value);
    }
}
