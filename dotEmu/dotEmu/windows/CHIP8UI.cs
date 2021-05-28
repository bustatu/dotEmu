using dotEmu.emulators.CHIP8;
using System.Windows.Forms;

namespace dotEmu.windows
{
    public partial class CHIP8UI : Form
    {
        CHIP8Emulator emu;

        public CHIP8UI()
        {
            InitializeComponent();
            emu = new CHIP8Emulator(softwareRenderer);
            emu.loadROM("roms/test_opcode.ch8");

            dissLabel.Text = "Dissasembly:\n";
            for (int i = -2; i <= 12; i += 2)
            {
                dissLabel.Text += "$" + (emu.cpu.PC + i).ToString("X2") + ": ";
                dissLabel.Text += ((emu.cpu.RAM[emu.cpu.PC + i] << 8) | emu.cpu.RAM[emu.cpu.PC + i + 1]).ToString("X4") + "\n";
            }

            ILabel.Text = "I: " + (emu.cpu.I).ToString("X3");

            RegisterLabel.Text = "Registers: \n";
            for(var i = 0; i <= 0xF; i++)
            {
                RegisterLabel.Text += "V[0x" + i.ToString("X1") + "]: " + emu.cpu.V[i].ToString("X2") + '\n';
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            emu.clock();

            dissLabel.Text = "Dissasembly:\n";
            for(int i = -2; i <= 12; i+=2)
            {
                dissLabel.Text += "$" + (emu.cpu.PC + i).ToString("X2") + ": ";
                dissLabel.Text += ((emu.cpu.RAM[emu.cpu.PC + i] << 8) | emu.cpu.RAM[emu.cpu.PC + i + 1]).ToString("X4") + "\n";
            }

            ILabel.Text = "I: " + (emu.cpu.I).ToString("X3");

            RegisterLabel.Text = "Registers: \n";
            for (var i = 0; i <= 0xF; i++)
            {
                RegisterLabel.Text += "V[0x" + i.ToString("X1") + "]: " + emu.cpu.V[i].ToString("X2") + '\n';
            }
        }
    }
}
