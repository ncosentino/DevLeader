using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IReadModel : IDisposable
    {
        #region Exposed Members

        bool Exists(string name);

        #endregion
    }
}
