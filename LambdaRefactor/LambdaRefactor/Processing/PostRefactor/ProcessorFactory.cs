using System;
using System.Collections.Generic;
using System.Text;

using LambdaRefactor.Processing.PostRefactor.Numeric;
using LambdaRefactor.Processing.PostRefactor.String;

namespace LambdaRefactor.Processing.PostRefactor
{
    public class ProcessorFactory : IProcessorFactory
    {
        public IProcessor Create(ProcessorType type, object mandatoryArgument, object value)
        {
            switch (type)
            {
                case ProcessorType.GreaterThan:
                    return new NumericProcessor(mandatoryArgument, value, (_, x, y) => x < y);
                case ProcessorType.StringEqual:
                    return new StringProcessor(mandatoryArgument, value, (_, x, y) => x == y);
                /*
                 * Look how easy it is to add new processors! Exercise for you:
                 * implement the remaining processors in the enum!
                 */
                default:
                    throw new NotImplementedException("The processor type '" + type + "' has not been implemented in this factory.");
            }
        }
    }
}