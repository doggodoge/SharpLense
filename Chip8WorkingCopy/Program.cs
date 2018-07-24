using System;
using System.Collections.Generic;

namespace Chip8WorkingCopy
{
    class Program
    {
        public static void Main(string[] args)
        {
            // This is just here for testing, but you should be able
            // to disassemble stuff from the console with this.
            
            var disassembler = new Disassembler();
            
            if (args != null)
            {
                var path = args[0];

                try
                {
                    var processedList = disassembler.Disassemble8080ToList(path);

                    processedList.ForEach(Console.WriteLine);
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not read file");
                }
            }
        }
    }
}