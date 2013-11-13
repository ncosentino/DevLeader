using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaRefactor.Processing.PreRefactor.String
{
    public class StringEqualsProcessor : Processor
    {
        private readonly string _value;

        public StringEqualsProcessor(object mandatoryArgument, object value)
            : base(mandatoryArgument)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            _value = (string)value; // will throw exception on mismatch
        }

        protected override bool Process(object importantReference, object input)
        {
            return Convert.ToString(input, System.Globalization.CultureInfo.InvariantCulture).Equals(_value);
        }
    }
}
