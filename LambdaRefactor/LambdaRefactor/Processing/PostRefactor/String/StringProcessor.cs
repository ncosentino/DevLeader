using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaRefactor.Processing.PostRefactor.String
{
    public class StringProcessor : Processor
    {
        private readonly string _value;
        private readonly ProcessDelegate<string> _processDelegate;

        public StringProcessor(object mandatoryArgument, object value, ProcessDelegate<string> processDelegate)
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

            _value = (string)value; // will throw exception on mismatch
            _processDelegate = processDelegate;
        }

        protected override bool Process(object importantReference, object input)
        {
            return _processDelegate(importantReference, _value, Convert.ToString(input, System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}
