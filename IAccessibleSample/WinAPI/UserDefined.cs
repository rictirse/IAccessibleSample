namespace Win32;

public static partial class WinAPI
{
    /// <summary>
    /// Activates (gives focus to) a window.
    /// </summary>
    public static IntPtr Activate(string? lpClassName = null, string? lpWindowName = null)
    {
        var winPtr = FindWindow(lpClassName, lpWindowName);
        if (winPtr == IntPtr.Zero) return IntPtr.Zero;

        SetForegroundWindow(winPtr);
        return winPtr;
    }

    /// <summary>
    /// Checks to see if a specified window exists and is currently active.
    /// </summary>
    public static bool Active(string? lpClassName = null, string? lpWindowName = null)
    {
        var winPtr = FindWindow(lpClassName, lpWindowName);
        if (winPtr == IntPtr.Zero) return false;

        var activePtr = GetActiveWindow();

        return winPtr == activePtr;
    }

    /// <summary>
    /// Checks to see if a specified window exists.
    /// </summary>
    public static bool Exists(string? lpClassName = null, string? lpWindowName = null)
    {
        var winPtr = FindWindow(lpClassName, lpWindowName);

        return winPtr != IntPtr.Zero;
    }

    /// <summary>
    /// Retrieves the position and size of a given window.
    /// </summary>
    public static (int Left, int Top, int Right, int Bottom) GetPos(string? lpClassName = null, string? lpWindowName = null)
    {
        var winPtr = FindWindow(lpClassName, lpWindowName);
        if (winPtr == IntPtr.Zero) return (0, 0, 0, 0);

        GetWindowRect(winPtr, out RECT lpRect);

        return (lpRect.Left, lpRect.Top, lpRect.Right, lpRect.Bottom);
    }

    /// <summary>
    /// Moves and/or resizes a window.
    /// </summary>
    public static IntPtr Move
        (string lpClassName,
        string lpWindowName,
        int x,
        int y,
        int? width = null,
        int? height = null)
    {
        var winPtr = FindWindow(lpClassName, lpWindowName);
        if (winPtr == IntPtr.Zero) return IntPtr.Zero;

        if (width == null || height == null)
        {
            GetWindowRect(winPtr, out RECT lpRect);
            width = lpRect.Width;
            height = lpRect.Height;
        }

        MoveWindow(winPtr, x, y, (int)width, (int)height, true);

        return winPtr;
    }

    public static bool SetOnTop(string lpClassName, string lpWindowName, bool flag)
    {
        var winPtr = FindWindow(lpClassName, lpWindowName);
        if (winPtr == IntPtr.Zero) return false;

        SetWindowPos(winPtr,
            flag ? HWND_TOPMOST : HWND_TOP,
            0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

        return true;
    }
}