using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class ItemDatabase
{
    private static ItemDatabase _instance;
    public static ItemDatabase Instance { get { if(_instance == null){ _instance = new(); } return _instance; } }

    string baseItemFolder = "res://Inventoy/Items";//change this to what ever path matches your project

    public ItemDatabase()
    {
        LoadItems();
    }
    public Dictionary<string, Item> Items = new();

    public Item this[string key]{
        get { return this.Items[key]; }
    }
    
    public Item GetItem(string id){
        if(this.Items.TryGetValue(id, out Item item)){
            return item;
        }
        GD.PrintErr($"Item id does not exist {id}");
        return null;
    }

    public void OverrideItemId(Item item, string id){
        Item newItem = new(id, item);
        ResourceSaver.Save(newItem, item.ResourcePath);
    }

    private void LoadItems(){
        var itemFolders = DirAccess.GetDirectoriesAt(baseItemFolder);//get actual path to item folder
        
        List<Item> foundItems = new();
        foreach (var file in DirAccess.GetFilesAt(baseItemFolder))
        {
            if(file.Contains(".tres") && ResourceLoader.Load($"{this.baseItemFolder}/{file}") is Item item){
                foundItems.Add(item);
            }
        }

        foreach (var item in foundItems)
        {
            string itemParent = this.baseItemFolder.Split("/").Last();
            string itemId = $"{itemParent}:{item.Name}";
            this.Items.Add(itemId, item);
            if(item.id == Item.Empty){
                OverrideItemId(item, itemId);
            }
            GD.Print(itemParent, item.Name);
        }
    }
}
