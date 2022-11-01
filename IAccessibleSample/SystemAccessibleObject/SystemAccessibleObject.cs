using Accessibility;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace IAccessibleSample;

public class SystemAccessibleObject : IDisposable
{
    private bool m_disposed = false;
    public IAccessible? Accessible { get; init; }
    public int ChildID { get; private set; }

    public SystemAccessibleObject(IAccessible iacc, int childID = 0)
    {
        if (iacc == null)
            throw new ArgumentNullException();

        if (childID != 0)
        {
            try
            {
                object? realChild = null;

                try
                {
                    realChild = iacc.get_accChild(childID);
                }
                catch (Exception)
                {
                }

                if (realChild != null)
                {
                    iacc = (IAccessible)realChild;
                    childID = 0;
                }
            }
            catch (ArgumentException)
            {
            }
        }

        Accessible = iacc;
        ChildID = childID;
    }

    public void Dispose()
    {
        if (!m_disposed)
        {
            m_disposed = true;
        }

        GC.SuppressFinalize(this);
    }

    public static SystemAccessibleObject FromPoint(int x, int y)
    {
        IAccessible iacc;
        object ci;

        POINT position = new POINT();
        position.x = x;
        position.y = y;

        IntPtr result = AccessibleObjectFromPoint(position, out iacc, out ci);

        if (result != IntPtr.Zero)
            throw new Exception("AccessibleObjectFromPoint returned " + result.ToInt32());

        return new SystemAccessibleObject(iacc, (int)(ci ?? 0));
    }

    public static SystemAccessibleObject FromWindow(IntPtr window, AccessibleObjectID objectID)
    {
        IAccessible iacc = (IAccessible)AccessibleObjectFromWindow(window, (uint)objectID, new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}"));
        return new SystemAccessibleObject(iacc, 0);
    }

    public static SystemAccessibleObject MouseCursor
    {
        get { return FromWindow(IntPtr.Zero, AccessibleObjectID.OBJID_CURSOR); }
    }

    public static SystemAccessibleObject? Caret
    {
        get
        {
            try
            {
                return FromWindow(IntPtr.Zero, AccessibleObjectID.OBJID_CARET);
            }
            catch
            {
                return null;
            }
        }
    }

    public static string RoleToString(int roleNumber)
    {
        var sb = new StringBuilder(1024);
        var result = GetRoleText((uint)roleNumber, sb, 1024);
        if (result == 0) throw new Exception("Invalid role number");
        return sb.ToString();
    }

    public static string StateToString(int? stateNumber)
    {
        var sn = stateNumber ?? 0;
        if (sn == 0) return "None";
        var lowBit = sn! & -sn;
        var restBits = sn - lowBit;
        var statusStr = StateBitToString(lowBit);
        if (restBits == 0) return statusStr;

        return $"{StateToString(restBits)}, {statusStr}";
    }

    public static string StateBitToString(int stateBit)
    {
        StringBuilder sb = new StringBuilder(1024);
        uint result = GetStateText((uint)stateBit, sb, 1024);
        if (result == 0) throw new Exception("Invalid role number");
        return sb.ToString();
    }

    public string? Description => Accessible?.get_accDescription(ChildID);

    public string? Name
    {
        get { return Accessible?.get_accName(ChildID); }
        set { Accessible?.set_accName(ChildID, value); }
    }

    public object? Role => Accessible?.get_accRole(ChildID);


    public int RoleIndex
    {
        get
        {
            object? role = Role;
            if (role is int)
            {
                return (int)role;
            }
            else
            {
                return -1;
            }
        }
    }

    public string? RoleString
    {
        get
        {
            switch (Role)
            {
                case int i:
                    return RoleToString(i);
                case string s:
                    return s;
                default:
                    return Role?.ToString();
            }
        }
    }

    /// <summary>
    /// 位置(X,Y,W,H)
    /// </summary>
    public Rectangle? Location
    {
        get
        {
            if (Accessible == null) return null;
            Accessible.accLocation(out var x, out var y, out var w, out var h, ChildID);
            return new Rectangle(x, y, w, h);
        }
    }

    public string? Value
    {
        get { return Accessible?.get_accValue(ChildID); }
        set { Accessible?.set_accValue(ChildID, value); }
    }

    public int? State => (int?)Accessible?.get_accState(ChildID); 

    public string StateString => StateToString(State); 
    
    public bool Visible
    {
        get { return (State & (int)ObjectStates.STATE_SYSTEM_INVISIBLE) == 0; }
    }

    /// <summary>
    /// 父項目
    /// </summary>
    public SystemAccessibleObject? Parent
    {
        get
        {
            if (Accessible == null) return null;
            if (ChildID != 0) return new SystemAccessibleObject(Accessible, 0);
            var parent = (IAccessible)Accessible.accParent;
            if (parent == null) return null;
            return new SystemAccessibleObject(parent, 0);
        }
    }

    public string? KeyboardShortcut
    {
        get
        {
            try
            {
                return Accessible?.get_accKeyboardShortcut(ChildID);
            }
            catch
            {
                return null;
            }
        }
    }

    public string? DefaultAction
    {
        get
        {
            try
            {
                return Accessible?.get_accDefaultAction(ChildID);
            }
            catch 
            { 
                return null; 
            }
        }
    }

    public void DoDefaultAction()
    {
        Accessible?.accDoDefaultAction(ChildID);
    }

