using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace LambdaRefactor.Processing.PreRefactor.Numeric
{
    public class GreaterProcessor : Processor
    {
        private readonly decimal _value;

        public GreaterProcessor(object mandatoryArgument, object value)
            : base(mandatoryArgument)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            _value = Convert.ToDecimal(value, CultureInfo.InvariantCulture); // will throw exception on mismatch
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

            return numericInput > _value;
        }
    }
}
