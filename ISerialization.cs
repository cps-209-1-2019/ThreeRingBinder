using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder
{
    public interface ISerialization
    {
        string Save();

        void Load(string obj);

    }
}
