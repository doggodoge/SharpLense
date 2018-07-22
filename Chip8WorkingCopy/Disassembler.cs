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

namespace Chip8WorkingCopy
{
    public class Disassembler
    {
    
        public void Disassemble8080(string fileToRead)
        {
            // Read bytes of 8080 binary to buffer
            var byteArray = File
                .ReadAllBytes(fileToRead)
                .ToArray();
            
            // Span specifies the amount of instructions to read ahead
            var i = 0;
            
            // An instruction is two bytes, so end of for is multiplied by 2
            while (i != byteArray.Length - 1)
            {
                var span = 1;
                
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
                catch (Exception)
                {
                        Console.WriteLine("Reached end of file");
                }
                
                // CombineBytes is a dodgy hack that uses bitshifting
                // to convert between datatypes, this should be changed
                ushort opcode = CombineBytes(b1, b2);
                ushort opcode2 = CombineBytes(b3, b4);
                ushort opcode3 = CombineBytes(b5, b6);
                
                Console.Write("0x{0:x3} ", i + 1);
                
                switch (opcode)
                {
                    case 0x00:
                    {
                        Console.WriteLine("NOP");
                        span = 1;
                        break;
                    }
                    case 0x01:
                    {
                        Console.WriteLine($"LXI\tB, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x02:
                    {
                        Console.WriteLine($"STAX\tB");
                        span = 1;
                        break;
                    }
                    case 0x03:
                    {
                        Console.WriteLine($"INX\tB");
                        span = 3;
                        break;
                    }
                    case 0x04:
                    {
                        Console.WriteLine($"INR\tB");
                        span = 1;
                        break;
                    }
                    case 0x05:
                    {
                        Console.WriteLine("DCR\tB");
                        span = 1;
                        break;
                    }
                    case 0x06:
                    {
                        Console.WriteLine($"MVI\tB, #${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x07:
                    {
                        Console.WriteLine("RLC");
                        span = 1;
                        break;
                    }
                    case 0x08:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0x09:
                    {
                        Console.WriteLine("DAD\tB");
                        span = 1;
                        break;
                    }
                    case 0x0a:
                    {
                        Console.WriteLine("LDAX\tB");
                        span = 1;
                        break;
                    }
                    case 0x0b:
                    {
                        Console.WriteLine("DCX\tB");
                        span = 1;
                        break;
                    }
                    case 0x0c:
                    {
                        Console.WriteLine("INR\tC");
                        span = 1;
                        break;
                    }
                    case 0x0d:
                    {
                        Console.WriteLine("DCR\tC");
                        span = 1;
                        break;
                    }
                    case 0x0e:
                    {
                        Console.WriteLine($"MVI\tC, {opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x0f:
                    {
                        Console.WriteLine("RRC");
                        span = 1;
                        break;
                    }
                    case 0x10:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0x11:
                    {
                        Console.WriteLine($"LXI\tD, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x12:
                    {
                        Console.WriteLine("STAX\tD");
                        span = 1;
                        break;
                    }
                    case 0x13:
                    {
                        Console.WriteLine("INX\tD");
                        span = 1;
                        break;
                    }
                    case 0x14:
                    {
                        Console.WriteLine("INR\tD");
                        span = 1;
                        break;
                    }
                    case 0x15:
                    {
                        Console.WriteLine("DCR\tD");
                        span = 1;
                        break;
                    }
                    case 0x16:
                    {
                        Console.WriteLine($"MVI\tD, #${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x17:
                    {
                        Console.WriteLine("RAL");
                        span = 1;
                        break;
                    }
                    case 0x18:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0x19:
                    {
                        Console.WriteLine("DAD\tD");
                        span = 1;
                        break;
                    }
                    case 0x1a:
                    {
                        Console.WriteLine("LDAX\tD");
                        span = 1;
                        break;
                    }
                    case 0x1b:
                    {
                        Console.WriteLine("DCX\tD");
                        span = 1;
                        break;
                    }
                    case 0x1c:
                    {
                        Console.WriteLine("INR\tE");
                        span = 1;
                        break;
                    }
                    case 0x1d:
                    {
                        Console.WriteLine("DCR\tE");
                        span = 1;
                        break;
                    }
                    case 0x1e:
                    {
                        Console.WriteLine($"MVI\tE, #${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x1f:
                    {
                        Console.WriteLine("RAR");
                        span = 1;
                        break;
                    }
                    case 0x20:
                    {
                        Console.WriteLine("RIM");
                        span = 1;
                        break;
                    }
                    case 0x21:
                    {
                        Console.WriteLine($"LXI\tH, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x22:
                    {
                        Console.WriteLine($"SHLD\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x23:
                    {
                        Console.WriteLine("INX\tH");
                        span = 1;
                        break;
                    }
                    case 0x24:
                    {
                        Console.WriteLine("INR\tH");
                        span = 1;
                        break;
                    }
                    case 0x25:
                    {
                        Console.WriteLine("DCR\tH");
                        span = 1;
                        break;
                    }
                    case 0x26:
                    {
                        Console.WriteLine($"MVI\tH, #${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x27:
                    {
                        Console.WriteLine("DAA");
                        span = 1;
                        break;
                    }
                    case 0x28:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0x29:
                    {
                        Console.WriteLine("DAD\tH");
                        span = 1;
                        break;
                    }
                    case 0x2a:
                    {
                        Console.WriteLine($"LHLD\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x2b:
                    {
                        Console.WriteLine("DCX\tH");
                        span = 1;
                        break;
                    }
                    case 0x2c:
                    {
                        Console.WriteLine("INR\tL");
                        span = 1;
                        break;
                    }
                    case 0x2d:
                    {
                        Console.WriteLine("DCR\tL");
                        span = 1;
                        break;
                    }
                    case 0x2e:
                    {
                        Console.WriteLine($"MVI\tL, #${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x2f:
                    {
                        Console.WriteLine($"CMA");
                        span = 1;
                        break;
                    }
                    case 0x30:
                    {
                        Console.WriteLine("SIM");
                        span = 1;
                        break;
                    }
                    case 0x31:
                    {
                        Console.WriteLine($"LXI\tSP, #${opcode3:X}, #${opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x32:
                    {
                        Console.WriteLine($"STA\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x33:
                    {
                        Console.WriteLine("INX\tSP");
                        span = 1;
                        break;
                    }
                    case 0x34:
                    {
                        Console.WriteLine("INR\tM");
                        span = 1;
                        break;
                    }
                    case 0x35:
                    {
                        Console.WriteLine("DCR\tM");
                        span = 1;
                        break;
                    }
                    case 0x36:
                    {
                        Console.WriteLine($"MVI\tM, #${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0x37:
                    {
                        Console.WriteLine("STC");
                        span = 1;
                        break;
                    }
                    case 0x38:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0x39:
                    {
                        Console.WriteLine("DAD\tSP");
                        span = 1;
                        break;
                    }
                    case 0x3a:
                    {
                        Console.WriteLine($"LDA\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x3b:
                    {
                        Console.WriteLine("DCX\tSP");
                        span = 1;
                        break;
                    }
                    case 0x3c:
                    {
                        Console.WriteLine("INR\tA");
                        span = 1;
                        break;
                    }
                    case 0x3d:
                    {
                        Console.WriteLine("DCR\tA");
                        span = 1;
                        break;
                    }
                    case 0x3e:
                    {
                        Console.WriteLine($"MVI\tA, #${opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0x3f:
                    {
                        Console.WriteLine("CMC");
                        span = 1;
                        break;
                    }
                    case 0x40:
                    {
                        Console.WriteLine("MOV\tB, B");
                        span = 1;
                        break;
                    }
                    case 0x41:
                    {
                        Console.WriteLine("MOV\tB, C");
                        span = 1;
                        break;
                    }
                    case 0x42:
                    {
                        Console.WriteLine("MOV\tB, D");
                        span = 1;
                        break;
                    }
                    case 0x43:
                    {
                        Console.WriteLine("MOV\tB, E");
                        span = 1;
                        break;
                    }
                    case 0x44:
                    {
                        Console.WriteLine("MOV\tB, H");
                        span = 1;
                        break;
                    }
                    case 0x45:
                    {
                        Console.WriteLine("MOB\tB, L");
                        span = 1;
                        break;
                    }
                    case 0x46:
                    {
                        Console.WriteLine("MOV\tB, M");
                        span = 1;
                        break;
                    }
                    case 0x47:
                    {
                        Console.WriteLine("MOV\tB, A");
                        span = 1;
                        break;
                    }
                    case 0x48:
                    {
                        Console.WriteLine("MOV\tC, B");
                        span = 1;
                        break;
                    }
                    case 0x49:
                    {
                        Console.WriteLine("MOV\tC, C");
                        span = 1;
                        break;
                    }
                    case 0x4a:
                    {
                        Console.WriteLine("MOV\tC, D");
                        span = 1;
                        break;
                    }
                    case 0x4b:
                    {
                        Console.WriteLine("MOV\tC, E");
                        span = 1;
                        break;
                    }
                    case 0x4c:
                    {
                        Console.WriteLine("MOV\tC, H");
                        span = 1;
                        break;
                    }
                    case 0x4d:
                    {
                        Console.WriteLine("MOV\tC, L");
                        span = 1;
                        break;
                    }
                    case 0x4e:
                    {
                        Console.WriteLine("MOV\tC, M");
                        span = 1;
                        break;
                    }
                    case 0x4f:
                    {
                        Console.WriteLine("MOV\tC, A");
                        span = 1;
                        break;
                    }
                    case 0x50:
                    {
                        Console.WriteLine("MOV\tD, B");
                        span = 1;
                        break;
                    }
                    case 0x51:
                    {
                        Console.WriteLine("MOV\tD, C");
                        span = 1;
                        break;
                    }
                    case 0x52:
                    {
                        Console.WriteLine("MOV\tD, D");
                        span = 1;
                        break;
                    }
                    case 0x53:
                    {
                        Console.WriteLine("MOV\tD, E");
                        span = 1;
                        break;
                    }
                    case 0x54:
                    {
                        Console.WriteLine("MOV\tD, H");
                        span = 1;
                        break;
                    }
                    case 0x55:
                    {
                        Console.WriteLine("MOV\tD, L");
                        span = 1;
                        break;
                    }
                    case 0x56:
                    {
                        Console.WriteLine("MOV\tD, M");
                        span = 1;
                        break;
                    }
                    case 0x57:
                    {
                        Console.WriteLine("MOV\tD, A");
                        span = 1;
                        break;
                    }
                    case 0x58:
                    {
                        Console.WriteLine("MOV\tE, B");
                        span = 1;
                        break;
                    }
                    case 0x59:
                    {
                        Console.WriteLine("MOV\tE, C");
                        span = 1;
                        break;
                    }
                    case 0x5a:
                    {
                        Console.WriteLine("MOV\tE, D");
                        span = 1;
                        break;
                    }
                    case 0x5b:
                    {
                        Console.WriteLine("MOV\tE, E");
                        span = 1;
                        break;
                    }
                    case 0x5c:
                    {
                        Console.WriteLine("MOV\tE, H");
                        span = 1;
                        break;
                    }
                    case 0x5d:
                    {
                        Console.WriteLine("MOV\tE, L");
                        span = 1;
                        break;
                    }
                    case 0x5e:
                    {
                        Console.WriteLine("MOV\tE, M");
                        span = 1;
                        break;
                    }
                    case 0x5f:
                    {
                        Console.WriteLine("MOV\tE, A");
                        span = 1;
                        break;
                    }
                    case 0x60:
                    {
                        Console.WriteLine("MOV\tH, B");
                        span = 1;
                        break;
                    }
                    case 0x61:
                    {
                        Console.WriteLine("MOV\tH, C");
                        span = 1;
                        break;
                    }
                    case 0x62:
                    {
                        Console.WriteLine("MOV\tH, D");
                        span = 1;
                        break;
                    }
                    case 0x63:
                    {
                        Console.WriteLine("MOV\tH, E");
                        span = 1;
                        break;
                    }
                    case 0x64:
                    {
                        Console.WriteLine("MOV\tH, H");
                        span = 1;
                        break;
                    }
                    case 0x65:
                    {
                        Console.WriteLine("MOV\tH, L");
                        span = 1;
                        break;
                    }
                    case 0x66:
                    {
                        Console.WriteLine("MOV\tH, M");
                        span = 1;
                        break;
                    }
                    case 0x67:
                    {
                        Console.WriteLine("MOV\tH, A");
                        span = 1;
                        break;
                    }
                    case 0x68:
                    {
                        Console.WriteLine("MOV\tL, B");
                        span = 1;
                        break;
                    }
                    case 0x69:
                    {
                        Console.WriteLine("MOV\tL, C");
                        span = 1;
                        break;
                    }
                    case 0x6a:
                    {
                        Console.WriteLine("MOV\tL, D");
                        span = 1;
                        break;
                    }
                    case 0x6b:
                    {
                        Console.WriteLine("MOV\tL, E");
                        span = 1;
                        break;
                    }
                    case 0x6c:
                    {
                        Console.WriteLine("MOV\tL, H");
                        span = 1;
                        break;
                    }
                    case 0x6d:
                    {
                        Console.WriteLine("MOV\tL, L");
                        span = 1;
                        break;
                    }
                    case 0x6e:
                    {
                        Console.WriteLine("MOV\tL, M");
                        span = 1;
                        break;
                    }
                    case 0x6f:
                    {
                        Console.WriteLine("MOV\tL, A");
                        span = 1;
                        break;
                    }
                    case 0x70:
                    {
                        Console.WriteLine("MOV\tM, B");
                        span = 1;
                        break;
                    }
                    case 0x71:
                    {
                        Console.WriteLine("MOV\tM, C");
                        span = 1;
                        break;
                    }
                    case 0x72:
                    {
                        Console.WriteLine("MOV\tM, D");
                        span = 1;
                        break;
                    }
                    case 0x73:
                    {
                        Console.WriteLine("MOV\tM, E");
                        span = 1;
                        break;
                    }
                    case 0x74:
                    {
                        Console.WriteLine("MOV\tM, H");
                        span = 1;
                        break;
                    }
                    case 0x75:
                    {
                        Console.WriteLine("MOV\tM, L");
                        span = 1;
                        break;
                    }
                    case 0x76:
                    {
                        Console.WriteLine("HLT");
                        span = 1;
                        break;
                    }
                    case 0x77:
                    {
                        Console.WriteLine("MOV\tM, A");
                        span = 1;
                        break;
                    }
                    case 0x78:
                    {
                        Console.WriteLine("MOV\tA, B");
                        span = 1;
                        break;
                    }
                    case 0x79:
                    {
                        Console.WriteLine("MOV\tA, C");
                        span = 1;
                        break;
                    }
                    case 0x7a:
                    {
                        Console.WriteLine("MOV\tA, D");
                        span = 1;
                        break;
                    }
                    case 0x7b:
                    {
                        Console.WriteLine("MOV\tA, E");
                        span = 1;
                        break;
                    }
                    case 0x7c:
                    {
                        Console.WriteLine("MOV\tA, H");
                        span = 1;
                        break;
                    }
                    case 0x7d:
                    {
                        Console.WriteLine("MOV\tA, L");
                        span = 1;
                        break;
                    }
                    case 0x7e:
                    {
                        Console.WriteLine("MOV\tA, M");
                        span = 1;
                        break;
                    }
                    case 0x7f:
                    {
                        Console.WriteLine("MOV\tA, A");
                        span = 1;
                        break;
                    }
                    case 0x80:
                    {
                        Console.WriteLine("ADD\tB");
                        span = 1;
                        break;
                    }
                    case 0x81:
                    {
                        Console.WriteLine("ADD\tC");
                        span = 1;
                        break;
                    }
                    case 0x82:
                    {
                        Console.WriteLine("ADD\tD");
                        span = 1;
                        break;
                    }
                    case 0x83:
                    {
                        Console.WriteLine("ADD\tE");
                        span = 1;
                        break;
                    }
                    case 0x84:
                    {
                        Console.WriteLine("ADD\tH");
                        span = 1;
                        break;
                    }
                    case 0x85:
                    {
                        Console.WriteLine("ADD\tL");
                        span = 1;
                        break;
                    }
                    case 0x86:
                    {
                        Console.WriteLine("ADD\tM");
                        span = 1;
                        break;
                    }
                    case 0x87:
                    {
                        Console.WriteLine("ADD\tA");
                        span = 1;
                        break;
                    }
                    case 0x88:
                    {
                        Console.WriteLine("ADC\tB");
                        span = 1;
                        break;
                    }
                    case 0x89:
                    {
                        Console.WriteLine("ADC\tC");
                        span = 1;
                        break;
                    }
                    case 0x8a:
                    {
                        Console.WriteLine("ADC\tD");
                        span = 1;
                        break;
                    }
                    case 0x8b:
                    {
                        Console.WriteLine("ADC\tE");
                        span = 1;
                        break;
                    }
                    case 0x8c:
                    {
                        Console.WriteLine("ADC\tH");
                        span = 1;
                        break;
                    }
                    case 0x8d:
                    {
                        Console.WriteLine("ADC\tL");
                        span = 1;
                        break;
                    }
                    case 0x8e:
                    {
                        Console.WriteLine("ADC\tM");
                        span = 1;
                        break;
                    }
                    case 0x8f:
                    {
                        Console.WriteLine("ADC\tA");
                        span = 1;
                        break;
                    }
                    case 0x90:
                    {
                        Console.WriteLine("SUB\tB");
                        span = 1;
                        break;
                    }
                    case 0x91:
                    {
                        Console.WriteLine("SUB\tC");
                        span = 1;
                        break;
                    }
                    case 0x92:
                    {
                        Console.WriteLine("SUB\tD");
                        span = 1;
                        break;
                    }
                    case 0x93:
                    {
                        Console.WriteLine("SUB\tE");
                        span = 1;
                        break;
                    }
                    case 0x94:
                    {
                        Console.WriteLine("SUB\tH");
                        span = 1;
                        break;
                    }
                    case 0x95:
                    {
                        Console.WriteLine("SUB\tL");
                        span = 1;
                        break;
                    }
                    case 0x96:
                    {
                        Console.WriteLine("SUB\tM");
                        span = 1;
                        break;
                    }
                    case 0x97:
                    {
                        Console.WriteLine("SUB\tA");
                        span = 1;
                        break;
                    }
                    case 0x98:
                    {
                        Console.WriteLine("SBB\tB");
                        span = 1;
                        break;
                    }
                    case 0x99:
                    {
                        Console.WriteLine("SBB\tC");
                        span = 1;
                        break;
                    }
                    case 0x9a:
                    {
                        Console.WriteLine("SBB\tD");
                        span = 1;
                        break;
                    }
                    case 0x9b:
                    {
                        Console.WriteLine("SBB\tE");
                        span = 1;
                        break;
                    }
                    case 0x9c:
                    {
                        Console.WriteLine("SBB\tH");
                        span = 1;
                        break;
                    }
                    case 0x9d:
                    {
                        Console.WriteLine("SBB\tL");
                        span = 1;
                        break;
                    }
                    case 0x9e:
                    {
                        Console.WriteLine("SBB\tM");
                        span = 1;
                        break;
                    }
                    case 0x9f:
                    {
                        Console.WriteLine("SBB\tA");
                        span = 1;
                        break;
                    }
                    case 0xa0:
                    {
                        Console.WriteLine("ANA\tB");
                        span = 1;
                        break;
                    }
                    case 0xa1:
                    {
                        Console.WriteLine("ANA\tC");
                        span = 1;
                        break;
                    }
                    case 0xa2:
                    {
                        Console.WriteLine("ANA\tD");
                        span = 1;
                        break;
                    }
                    case 0xa3:
                    {
                        Console.WriteLine("ANA\tE");
                        span = 1;
                        break;
                    }
                    case 0xa4:
                    {
                        Console.WriteLine("ANA\tH");
                        span = 1;
                        break;
                    }
                    case 0xa5:
                    {
                        Console.WriteLine("ANA\tL");
                        span = 1;
                        break;
                    }
                    case 0xa6:
                    {
                        Console.WriteLine("ANA\tM");
                        span = 1;
                        break;
                    }
                    case 0xa7:
                    {
                        Console.WriteLine("ANA\tA");
                        span = 1;
                        break;
                    }
                    case 0xa8:
                    {
                        Console.WriteLine("XRA\tB");
                        span = 1;
                        break;
                    }
                    case 0xa9:
                    {
                        Console.WriteLine("XRA\tC");
                        span = 1;
                        break;
                    }
                    case 0xaa:
                    {
                        Console.WriteLine("XRA\tD");
                        span = 1;
                        break;
                    }
                    case 0xab:
                    {
                        Console.WriteLine("XRA\tE");
                        span = 1;
                        break;
                    }
                    case 0xac:
                    {
                        Console.WriteLine("XRA\tH");
                        span = 1;
                        break;
                    }
                    case 0xad:
                    {
                        Console.WriteLine("XRA\tL");
                        span = 1;
                        break;
                    }
                    case 0xae:
                    {
                        Console.WriteLine("XRA\tM");
                        span = 1;
                        break;
                    }
                    case 0xaf:
                    {
                        Console.WriteLine("XRA\tA");
                        span = 1;
                        break;
                    }
                    case 0xb0:
                    {
                        Console.WriteLine("ORA\tB");
                        span = 1;
                        break;
                    }
                    case 0xb1:
                    {
                        Console.WriteLine("ORA\tC");
                        span = 1;
                        break;
                    }
                    case 0xb2:
                    {
                        Console.WriteLine("ORA\tD");
                        span = 1;
                        break;
                    }
                    case 0xb3:
                    {
                        Console.WriteLine("ORA\tE");
                        span = 1;
                        break;
                    }
                    case 0xb4:
                    {
                        Console.WriteLine("ORA\tH");
                        span = 1;
                        break;
                    }
                    case 0xb5:
                    {
                        Console.WriteLine("ORA\tL");
                        span = 1;
                        break;
                    }
                    case 0xb6:
                    {
                        Console.WriteLine("ORA\tM");
                        span = 1;
                        break;
                    }
                    case 0xb7:
                    {
                        Console.WriteLine("ORA\tA");
                        span = 1;
                        break;
                    }
                    case 0xb8:
                    {
                        Console.WriteLine("CMP\tB");
                        span = 1;
                        break;
                    }
                    case 0xb9:
                    {
                        Console.WriteLine("CMP\tC");
                        span = 1;
                        break;
                    }
                    case 0xba:
                    {
                        Console.WriteLine("CMP\tD");
                        span = 1;
                        break;
                    }
                    case 0xbb:
                    {
                        Console.WriteLine("CMP\tE");
                        span = 1;
                        break;
                    }
                    case 0xbc:
                    {
                        Console.WriteLine("CMP\tH");
                        span = 1;
                        break;
                    }
                    case 0xbd:
                    {
                        Console.WriteLine("CMP\tL");
                        span = 1;
                        break;
                    }
                    case 0xbe:
                    {
                        Console.WriteLine("CMP\tM");
                        span = 1;
                        break;
                    }
                    case 0xbf:
                    {
                        Console.WriteLine("CMP\tA");
                        span = 1;
                        break;
                    }
                    case 0xc0:
                    {
                        Console.WriteLine("RNZ");
                        span = 1;
                        break;
                    }
                    case 0xc1:
                    {
                        Console.WriteLine("POP\tB");
                        span = 1;
                        break;
                    }
                    case 0xc2:
                    {
                        Console.WriteLine($"JNZ\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xc3:
                    {
                        Console.WriteLine($"JMP\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xc4:
                    {
                        Console.WriteLine($"CNZ\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xc5:
                    {
                        Console.WriteLine("PUSH\tB");
                        span = 1;
                        break;
                    }
                    case 0xc6:
                    {
                        Console.WriteLine($"ADI\t#${opcode2:X}");
                        span = 2;
                        break;
                    }
                    case 0xc7:
                    {
                        Console.WriteLine("RST\t0");
                        span = 1;
                        break;
                    }
                    case 0xc8:
                    {
                        Console.WriteLine("RZ");
                        span = 1;
                        break;
                    }
                    case 0xc9:
                    {
                        Console.WriteLine("RET");
                        span = 1;
                        break;
                    }
                    case 0xca:
                    {
                        Console.WriteLine($"JZ\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xcb:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0xcc:
                    {
                        Console.WriteLine($"CZ\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xcd:
                    {
                        Console.WriteLine($"CALL\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xce:
                    {
                        Console.WriteLine("ACI");
                        span = 2;
                        break;
                    }
                    case 0xcf:
                    {
                        Console.WriteLine("RST");
                        span = 1;
                        break;
                    }
                    case 0xd0:
                    {
                        Console.WriteLine("RNC");
                        span = 1;
                        break;
                    }
                    case 0xd1:
                    {
                        Console.WriteLine("POP\tD");
                        span = 1;
                        break;
                    }
                    case 0xd2:
                    {
                        Console.WriteLine($"JNC\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xd3:
                    {
                        Console.WriteLine("OUT\tD3");
                        span = 1;
                        break;
                    }
                    case 0xd4:
                    {
                        Console.WriteLine($"CNC\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xd5:
                    {
                        Console.WriteLine("PUSH\tD");
                        span = 1;
                        break;
                    }
                    case 0xd6:
                    {
                        Console.WriteLine("SUI\tD");
                        span = 2;
                        break;
                    }
                    case 0xd7:
                    {
                        Console.WriteLine("RST\t2");
                        span = 1;
                        break;
                    }
                    case 0xd8:
                    {
                        Console.WriteLine("RC");
                        span = 1;
                        break;
                    }
                    case 0xd9:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0xda:
                    {
                        Console.WriteLine($"JC\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xdb:
                    {
                        Console.WriteLine("IN\tD8");
                        span = 2;
                        break;
                    }
                    case 0xdc:
                    {
                        Console.WriteLine($"CC\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xdd:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0xde:
                    {
                        Console.WriteLine("SBI\tD8");
                        span = 2;
                        break;
                    }
                    case 0xdf:
                    {
                        Console.WriteLine("RST\t3");
                        span = 1;
                        break;
                    }
                    case 0xe0:
                    {
                        Console.WriteLine("RPO");
                        span = 1;
                        break;
                    }
                    case 0xe1:
                    {
                        Console.WriteLine("POP\tH");
                        span = 1;
                        break;
                    }
                    case 0xe2:
                    {
                        Console.WriteLine($"JPO\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xe3:
                    {
                        Console.WriteLine("XTHL");
                        span = 1;
                        break;
                    }
                    case 0xe4:
                    {
                        Console.WriteLine($"CPO\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xe5:
                    {
                        Console.WriteLine("PUSH\tH");
                        span = 1;
                        break;
                    }
                    case 0xe6:
                    {
                        Console.WriteLine("ANI\tD8");
                        span = 2;
                        break;
                    }
                    case 0xe7:
                    {
                        Console.WriteLine("RST\t4");
                        span = 1;
                        break;
                    }
                    case 0xe8:
                    {
                        Console.WriteLine("RPE");
                        span = 1;
                        break;
                    }
                    case 0xe9:
                    {
                        Console.WriteLine("PCHL");
                        span = 1;
                        break;
                    }
                    case 0xea:
                    {
                        Console.WriteLine($"JPE\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xeb:
                    {
                        Console.WriteLine("XCHG");
                        span = 1;
                        break;
                    }
                    case 0xec:
                    {
                        Console.WriteLine($"CPE\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xed:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0xee:
                    {
                        Console.WriteLine("XRI\tD8");
                        span = 2;
                        break;
                    }
                    case 0xef:
                    {
                        Console.WriteLine("RST\t5");
                        span = 1;
                        break;
                    }
                    case 0xf0:
                    {
                        Console.WriteLine("RP");
                        span = 1;
                        break;
                    }
                    case 0xf1:
                    {
                        Console.WriteLine("POP\tPSW");
                        span = 1;
                        break;
                    }
                    case 0xf2:
                    {
                        Console.WriteLine($"JP\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xf3:
                    {
                        Console.WriteLine("DI");
                        span = 1;
                        break;
                    }
                    case 0xf4:
                    {
                        Console.WriteLine($"CP\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xf5:
                    {
                        Console.WriteLine("PUSH\tPSW");
                        span = 1;
                        break;
                    }
                    case 0xf6:
                    {
                        Console.WriteLine("ORI\tD8");
                        span = 2;
                        break;
                    }
                    case 0xf7:
                    {
                        Console.WriteLine("RST\t6");
                        span = 1;
                        break;
                    }
                    case 0xf8:
                    {
                        Console.WriteLine("RM");
                        span = 1;
                        break;
                    }
                    case 0xf9:
                    {
                        Console.WriteLine("SPHL");
                        span = 1;
                        break;
                    }
                    case 0xfa:
                    {
                        Console.WriteLine($"JM\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xfb:
                    {
                        Console.WriteLine("EI");
                        span = 1;
                        break;
                    }
                    case 0xfc:
                    {
                        Console.WriteLine($"CM\t0x{opcode2:X}");
                        span = 3;
                        break;
                    }
                    case 0xfd:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
                    case 0xfe:
                    {
                        Console.WriteLine("CPI\tD8");
                        span = 2;
                        break;
                    }
                    case 0xff:
                    {
                        Console.WriteLine("RST\t7");
                        span = 2;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("-");
                        span = 1;
                        break;
                    }
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