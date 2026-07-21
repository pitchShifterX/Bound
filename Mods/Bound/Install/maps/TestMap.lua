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
        },
        tiles = tiles
    }
end