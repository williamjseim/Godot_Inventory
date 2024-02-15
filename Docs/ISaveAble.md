![banner](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkdownBanner.png)
# [ISaveAble](../Inventory/Scripts/ISaveAble.cs)

#### i only included this interface to show an example of how you could save inventory data

this interface makes looping through all objects in the [Scene tree](https://docs.godotengine.org/en/stable/classes/class_scenetree.html) to check if they need to be saved or can just be ignored

### Save
the save method returns an object of what every data youre trying to save

### Load
load requires a deserialized object to load the state of the object implementing ISaveAble

this is a good way to save object that inherit from godot objects since so you can choose what veriables need to be changed and if you have something that inherits from dog that also need to be saved you can make a class that inherits DogState and then use contructor chaining. Remember then serializing derived classes enable type name handling and pass that to the (de)serializer as a setting parameter for both serializer and deserializer. I recommend [NewtonSoft.Json](https://www.newtonsoft.com/json) for json serialization

```C#
public class Dog : Node2d ISaveAble{
    public vector2 position;
    public string name;

    public object Save(){
        return new DogState(this);
    }

    public void Load(object state){
        DogState dog = state as DogState;
        this.position = dog.position;
        this.name = dog.name;
    }

    [Serializable]
    public class DogState{
        public vector2 position;
        public string name;
        public DogState(Dog dog){
            this.position = dog.position;
            this.name = dog.name;
        }
    }
}
```

since this example doesnt inherit from any godot classes it can be serialized directly constructors how ever cant really be used in this example to load the object since you cant call contructors inside its class, if you want to use constructor you should use a [factory pattern](https://en.wikipedia.org/wiki/Factory_method_pattern) or somekind of Manager or Controller like the [InventoryManager](InventoryManager.md)

```C#
[Serializable]
public class Cat : ISaveAble{
    public string name;
    public string age;

    public object Save(){
        return this;
    }

    public void Load(object state){
        Cat cat = state as Cat;
        this.name = cat.name;
        this.age = cat.age
    }
}
```

![Watermark](https://github.com/williamjseim/williamjseim/blob/main/Documentation/MarkDownWatermark.png)
