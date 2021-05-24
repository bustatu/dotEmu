using dotEmu.emulators.NES.Mappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace dotEmu.emulators.NES
{
    class Cartridge
    {
        // iNES format header
        struct NESHeader
        {
            public char[] name;
            public Byte prg_rom_chunks;
            public Byte chr_rom_chunks;
            public Byte mapper1;
            public Byte mapper2;
            public Byte prg_ram_size;
            public Byte tv_system1;
            public Byte tv_system2;
            public char[] unused;
        };

        private Byte[] PRGMemory;
        private Byte[] CHRMemory;
        Byte mapperID = 0;
        Byte PRGBanks = 0;
        Byte CHRBanks = 0;

        Mapper Mapper;

        public Cartridge(string fileName)
        {
            // Create header info
            NESHeader header = new NESHeader();
            header.name = new char[4];
            header.unused = new char[5];

            // Read file if exists
            if(File.Exists(fileName))
            {
                // Read header
                BinaryReader binReader = new BinaryReader(File.Open(fileName, FileMode.Open));

                // Imagine if I made this in C++
                for (var i = 0; i < 4; i++)
                    header.name[i] = binReader.ReadChar();
                header.prg_rom_chunks = binReader.ReadByte();
                header.chr_rom_chunks = binReader.ReadByte();
                header.mapper1 = binReader.ReadByte();
                header.mapper2 = binReader.ReadByte();
                header.prg_ram_size = binReader.ReadByte();
                header.tv_system1 = binReader.ReadByte();
                header.tv_system2 = binReader.ReadByte();
                for (var i = 0; i < 5; i++)
                    header.unused[i] = binReader.ReadChar();

                // Junk data
                if ((header.mapper1 & 0x04) != 0)
                    binReader.BaseStream.Seek(512, SeekOrigin.Current);

                // Determine mapper id
                mapperID = (Byte)(((header.mapper2 >> 4) << 4) | (header.mapper1 >> 4));

                // "Discover" file format
                Byte fileType = 1;

                if(fileType == 0)
                {
                   
                }
                else if(fileType == 1)
                {
                    PRGBanks = header.prg_rom_chunks;
                    PRGMemory = binReader.ReadBytes(PRGBanks * 16384);

                    CHRBanks = header.chr_rom_chunks;
                    CHRMemory = binReader.ReadBytes(PRGBanks * 8192);
                }
                else if(fileType == 2)
                {

                }

                // Load aprpropriate mapper
                switch(mapperID)
                {
                    case 0:
                        Mapper = new Mapper000(PRGBanks, CHRBanks);
                        break;
                    default:
                        MessageBox.Show("Mapper unsuported!");
                        break;
                }

                // Close the file
                binReader.Close();
            }
        }

        ~Cartridge()
        {
            // Destructor has nothing to do yet
        }

        // Comunicate with main bus
        public bool cpuRead(UInt16 addr, ref Byte data)
        {
            uint mapped_addr = 0;
            if (Mapper.cpuMapRead(addr, ref mapped_addr))
            {
                data = PRGMemory[mapped_addr];
                return true;
            }
            return false;
        }

        public bool cpuWrite(UInt16 addr, Byte data)
        {
            uint mapped_addr = 0;
            if (Mapper.cpuMapWrite(addr, ref mapped_addr))
            {
                PRGMemory[mapped_addr] = data;
                return true;
            }
            return false;
        }

        // Comunicate with PPU bus
        public bool ppuRead(UInt16 addr, ref Byte data)
        {
            uint mapped_addr = 0;
            if (Mapper.ppuMapRead(addr, ref mapped_addr))
            {
                data = CHRMemory[mapped_addr];
                return true;
            }
            return false;
        }

        public bool ppuWrite(UInt16 addr, Byte data)
        {
            uint mapped_addr = 0;
            if (Mapper.ppuMapRead(addr, ref mapped_addr))
            {
                CHRMemory[mapped_addr] = data;
                return true;
            }
            return false;
        }
    }
}
