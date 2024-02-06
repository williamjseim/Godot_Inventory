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

	public virtual void ChangeSprite(Texture2D sprite){
		this.itemSprite.Texture = sprite;
	}

	public virtual void PlaceStack(DraggedItem DraggedItem){
		if(this.IsEmpty){
			this.itemHolder = DraggedItem.itemHolder;
			DraggedItem.Destroy();
		}
	}

	public virtual void PlaceHalfStack(DraggedItem itemHolder){

	}
}
