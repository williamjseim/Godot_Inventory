using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryManager : ContainerManager
{
	[Export] protected DraggedItem draggedItem;
    [Export] Item item;
	
	public override void _Ready()
	{
		base._Ready();
		if(draggedItem == null)
			GD.PrintErr($"{nameof(draggedItem)} is null");
	}

	public override void _Process(double delta)
	{

	}

	//gets called whenever player clicks inventory slot
	public override void SlotInteraction(Slot slot, MouseButton mouse){
		if(mouse == MouseButton.Left){
			slot.LeftClick(draggedItem);
		}
	}

	//should be called then opening inventory
	public void Open(){

	}

	//should be called then closing the inventory
	public void Close(){

	}
}
