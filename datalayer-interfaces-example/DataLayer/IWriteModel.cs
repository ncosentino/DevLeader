using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IWriteModel : IDisposable
    {
        #region Exposed Members

        void Create(string name, IDictionary<string, string> definitions);

        void Insert(string name, IDictionary<string, object> values);

        #endregion
    }
}
