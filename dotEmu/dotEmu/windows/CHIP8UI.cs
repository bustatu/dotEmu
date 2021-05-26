using dotEmu.emulators.CHIP8;
using System.Windows.Forms;

namespace dotEmu.windows
{
    public partial class CHIP8UI : Form
    {
        CHIP8Emulator emu = new CHIP8Emulator();

        public CHIP8UI()
        {
            InitializeComponent();
            emu.loadROM("roms/test_opcode.ch8");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            emu.clock();

            dissLabel.Text = "Dissasembly:\n";
            for(int i = 0; i <= 12; i+=2)
            {
                dissLabel.Text += "$" + (emu.cpu.PC + i).ToString("X2") + ": ";
                dissLabel.Text += ((emu.cpu.RAM[emu.cpu.PC + i + 1] << 8) | emu.cpu.RAM[emu.cpu.PC + i]).ToString("X4") + "\n";
            }
        }
    }
}
