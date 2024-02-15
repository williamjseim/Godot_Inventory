using Godot;
using System.Collections.Generic;

public class ItemDatabase
{
    private static ItemDatabase _instance;
    public static ItemDatabase Instance { get { if(_instance == null){ _instance = new(); } return _instance; } }

    string baseItemFolder = "res://Inventory/ItemParent";//change this to what ever path matches your project

    public ItemDatabase()
    {
        LoadItems();
    }
    
    public Dictionary<string, Item> Items = new();

    public Item this[string key]{
        get { return this.Items[key]; }
    }
    
    public Item GetItem(string id){
        if(id == Item.Empty){
            return null;
        }
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
        List<Item> foundItems = new();
        foreach (var file in DirAccess.GetFilesAt(baseItemFolder))
        {
            if(file.Contains(".tres") && ResourceLoader.Load($"{this.baseItemFolder}/{file}") is Item item){
                foundItems.Add(item);
            }
        }

        string[] foldersplit = baseItemFolder.Split("/");
        string itemParent = foldersplit[foldersplit.Length-2];
        
        foreach (var item in foundItems)
        {
            if(item.id != string.Empty){
                this.Items.Add(item.id, item);
                continue;
            }
            string itemId = $"{itemParent}:{item.Name}";
            this.Items.Add(itemId, item);
            if(item.id == Item.Empty){
                //item id gets save to the resource so it doesnt need to be set again
                OverrideItemId(item, itemId);
            }
        }
    }
}
