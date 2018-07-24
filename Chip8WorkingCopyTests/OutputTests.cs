using Xunit;

using Chip8WorkingCopy;

namespace Chip8WorkingCopyTests
{
    public class OutputTests
    {
        [Fact]
        public void BrixTest1()
        {
            // Setup
            var disassembler = new Disassembler();
            
            // Test
            var result = disassembler.Disassemble8080ToList(
                "/Users/garymoore/RiderProjects/Chip8WorkingCopy/Chip8WorkingCopyTests/BinariesForTesting/BRIX");
            
            // Assert
            Assert.Contains("DCR\tB", result[1]);
        }

        [Fact]
        public void BrixTest2()
        {
            // Setup
            var disassembler = new Disassembler();
            
            // Test
            var result = disassembler.Disassemble8080ToList(
                "/Users/garymoore/RiderProjects/Chip8WorkingCopy/Chip8WorkingCopyTests/BinariesForTesting/BRIX");
            
            // Assert
            Assert.Contains("JC\t0xB1", result[9]);
        }

        [Fact]
        public void BrixTest3()
        {
            // Setup
            var disassembler = new Disassembler();
            
            // Test
            var result = disassembler.Disassemble8080ToList(
                "/Users/garymoore/RiderProjects/Chip8WorkingCopy/Chip8WorkingCopyTests/BinariesForTesting/BRIX");
            
            // Assert
            Assert.Contains("-", result[12]);
        }
    }
}