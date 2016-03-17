using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paint.UndoRedo.Model;
using Paint.UndoRedo;

namespace Paint2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMementoCaretaker _undoRedoCaretaker;

        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, OnExecutedCommands));
            paintWorkspace.MouseUp += new MouseButtonEventHandler(paintWorkspace_MouseUp);

            var mementoDesigner = new InkCanvasMementoDesigner(paintWorkspace);
            _undoRedoCaretaker = new UndoRedoCaretaker(mementoDesigner);
            _undoRedoCaretaker.Initialize();
        }

        void paintWorkspace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _undoRedoCaretaker.StoreState();
            }
        }

        private void OnExecutedCommands(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Undo)
            {
                _undoRedoCaretaker.Undo();
            }
        }
    }
}
