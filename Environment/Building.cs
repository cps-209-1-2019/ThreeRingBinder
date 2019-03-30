//--------------------------------------------------------------------------------------------
//File:   Building.cs
//Desc:   This is the class that contains logic for the Buildings the player will be in.
//---------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environment
{
    //Added public accessibility modifier - Day
    public class Building : ISerialization<Building>
    {

        //Turn the object into a string
        public string Serialize()
        {
            throw new NotImplementedException();
        }

        //Take a strin and turn it into a Building object
        public Building Deserialize(string obj)
        {
            throw new NotImplementedException();
        }

        
    }
}
