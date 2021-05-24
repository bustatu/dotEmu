using System;

namespace dotEmu.emulators.NES
{
    class PPU2502
    {
        // Table name
        Byte[,] tblName;
        Byte[] tblPalette;
        Byte[,] tblPattern;

        public PPU2502()
        {
            // Create name table
            tblName = new Byte[2,1024];
            tblPalette = new Byte[32];
            tblPattern = new Byte[2, 4096];
        }

        ~PPU2502()
        {
            // Destructor has nothing to do yet
        }

        // Has acces to cartridge
        private Cartridge cart;

        // System interfaces
        public void clock()
        {

        }
        public void connectCartridge(Cartridge cartridge)
        {
            cart = cartridge;
        }

        // Comunicate with main bus
        public Byte cpuRead(UInt16 addr, bool rdonly = false)
        {
            Byte data = 0;

            switch (addr)
            {
                case 0x0000: // Control
                    break;
                case 0x0001: // Mask
                    break;
                case 0x0002: // Status
                    break;
                case 0x0003: // OAM Address
                    break;
                case 0x0004: // OAM Data
                    break;
                case 0x0005: // Scroll
                    break;
                case 0x0006: // PPU Address
                    break;
                case 0x0007: // PPU Data
                    break;
            }

            return data;
        }

        public void cpuWrite(UInt16 addr, Byte data)
        {
            switch (addr)
            {
                case 0x0000: // Control
                    break;
                case 0x0001: // Mask
                    break;
                case 0x0002: // Status
                    break;
                case 0x0003: // OAM Address
                    break;
                case 0x0004: // OAM Data
                    break;
                case 0x0005: // Scroll
                    break;
                case 0x0006: // PPU Address
                    break;
                case 0x0007: // PPU Data
                    break;
            }
        }

        // Comunicate with PPU bus
        public Byte ppuRead(UInt16 addr, bool rdonly = false)
        {
            Byte data = 0;
            addr &= 0x3FFF;

            if(cart.ppuRead(addr, ref data))
            {

            }

            return data;
        }

        public void ppuWrite(UInt16 addr, Byte data)
        {
            addr &= 0x3FFF;

            if (cart.ppuWrite(addr, data))
            {

            }
        }
    }
}
