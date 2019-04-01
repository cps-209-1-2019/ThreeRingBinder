using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    public class AI : MovableCharacter, ISerialization<AI> 
    {
        public void Patrol()
        {

        }
        public void Chase()
        {

        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public AI Deserialize(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
