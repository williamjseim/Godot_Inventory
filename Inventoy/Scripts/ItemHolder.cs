using Godot;
using System;
using System.Text.Json.Serialization;

[Serializable]
public class ItemHolder
{
	[JsonIgnore]
	public Item Item { get; set; } // resources cant be serialized and therefore not saved to a json file
	public int Id { get { return this.Item == null ? -1 : Item.id; }}// -1 means the slot is empty
	public int Amount { get; set; }//amount of items in slot

    public override bool Equals(object obj)
    {
		if(obj is ItemHolder itemHolder && itemHolder.Id == this.Id)
			return true;

		return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
