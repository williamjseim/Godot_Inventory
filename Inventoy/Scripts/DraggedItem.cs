using Godot;
using System;

public partial class DraggedItem : Panel
{
	public bool IsEmpty { get { return (ItemHolder == null || ItemHolder.Amount == 0 || ItemHolder.Id == -1) ? true : false; } }
	private ItemHolder itemHolder;
	public ItemHolder ItemHolder { get { return itemHolder; } set {
		if(value == null || value.Equals(ItemHolder.Empty)){
			this.Visible = false;
		}
		this.itemHolder = value;
		UpdateSprite(this.itemHolder.Texture);
		}
	}

	public int Amount { get { return this.itemHolder.Amount; } set { itemHolder.Amount -= value; if(this.itemHolder.Amount <= 0){
		this.Empty();
	}}}

	StyleBoxTexture texture = new();
	public override void _Ready()
	{
		this.texture.Texture = ItemHolder == null ? null : ItemHolder.Item.ItemSprite;
		this.AddThemeStyleboxOverride("Panel", texture);
	}

    public override void _Process(double delta)
    {
		if(!IsEmpty)
        	this.SetPosition(this.GetGlobalMousePosition() - (this.Size / 2));
    }

	public void UpdateSprite(Texture2D texture){
		this.texture.Texture = texture;
	}

	public void Empty(){
		this.itemHolder = ItemHolder.Empty;
		this.UpdateSprite(null);
	}



}
