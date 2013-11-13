using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace LambdaRefactor.Processing.PostRefactor.Numeric
{
    public class NumericProcessor : Processor
    {
        private readonly decimal _value;
        private readonly ProcessDelegate<decimal> _processDelegate;

        public NumericProcessor(object mandatoryArgument, object value, ProcessDelegate<decimal> processDelegate)
            : base(mandatoryArgument)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (processDelegate == null)
            {
                throw new ArgumentNullException("processDelegate");
            }

            _value = Convert.ToDecimal(value, CultureInfo.InvariantCulture); // will throw exception on mismatch
            _processDelegate = processDelegate;
        }

        protected override bool Process(object importantReference, object input)
        {
            decimal numericInput;
            try
            {
                numericInput = Convert.ToDecimal(input, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return false;
            }

            return _processDelegate(importantReference, _value, numericInput);
        }
    }
}
