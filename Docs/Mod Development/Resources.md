# Resources

Resources refers to textures, sounds, fonts, and other assets that will be used in your mod.

## How it Works

When you instantiate your mod, a `SceneManager` is instantiated with it under the ModContext property (which also hosts other managers). Another instantiated manager is the `ResourceManager`. This grants you access to loading and unloading your resources. The `ResourceManager` allows you to load persistent resources which carries across scenes -- they exist over the engine's lifetime. When you initialize a scene, your scene resources are loaded and unloaded when scenes change. If a scene exists on the stack, it will keep all resources. This means if you use `SceneManager.PushScene()`, the next scene is an overlay. `ReplaceScene()` and `PopScene()` (if it's on the current scene) will unload the scene's resources.

So let's say you want to load in some textures. In your scene's `Load()` method, you will use the `Context` property to load in those textures. While Scene constructors require a resource manager, it is only passed to the scene's `Context` property. You will load, retrieve, and unload your resources that are relevant to your scene from the context. The context stores what resources you've loaded for that specific scene; so when you write `UnloadAllResources()` for example, it's only going to unload all of the resources that your scene has created.

> [!NOTE]
> All scene resources are transient and will not persist across other scenes. Resources you wish to persist should be loaded outside of scenes, directly using the ModContext's ResourceManager property. In a scene, however, you can request persistent resources.

## Audio Resources

In your scene, you're going to first get the path to the audio via `Context.Paths` property. Then you'll load it. It's recommended to assign a local, private variable in your scene to your resources so you don't have to requery every frame.

Aftewards, you can play the music via `Context.PlayMusic()`. By default, `PlayMusic()` always loops. You can specify in the second parameter how many times you wish for it to loop e.g. `PlayMusic("my_sound", 5)` will loop 5 times.


```cs
public class MyScene(IModContext modContext)
    : Scene(modContext)
{
    private Audio? _audio;

    public override void Load()
    {
        var audioPath = Context.Paths.GetAssetPath("audio/my_sound.wav");

        Context.Load<Audio>("my_sound", audioPath);
        _audio = Context.GetById<Audio>("my_sound");

        Context.PlayMusic("my_sound");
    }
}
```

## Font Resources

to do

## Texture Resources

to do