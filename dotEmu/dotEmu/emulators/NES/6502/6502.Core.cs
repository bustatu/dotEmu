using System;

namespace dotEmu.emulators.NES
{
    partial class CPU6502
    {
        // Pointer to bus
        private Bus bus;

        public Byte status;    // Status register
        public Byte a;         // Acumulator
        public Byte x, y;      // X and Y registers
        public Byte sp;        // Stack pointer
        public UInt16 pc;      // Program counter

        // Flags
        public enum CPUFlags
        {
            C = (1 << 0),   // Carry
            Z = (1 << 1),   // Zero
            I = (1 << 2),   // Disable Intrerupts
            D = (1 << 3),   // Decimal mode
            B = (1 << 4),   // Break
            U = (1 << 5),   // Unused
            V = (1 << 6),   // Overflow
            N = (1 << 7)    // Negative
        }

        // Constructor and destructor
        public CPU6502()
        {
            // Initialise lookup table for opcodes
            initLookupTable();
        }
        ~CPU6502()
        {
            // Destructor has nothing to do yet
        }

        // Reads from the bus
        private byte read(UInt16 addr)
        {
            // Read from the bus
            return bus.CPURead(addr, false);
        }

        // Writes to the bus
        private void write(UInt16 addr, Byte data)
        {
            // Write to the bus
            bus.CPUWrite(addr, data);
        }

        // Attaches the CPU to a specific bus
        public void attachToBus(Bus b)
        {
            // Save bus for later usage
            bus = b;
        }

        // Sets and gets flags
        public Byte getFlag(CPUFlags flag)
        {
            return (Byte)(((status & (Byte)flag) > 0) ? 1 : 0);
        }
        public void setFlag(CPUFlags flag, bool value)
        {
            if (value)
                status |= (Byte)flag;
            else
                status &= (Byte)~flag;
        }
    }
}
