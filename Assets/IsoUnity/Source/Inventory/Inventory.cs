using UnityEngine;
using System.Collections.Generic;
using System;

namespace Isometra {
	public class Inventory : EventManager
	{

	    private List<Item> itemsToAdd = new List<Item>();
	    private List<Item> itemsToRemove = new List<Item>();
	    private bool openInventory = false;
	    private List<Item> itemsToUse = new List<Item>();
	    private List<IGameEvent> events = new List<IGameEvent>();

	    public override void ReceiveEvent(IGameEvent ge)
	    {
			var p = ge.getParameter ("Inventory");
			if (p == this || (p is string && ((string)p) == gameObject.name))
	        {
	            Item item = (Item)ge.getParameter("Item");
	            switch (ge.Name.ToLower())
	            {
	                case "open inventory":
	                    openInventory = true;
	                    events.Add(ge);
	                    break;
	                case "add item":
	                    itemsToAdd.Add(item);
	                    events.Add(ge);
	                    break;
	                case "remove item":
	                    itemsToRemove.Add(item);
	                    events.Add(ge);
	                    break;
	                case "use item":
	                    itemsToUse.Add(item);
	                    events.Add(ge);
	                    break;
	            }
	        }
	    }

	    /*public override Option[] getOptions()
	    {
	        if (this.Entity.GetComponent<Player>() != null)
	        {
	            GameEvent ge = ScriptableObject.CreateInstance<GameEvent>();
	            ge.Name = "Open Inventory";
	            ge.setParameter("Inventory", this);
	            Option option = new Option("Inventory", ge, false, 0);
	            return new Option[] { option };
	        }
	        else
	            return null;
	    }*/

	    public override void Tick()
	    {
	        if (openInventory)
	        {
	            // TODO
	            openInventory = false;
	        }
	        //ADDS
	        while (itemsToAdd.Count > 0)
	        {
	            //if (!items.Contains (itemsToAdd [0])) 
	            items.Add(itemsToAdd[0]);
	            itemsToAdd.RemoveAt(0);
	        }
	        //USES
	        while (itemsToUse.Count > 0)
	        {
	            if (items.Contains(itemsToUse[0])) itemsToUse[0].use();
	            itemsToUse.RemoveAt(0);
	        }
	        //REMOVES
	        while (itemsToRemove.Count > 0)
	        {
	            if (items.Contains(itemsToRemove[0])) items.Remove(itemsToRemove[0]);
	            itemsToRemove.RemoveAt(0);
	        }

	        while (events.Count > 0)
	        {
	            Game.main.eventFinished(events[0]);
	            events.RemoveAt(0);
	        }
	    }

	    [SerializeField]
	    private List<Item> items = new List<Item>();
	    public Item[] Items
	    {
	        get { return items.ToArray() as Item[]; }
	    }

	}
}