using Godot;
using System;

public partial class DraggedItem : Panel
{
	public bool IsEmpty { get { return (ItemHolder.Equals(ItemHolder.Empty) || ItemHolder.Amount == 0 || ItemHolder.Id == -1) ? true : false; } }
	private ItemHolder holder = ItemHolder.Empty;
	public ItemHolder ItemHolder { get { return holder; } 
	set {
			this.Visible = (value == null || value.Equals(ItemHolder.Empty)) ? false : true;
			this.holder = value;
			UpdateSprite(value.Equals(ItemHolder.Empty) ? null : this.holder.Texture);
		}
	}

	public int Amount { get { return this.ItemHolder.Amount; } set { ItemHolder.Amount -= value; if(this.ItemHolder.Amount <= 0){
		this.Empty();
	}}}

	StyleBoxTexture stylebox = new();
	public override void _Ready()
	{
		this.stylebox.Texture = ItemHolder.Equals(ItemHolder.Empty) ? null : ItemHolder.Item.ItemSprite;
		this.AddThemeStyleboxOverride("panel", stylebox);
	}

	public void InsertItem(ItemHolder itemHolder){
		this.ItemHolder = itemHolder;
	}

    public override void _Process(double delta)
    {
		if(!IsEmpty){
			GD.Print("asdwa1536636");
        	this.SetPosition(this.GetGlobalMousePosition() - (this.Size / 2));
		}
    }

	public void UpdateSprite(Texture2D texture){
		this.stylebox.Texture = texture;
	}

	public void Empty(){
		this.ItemHolder = ItemHolder.Empty;
	}



}
