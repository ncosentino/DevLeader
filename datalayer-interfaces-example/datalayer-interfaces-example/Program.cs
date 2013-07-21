using System;
using System.Collections.Generic;
using System.Text;

using ApplicationLayer;

namespace ncosentino.DatalayerInterfacesExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // you can create any type of IModel you want here and pass it to the storage class
            var model = DataLayer.Sqlite.SqliteModel.Create(":memory:");
            var storage = TwoStringStorage.Create(model);

            storage.Store("location1", new Dictionary<string, object> { { "Value1", "ABC" }, { "Value2", "123" } });
            storage.Store("location1", new Dictionary<string, object> { { "Value1", "AAA" }, { "Value2", "BBB" } });
            storage.Store("location1", new Dictionary<string, object> { { "Value1", "111" }, { "Value2", "222" } });
        }
    }
}
