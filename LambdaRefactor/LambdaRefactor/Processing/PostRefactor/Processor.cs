using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaRefactor.Processing.PostRefactor
{
    public abstract class Processor : IProcessor
    {
        private readonly object _importantReference;

        public Processor(object mandatoryArgument)
        {
            if (mandatoryArgument == null)
            {
                throw new ArgumentNullException("mandatoryArgument");
            }

            _importantReference = mandatoryArgument;
        }

        public delegate bool ProcessDelegate<T>(object importantReference, T processorValue, T input);

        public bool TryProcess(object input)
        {
            if (input == null)
            {
                return false;
            }

            return Process(_importantReference, input);
        }

        protected abstract bool Process(object importantReference, object input);
    }
}
