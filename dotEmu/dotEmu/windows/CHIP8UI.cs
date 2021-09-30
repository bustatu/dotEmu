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
        }

        private void softwareRenderer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                emu.reset();
                emu.loadROM(file);
            }
        }

        private void softwareRenderer_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
