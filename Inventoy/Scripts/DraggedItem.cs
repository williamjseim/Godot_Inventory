using Godot;
using System;

public partial class DraggedItem : Panel
{
	public readonly ItemHolder itemHolder = new();
	StyleBoxTexture texture = new();
	public override void _Ready()
	{
		this.texture.Texture = itemHolder.Item.ItemSprite;
		this.AddThemeStyleboxOverride("Panel", texture);
	}

    public override void _Process(double delta)
    {
        this.SetPosition(this.GetGlobalMousePosition() - (this.Size / 2));
    }

	public void Destroy(){
		this.QueueFree();
	}
}
