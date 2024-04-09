using Godot;
using System.Linq;

public partial class InventoryManager : ContainerManager, IInsertItem
{
	private static InventoryManager _instance;

	public static InventoryManager Instance
	{
		get { return _instance; }
	}


	[Export] protected DraggedItem draggedItem;
	public override void _Ready()
	{
		base._Ready();
		_instance = this;
		if(draggedItem == null)
			GD.PrintErr($"{nameof(draggedItem)} is null");

		SetupInventoryGrid();
		Slot.Interact += this.SlotInteraction;
		InsertItem(ItemDatabase.Instance.GetItem("ItemParent:Test"), 100);
		InsertItem(ItemDatabase.Instance.GetItem("ItemParent:Test2"), 100);
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
