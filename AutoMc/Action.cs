using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace AutoMc
{
    /// <summary>
    /// 表示一个动作 (一直按左键,一直按w键)
    /// </summary>
    public class Action:IDisposable
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
