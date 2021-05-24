using System;
using System.Windows.Forms;

/* Resources used:
 * http://archive.6502.org/datasheets/rockwell_r650x_r651x.pdf
 * https://www.youtube.com/watch?v=8XmxKPJDGU0
 * https://github.com/OneLoneCoder/olcNES
 * https://github.com/Xyene/Emulator.NES
*/

namespace dotEmu.emulators.NES
{
    partial class CPU6502
    {
        // Data fetched by fetch function
        public Byte fetched = 0x00;
        public Byte fetch()
        {
            // Implied adress mode is not allowed to fetch
            if (!(lookupTable[opcode].addrMode == IMP))
                fetched = read(addr_abs);
            return fetched;
        }

        // Absolute and relative adress to extract data from
        public UInt16 addr_abs = 0x0000;
        public UInt16 addr_rel = 0x0000;

        // Current opcode and remaining cycles
        public Byte opcode = 0x00;
        public Byte cycles = 0x00;

        // OPCodes
        public Byte ADC()
        {
            // Fetch data
            fetch();

            // Addition done in 16 bit domain
            UInt16 temp = (UInt16)(a + fetched + getFlag(CPUFlags.C));

            // Carry flag
            setFlag(CPUFlags.C, temp > 0xFF);

            // Zero flag
            setFlag(CPUFlags.Z, temp == 0);

            // Overflow flag
            setFlag(CPUFlags.V, ((~((UInt16)a ^ (UInt16)fetched) & ((UInt16)a ^ (UInt16)temp)) & 0x0080) != 0);

            // Negative flag
            setFlag(CPUFlags.N, (temp & 0x80) != 0);

            // Result into acumulator
            a = (Byte)(temp & 0x00FF);

            // Can have aditional clock cycles
            return 1;
        }
        public Byte AND()
        {
            // Fetch data
            fetch();

            // Bitwise AND
            a = (Byte)(a & fetched);

            // If everything is 0 set Z flag
            setFlag(CPUFlags.Z, a == 0x0);

            // If negative set N flag
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            // Can have aditional clock cycles
            return 1;
        }
        public Byte ASL()
        {
            // Shift left
            fetch();
            UInt16 temp = (UInt16)(fetched << 1);

            // Set flags
            setFlag(CPUFlags.C, (temp & 0xFF00) > 0);
            setFlag(CPUFlags.Z, (temp & 0xFF00) == 0);
            setFlag(CPUFlags.N, (temp & 0x80) != 0);

            // If implied adressing we save directly to the acumulator
            if (lookupTable[opcode].addrMode == IMP)
                a = (Byte)(temp & 0x00FF);
            // Else we write to the memory
            else
                write(addr_abs, (Byte)(temp & 0x00FF));

            return 0;
        }
        public Byte BCC()
        {
            // Branching instruction if carry flag is 0
            if (getFlag(CPUFlags.C) == 0)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BCS()
        {
            // Branching instruction if carry flag is 1
            if (getFlag(CPUFlags.C) == 1)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BEQ()
        {
            // Branching instruction if equal (Z flag is set)
            if (getFlag(CPUFlags.Z) == 1)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BIT()
        {
            // Bitwise AND ?????
            fetch();

            Byte temp = (Byte)(a & fetched);

            // Set flags
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);
            setFlag(CPUFlags.N, (fetched & (1 << 7)) != 0);
            setFlag(CPUFlags.V, (fetched & (1 << 6)) != 0);

            return 0;
        }
        public Byte BMI()
        {
            // Branching instruction if negative (N flag is set)
            if (getFlag(CPUFlags.N) == 1)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BNE()
        {
            // Branching instruction if not equal (Z flag is not set)
            if (getFlag(CPUFlags.Z) == 0)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BPL()
        {
            // Branching instruction if positive (N flag is not set)
            if (getFlag(CPUFlags.N) == 0)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BRK()
        {
            // Program sourced intrerupt
            pc++;

            // Write current location to stack
            setFlag(CPUFlags.I, true);
            write((UInt16)(0x100 + sp), (Byte)((pc >> 8) & 0x00FF));
            sp--;
            write((UInt16)(0x100 + sp), (Byte)(pc & 0x00FF));
            sp--;

            // Write status to flag
            setFlag(CPUFlags.B, true);
            write((UInt16)(0x100 + sp), status);
            sp--;
            setFlag(CPUFlags.B, false);

            // Go to specific location from PC
            pc = (UInt16)(read(0xFFFE) | (read(0xFFFF) << 8));

            return 0;
        }
        public Byte BVC()
        {
            // Branching instruction if not overflow (V flag is not set)
            if (getFlag(CPUFlags.V) == 0)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte BVS()
        {
            // Branching instruction if overflow (V flag is set)
            if (getFlag(CPUFlags.V) == 1)
            {
                cycles++;
                addr_abs = (UInt16)(pc + addr_rel);

                // Page boundary crossing
                if ((addr_rel & 0xFF00) != (pc & 0xFF00))
                    cycles++;

                pc = addr_abs;
            }
            return 0;
        }
        public Byte CLC()
        {
            // Clear carry flag
            setFlag(CPUFlags.C, false);
            return 0;
        }
        public Byte CLD()
        {
            // Clear decimal flag
            setFlag(CPUFlags.D, false);
            return 0;
        }
        public Byte CLI()
        {
            // Clear intrerupts flag
            setFlag(CPUFlags.I, false);
            return 0;
        }
        public Byte CLV()
        {
            // Clear overflow flag
            setFlag(CPUFlags.V, false);
            return 0;
        }
        public Byte CMP()
        {
            // Compare acumulator to fetched
            fetch();

            // Calculate results
            UInt16 temp = (UInt16)(a - fetched);

            // Set flags
            setFlag(CPUFlags.C, a >= fetched);
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            return 1;
        }
        public Byte CPX()
        {
            // Compare X register to fetched
            fetch();

            // Calculate results
            UInt16 temp = (UInt16)(x - fetched);

            // Set flags
            setFlag(CPUFlags.C, x >= fetched);
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            return 0;
        }
        public Byte CPY()
        {
            // Compare X register to fetched
            fetch();

            // Calculate results
            UInt16 temp = (UInt16)(y - fetched);

            // Set flags
            setFlag(CPUFlags.C, y >= fetched);
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            return 0;
        }
        public Byte DEC()
        {
            // Decrement value at memory location
            fetch();

            // Fetch and decrement and write
            Byte temp = (Byte)(fetched - 1);
            write(addr_abs, (Byte)(temp & 0x00FF));

            // Set flags
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0x0000);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            return 0;
        }
        public Byte DEX()
        {
            // Decrement X register
            x--;

            // Set flags
            setFlag(CPUFlags.Z, x == 0);
            setFlag(CPUFlags.N, (x & 0x80) != 0);

            return 0;
        }
        public Byte DEY()
        {
            // Decrement Y register
            y--;

            // Set flags
            setFlag(CPUFlags.Z, y == 0);
            setFlag(CPUFlags.N, (y & 0x80) != 0);

            return 0;
        }
        public Byte EOR()
        {
            // Logic XOR
            fetch();

            // XOR the acumulator with the fetched result
            a = (Byte)(a ^ fetched);

            // Set flags
            setFlag(CPUFlags.Z, a == 0);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 1;
        }
        public Byte INC()
        {
            // Decrement value at memory location
            fetch();

            // Fetch and decrement and write
            Byte temp = (Byte)(fetched + 1);
            write(addr_abs, (Byte)(temp & 0x00FF));

            // Set flags
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0x0000);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            return 0;
        }
        public Byte INX()
        {
            // Increment X register
            x++;

            // Set flags
            setFlag(CPUFlags.Z, x == 0);
            setFlag(CPUFlags.N, (x & 0x80) != 0);

            return 0;
        }
        public Byte INY()
        {
            // Increment Y register
            y++;

            // Set flags
            setFlag(CPUFlags.Z, y == 0);
            setFlag(CPUFlags.N, (y & 0x80) != 0);

            return 0;
        }
        public Byte JMP()
        {
            // Jump to adress at location
            pc = addr_abs;

            return 0;
        }
        public Byte JSR()
        {
            // Jump to subroutine
            pc--;

            // Write current position to stack
            write((UInt16)(0x0100 + sp), (Byte)((pc >> 8) & 0x00FF));
            sp--;
            write((UInt16)(0x0100 + sp), (Byte)(pc & 0x00FF));
            sp--;

            // Jump
            pc = addr_abs;

            return 0;
        }
        public Byte LDA()
        {
            // Load value in A
            fetch();

            a = fetched;

            // Set flags
            setFlag(CPUFlags.Z, a == 0);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 1;
        }
        public Byte LDX()
        {
            // Load value in X
            fetch();

            x = fetched;

            // Set flags
            setFlag(CPUFlags.Z, x == 0);
            setFlag(CPUFlags.N, (x & 0x80) != 0);

            return 1;
        }
        public Byte LDY()
        {
            // Load value in Y
            fetch();

            y = fetched;

            // Set flags
            setFlag(CPUFlags.Z, y == 0);
            setFlag(CPUFlags.N, (y & 0x80) != 0);

            return 1;
        }
        public Byte LSR()
        {
            // Right shift
            fetch();
            
            // Save last byte digit
            setFlag(CPUFlags.C, (fetched & 0x0001) != 0);

            // Shift
            Byte temp = (Byte)(fetched >> 1);

            // Set flags
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            // If adress mode is implied save to acumulator
            if (lookupTable[opcode].addrMode == IMP)
                a = (Byte)(temp & 0x00FF);
            else
                // Write to memory
                write(addr_abs, (Byte)(temp & 0x00FF));
            return 0;
        }
        public Byte NOP()
        {
            // NOPs are not equal
            switch (opcode)
            {
                case 0x1C:
                case 0x3C:
                case 0x5C:
                case 0x7C:
                case 0xDC:
                case 0xFC:
                    return 1;
            }
            return 0;
        }
        public Byte ORA()
        {
            // Logic OR
            fetch();

            // Store result in acumulator
            a = (Byte)(a | fetched);

            // Set flags
            setFlag(CPUFlags.Z, a == 0x00);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 1;
        }
        public Byte PHA()
        {
            // Push acumulator to stack
            write((UInt16)(0x100 + sp), a);
            sp--;
            return 0;
        }
        public Byte PHP()
        {
            // Push status register to stack
            write((UInt16)(0x0100 + sp), (Byte)(status | (Byte)CPUFlags.B | (Byte)CPUFlags.U));
            
            // Set flags
            setFlag(CPUFlags.B, false);
            setFlag(CPUFlags.U, false);
            sp--;

            return 0;
        }
        public Byte PLA()
        {
            // Pop acumulator from stack
            sp++;
            a = read((UInt16)(0x100 + sp));

            // Set negative and zero flags
            setFlag(CPUFlags.Z, a == 0);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 0;
        }
        public Byte PLP()
        {
            // Pop status register from stack
            sp++;
            status = read((Byte)(0x1000 + sp));

            // Set flags
            setFlag(CPUFlags.U, true);

            return 0;
        }
        public Byte ROL()
        {
            // Add digit to the back of the number
            fetch();
            UInt16 temp = (UInt16)((fetched << 1) | getFlag(CPUFlags.C));

            // Set flags
            setFlag(CPUFlags.C, (temp & 0xFF00) != 0);
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            // If implied mode save to accumulator
            if (lookupTable[opcode].addrMode == IMP)
                a = (Byte)(temp & 0x00FF);
            else
                // Write to memory
                write(addr_abs, (Byte)(temp & 0x00FF));

            return 0;
        }
        public Byte ROR()
        {
            // Add digit to the front of the number
            fetch();
            UInt16 temp = (UInt16)((getFlag(CPUFlags.C) << 7) | (fetched >> 1));

            // Set flags
            setFlag(CPUFlags.C, (fetched & 0x01) != 0);
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0x00);
            setFlag(CPUFlags.N, (temp & 0x0080) != 0);

            // If implied mode then save to acumulator
            if (lookupTable[opcode].addrMode == IMP)
                a = (Byte)(temp & 0x00FF);
            else
                // Write to memory
                write(addr_abs, (Byte)(temp & 0x00FF));

            return 0;
        }
        public Byte RTI()
        {
            // Return from intrerupt
            // Read status
            sp++;
            status = read((UInt16)(0x100 + sp));

            // HACK (remove it stuff doesnt work properly)
            status &= (Byte)(-(~(Byte)CPUFlags.B));
            status &= (Byte)(-(~(Byte)CPUFlags.U));

            // Read PC from stack
            sp++;
            pc = (UInt16)(read((UInt16)(0x100 + sp)));
            sp++;
            pc = (UInt16)(read((UInt16)(0x100 + sp)));
            return 0;
        }
        public Byte RTS()
        {
            // Return from stack
            sp++;
            pc = (UInt16)read((UInt16)(0x0100 + sp));
            sp++;
            pc |= (UInt16)(read((UInt16)(0x0100 + sp)) << 8);
            pc++;

            return 0;
        }
        public Byte SBC()
        {
            // Fetch data
            fetch();

            // Just make it the same as addition but with negative value
            UInt16 value = (UInt16)(fetched ^ 0x00FF);

            // Subtraction done in 16 bit domain
            UInt16 temp = (UInt16)(a + value + getFlag(CPUFlags.C));

            // Carry flag
            setFlag(CPUFlags.C, (temp & 0xFF00) != 0);

            // Zero flag
            setFlag(CPUFlags.Z, (temp & 0x00FF) == 0);

            // Overflow flag
            setFlag(CPUFlags.V, ((temp ^ a) & (temp ^ value) & 0x0080) != 0);

            // Negative flag
            setFlag(CPUFlags.N, (temp & 0x80) != 0);

            // Result into acumulator
            a = (Byte)(temp & 0x00FF);

            // Can have aditional clock cycles
            return 1;
        }
        public Byte SEC()
        {
            // Set carry flag
            setFlag(CPUFlags.C, true);
            return 0;
        }
        public Byte SED()
        {
            // Set decimal flag
            setFlag(CPUFlags.D, true);
            return 0;
        }
        public Byte SEI()
        {
            // Set intrerupt flag
            setFlag(CPUFlags.I, true);
            return 0;
        }
        public Byte STA()
        {
            // Store acumulator at adress
            write(addr_abs, a);
            return 0;
        }
        public Byte STX()
        {
            // Store x at adress
            write(addr_abs, x);
            return 0;
        }
        public Byte STY()
        {
            // Store y at adress
            write(addr_abs, y);
            return 0;
        }
        public Byte TAX()
        {
            // Transfer acumulator to x
            x = a;

            // Set flags
            setFlag(CPUFlags.Z, x == 0x00);
            setFlag(CPUFlags.N, (x & 0x80) != 0);

            return 0;
        }
        public Byte TAY()
        {
            // Transfer acumulator to x
            y = a;

            // Set flags
            setFlag(CPUFlags.Z, y == 0x00);
            setFlag(CPUFlags.N, (y & 0x80) != 0);

            return 0;
        }
        public Byte TSX()
        {
            // Transfer stack pointer to x
            x = sp;

            // Set flags
            setFlag(CPUFlags.Z, x == 0x00);
            setFlag(CPUFlags.N, (x & 0x80) != 0);

            return 0;
        }
        public Byte TXA()
        {
            // Transfer x to acumulator
            a = x;

            // Set flags
            setFlag(CPUFlags.Z, a == 0x00);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 0;
        }
        public Byte TXS()
        {
            // Transfer x to stack pointer
            sp = x;

            return 0;
        }
        public Byte TYA()
        {
            // Transfer y to acumulator
            a = y;

            // Set flags
            setFlag(CPUFlags.Z, a == 0x00);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 0;
        }
        public Byte XXX()
        {
            // Illegal opcode
            return 0;
        }
        public void clock()
        {
            // If we can execute the opcode
            if (cycles == 0)
            {
                // Read it from the location and increment
                opcode = read(pc);
                pc++;

                // Always set the unused flag to true
                setFlag(CPUFlags.U, true);

                // Get starting number of cycles
                cycles = lookupTable[opcode].cycles;

                // Set the addressing mode and execute the opcode
                Byte duration1 = lookupTable[opcode].addrMode();
                Byte duration2 = lookupTable[opcode].operate();

                // Update for variable lengths
                cycles += (Byte)(duration1 & duration2);
            }

            // One clock cycle has passed
            cycles--;
        }
        public void reset()
        {
            // Configures the CPU into the default state
            a = 0;
            x = 0;
            y = 0;
            sp = 0xFD;
            status = 0 | (Byte)CPUFlags.U;

            // Search for program start
            addr_abs = 0xFFFC;

            // Set program counter
            UInt16 lo = read(addr_abs);
            UInt16 hi = read((UInt16)(addr_abs + 1));

            pc = (UInt16)((hi << 8) | lo);

            // Default values
            addr_rel = 0;
            addr_abs = 0;
            fetched = 0;

            // Reset takes time
            cycles = 8;
        }
        public void irq()
        {
            // If intrerupts are not disabled
            if (getFlag(CPUFlags.I) == 0)
            {
                // Write current PC to stack
                write((UInt16)(0x100 + sp), (Byte)((pc >> 8) & 0x00FF));
                sp--;
                write((UInt16)(0x100 + sp), (Byte)(pc & 0xFF));
                sp--;

                // Set flags
                setFlag(CPUFlags.B, false);
                setFlag(CPUFlags.U, true);
                setFlag(CPUFlags.I, true);

                // Write status to stack
                write((UInt16)(0x100 + sp), status);
                sp--;

                // Get new PC from hardcoded adress
                addr_abs = 0xFFFE;

                // Set program counter
                UInt16 lo = read(addr_abs);
                UInt16 hi = read((UInt16)(addr_abs + 1));

                pc = (UInt16)((hi << 8) | lo);

                // Take time
                cycles = 7;
            }
        }
        public void nmi()
        {
            // Same as IRQ but the non-maskable intrerupt can't be stopped

            // Write current PC to stack
            write((UInt16)(0x100 + sp), (Byte)((pc >> 8) & 0x00FF));
            sp--;
            write((UInt16)(0x100 + sp), (Byte)(pc & 0xFF));
            sp--;

            // Set flags
            setFlag(CPUFlags.B, false);
            setFlag(CPUFlags.U, true);
            setFlag(CPUFlags.I, true);

            // Write status to stack
            write((UInt16)(0x100 + sp), status);
            sp--;

            // Get new PC from hardcoded adress
            addr_abs = 0xFFFA;

            // Set program counter
            UInt16 lo = read(addr_abs);
            UInt16 hi = read((UInt16)(addr_abs + 1));

            pc = (UInt16)((hi << 8) | lo);

            // Take time
            cycles = 7;
        }

        // Instruction struct
        public class Instruction
        {
            // Instruction name
            public string name;
            // Which instruction to use
            public Func<Byte> operate;
            // Adressing mode
            public Func<Byte> addrMode;
            // Duration in cycles
            public Byte cycles;
            // Assign values to that instruction
            public void assign(string instrName, Func<Byte> operateMode, Func<Byte> addressMode, Byte cycleDuration)
            {
                name = instrName;
                operate = operateMode;
                addrMode = addressMode;
                cycles = cycleDuration;
            }
        }

        // OPCode table
        public Instruction[] lookupTable;

        // Function to initialise lookup table
        private void initLookupTable()
        {
            // Initialise table
            lookupTable = new Instruction[16 * 16];

            // Default everything to unknown values
            for (var i = 0; i < 16 * 16; i++)
            {
                lookupTable[i] = new Instruction();
                lookupTable[i].assign("???", XXX, IMP, 0);
            }

            // Have to believe it's the correct values
            lookupTable[0x00].assign("BRK", BRK, IMM, 7);
            lookupTable[0x01].assign("ORA", ORA, IZX, 6);
            lookupTable[0x05].assign("ORA", ORA, ZP0, 3);
            lookupTable[0x06].assign("ASL", ASL, ZP0, 5);
            lookupTable[0x08].assign("PHP", PHP, IMP, 3);
            lookupTable[0x09].assign("ORA", ORA, IMM, 2);
            lookupTable[0x0A].assign("ASL", ASL, IMP, 2);
            lookupTable[0x0D].assign("ORA", ORA, ABS, 4);
            lookupTable[0x0E].assign("ASL", ASL, ABS, 6);

            lookupTable[0x10].assign("BPL", BPL, REL, 2);
            lookupTable[0x11].assign("ORA", ORA, IZY, 5);
            lookupTable[0x15].assign("ORA", ORA, ZPX, 4);
            lookupTable[0x16].assign("ASL", ASL, ZPX, 6);
            lookupTable[0x18].assign("PLP", PLP, IMP, 4);
            lookupTable[0x19].assign("ORA", ORA, ABY, 4);
            lookupTable[0x1D].assign("ORA", ORA, ABX, 4);
            lookupTable[0x1E].assign("ASL", ASL, ABX, 7);

            lookupTable[0x20].assign("JSR", JSR, ABS, 6);
            lookupTable[0x21].assign("AND", AND, IZX, 6);
            lookupTable[0x24].assign("BIT", BIT, ZP0, 3);
            lookupTable[0x25].assign("AND", AND, ZP0, 3);
            lookupTable[0x26].assign("ROL", ROL, ZP0, 5);
            lookupTable[0x28].assign("PLP", PLP, IMP, 4);
            lookupTable[0x29].assign("AND", AND, IMM, 2);
            lookupTable[0x2A].assign("ROL", ROL, IMP, 2);
            lookupTable[0x2C].assign("BIT", BIT, ABS, 4);
            lookupTable[0x2D].assign("AND", AND, ABS, 4);
            lookupTable[0x2E].assign("ROL", ROL, ABS, 6);

            lookupTable[0x30].assign("BMI", BMI, REL, 2);
            lookupTable[0x31].assign("AND", AND, IZY, 5);
            lookupTable[0x35].assign("AND", AND, ZPX, 4);
            lookupTable[0x36].assign("ROL", ROL, ZPX, 6);
            lookupTable[0x38].assign("SEC", SEC, IMP, 2);
            lookupTable[0x39].assign("AND", AND, ABY, 4);
            lookupTable[0x3D].assign("AND", AND, ABX, 4);
            lookupTable[0x3E].assign("ROL", ROL, ABX, 7);

            lookupTable[0x40].assign("RTI", RTI, IMP, 6);
            lookupTable[0x41].assign("EOR", EOR, IZX, 6);
            lookupTable[0x45].assign("EOR", EOR, ZP0, 3);
            lookupTable[0x46].assign("LSR", LSR, ZP0, 5);
            lookupTable[0x48].assign("PHA", PHA, IMP, 3);
            lookupTable[0x49].assign("EOR", EOR, IMM, 2);
            lookupTable[0x4A].assign("LSR", LSR, IMP, 2);
            lookupTable[0x4C].assign("JMP", JMP, ABS, 3);
            lookupTable[0x4D].assign("EOR", EOR, ABS, 4);
            lookupTable[0x4E].assign("LSR", LSR, ABS, 6);

            lookupTable[0x50].assign("BVC", BVC, REL, 2);
            lookupTable[0x51].assign("EOR", EOR, IZY, 5);
            lookupTable[0x55].assign("EOR", EOR, ZPX, 4);
            lookupTable[0x56].assign("LSR", LSR, ZPX, 6);
            lookupTable[0x58].assign("CLI", CLI, IMP, 2);
            lookupTable[0x59].assign("EOR", EOR, ABY, 4);
            lookupTable[0x5D].assign("EOR", EOR, ABX, 4);
            lookupTable[0x5E].assign("LSR", LSR, ABX, 7);

            lookupTable[0x60].assign("RTS", RTS, IMP, 6);
            lookupTable[0x61].assign("ADC", ADC, IZX, 6);
            lookupTable[0x65].assign("ADC", ADC, ZP0, 3);
            lookupTable[0x66].assign("ROR", ROR, ZP0, 5);
            lookupTable[0x68].assign("PLA", PLA, IMP, 4);
            lookupTable[0x69].assign("ADC", ADC, IMM, 2);
            lookupTable[0x6A].assign("ROR", ROR, IMP, 2);
            lookupTable[0x6C].assign("JMP", JMP, IND, 5);
            lookupTable[0x6D].assign("ADC", ADC, ABS, 4);
            lookupTable[0x6E].assign("ROR", ROR, ABS, 6);

            lookupTable[0x70].assign("BVS", BVS, REL, 2);
            lookupTable[0x71].assign("ADC", ADC, IZY, 5);
            lookupTable[0x75].assign("ADC", ADC, ZPX, 4);
            lookupTable[0x76].assign("ROR", ROR, ZPX, 6);
            lookupTable[0x78].assign("SEI", SEI, IMP, 2);
            lookupTable[0x79].assign("ADC", ADC, ABY, 4);
            lookupTable[0x7D].assign("ADC", ADC, ABX, 4);
            lookupTable[0x7E].assign("ROR", ROR, ABX, 7);

            lookupTable[0x81].assign("STA", STA, IZX, 6);
            lookupTable[0x84].assign("STY", STY, ZP0, 3);
            lookupTable[0x85].assign("STA", STA, ZP0, 3);
            lookupTable[0x86].assign("STX", STX, ZP0, 3);
            lookupTable[0x88].assign("DEY", DEY, IMP, 2);
            lookupTable[0x8A].assign("TXA", TXA, IMP, 2);
            lookupTable[0x8C].assign("STY", STY, ABS, 4);
            lookupTable[0x8D].assign("STA", STA, ABS, 4);
            lookupTable[0x8E].assign("STX", STX, ABS, 4);

            lookupTable[0x90].assign("BCC", BCC, REL, 2);
            lookupTable[0x91].assign("STA", STA, IZY, 6);
            lookupTable[0x94].assign("STY", STY, ZPX, 4);
            lookupTable[0x95].assign("STA", STA, ZPX, 4);
            lookupTable[0x96].assign("STX", STX, ZPY, 4);
            lookupTable[0x98].assign("TYA", TYA, IMP, 2);
            lookupTable[0x99].assign("STA", STA, ABY, 5);
            lookupTable[0x9A].assign("TXS", TXS, IMP, 2);
            lookupTable[0x9D].assign("STA", STA, ABX, 5);

            lookupTable[0xA0].assign("LDY", LDY, IMM, 2);
            lookupTable[0xA1].assign("LDA", LDA, IZX, 6);
            lookupTable[0xA2].assign("LDX", LDX, IMM, 2);
            lookupTable[0xA4].assign("LDY", LDY, ZP0, 3);
            lookupTable[0xA5].assign("LDA", LDA, ZP0, 3);
            lookupTable[0xA6].assign("LDX", LDX, ZP0, 3);
            lookupTable[0xA8].assign("TAY", TAY, IMP, 2);
            lookupTable[0xA9].assign("LDA", LDA, IMM, 2);
            lookupTable[0xAA].assign("TAX", TAX, IMP, 2);
            lookupTable[0xAC].assign("LDY", LDY, ABS, 4);
            lookupTable[0xAD].assign("LDA", LDA, ABS, 4);
            lookupTable[0xAE].assign("LDX", LDX, ABS, 4);

            lookupTable[0xB0].assign("BCS", BCS, REL, 2);
            lookupTable[0xB1].assign("LDA", LDA, IZY, 5);
            lookupTable[0xB4].assign("LDY", LDY, ZPX, 4);
            lookupTable[0xB5].assign("LDA", LDA, ZPX, 4);
            lookupTable[0xB6].assign("LDX", LDX, ZPY, 4);
            lookupTable[0xB8].assign("CLV", CLV, IMP, 2);
            lookupTable[0xB9].assign("LDA", LDA, ABY, 4);
            lookupTable[0xBA].assign("TSX", TSX, IMP, 2);
            lookupTable[0xBC].assign("LDY", LDY, ABX, 4);
            lookupTable[0xBD].assign("LDA", LDA, ABX, 4);
            lookupTable[0xBE].assign("LDX", LDX, ABY, 4);

            lookupTable[0xC0].assign("CPY", CPY, IMM, 2);
            lookupTable[0xC1].assign("CMP", CMP, IZX, 6);
            lookupTable[0xC4].assign("CPY", CPY, ZP0, 3);
            lookupTable[0xC5].assign("CMP", CMP, ZP0, 3);
            lookupTable[0xC6].assign("DEC", DEC, ZP0, 5);
            lookupTable[0xC8].assign("INY", INY, IMP, 2);
            lookupTable[0xC9].assign("CMP", CMP, IMM, 2);
            lookupTable[0xCA].assign("DEX", DEX, IMP, 2);
            lookupTable[0xCC].assign("CPY", CPY, ABS, 4);
            lookupTable[0xCD].assign("CMP", CMP, ABS, 4);
            lookupTable[0xCE].assign("DEC", DEC, ABS, 6);

            lookupTable[0xD0].assign("BNE", BNE, REL, 2);
            lookupTable[0xD1].assign("CMP", CMP, IZY, 5);
            lookupTable[0xD5].assign("CMP", CMP, ZPX, 4);
            lookupTable[0xD6].assign("DEC", DEC, ZPX, 6);
            lookupTable[0xD8].assign("CLD", CLD, IMP, 2);
            lookupTable[0xD9].assign("CMP", CMP, ABY, 4);
            lookupTable[0xDD].assign("CMP", CMP, ABX, 4);
            lookupTable[0xDE].assign("DEC", DEC, ABX, 7);

            lookupTable[0xE0].assign("CPX", CPX, IMM, 2);
            lookupTable[0xE1].assign("SBC", SBC, IZX, 6);
            lookupTable[0xE4].assign("CPX", CPX, ZP0, 3);
            lookupTable[0xE5].assign("SBC", SBC, ZP0, 3);
            lookupTable[0xE6].assign("INC", INC, ZP0, 5);
            lookupTable[0xE8].assign("INX", INX, IMP, 2);
            lookupTable[0xE9].assign("SBC", SBC, IMM, 2);
            lookupTable[0xEA].assign("NOP", NOP, IMP, 2);
            lookupTable[0xEC].assign("CPX", CPX, ABS, 4);
            lookupTable[0xED].assign("SBC", SBC, ABS, 4);
            lookupTable[0xEE].assign("INC", INC, ABS, 6);

            lookupTable[0xF0].assign("BEQ", BEQ, REL, 2);
            lookupTable[0xF1].assign("SBC", SBC, IZY, 5);
            lookupTable[0xF5].assign("SBC", SBC, ZPX, 4);
            lookupTable[0xF6].assign("INC", INC, ZPX, 6);
            lookupTable[0xF8].assign("SED", SED, IMP, 2);
            lookupTable[0xF9].assign("SBC", SBC, ABY, 4);
            lookupTable[0xFD].assign("SBC", SBC, ABX, 4);
            lookupTable[0xFE].assign("INC", INC, ABX, 7);
        }
    }
}
