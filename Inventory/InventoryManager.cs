using Godot;
using System.Linq;

public partial class InventoryManager : ContainerManager, IInsertItem
{
	[Export] protected DraggedItem draggedItem;
	[Export] protected Item testItem;
	
	public override void _Ready()
	{
		base._Ready();
		if(draggedItem == null)
			GD.PrintErr($"{nameof(draggedItem)} is null");

		SetupInventoryGrid();
		Slot.Interact += this.SlotInteraction;
		InsertItem(ItemDatabase.Instance.Items.Last().Value, 100);
	}

	public override void _Process(double delta)
	{
		
	}

	/// <summary>
	/// gets called whenever player clicks inventory slot
	/// </summary>
	/// <param name="slot"></param>
	/// <param name="mouse"></param>
	public virtual void SlotInteraction(Slot slot, MouseButton mouse){
		if(mouse == MouseButton.Left){
			slot.LeftClick(draggedItem);
		}
		else if(mouse == MouseButton.Right){
			slot.RightClick(draggedItem);
		}
	}

	/// <summary>
	/// Use to add item 
	/// </summary>
	/// <param name="item"></param>
	/// <param name="amount"></param>
	public void InsertItem(Item item, int amount){
		foreach (Slot slot in slots)
		{
			if(slot.IsEmpty || slot.Itemholder.Equals(item)){
				slot.InsertItem(item, amount);
				break;
			}
		}
	}

    public void InsertItem(ItemHolder item)
    {
        foreach (Slot slot in slots)
		{
			if(slot.IsEmpty || slot.Itemholder.Equals(item)){
				slot.InsertItem(item);
				break;
			}
		}
    }

    public override void Close()
    {
		//quickly throw the item in an empty slot then inventory closes
		if(!this.draggedItem.IsEmpty){
			this.InsertItem(this.draggedItem.ItemHolder);
			this.draggedItem.Empty();
		}
    }


}
