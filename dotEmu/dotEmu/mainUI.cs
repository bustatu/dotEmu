using dotEmu.windows;
using System.Windows.Forms;

namespace dotEmu
{
    public partial class mainUI : Form
    {
        public mainUI()
        {
            InitializeComponent();
        }

        private void CHIP8Button_Click(object sender, System.EventArgs e)
        {
            CHIP8UI form = new CHIP8UI();
            Hide();
            form.ShowDialog();
            Show();
        }

        private void NESButton_Click(object sender, System.EventArgs e)
        {
            NESUI form = new NESUI();
            Hide();
            form.ShowDialog();
            Show();
        }
    }
}
