using dotEmu.emulators;
using dotEmu.emulators.NES;
using System.Drawing;
using System.Windows.Forms;

namespace dotEmu
{
    public partial class mainUI : Form
    {
        NESEmulator emu = new NESEmulator();

        public mainUI()
        {
            InitializeComponent();
        }

        private void doStepButton_Click(object sender, System.EventArgs e)
        {
            while(emu.mainBus.cpu.cycles != 0) emu.mainBus.cpu.clock();

            clockLabel.Text = "Clock: " + emu.mainBus.cpu.cycles.ToString();
            PCLabel.Text = "PC: " + emu.mainBus.cpu.pc.ToString("X4");
            SPLabel.Text = "SP: " + emu.mainBus.cpu.sp.ToString("X4");
            ALabel.Text = "A: " + emu.mainBus.cpu.a.ToString("X4");
            XLabel.Text = "X: " + emu.mainBus.cpu.x.ToString("X4");
            YLabel.Text = "Y: " + emu.mainBus.cpu.y.ToString("X4");

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.N) == 0)
                NLabel.ForeColor = Color.Red;
            else
                NLabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.V) == 0)
                VLabel.ForeColor = Color.Red;
            else
                VLabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.U) == 0)
                ULabel.ForeColor = Color.Red;
            else
                ULabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.B) == 0)
                BLabel.ForeColor = Color.Red;
            else
                BLabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.D) == 0)
                DLabel.ForeColor = Color.Red;
            else
                DLabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.I) == 0)
                ILabel.ForeColor = Color.Red;
            else
                ILabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.Z) == 0)
                ZLabel.ForeColor = Color.Red;
            else
                ZLabel.ForeColor = Color.Green;

            if (emu.mainBus.cpu.getFlag(CPU6502.CPUFlags.C) == 0)
                CLabel.ForeColor = Color.Red;
            else
                CLabel.ForeColor = Color.Green;
        }
    }
}
