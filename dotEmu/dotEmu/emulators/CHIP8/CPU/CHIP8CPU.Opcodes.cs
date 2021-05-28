using System;

namespace dotEmu.emulators.CHIP8
{
    partial class CHIP8CPU
    {
        private void do1000(UInt16 opcode)
        {
            // 1NNN - Jump to adress NNN
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
                    gfxpos = (UInt16)((V[(opcode & 0xF00) >> 8] + xline) % settings.screenW + ((V[(opcode & 0xF0) >> 8] + yline) % settings.screenH) * settings.screenW);
                    if ((RAM[I + yline] & (0x80 >> xline)) != 0)
                    {
                        V[0xF] = (Byte)((display[gfxpos] != 0) ? 1 : V[0xF]);
                        display[gfxpos] ^= 1;
                    }
                }
            }
            drawFlag = true;
        }

        private void decodeOPCode(UInt16 opcode)
        {
            switch(opcode & 0xF000)
            {
                case 0x0000:
                    break;
                case 0x1000:
                    do1000(opcode);
                    break;
                case 0x3000:
                    do3000(opcode);
                    break;
                case 0x4000:
                    do4000(opcode);
                    break;
                case 0x6000:
                    do6000(opcode);
                    break;
                case 0x7000:
                    do7000(opcode);
                    break;
                case 0xA000:
                    doA000(opcode);
                    break;
                case 0xD000:
                    doD000(opcode);
                    break;
            }
        }
    }
}
