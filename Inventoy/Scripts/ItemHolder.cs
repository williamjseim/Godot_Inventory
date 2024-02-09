using Godot;
using System;
using System.Text.Json.Serialization;

[Serializable]
public class ItemHolder
{
  public ItemHolder(){ }

  public ItemHolder(Item item, int amount)
  {
    this.Item = item;
    this.Amount = amount;
  }

	[JsonIgnore]
	public Item Item { get; set; } // resources cant be serialized and therefore not saved to a json file
	public int Id { get { return this.Item == null ? -1 : Item.id; }}// -1 means the slot is empty
	public int Amount { get; set; }//amount of items in slot
  public Texture2D Texture { get{ return this.Item == null ? null : this.Item.ItemSprite; } }

  public override bool Equals(object obj)
  {
    if(obj is ItemHolder itemHolder && itemHolder.Id == this.Id)
      return true;
    if(obj is Item item && this.Item == item){
      return true;
    }

    return false;
  }
  public override int GetHashCode()
  {
      return base.GetHashCode();
  }

  public static ItemHolder Empty => new ItemHolder(){Item = null};
}
