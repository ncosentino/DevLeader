using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaRefactor.Processing
{
    public enum ProcessorType
    {
        GreaterThan,
        LessThan,
        NumericEqual,
        StringEqual,
        StringNotEqual,
        /* we could add countless more types of processors here. realistically,
         * an enum may not be the best option to accomplish this, but for 
         * demonstration purposes it'll make things much easier. 
         */
    }
}
