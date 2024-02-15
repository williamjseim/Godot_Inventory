![banner](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkdownBanner.png)
# item
##### Inherits [Resource](https://docs.godotengine.org/en/stable/classes/class_resource.html#class-resource)

### [Id](#string-id)


#### Inheritance
>if you want make different types of items inherit from this class never add to this class unless you know what you're doing

>#### Equals
>you should check if the itemholder is equal to itemholder instead of checking if item is equal to item

>#### string Id
>i like how minecraft has changed it id to be a string like Parent:ItemName this makes it very easy to make crafting recipes and add more items down the line and even items through mods but if you dont want that you could pretty easily make it use a numbering system instead

>###### string id vs number id
>using a numbering system for ids is makes it faster to compare ids between 2 items so that would be better for a mmo or always online game but incase you want to make your game moddable adding more items and crafting recipes get harder since 2 mods could be using the same id for items and dynamically set ids are almost impossible for crafting systems without using file references for every recipe

>#### making item with json or xml
>depending on how many items you want to have in your game you should think about using json or xml for storing items instead of godot's [Resources](https://docs.godotengine.org/en/stable/classes/class_resource.html#class-resource) this would make it easier to load lots of items it might not speed up image loading if you have all images separated then you should use a sprite sheet or
[Texture Atlas](https://docs.godotengine.org/en/stable/classes/class_atlastexture.html)

![Watermark](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkDownWatermark.png)