    public IEnumerable<SystemAccessibleObject> SelectedObjects
    {
        get
        {
            if (ChildID != 0) return new SystemAccessibleObject[0];
            object? sel;
            try
            {
                sel = Accessible?.accSelection;
            }
            catch (NotImplementedException)
            {
                return new SystemAccessibleObject[0];
            }
            catch (COMException)
            {
                return new SystemAccessibleObject[0];
            }

            if (sel == null) return new SystemAccessibleObject[0];
            if (sel is IEnumVARIANT)
            {
                IEnumVARIANT e = (IEnumVARIANT)sel;
                e.Reset();
                var retval = new List<SystemAccessibleObject>();
                object[] tmp = new object[1];
                while (e.Next(1, tmp, IntPtr.Zero) == 0)
                {
                    if (tmp[0] is int && (int)tmp[0] < 0) break;
                    retval.Add(ObjectToSAO(tmp[0]));
                }
                return retval.ToArray();
            }
            else
            {
                if (sel is int && (int)sel < 0)
                {
                    return new SystemAccessibleObject[0];
                }
                return new SystemAccessibleObject[] { ObjectToSAO(sel) };
            }
        }
    }

    private SystemAccessibleObject? ObjectToSAO(object? obj)
    {
        if (Accessible == null) return null;
        switch (obj)
        {
            case null: 
                return null;
            case int i:
                return new SystemAccessibleObject(Accessible, i);
            default:
                return new SystemAccessibleObject((IAccessible)obj, 0);
        }
    }

    /// <summary>
    /// Window handle
    /// </summary>
    public IntPtr? Window
    {
        get
        {
            if (Accessible == null) return null;    
            WindowFromAccessibleObject(Accessible, out var hwnd);

            return hwnd;
        }
    }

    /// <summary>
    /// 子項目
    /// </summary>
    public IEnumerable<SystemAccessibleObject>? Children
    {
        get
        {
            if (Accessible == null) return null;
            if (ChildID != 0) return new SystemAccessibleObject[0];

            int cs = Accessible.accChildCount, csReal;
            object[] children = new object[cs * 2];

            uint result = AccessibleChildren(Accessible, 0, cs * 2, children, out csReal);
            if (result != 0 && result != 1)
                return new SystemAccessibleObject[0];
            if (csReal == 1 && children[0] is int && (int)children[0] < 0)
                return new SystemAccessibleObject[0];
            var retval = new SystemAccessibleObject[csReal];
            for (int i = 0; i < retval.Length; i++)
            {
                retval[i] = ObjectToSAO(children[i]);
            }
            return retval;
        }
    }
    #region Equals and HashCode 

    public override bool Equals(System.Object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        var sao = obj as SystemAccessibleObject;
        return Equals(sao);
    }

    public bool Equals(SystemAccessibleObject? sao)
    {
        if ((object?)sao == null)
        {
            return false;
        }
        return ChildID == sao.ChildID && DeepEquals(Accessible, sao.Accessible);
    }

    private static bool DeepEquals(IAccessible ia1, IAccessible ia2)
    {
        if (ia1.Equals(ia2)) return true;
        if (Marshal.GetIUnknownForObject(ia1) == Marshal.GetIUnknownForObject(ia2)) return true;
        if (ia1.accChildCount != ia2.accChildCount) return false;
        SystemAccessibleObject sa1 = new SystemAccessibleObject(ia1, 0);
        SystemAccessibleObject sa2 = new SystemAccessibleObject(ia2, 0);
        if (sa1.Window != sa2.Window) return false;
        if (sa1.Location != sa2.Location) return false;
        if (sa1.DefaultAction != sa2.DefaultAction) return false;
        if (sa1.Description != sa2.Description) return false;
        if (sa1.KeyboardShortcut != sa2.KeyboardShortcut) return false;
        if (sa1.Name != sa2.Name) return false;
        if (!sa1.Role.Equals(sa2.Role)) return false;
        if (sa1.State != sa2.State) return false;
        if (sa1.Value != sa2.Value) return false;
        if (sa1.Visible != sa2.Visible) return false;
        if (ia1.accParent == null && ia2.accParent == null) return true;
        if (ia1.accParent == null || ia2.accParent == null) return false;
        bool de = DeepEquals((IAccessible)ia1.accParent, (IAccessible)ia2.accParent);
        return de;
    }

    public override int GetHashCode()
    {
        return ChildID ^ Accessible.GetHashCode();
    }

    public static bool operator ==(SystemAccessibleObject a, SystemAccessibleObject b)
    {
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }
        return a.Accessible == b.Accessible && a.ChildID == b.ChildID;
    }

    public static bool operator !=(SystemAccessibleObject a, SystemAccessibleObject b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        try
        {
            return Name + " [" + RoleString + "]";
        }
        catch
        {
            return "??";
        }
    }
    #endregion
    #region PInvoke Declarations 

    [DllImport("oleacc.dll")]
    private static extern IntPtr AccessibleObjectFromPoint(POINT pt, [Out, MarshalAs(UnmanagedType.Interface)] out IAccessible accObj, [Out] out object ChildID);
    [DllImport("oleacc.dll")]
    private static extern uint GetRoleText(uint dwRole, [Out] StringBuilder lpszRole, uint cchRoleMax);

    [DllImport("oleacc.dll", ExactSpelling = true, PreserveSig = false)]
    [return: MarshalAs(UnmanagedType.Interface)]
    private static extern object AccessibleObjectFromWindow(
        IntPtr hwnd,
        uint dwObjectID,
        [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

    [DllImport("oleacc.dll")]
    private static extern uint GetStateText(uint dwStateBit, [Out] StringBuilder lpszStateBit, uint cchStateBitMax);

    [DllImport("oleacc.dll")]
    private static extern uint WindowFromAccessibleObject(IAccessible pacc, out IntPtr phwnd);

    [DllImport("oleacc.dll")]
    private static extern uint AccessibleChildren(IAccessible paccContainer, int iChildStart, int cChildren, [Out] object[] rgvarChildren, out int pcObtained);

    private class POINT
    {
        public int x;
        public int y;
    }
    #endregion
}
