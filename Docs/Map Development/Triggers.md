# Trigger System

## Overview

The trigger system is an event-based scripting system to help control gameplay. I designed this trigger system around my experience with Starcraft Brood War's trigger system. I remembered the faults with some of the architecture and have tried to improve upon it.

For example, we start by having a `TriggerGroup` with a flag to determine if it's enabled. This is useful for efficiency. Back in the Starcraft days, we had "Switches" which were used to enable/disable triggers. For Bound maps, it was a way to say "okay, they beat this level, set this level to disabled and then enable the next level." This meant we had a `Condition` for checking the status of the switch and an `Action` for setting that switch's value.

The problem with this, behind the scenes, is that you're going to be constantly running through the game loop evaluating all of the triggers. With my implementation of `TriggerGroup`, the loop goes through each group and checks the `IsEnabled` property first before continuing. If false, we just skip over the trigger group. This means only enabled trigger groups are going to be continuously evaluated until the conditions are met.

So the workflow is simply:

- IF trigger group is enabled, we begin evaluating the list of triggers within the group.
- IF every condition is met for a given trigger, the trigger executes its actions.

The `TriggerGroup` is simply a wrapper of multiple triggers. This means if your `TriggerGroup` is about level one, the triggers inside of it will evaluate and execute under that level one context. Let's say you're on level one, and you want multiple triggers related to it. You might want a trigger to always play a specific song on level one, and you want another trigger to create explosions in a pattern. Both of those triggers will belong to the same `TriggerGroup`. This fulfills the need of switches in an organized and efficient manner.

## Trigger Groups

Container for multiple triggers.

Example:

```lua
triggerGroups = {
    start = {
        name = "Map Initialization",
        description = "After 5 seconds, write to console.",
        enabled = true,
        triggers = {
            init = {
                name = "Init",
                preserved = false,
                conditions = {
                    ElapsedTime(5),
                },
                actions = {
                    WriteToConsole("Welcome!");
                }
            }
        }
    },
}
```

### Properties

| Property | Type | Description |
|----------|------|-------------|
| name | string | Unique identifier. |
| description | string | Describes the intent of the trigger group. |
| enabled | bool | Determines whether triggers inside this group are evaluated. |
| triggers | Trigger[] | A list of triggers to be evaluated and executed. |

---

## Triggers

A trigger is a set of conditions and actions to drive gameplay.

Example:

```lua
triggers = {
    init = {
        name = "Init",
        preserved = false,
        conditions = {
            ElapsedTime(5),
        },
        actions = {
            WriteToConsole("Welcome!");
        }
    }
}
```

### Properties

| Property | Type | Description |
|----------|------|-------------|
| name | string | Unique identifier. |
| preserved | bool | Continuously evaluate and execute this trigger. |
| conditions | Condition[] | A set of conditions to be evaluated. |
| actions | Action[] | A set of actions to be executed when conditions are met. |

# Conditions

## Always

Always returns `true`.

### Parameters

None.

### Example

```lua
conditions = {
    Always(),
},
```

## ElapsedTime

Returns true once the specified amount of game time has elapsed.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | float | Time in seconds. |

### Example

```lua
conditions = {
    ElapsedTime(5),
},
```

## PlayerBringsUnitToLocation

Returns true if player brings any unit of specified name to location.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | int | Player id |
| args[1] | string | Unit name |
| args[2] | string | Location name |

### Example

```lua
conditions = {
    PlayerBringsUnitToLocation(0, "Bounder", "end_level_one")
},
```

# Actions

## CreateUnitAtLocation

Creates a unit for a specified player at location.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | string | Unit name |
| args[1] | int | Player id |
| args[2] | string | Location name |

### Example

```lua
actions = {
    CreateUnitAtLocation("Bounder", 0, "start")
},
```

## KillAllUnitsAtLocation

Kills all specified unit type for player at location.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | string | Unit name |
| args[1] | int | Player id |
| args[2] | string | Location name |

### Example

```lua
actions = {
    KillAllUnitsAtLocation("Bounder", 0, "start")
},
```

## SetTriggerGroupStatus

Enables or disables another Trigger Group.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | string | Trigger group name. |
| args[1] | bool | Value to set trigger group status. |

### Example

```lua
actions = {
    SetTriggerGroupStatus("Map Initializer", false),
},
```

## Wait

Wait between actions for a specified amount of seconds.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | float | Amount of seconds to wait. |

### Example

```lua
actions = {
    Wait(5),
},
```

## WriteToConsole

Prints a message to the engine console. Useful for debugging.

### Parameters

| Property | Type | Description |
|----------|------|-------------|
| args[0] | string | Text to print. |

### Example

```lua
actions = {
    WriteToConsole("Hello!"),
},
```