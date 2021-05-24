using System;

namespace dotEmu.emulators.NES.Mappers
{
    class Mapper000 : Mapper
    {
        public Mapper000(Byte prgBanks, Byte chrBanks) : base(prgBanks, chrBanks)
        {

        }

        ~Mapper000()
        {

        }

        public override bool cpuMapRead(ushort addr, ref uint mapped_addr)
        {
            if(addr >= 0x8000 && addr <= 0xFFFF)
            {
                mapped_addr = (uint)(addr & (PRGBanks > 1 ? 0x7FFF : 0x3FFF));
                return true;
            }

            return false;
        }

        public override bool cpuMapWrite(ushort addr, ref uint mapped_addr)
        {
            if (addr >= 0x8000 && addr <= 0xFFFF)
            {
                mapped_addr = (uint)(addr & (PRGBanks > 1 ? 0x7FFF : 0x3FFF));
                return true;
            }

            return false;
        }

        public override bool ppuMapRead(ushort addr, ref uint mapped_addr)
        {
            if (addr >= 0x0000 && addr <= 0x1FFF)
            {
                mapped_addr = addr;
                return true;
            }

            return false;
        }

        public override bool ppuMapWrite(ushort addr, ref uint mapped_addr)
        {
            return false;
        }
    }
}
