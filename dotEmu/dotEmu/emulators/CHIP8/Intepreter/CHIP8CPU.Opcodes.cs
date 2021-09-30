using System;
using System.Windows.Forms;

namespace dotEmu.emulators.CHIP8
{
    partial class CHIP8CPU
    {
        /// <returns>True if the pixel was set before and false if not.</returns>
        private bool xorPixel(int pos)
        {
            // Update pixel only in-bounds
            if (pos >= 0 && pos < settings.screenH * settings.screenW)
            {
                // Set draw flag
                drawFlag = true;

                // Update the pixel
                if (display[pos] == 1)
                {
                    display[pos] ^= 1;
                    return true;
                }
                else
                    display[pos] ^= 1;
            }
            return false;
        }

        private void decodeOPCode(UInt16 opcode)
        {
            switch(opcode & 0xF000)
            {
                // 0NNN - syscall
                case 0x0000:
                    switch(opcode)
                    {
                        // Clear screen
                        case 0x00E0:
                            display = new Byte[settings.screenW * settings.screenH];
                            drawFlag = true;
                            break;

                        // Return from function call
                        case 0x00EE:
                            PC = stack[stackPointer--];
                            break;
                        default:
                            throw new Exception("{E}: Opcode not implemented: " + opcode.ToString("X4"));
                    }
                    break;

                // 1NNN - Jump to adress NNN
                case 0x1000:
                    PC = (UInt16)(opcode & 0xFFF);
                    break;

                // 2NNN - Execute subroutine at adress NNN
                case 0x2000:
                    stack[++stackPointer] = PC;
                    PC = (UInt16)(opcode & 0xFFF);
                    break;

                // 3XNN - If Vx == NN then skip
                case 0x3000:
                    if (V[(opcode & 0xF00) >> 8] == (opcode & 0xFF))
                        PC += 2;
                    break;

                // 4XNN - If Vx != NN then skip
                case 0x4000:
                    if (V[(opcode & 0xF00) >> 8] != (opcode & 0xFF))
                        PC += 2;
                    break;

                // 5XYN - If Vx == Vy then skip
                case 0x5000:
                    if (V[(opcode & 0xF00) >> 8] == V[(opcode & 0xF0) >> 4])
                        PC += 2;
                    break;

                // 6XNN - Vx = NN
                case 0x6000:
                    V[(opcode & 0xF00) >> 8] = (Byte)(opcode & 0xFF);
                    break;

                // 7XNN - Vx += NN
                case 0x7000:
                    V[(opcode & 0xF00) >> 8] += (Byte)(opcode & 0xFF);
                    break;

                // 8XYN - Arithmetic instructions
                case 0x8000:
                    switch (opcode & 0xF)
                    {
                        // Vy = Vx
                        case 0x0:
                            V[(opcode & 0xF00) >> 8] = V[(opcode & 0xF0) >> 4];
                            break;
                        default:
                            throw new Exception("{E}: Opcode not implemented: " + opcode.ToString("X4"));
                    }
                    break;

                // 9XYN - If Vx != Vy then skip
                case 0x9000:
                    if (V[(opcode & 0xF00) >> 8] != V[(opcode & 0xF0) >> 4])
                        PC += 2;
                    break;

                // ANNN - Load I with constant NNN
                case 0xA000:
                    I = (UInt16)(opcode & 0x0FFF);
                    break;

                // DXYN - Draw sprite at position V[X] V[Y] with width N, set VF = collision
                case 0xD000:
                    // Sprite width offset
                    int offset = 1;

                    // Parameters
                    int va = V[(opcode >> 8) & 0xF];
                    int vb = V[(opcode >> 4) & 0xF];
                    int vc = (opcode >> 0) & 0xF;

                    //Set the F register to 0
                    V[0xF] = 0;

                    //Go trough the sprite lines
                    for (int yline = 0; yline < vc; yline++)
                    {
                        //Read pixel from memory
                        UInt16 pixel = (UInt16)((RAM[I + offset * yline] << 8) | RAM[I + offset * yline + 1]);

                        //Go trough the sprite columns
                        for (int xline = 0; xline < 8 * offset; xline++)
                            //If the pixel needs to be set
                            if ((pixel & ((1 << 15) >> xline)) != 0)
                                // Update the pixel and update V[0xF] if necessary
                                V[0xF] = xorPixel(va + xline + ((vb + yline) * settings.screenW)) ? (Byte)1 : V[0xF];
                    }
                    break;

                default:
                    throw new Exception("{E}: Opcode not implemented: " + opcode.ToString("X4"));
            }
        }
    }
}
