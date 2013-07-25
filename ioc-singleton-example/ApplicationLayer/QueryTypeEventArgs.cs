using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer
{
    public class QueryTypeEventArgs : EventArgs
    {
        public Type Type { get; set; }
    }
}