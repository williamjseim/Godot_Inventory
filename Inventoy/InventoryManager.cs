using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryManager : ContainerManager
{
	[Export] protected DraggedItem draggedItem;
	[Export] protected Item testItem;
	
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

	/// <summary>
	/// gets called whenever player clicks inventory slot
	/// </summary>
	/// <param name="slot"></param>
	/// <param name="mouse"></param>
	public virtual void SlotInteraction(Slot slot, MouseButton mouse){
		if(mouse == MouseButton.Left){
			slot.LeftClick(draggedItem);
		}
	}

	/// <summary>
	/// Inserts 
	/// </summary>
	/// <param name="item"></param>
	/// <param name="amount"></param>
	public void InsertItem(Item item, int amount){
		foreach (Slot slot in slots)
		{
			if(slot.IsEmpty || slot.Itemholder.Equals(item)){
				slot.InsertItem(item, amount);
				break;
			}
		}
	}
}
