using Godot;
using Godot_Inventory.Inventoy.Scripts;
using System;
using System.Collections.Generic;

public partial class InventoryManager : ContainerManager
{
	[Export] protected DraggedItem draggedItem;
	[Export] protected Item testItem;
	ItemFactory factory = new();
	
	public override void _Ready()
	{
		base._Ready();
		if(draggedItem == null)
			GD.PrintErr($"{nameof(draggedItem)} is null");

		SetupInventoryGrid();
		Slot.Interact += this.SlotInteraction;
		InsertItem(this.testItem, 100);
	}

	public override void _Process(double delta)
	{
		
	}

	//gets called whenever player clicks inventory slot
	public virtual void SlotInteraction(Slot slot, MouseButton mouse){
		if(mouse == MouseButton.Left){
			slot.LeftClick(draggedItem);
		}
	}

	public void InsertItem(Item item, int amount){
		foreach (Slot slot in slots)
		{
			if(slot.IsEmpty || slot.Itemholder.Equals(item)){
				slot.InsertItem(factory.ProduceItemHolder(item, amount));
				break;
			}
		}
	}
}
