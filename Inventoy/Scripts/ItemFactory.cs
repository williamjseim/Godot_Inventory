using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godot_Inventory.Inventoy.Scripts
{
    public class ItemFactory
    {
        public ItemHolder ProduceItemHolder(){
            return new ItemHolder();
        }

        public ItemHolder ProduceItemHolder(Item item, int amount){
            return new ItemHolder(item, amount);
        }
    }
}