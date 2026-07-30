function main()
    local tiles = {}
    
    for x = 0, 63 do
        for y = 0, 63 do
            local key = x .. "," .. y
            
            tiles[key] = {
                texture = "dirt"
            }
        end
    end

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
            locations = {
                {
                    name = "start_area",
                    tiles = {
                        x = 10,
                        y = 10,
                        w = 3,
                        h = 3
                    }
                },
                {
                    name = "red_loc",
                    tiles = {
                        x = 0,
                        y = 0,
                        w = 1,
                        h = 1
                    },
                    color = "red"
                }
            }
        },
        tiles = tiles,
        triggerGroups = {
            start = {
                name = "Map Initialization",
                description = "After 5 seconds, change welcome trigger group status.",
                enabled = true,
                triggers = {
                    init = {
                        name = "Init",
                        preserved = false,
                        conditions = {
                            ElapsedTime(5),
                        },
                        actions = {
                            SetTriggerGroupStatus("Welcome", true),
                            SetTriggerGroupStatus("Init", false)
                        }
                    }
                }
            },
            welcome = {
                name = "Welcome",
                description = "Displays a welcome message.",
                enabled = false,
                triggers = {
                    welcome = {
                        name = "Welcome Message",
                        preserved = false,
                        conditions = {
                            Always(),
                        },
                        actions = {
                            WriteToConsole("Howdy! This is pitch."),
                            Wait(5),
                            CreateUnitAtLocation("Bounder", "one", "start_area")
                        }
                    }
                }
            }
        }
    }
end