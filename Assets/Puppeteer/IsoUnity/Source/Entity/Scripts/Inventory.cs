using UnityEngine;
using System.Collections.Generic;
using IsoUnity;
using IsoUnity.Entities;

namespace IsoUnity.Events
{

    public class Inventory : EventedEventManager
    {

        [GameEvent]
        public void OpenInventory()
        {
        }

        [GameEvent]
        public void AddItem(Item item)
        {
            items.Add(item);
        }


        [GameEvent]
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }


        [GameEvent]
        public void UseItem(Item item)
        {
            if (items.Contains(item)) item.use();
        }

        [SerializeField]
        private List<Item> items = new List<Item>();
        public Item[] Items
        {
            get { return items.ToArray() as Item[]; }
        }

    }
}