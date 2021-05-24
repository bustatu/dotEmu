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
            emu.mainBus.cpu.clock();
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

        private void mainUI_Load(object sender, System.EventArgs e)
        {
            // Load ROM by hand
            emu.mainBus.ram[0x8000] = 0xA2;
            emu.mainBus.ram[0x8001] = 0x0A;
            emu.mainBus.ram[0x8002] = 0x8E;
            emu.mainBus.ram[0x8003] = 0x00;
            emu.mainBus.ram[0x8004] = 0x00;
            emu.mainBus.ram[0x8005] = 0xA2;
            emu.mainBus.ram[0x8006] = 0x03;
            emu.mainBus.ram[0x8007] = 0x8E;
            emu.mainBus.ram[0x8008] = 0x01;
            emu.mainBus.ram[0x8009] = 0x00;
            emu.mainBus.ram[0x800A] = 0xAC;
            emu.mainBus.ram[0x800B] = 0x00;
            emu.mainBus.ram[0x800C] = 0x00;
            emu.mainBus.ram[0x800D] = 0xA9;
            emu.mainBus.ram[0x800E] = 0x00;
            emu.mainBus.ram[0x800F] = 0x18;
            emu.mainBus.ram[0x8010] = 0x6D;
            emu.mainBus.ram[0x8011] = 0x01;
            emu.mainBus.ram[0x8012] = 0x00;
            emu.mainBus.ram[0x8013] = 0x88;
            emu.mainBus.ram[0x8014] = 0xD0;
            emu.mainBus.ram[0x8015] = 0xFA;
            emu.mainBus.ram[0x8016] = 0x8D;
            emu.mainBus.ram[0x8017] = 0x02;
            emu.mainBus.ram[0x8018] = 0x00;
            emu.mainBus.ram[0x8019] = 0xEA;
            emu.mainBus.ram[0x801A] = 0xEA;
            emu.mainBus.ram[0x801B] = 0xEA;

            // Set reset vector
            emu.mainBus.ram[0xFFFC] = 0x00;
            emu.mainBus.ram[0xFFFD] = 0x80;

            // Reset CPU
            emu.mainBus.cpu.reset();
        }
    }
}
