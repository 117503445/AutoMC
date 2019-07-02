using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TLib.Windows;
using Newtonsoft.Json;
namespace AutoMc
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window, IDisposable
    {
        List<KeyAction> keyActions=new List<KeyAction>();
        public MainWindow()
        {
            InitializeComponent();
            var keyAction = new KeyAction(ModifierKeys.Control, System.Windows.Forms.Keys.P, this, "de", new List<Key>() { Key.W }, new List<MouseButton>() {MouseButton.Left});
            string json = JsonConvert.SerializeObject(keyAction);
            Console.WriteLine(json);
            keyActions.Add(keyAction);

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //keyAction.Dispose();
        }


        private void CboIsTopMost_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (bool)CboIsTopMost.IsChecked;
        }

        public void Dispose()
        {
            //keyAction?.Dispose();
        }
    }
}
