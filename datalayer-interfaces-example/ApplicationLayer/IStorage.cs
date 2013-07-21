using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer
{
    public interface IStorage
    {
        #region Exposed Members

        void Store(string name, IDictionary<string, object> data);

        #endregion
    }
}
