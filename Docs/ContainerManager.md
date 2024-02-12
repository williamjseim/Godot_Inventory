![banner](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkdownBanner.png)
# [ContainerManager](../Inventoy/Scripts/ContainerManager.cs)

[SlotContainer](#slotcontainer-1)
[Slot array](#slot-array-1)
[SlotAmount]()
[SlotScene]()

[SetupInventoryGrid()]()

#### SlotContainer
slotContainer is the [Container](https://docs.godotengine.org/en/stable/classes/class_container.html) that all the inventory slots are contained in you could change this to anything that inherits control but it should properly be a grid unless you want to place alle the slots manually which could be done but then you should turn off [AutoGenerateSlots](#autogenerateslots) which you can do from the editor just remember to populate the [slot array](#slot-array-1) or you'll get an <span style="color:red;">error<span>

#### Slot array
holds a reference to all slots in the container it either has to be populated manually in the editor or you can turn on [AutoGenerateSlots](#autogenerateslots) and it will generate a certain amount of slot then ever the game starts

#### Slot amount
tells [AutoGenerateSlots](#autogenerateslots) how many slots to make can be set either in the script or the editor

#### Slot scene
this is the [PackedScene](https://docs.godotengine.org/en/stable/classes/class_packedscene.html) that gets instatiated by [AutoGenerateSlots](#autogenerateslots)

#### AutoGenerateSlots [Readmore here](../Inventoy/Scripts/ContainerManager.cs)
if this is set to true whenever the game start it will create a certain amount of slots based on the [SlotAmount](#slot-amount)
if set to false then it will just take all children of [SlotContainer](#slotcontainer) and add them to [Slot array](#slot-array)


![Watermark](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkDownWatermark.png)