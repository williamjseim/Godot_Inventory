using Godot;
using System;

public interface ISaveAble
{
	public object Save();
	public void Load(object obj);
}
