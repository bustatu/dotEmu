using dotEmu.emulators.CHIP8;
using System;
using System.Windows.Forms;

namespace dotEmu.windows
{
    public partial class CHIP8UI : Form
    {
        CHIP8Emulator emu;

        public CHIP8UI()
        {
            emu = new CHIP8Emulator(softwareRenderer);
            InitializeComponent();
        }

        private void CHIP8UI_FormClosed(object sender, FormClosedEventArgs e)
        {
            emu.exit();
        }

        private void CHIP8UI_Load(object sender, System.EventArgs e)
        {

        }

        private void CHIP8UI_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                try
                {
                    emu.loadROM(files[0]);
                    emu.start();
                    AllowDrop = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("Error loading ROM file; either corrupt or unsupported");
                }
            }
        }

        private void CHIP8UI_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
