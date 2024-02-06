using Godot;
using System;

public partial class Slot : Panel
{
	protected ItemHolder itemHolder;
	protected StyleBoxTexture itemSprite = new();
	protected bool IsEmpty {
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

	public virtual void PlaceStack(DraggedItem DraggedItem){
		if(this.IsEmpty){
			this.itemHolder = DraggedItem.itemHolder;
			DraggedItem.Destroy();
		}
	}

	public virtual void PlaceHalfStack(DraggedItem draggedItem){
		int stackSpace = Math.Abs(this.StackSize - this.Amount);
		int transferAmount = draggedItem.itemHolder.Amount == 1 ? 1 : draggedItem.itemHolder.Amount / 2;
		if(stackSpace > transferAmount){
			draggedItem.itemHolder.Amount -= transferAmount;
			
		}
	}
}
