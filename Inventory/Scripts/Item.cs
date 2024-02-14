using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{

  public Item(){}
  public Item(string id, Item item)
  {
    this.ItemSprite = item.ItemSprite;
    this.StackSize = item.StackSize;
    this.Name = item.Name;
    this.id = id;
  }


	[Export] public string Name { get; protected set;}
	[Export] public string id { get; protected set; } = Item.Empty; // id for saving item to json like how minecraft does it so ParentFolder:ItemName
	[Export] public virtual Texture2D ItemSprite { get; protected set; } // item image
	[Export] public virtual int StackSize { get; protected set; } = 999; // max stack size

  /// <summary>
  ///  Quick way to mark item id as empty and make sure the spelling is alway correct
  /// </summary>
  public static string Empty { get { return "Empty"; } }
  public override bool Equals(object obj)
  {
    return this.id == (obj as Item).id;
  }

  public override int GetHashCode()
  {
      return base.GetHashCode();
  }
}
