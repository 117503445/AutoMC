using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TLib.Windows;
using WF = System.Windows.Forms;
namespace AutoMc
{
    /// <summary>
    /// 表示一个按键动作 (一直按左键,一直按w键)
    /// </summary>
    public class KeyAction : IDisposable
    {
        /// <summary>
        /// 标识按键 如 Ctrl Shift,支持 ModifierKeys.Ctrl | ModifierKeys.Shift
        /// </summary>
        public ModifierKeys ModifierKeys { get; set; }
        /// <summary>
        /// 普通键盘按键,如 C
        /// </summary>
        public Key Key { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 按下热键后 要一直按住的键盘键值
        /// </summary>
        public List<Key> PressedKey { get; set; }
        /// <summary>
        /// 按下热键后 要一直按住的鼠标键值
        /// </summary>
        public List<MouseButton> PressedMouse { get; set; }
        [Newtonsoft.Json.JsonIgnore()]
        private readonly HotKey hotKey;
        [Newtonsoft.Json.JsonIgnore()]

        public bool IsKeyRegistered
        {
            get
            {
                try
                {
                    return hotKey.IsKeyRegistered;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
        public KeyAction(ModifierKeys modifierKeys, WF.Keys key, Window window, string describe, List<Key> pressedKey, List<MouseButton> pressedMouse)
        {
            hotKey = new HotKey(modifierKeys, key, window);
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
        }

        private void HotKey_HotKeyPressed(HotKey obj)
        {
            foreach (var key in PressedKey)
            {
                KeyboardSimulation.Press(key);
            }
            foreach (var mouse in PressedMouse)
            {
                MouseSimulation.Down(mouse);
            }
        }

        public void Dispose()
        {
            try
            {
                hotKey.UnregisterHotKey();
                hotKey.Dispose();
            }
            catch (Exception)
            {


            }
        }
    }
}
