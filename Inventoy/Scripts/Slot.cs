using Godot;
using System;

public partial class Slot : Panel
{
	protected ItemHolder itemHolder;
	public virtual ItemHolder Itemholder { get {return this.itemHolder;} set {
		this.itemHolder = value;
	}}
	protected StyleBoxTexture itemSprite = new();
	public bool IsEmpty {
		get{
			if(itemHolder == null || itemHolder.Id == -1)
				return false;

			return true;
		}
	}

	public virtual int Amount {get { return (this.itemHolder == null || this.itemHolder.Id == -1) ? 0 : this.itemHolder.Amount; } protected set
		{ 
			this.itemHolder.Amount = value;
			if(this.Amount <= 0){
				itemHolder = null;
				UpdateSprite(null);
			}
		}
	}

	protected virtual int StackSize {get { return (this.itemHolder == null || this.itemHolder.Id == -1) ? 0 : this.itemHolder.Item.StackSize; }}
	public static event Action<Slot, MouseButton> Interact;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AssignEvents();
	}

	protected virtual void AssignEvents(){
		this.GuiInput += this.Input;
	}

	public void Input(InputEvent input){
		if(input is InputEventMouseButton mouse)
			Interact?.Invoke(this, mouse.ButtonIndex);
	}

	public virtual void UpdateSprite(Texture2D sprite){
		this.itemSprite.Texture = sprite;
	}

	public virtual void Empty(){
		this.itemHolder = ItemHolder.Empty;
	}

#region LeftClick
	public virtual void LeftClick(DraggedItem draggedItem){
		if(draggedItem.IsEmpty){
		}
		if(this.IsEmpty){//places items in empty slot
		}
		else{//swaps items between draggeditem and selected inventoryslot
		}
	}

	/// <summary>
	/// places dragged stack in slot
	/// </summary>
	/// <param name="draggedItem"></param>
	public virtual void PlaceStack(DraggedItem draggedItem){
		this.itemHolder = draggedItem.ItemHolder;
		draggedItem.Empty();
	}

	/// <summary>
	/// takes stack from slot
	/// </summary>
	/// <param name="draggedItem"></param>
	public virtual void TakeStack(DraggedItem draggedItem){
		draggedItem.ItemHolder = this.itemHolder;
		this.Empty();
	}

	/// <summary>
	/// Swaps stacks
	/// </summary>
	/// <param name="draggedItem"></param>
	public virtual void SwapStack(DraggedItem draggedItem){
		var tempItemHolder = this.itemHolder;
		this.itemHolder = draggedItem.ItemHolder;
		draggedItem.ItemHolder = tempItemHolder;
	}
#endregion

#region RightClick

	/// <summary>
	/// places one item into slot
	/// </summary>
	/// <param name="draggedItem"></param>
	public virtual void PlaceOneItem(DraggedItem draggedItem){
		if(SpaceAvailable(1)){
			this.Amount++;
			draggedItem.Amount--;
		}
	}
#endregion

/// <summary>
/// Checks if there a certain amount of space available
/// </summary>
/// <param name="Amount"></param>
/// <returns>true if space is available</returns>
public virtual bool SpaceAvailable(int Amount = 0){
	if((this.Amount + Amount) >= this.StackSize)
		return false;
		
	return true;
}

}
