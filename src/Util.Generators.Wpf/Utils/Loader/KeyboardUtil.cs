using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Util.Generators.Utils.Loader
{
    /// <summary>
    /// 键盘工具类
    /// </summary>
    public class KeyboardUtil
    {
        /// <summary>
        /// 导入模拟键盘的方法
        /// </summary>
        /// <param name="bVk" >按键的虚拟键值</param>
        /// <param name= "bScan" >扫描码，一般不用设置，用0代替就行</param>
        /// <param name= "dwFlags" >选项标志：0：表示按下，2：表示松开</param>
        /// <param name= "dwExtraInfo">一般设置为0</param>
        [DllImport("User32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// 键盘字符字典
        /// </summary>
        public static Dictionary<string, byte>? Keys;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static KeyboardUtil()
        {
            CreateKeys();
        }

        /// <summary>
        /// 键盘按下
        /// </summary>
        /// <param name="key"></param>
        public static void PressKeyDown(byte key)
        {
            keybd_event(key, 0, 0, 0);
        }

        /// <summary>
        /// 键盘弹起
        /// </summary>
        /// <param name="key"></param>
        public static void PressKeyUp(byte key)
        {
            keybd_event(key, 0, 2, 0);
        }

        /// <summary>
        /// 创建键-值对
        /// </summary>
        private static void CreateKeys()
        {
            Keys = new Dictionary<string, byte>
            {
                { "0", 96 },
                { "1", 97 },
                { "2", 98 },
                { "3", 99 },
                { "4", 100 },
                { "5", 101 },
                { "6", 102 },
                { "7", 103 },
                { "8", 104 },
                { "9", 105 },
                { "A", 65 },
                { "B", 66 },
                { "C", 67 },
                { "D", 68 },
                { "E", 69 },
                { "F", 70 },
                { "G", 71 },
                { "H", 72 },
                { "I", 73 },
                { "J", 74 },
                { "K", 75 },
                { "L", 76 },
                { "M", 77 },
                { "N", 78 },
                { "O", 79 },
                { "P", 80 },
                { "Q", 81 },
                { "R", 82 },
                { "S", 83 },
                { "T", 84 },
                { "U", 85 },
                { "V", 86 },
                { "W", 87 },
                { "X", 88 },
                { "Y", 89 },
                { "Z", 90 },
                { "*", 106 },
                { "+", 107 },
                { "-", 109 },
                { ".", 110 },
                { "/", 111 },
                { "F1", 112 },
                { "F2", 113 },
                { "F3", 114 },
                { "F4", 115 },
                { "F5", 116 },
                { "F6", 117 },
                { "F7", 118 },
                { "F8", 119 },
                { "F9", 120 },
                { "F10", 121 },
                { "F11", 122 },
                { "F12", 123 },
                { "Backspace", 8 },
                { "Tab", 9 },
                { "Clear", 12 },
                { "Enter", 13 },
                { "Shift", 16 },
                { "Control", 17 },
                { "Alt", 18 },
                { "Cape Lock", 20 },
                { "Esc", 27 },
                { "Space Bar", 32 },
                { "Page Up", 33 },
                { "Page Down", 34 },
                { "End", 35 },
                { "Home", 36 },
                { "Left Arrow", 37 },
                { "Up Arrow", 38 },
                { "Right Arrow", 39 },
                { "Down Arrow", 40 },
                { "Insert", 45 },
                { "Delete", 46 },
                { "Num Lock", 144 },
            };
        }
    }
}