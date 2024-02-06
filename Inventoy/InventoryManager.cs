using Godot;
using System;

public partial class InventoryManager : Node
{
#nullable enable
	DraggedItem? draggedItem;

#nullable disable
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Slot.Interact += this.SlotInteraction;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SlotInteraction(Slot slot, MouseButton mouse){
		
	}

	//should be called then opening inventory
	public void Open(){

	}

	//should be called then closing the inventory
	public void Close(){

	}
}
