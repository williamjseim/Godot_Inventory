![banner](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkdownBanner.png)
# [ItemHolder](../Inventory/Scripts/ItemHolder.cs) 

#### Implements [ISaveAble](ISaveAble.md)

as the name suggests it holds a reference to an [Item](Item.md) since resources then loaded only make a new instance if it hasnt been loaded before so changing a value on an item changes it on every one of them unless you make them unique but that add unessery overhead and doesnt adhere to [SOLID](https://en.wikipedia.org/wiki/SOLID) specifically the S which stands for [Single responsibility](https://en.wikipedia.org/wiki/Single_responsibility_principle) instead all [modifiers](#modifiers) to the item should be stored in the itemholder and 

 you can read more about how resources are loaded and stored [here](https://docs.godotengine.org/en/stable/classes/class_resource.html#class-resource)

instead of remaking every item in every slot in every container and for ease of moving the data between slots 


#### Modifiers
The standard ItemHolder has 2 Modifiers Amount and Name,

Amount is just the number of items in the itemholder,

Name is if you want to set a custom name for a rare version of a standard sword, like Terraria where if you craft a really good Copper Sword its called Godly Copper Sword or if you craft a really bad one its called Terrible Copper Sword,

You could also add an enchantment list that changes the behavior of the item
#### Clone
since ItemHolder is [Class](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes) and classes are passed as a reference instead of a value like a [Struct](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/structs) we have to create a new Itemholder, then passing the Itemholder to a new slot since if the Itemholder is not cloned and we change it in the original slot it gets changed everywhere and the reason we cant just use a constructor is you dont always know what type the Itemholder is, since you can you inherit from it

the reason its not a [Struct](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/structs) isstruct can not be inherited by other struct so then all modifiers items could have would need to be on the in the same struct which would kinda go against the [O](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) in [SOLID](https://en.wikipedia.org/wiki/SOLID)

![Watermark](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkDownWatermark.png)