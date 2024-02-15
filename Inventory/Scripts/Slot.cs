using Godot;
using System;
using System.Threading;

public partial class Slot : Panel, IInsertItem
{
	[Export] private Label amountLabel;
	protected ItemHolder holder = ItemHolder.Empty;

	/// <summary>
	/// use InsertItem to replace the itemholder
	/// </summary>
	public ItemHolder Itemholder { get { return this.holder; } protected set{
		this.holder = value;
		UpdateSprite(value.Equals(ItemHolder.Empty) ? null : value.Item.ItemSprite);
		UpdateAmount(this.Amount);
	}}
	
	protected StyleBoxTexture stylebox = new();
	public bool IsEmpty {
		get{
			return Itemholder.Equals(ItemHolder.Empty);
		}
	}

	public virtual int Amount {get { return (this.Itemholder.Equals(ItemHolder.Empty) || this.Itemholder.Id == Item.Empty) ? 0 : this.holder.Amount; } protected set
		{ 
			this.holder.Amount = value;
			if(this.Amount <= 0){
				this.Itemholder = ItemHolder.Empty;
			}
			UpdateAmount(this.Amount);
		}
	}
	protected virtual int StackSize {get { return (this.Itemholder.Equals(ItemHolder.Empty) || this.Itemholder.Id == Item.Empty) ? 0 : this.Itemholder.Item.StackSize; }}
	
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

	public virtual void InsertItem(Item item, int amount){
		this.Itemholder = new(item, amount);
	}

	public virtual void LeftClick(DraggedItem DraggedItem){
		if(this.IsEmpty && DraggedItem.IsEmpty){
			return;
		}
		if(DraggedItem.IsEmpty){//picks up stack
			DraggedItem.InsertItem(this.Itemholder);
			this.Empty();
			return;
		}
		if(this.IsEmpty){//places items in empty slot
			this.InsertItem(DraggedItem.ItemHolder);
			DraggedItem.Empty();
			return;
		}
		if(this.Itemholder == DraggedItem.ItemHolder){//combines stack
			CombineStacks(DraggedItem);
			return;
		}
		//swaps items between draggeditem and selected inventoryslot
		SwapItems(DraggedItem);
	}

	public virtual void CombineStacks(DraggedItem draggedItem){
		int stackSpace = Math.Abs(this.Amount - this.StackSize);
		if(draggedItem.Amount > stackSpace){
			draggedItem.Amount -= stackSpace;
			this.Amount += stackSpace;
			return;
		}
		this.Amount += draggedItem.Amount;
		draggedItem.Empty();
	}

	public virtual void RightClick(DraggedItem draggedItem){
		if(draggedItem.IsEmpty){
			TakeHalfStack(draggedItem);
			return;
		}
		if(!draggedItem.IsEmpty && (this.IsEmpty || this.Itemholder == draggedItem.ItemHolder)){
			PlaceSingleItem(draggedItem);
			return;
		}
		SwapItems(draggedItem);
	}

	public virtual void TakeHalfStack(DraggedItem draggedItem){
		int transferAmount = this.Amount == 1 ? 1 : this.Amount / 2;
		draggedItem.InsertItem(new ItemHolder(this.Itemholder.Item, transferAmount));
		this.Amount -= transferAmount;

	}

	public virtual void PlaceSingleItem(DraggedItem draggedItem){
		if(this.IsEmpty){
			this.InsertItem(draggedItem.ItemHolder);
			this.Amount = 1;
			draggedItem.Amount--;
			return;
		}
		draggedItem.Amount--;
		this.Amount++;
		return;
	}

	public virtual void SwapItems(DraggedItem draggedItem){
		var tempItemHolder = this.Itemholder;
		this.InsertItem(draggedItem.ItemHolder);
		draggedItem.InsertItem(tempItemHolder);
		return;
	}
}
