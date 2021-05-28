using System;
using System.IO;
using System.Windows.Forms;

namespace dotEmu.emulators.CHIP8
{
    class CHIP8Settings
    {
        public int RAMSize = 0x1000;
        public int startPos = 0x200;
        public int screenW = 64;
        public int screenH = 32;
        public int stackSize = 16;
    }

    partial class CHIP8CPU
    {
        public Byte[] RAM;
        public Byte[] V;
        public UInt16 PC;
        public Byte[] display;
        public UInt16[] stack;
        public Byte sp;
        public UInt16 I;
        public bool drawFlag = true;
        public CHIP8Settings settings = new CHIP8Settings();
        private CHIP8Emulator parent;

        public CHIP8CPU(CHIP8Emulator parentEMU)
        {
            parent = parentEMU;
            reset();
        }

        public void clock()
        {
            decodeOPCode((UInt16)((RAM[PC] << 8) | RAM[PC + 1]));
            PC += 2;
        }

        public void readROM(string path)
        {
            // Reset the emu state
            reset();

            // If rom exists
            if(File.Exists(path))
            {
                // ROM reader
                BinaryReader binReader = new BinaryReader(File.Open(path, FileMode.Open));

                // ROM is valid
                if (binReader.BaseStream.Length < settings.RAMSize - settings.startPos)
                    for (var i = 0; i < binReader.BaseStream.Length; i++)
                        RAM[settings.startPos + i] = binReader.ReadByte();
                else
                    MessageBox.Show("File is too big!\nMax length: " + (settings.RAMSize - settings.startPos) + "\nActual length: " + binReader.BaseStream.Length);

                // Close file
                binReader.Close();
            }
            else
                MessageBox.Show("The ROM you are trying to load has not been found.");
        }

        public void reset()
        {
            RAM = new Byte[settings.RAMSize];
            PC = (UInt16)settings.startPos;
            display = new Byte[settings.screenW * settings.screenH];
            stack = new UInt16[settings.stackSize];
            I = sp = 0;

            V = new Byte[0x10];
        }
    }
}
