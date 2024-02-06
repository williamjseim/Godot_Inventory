using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
	[Export] public int id { get; set; } // id for saving item to json
	[Export] public virtual Texture2D ItemSprite { get; set; } // item image
	[Export] public virtual int StackSize { get; set; } = 999; // max stack size
}
