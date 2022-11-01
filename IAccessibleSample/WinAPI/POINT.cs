using System.Drawing;
using System.Runtime.InteropServices;

namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;

    public POINT(int x, int y)
        => (X, Y) = (x, y);

    public static implicit operator Point(POINT p)
        => new Point(p.X, p.Y);

    public static implicit operator POINT(Point p)
        => new POINT(p.X, p.Y);
}