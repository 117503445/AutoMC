using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using TLib.Windows;
using WF = System.Windows.Forms;
namespace AutoMc
{
    /// <summary>
    /// 表示一个按键动作 (一直按左键,一直按w键)
    /// </summary>
    public sealed class KeyAction : IDisposable
    {
        /// <summary>
        /// 对这次按键动作的文本描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 按下热键后 要一直按住的键盘键值
        /// </summary>
        private List<Key> PressedKey { get; }
        /// <summary>
        /// 按下热键后 要一直按住的鼠标键值
        /// </summary>
        private List<MouseButton> PressedMouse { get; }
        /// <summary>
        /// 返回当前热键是否已经成功注册
        /// </summary>
        [Newtonsoft.Json.JsonIgnore()]
        public bool IsKeyRegistered
        {
            get
            {
                try
                {
                    return hotKey.IsKeyRegistered;
                }
                catch (NullReferenceException)
                {

                    return false;
                }
            }
        }
        /// <summary>
        /// 私有热键对象
        /// </summary>
        [Newtonsoft.Json.JsonIgnore()]
        private readonly HotKey hotKey;
        /// <summary>
        /// 用于模拟一直按下按键
        /// </summary>
        [Newtonsoft.Json.JsonIgnore()]
        private readonly DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(10),
            IsEnabled = false,
        };
        /// <summary>
        /// 表示一个按键动作 (一直按左键,一直按w键)
        /// </summary>
        /// <param name="hotkeyModifierKeys">标识按键 如 Ctrl Shift,支持 ModifierKeys.Ctrl | ModifierKeys.Shift</param>
        /// <param name="hotkeyNormalKeys">普通键盘按键,如 C</param>
        /// <param name="window">传入this即可</param>
        /// <param name="describe">对这次按键动作的文本描述</param>
        /// <param name="pressedKey">按下热键后 要一直按住的键盘键值</param>
        /// <param name="pressedMouse">按下热键后 要一直按住的鼠标键值</param>
        public KeyAction(ModifierKeys hotkeyModifierKeys, WF.Keys hotkeyNormalKeys, Window window, string describe, List<Key> pressedKey, List<MouseButton> pressedMouse)
        {
            hotKey = new HotKey(hotkeyModifierKeys, hotkeyNormalKeys, window);
            hotKey.HotKeyPressed += HotKey_HotKeyPressed;
            Describe = describe;
            if (pressedKey == null)
            {
                pressedKey = new List<Key>();
            }
            if (pressedMouse == null)
            {
                pressedMouse = new List<MouseButton>();
            }
            PressedKey = pressedKey;
            PressedMouse = pressedMouse;


            foreach (var key in PressedKey)
            {
                KeyboardSimulation.Reset();
                timer.Tag = key;
            }
            foreach (var mouse in PressedMouse)
            {
                MouseSimulation.Down(mouse);
            }


            timer.Tick += Timer_Tick;
        }
        /// <summary>
        /// 由 hotkeyArgs 定义的热键触发,持续按下 keyPressedArgs 定义的键盘和鼠标键值
        /// </summary>
        /// <param name="hotkeyArgs"></param>
        /// <param name="describe"></param>
        /// <param name="keyPressedArgs"></param>
        public KeyAction(HotkeyArgs hotkeyArgs, string describe, KeyPressedArgs keyPressedArgs) : this(HotkeyArgs.PassThroughNonNull(hotkeyArgs).HotkeyModifierKeys, hotkeyArgs.HotkeyNormalKey, hotkeyArgs.Window, describe, KeyPressedArgs.PassThroughNonNull(keyPressedArgs).PressedKey, keyPressedArgs.PressedMouse)
        {
            Contract.Requires(hotkeyArgs != null);
            Contract.Requires(keyPressedArgs != null);
        }
        private void HotKey_HotKeyPressed(HotKey obj)
        {
            if (timer.IsEnabled)
            {
                KeyboardSimulation.Reset();
                foreach (var key in PressedMouse)
                {
                    MouseSimulation.Up(key);
                }
            }
            timer.IsEnabled = !timer.IsEnabled;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            var key = (Key)((DispatcherTimer)sender).Tag;
            KeyboardSimulation.Press(key);
        }
        public void Dispose()
        {
            try
            {
                hotKey.UnregisterHotKey();
                hotKey.Dispose();
            }
            catch (NullReferenceException)
            {


            }
        }
    }
    public class HotkeyArgs
    {
        public ModifierKeys HotkeyModifierKeys { get; set; }
        public WF.Keys HotkeyNormalKey { get; set; }
        public Window Window { get; set; }
        public static HotkeyArgs PassThroughNonNull(HotkeyArgs hotkeyArgs)
        {
            if (hotkeyArgs == null)
            {
                throw new ArgumentNullException(nameof(hotkeyArgs));
            }
            return hotkeyArgs;
        }
    }
    public class KeyPressedArgs
    {
#pragma warning disable CA2227 // Collection properties should be read only
        public List<Key> PressedKey { get; set; }
        public List<MouseButton> PressedMouse { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
        public static KeyPressedArgs PassThroughNonNull(KeyPressedArgs keyPressedArgs)
        {
            if (keyPressedArgs == null)
            {
                throw new ArgumentNullException(nameof(keyPressedArgs));
            }
            return keyPressedArgs;
        }
    }
}
