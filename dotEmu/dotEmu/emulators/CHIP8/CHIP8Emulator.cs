using dotEmu.renderer;
using System.IO;
using System.Windows.Forms;

namespace dotEmu.emulators.CHIP8
{
    class CHIP8Emulator : IEmulator
    {
        public string Name => "CHIP8";

        public CHIP8CPU cpu;
        public SoftwareRenderer renderer;
        

        public void loadROM(string path)
        {
            // Pass to the CPU
            cpu.readROM(path);
        }

        public void clock()
        {
            cpu.clock();

            if(cpu.drawFlag)
            {
                for(var i = 0; i < cpu.settings.screenH; i++)
                {
                    for(var j = 0; j < cpu.settings.screenW; j++)
                    {
                        if (cpu.display[i * cpu.settings.screenW + j] != 0)
                            renderer.setPixel(j + 1, i + 1, 0xFF, 0xFF, 0xFF);
                        else renderer.setPixel(j + 1, i + 1, 0x00, 0x00, 0x00);
                    }
                }
                renderer.update();
                cpu.drawFlag = false;
            }
        }

        public CHIP8Emulator(SoftwareRenderer rendererToUse)
        {
            // Init
            cpu = new CHIP8CPU(this);
            renderer = rendererToUse;
        }
    }
}
