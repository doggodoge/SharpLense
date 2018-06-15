using Veldrid;
using Veldrid.StartupUtilities;

namespace Chip8WorkingCopy
{
    class Program
    {
        private static GraphicsDevice _graphicsDevice;
        
        static void Main()
        {
            var disassembler = new Disassembler();
            
            disassembler.Disassemble8080("./INVADERS");
        }
    }
}