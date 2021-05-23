using System;
using System.Linq;

namespace dotEmu.emulators.NES
{
    class Bus
    {
        // Devices connected to the bus
        public CPU6502 cpu;

        // Debug for now (64KB RAM)
        public Byte[] ram;

        // Constructor and destructor
        public Bus()
        {
            // Set and clear ram
            ram = new Byte[64 * 1024];
            for (var i = 0; i < 64 * 1024; i++) ram[i] = 0;

            // Connect the 6502 to the bus
            cpu = new CPU6502();
            cpu.attachToBus(this);
        }
        ~Bus()
        {

        }

        // Writes a byte to a specific adress
        public void write(UInt16 addr, Byte data)
        {
            // Bounds check
            if(addr >= 0x0000 && addr <= 0xFFFF)
                ram[addr] = data;
        }

        // Reads a byte from a specific adress
        public Byte read(UInt16 addr, bool bReadOnly = false)
        {
            // Bounds check
            if (addr >= 0x0000 && addr <= 0xFFFF)
                return ram[addr];

            // If out of range
            return 0;
        }
    }
}
