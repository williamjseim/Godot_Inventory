using Godot;
using System;
//this object could be freed and reinstantiated everytime you need instead of just making it invisible and not move
public partial class DraggedItem : Panel, IInsertItem
{
	public bool IsEmpty { get { return (holder.Equals(ItemHolder.Empty) || holder.Amount == 0 || holder.Id == Item.Empty) ? true : false; } }
	private ItemHolder holder = ItemHolder.Empty;
	public ItemHolder ItemHolder { get { return holder; } 
		protected set {
			this.holder = value;
			this.Visible = (value.Equals(ItemHolder.Empty) || value.Equals(ItemHolder.Empty)) ? false : true;
			UpdateSprite(value.Equals(ItemHolder.Empty) ? null : this.holder.Texture);
		}
	}

	public int Amount { get { return this.ItemHolder.Amount; } set { holder.Amount = value; if(holder.Amount <= 0){
		this.Empty();
	}}}

	StyleBoxTexture stylebox = new();
	public override void _Ready()
	{
		this.stylebox.Texture = ItemHolder.Equals(ItemHolder.Empty) ? null : ItemHolder.Item.ItemSprite;
		this.AddThemeStyleboxOverride("panel", stylebox);
	}

    public override void _Process(double delta)
    {
		if(!IsEmpty){
        	this.SetPosition(this.GetGlobalMousePosition() - (this.Size / 2));
		}
    }

	public void UpdateSprite(Texture2D texture){
		this.stylebox.Texture = texture;
	}

	/// <summary>
	/// this empties the dragged item
	/// </summary>
	public void Empty(){
		this.holder = new ItemHolder();
		UpdateSprite(null);
		this.Visible = false;
	}

    public void InsertItem(ItemHolder itemHolder)
    {
		this.ItemHolder = itemHolder.Clone();
    }

}
