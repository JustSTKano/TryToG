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

        //ReadMap();
    }


    private void Window_KeyDown(object sender, KeyEventArgs e)    //Управление
    {
        Movement.Move(Reader, e.Key);


        Render.RenderMap(Reader, Canvas);
        Reader.WinLose(Reader.Cell[Reader.Player.Coordinates.y, Reader.Player.Coordinates.x].Type);
        Render.RenderMap(Reader, Canvas);
    }



   

   
}