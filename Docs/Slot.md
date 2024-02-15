![banner](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkdownBanner.png)
# [Slot](../Inventory/Scripts/Slot.cs)

### Inherits [Panel](https://docs.godotengine.org/en/stable/classes/class_panel.html)

### Implements [IInsertable](../Inventory/Scripts/IInsertItem.cs)

#### Interact Event
Should only be subscribed to by the [InventoryManager](InventoryManager.md) or its equivalent

#### StyleBox
instead of using a Sprite2D, Slot uses a [Panel](https://docs.godotengine.org/en/stable/classes/class_panel.html) with a [StyleBoxTexture](https://docs.godotengine.org/en/stable/classes/class_styleboxtexture.html) theme for rendering item sprites, because it resizes the texture to the size of the node and sprite2d resizes the to texture size

#### IsEmpty
IsEmpty is a quick overridable way to check if slot is empty

#### UpdateSprite
used for changing the sprite of [StyleBox](#stylebox)

#### Empty
Method for empty slot

#### UpdateAmount
Changes text on amountlabel and makes it invisible if amount == 1

#### InsertItem
This object has 2 InsertItems one is from an interface the other is an easy way to give starter items

#### LeftClick RightClick
Logic that decides what happens when clicking the slot

#### AssignEvents
Gets called in Ready Method assign GuiInput to [Input](#input)

#### Input
Invokes [Interact](#interact-event) event which is a static event which should only be subscribed to by the [InventoryManager](InventoryManager.md) or the equivalent

![Watermark](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkDownWatermark.png)