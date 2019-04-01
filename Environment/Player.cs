using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    class Player : ISerialization<Player>
    {
        public string Name { get; set; }
        public List<String> Inventory { get; set; }
        public Player(string name)
        {
            Name = name;
        }
        public void Enteract()
        {

        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public Player Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
