using System;
using System.Collections.Generic;
using System.Text;

using LambdaRefactor.Processing.PreRefactor.Numeric;
using LambdaRefactor.Processing.PreRefactor.String;

namespace LambdaRefactor.Processing.PreRefactor
{
    public class ProcessorFactory : IProcessorFactory
    {
        public IProcessor Create(ProcessorType type, object mandatoryArgument, object value)
        {
            switch (type)
            {
                case ProcessorType.GreaterThan:
                    return new GreaterProcessor(mandatoryArgument, value);
                case ProcessorType.StringEqual:
                    return new StringEqualsProcessor(mandatoryArgument, value);
                /* 
                 * we still have to go implement all the other classes! 
                 */
                default:
                    throw new NotImplementedException("The processor type '" + type + "' has not been implemented in this factory.");
            }
        }
    }
}