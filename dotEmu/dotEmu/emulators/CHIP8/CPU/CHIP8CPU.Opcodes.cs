using System;
using System.Linq;
using System.Windows.Forms;

namespace dotEmu.emulators.CHIP8
{
    partial class CHIP8CPU
    {
        private void do0000(UInt16 opcode)
        {
            // Multiple 0000 instructions
            switch (opcode & 0xFF)
            {
                case 0xE0:
                    // 00E0 - Clears screen
                    for (var i = 0; i < settings.screenH * settings.screenW; i++)
                        display[i] = 0;
                    break;
                case 0xEE:
                    // 00EE - Returns from subroutine
                    PC = stack[--sp];
                    break;
                default:
                    // Unknown opcode
                    MessageBox.Show("Unknown OPCode: " + opcode.ToString("X4"));
                    break;
            }
        }

        private void do1000(UInt16 opcode)
        {
            // 1NNN - Jump to adress NNN
            PC = (UInt16)((opcode & 0xFFF) - 2);
        }

        private void do2000(UInt16 opcode)
        {
            // 2NNN - Calls subroutine at adress NNN
            stack[sp++] = PC;
            PC = (UInt16)((opcode & 0xFFF) - 2);
        }

        private void do3000(UInt16 opcode)
        {
            // 3XNN - If Vx == NN then skip
            if (V[(opcode & 0xF00) >> 8] == (opcode & 0xFF))
                PC += 2;
        }

        private void do4000(UInt16 opcode)
        {
            // 4XNN - If Vx != NN then skip
            if (V[(opcode & 0xF00) >> 8] != (opcode & 0xFF))
                PC += 2;
        }

        private void do5000(UInt16 opcode)
        {
            // 5XY0 - If Vx == Vy then skip
            if (V[(opcode & 0xF00) >> 8] == V[(opcode & 0xF0) >> 4])
                PC += 2;
        }

        private void do6000(UInt16 opcode)
        {
            // 6XNN - Vx = NN
            V[(opcode & 0xF00) >> 8] = (Byte)(opcode & 0xFF);
        }

        private void do7000(UInt16 opcode)
        {
            // 7XNN - Vx += NN
            V[(opcode & 0xF00) >> 8] += (Byte)(opcode & 0xFF);
        }

        private void do8000(UInt16 opcode)
        {
            // Multiple 8000 instructions
            switch (opcode & 0xF)
            {
                case 0x0:
                    // 8XY0 - Vx = Vy
                    V[(opcode & 0xF00) >> 8] = V[(opcode & 0xF0) >> 4];
                    break;
                case 0x1:
                    // 8XY1 - Vx ^= Vy
                    V[(opcode & 0xF00) >> 8] |= V[(opcode & 0xF0) >> 4];
                    break;
                case 0x2:
                    // 8XY2 - Vx &= Vy
                    V[(opcode & 0xF00) >> 8] &= V[(opcode & 0xF0) >> 4];
                    break;
                case 0x3:
                    // 8XY3 - Vx ^= Vy
                    V[(opcode & 0xF00) >> 8] ^= V[(opcode & 0xF0) >> 4];
                    break;
                case 0x4:
                    // 8XY4 - Vx += Vy set carry flag if overflow
                    UInt16 result = (UInt16)(V[(opcode & 0xF00) >> 8] + V[(opcode & 0xF0) >> 4]);
                    V[(opcode & 0xF00) >> 8] = (Byte)(result & 0xFF);
                    V[0xF] = (Byte)((result & 0xF00) >> 8);
                    break;
                case 0x5:
                    // 8XY5 - Vx -= Vy set carry flag if borrow
                    V[0xF] = (Byte)((V[(opcode & 0xF00) >> 8] < V[(opcode & 0xF0) >> 4]) ? 1 : 0);
                    V[(opcode & 0xF00) >> 8] -= V[(opcode & 0xF0) >> 4];
                    break;
                case 0x6:
                    // 8XY6 - Vx >>= 1 set carry flag to least significant bit
                    V[0xF] = (Byte)(V[(opcode & 0xF00) >> 8] & 1);
                    V[(opcode & 0xF00) >> 8] >>= 1;
                    break;
                case 0x7:
                    // 8XY5 - Vx = Vy - Vx set carry flag if borrow
                    V[0xF] = (Byte)((V[(opcode & 0xF00) >> 8] < V[(opcode & 0xF0) >> 4]) ? 1 : 0);
                    V[(opcode & 0xF00) >> 8] = (Byte)(V[(opcode & 0xF0) >> 4] - V[(opcode & 0xF00) >> 8]);
                    break;
                case 0xE:
                    // 8XYE - Vx <<= 1 set carry flag to least significant bit
                    V[0xF] = (Byte)(V[(opcode & 0xF00) >> 8] & 0x10000000);
                    V[(opcode & 0xF00) >> 8] <<= 1;
                    break;
                default:
                    // Unknown opcode
                    MessageBox.Show("Unknown OPCode: " + opcode.ToString("X4"));
                    break;
            }
        }

