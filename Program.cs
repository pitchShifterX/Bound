using SDL2;

class Program
{
    /// <summary>
    /// To do:
    /// 
    /// Change method to support selecting mod from 
    /// provided args.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine("SDL could not initialize! SDL_Error: " + SDL.SDL_GetError());
            return;
        }

        IntPtr window = SDL.SDL_CreateWindow(
            "My SDL2 C# Game",
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            800, 600,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
        );

        if (window == IntPtr.Zero)
        {
            Console.WriteLine("Window could not be created! SDL_Error: " + SDL.SDL_GetError());
            return;
        }

        IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

        bool running = true;
        SDL.SDL_Event e;

        while (running)
        {
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                    running = false;
            }

            SDL.SDL_SetRenderDrawColor(renderer, 30, 30, 30, 255);
            SDL.SDL_RenderClear(renderer);

            SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
            SDL.SDL_Rect rect = new SDL.SDL_Rect { x = 100, y = 100, w = 50, h = 50 };
            SDL.SDL_RenderFillRect(renderer, ref rect);

            SDL.SDL_RenderPresent(renderer);

            SDL.SDL_Delay(16); // ~60 FPS
        }

        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }
}
