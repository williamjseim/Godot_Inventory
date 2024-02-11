using Godot;
using System;

public partial class Slot : Panel, IInsertItem
{
	[Export] private Label amountLabel;
	protected ItemHolder holder = ItemHolder.Empty;
	public ItemHolder Itemholder { get { return this.holder; } protected set{
		this.holder = value;
		UpdateSprite(value.Equals(ItemHolder.Empty) ? null : value.Item.ItemSprite);
		UpdateAmount(this.Amount);
	}}
	
	protected StyleBoxTexture stylebox = new();
	public bool IsEmpty {
		get{
			return Itemholder.Equals(ItemHolder.Empty) || Itemholder.Id == -1;
		}
	}

	public virtual int Amount {get { return (this.Itemholder.Equals(ItemHolder.Empty) || this.Itemholder.Id == -1) ? 0 : this.holder.Amount; } protected set
		{ 
			this.holder.Amount = value;
			if(this.Amount <= 0){
				this.Itemholder = ItemHolder.Empty;
			}
			UpdateAmount(this.Amount);
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
		this.stylebox.Texture = sprite;
	}

	public virtual void UpdateAmount(int amount){
		if(amount <= 0){
			this.amountLabel.Visible = false;
			return;
		}
		this.amountLabel.Visible = true;
		this.amountLabel.Text = amount.ToString();
	}

	public virtual void Empty(){
		this.holder = ItemHolder.Empty;
		UpdateSprite(null);
		UpdateAmount(this.Amount);
	}

	public virtual void InsertItem(ItemHolder item){
		this.Itemholder = item.Clone();
	}

	public virtual void LeftClick(DraggedItem DraggedItem){
		if(this.IsEmpty && DraggedItem.IsEmpty){
			return;
		}
		if(DraggedItem.IsEmpty){
			DraggedItem.InsertItem(this.Itemholder);
			this.Empty();
			return;
		}
		if(this.IsEmpty){//places items in empty slot
			this.InsertItem(DraggedItem.ItemHolder);
			DraggedItem.Empty();
			return;
		}
		else{//swaps items between draggeditem and selected inventoryslot
			var tempItemHolder = this.Itemholder;
			this.InsertItem(DraggedItem.ItemHolder);
			DraggedItem.InsertItem(tempItemHolder);
			return;
		}
	}

	public virtual void RightClick(DraggedItem draggedItem){
		int stackSpace = Math.Abs(this.StackSize - this.Amount);
		int transferAmount = draggedItem.ItemHolder.Amount == 1 ? 1 : draggedItem.ItemHolder.Amount / 2;
		if(stackSpace > transferAmount){
			draggedItem.Amount -= transferAmount;
		}//*/
	}
}
