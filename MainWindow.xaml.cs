using System.Windows;
using System.Windows.Input;
using TryToG.Data;
using TryToG.Engine;

namespace TryToG;


public partial class MainWindow : Window
{
    private Reader Reader { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        Reader = new Reader("Data/Levels\\");
        
        Render.RenderMap(Reader, Canvas);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)    //Управление
    {
        Control.GetCommand(Reader, e.Key);

        if (!StatusCheck.CheckNextLvl(Reader)) Render.RenderMap(Reader, Canvas);
        StatusCheck.WinLose(Reader.Cells[Reader.Player.Coordinates.y, Reader.Player.Coordinates.x].Type, Reader);

        Render.RenderMap(Reader, Canvas);
    }
}