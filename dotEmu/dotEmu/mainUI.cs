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
            MemoryLabel.Text = "Memory: \n";
            for(var i = 0; i <= 0xF; i++)
            {
                MemoryLabel.Text += "$00" + i.ToString("X1") + "0: ";
                for (int j = 0; j <= 0xF; j++)
                    MemoryLabel.Text += emu.mainBus.ram[(i << 8) | j].ToString("X2") + " ";
                MemoryLabel.Text += "\n";
            }
            MemoryLabel1.Text = "Memory: \n";
            for (var i = 0; i <= 0xF; i++)
            {
                MemoryLabel1.Text += "$80" + i.ToString("X1") + "0: ";
                for (int j = 0; j <= 0xF; j++)
                    MemoryLabel1.Text += emu.mainBus.ram[0x8000 |((i << 8) | j)].ToString("X2") + " ";
                MemoryLabel1.Text += "\n";
            }

            dissLabel.Text = "Dissasembly: \n";
            for (int j = -0xA; j <= 0xA; j++)
                dissLabel.Text += "$" + (emu.mainBus.cpu.pc + j).ToString("X4") + ": " + emu.mainBus.cpu.lookupTable[emu.mainBus.ram[emu.mainBus.cpu.pc + j]].name + "\n";

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

            // Set reset vector
            emu.mainBus.ram[0xFFFC] = 0x00;
            emu.mainBus.ram[0xFFFD] = 0x80;

            // Reset CPU
            emu.mainBus.cpu.reset();
        }
    }
}
