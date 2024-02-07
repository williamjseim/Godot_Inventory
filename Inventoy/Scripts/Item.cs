using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
	[Export] public string Name { get; protected set;}
	[Export] public int id { get; protected set; } // id for saving item to json
	[Export] public virtual Texture2D ItemSprite { get; protected set; } // item image
	[Export] public virtual int StackSize { get; protected set; } = 999; // max stack size

    public override bool Equals(object obj)
    {
		return this.id == (obj as Item).id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
