// This class will read a programs hex and print out formatted 8080 assembly
// langauge.


using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Chip8WorkingCopy
{
    public class Disassembler
    {
        
        public List<string> Disassemble8080ToList(string fileToRead)
        {     
            
            // Read bytes of 8080 binary to buffer
            var byteArray = File
                .ReadAllBytes(fileToRead)
                .ToArray();
            
            // Span specifies the amount of instructions to read ahead
            var i = 0;
            
            // List of parsed instructions to be returned from function
            var parsedList = new List<string>();
            
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
                    b1 = byteArray[i];
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
                
                // Create variable that stores generated code
                var generatedCode = String.Format("0x{0:x3} ", i + 1);
                
                switch (opcode)
                {
                    case 0x00:
                    {
                        generatedCode += "NOP";
                        span = 1;
                        break;
                    }
                    case 0x01:
                    {
                        generatedCode += $"LXI\tB, #${opcode3:X}, #${opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x02:
                    {
                        generatedCode += "STAX\tB";
                        span = 1;
                        break;
                    }
                    case 0x03:
                    {
                        generatedCode += "INX\tB";
                        span = 3;
                        break;
                    }
                    case 0x04:
                    {
                        generatedCode += "INR\tB";
                        span = 1;
                        break;
                    }
                    case 0x05:
                    {
                        generatedCode += "DCR\tB";
                        span = 1;
                        break;
                    }
                    case 0x06:
                    {
                        generatedCode += $"MVI\tB, #${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x07:
                    {
                        generatedCode += "RLC";
                        span = 1;
                        break;
                    }
                    case 0x08:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0x09:
                    {
                        generatedCode += "DAD\tB";
                        span = 1;
                        break;
                    }
                    case 0x0a:
                    {
                        generatedCode += "LDAX\tB";
                        span = 1;
                        break;
                    }
                    case 0x0b:
                    {
                        generatedCode += "DCX\tB";
                        span = 1;
                        break;
                    }
                    case 0x0c:
                    {
                        generatedCode += "INR\tC";
                        span = 1;
                        break;
                    }
                    case 0x0d:
                    {
                        generatedCode += "DCR\tC";
                        span = 1;
                        break;
                    }
                    case 0x0e:
                    {
                        generatedCode += $"MVI\tC, {opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x0f:
                    {
                        generatedCode += "RRC";
                        span = 1;
                        break;
                    }
                    case 0x10:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0x11:
                    {
                        generatedCode += $"LXI\tD, #${opcode3:X}, #${opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x12:
                    {
                        generatedCode += "STAX\tD";
                        span = 1;
                        break;
                    }
                    case 0x13:
                    {
                        generatedCode += "INX\tD";
                        span = 1;
                        break;
                    }
                    case 0x14:
                    {
                        generatedCode += "INR\tD";
                        span = 1;
                        break;
                    }
                    case 0x15:
                    {
                        generatedCode += "DCR\tD";
                        span = 1;
                        break;
                    }
                    case 0x16:
                    {
                        generatedCode += $"MVI\tD, #${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x17:
                    {
                        generatedCode += "RAL";
                        span = 1;
                        break;
                    }
                    case 0x18:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0x19:
                    {
                        generatedCode += "DAD\tD";
                        span = 1;
                        break;
                    }
                    case 0x1a:
                    {
                        generatedCode += "LDAX\tD";
                        span = 1;
                        break;
                    }
                    case 0x1b:
                    {
                        generatedCode += "DCX\tD";
                        span = 1;
                        break;
                    }
                    case 0x1c:
                    {
                        generatedCode += "INR\tE";
                        span = 1;
                        break;
                    }
                    case 0x1d:
                    {
                        generatedCode += "DCR\tE";
                        span = 1;
                        break;
                    }
                    case 0x1e:
                    {
                        generatedCode += $"MVI\tE, #${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x1f:
                    {
                        generatedCode += "RAR";
                        span = 1;
                        break;
                    }
                    case 0x20:
                    {
                        generatedCode += "RIM";
                        span = 1;
                        break;
                    }
                    case 0x21:
                    {
                        generatedCode += $"LXI\tH, #${opcode3:X}, #${opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x22:
                    {
                        generatedCode += $"SHLD\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x23:
                    {
                        generatedCode += "INX\tH";
                        span = 1;
                        break;
                    }
                    case 0x24:
                    {
                        generatedCode += "INR\tH";
                        span = 1;
                        break;
                    }
                    case 0x25:
                    {
                        generatedCode += "DCR\tH";
                        span = 1;
                        break;
                    }
                    case 0x26:
                    {
                        generatedCode += $"MVI\tH, #${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x27:
                    {
                        generatedCode += "DAA";
                        span = 1;
                        break;
                    }
                    case 0x28:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0x29:
                    {
                        generatedCode += "DAD\tH";
                        span = 1;
                        break;
                    }
                    case 0x2a:
                    {
                        generatedCode += $"LHLD\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x2b:
                    {
                        generatedCode += "DCX\tH";
                        span = 1;
                        break;
                    }
                    case 0x2c:
                    {
                        generatedCode += "INR\tL";
                        span = 1;
                        break;
                    }
                    case 0x2d:
                    {
                        generatedCode += "DCR\tL";
                        span = 1;
                        break;
                    }
                    case 0x2e:
                    {
                        generatedCode += $"MVI\tL, #${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x2f:
                    {
                        generatedCode += "CMA";
                        span = 1;
                        break;
                    }
                    case 0x30:
                    {
                        generatedCode += "SIM";
                        span = 1;
                        break;
                    }
                    case 0x31:
                    {
                        generatedCode += $"LXI\tSP, #${opcode3:X}, #${opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x32:
                    {
                        generatedCode += $"STA\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x33:
                    {
                        generatedCode += "INX\tSP";
                        span = 1;
                        break;
                    }
                    case 0x34:
                    {
                        generatedCode += "INR\tM";
                        span = 1;
                        break;
                    }
                    case 0x35:
                    {
                        generatedCode += "DCR\tM";
                        span = 1;
                        break;
                    }
                    case 0x36:
                    {
                        generatedCode += $"MVI\tM, #${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0x37:
                    {
                        generatedCode += "STC";
                        span = 1;
                        break;
                    }
                    case 0x38:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0x39:
                    {
                        generatedCode += "DAD\tSP";
                        span = 1;
                        break;
                    }
                    case 0x3a:
                    {
                        generatedCode += $"LDA\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x3b:
                    {
                        generatedCode += "DCX\tSP";
                        span = 1;
                        break;
                    }
                    case 0x3c:
                    {
                        generatedCode += "INR\tA";
                        span = 1;
                        break;
                    }
                    case 0x3d:
                    {
                        generatedCode += "DCR\tA";
                        span = 1;
                        break;
                    }
                    case 0x3e:
                    {
                        generatedCode += $"MVI\tA, #${opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0x3f:
                    {
                        generatedCode += "CMC";
                        span = 1;
                        break;
                    }
                    case 0x40:
                    {
                        generatedCode += "MOV\tB, B";
                        span = 1;
                        break;
                    }
                    case 0x41:
                    {
                        generatedCode += "MOV\tB, C";
                        span = 1;
                        break;
                    }
                    case 0x42:
                    {
                        generatedCode += "MOV\tB, D";
                        span = 1;
                        break;
                    }
                    case 0x43:
                    {
                        generatedCode += "MOV\tB, E";
                        span = 1;
                        break;
                    }
                    case 0x44:
                    {
                        generatedCode += "MOV\tB, H"; 
                        span = 1;
                        break;
                    }
                    case 0x45:
                    {
                        generatedCode += "MOB\tB, L";
                        span = 1;
                        break;
                    }
                    case 0x46:
                    {
                        generatedCode += "MOV\tB, M";
                        span = 1;
                        break;
                    }
                    case 0x47:
                    {
                        generatedCode += "MOV\tB, A";
                        span = 1;
                        break;
                    }
                    case 0x48:
                    {
                        generatedCode += "MOV\tC, B";
                        span = 1;
                        break;
                    }
                    case 0x49:
                    {
                        generatedCode += "MOV\tC, C";
                        span = 1;
                        break;
                    }
                    case 0x4a:
                    {
                        generatedCode += "MOV\tC, D";
                        span = 1;
                        break;
                    }
                    case 0x4b:
                    {
                        generatedCode += "MOV\tC, E";
                        span = 1;
                        break;
                    }
                    case 0x4c:
                    {
                        generatedCode += "MOV\tC, H";
                        span = 1;
                        break;
                    }
                    case 0x4d:
                    {
                        generatedCode += "MOV\tC, L";
                        span = 1;
                        break;
                    }
                    case 0x4e:
                    {
                        generatedCode += "MOV\tC, M";
                        span = 1;
                        break;
                    }
                    case 0x4f:
                    {
                        generatedCode += "MOV\tC, A";
                        span = 1;
                        break;
                    }
                    case 0x50:
                    {
                        generatedCode += "MOV\tD, B";
                        span = 1;
                        break;
                    }
                    case 0x51:
                    {
                        generatedCode += "MOV\tD, C";
                        span = 1;
                        break;
                    }
                    case 0x52:
                    {
                        generatedCode += "MOV\tD, D";
                        span = 1;
                        break;
                    }
                    case 0x53:
                    {
                        generatedCode += "MOV\tD, E";
                        span = 1;
                        break;
                    }
                    case 0x54:
                    {
                        generatedCode += "MOV\tD, H";
                        span = 1;
                        break;
                    }
                    case 0x55:
                    {
                        generatedCode += "MOV\tD, L";
                        span = 1;
                        break;
                    }
                    case 0x56:
                    {
                        generatedCode += "MOV\tD, M";
                        span = 1;
                        break;
                    }
                    case 0x57:
                    {
                        generatedCode += "MOV\tD, A";
                        span = 1;
                        break;
                    }
                    case 0x58:
                    {
                        generatedCode += "MOV\tE, B";
                        span = 1;
                        break;
                    }
                    case 0x59:
                    {
                        generatedCode += "MOV\tE, C";
                        span = 1;
                        break;
                    }
                    case 0x5a:
                    {
                        generatedCode += "MOV\tE, D";
                        span = 1;
                        break;
                    }
                    case 0x5b:
                    {
                        generatedCode += "MOV\tE, E";
                        span = 1;
                        break;
                    }
                    case 0x5c:
                    {
                        generatedCode += "MOV\tE, H";
                        span = 1;
                        break;
                    }
                    case 0x5d:
                    {
                        generatedCode += "MOV\tE, L";
                        span = 1;
                        break;
                    }
                    case 0x5e:
                    {
                        generatedCode += "MOV\tE, M";
                        span = 1;
                        break;
                    }
                    case 0x5f:
                    {
                        generatedCode += "MOV\tE, A";
                        span = 1;
                        break;
                    }
                    case 0x60:
                    {
                        generatedCode += "MOV\tH, B";
                        span = 1;
                        break;
                    }
                    case 0x61:
                    {
                        generatedCode += "MOV\tH, C";
                        span = 1;
                        break;
                    }
                    case 0x62:
                    {
                        generatedCode += "MOV\tH, D";
                        span = 1;
                        break;
                    }
                    case 0x63:
                    {
                        generatedCode += "MOV\tH, E";
                        span = 1;
                        break;
                    }
                    case 0x64:
                    {
                        generatedCode += "MOV\tH, H";
                        span = 1;
                        break;
                    }
                    case 0x65:
                    {
                        generatedCode += "MOV\tH, L";
                        span = 1;
                        break;
                    }
                    case 0x66:
                    {
                        generatedCode += "MOV\tH, M";
                        span = 1;
                        break;
                    }
                    case 0x67:
                    {
                        generatedCode += "MOV\tH, A";
                        span = 1;
                        break;
                    }
                    case 0x68:
                    {
                        generatedCode += "MOV\tL, B";
                        span = 1;
                        break;
                    }
                    case 0x69:
                    {
                        generatedCode += "MOV\tL, C";
                        span = 1;
                        break;
                    }
                    case 0x6a:
                    {
                        generatedCode += "MOV\tL, D";
                        span = 1;
                        break;
                    }
                    case 0x6b:
                    {
                        generatedCode += "MOV\tL, E";
                        span = 1;
                        break;
                    }
                    case 0x6c:
                    {
                        generatedCode += "MOV\tL, H";
                        span = 1;
                        break;
                    }
                    case 0x6d:
                    {
                        generatedCode += "MOV\tL, L";
                        span = 1;
                        break;
                    }
                    case 0x6e:
                    {
                        generatedCode += "MOV\tL, M";
                        span = 1;
                        break;
                    }
                    case 0x6f:
                    {
                        generatedCode += "MOV\tL, A";
                        span = 1;
                        break;
                    }
                    case 0x70:
                    {
                        generatedCode += "MOV\tM, B";
                        span = 1;
                        break;
                    }
                    case 0x71:
                    {
                        generatedCode += "MOV\tM, C";
                        span = 1;
                        break;
                    }
                    case 0x72:
                    {
                        generatedCode += "MOV\tM, D";
                        span = 1;
                        break;
                    }
                    case 0x73:
                    {
                        generatedCode += "MOV\tM, E";
                        span = 1;
                        break;
                    }
                    case 0x74:
                    {
                        generatedCode += "MOV\tM, H";
                        span = 1;
                        break;
                    }
                    case 0x75:
                    {
                        generatedCode += "MOV\tM, L";
                        span = 1;
                        break;
                    }
                    case 0x76:
                    {
                        generatedCode += "HLT";
                        span = 1;
                        break;
                    }
                    case 0x77:
                    {
                        generatedCode += "MOV\tM, A";
                        span = 1;
                        break;
                    }
                    case 0x78:
                    {
                        generatedCode += "MOV\tA, B";
                        span = 1;
                        break;
                    }
                    case 0x79:
                    {
                        generatedCode += "MOV\tA, C";
                        span = 1;
                        break;
                    }
                    case 0x7a:
                    {
                        generatedCode += "MOV\tA, D";
                        span = 1;
                        break;
                    }
                    case 0x7b:
                    {
                        generatedCode += "MOV\tA, E";
                        span = 1;
                        break;
                    }
                    case 0x7c:
                    {
                        generatedCode += "MOV\tA, H";
                        span = 1;
                        break;
                    }
                    case 0x7d:
                    {
                        generatedCode += "MOV\tA, L";
                        span = 1;
                        break;
                    }
                    case 0x7e:
                    {
                        generatedCode += "MOV\tA, M";
                        span = 1;
                        break;
                    }
                    case 0x7f:
                    {
                        generatedCode += "MOV\tA, A";
                        span = 1;
                        break;
                    }
                    case 0x80:
                    {
                        generatedCode += "ADD\tB";
                        span = 1;
                        break;
                    }
                    case 0x81:
                    {
                        generatedCode += "ADD\tC";
                        span = 1;
                        break;
                    }
                    case 0x82:
                    {
                        generatedCode += "ADD\tD";
                        span = 1;
                        break;
                    }
                    case 0x83:
                    {
                        generatedCode += "ADD\tE";
                        span = 1;
                        break;
                    }
                    case 0x84:
                    {
                        generatedCode += "ADD\tH";
                        span = 1;
                        break;
                    }
                    case 0x85:
                    {
                        generatedCode += "ADD\tL";
                        span = 1;
                        break;
                    }
                    case 0x86:
                    {
                        generatedCode += "ADD\tM";
                        span = 1;
                        break;
                    }
                    case 0x87:
                    {
                        generatedCode += "ADD\tA";
                        span = 1;
                        break;
                    }
                    case 0x88:
                    {
                        generatedCode += "ADC\tB";
                        span = 1;
                        break;
                    }
                    case 0x89:
                    {
                        generatedCode += "ADC\tC";
                        span = 1;
                        break;
                    }
                    case 0x8a:
                    {
                        generatedCode += "ADC\tD";
                        span = 1;
                        break;
                    }
                    case 0x8b:
                    {
                        generatedCode += "ADC\tE";
                        span = 1;
                        break;
                    }
                    case 0x8c:
                    {
                        generatedCode += "ADC\tH";
                        span = 1;
                        break;
                    }
                    case 0x8d:
                    {
                        generatedCode += "ADC\tL";
                        span = 1;
                        break;
                    }
                    case 0x8e:
                    {
                        generatedCode += "ADC\tM";
                        span = 1;
                        break;
                    }
                    case 0x8f:
                    {
                        generatedCode += "ADC\tA";
                        span = 1;
                        break;
                    }
                    case 0x90:
                    {
                        generatedCode += "SUB\tB";
                        span = 1;
                        break;
                    }
                    case 0x91:
                    {
                        generatedCode += "SUB\tC";
                        span = 1;
                        break;
                    }
                    case 0x92:
                    {
                        generatedCode += "SUB\tD";
                        span = 1;
                        break;
                    }
                    case 0x93:
                    {
                        generatedCode += "SUB\tE";
                        span = 1;
                        break;
                    }
                    case 0x94:
                    {
                        generatedCode += "SUB\tH";
                        span = 1;
                        break;
                    }
                    case 0x95:
                    {
                        generatedCode += "SUB\tL";
                        span = 1;
                        break;
                    }
                    case 0x96:
                    {
                        generatedCode += "SUB\tM";
                        span = 1;
                        break;
                    }
                    case 0x97:
                    {
                        generatedCode += "SUB\tA";
                        span = 1;
                        break;
                    }
                    case 0x98:
                    {
                        generatedCode += "SBB\tB";
                        span = 1;
                        break;
                    }
                    case 0x99:
                    {
                        generatedCode += "SBB\tC";
                        span = 1;
                        break;
                    }
                    case 0x9a:
                    {
                        generatedCode += "SBB\tD";
                        span = 1;
                        break;
                    }
                    case 0x9b:
                    {
                        generatedCode += "SBB\tE";
                        span = 1;
                        break;
                    }
                    case 0x9c:
                    {
                        generatedCode += "SBB\tH";
                        span = 1;
                        break;
                    }
                    case 0x9d:
                    {
                        generatedCode += "SBB\tL";
                        span = 1;
                        break;
                    }
                    case 0x9e:
                    {
                        generatedCode += "SBB\tM";
                        span = 1;
                        break;
                    }
                    case 0x9f:
                    {
                        generatedCode += "SBB\tA";
                        span = 1;
                        break;
                    }
                    case 0xa0:
                    {
                        generatedCode += "ANA\tB"; 
                        span = 1;
                        break;
                    }
                    case 0xa1:
                    {
                        generatedCode += "ANA\tC";
                        span = 1;
                        break;
                    }
                    case 0xa2:
                    {
                        generatedCode += "ANA\tD";
                        span = 1;
                        break;
                    }
                    case 0xa3:
                    {
                        generatedCode += "ANA\tE";
                        span = 1;
                        break;
                    }
                    case 0xa4:
                    {
                        generatedCode += "ANA\tH";
                        span = 1;
                        break;
                    }
                    case 0xa5:
                    {
                        generatedCode += "ANA\tL";
                        span = 1;
                        break;
                    }
                    case 0xa6:
                    {
                        generatedCode += "ANA\tM";
                        span = 1;
                        break;
                    }
                    case 0xa7:
                    {
                        generatedCode += "ANA\tA";
                        span = 1;
                        break;
                    }
                    case 0xa8:
                    {
                        generatedCode += "XRA\tB";
                        span = 1;
                        break;
                    }
                    case 0xa9:
                    {
                        generatedCode += "XRA\tC";
                        span = 1;
                        break;
                    }
                    case 0xaa:
                    {
                        generatedCode += "XRA\tD";
                        span = 1;
                        break;
                    }
                    case 0xab:
                    {
                        generatedCode += "XRA\tE";
                        span = 1;
                        break;
                    }
                    case 0xac:
                    {
                        generatedCode += "XRA\tH";
                        span = 1;
                        break;
                    }
                    case 0xad:
                    {
                        generatedCode += "XRA\tL";
                        span = 1;
                        break;
                    }
                    case 0xae:
                    {
                        generatedCode += "XRA\tM";
                        span = 1;
                        break;
                    }
                    case 0xaf:
                    {
                        generatedCode += "XRA\tA";
                        span = 1;
                        break;
                    }
                    case 0xb0:
                    {
                        generatedCode += "ORA\tB";
                        span = 1;
                        break;
                    }
                    case 0xb1:
                    {
                        generatedCode += "ORA\tC";
                        span = 1;
                        break;
                    }
                    case 0xb2:
                    {
                        generatedCode += "ORA\tD";
                        span = 1;
                        break;
                    }
                    case 0xb3:
                    {
                        generatedCode += "ORA\tE";
                        span = 1;
                        break;
                    }
                    case 0xb4:
                    {
                        generatedCode += "ORA\tH";
                        span = 1;
                        break;
                    }
                    case 0xb5:
                    {
                        generatedCode += "ORA\tL";
                        span = 1;
                        break;
                    }
                    case 0xb6:
                    {
                        generatedCode += "ORA\tM";
                        span = 1;
                        break;
                    }
                    case 0xb7:
                    {
                        generatedCode += "ORA\tA";
                        span = 1;
                        break;
                    }
                    case 0xb8:
                    {
                        generatedCode += "CMP\tB";
                        span = 1;
                        break;
                    }
                    case 0xb9:
                    {
                        generatedCode += "CMP\tC";
                        span = 1;
                        break;
                    }
                    case 0xba:
                    {
                        generatedCode += "CMP\tD";
                        span = 1;
                        break;
                    }
                    case 0xbb:
                    {
                        generatedCode += "CMP\tE";
                        span = 1;
                        break;
                    }
                    case 0xbc:
                    {
                        generatedCode += "CMP\tH";
                        span = 1;
                        break;
                    }
                    case 0xbd:
                    {
                        generatedCode += "CMP\tL";
                        span = 1;
                        break;
                    }
                    case 0xbe:
                    {
                        generatedCode += "CMP\tM";
                        span = 1;
                        break;
                    }
                    case 0xbf:
                    {
                        generatedCode += "CMP\tA";
                        span = 1;
                        break;
                    }
                    case 0xc0:
                    {
                        generatedCode += "RNZ";
                        span = 1;
                        break;
                    }
                    case 0xc1:
                    {
                        generatedCode += "POP\tB";
                        span = 1;
                        break;
                    }
                    case 0xc2:
                    {
                        generatedCode += $"JNZ\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xc3:
                    {
                        generatedCode += $"JMP\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xc4:
                    {
                        generatedCode += $"CNZ\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xc5:
                    {
                        generatedCode += "PUSH\tB";
                        span = 1;
                        break;
                    }
                    case 0xc6:
                    {
                        generatedCode += $"ADI\t#${opcode2:X}";
                        span = 2;
                        break;
                    }
                    case 0xc7:
                    {
                        generatedCode += "RST\t0";
                        span = 1;
                        break;
                    }
                    case 0xc8:
                    {
                        generatedCode += "RZ";
                        span = 1;
                        break;
                    }
                    case 0xc9:
                    {
                        generatedCode += "RET";
                        span = 1;
                        break;
                    }
                    case 0xca:
                    {
                        generatedCode += $"JZ\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xcb:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0xcc:
                    {
                        generatedCode += $"CZ\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xcd:
                    {
                        generatedCode += $"CALL\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xce:
                    {
                        generatedCode += "ACI";
                        span = 2;
                        break;
                    }
                    case 0xcf:
                    {
                        generatedCode += "RST";
                        span = 1;
                        break;
                    }
                    case 0xd0:
                    {
                        generatedCode += "RNC";
                        span = 1;
                        break;
                    }
                    case 0xd1:
                    {
                        generatedCode += "POP\tD";
                        span = 1;
                        break;
                    }
                    case 0xd2:
                    {
                        generatedCode += $"JNC\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xd3:
                    {
                        generatedCode += "OUT\tD3";
                        span = 1;
                        break;
                    }
                    case 0xd4:
                    {
                        generatedCode += $"CNC\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xd5:
                    {
                        generatedCode += "PUSH\tD";
                        span = 1;
                        break;
                    }
                    case 0xd6:
                    {
                        generatedCode += "SUI\tD";
                        span = 2;
                        break;
                    }
                    case 0xd7:
                    {
                        generatedCode += "RST\t2";
                        span = 1;
                        break;
                    }
                    case 0xd8:
                    {
                        generatedCode += "RC";
                        span = 1;
                        break;
                    }
                    case 0xd9:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0xda:
                    {
                        generatedCode += $"JC\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xdb:
                    {
                        generatedCode += "IN\tD8";
                        span = 2;
                        break;
                    }
                    case 0xdc:
                    {
                        generatedCode += $"CC\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xdd:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0xde:
                    {
                        generatedCode += "SBI\tD8";
                        span = 2;
                        break;
                    }
                    case 0xdf:
                    {
                        generatedCode += "RST\t3";
                        span = 1;
                        break;
                    }
                    case 0xe0:
                    {
                        generatedCode += "RPO";
                        span = 1;
                        break;
                    }
                    case 0xe1:
                    {
                        generatedCode += "POP\tH";
                        span = 1;
                        break;
                    }
                    case 0xe2:
                    {
                        generatedCode += $"JPO\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xe3:
                    {
                        generatedCode += "XTHL";
                        span = 1;
                        break;
                    }
                    case 0xe4:
                    {
                        generatedCode += $"CPO\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xe5:
                    {
                        generatedCode += "PUSH\tH";
                        span = 1;
                        break;
                    }
                    case 0xe6:
                    {
                        generatedCode += "ANI\tD8";
                        span = 2;
                        break;
                    }
                    case 0xe7:
                    {
                        generatedCode += "RST\t4";
                        span = 1;
                        break;
                    }
                    case 0xe8:
                    {
                        generatedCode += "RPE"; 
                        span = 1;
                        break;
                    }
                    case 0xe9:
                    {
                        generatedCode += "PCHL";
                        span = 1;
                        break;
                    }
                    case 0xea:
                    {
                        generatedCode += $"JPE\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xeb:
                    {
                        generatedCode += "XCHG";
                        span = 1;
                        break;
                    }
                    case 0xec:
                    {
                        generatedCode += $"CPE\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xed:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0xee:
                    {
                        generatedCode += "XRI\tD8";
                        span = 2;
                        break;
                    }
                    case 0xef:
                    {
                        generatedCode += "RST\t5";
                        span = 1;
                        break;
                    }
                    case 0xf0:
                    {
                        generatedCode += "RP";
                        span = 1;
                        break;
                    }
                    case 0xf1:
                    {
                        generatedCode += "POP\tPSW";
                        span = 1;
                        break;
                    }
                    case 0xf2:
                    {
                        generatedCode += $"JP\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xf3:
                    {
                        generatedCode += "DI";
                        span = 1;
                        break;
                    }
                    case 0xf4:
                    {
                        generatedCode += $"CP\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xf5:
                    {
                        generatedCode += "PUSH\tPSW";
                        span = 1;
                        break;
                    }
                    case 0xf6:
                    {
                        generatedCode += "ORI\tD8";
                        span = 2;
                        break;
                    }
                    case 0xf7:
                    {
                        generatedCode += "RST\t6";
                        span = 1;
                        break;
                    }
                    case 0xf8:
                    {
                        generatedCode += "RM";
                        span = 1;
                        break;
                    }
                    case 0xf9:
                    {
                        generatedCode += "SPHL";
                        span = 1;
                        break;
                    }
                    case 0xfa:
                    {
                        generatedCode += $"JM\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xfb:
                    {
                        generatedCode += "EI";
                        span = 1;
                        break;
                    }
                    case 0xfc:
                    {
                        generatedCode += $"CM\t0x{opcode2:X}";
                        span = 3;
                        break;
                    }
                    case 0xfd:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                    case 0xfe:
                    {
                        generatedCode += "CPI\tD8";
                        span = 2;
                        break;
                    }
                    case 0xff:
                    {
                        generatedCode += "RST\t7";
                        span = 2;
                        break;
                    }
                    default:
                    {
                        generatedCode += "-";
                        span = 1;
                        break;
                    }
                }

                parsedList.Add(generatedCode);
                
                i += 2 * span;
            }

            return parsedList;
        }

        private ushort CombineBytes(byte b1, byte b2)
        {
            ushort result = (ushort) ((b1 << 8) | b2);
            return result;
        }
    }
}