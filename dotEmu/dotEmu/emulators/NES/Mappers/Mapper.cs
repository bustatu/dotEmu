using System;

namespace dotEmu.emulators.NES.Mappers
{
    class Mapper
    {
        public Mapper(Byte prgBanks, Byte chrBanks)
        {
            PRGBanks = prgBanks;
            CHRBanks = chrBanks;
        }
        ~Mapper()
        {

        }

        public virtual bool cpuMapRead(UInt16 addr, ref UInt32 mapped_addr) { return false; }
        public virtual bool cpuMapWrite(UInt16 addr, ref UInt32 mapped_addr) { return false; }
        public virtual bool ppuMapRead(UInt16 addr, ref UInt32 mapped_addr) { return false; }
        public virtual bool ppuMapWrite(UInt16 addr, ref UInt32 mapped_addr) { return false; }

        protected Byte PRGBanks = 0;
        protected Byte CHRBanks = 0;
    }
}
