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
using System.Xml;

namespace Chip8WorkingCopy
{
    public class Disassembler
    {
    
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
                    case 0x20:
                        Console.WriteLine("RIM");
                        span = 1;
                        break;
                    case 0x21:
                        Console.WriteLine($"LXI\tH, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    case 0x22:
                        Console.WriteLine($"SHLD\t0x{opcode2:X}");
                        span = 3;
                        break;
                    case 0x23:
                        Console.WriteLine("INX\tH");
                        span = 1;
                        break;
                    case 0x24:
                        Console.WriteLine("INR\tH");
                        span = 1;
                        break;
                    case 0x25:
                        Console.WriteLine("DCR\tH");
                        span = 1;
                        break;
                    case 0x26:
                        Console.WriteLine($"MVI\tH, #${opcode2:X}");
                        span = 2;
                        break;
                    case 0x27:
                        Console.WriteLine("DAA");
                        span = 1;
                        break;
                    case 0x28:
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    case 0x29:
                        Console.WriteLine("DAD\tH");
                        span = 1;
                        break;
                    case 0x2a:
                        Console.WriteLine($"LHLD\t0x{opcode2:X}");
                        span = 3;
                        break;
                    case 0x2b:
                        Console.WriteLine("DCX\tH");
                        span = 1;
                        break;
                    case 0x2c:
                        Console.WriteLine("INR\tL");
                        span = 1;
                        break;
                    case 0x2d:
                        Console.WriteLine("DCR\tL");
                        span = 1;
                        break;
                    case 0x2e:
                        Console.WriteLine($"MVI\tL, #${opcode2:X}");
                        span = 2;
                        break;
                    case 0x2f:
                        Console.WriteLine($"CMA");
                        span = 1;
                        break;
                    case 0x30:
                        Console.WriteLine("SIM");
                        span = 1;
                        break;
                    case 0x31:
                        Console.WriteLine($"LXI\tSP, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    case 0x32:
                        Console.WriteLine($"STA\t0x{opcode2:X}");
                        span = 3;
                        break;
                    case 0x33:
                        Console.WriteLine("INX\tSP");
                        span = 1;
                        break;
                    case 0x34:
                        Console.WriteLine("INR\tM");
                        span = 1;
                        break;
                    case 0x35:
                        Console.WriteLine("DCR\tM");
                        span = 1;
                        break;
                    case 0x36:
                        Console.WriteLine($"MVI\tM, #${opcode2:X}");
                        span = 2;
                        break;
                    case 0x37:
                        Console.WriteLine("STC");
                        span = 1;
                        break;
                    case 0x38:
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    case 0x39:
                        Console.WriteLine("DAD\tSP");
                        span = 1;
                        break;
                    case 0x3a:
                        Console.WriteLine($"LDA\t0x{opcode2:X}");
                        span = 3;
                        break;
                    case 0x3b:
                        Console.WriteLine("DCX\tSP");
                        span = 1;
                        break;
                    case 0x3c:
                        Console.WriteLine("INR\tA");
                        span = 1;
                        break;
                    case 0x3d:
                        Console.WriteLine("DCR\tA");
                        span = 1;
                        break;
                    case 0x3e:
                        Console.WriteLine($"MVI\tA, #${opcode2:X}");
                        span = 3;
                        break;
                    case 0x3f:
                        Console.WriteLine("CMC");
                        span = 1;
                        break;
                    case 0x40:
                        Console.WriteLine("MOV\tB, B");
                        span = 1;
                        break;
                    case 0x41:
                        Console.WriteLine("MOV\tB, C");
                        span = 1;
                        break;
                    case 0x42:
                        Console.WriteLine("MOV\tB, D");
                        span = 1;
                        break;
                    case 0x43:
                        Console.WriteLine("MOV\tB, E");
                        span = 1;
                        break;
                    case 0x44:
                        Console.WriteLine("MOV\tB, H");
                        span = 1;
                        break;
                    case 0x45:
                        Console.WriteLine("MOB\tB, L");
                        span = 1;
                        break;
                    case 0x46:
                        Console.WriteLine("MOV\tB, M");
                        span = 1;
                        break;
                    case 0x47:
                        Console.WriteLine("MOV\tB, A");
                        span = 1;
                        break;
                    case 0x48:
                        Console.WriteLine("MOV\tC, B");
                        span = 1;
                        break;
                    case 0x49:
                        Console.WriteLine("MOV\tC, C");
                        span = 1;
                        break;
                    case 0x4a:
                        Console.WriteLine("MOV\tC, D");
                        span = 1;
                        break;
                    case 0x4b:
                        Console.WriteLine("MOV\tC, E");
                        span = 1;
                        break;
                    case 0x4c:
                        Console.WriteLine("MOV\tC, H");
                        span = 1;
                        break;
                    case 0x4d:
                        Console.WriteLine("MOV\tC, L");
                        span = 1;
                        break;
                    case 0x4e:
                        Console.WriteLine("MOV\tC, M");
                        span = 1;
                        break;
                    case 0x4f:
                        Console.WriteLine("MOV\tC, A");
                        span = 1;
                        break;
                    case 0x50:
                        Console.WriteLine("MOV\tD, B");
                        span = 1;
                        break;
                    case 0x51:
                        Console.WriteLine("MOV\tD, C");
                        span = 1;
                        break;
                    case 0x52:
                        Console.WriteLine("MOV\tD, D");
                        span = 1;
                        break;
                    case 0x53:
                        Console.WriteLine("MOV\tD, E");
                        span = 1;
                        break;
                    case 0x54:
                        Console.WriteLine("MOV\tD, H");
                        span = 1;
                        break;
                    case 0x55:
                        Console.WriteLine("MOV\tD, L");
                        span = 1;
                        break;
                    case 0x56:
                        Console.WriteLine("MOV\tD, M");
                        span = 1;
                        break;
                    case 0x57:
                        Console.WriteLine("MOV\tD, A");
                        span = 1;
                        break;
                    case 0x58:
                        Console.WriteLine("MOV\tE, B");
                        span = 1;
                        break;
                    case 0x59:
                        Console.WriteLine("MOV\tE, C");
                        span = 1;
                        break;
                    case 0x5a:
                        Console.WriteLine("MOV\tE, D");
                        span = 1;
                        break;
                    case 0x5b:
                        Console.WriteLine("MOV\tE, E");
                        span = 1;
                        break;
                    case 0x5c:
                        Console.WriteLine("MOV\tE, H");
                        span = 1;
                        break;
                    case 0x5d:
                        Console.WriteLine("MOV\tE, L");
                        span = 1;
                        break;
                    case 0x5e:
                        Console.WriteLine("MOV\tE, M");
                        span = 1;
                        break;
                    case 0x5f:
                        Console.WriteLine("MOV\tE, A");
                        span = 1;
                        break;
                    case 0x60:
                        Console.WriteLine("MOV\tH, B");
                        span = 1;
                        break;
                    case 0x61:
                        Console.WriteLine("MOV\tH, C");
                        span = 1;
                        break;
                    case 0x62:
                        Console.WriteLine("MOV\tH, D");
                        span = 1;
                        break;
                    case 0x63:
                        Console.WriteLine("MOV\tH, E");
                        span = 1;
                        break;
                    case 0x64:
                        Console.WriteLine("MOV\tH, H");
                        span = 1;
                        break;
                    case 0x65:
                        Console.WriteLine("MOV\tH, L");
                        span = 1;
                        break;
                    case 0x66:
                        Console.WriteLine("MOV\tH, M");
                        span = 1;
                        break;
                    case 0x67:
                        Console.WriteLine("MOV\tH, A");
                        span = 1;
                        break;
                    case 0x68:
                        Console.WriteLine("MOV\tL, B");
                        span = 1;
                        break;
                    case 0x69:
                        Console.WriteLine("MOV\tL, C");
                        span = 1;
                        break;
                    case 0x6a:
                        Console.WriteLine("MOV\tL, D");
                        span = 1;
                        break;
                    case 0x6b:
                        Console.WriteLine("MOV\tL, E");
                        span = 1;
                        break;
                    case 0x6c:
                        Console.WriteLine("MOV\tL, H");
                        span = 1;
                        break;
                    case 0x6d:
                        Console.WriteLine("MOV\tL, L");
                        span = 1;
                        break;
                    case 0x6e:
                        Console.WriteLine("MOV\tL, M");
                        span = 1;
                        break;
                    case 0x6f:
                        Console.WriteLine("MOV\tL, A");
                        span = 1;
                        break;
                    case 0x70:
                        Console.WriteLine("MOV\tM, B");
                        span = 1;
                        break;
                    case 0x71:
                        Console.WriteLine("MOV\tM, C");
                        span = 1;
                        break;
                    case 0x72:
                        Console.WriteLine("MOV\tM, D");
                        span = 1;
                        break;
                    case 0x73:
                        Console.WriteLine("MOV\tM, E");
                        span = 1;
                        break;
                    case 0x74:
                        Console.WriteLine("MOV\tM, H");
                        span = 1;
                        break;
                    case 0x75:
                        Console.WriteLine("MOV\tM, L");
                        span = 1;
                        break;
                    case 0x76:
                        Console.WriteLine("HLT");
                        span = 1;
                        break;
                    case 0x77:
                        Console.WriteLine("MOV\tM, A");
                        span = 1;
                        break;
                    case 0x78:
                        Console.WriteLine("MOV\tA, B");
                        span = 1;
                        break;
                    case 0x79:
                        Console.WriteLine("MOV\tA, C");
                        span = 1;
                        break;
                    case 0x7a:
                        Console.WriteLine("MOV\tA, D");
                        span = 1;
                        break;
                    case 0x7b:
                        Console.WriteLine("MOV\tA, E");
                        span = 1;
                        break;
                    case 0x7c:
                        Console.WriteLine("MOV\tA, H");
                        span = 1;
                        break;
                    case 0x7d:
                        Console.WriteLine("MOV\tA, L");
                        span = 1;
                        break;
                    case 0x7e:
                        Console.WriteLine("MOV\tA, M");
                        span = 1;
                        break;
                    case 0x7f:
                        Console.WriteLine("MOV\tA, A");
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