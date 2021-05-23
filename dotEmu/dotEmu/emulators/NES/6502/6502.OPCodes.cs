using System;
using System.Windows.Forms;

/* Resources used:
 * http://archive.6502.org/datasheets/rockwell_r650x_r651x.pdf
 * https://www.youtube.com/watch?v=8XmxKPJDGU0
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
            return 0;
        }
        public Byte CMP()
        {
            return 0;
        }
        public Byte CPX()
        {
            return 0;
        }
        public Byte CPY()
        {
            return 0;
        }
        public Byte DEC()
        {
            return 0;
        }
        public Byte DEX()
        {
            return 0;
        }
        public Byte DEY()
        {
            return 0;
        }
        public Byte EOR()
        {
            return 0;
        }
        public Byte INC()
        {
            return 0;
        }
        public Byte INX()
        {
            return 0;
        }
        public Byte INY()
        {
            return 0;
        }
        public Byte JMP()
        {
            return 0;
        }
        public Byte JSR()
        {
            return 0;
        }
        public Byte LDA()
        {
            return 0;
        }
        public Byte LDX()
        {
            // Load value in X
            fetch();

            x = fetched;

            // Set flags
            setFlag(CPUFlags.Z, x == 0);
            setFlag(CPUFlags.N, (x & 0x80) != 0);

            return 0;
        }
        public Byte LDY()
        {
            return 0;
        }
        public Byte LSR()
        {
            return 0;
        }
        public Byte NOP()
        {
            return 0;
        }
        public Byte ORA()
        {
            return 0;
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
            return 0;
        }
        public Byte PLA()
        {
            // Pop from stack
            sp++;
            a = read((UInt16)(0x100 + sp));

            // Set negative and zero flags
            setFlag(CPUFlags.Z, a == 0);
            setFlag(CPUFlags.N, (a & 0x80) != 0);

            return 0;
        }
        public Byte PLP()
        {
            return 0;
        }
        public Byte ROL()
        {
            return 0;
        }
        public Byte ROR()
        {
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
            return 0;
        }
        public Byte SED()
        {
            return 0;
        }
        public Byte SEI()
        {
            return 0;
        }
        public Byte STA()
        {
            return 0;
        }
        public Byte STX()
        {
            return 0;
        }
        public Byte STY()
        {
            return 0;
        }
        public Byte TAX()
        {
            return 0;
        }
        public Byte TAY()
        {
            return 0;
        }
        public Byte TSX()
        {
            return 0;
        }
        public Byte TXA()
        {
            return 0;
        }
        public Byte TXS()
        {
            return 0;
        }
        public Byte TYA()
        {
            return 0;
        }
        public Byte XXX()
        {
            return 0;
        }
        public void clock()
        {
            // If we can execute the opcode
            if(cycles == 0)
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
            fetched  = 0;

            // Reset takes time
            cycles = 8;
        }
        public void irq()
        {
            // If intrerupts are not disabled
            if(getFlag(CPUFlags.I) == 0)
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
            lookupTable[0x17].assign("CLC", CLC, IMP, 2);
            lookupTable[0x18].assign("ORA", ORA, ABY, 4);
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

            lookupTable[0x81].assign("STA", STA, IZX, 6);
            lookupTable[0x84].assign("STY", STY, ZP0, 3);
            lookupTable[0x85].assign("STA", STA, ZP0, 3);
            lookupTable[0x86].assign("STX", STX, ZP0, 3);
            lookupTable[0x88].assign("DEY", DEY, IMP, 2);
            lookupTable[0x8A].assign("TXA", TXA, IMP, 2);
            lookupTable[0x8C].assign("STY", STY, ABS, 4);
            lookupTable[0x8D].assign("STA", STA, ABS, 4);
            lookupTable[0x8E].assign("STX", STX, ABS, 4);

            lookupTable[0xA0].assign("LDY", LDY, IMM, 2);
            lookupTable[0xA1].assign("LDA", LDA, IZX, 6);
            lookupTable[0xA2].assign("LDX", LDX, IMM, 2);
            lookupTable[0xA4].assign("LDY", LDY, ZP0, 3);

            /*{ "BMI", &a::BMI, &a::REL, 2 },{ "AND", &a::AND, &a::IZY, 5 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 4 },{ "AND", &a::AND, &a::ZPX, 4 },{ "ROL", &a::ROL, &a::ZPX, 6 },{ "???", &a::XXX, &a::IMP, 6 },{ "SEC", &a::SEC, &a::IMP, 2 },{ "AND", &a::AND, &a::ABY, 4 },{ "???", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 7 },{ "???", &a::NOP, &a::IMP, 4 },{ "AND", &a::AND, &a::ABX, 4 },{ "ROL", &a::ROL, &a::ABX, 7 },{ "???", &a::XXX, &a::IMP, 7 },
		    { "RTI", &a::RTI, &a::IMP, 6 },{ "EOR", &a::EOR, &a::IZX, 6 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 3 },{ "EOR", &a::EOR, &a::ZP0, 3 },{ "LSR", &a::LSR, &a::ZP0, 5 },{ "???", &a::XXX, &a::IMP, 5 },{ "PHA", &a::PHA, &a::IMP, 3 },{ "EOR", &a::EOR, &a::IMM, 2 },{ "LSR", &a::LSR, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 2 },{ "JMP", &a::JMP, &a::ABS, 3 },{ "EOR", &a::EOR, &a::ABS, 4 },{ "LSR", &a::LSR, &a::ABS, 6 },{ "???", &a::XXX, &a::IMP, 6 },
		    { "BVC", &a::BVC, &a::REL, 2 },{ "EOR", &a::EOR, &a::IZY, 5 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 4 },{ "EOR", &a::EOR, &a::ZPX, 4 },{ "LSR", &a::LSR, &a::ZPX, 6 },{ "???", &a::XXX, &a::IMP, 6 },{ "CLI", &a::CLI, &a::IMP, 2 },{ "EOR", &a::EOR, &a::ABY, 4 },{ "???", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 7 },{ "???", &a::NOP, &a::IMP, 4 },{ "EOR", &a::EOR, &a::ABX, 4 },{ "LSR", &a::LSR, &a::ABX, 7 },{ "???", &a::XXX, &a::IMP, 7 },
		    { "RTS", &a::RTS, &a::IMP, 6 },{ "ADC", &a::ADC, &a::IZX, 6 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 3 },{ "ADC", &a::ADC, &a::ZP0, 3 },{ "ROR", &a::ROR, &a::ZP0, 5 },{ "???", &a::XXX, &a::IMP, 5 },{ "PLA", &a::PLA, &a::IMP, 4 },{ "ADC", &a::ADC, &a::IMM, 2 },{ "ROR", &a::ROR, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 2 },{ "JMP", &a::JMP, &a::IND, 5 },{ "ADC", &a::ADC, &a::ABS, 4 },{ "ROR", &a::ROR, &a::ABS, 6 },{ "???", &a::XXX, &a::IMP, 6 },
		    { "BVS", &a::BVS, &a::REL, 2 },{ "ADC", &a::ADC, &a::IZY, 5 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 4 },{ "ADC", &a::ADC, &a::ZPX, 4 },{ "ROR", &a::ROR, &a::ZPX, 6 },{ "???", &a::XXX, &a::IMP, 6 },{ "SEI", &a::SEI, &a::IMP, 2 },{ "ADC", &a::ADC, &a::ABY, 4 },{ "???", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 7 },{ "???", &a::NOP, &a::IMP, 4 },{ "ADC", &a::ADC, &a::ABX, 4 },{ "ROR", &a::ROR, &a::ABX, 7 },{ "???", &a::XXX, &a::IMP, 7 },

		    { "BCC", &a::BCC, &a::REL, 2 },{ "STA", &a::STA, &a::IZY, 6 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 6 },{ "STY", &a::STY, &a::ZPX, 4 },{ "STA", &a::STA, &a::ZPX, 4 },{ "STX", &a::STX, &a::ZPY, 4 },{ "???", &a::XXX, &a::IMP, 4 },{ "TYA", &a::TYA, &a::IMP, 2 },{ "STA", &a::STA, &a::ABY, 5 },{ "TXS", &a::TXS, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 5 },{ "???", &a::NOP, &a::IMP, 5 },{ "STA", &a::STA, &a::ABX, 5 },{ "???", &a::XXX, &a::IMP, 5 },{ "???", &a::XXX, &a::IMP, 5 },
		    { "LDA", &a::LDA, &a::ZP0, 3 },{ "LDX", &a::LDX, &a::ZP0, 3 },{ "???", &a::XXX, &a::IMP, 3 },{ "TAY", &a::TAY, &a::IMP, 2 },{ "LDA", &a::LDA, &a::IMM, 2 },{ "TAX", &a::TAX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 2 },{ "LDY", &a::LDY, &a::ABS, 4 },{ "LDA", &a::LDA, &a::ABS, 4 },{ "LDX", &a::LDX, &a::ABS, 4 },{ "???", &a::XXX, &a::IMP, 4 },
		    { "BCS", &a::BCS, &a::REL, 2 },{ "LDA", &a::LDA, &a::IZY, 5 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 5 },{ "LDY", &a::LDY, &a::ZPX, 4 },{ "LDA", &a::LDA, &a::ZPX, 4 },{ "LDX", &a::LDX, &a::ZPY, 4 },{ "???", &a::XXX, &a::IMP, 4 },{ "CLV", &a::CLV, &a::IMP, 2 },{ "LDA", &a::LDA, &a::ABY, 4 },{ "TSX", &a::TSX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 4 },{ "LDY", &a::LDY, &a::ABX, 4 },{ "LDA", &a::LDA, &a::ABX, 4 },{ "LDX", &a::LDX, &a::ABY, 4 },{ "???", &a::XXX, &a::IMP, 4 },
		    { "CPY", &a::CPY, &a::IMM, 2 },{ "CMP", &a::CMP, &a::IZX, 6 },{ "???", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "CPY", &a::CPY, &a::ZP0, 3 },{ "CMP", &a::CMP, &a::ZP0, 3 },{ "DEC", &a::DEC, &a::ZP0, 5 },{ "???", &a::XXX, &a::IMP, 5 },{ "INY", &a::INY, &a::IMP, 2 },{ "CMP", &a::CMP, &a::IMM, 2 },{ "DEX", &a::DEX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 2 },{ "CPY", &a::CPY, &a::ABS, 4 },{ "CMP", &a::CMP, &a::ABS, 4 },{ "DEC", &a::DEC, &a::ABS, 6 },{ "???", &a::XXX, &a::IMP, 6 },
		    { "BNE", &a::BNE, &a::REL, 2 },{ "CMP", &a::CMP, &a::IZY, 5 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 4 },{ "CMP", &a::CMP, &a::ZPX, 4 },{ "DEC", &a::DEC, &a::ZPX, 6 },{ "???", &a::XXX, &a::IMP, 6 },{ "CLD", &a::CLD, &a::IMP, 2 },{ "CMP", &a::CMP, &a::ABY, 4 },{ "NOP", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 7 },{ "???", &a::NOP, &a::IMP, 4 },{ "CMP", &a::CMP, &a::ABX, 4 },{ "DEC", &a::DEC, &a::ABX, 7 },{ "???", &a::XXX, &a::IMP, 7 },
		    { "CPX", &a::CPX, &a::IMM, 2 },{ "SBC", &a::SBC, &a::IZX, 6 },{ "???", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "CPX", &a::CPX, &a::ZP0, 3 },{ "SBC", &a::SBC, &a::ZP0, 3 },{ "INC", &a::INC, &a::ZP0, 5 },{ "???", &a::XXX, &a::IMP, 5 },{ "INX", &a::INX, &a::IMP, 2 },{ "SBC", &a::SBC, &a::IMM, 2 },{ "NOP", &a::NOP, &a::IMP, 2 },{ "???", &a::SBC, &a::IMP, 2 },{ "CPX", &a::CPX, &a::ABS, 4 },{ "SBC", &a::SBC, &a::ABS, 4 },{ "INC", &a::INC, &a::ABS, 6 },{ "???", &a::XXX, &a::IMP, 6 },
		    { "BEQ", &a::BEQ, &a::REL, 2 },{ "SBC", &a::SBC, &a::IZY, 5 },{ "???", &a::XXX, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 8 },{ "???", &a::NOP, &a::IMP, 4 },{ "SBC", &a::SBC, &a::ZPX, 4 },{ "INC", &a::INC, &a::ZPX, 6 },{ "???", &a::XXX, &a::IMP, 6 },{ "SED", &a::SED, &a::IMP, 2 },{ "SBC", &a::SBC, &a::ABY, 4 },{ "NOP", &a::NOP, &a::IMP, 2 },{ "???", &a::XXX, &a::IMP, 7 },{ "???", &a::NOP, &a::IMP, 4 },{ "SBC", &a::SBC, &a::ABX, 4 },{ "INC", &a::INC, &a::ABX, 7 },{ "???", &a::XXX, &a::IMP, 7 },*/
        }
    }
}
