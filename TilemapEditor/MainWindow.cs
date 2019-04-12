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
        private int _viewOffsetX;
        private int _viewOffsetY;
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
            if (Texture == null)
                return;
            var zx = Texture.TileSizeX;
            var zy = Texture.TileSizeY;
            if (Tilemap != null)
            {
                var visibleX = Width / zx;
                var visibleY = Height / zy;
                for (var y = 0; y <= visibleY; y++)
                {
                    for (var x = 0; x <= visibleX; x++)
                    {
                        var tileX = x + _viewOffsetX;
                        var tileY = y + _viewOffsetY;
                        var pixelX = x * zx;
                        var pixelY = y * zy;
                        if (tileX < 0 || tileX >= Tilemap.GridSizeX || tileY < 0 || tileY >= Tilemap.GridSizeY)
                        {
                            e.Graphics.DrawRectangle(Pens.Black, pixelX, pixelY, zx, zy);
                            e.Graphics.FillRectangle(Brushes.Black, pixelX + 2, pixelY + 2, zy - 3, zy - 3);
                        }
                        else
                        {
                            var tile = Tilemap.GetValue(tileX, tileY);
                            if (tile == null)
                                e.Graphics.DrawRectangle(Pens.Blue, x * zx, y * zy, zx, zy);
                            else
                            {
                                var source = new Rectangle(zx * tile.Value, 0, zx, zy);
                                var destination = new Rectangle(x * zx, y * zy, zx, zy);
                                e.Graphics.DrawImage(Texture.Bitmap, destination, source, GraphicsUnit.Pixel);
                                e.Graphics.DrawRectangle(Pens.Black, x * zx, y * zy, zx, zy);
                            }
                        }
                    }
                }
            }
            var xp = 5;
            var yp = 5;
            for (var i = 0; i < Texture.Count; i++)
            {
                var source = new Rectangle(i * zx, 0, zx, zy);
                var destination = new Rectangle(xp, yp, zx, zy);
                e.Graphics.DrawImage(Texture.Bitmap, destination, source, GraphicsUnit.Pixel);
                e.Graphics.DrawRectangle(Pens.Black, xp, yp, zx - 2, zy - 2);
                if (i == CurrentTexture)
                {
                    e.Graphics.DrawRectangle(Pens.White, xp - 1, yp - 1, zx, zy);
                    e.Graphics.DrawRectangle(Pens.Black, xp - 2, yp - 2, zx + 2, zy + 2);
                }
                yp += zy + 5;
                if (yp < p.Height - zy)
                    continue;
                xp += zx + 5;
                yp = 5;
            }
        }

        private void p_MouseMove(object sender, MouseEventArgs e)
        {
            if (Texture == null || Tilemap == null)
                return;
            var x = (int)(e.X / (double)Texture.TileSizeX);
            var y = (int)(e.Y / (double)Texture.TileSizeX);
            x += _viewOffsetX;
            y += _viewOffsetY;
            if (x >= 0 && y >= 0 && x < Tilemap.GridSizeX && y < Tilemap.GridSizeY)
            {
                var tile = Tilemap.GetValue(x, y);
                lblStatus.Text = tile == null ? $@"X: {x}, Y: {y}" : $@"X: {x}, Y: {y}, Tile: {tile.Value}";
            }
            else
                lblStatus.Text = "";
        }

        private void p_Resize(object sender, EventArgs e) =>
            Refresh();

        private void p_MouseClick(object sender, MouseEventArgs e)
        {
            if (Texture == null || Tilemap == null)
                return;
            var zx = Texture.TileSizeX;
            var zy = Texture.TileSizeY;
            //Click on tile palette?
            var xp = 5;
            var yp = 5;
            for (var i = 0; i < Texture.Count; i++)
            {
                var destination = new Rectangle(xp, yp, zx, zy);
                if (destination.IntersectsWith(new Rectangle(e.X, e.Y, 1, 1)))
                {
                    CurrentTexture = i;
                    p.Invalidate();
                    return;
                }
                yp += zy + 5;
                if (yp < p.Height - zy)
                    continue;
                xp += zx + 5;
                yp = 5;
            }
            //Click on tile?
            var x = (int)(e.X / (double)Texture.TileSizeX);
            var y = (int)(e.Y / (double)Texture.TileSizeY);
            x += _viewOffsetX;
            y += _viewOffsetY;
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

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _viewOffsetX--;
                    p.Invalidate();
                    lblView.Text = $@"(View at {_viewOffsetX}, {_viewOffsetY})";
                    break;
                case Keys.Right:
                    _viewOffsetX++;
                    p.Invalidate();
                    lblView.Text = $@"(View at {_viewOffsetX}, {_viewOffsetY})";
                    break;
                case Keys.Up:
                    _viewOffsetY--;
                    p.Invalidate();
                    lblView.Text = $@"(View at {_viewOffsetX}, {_viewOffsetY})";
                    break;
                case Keys.Down:
                    _viewOffsetY++;
                    p.Invalidate();
                    lblView.Text = $@"(View at {_viewOffsetX}, {_viewOffsetY})";
                    break;
            }
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var x = new SizeGridDialog())
                if (x.ShowDialog(this) == DialogResult.OK)
                {
                    Tilemap.ResizeGrid(x.Width, x.Height);
                    Invalidate();
                }
        }
    }
}