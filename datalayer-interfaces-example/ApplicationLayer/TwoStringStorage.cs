using System;
using System.Collections.Generic;
using System.Text;

using DataLayer;

namespace ApplicationLayer
{
    public class TwoStringStorage : IStorage
    {
        #region Fields

        private readonly IModel _model;
        private readonly Dictionary<string, bool> _tableExistsCache;

        #endregion

        #region Constructors

        private TwoStringStorage(IModel model)
        {
            _model = model;
            _tableExistsCache = new Dictionary<string, bool>();
        }

        #endregion

        #region Exposed Members

        public static IStorage Create(IModel model)
        {
            return new TwoStringStorage(model);
        }

        public void Store(string name, IDictionary<string, object> data)
        {
            if (!CheckTableCache(name))
            {
                lock (_tableExistsCache)
                {
                    if (!CheckTableCache(name))
                    {
                        _model.Create(name, new Dictionary<string, string> { { "Value1", "TEXT" }, { "Value2", "TEXT" } });
                        _tableExistsCache[name] = true;
                    }
                }
            }

            _model.Insert(name, data);
        }

        #endregion

        #region Internal Members

        private bool CheckTableCache(string name)
        {
            return _tableExistsCache.ContainsKey(name) && _tableExistsCache[name];
        }
        
        #endregion
    }
}