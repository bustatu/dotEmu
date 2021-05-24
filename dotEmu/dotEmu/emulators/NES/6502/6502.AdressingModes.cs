using System;

namespace dotEmu.emulators.NES
{
    partial class CPU6502
    {
        // Adressing modes
        public Byte IMP()
        {
            // It might operate on the accumulator
            fetched = a;

            // Do nothing for implied
            return 0;
        }
        public Byte IMM()
        {
            // Operates on the next byte
            addr_abs = pc++;

            // Do nothing for immediate
            return 0;
        }
        public Byte ZP0()
        {
            // Get byte from 0th page
            addr_abs = read(pc);
            pc++;
            addr_abs &= 0x00FF;

            // Do nothing for Zero page adressing
            return 0;
        }
        public Byte ZPX()
        {
            // Get byte from 0th page at offset X
            addr_abs = read((Byte)(pc + x));
            pc++;
            addr_abs &= 0x00FF;

            // Do nothing for Zero page adressing
            return 0;
        }
        public Byte ZPY()
        {
            // Get byte from 0th page at offset Y
            addr_abs = read((Byte)(pc + y));
            pc++;
            addr_abs &= 0x00FF;

            // Do nothing for Zero page adressing
            return 0;
        }
        public Byte REL()
        {
            // Relative adressing for branching instructions only (like ifs)
            // Can't jump further than 127 bytes

            // Read difference
            addr_rel = read(pc);
            pc++;

            // Make it signed if required
            if ((addr_rel & 0x80) != 0)
                addr_rel |= 0xFF00;
            return 0;
        }
        public Byte ABS()
        {
            // Get adress from memory
            UInt16 lo = read(pc);
            pc++;
            UInt16 hi = read(pc);
            pc++;

            // Compose adress from high and low bits
            addr_abs = (UInt16)((hi << 8) | lo);

            // Do nothing for Absolute adressing
            return 0;
        }
        public Byte ABX()
        {
            // Get adress from memory
            UInt16 lo = read(pc);
            pc++;
            UInt16 hi = read(pc);
            pc++;

            // Compose adress from high and low bits and offset X
            addr_abs = (UInt16)((hi << 8) | lo);
            addr_abs += x;

            // If the adress page has changed
            if ((addr_abs & 0xFF00) != (hi << 8))
                return 1;
            return 0;
        }
        public Byte ABY()
        {
            // Get adress from memory
            UInt16 lo = read(pc);
            pc++;
            UInt16 hi = read(pc);
            pc++;

            // Compose adress from high and low bits and offset Y
            addr_abs = (UInt16)((hi << 8) | lo);
            addr_abs += y;

            // If the adress page has changed
            if ((addr_abs & 0xFF00) != (hi << 8))
                return 1;
            return 0;
        }
        public Byte IND()
        {
            // Get pointer from memory
            UInt16 ptr_lo = read(pc);
            pc++;
            UInt16 ptr_hi = read(pc);
            pc++;

            // Compose pointer from high and low bits
            UInt16 ptr = (UInt16)((ptr_hi << 8) | ptr_lo);

            // Page boundary
            if (ptr_lo == 0x00FF)
            {
                // Compose adress from pointer location
                addr_abs = (UInt16)((read((UInt16)(ptr & 0xFF00)) << 8) | read(ptr));
            }
            else
            {
                // Compose adress from pointer location
                addr_abs = (UInt16)((read((UInt16)(ptr + 1)) << 8) | read(ptr));
            }

            return 0;
        }
        public Byte IZX()
        {
            // Get pointer from memory
            UInt16 t = read(pc);
            pc++;

            // Compose adress from low and high bits
            UInt16 lo = read((UInt16)((t + x) & 0x00FF));
            UInt16 hi = read((UInt16)((t + x + 1) & 0x00FF));

            addr_abs = (UInt16)((hi << 8) | lo);

            return 0;
        }
        public Byte IZY()
        {
            // Get pointer from memory
            UInt16 t = read(pc);
            pc++;

            // Compose adress from low and high bits
            UInt16 lo = read((UInt16)(t & 0x00FF));
            UInt16 hi = read((UInt16)((t + 1) & 0x00FF));

            // Add y to it
            addr_abs = (UInt16)((hi << 8) | lo);
            addr_abs += y;

            // If the adress page has changed
            if ((addr_abs & 0xFF00) != (hi << 8))
                return 1;
            return 0;
        }
    }
}
