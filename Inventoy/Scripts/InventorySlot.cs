using Godot;
using System;

public partial class InventorySlot : Slot
{
    public override void _Ready()
    {
        base._Ready();
		this.AddThemeStyleboxOverride("Panel", itemSprite);
    }

    public override void _Process(double delta)
	{
	}
}