        private void do9000(UInt16 opcode)
        {
            // 9XY0 - If Vx != Vy then skip
            if (V[(opcode & 0xF00) >> 8] != V[(opcode & 0xF0) >> 4])
                PC += 2;
        }

        private void doA000(UInt16 opcode)
        {
            // ANNN - Load I with constant NNN
            I = (UInt16)(opcode & 0x0FFF);
        }

        private void doD000(UInt16 opcode)
        {
            // DXYN - Draw sprite at position V[X] V[Y] with width N, set VF = collision
            // The drawing is done in XOR mode
            // Took from my C++ implementation
            UInt16 gfxpos = 0;
            V[0xF] = 0;
            for (int yline = 0; yline < (opcode & 0x000F); yline++)
            {
                for (int xline = 0; xline < 8; xline++)
                {
                    gfxpos = (UInt16)((V[(opcode & 0xF00) >> 8] + xline) % settings.screenW + ((V[(opcode & 0xF0) >> 4] + yline) % settings.screenH) * settings.screenW);
                    if ((RAM[I + yline] & (0x80 >> xline)) != 0)
                    {
                        V[0xF] = (Byte)((display[gfxpos] != 0) ? 1 : V[0xF]);
                        display[gfxpos] ^= 1;
                    }
                }
            }
            drawFlag = true;
        }

        private void doF000(UInt16 opcode)
        {
            // Multiple F000 instructions
            switch(opcode & 0xFF)
            {
                case 0x29:
                    // FX29 - Set I = location of sprite for digit Vx.
                    I = RAM[V[(opcode & 0xF00) >> 8] * 5];
                    break;
                case 0x33:
                    // FX33 - Store binary coded value of Vx in memory at position I
                    RAM[I + 2] = (Byte)(V[(opcode & 0xF00) >> 8] % 10);
                    RAM[I + 1] = (Byte)((V[(opcode & 0xF00) >> 8] / 10) % 10);
                    RAM[I] = (Byte)((V[(opcode & 0xF00) >> 8] / 100) % 10);
                    break;
                case 0x55:
                    // FX55 - Stores register from V0 to Vx in memory at position I
                    for(var i = 0; i <= (opcode & 0xF00) >> 8; i++)
                        RAM[I + i] = V[i];
                    break;
                case 0x65:
                    // FX65 - Reads register from V0 to Vx from memory at position I
                    for (var i = 0; i <= (opcode & 0xF00) >> 8; i++)
                        V[i] = RAM[I + i];
                    break;
                default:
                    // Unknown opcode
                    MessageBox.Show("Unknown OPCode: " + opcode.ToString("X4"));
                    break;
            }
        }

        private void decodeOPCode(UInt16 opcode)
        {

            switch(opcode & 0xF000)
            {
                case 0x0000:
                    do0000(opcode);
                    break;
                case 0x1000:
                    do1000(opcode);
                    break;
                case 0x2000:
                    do2000(opcode);
                    break;
                case 0x3000:
                    do3000(opcode);
                    break;
                case 0x4000:
                    do4000(opcode);
                    break;
                case 0x5000:
                    do5000(opcode);
                    break;
                case 0x6000:
                    do6000(opcode);
                    break;
                case 0x7000:
                    do7000(opcode);
                    break;
                case 0x8000:
                    do8000(opcode);
                    break;
                case 0x9000:
                    do9000(opcode);
                    break;
                case 0xA000:
                    doA000(opcode);
                    break;
                case 0xD000:
                    doD000(opcode);
                    break;
                case 0xF000:
                    doF000(opcode);
                    break;
                default:
                    {
                        // Unknown opcode
                        MessageBox.Show("Unknown OPCode: " + opcode.ToString("X4"));
                        break;
                    }
            }
        }
    }
}
