using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Howell.Media
{
    /// <summary>
    /// 媒体音频播放异常类型
    /// </summary>
    public class MeidaSoundException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        public MeidaSoundException()
            : base()
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常信息</param>
        public MeidaSoundException(String message)
            : base(message)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常对象</param>
        public MeidaSoundException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    /// <summary>
    /// 音频播放器状态
    /// </summary>
    public enum SoundPlayerState
    {
        /// <summary>
        /// 设备尚未准备
        /// </summary>
        NotReady,
        /// <summary>
        /// 已停止
        /// </summary>
        Stop,
        /// <summary>
        /// 正在播放
        /// </summary>
        Play,
        /// <summary>
        /// 正在录制
        /// </summary>
        Record,
        /// <summary>
        /// 正在定位
        /// </summary>
        Seek,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 音频文件已打开
        /// </summary>
        Open,
    }
    /// <summary>
    /// Windows 音频播放器, 支持播放.wav,.midi,.mp3 音频文件
    /// 注意在部分机器上无法播放Mp3文件
    /// </summary>
    public class SoundPlayer : IDisposable
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct MCI_OPEN_PARMS
        {
            public IntPtr dwCallback;
            public UInt32 wDeviceID;
            [MarshalAs(UnmanagedType.LPStr)]
            public String lpstrDeviceType;
            [MarshalAs(UnmanagedType.LPStr)]
            public String lpstrElementName;
            [MarshalAs(UnmanagedType.LPStr)]
            public String lpstrAlias;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct MCI_SET_PARMS
        {
            public IntPtr dwCallback;
            public UInt32 dwTimeFormat;
            public UInt32 dwAudio;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct MCI_GENERIC_PARMS
        {
            public IntPtr dwCallback;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct MCI_PLAY_PARMS
        {
            public IntPtr dwCallback;
            public UInt32 dwFrom;
            public UInt32 dwTo;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct MCI_STATUS_PARMS
        {
            public IntPtr dwCallback;
            public UInt32 dwReturn;
            public UInt32 dwItem;
            public UInt32 dwTrack;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct MCI_SEEK_PARMS
        {
            public IntPtr dwCallback;
            public UInt32 dwTo;
        }
        
        private const UInt32 MCI_STRING_OFFSET = 512;
        private static class MCI_Command
        {
            public const Int32 MCI_OPEN = 0x0803;
            public const Int32 MCI_CLOSE = 0x0804;
            public const Int32 MCI_ESCAPE = 0x0805;
            public const Int32 MCI_PLAY = 0x0806;
            public const Int32 MCI_SEEK = 0x0807;
            public const Int32 MCI_STOP = 0x0808;
            public const Int32 MCI_PAUSE = 0x0809;
            public const Int32 MCI_INFO = 0x080A;
            public const Int32 MCI_GETDEVCAPS = 0x080B;
            public const Int32 MCI_SPIN = 0x080C;
            public const Int32 MCI_SET = 0x080D;
            public const Int32 MCI_STEP = 0x080E;
            public const Int32 MCI_RECORD = 0x080F;
            public const Int32 MCI_SYSINFO = 0x0810;
            public const Int32 MCI_BREAK = 0x0811;
            public const Int32 MCI_SAVE = 0x0813;
            public const Int32 MCI_STATUS = 0x0814;
            public const Int32 MCI_CUE = 0x0830;
            public const Int32 MCI_REALIZE = 0x0840;
            public const Int32 MCI_WINDOW = 0x0841;
            public const Int32 MCI_PUT = 0x0842;
            public const Int32 MCI_WHERE = 0x0843;
            public const Int32 MCI_FREEZE = 0x0844;
            public const Int32 MCI_UNFREEZE = 0x0845;
            public const Int32 MCI_LOAD = 0x0850;
            public const Int32 MCI_CUT = 0x0851;
            public const Int32 MCI_COPY = 0x0852;
            public const Int32 MCI_PASTE = 0x0853;
            public const Int32 MCI_UPDATE = 0x0854;
            public const Int32 MCI_RESUME = 0x0855;
            public const Int32 MCI_DELETE = 0x0856;
        }
        private static class MCI_DeviceType
        {
            public const UInt32 MCI_ALL_DEVICE_ID = UInt32.MaxValue;
            public const UInt32 MCI_DEVTYPE_VCR = MCI_STRING_OFFSET + 1;//513
            public const UInt32 MCI_DEVTYPE_VIDEODISC = MCI_STRING_OFFSET + 2;//514
            public const UInt32 MCI_DEVTYPE_OVERLAY = MCI_STRING_OFFSET + 3;//515
            public const UInt32 MCI_DEVTYPE_CD_AUDIO = MCI_STRING_OFFSET + 4;//516
            public const UInt32 MCI_DEVTYPE_DAT = MCI_STRING_OFFSET + 5;//517
            public const UInt32 MCI_DEVTYPE_SCANNER = MCI_STRING_OFFSET + 6;//518
            public const UInt32 MCI_DEVTYPE_ANIMATION = MCI_STRING_OFFSET + 7;//519
            public const UInt32 MCI_DEVTYPE_DIGITAL_VIDEO = MCI_STRING_OFFSET + 8;//520
            public const UInt32 MCI_DEVTYPE_OTHER = MCI_STRING_OFFSET + 9;//521
            public const UInt32 MCI_DEVTYPE_WAVEFORM_AUDIO = MCI_STRING_OFFSET + 10;//522
            public const UInt32 MCI_DEVTYPE_SEQUENCER = MCI_STRING_OFFSET + 11;//523

            public const UInt32 MCI_DEVTYPE_FIRST = MCI_DEVTYPE_VCR;
            public const UInt32 MCI_DEVTYPE_LAST = MCI_DEVTYPE_SEQUENCER;

            public const UInt32 MCI_DEVTYPE_FIRST_USER = 0x1000;
        }
        private static class MCI_Format
        {
            public const UInt32 MCI_FORMAT_MILLISECONDS = 0;
            public const UInt32 MCI_FORMAT_HMS = 1;
            public const UInt32 MCI_FORMAT_MSF = 2;
            public const UInt32 MCI_FORMAT_FRAMES = 3;
            public const UInt32 MCI_FORMAT_SMPTE_24 = 4;
            public const UInt32 MCI_FORMAT_SMPTE_25 = 5;
            public const UInt32 MCI_FORMAT_SMPTE_30 = 6;
            public const UInt32 MCI_FORMAT_SMPTE_30DROP = 7;
            public const UInt32 MCI_FORMAT_BYTES = 8;
            public const UInt32 MCI_FORMAT_SAMPLES = 9;
            public const UInt32 MCI_FORMAT_TMSF = 10;
        }
        private static class MCI_Status
        {
            public const UInt32 MCI_STATUS_LENGTH = 0x00000001;
            public const UInt32 MCI_STATUS_POSITION = 0x00000002;
            public const UInt32 MCI_STATUS_NUMBER_OF_TRACKS = 0x00000003;
            public const UInt32 MCI_STATUS_MODE = 0x00000004;
            public const UInt32 MCI_STATUS_MEDIA_PRESENT = 0x00000005;
            public const UInt32 MCI_STATUS_TIME_FORMAT = 0x00000006;
            public const UInt32 MCI_STATUS_READY = 0x00000007;
            public const UInt32 MCI_STATUS_CURRENT_TRACK = 0x00000008;
        }
        private static class MCI_Mode
        {
            public const UInt32 MCI_MODE_NOT_READY = MCI_STRING_OFFSET + 12;
            public const UInt32 MCI_MODE_STOP = MCI_STRING_OFFSET + 13;
            public const UInt32 MCI_MODE_PLAY = MCI_STRING_OFFSET + 14;
            public const UInt32 MCI_MODE_RECORD = MCI_STRING_OFFSET + 15;
            public const UInt32 MCI_MODE_SEEK = MCI_STRING_OFFSET + 16;
            public const UInt32 MCI_MODE_PAUSE = MCI_STRING_OFFSET + 17;
            public const UInt32 MCI_MODE_OPEN = MCI_STRING_OFFSET + 18;

        }
        [Flags()]
        private enum MCI_OpenFlags
        {
            MCI_NOTIFY = 0x00000001,
            MCI_WAIT = 0x00000002,
            MCI_FROM = 0x00000004,
            MCI_TO = 0x00000008,
            MCI_TRACK = 0x00000010,
            MCI_OPEN_SHAREABLE = 0x00000100,
            MCI_OPEN_ELEMENT = 0x00000200,
            MCI_OPEN_ALIAS = 0x00000400,
            MCI_OPEN_ELEMENT_ID = 0x00000800,
            MCI_OPEN_TYPE_ID = 0x00001000,
            MCI_OPEN_TYPE = 0x00002000,
        }
        [Flags()]
        private enum MCI_SeekFlags
        {
            MCI_NOTIFY = 0x00000001,
            MCI_WAIT = 0x00000002,
            MCI_FROM = 0x00000004,
            MCI_TO = 0x00000008,
            MCI_TRACK = 0x00000010,
            MCI_SEEK_TO_START = 0x00000100,
            MCI_SEEK_TO_END = 0x00000200,
        }
        [Flags()]
        private enum MCI_StatusFlags
        {
            MCI_NOTIFY = 0x00000001,
            MCI_WAIT = 0x00000002,
            MCI_FROM = 0x00000004,
            MCI_TO = 0x00000008,
            MCI_TRACK = 0x00000010,
            MCI_STATUS_ITEM = 0x00000100,
            MCI_STATUS_START = 0x00000200,
        }
        [Flags()]
        private enum MCI_SysInfoFlags
        {
            MCI_NOTIFY = 0x00000001,
            MCI_WAIT = 0x00000002,
            MCI_FROM = 0x00000004,
            MCI_TO = 0x00000008,
            MCI_TRACK = 0x00000010,
            MCI_SYSINFO_QUANTITY = 0x00000100,
            MCI_SYSINFO_OPEN = 0x00000200,
            MCI_SYSINFO_NAME = 0x00000400,
            MCI_SYSINFO_INSTALLNAME = 0x00000800,
        }
        [Flags()]
        private enum MCI_SetFlags
        {
            MCI_NOTIFY = 0x00000001,
            MCI_WAIT = 0x00000002,
            MCI_FROM = 0x00000004,
            MCI_TO = 0x00000008,
            MCI_TRACK = 0x00000010,
            MCI_SET_DOOR_OPEN = 0x00000100,
            MCI_SET_DOOR_CLOSED = 0x00000200,
            MCI_SET_TIME_FORMAT = 0x00000400,
            MCI_SET_AUDIO = 0x00000800,
            MCI_SET_VIDEO = 0x00001000,
            MCI_SET_ON = 0x00002000,
            MCI_SET_OFF = 0x00004000,
        }

        /// <summary>
        /// The mciSendCommand function sends a command message to the specified MCI device.
        /// </summary>
        /// <param name="mciId">Device identifier of the MCI device that is to receive the command message. This parameter is not used with the MCI_OPEN command message.</param>
        /// <param name="uMessage">Command message. For a list, see Multimedia Commands.</param>
        /// <param name="fdwCommand">Flags for the command message.</param>
        /// <param name="dwParam">Pointer to a structure that contains parameters for the command message.</param>
        /// <returns>
        /// Returns zero if successful or an error otherwise. 
        /// The low-order word of the returned DWORD value contains the error return value. 
        /// If the error is device-specific, the high-order word of the return value is the driver identifier; 
        /// otherwise, the high-order word is zero. For a list of possible return values, see MCIERR Return Values.
        /// </returns>
        [DllImport("winmm.dll", EntryPoint = "mciSendCommand", CharSet = CharSet.Ansi, BestFitMapping = true, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        private static extern UInt32 mciSendCommand(UInt32 mciId, UInt32 uMessage, UInt32 fdwCommand, IntPtr dwParam);
        /// <summary>
        /// The mciGetErrorString function retrieves a string that describes the specified MCI error code.
        /// </summary>
        /// <param name="fdwError">Error code returned by the mciSendCommand or mciSendString function.</param>
        /// <param name="lpszErrorText">Pointer to a buffer that receives a null-terminated string describing the specified error.</param>
        /// <param name="cchErrorText">Length of the buffer, in characters, pointed to by the lpszErrorText parameter.</param>
        /// <returns>Returns TRUE if successful or FALSE if the error code is not known.</returns>
        [DllImport("winmm.dll", EntryPoint = "mciGetErrorString", CharSet = CharSet.Ansi, BestFitMapping = true, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean mciGetErrorString(UInt32 fdwError, [MarshalAs(UnmanagedType.LPStr)] System.Text.StringBuilder lpszErrorText, uint cchErrorText);
        private UInt32 m_DeviceID = 0;
        private Boolean m_Paused = false;
        private Boolean m_IsPlaying = false;
        /// <summary>
        /// 构造函数，创建SoundPlayer对象
        /// </summary>
        public SoundPlayer()
            : this("")
        {
        }
        /// <summary>
        /// 构造函数，创建SoundPlayer对象
        /// </summary>
        /// <param name="soundLocation">要加载的音频文件的位置。</param>
        public SoundPlayer(String soundLocation)
        {
            this.SoundLocation = soundLocation;
            m_IsPlaying = false;
            m_Paused = false;
            m_DeviceID = 0;
        }
        /// <summary>
        /// 获取或设置要加载的音频文件的文件路径或 URL。
        /// </summary>
        public string SoundLocation { get; set; }
        /// <summary>
        /// 同步加载声音。
        /// </summary>
        /// <exception cref="System.ArgumentNullException"> Howell.Media.SoundPlayer.SoundLocation 未指定音频文件。</exception>
        /// <exception cref="System.TimeoutException">加载所用的时间超出了 Howell.Media.SoundPlayer.LoadTimeout 指定的时间（以毫秒为单位）。</exception>
        /// <exception cref="System.IO.FileNotFoundException">无法找到由 Howell.Media.SoundPlayer.SoundLocation 指定的文件。</exception>
        public void Load()
        {
            if (String.IsNullOrEmpty(this.SoundLocation) == true)
            {
                throw new ArgumentNullException("SoundLocation does not set.");
            }
            if (File.Exists(this.SoundLocation) == false)
            {
                throw new FileNotFoundException(String.Format("Sound file not exists, file path is {0}.", this.SoundLocation));
            }
            Open();
        }
        /// <summary>
        /// 使用新线程播放音频文件，如果尚未加载音频文件，则先加载该文件。
        /// </summary>
        public void Play()
        {
            Play(false);
        }
        /// <summary>
        /// 使用当前线程播放音频文件，如果尚未加载音频文件，则先加载该文件。
        /// </summary>
        public void PlaySync()
        {
            Play(true);
        }
        /// <summary>
        /// 暂停音频播放
        /// </summary>
        public void Pause()
        {
            if (m_DeviceID != 0)
            {
                MCI_GENERIC_PARMS gp = new MCI_GENERIC_PARMS();
                gp.dwCallback = IntPtr.Zero;
                IntPtr gpPointer = Marshal.AllocHGlobal(Marshal.SizeOf(gp));
                try
                {
                    Marshal.StructureToPtr(gp, gpPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_PAUSE, (UInt32)MCI_OpenFlags.MCI_WAIT, gpPointer);
                    CheckError(errorCode);
                    m_Paused = true;
                }
                finally
                {
                    Marshal.FreeHGlobal(gpPointer);
                }
            }
            else
            {
                throw new InvalidOperationException("Sound device has already closed.");
            }
        }
        /// <summary>
        /// 定位音频播放位置
        /// </summary>
        /// <param name="position">音频播放位置</param>
        public void Seek(TimeSpan position)
        {
            if (m_DeviceID != 0)
            {
                MCI_SEEK_PARMS sp = new MCI_SEEK_PARMS();
                sp.dwCallback = IntPtr.Zero;
                sp.dwTo = Convert.ToUInt32(position.TotalMilliseconds);
                IntPtr spPointer = Marshal.AllocHGlobal(Marshal.SizeOf(sp));
                try
                {
                    Marshal.StructureToPtr(sp, spPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_SEEK, (UInt32)(MCI_SeekFlags.MCI_WAIT | MCI_SeekFlags.MCI_TO), spPointer);
                    CheckError(errorCode);
                    if (Paused == true)
                    {
                        return;
                    }
                    else
                    {
                        m_Paused = true;
                        Play();
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(spPointer);
                }
            }
            else
            {
                throw new InvalidOperationException("Sound device has already closed.");
            }
        }
        /// <summary>
        /// 如果播放正在进行，则停止播放声音。
        /// </summary>
        public void Stop()
        {
            m_Paused = false;
            m_IsPlaying = false;
            if (m_DeviceID != 0)
            {
                MCI_GENERIC_PARMS gp = new MCI_GENERIC_PARMS();
                gp.dwCallback = IntPtr.Zero;
                IntPtr gpPointer = Marshal.AllocHGlobal(Marshal.SizeOf(gp));
                try
                {
                    Marshal.StructureToPtr(gp, gpPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_STOP, (UInt32)MCI_OpenFlags.MCI_WAIT, gpPointer);
                    CheckError(errorCode);
                    m_IsPlaying = false;
                }
                finally
                {
                    Marshal.FreeHGlobal(gpPointer);
                }
                Close();
            }
        }
        /// <summary>
        /// 销毁音频播放器对象
        /// </summary>
        public void Dispose()
        {
            Stop();
            return;
        }
        /// <summary>
        /// 获取音频文件的播放进度
        /// </summary>
        /// <returns>返回已播放的时间长度</returns>
        public TimeSpan GetPosition()
        {
            if (m_DeviceID != 0)
            {
                // Get audio position in millisecs
                MCI_STATUS_PARMS sp = new MCI_STATUS_PARMS();
                sp.dwCallback = IntPtr.Zero;
                sp.dwItem = MCI_Status.MCI_STATUS_POSITION;
                IntPtr spPointer = Marshal.AllocHGlobal(Marshal.SizeOf(sp));
                try
                {
                    Marshal.StructureToPtr(sp, spPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_STATUS, (UInt32)(MCI_StatusFlags.MCI_WAIT | MCI_StatusFlags.MCI_STATUS_ITEM), spPointer);
                    CheckError(errorCode);
                    MCI_STATUS_PARMS sp2 = (MCI_STATUS_PARMS)Marshal.PtrToStructure(spPointer, typeof(MCI_STATUS_PARMS));
                    return new TimeSpan(0, 0, 0, 0, (Int32)sp2.dwReturn);
                }
                finally
                {
                    Marshal.FreeHGlobal(spPointer);
                }
            }
            else
            {
                throw new InvalidOperationException("Sound device has already closed.");
            }
        }
        /// <summary>
        /// 获取音频文件的总时长
        /// </summary>
        /// <returns>返回音频文件的总时长</returns>
        public TimeSpan GetLength()
        {
            // Get audio length in millisecs
            if (m_DeviceID != 0)
            {
                MCI_STATUS_PARMS sp = new MCI_STATUS_PARMS();
                sp.dwCallback = IntPtr.Zero;
                sp.dwItem = MCI_Status.MCI_STATUS_LENGTH;
                IntPtr spPointer = Marshal.AllocHGlobal(Marshal.SizeOf(sp));
                try
                {
                    Marshal.StructureToPtr(sp, spPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_STATUS, (UInt32)(MCI_StatusFlags.MCI_WAIT | MCI_StatusFlags.MCI_STATUS_ITEM), spPointer);
                    CheckError(errorCode);
                    MCI_STATUS_PARMS sp2 = (MCI_STATUS_PARMS)Marshal.PtrToStructure(spPointer, typeof(MCI_STATUS_PARMS));
                    return new TimeSpan(0, 0, 0, 0, (Int32)sp2.dwReturn);
                }
                finally
                {
                    Marshal.FreeHGlobal(spPointer);
                }
            }
            else
            {
                throw new InvalidOperationException("Sound device has already closed.");
            }
        }
        /// <summary>
        /// 是否处于暂停状态
        /// </summary>
        public Boolean Paused
        {
            get
            {
                return m_Paused;
            }
        }
        /// <summary>
        /// 是否正在播放
        /// </summary>
        public Boolean IsPlaying
        {
            get
            {
                return m_IsPlaying;
            }
        }
        /// <summary>
        /// 获取播放状态
        /// </summary>
        /// <returns></returns>
        public SoundPlayerState GetState()
        {
            if (m_DeviceID != 0)
            {
                MCI_STATUS_PARMS sp = new MCI_STATUS_PARMS();
                sp.dwCallback = IntPtr.Zero;
                sp.dwItem = MCI_Status.MCI_STATUS_MODE;
                IntPtr spPointer = Marshal.AllocHGlobal(Marshal.SizeOf(sp));
                try
                {
                    Marshal.StructureToPtr(sp, spPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_STATUS, (UInt32)(MCI_StatusFlags.MCI_WAIT | MCI_StatusFlags.MCI_STATUS_ITEM), spPointer);
                    CheckError(errorCode);
                    MCI_STATUS_PARMS sp2 = (MCI_STATUS_PARMS)Marshal.PtrToStructure(spPointer, typeof(MCI_STATUS_PARMS));
                    switch(sp2.dwReturn)
                    {
                        case MCI_Mode.MCI_MODE_NOT_READY:
                            return SoundPlayerState.NotReady;
                        case MCI_Mode.MCI_MODE_OPEN:
                            return SoundPlayerState.Open;
                        case MCI_Mode.MCI_MODE_PAUSE:
                            return SoundPlayerState.Pause;
                        case MCI_Mode.MCI_MODE_PLAY:
                            return SoundPlayerState.Play;
                        case MCI_Mode.MCI_MODE_RECORD:
                            return SoundPlayerState.Record;
                        case MCI_Mode.MCI_MODE_SEEK:
                            return SoundPlayerState.Seek;
                        default:
                            return SoundPlayerState.Stop;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(spPointer);
                }
            }
            else
            {
                throw new InvalidOperationException("Sound device has already closed.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="synchronized"></param>
        private void Play(Boolean synchronized)
        {

            if (m_DeviceID == 0)
                Load();
            // Play current audio file or resume playing
            if (m_DeviceID != 0)
            {
                if (m_Paused)
                {
                    // Play from current position
                    MCI_PLAY_PARMS pp = new MCI_PLAY_PARMS();
                    pp.dwCallback = IntPtr.Zero;
                    IntPtr ppPointer = Marshal.AllocHGlobal(Marshal.SizeOf(pp));
                    try
                    {
                        Marshal.StructureToPtr(pp, ppPointer, false);
                        UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_PLAY, (UInt32)(synchronized ? MCI_OpenFlags.MCI_WAIT : MCI_OpenFlags.MCI_NOTIFY), ppPointer);
                        CheckError(errorCode);
                        m_Paused = false;
                        return;
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(ppPointer);
                    }
                }
                else
                {
                    //Stop();
                    MCI_PLAY_PARMS pp = new MCI_PLAY_PARMS();
                    pp.dwCallback = IntPtr.Zero;
                    pp.dwFrom = 0;
                    IntPtr ppPointer = Marshal.AllocHGlobal(Marshal.SizeOf(pp));
                    try
                    {
                        Marshal.StructureToPtr(pp, ppPointer, false);
                        UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_PLAY, (UInt32)((synchronized ? MCI_OpenFlags.MCI_WAIT : MCI_OpenFlags.MCI_NOTIFY) | MCI_OpenFlags.MCI_FROM), ppPointer);
                        CheckError(errorCode);
                        m_IsPlaying = true;
                        return;
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(ppPointer);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Sound device has already closed.");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Open()
        {
            MCI_OPEN_PARMS op = new MCI_OPEN_PARMS();
            op.dwCallback = IntPtr.Zero;
            op.lpstrDeviceType = null;
            op.lpstrElementName = this.SoundLocation;
            op.lpstrAlias = null;
            op.wDeviceID = 0;
            MCI_OpenFlags flags = 0;
            //if (deviceType != 0)
            //{
            //    flags = MCI_OpenFlags.MCI_OPEN_TYPE | MCI_OpenFlags.MCI_OPEN_TYPE_ID | MCI_OpenFlags.MCI_OPEN_ELEMENT | MCI_OpenFlags.MCI_WAIT;
            //}
            //else
            //{
                flags = MCI_OpenFlags.MCI_OPEN_ELEMENT | MCI_OpenFlags.MCI_WAIT;
            //}
            IntPtr opPointer = Marshal.AllocHGlobal(Marshal.SizeOf(op));
            try
            {
                Marshal.StructureToPtr(op, opPointer, false);
                UInt32 errorCode = mciSendCommand(0, (UInt32)MCI_Command.MCI_OPEN, (UInt32)flags, opPointer);
                CheckError(errorCode);
                MCI_OPEN_PARMS op2 = (MCI_OPEN_PARMS)Marshal.PtrToStructure(opPointer, typeof(MCI_OPEN_PARMS));
                m_DeviceID = op2.wDeviceID;
            }
            finally
            {
                Marshal.FreeHGlobal(opPointer);
            }
            MCI_SET_PARMS sp = new MCI_SET_PARMS();
            sp.dwCallback = IntPtr.Zero;
            sp.dwTimeFormat = MCI_Format.MCI_FORMAT_MILLISECONDS;
            IntPtr spPointer = Marshal.AllocHGlobal(Marshal.SizeOf(sp));
            try
            {
                Marshal.StructureToPtr(sp, spPointer, false);
                UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_SET, (UInt32)(MCI_SetFlags.MCI_SET_TIME_FORMAT | MCI_SetFlags.MCI_WAIT), spPointer);
                CheckError(errorCode);
                if (errorCode != 0)
                {
                    Close();
                }
            }
            finally
            {
                Marshal.FreeHGlobal(spPointer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void Close()
        {
            if (m_DeviceID != 0)
            {
                MCI_GENERIC_PARMS gp = new MCI_GENERIC_PARMS();
                gp.dwCallback = IntPtr.Zero;
                IntPtr gpPointer = Marshal.AllocHGlobal(Marshal.SizeOf(gp));
                try
                {
                    Marshal.StructureToPtr(gp, gpPointer, false);
                    UInt32 errorCode = mciSendCommand(m_DeviceID, (UInt32)MCI_Command.MCI_CLOSE, (UInt32)MCI_OpenFlags.MCI_WAIT, gpPointer);
                    CheckError(errorCode);
                    if (errorCode == 0)
                    {
                        m_DeviceID = 0;
                        m_Paused = false;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(gpPointer);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errCode"></param>
        private static void CheckError(UInt32 errCode)
        {
            if (errCode == 0) return;
            StringBuilder builder = new StringBuilder(255);
            if (mciGetErrorString(errCode, builder, 255) == false)
            {
                throw new MeidaSoundException("Unknown media device error.");
            }
            else
            {
                throw new MeidaSoundException(builder.ToString());
            }
        }
    }
}
