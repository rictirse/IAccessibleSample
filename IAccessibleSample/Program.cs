using System.Diagnostics;
using System.Text;
using Win32;

namespace IAccessibleSample;

public class Program
{
    public async static Task Main(string[] args)
    {
        var notepadProcess = Process.Start("notepad.exe");

        do
        {
            await Task.Delay(50);
            notepadProcess.Refresh();
        } while (notepadProcess.MainWindowHandle == IntPtr.Zero);

        try
		{
            Console.WriteLine($"Notepad handle: 0x{notepadProcess.MainWindowHandle:X}");
			var notepadRoot = SystemAccessibleObject.FromWindow(notepadProcess.MainWindowHandle, AccessibleObjectID.OBJID_WINDOW);

            ///找NOTEPAD上的edit control
            var editSAO = notepadRoot
                ?.Children?.ElementAtOrDefault(3)
                ?.Children?.ElementAtOrDefault(0)
                ?.Children?.ElementAtOrDefault(3);

            ///SAO寫入文字
            editSAO!.Value = "SAO寫入文字";
            ///SAO取得文字
            Console.WriteLine($"剛剛寫入的文字: {editSAO?.Value}");
            ///WinAPI寫入文字
            var editHWND = (IntPtr)editSAO!.Window!;
            WinAPI.SendMessage(editHWND, 0x000C, IntPtr.Zero, "Win32API寫入文字");
            ///WinAPI取得文字
            var getText = new StringBuilder(128);
            WinAPI.GetWindowText(editHWND, getText, getText.Capacity);
            Console.WriteLine($"WinAPI寫入的文字: {editSAO?.Value}");
        }
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		finally
		{
            notepadProcess.CloseMainWindow();
        }
    }
}
