using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer
{
    public interface IApplication
    {
        string Name { get; }

        string Version { get; }
    }
}
