using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TilemapEditor
{
    public partial class MainWindow : Form
    {
        private string _filename;
        private bool _changed;
        private int CurrentTexture { get; set; }
        public EditableTilemap Tilemap { get; set; }
        public Texture Texture { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

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
            Tilemap = new EditableTilemap(8, 8);
            Texture = new Texture(24, 24);
            CheckFilename();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void p_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Green);
            if (Texture == null || Tilemap == null)
                return;
            for (var y = 0; y < Tilemap.GridSizeY; y++)
            {
                for (var x = 0; x < Tilemap.GridSizeX; x++)
                {
                    var tile = Tilemap.GetValue(x, y);
                    if (tile == null)
                        e.Graphics.DrawRectangle(Pens.Blue, x * Texture.TileSizeX, y * Texture.TileSizeY, Texture.TileSizeX, Texture.TileSizeY);
                    else
                    {
                        var source = new Rectangle(Texture.TileSizeX * tile.Value, 0, Texture.TileSizeX, Texture.TileSizeY);
                        var destination = new Rectangle(x * Texture.TileSizeX, y * Texture.TileSizeY, Texture.TileSizeX, Texture.TileSizeY);
                        e.Graphics.DrawImage(Texture.Bitmap, destination, source, GraphicsUnit.Pixel);
                        e.Graphics.DrawRectangle(Pens.Black, x * Texture.TileSizeX, y * Texture.TileSizeY, Texture.TileSizeX, Texture.TileSizeY);
                    }
                }
            }
        }

        private void p_MouseMove(object sender, MouseEventArgs e)
        {
            if (Texture == null || Tilemap == null)
                return;
            var x = (int)(e.X / (double)Texture.TileSizeX);
            var y = (int)(e.Y / (double)Texture.TileSizeX);
            if (x >= 0 && y >= 0 && x < Tilemap.GridSizeX && y < Tilemap.GridSizeY)
            {
                var tile = Tilemap.GetValue(x, y);
                lblStatus.Text = tile == null ? $@"X: {x}, Y: {y}" : $@"X: {x}, Y: {y}, Tile: {tile.Value}";
            }
            else
                lblStatus.Text = "";
        }

        private void p_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void p_MouseClick(object sender, MouseEventArgs e)
        {
            if (Texture == null || Tilemap == null)
                return;
            var x = (int)(e.X / (double)Texture.TileSizeX);
            var y = (int)(e.Y / (double)Texture.TileSizeX);
            if (x < 0 || y < 0 || x >= Tilemap.GridSizeX || y >= Tilemap.GridSizeY)
                return;
            if ((e.Button & MouseButtons.Left) > 0)
                Tilemap.SetValue(x, y, CurrentTexture);
            else if ((e.Button & MouseButtons.Right) > 0)
                Tilemap.SetValue(x, y, null);
            var tile = Tilemap.GetValue(x, y);
            lblStatus.Text = tile == null ? $@"X: {x}, Y: {y}" : $@"X: {x}, Y: {y}, Tile: {tile.Value}";
            p.Invalidate();
        }
    }
}