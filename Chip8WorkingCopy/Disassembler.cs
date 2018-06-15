// This class will read a hex dump and print out formatted 8080 assembly
// langauge.

/*
 * Required structures
 *     codebuffer: the pointer to the current place in the file
 *     pc: The number of instructions since the start of the file
 */

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Vulkan;

namespace Chip8WorkingCopy
{
    public class Disassembler
    {
        // 1. Read code from the buffer

        // 2. Go to beginning of buffer

        // 3. Use the byte to determine the opcode

        // 4. Print name of opcode using bytes after the opcode as data, if applicable

        // 5. Advance the pointer the number of reads used by that instruction (1, 2 or 3)

        // 6. If not the end of buffer, go to step 3

        public void Disassemble8080(string fileToRead)
        {
            var byteArray = File
                .ReadAllBytes(fileToRead)
                .ToArray();

            var pc = 0;
            var span = 1;
            var i = 0;

            while (i != byteArray.Length - 1)
            {
                byte b1 = 0;
                byte b2 = 0;
                byte b3 = 0;
                byte b4 = 0;
                byte b5 = 0;
                byte b6 = 0;
                try
                {
                    b2 = byteArray[i + 1];
                    b3 = byteArray[i + 2];
                    b4 = byteArray[i + 3];
                    b5 = byteArray[i + 4];
                    b6 = byteArray[i + 5];
                }
                catch (Exception e)
                {
                    
                }
                ushort opcode = CombineBytes(b1, b2);
                ushort opcode2 = CombineBytes(b3, b4);
                ushort opcode3 = CombineBytes(b5, b6);
                
                Console.Write("0x{0:x3} ", i + 1);
                
                switch (opcode)
                {
                    case 0x00:
                        Console.WriteLine("NOP");
                        span = 1;
                        break;
                    case 0x01:
                        Console.WriteLine($"LXI\tB, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    case 0x02:
                        Console.WriteLine($"STAX\tB");
                        span = 1;
                        break;
                    case 0x03:
                        Console.WriteLine($"INX\tB");
                        span = 3;
                        break;
                    case 0x04:
                        Console.WriteLine($"INR\tB");
                        span = 1;
                        break;
                    case 0x05:
                        Console.WriteLine("DCR\tB");
                        span = 1;
                        break;
                    case 0x06:
                        Console.WriteLine($"MVI\tB, #${opcode2:X}");
                        span = 2;
                        break;
                    case 0x07:
                        Console.WriteLine("RLC");
                        span = 1;
                        break;
                    case 0x08:
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    case 0x09:
                        Console.WriteLine("DAD\tB");
                        span = 1;
                        break;
                    case 0x0a:
                        Console.WriteLine("LDAX\tB");
                        span = 1;
                        break;
                    case 0x0b:
                        Console.WriteLine("DCX\tB");
                        span = 1;
                        break;
                    case 0x0c:
                        Console.WriteLine("INR\tC");
                        span = 1;
                        break;
                    case 0x0d:
                        Console.WriteLine("DCR\tC");
                        span = 1;
                        break;
                    case 0x0e:
                        Console.WriteLine($"MVI\tC, {opcode2:X}");
                        span = 2;
                        break;
                    case 0x0f:
                        Console.WriteLine("RRC");
                        span = 1;
                        break;
                    case 0x10:
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    case 0x11:
                        Console.WriteLine($"LXI\tD, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    case 0x12:
                        Console.WriteLine("STAX\tD");
                        span = 1;
                        break;
                    case 0x13:
                        Console.WriteLine("INX\tD");
                        span = 1;
                        break;
                    case 0x14:
                        Console.WriteLine("INR\tD");
                        span = 1;
                        break;
                    case 0x15:
                        Console.WriteLine("DCR\tD");
                        span = 1;
                        break;
                    case 0x16:
                        Console.WriteLine($"MVI\tD, #${opcode2:X}");
                        span = 2;
                        break;
                    case 0x17:
                        Console.WriteLine("RAL");
                        span = 1;
                        break;
                    case 0x18:
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    case 0x19:
                        Console.WriteLine("DAD\tD");
                        span = 1;
                        break;
                    case 0x1a:
                        Console.WriteLine("LDAX\tD");
                        span = 1;
                        break;
                    case 0x1b:
                        Console.WriteLine("DCX\tD");
                        span = 1;
                        break;
                    case 0x1c:
                        Console.WriteLine("INR\tE");
                        span = 1;
                        break;
                    case 0x1d:
                        Console.WriteLine("DCR\tE");
                        span = 1;
                        break;
                    case 0x1e:
                        Console.WriteLine($"MVI\tE, #${opcode2:X}");
                        span = 2;
                        break;
                    case 0x1f:
                        Console.WriteLine("RAR");
                        span = 1;
                        break;
                    default:
                        Console.WriteLine("-");
                        span = 1;
                        break;
                }

                i += 2 * span;
            }
        }

        ushort CombineBytes(byte b1, byte b2)
        {
            ushort combined = (ushort) (b1 << 8 | b2);
            return combined;
        }
    }
}