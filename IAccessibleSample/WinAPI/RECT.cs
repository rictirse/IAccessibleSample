using System.Drawing;
using System.Runtime.InteropServices;

namespace Win32;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    private int _Left;
    private int _Top;
    private int _Right;
    private int _Bottom;

    public RECT(Rectangle Rectangle)
        : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
    {
    }

    public RECT(int Left, int Top, int Right, int Bottom)
        => (_Left, _Top, _Right, _Bottom) = (Left, Top, Right, Bottom);

    public int X
    {
        get => _Left;
        set { _Left = value; }
    }

    public int Y
    {
        get => _Top;
        set { _Top = value; }
    }

    public int Left
    {
        get => _Left;
        set { _Left = value; }
    }

    public int Top
    {
        get => _Top;
        set { _Top = value; }
    }

    public int Right
    {
        get => _Right;
        set { _Right = value; }
    }

    public int Bottom
    {
        get => _Bottom;
        set { _Bottom = value; }
    }

    public int Height
    {
        get => _Bottom - _Top;
        set { _Bottom = value - _Top; }
    }

    public int Width
    {
        get => _Right - _Left;
        set { _Right = value + _Left; }
    }

    public Point Location
    {
        get => new Point(Left, Top);
        set
        {
            _Left = value.X;
            _Top = value.Y;
        }
    }

    public Size Size
    {
        get => new(Width, Height);
        set
        {
            _Right = value.Height + _Left;
            _Bottom = value.Height + _Top;
        }
    }

    public bool IsValid()
    {
        if (Size.Width <= 0 || Size.Height <= 0) return false;

        return true;
    }

    public Rectangle ToRectangle()
    {
        return new(this.Left, this.Top, this.Width, this.Height);
    }

    public static Rectangle ToRectangle(RECT Rectangle)
    {
        return Rectangle.ToRectangle();
    }

    public static RECT FromRectangle(Rectangle Rectangle)
        => new(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);

    public static implicit operator Rectangle(RECT Rectangle)
        => Rectangle.ToRectangle();

    public static implicit operator RECT(Rectangle Rectangle)
        => new(Rectangle);

    public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
        => Rectangle1.Equals(Rectangle2);

    public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
        => !Rectangle1.Equals(Rectangle2);

    public override string ToString()
        => $"{{Left: {_Left}; Top: {_Top}; Right: {_Right}; Bottom: {_Bottom}}}";

    public bool Equals(RECT Rectangle)
        => Rectangle.Left == _Left
        && Rectangle.Top == _Top
        && Rectangle.Right == _Right
        && Rectangle.Bottom == _Bottom;

    public override bool Equals(object? Object)
    {
        if (Object is RECT)
        {
            return Equals((RECT)Object);
        }
        else if (Object is Rectangle)
        {
            return Equals(new RECT((Rectangle)Object));
        }

        return false;
    }

    public override int GetHashCode()
        => Left.GetHashCode()
        ^ Right.GetHashCode()
        ^ Top.GetHashCode()
        ^ Bottom.GetHashCode();
}