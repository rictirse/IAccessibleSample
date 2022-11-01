using System.Runtime.InteropServices;
using System.Text;

namespace Win32;

public static partial class WinAPI
{
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hwnd, out RECT rc);

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, ShowWindow_Cmd nCmdShow);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
    [DllImport("user32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Unicode)]
    public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr PostMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr PostMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
    public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(POINT lpPoint);

    [DllImport("user32.dll", SetLastError = true)]
    internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowDC(IntPtr hwnd);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, IntPtr className, IntPtr windowTitle);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string? className, string windowTitle);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string? className, IntPtr windowTitle);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, IntPtr className, string? windowTitle);

    [DllImport("user32.dll")]
    public static extern int SetForegroundWindow(IntPtr parentHandle);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(string? lpClassName, string? lpWindowName);

    [DllImport("user32")]
    public static extern IntPtr GetActiveWindow();

    [DllImport("user32")]
    public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

    public delegate bool EnumWindowsProc(IntPtr callback, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern int EnumWindows(EnumWindowsProc hWnd, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

    [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool SetProcessDPIAware();

    [DllImport("User32.dll")]
    public static extern void SetWindowLong(IntPtr hWnd, WindowLongIndex nIndex, long dwNewLong);

    [DllImport("User32.dll")]
    public static extern long GetWindowLong(IntPtr hWnd, WindowLongIndex nIndex);

    [DllImport("User32.dll")]
    public static extern int SetWindowRgn(IntPtr hWnd, IntPtr handleRegion, bool regraw);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int width, int height);

    [DllImport("User32.dll")]
    public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, ulong colorKey, byte alpha, long flags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindow(IntPtr hWnd);
}
