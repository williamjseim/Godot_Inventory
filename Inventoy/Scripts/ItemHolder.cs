using Godot;
using System;
using System.Text.Json.Serialization;

[Serializable]
public class ItemHolder
{
  public ItemHolder(){ Item = null; Amount = 0; }

  public ItemHolder(Item item, int amount)
  {
    this.Item = item;
    this.Amount = amount;
  }

  public ItemHolder(ItemHolder itemHolder)
  {
    this.Item = itemHolder.Item;
    this.Amount = itemHolder.Amount;
  }

	[JsonIgnore]
	public Item Item { get; set; } // resources cant be serialized and therefore not saved to a json file
	public int Id { get { return this.Item == null ? -1 : Item.id; }} // -1 means the slot is empty
	public int Amount { get; set; } //amount of items in slot
  public Texture2D Texture { get{ return this.Item == null ? null : this.Item.ItemSprite; } }

  /// <summary>
  /// Use this to check if itemholders are equal
  /// </summary>
  /// <param name="obj"></param>
  /// <returns>True if itemholders are equal</returns>
  public override bool Equals(object obj)
  {
    if(obj is ItemHolder itemHolder && itemHolder.Id == this.Id)
      return true;

    return false;
  }

  /// <summary>
  /// Should be called In the class that gonna use it
  /// </summary>
  /// <returns></returns>
  public virtual ItemHolder Clone(){
    return new ItemHolder(){
      Item = this.Item,
      Amount = this.Amount,
    };
  }
  //ignore
  public override int GetHashCode()
  {
      return base.GetHashCode();
  }

  public static ItemHolder Empty => new ItemHolder();

}