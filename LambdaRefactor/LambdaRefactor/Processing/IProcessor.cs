using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaRefactor.Processing
{
    public interface IProcessor
    {
        bool TryProcess(object input);
    }
}
