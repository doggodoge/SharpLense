using Veldrid;
using Veldrid.StartupUtilities;

namespace Chip8WorkingCopy
{
    class Program
    {
        private static GraphicsDevice _graphicsDevice;
        
        static void Main(string[] args)
        {
            // Setup windowing system for .NET Core to
            // display graphics from emulator
            
            var windowCi = new WindowCreateInfo
            {
                X = 100,
                Y = 100,
                WindowWidth = 960,
                WindowHeight = 540,
                WindowTitle = "Chip8 Emulator"
            };
            var window = VeldridStartup.CreateWindow(ref windowCi);

            _graphicsDevice = VeldridStartup.CreateGraphicsDevice(window);

            while (window.Exists)
            {
                window.PumpEvents();
            }
        }
    }
}