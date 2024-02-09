using Godot;
using System;

public partial class Slot : Panel
{
	protected ItemHolder holder = ItemHolder.Empty;
	public ItemHolder Itemholder { get { return this.holder; } protected set{
		this.holder = value;
		UpdateSprite(value.Equals(ItemHolder.Empty) ? null : value.Item.ItemSprite);
	}}
	protected StyleBoxTexture itemSprite = new();
	public bool IsEmpty {
		get{
			return Itemholder.Equals(ItemHolder.Empty) || Itemholder.Id == -1;
		}
	}

	public virtual int Amount {get { return (this.Itemholder.Equals(ItemHolder.Empty) || this.Itemholder.Id == -1) ? 0 : this.Itemholder.Amount; } protected set
		{ 
			this.Itemholder.Amount = value;
			if(this.Amount <= 0){
				Itemholder = ItemHolder.Empty;
			}
		}
	}
	protected virtual int StackSize {get { return (this.Itemholder.Equals(ItemHolder.Empty) || this.Itemholder.Id == -1) ? 0 : this.Itemholder.Item.StackSize; }}
	
	//used for communicating with the inventory without a reference
	public static event Action<Slot, MouseButton> Interact;

	public override void _Ready()
	{
		AssignEvents();
	}

	protected virtual void AssignEvents(){
		this.GuiInput += this.Input;
	}

	//communicates to the InventoryManager that a slot was clicked
	public void Input(InputEvent input){
		if(input is InputEventMouseButton mouse && mouse.IsPressed())
			Interact?.Invoke(this, mouse.ButtonIndex);
	}

	public virtual void UpdateSprite(Texture2D sprite){
		this.itemSprite.Texture = sprite;
	}

	public virtual void Empty(){
		this.Itemholder = ItemHolder.Empty;
	}

	public virtual void InsertItem(ItemHolder item){
		this.Itemholder = item;
	}

	public virtual void LeftClick(DraggedItem DraggedItem){
		GD.Print("left click");
		if(this.IsEmpty && DraggedItem.IsEmpty){
			return;
		}
		if(DraggedItem.IsEmpty){
			GD.Print("pickup");
			DraggedItem.InsertItem(this.Itemholder);
			this.Empty();
		}
		if(this.IsEmpty){//places items in empty slot
			this.Itemholder = DraggedItem.ItemHolder;
			DraggedItem.Empty();
			return;
		}
		else{//swaps items between draggeditem and selected inventoryslot
			var tempItemHolder = this.Itemholder;
			this.Itemholder = DraggedItem.ItemHolder;
			DraggedItem.ItemHolder = this.Itemholder;
			return;
		}
	}

	public virtual void RightClick(DraggedItem draggedItem){
		int stackSpace = Math.Abs(this.StackSize - this.Amount);
		int transferAmount = draggedItem.ItemHolder.Amount == 1 ? 1 : draggedItem.ItemHolder.Amount / 2;
		if(stackSpace > transferAmount){
			draggedItem.ItemHolder.Amount -= transferAmount;
			
		}
	}
}
