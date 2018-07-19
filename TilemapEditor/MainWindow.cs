using System;
using System.IO;
using System.Windows.Forms;

namespace TilemapEditor
{
    public partial class MainWindow : Form
    {
        private string _filename;
        private bool _changed;
        public MainWindow()
        {
            InitializeComponent();
        }
        public EditableTilemap Tilemap { get; set; }
        public string Filename
        {
            get
            {
                CheckFilename();
                return _filename;
            }
            set
            {
                _filename = value;
                CheckFilename();
            }
        }
        public bool Changed
        {
            get => _changed;
            set
            {
                _changed = value;
                CheckFilename();
            }
        }
        private void CheckFilename()
        {
            if (!string.IsNullOrWhiteSpace(_filename))
                return;
            _filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "newmap.tilemap");
            Text = $@"RetroGame Tile editor - {_filename}{(_changed ? "*" : "")}";
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            Tilemap = new EditableTilemap();
            CheckFilename();
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) => Close();
        private void p_Paint(object sender, PaintEventArgs e)
        {

        }
        private void p_MouseMove(object sender, MouseEventArgs e) => UpdateStatus();
        private void p_Resize(object sender, EventArgs e)
        {
            Invalidate();
            UpdateStatus();
        }
        private void UpdateStatus()
        {

        }
    }
}
