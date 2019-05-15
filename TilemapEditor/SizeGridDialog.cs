using System.Windows.Forms;

namespace TilemapEditor
{
    public partial class SizeGridDialog : Form
    {
        public int CurrentGridSizeX { get; set; }
        public int CurrentGridSizeY { get; set; }
        public int NewGridSizeX { get; set; }
        public int NewGridSizeY { get; set; }

        public SizeGridDialog()
        {
            InitializeComponent();
        }

        private void SizeGridDialog_Load(object sender, System.EventArgs e)
        {

        }

        private void TxtWidth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void TxtHeight_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {

        }
    }
}