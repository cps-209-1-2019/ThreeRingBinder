//--------------------------------------------------------------------------------------------
//File:   Items.cs
//Desc:   This class contains logic for the items in the room.
//---------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binder.Environments
{
    class Items
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int[] Position { get; set; }
        public bool Found { get; set; }
    }

    public class InventoryItem: Items
    {
        
    }

    public class DecoyItem: Items
    {

    }
}
