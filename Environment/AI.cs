using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    class AI : ISerialization<AI>, MovableCharacter
    {
        public void Patrol()
        {

        }

        public void Chase()
        {

        }

        public override void Move()
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
