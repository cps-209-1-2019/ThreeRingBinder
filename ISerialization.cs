using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    public interface ISerialization<T>
    {
        string Serialize();

        T Deserialize(string obj);
    }
}
