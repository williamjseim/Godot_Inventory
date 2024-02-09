using Godot;
using System;
using System.Collections.Generic;

public partial class ContainerManager : Control
{
	[Export] protected GridContainer grid;
	[Export] protected Slot[] slots;
	[ExportCategory("Generate slots")]
	[Export] protected bool AutoGenerateSlots;//this will create array of slot everytime game starts
	[Export] protected int SlotAmount;
	[Export] protected PackedScene slotScene;
	public override void _Ready()
	{
		if(grid == null)
			GD.PrintErr("Grid is null");
	}

	public override void _Process(double delta)
	{

	}

	public virtual void SetupInventoryGrid(){
		List<Slot> slots = new();
		if(AutoGenerateSlots){
			for (int i = 0; i < SlotAmount; i++)
			{
				var slot = this.slotScene.Instantiate<Slot>();
				slots.Add(slot);
				this.grid.AddChild(slot);
			}
			this.slots = slots.ToArray();
			return;
		}

		var children = grid.GetChildren();

		if(children.Count == 0){
			GD.PrintErr("no children in inventoryGrid if you dont want to manually assign slots then enable AutoGenerateSlots");
			return;
		}
		foreach (var item in grid.GetChildren())
		{
			if(item is Slot slot){
				slots.Add(slot);
			}
		}
		this.slots = slots.ToArray();
	}

	//should be called then opening inventory
	public virtual void Open(){

	}

	//should be called then closing the inventory
	public virtual void Close(){

	}
}
