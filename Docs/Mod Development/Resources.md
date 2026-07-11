# Resources

Resources refers to textures, sounds, fonts, and other assets that will be used in your mod.

## How it Works

When you instantiate your mod, a `SceneManager` is instantiated with it under the ModContext property (which also hosts other managers). Another instantiated manager is the `ResourceManager`. This gives you access to loading and unloading your resources. The `ResourceManager` allows you to load persistent resources which carries across scenes -- they exist over the engine's lifetime. When you initialize a scene, your scene resources are loaded and unloaded when scenes change.

So let's say you want to load in some textures. In your scene's `Load()` method, you will use the `Context` property to load in those textures. While Scene constructors require a resource manager, it is only passed to the scene's `Context` property. You will load, retrieve, and unload your resources that are relevant to your scene from the context. The context stores what resources you've loaded for that specific scene; so when you write `UnloadAllResources()` for example, it's only going to unload all of the resources that your scene has created.

> [!NOTE]
> All scene resources are transient and will not persist across other scenes. Resources you wish to persist should be loaded outside of scenes, directly using the ModContext's ResourceManager property. In a scene, however, you can request persistent resources.