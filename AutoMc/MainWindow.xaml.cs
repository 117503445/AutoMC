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
namespace AutoMc
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HotKey hotkey_upleft = new HotKey(ModifierKeys.Alt | ModifierKeys.Shift, System.Windows.Forms.Keys.E, this);
            hotkey_upleft.HotKeyPressed += HotKey_HotKeyPressed;
            HotKey hotkey_up = new HotKey(ModifierKeys.Alt, System.Windows.Forms.Keys.E, this);
            hotkey_up.HotKeyPressed += Key_HotKeyPressed;
            HotKey hotkey_left = new HotKey(ModifierKeys.Shift, System.Windows.Forms.Keys.E, this);
            hotkey_left.HotKeyPressed += Hot_HotKeyPressed;
            HotKey hotKey_right = new HotKey(ModifierKeys.Alt, System.Windows.Forms.Keys.W, this);
            hotKey_right.HotKeyPressed += HotKey_right_HotKeyPressed;

            #region 让当前窗体不获取输入焦点
            //WindowInteropHelper wndHelper = new WindowInteropHelper(this);
            //IntPtr HWND = wndHelper.Handle;
            //int GWL_EXSTYLE = -20;
            //SetWindowLong(HWND, GWL_EXSTYLE, (IntPtr)(0x8000000));
            #endregion

        }

        private void HotKey_right_HotKeyPressed(HotKey obj)
        {
            PostMessage(Handle, 0x204, 0, 1);
        }

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string ClassN, string WindN);
        private void Hot_HotKeyPressed(HotKey obj)
        {
            //IntPtr intPtr = (IntPtr)int.Parse(TbIntPtr.Text, System.Globalization.NumberStyles.HexNumber);
            //PostMessage(Handle, 0x0100, 0x57, 1);
            MouseSimulation.Down(MouseButton.Left);
        }

        private void Key_HotKeyPressed(HotKey obj)
        {
            //IntPtr intPtr = (IntPtr)int.Parse(TbIntPtr.Text, System.Globalization.NumberStyles.HexNumber);
            PostMessage(Handle, 0x201, 0, 1);
        }
        public IntPtr Handle
        {
            get
            {
                return FindWindow(null, "Minecraft 1.12.2");
            }
        }
        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        private void HotKey_HotKeyPressed(HotKey obj)
        {
            //IntPtr intPtr = (IntPtr)int.Parse(TbIntPtr.Text, System.Globalization.NumberStyles.HexNumber);

            PostMessage(Handle, 0x0100, 0x57, 1);
            PostMessage(Handle, 0x201, 0, 1);
            //Console.WriteLine(1);
            //await Task.Delay(1000);
            //MouseSimulation.Down(MouseButton.Left);
            //KeyboardSimulation.Press(Key.W);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SendMessage(htextbox, ControlDLL.WM_LBUTTONDOWN, IntPtr.Zero, null);
            //IntPtr intPtr = (IntPtr)int.Parse(TbIntPtr.Text, System.Globalization.NumberStyles.HexNumber);
            PostMessage(Handle, 0x0100, 0x57, 1);
            PostMessage(Handle, 0x201, 0, 1);
        }
    }
}
