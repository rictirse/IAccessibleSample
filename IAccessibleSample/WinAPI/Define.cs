using System.Runtime.InteropServices;

namespace Win32;

static public partial class WinAPI
{
    /// <summary>
    /// Retains the current size (ignores the cx and cy parameters).
    /// </summary>
    public const UInt32 SWP_NOSIZE = 0x0001;
    /// <summary>
    /// Retains the current position (ignores X and Y parameters).
    /// </summary>
    public const UInt32 SWP_NOMOVE = 0x0002;
    /// <summary>
    /// Retains the current Z order (ignores the hWndInsertAfter parameter).
    /// </summary>
    public const UInt32 SWP_NOZORDER = 0x0004;
    /// <summary>
    /// Does not redraw changes. If this flag is set,no repainting of any kind occurs. This applies to the client area,
    /// the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved.
    /// When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
    /// </summary>
    public const UInt32 SWP_NOREDRAW = 0x0008;
    public const UInt32 SWP_NOACTIVATE = 0x0010;
    public const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
    public const UInt32 SWP_SHOWWINDOW = 0x0040;
    public const UInt32 SWP_HIDEWINDOW = 0x0080;
    public const UInt32 SWP_NOCOPYBITS = 0x0100;
    public const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
    public const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

    public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
    public static readonly IntPtr HWND_TOP = new IntPtr(0);

    public enum KEYEVENTF : uint
    {
        KEYDOWN = 0x00,
        EXTENDEDKEY = 0x01,
        KEYUP = 0x02,
    }

    [Flags]
    public enum MouseEventFlags
    {
        LEFTDOWN = 0x0002,
        LEFTUP = 0x0004,
        MIDDLEDOWN = 0x0020,
        MIDDLEUP = 0x0040,
        WHEEL = 0x0800,
        MOVE = 0x0001,
        ABSOLUTE = 0x8000,
        RIGHTDOWN = 0x0008,
        RIGHTUP = 0x0010
    }

    public enum SW
    {
        SW_HIDE = 0,
        SW_NORMAL = 1,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10
    }

    public enum VirtualKey : int
    {
        VK_PRIOR = 0x21,
        VK_NEXT = 0x22,
        VK_PAGEDOWN = 34,
        VK_PAGEUP = 33,
    }

    public enum MouseKey : int
    {
        MK_CONTROL = 0x0008,
        MK_LBUTTON = 0x0001,
        MK_MBUTTON = 0x0010,
        MK_RBUTTON = 0x0002,
        MK_SHIFT = 0x0004,
        MK_XBUTTON1 = 0x0020,
        MK_XBUTTON2 = 0x0040,
    }

    public enum ScrollBarCommands : int
    {
        SB_LINEUP = 0x0,
        SB_LINEDOWN = 0x1,
    }

    public enum WindowMessage : int
    {
        WM_VSCROLL = 0x115,
        WM_HSCROLL = 0x114,
        WM_KEYDOWN = 0x100,
        WM_KEYUP = 0x101,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEWHEEL = 0x020A,
    }

    public enum GetWindow_Cmd : uint
    {
        GW_HWNDFIRST = 0,
        GW_HWNDLAST = 1,
        GW_HWNDNEXT = 2,
        GW_HWNDPREV = 3,
        GW_OWNER = 4,
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6
    }

    public enum ShowWindow_Cmd : uint
    {
        SW_HIDE = 0,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_FORCEMINIMIZE = 11,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowPlacement
    {
        public int length;
        public int flags;
        public ShowWindow_Cmd showCmd;
        public POINT minPosition;
        public POINT maxPosition;
        public RECT normalPosition;
    }

    public enum WindowLongIndex : int
    {
        GWL_WNDPROC = -4,
        GWL_HINSTANCE = -6,
        GWL_HWNDPARENT = -8,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_EXSTYLE = -20,
        GWL_USERDATA = -21,
        DWL_MSGRESULT = 0,
        DWL_DLGPROC = 4,
        DWL_USER = 8,
    }

    [Flags]
    public enum WindowStyles : uint
    {
        OVERLAPPED = 0,
        POPUP = 0x80000000,
        CHILD = 0x40000000,
        MINIMIZE = 0x20000000,
        VISIBLE = 0x10000000,
        DISABLED = 0x8000000,
        CLIPSIBLINGS = 0x4000000,
        CLIPCHILDREN = 0x2000000,
        MAXIMIZE = 0x1000000,
        CAPTION = 0xC00000,
        BORDER = 0x800000,
        DLGFRAME = 0x400000,
        VSCROLL = 0x200000,
        HSCROLL = 0x100000,
        SYSMENU = 0x80000,
        THICKFRAME = 0x40000,
        GROUP = 0x20000,
        TABSTOP = 0x10000,
        MINIMIZEBOX = 0x20000,
        MAXIMIZEBOX = 0x10000,
        TILED = OVERLAPPED,
        ICONIC = MINIMIZE,
        SIZEBOX = THICKFRAME,
    }

    [Flags]
    public enum ExtendedWindowStyles : uint
    {
        DLGMODALFRAME = 0x0001,
        NOPARENTNOTIFY = 0x0004,
        TOPMOST = 0x0008,
        ACCEPTFILES = 0x0010,
        TRANSPARENT = 0x0020,
        MDICHILD = 0x0040,
        TOOLWINDOW = 0x0080,
        WINDOWEDGE = 0x0100,
        CLIENTEDGE = 0x0200,
        CONTEXTHELP = 0x0400,
        RIGHT = 0x1000,
        LEFT = 0x0000,
        RTLREADING = 0x2000,
        LTRREADING = 0x0000,
        LEFTSCROLLBAR = 0x4000,
        RIGHTSCROLLBAR = 0x0000,
        CONTROLPARENT = 0x10000,
        STATICEDGE = 0x20000,
        APPWINDOW = 0x40000,
        OVERLAPPEDWINDOW = (WINDOWEDGE | CLIENTEDGE),
        PALETTEWINDOW = (WINDOWEDGE | TOOLWINDOW | TOPMOST),
        LAYERED = 0x00080000,
        /// <summary> Disable inheritence of mirroring by children </summary>
        NOINHERITLAYOUT = 0x00100000,
        /// <summary> Right to left mirroring </summary>
        LAYOUTRTL = 0x00400000,
        COMPOSITED = 0x02000000,
        NOACTIVATE = 0x08000000,
    }

    public const long LWA_ALPHA = 0x00000002L;

    public const long LWA_COLORKEY = 0x00000001L;
}