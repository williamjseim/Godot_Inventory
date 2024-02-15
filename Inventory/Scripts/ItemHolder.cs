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

  private Item item;
	[JsonIgnore]
	public Item Item { get{ return item; } set{
    this.item = value;
    this.id = item.id;
  } } // resources cant be serialized and therefore not saved to a json file
  [JsonIgnore]
  public Texture2D Texture { get{ return this.Item == null ? null : this.Item.ItemSprite; } }

  public string id;
	public string Id { get { return this.Item == null ? Item.Empty : Item.id; }} // -1 means the slot is empty
	public int Amount { get; set; } //amount of items in slot

  /// <summary>
  /// Usefull if you want a item to have a custom name and dont wanna override the name the item resource has
  /// </summary>
  private string name = string.Empty;
  public string Name
  {
    get { return this.name == string.Empty ? this.Item.Name : this.name; }
    protected set { name = value; }
  }
  
  /// <summary>
  /// Use this to check if itemholders are equal
  /// </summary>
  /// <param name="obj"></param>
  /// <returns>True if itemholders are equal</returns>
  public override bool Equals(object obj)
  {
    if(obj is ItemHolder itemHolder && itemHolder.Id == this.Id && this.name == itemHolder.name)
      return true;

    return false;
  }

  public static bool operator == (ItemHolder a, ItemHolder b){
    return a.Equals(b);
  }

  public static bool operator != ( ItemHolder a, ItemHolder b){
    return !a.Equals(b);
  }

  /// <summary>
  /// Itemholder cant be transfered between slots without cloning them if they dont get cloned they will get overriden no matter what slot the itemholder was transfered to
  /// Should be called In the class that gonna use it
  /// </summary>
  /// <returns></returns>
  public virtual ItemHolder Clone(){
    return new ItemHolder(){
      Item = this.Item,
      Amount = this.Amount,
    };
  }

  public override int GetHashCode()
  {
      return base.GetHashCode();
  }

    public static ItemHolder Empty => new ItemHolder();

}