using System;

namespace Chip8WorkingCopy
{
    class Program
    {
        static void Main()
        {
            var disassembler = new Disassembler();
            
            disassembler.Disassemble8080("./INVADERS");
        }
    }
}
