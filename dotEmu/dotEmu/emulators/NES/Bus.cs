using System;

namespace dotEmu.emulators.NES
{
    class Bus
    {
        // Devices connected to the bus
        public CPU6502 cpu;
        public PPU2502 ppu;
        public Cartridge cart;

        // CPU RAM (2 KB)
        public Byte[] cpuRAM;

        // Internal clock counter
        UInt32 systemClockCounter = 0;

        // Constructor and destructor
        public Bus()
        {
            // Set and clear ram
            cpuRAM = new Byte[2 * 1024];
            for (var i = 0; i < 2 * 1024; i++) cpuRAM[i] = 0;

            // Connect the 6502 to the bus
            cpu = new CPU6502();
            cpu.attachToBus(this);

            // Connect the ppu to the bus
            ppu = new PPU2502();
        }
        ~Bus()
        {

        }

        // System interfaces

        public void insertCartridge(Cartridge cartridge)
        {
            cart = cartridge;
            ppu.connectCartridge(cartridge);
        }
        public void reset()
        {
            cpu.reset();
            systemClockCounter = 0;
        }
        public void clock()
        {
            ppu.clock();
            if(systemClockCounter % 3 == 0)
            {
                cpu.clock();
            }
            systemClockCounter++;
        }

        // Writes a byte to a specific adress
        public void CPUWrite(UInt16 addr, Byte data)
        {
            if (cart.cpuWrite(addr, data) != false)
            {

            }
            else
            {
                // Adresses the CPU
                if (addr >= 0x0000 && addr <= 0x1FFF)
                    cpuRAM[addr & 0x7FF] = data;

                // Adresses the PPU
                else if (addr >= 0x2000 && addr <= 0x3FFF)
                    ppu.cpuWrite((UInt16)(addr & 0x0007), data);
            }
        }

        // Reads a byte from a specific adress
        public Byte CPURead(UInt16 addr, bool bReadOnly = false)
        {
            Byte data = 0;

            if (cart.cpuRead(addr, ref data) != false)
            {

            }
            else
            {
                // Adresses the CPU
                if (addr >= 0x0000 && addr <= 0x1FFF)
                    data = cpuRAM[addr & 0x7FF];

                // Adresses the PPU
                else if (addr >= 0x2000 && addr <= 0x3FFF)
                    ppu.cpuRead((UInt16)(addr & 0x0007), bReadOnly);
            }

            // Return result
            return data;
        }
    }
}
