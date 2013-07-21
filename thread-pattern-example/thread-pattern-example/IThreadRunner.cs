using System;
using System.Collections.Generic;
using System.Text;

namespace ncosentino.ThreadPatternExample
{
    internal interface IThreadRunner
    {
        #region Exposed Members

        void Start();

        void Stop();

        #endregion
    }
}
