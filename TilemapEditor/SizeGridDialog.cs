using System.ComponentModel;

namespace TilemapEditor;

public partial class SizeGridDialog : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentGridSizeX { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentGridSizeY { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int NewGridSizeX { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int NewGridSizeY { get; set; }

    public SizeGridDialog()
    {
        InitializeComponent();
    }

    private void SizeGridDialog_Load(object sender, EventArgs e)
    {

    }

    private void TxtWidth_Validating(object sender, CancelEventArgs e)
    {

    }

    private void TxtHeight_Validating(object sender, CancelEventArgs e)
    {

    }

    private void BtnOk_Click(object sender, EventArgs e)
    {

    }
}