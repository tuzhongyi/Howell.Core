using System;
using System.IO;
using System.Text;
using System.Security;

namespace System
{
    /// <summary>
    /// 多色彩的控制台
    /// </summary>
    public static class ColorConsole
    {
        /// <summary>
        /// 获取或设置控制台的背景色。
        /// </summary>
        /// <returns> 一个 System.ConsoleColor，指定控制台的背景色；也就是显示在每个字符后面的颜色。默认为黑色。</returns>
        /// <exception cref="System.ArgumentException">在 Set 操作中指定的颜色不是 System.ConsoleColor 的有效成员。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static ConsoleColor BackgroundColor
        {            
            get
            {
                return Console.BackgroundColor;
            }
            set
            {
                Console.BackgroundColor = value;
            }
        }
        /// <summary>
        /// 获取或设置缓冲区的高度。
        /// </summary>
        /// <returns>缓冲区的当前高度，以行为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Set 操作中的值小于或等于零。- 或 -Set 操作中的值大于等于 System.Int16.MaxValue。- 或 -Set 操作中的值小于
        /// System.Console.WindowTop + System.Console.WindowHeight。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static int BufferHeight
        {
            get
            {
                return Console.BufferHeight;
            }
            set
            {
                Console.BufferHeight = value;
            }
        }
        /// <summary>
        /// 获取或设置缓冲区的宽度。
        /// </summary>
        /// <returns>缓冲区的当前宽度，以列为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Set 操作中的值小于或等于零。- 或 -Set 操作中的值大于等于 System.Int16.MaxValue。- 或 -Set 操作中的值小于
        /// System.Console.WindowLeft + System.Console.WindowWidth。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static int BufferWidth
        {
            get
            {
                return Console.BufferWidth;
            }
            set
            {
                Console.BufferWidth = value;
            }
        }
        /// <summary>
        /// 获取一个值，该值指示 Caps Lock 键盘切换键是打开的还是关闭的。
        /// </summary>
        /// <returns>如果 Caps Lock 是打开的，则为 true；如果 Caps Lock 是关闭的，则为 false。</returns>
        public static bool CapsLock 
        {
            get
            {
                return Console.CapsLock;
            }
        }
        /// <summary>
        /// 获取或设置光标在缓冲区中的列位置。
        /// </summary>
        /// <returns>光标的当前位置，以列为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Set 操作中的值小于零。- 或 -Set 操作中的值大于等于 System.Console.BufferWidth。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static int CursorLeft 
        {
            get
            {
                return Console.CursorLeft;
            }
            set
            {
                Console.CursorLeft = value;
            }
        }
        /// <summary>
        /// 获取或设置光标在字符单元格中的高度。
        /// </summary>
        /// <returns>光标的大小，以字符单元格高度的百分比表示。属性值的范围为 1 到 100。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">在 Set 操作中指定的值小于 1 或大于 100。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static int CursorSize
        {
            get
            {
                return Console.CursorSize;
            }
            set
            {
                Console.CursorSize = value;
            }
        }
        /// <summary>
        /// 获取或设置光标在缓冲区中的行位置。
        /// </summary>
        /// <returns>光标的当前位置，以行为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Set 操作中的值小于零。- 或 -Set 操作中的值大于等于 System.Console.BufferHeight。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static int CursorTop
        {
            get
            {
                return Console.CursorTop;
            }
            set
            {
                Console.CursorTop = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，用以指示光标是否可见。
        /// </summary>
        /// <returns> 如果光标可见，则为 true；否则为 false。</returns>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static bool CursorVisible 
        {
            get
            {
                return Console.CursorVisible;
            }
            set
            {
                Console.CursorVisible = value;
            }
        }
        /// <summary>
        /// 获取标准错误输出流。
        /// </summary>
        /// <returns>表示标准错误输出流的 System.IO.TextWriter。</returns>
        public static TextWriter Error
        {
            get
            {
                return Console.Error;
            }
        }
        /// <summary>
        /// 获取或设置控制台的前景色。
        /// </summary>
        /// <returns>一个 System.ConsoleColor，指定控制台的前景色；也就是显示的每个字符的颜色。默认为灰色。</returns>
        /// <exception cref="System.ArgumentException">在 Set 操作中指定的颜色不是 System.ConsoleColor 的有效成员。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static ConsoleColor ForegroundColor 
        {
            get
            {
                return Console.ForegroundColor;
            }
            set
            {
                Console.ForegroundColor = value;
            }
        }
        /// <summary>
        ///  获取标准输入流。
        /// </summary>
        /// <returns>表示标准输入流的 System.IO.TextReader。</returns>
        public static TextReader In 
        {
            get
            {
                return Console.In;
            }
        }
        /// <summary>
        ///  获取或设置控制台用于读取输入的编码。
        /// </summary>
        /// <returns>用于读取控制台输入的编码。</returns>
        /// <exception cref="System.ArgumentNullException">Set 操作中的属性值为 null。</exception>
        /// <exception cref="System.PlatformNotSupportedException">此属性的 Set 操作在 Windows 98、Windows 98 Second Edition 或 Windows Millennium Edition上不受支持。</exception>
        /// <exception cref="System.IO.IOException">此属性的 Set 操作在 Windows 98、Windows 98 Second Edition 或 Windows Millennium Edition上不受支持。</exception>
        /// <exception cref="System.Security.SecurityException">您的应用程序不具有执行此操作的权限。</exception>
        public static Encoding InputEncoding
        {
            get
            {
                return Console.InputEncoding;
            }
            set
            {
                Console.InputEncoding = value;
            }
        }
        /// <summary>
        ///  获取一个值，该值指示按键操作在输入流中是否可用。
        /// </summary>
        /// <returns>如果按键操作可用，则为 true；否则为 false。</returns>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        /// <exception cref="System.InvalidOperationException">标准输入重定向到文件而不是键盘。</exception>
        public static bool KeyAvailable 
        {
            get
            {
                return Console.KeyAvailable;
            }
        }
        /// <summary>
        /// 根据当前字体和屏幕分辨率获取控制台窗口可能具有的最大行数。
        /// </summary>
        /// <returns>控制台窗口可能具有的最大高度，以行为单位。</returns>
        public static int LargestWindowHeight 
        {
            get
            {
                return Console.LargestWindowHeight;
            }
        }
        /// <summary>
        /// 根据当前字体和屏幕分辨率获取控制台窗口可能具有的最大列数。
        /// </summary>
        /// <returns>控制台窗口可能具有的最大宽度，以列为单位。</returns>
        public static int LargestWindowWidth
        {
            get
            {
                return Console.LargestWindowWidth;
            }
        }
        /// <summary>
        /// 获取一个值，该值指示 Num Lock 键盘切换键是打开的还是关闭的。
        /// </summary>
        /// <returns>如果 Num Lock 是打开的，则为 true；如果 Num Lock 是关闭的，则为 false。</returns>
        public static bool NumberLock
        {
            get
            {
                return Console.NumberLock;
            }
        }
        /// <summary>
        /// 获取标准输出流。
        /// </summary>
        /// <returns>获取标准输出流。</returns>
        public static TextWriter Out 
        {
            get
            {
                return Console.Out;
            }
        }
        /// <summary>
        /// 获取或设置控制台用于写入输出的编码。
        /// </summary>
        /// <returns>用于写入控制台输出的编码。</returns>
        /// <exception cref="System.ArgumentNullException">Set 操作中的属性值为 null。</exception>
        /// <exception cref="System.PlatformNotSupportedException">此属性的 Set 操作在 Windows 98、Windows 98 Second Edition 或 Windows Millennium Edition上不受支持。</exception>
        /// <exception cref="System.IO.IOException">在执行此操作的过程中发生错误。</exception>
        /// <exception cref="System.Security.SecurityException">您的应用程序不具有执行此操作的权限。</exception>
        public static Encoding OutputEncoding
        {
            get
            {
                return Console.OutputEncoding;
            }
            set
            {
                Console.OutputEncoding = value;
            }
        }
        /// <summary>
        /// 获取或设置要显示在控制台标题栏中的标题。
        /// </summary>
        /// <returns>要在控制台的标题栏中显示的字符串。标题字符串的最大长度是 24500 个字符。</returns>
        /// <exception cref="System.InvalidOperationException">在 Get 操作中，检索到的标题的长度超过 24500 个字符。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">在 Set 操作中，指定标题的长度超过 24500 个字符。</exception>
        /// <exception cref="System.ArgumentNullException">在 Set 操作中，指定的标题为 null。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static string Title
        {
            get
            {
                return Console.Title;
            }
            set
            {
                Console.Title = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示是将修改键 System.ConsoleModifiers.Control 和控制台键 System.ConsoleKey.C
        /// 的组合 (Ctrl+C) 视为普通输入，还是视为由操作系统处理的中断。
        /// </summary>
        /// <returns>如果将 Ctrl+C 视为普通输入，则为 true；否则为 false。</returns>
        /// <exception cref="System.IO.IOException">无法获取或设置控制台输入缓冲区的输入模式。</exception>
        public static bool TreatControlCAsInput
        {
            get
            {
                return Console.TreatControlCAsInput;
            }
            set
            {
                Console.TreatControlCAsInput = value;
            }
        }
        /// <summary>
        /// 获取或设置控制台窗口区域的高度。
        /// </summary>
        /// <returns> 控制台窗口的高度，以行为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// System.Console.WindowWidth 属性值或 System.Console.WindowHeight 属性值小于或等于 0。-
        /// 或 -System.Console.WindowHeight 属性值加上 System.Console.WindowTop 属性值大于等于 System.Int16.MaxValue。-
        /// 或 -System.Console.WindowWidth 属性值或 System.Console.WindowHeight 属性值大于当前屏幕分辨率和控制台字体的最大可能窗口宽度或高度。
        /// </exception>
        /// <exception cref="System.IO.IOException">读取或写入信息时发生错误。</exception>
        public static int WindowHeight
        {
            get
            {
                return Console.WindowHeight;
            }
            set
            {
                Console.WindowHeight = value;
            }
        }
         /// <summary>
        ///  获取或设置控制台窗口区域的最左边相对于屏幕缓冲区的位置。
        /// </summary>
        /// <returns>控制台窗口的最左边的位置，以列为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 在设置操作中，所赋的值小于零。- 或 -赋值后，System.Console.WindowLeft 加上 System.Console.WindowWidth
        /// 将超过 System.Console.BufferWidth。
        /// </exception>
        /// <exception cref="System.IO.IOException">读取或写入信息时发生错误。</exception>
        public static int WindowLeft 
        {
            get
            {
                return Console.WindowLeft;
            }
            set
            {
                Console.WindowLeft = value;
            }
        }
        /// <summary>
        /// 获取或设置控制台窗口区域的最顶部相对于屏幕缓冲区的位置。
        /// </summary>
        /// <returns>控制台窗口最顶部的位置，以行为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 在设置操作中，所赋的值小于零。- 或 -赋值后，System.Console.WindowTop 加上 System.Console.WindowHeight
        /// 将超过 System.Console.BufferHeight。
        /// </exception>
        /// <exception cref="System.IO.IOException">读取或写入信息时发生错误。</exception>
        public static int WindowTop 
        {
            get
            {
                return Console.WindowTop;
            }
            set
            {
                Console.WindowTop = value;
            }

        }
        /// <summary>
        /// 获取或设置控制台窗口的宽度。
        /// </summary>
        /// <returns>控制台窗口的宽度，以列为单位。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// System.Console.WindowWidth 属性值或 System.Console.WindowHeight 属性值小于或等于 0。-
        /// 或 -System.Console.WindowHeight 属性值加上 System.Console.WindowTop 属性值大于等于 System.Int16.MaxValue。-
        /// 或 -System.Console.WindowWidth 属性值或 System.Console.WindowHeight 属性值大于当前屏幕分辨率和控制台字体的最大可能窗口宽度或高度。
        /// </exception>
        /// <exception cref="System.IO.IOException">读取或写入信息时发生错误。</exception>
        public static int WindowWidth 
        {
            get
            {
                return Console.WindowWidth;
            }
            set
            {
                Console.WindowWidth = value;
            }

        }
        /// <summary>
        /// 在同时按下修改键 System.ConsoleModifiers.Control (Ctrl) 和控制台键 System.ConsoleKey.C
        /// (C)（即 Ctrl+C）时发生。
        /// </summary>
        public static event ConsoleCancelEventHandler CancelKeyPress
        {
            add
            {
                Console.CancelKeyPress += new ConsoleCancelEventHandler(value);
            }
            remove
            {
                Console.CancelKeyPress -= new ConsoleCancelEventHandler(value);
            }
        }
        /// <summary>
        /// 通过控制台扬声器播放提示音。
        /// </summary>
        /// <exception cref="System.Security.HostProtectionException">在不允许访问用户界面的服务器（如 SQL Server）上执行此方法。</exception>
        public static void Beep()
        {
            Console.Beep();
        }
        /// <summary>
        /// 通过控制台扬声器播放具有指定频率和持续时间的提示音。
        /// </summary>
        /// <param name="frequency">提示音的频率，介于 37 到 32767 赫兹之间。</param>
        /// <param name="duration">提示音的持续时间，以毫秒为单位。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">frequency 小于 37 或大于 32767 赫兹。- 或 -duration 小于或等于零。</exception>
        /// <exception cref="System.Security.HostProtectionException">在不允许访问控制台的服务器（如 SQL Server）上执行此方法。</exception>
        [SecuritySafeCritical]
        public static void Beep(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }
        /// <summary>
        /// 清除控制台缓冲区和相应的控制台窗口的显示信息。
        /// </summary>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void Clear()
        {
            Console.Clear();
        }
        /// <summary>
        /// 将屏幕缓冲区的指定源区域复制到指定的目标区域。
        /// </summary>
        /// <param name="sourceLeft">源区域最左边的列。</param>
        /// <param name="sourceTop">源区域最顶部的行。</param>
        /// <param name="sourceWidth">源区域中列的数目。</param>
        /// <param name="sourceHeight">源区域中的行的数目。</param>
        /// <param name="targetLeft">目标区域最左边的列。</param>
        /// <param name="targetTop">目标区域最顶部的行。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 一个或多个参数小于零。- 或 -sourceLeft 或 targetLeft 大于等于 System.Console.BufferWidth。-
        /// 或 -sourceTop 或 targetTop 大于等于 System.Console.BufferHeight。- 或 -sourceTop
        /// + sourceHeight 大于等于 System.Console.BufferHeight。- 或 -sourceLeft + sourceWidth
        /// 大于等于 System.Console.BufferWidth。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
            Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
        }
        /// <summary>
        /// 将屏幕缓冲区的指定源区域复制到指定的目标区域。
        /// </summary>
        /// <param name="sourceLeft">源区域最左边的列。</param>
        /// <param name="sourceTop">源区域最顶部的行。</param>
        /// <param name="sourceWidth">源区域中列的数目。</param>
        /// <param name="sourceHeight">源区域中的行的数目。</param>
        /// <param name="targetLeft">目标区域最左边的列。</param>
        /// <param name="targetTop">目标区域最顶部的行。</param>
        /// <param name="sourceChar">用于填充源区域的字符。</param>
        /// <param name="sourceForeColor">用于填充源区域的前景色。</param>
        /// <param name="sourceBackColor">用于填充源区域的背景色。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// 一个或多个参数小于零。- 或 -sourceLeft 或 targetLeft 大于等于 System.Console.BufferWidth。-
        /// 或 -sourceTop 或 targetTop 大于等于 System.Console.BufferHeight。- 或 -sourceTop
        /// + sourceHeight 大于等于 System.Console.BufferHeight。- 或 -sourceLeft + sourceWidth
        /// 大于等于 System.Console.BufferWidth。
        /// </exception>
        /// <exception cref="System.ArgumentException">一个颜色参数不是或两个颜色参数都不是 System.ConsoleColor 枚举的成员。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
        }
        /// <summary>
        /// 获取标准错误流。
        /// </summary>
        /// <returns>标准错误流。</returns>
        public static Stream OpenStandardError()
        {
            return Console.OpenStandardError();
        }
        /// <summary>
        /// 获取设置为指定缓冲区大小的标准错误流。
        /// </summary>
        /// <param name="bufferSize">内部流缓冲区大小。</param>
        /// <returns>标准错误流。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">bufferSize 小于或等于零。</exception>
        public static Stream OpenStandardError(int bufferSize)
        {
            return Console.OpenStandardError(bufferSize);
        }
        /// <summary>
        /// 获取标准输入流。
        /// </summary>
        /// <returns>标准输入流。</returns>
        public static Stream OpenStandardInput()
        {
            return Console.OpenStandardInput();
        }
        /// <summary>
        /// 获取设置为指定缓冲区大小的标准输入流。
        /// </summary>
        /// <param name="bufferSize">内部流缓冲区大小。</param>
        /// <returns>标准输入流。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">bufferSize 小于或等于零。</exception>
        public static Stream OpenStandardInput(int bufferSize)
        {
            return Console.OpenStandardInput(bufferSize);
        }
        /// <summary>
        /// 获取标准输出流。
        /// </summary>
        /// <returns>标准输出流。</returns>
        public static Stream OpenStandardOutput()
        {
            return Console.OpenStandardOutput();
        }
        /// <summary>
        /// 获取设置为指定缓冲区大小的标准输出流。
        /// </summary>
        /// <param name="bufferSize">内部流缓冲区大小。</param>
        /// <returns>标准输出流。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">bufferSize 小于或等于零。</exception>
        public static Stream OpenStandardOutput(int bufferSize)
        {
            return Console.OpenStandardOutput(bufferSize);
        }
        /// <summary>
        /// 从标准输入流读取下一个字符。
        /// </summary>
        /// <returns>输入流中的下一个字符；如果当前没有更多的字符可供读取，则为负一 (-1)。</returns>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static int Read()
        {
            return Console.Read();
        }
        /// <summary>
        /// 获取用户按下的下一个字符或功能键。按下的键显示在控制台窗口中。
        /// </summary>
        /// <returns>
        /// 一个 System.ConsoleKeyInfo 对象，描述 System.ConsoleKey 常数和对应于按下的控制台键的 Unicode 字符（如果存在这样的字符）。System.ConsoleKeyInfo一个 System.ConsoleKeyInfo 对象，描述 System.ConsoleKey 常数和对应于按下的控制台键的 Unicode 字符（如果存在这样的字符）。System.ConsoleKeyInfo
        /// 对象还以 System.ConsoleModifiers 值的按位组合描述是否在按下该控制台键的同时按下了 Shift、Alt 或 Ctrl 修改键中的一个或多个。
        /// </returns>
        /// <exception cref="System.InvalidOperationException">System.Console.In 属性是从非控制台的某种流进行重定向的。</exception>
        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }
        /// <summary>
        /// 获取用户按下的下一个字符或功能键。按下的键可以选择显示在控制台窗口中。
        /// </summary>
        /// <param name="intercept">确定是否在控制台窗口中显示按下的键。如果为 true，则不显示按下的键；否则为 false。</param>
        /// <returns>
        /// 一个 System.ConsoleKeyInfo 对象，描述 System.ConsoleKey 常数和对应于按下的控制台键的 Unicode 字符（如果存在这样的字符）。System.ConsoleKeyInfo
        /// 对象还以 System.ConsoleModifiers 值的按位组合描述是否在按下该控制台键的同时按下了 Shift、Alt 或 Ctrl 修改键中的一个或多个。
        /// </returns>
        [SecuritySafeCritical]
        public static ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }
        /// <summary>
        /// 从标准输入流读取下一行字符。
        /// </summary>
        /// <returns>输入流中的下一行字符；如果没有更多的可用行，则为 null。</returns>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        /// <exception cref="System.OutOfMemoryException">内存不足，无法为返回的字符串分配缓冲区。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">下一行字符中的字符数大于 System.Int32.MaxValue。</exception>
        public static string ReadLine()
        {
            return Console.ReadLine();
        }
        /// <summary>
        /// 将控制台的前景色和背景色设置为默认值。
        /// </summary>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void ResetColor()
        {
            Console.ResetColor();
        }
        /// <summary>
        /// 将屏幕缓冲区的高度和宽度设置为指定值。
        /// </summary>
        /// <param name="width">缓冲区的宽度，以列为单位。</param>
        /// <param name="height">缓冲区的高度，以行为单位。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// height 或 width 小于或等于零。- 或 -height 或 width 大于等于 System.Int16.MaxValue。- 或
        /// -width 小于 System.Console.WindowLeft + System.Console.WindowWidth。- 或 -height
        /// 小于 System.Console.WindowTop + System.Console.WindowHeight。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void SetBufferSize(int width, int height)
        {
            Console.SetBufferSize(width, height);
        }
        /// <summary>
        /// 设置光标位置。
        /// </summary>
        /// <param name="left">光标的列位置。</param>
        /// <param name="top">光标的行位置。</param>
        /// <exception cref="System.ArgumentOutOfRangeException"> left 或 top 小于零。- 或 -left 大于等于 System.Console.BufferWidth。- 或 -top 大于等于 System.Console.BufferHeight。</exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }
        /// <summary>
        /// 将 System.Console.Error 属性设置为指定的 System.IO.TextWriter 对象。
        /// </summary>
        /// <param name="newError">一个 System.IO.TextWriter 流，它是新的标准错误输出。</param>
        /// <exception cref="System.ArgumentNullException">newError 为 null。</exception>
        /// <exception cref="System.Security.SecurityException">调用方没有所要求的权限。</exception>
        [SecuritySafeCritical]
        public static void SetError(TextWriter newError)
        {
            Console.SetError(newError);
        }
        /// <summary>
        /// 将 System.Console.In 属性设置为指定的 System.IO.TextReader 对象。
        /// </summary>
        /// <param name="newIn">一个 System.IO.TextReader 流，它是新的标准输入。</param>
        /// <exception cref="System.ArgumentNullException">newIn 为 null。</exception>
        /// <exception cref="System.Security.SecurityException">调用方没有所要求的权限。</exception>
        [SecuritySafeCritical]
        public static void SetIn(TextReader newIn)
        {
            Console.SetIn(newIn);
        }
        /// <summary>
        /// 将 System.Console.Out 属性设置为指定的 System.IO.TextWriter 对象。
        /// </summary>
        /// <param name="newOut">一个 System.IO.TextWriter 流，它是新的标准输出。</param>
        /// <exception cref="System.ArgumentNullException">newOut 为 null。</exception>
        /// <exception cref="System.Security.SecurityException">调用方没有所要求的权限。</exception>
        [SecuritySafeCritical]
        public static void SetOut(TextWriter newOut)
        {
            Console.SetOut(newOut);
        }
        /// <summary>
        /// 设置控制台窗口相对于屏幕缓冲区的位置。
        /// </summary>
        /// <param name="left">控制台窗口左上角的列位置。</param>
        /// <param name="top">控制台窗口左上角的行位置。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// left 或 top 小于零。- 或 -left + System.Console.WindowWidth 大于 System.Console.BufferWidth。-
        /// 或 -top + System.Console.WindowHeight 大于 System.Console.BufferHeight。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void SetWindowPosition(int left, int top)
        {
            Console.SetWindowPosition(left, top);
        }
        /// <summary>
        /// 将控制台窗口的高度和宽度设置为指定值。
        /// </summary>
        /// <param name="width">控制台窗口的宽度，以列为单位。</param>
        /// <param name="height">控制台窗口的高度，以行为单位。</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// width 或 height 小于或等于零。- 或 -width 加 System.Console.WindowLeft 或者 height 加
        /// System.Console.WindowTop 大于等于 System.Int16.MaxValue。- 或 -width 或 height 大于当前屏幕分辨率和控制台字体所允许的最大窗口宽度和高度。
        /// </exception>
        /// <exception cref="System.Security.SecurityException">该用户没有执行此操作的权限。</exception>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        [SecuritySafeCritical]
        public static void SetWindowSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
        }
        /// <summary>
        /// 将指定的布尔值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(bool value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的布尔值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(bool value,ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的 Unicode 字符值写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(char value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的 Unicode 字符值写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(char value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的 Unicode 字符数组写入标准输出流。
        /// </summary>
        /// <param name="buffer">Unicode 字符数组。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(char[] buffer)
        {
            Console.Write(buffer);
        }
        /// <summary>
        /// 将指定的 Unicode 字符数组写入标准输出流。
        /// </summary>
        /// <param name="buffer">Unicode 字符数组。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(char[] buffer, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), buffer);
        }
        /// <summary>
        /// 将指定的 System.Decimal 值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(decimal value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的 System.Decimal 值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(decimal value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的双精度浮点值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(double value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的双精度浮点值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(double value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的单精度浮点值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(float value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的单精度浮点值的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(float value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的 32 位有符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(int value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的 32 位有符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(int value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的 64 位有符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(long value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的 64 位有符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(long value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定对象的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值，或者为 null。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(object value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定对象的文本表示形式写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(object value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的字符串值写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(string value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的字符串值写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(string value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的 32 位无符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(uint value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的 32 位无符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(uint value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将指定的 64 位无符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(ulong value)
        {
            Console.Write(value);
        }
        /// <summary>
        /// 将指定的 64 位无符号整数值的文本表示写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void Write(ulong value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.Write(x), value);
        }
        /// <summary>
        /// 将当前行终止符写入标准输出流。
        /// </summary>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine()
        {
            Console.WriteLine();
        }
        /// <summary>
        /// 将指定布尔值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(bool value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定布尔值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(bool value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的 Unicode 字符值（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(char value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的 Unicode 字符值（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(char value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的 Unicode 字符数组（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="buffer">Unicode 字符数组。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(char[] buffer)
        {
            Console.WriteLine(buffer);
        }
        /// <summary>
        /// 将指定的 Unicode 字符数组（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="buffer">Unicode 字符数组。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(char[] buffer, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), buffer);
        }
        /// <summary>
        /// 将指定的 System.Decimal 值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(decimal value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的 System.Decimal 值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(decimal value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的双精度浮点值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(double value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的双精度浮点值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(double value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的单精度浮点值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(float value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的单精度浮点值的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(float value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的 32 位有符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(int value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的 32 位有符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(int value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的 64 位有符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(long value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的 64 位有符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(long value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定对象的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定对象的文本表示形式（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(object value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的字符串值（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的字符串值（后跟当前行终止符）写入标准输出流。
        /// </summary>
        /// <param name="value">要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(string value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的 32 位无符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value"> 要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(uint value)
        {
            Console.WriteLine(value);
        }
        /// <summary>
        /// 将指定的 32 位无符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value"> 要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(uint value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        /// <summary>
        /// 将指定的 64 位无符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value"> 要写入的值。</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(ulong value)
        {
            Console.WriteLine(value);
        }        
        /// <summary>
        /// 将指定的 64 位无符号的整数值的文本表示（后跟当前行的结束符）写入标准输出流。
        /// </summary>
        /// <param name="value"> 要写入的值。</param>
        /// <param name="color">要显示的字体颜色</param>
        /// <exception cref="System.IO.IOException">发生了 I/O 错误。</exception>
        public static void WriteLine(ulong value, ConsoleColor color)
        {
            Write(color, x => ColorConsole.WriteLine(x), value);
        }
        private static void Write<T>(ConsoleColor color,Action<T> action,T value)
        {
            lock (typeof(ColorConsole))
            {
                ConsoleColor oldColor = ForegroundColor;
                try
                {
                    ForegroundColor = color;
                    action(value);
                }
                finally
                {
                    ForegroundColor = oldColor;
                }
            }
        }
    }
}
