# Map Development

Before developing a map for a mod, there are a few things to be aware of first.

1. The engine has a base map system consisting of some pre-defined objects, variables, etc. For example, the entry point to a map is `main()`. It returns a lua table of data.
2. Mods may extend this system functionality by adding its own pre-defined objects, trigger types, etc.
3. Since the default mod is Bound, the engine and Bound mod will be 1:1 in how maps are developed.

## Example Bound Map

```lua
function main()
    return {
        metadata = {
            title = "Test Map",
            description = "For testing and debugging Bound maps.",
            author = "pitch",
            width = 64,
            height = 64,
            players = {
                one = {
                    color = "red",
                    human = true,
                },
                two = {
                    color = "white",
                    human = false,
                },
                three = {
                    color = "yellow",
                    human = false,
                }
            },
        }
    }
end
```

Looking at the metadata, you can see basic information about the map title, description, the author, width and height, and players.

### Width and Height

A single tile in this engine is 32x32 pixels. This cannot be overridden.

When you define the width as 64, you're saying you want 64 tiles horizontally. This means your map is 2048 pixels wide. When it comes to screen resolution, this is accounted for with the engine providing a camera that has a standardized zoom-level.

### Players

Each player object is first defined by its key. `one`, `two`, and `three` are player ids. This will be used when writing triggers to refer to the player.

Currently, there is no multiplayer support but this engine is being built with it in mind for the future.

Players will also be given a player color which recolors certain pieces of units in their spritesheet.