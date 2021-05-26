using System.IO;

namespace dotEmu.emulators.CHIP8
{
    class CHIP8Emulator : IEmulator
    {
        public string Name => "CHIP8";

        public CHIP8CPU cpu;
        

        public void loadROM(string path)
        {
            // Pass to the CPU
            cpu.readROM(path);
        }

        public void clock()
        {
            cpu.clock();
        }

        public CHIP8Emulator()
        {
            // Init CPU
            cpu = new CHIP8CPU(this);
        }
    }
}
