Imports System.Runtime.InteropServices
Imports System.Reflection

Public Class KeyboardJammer
  Private Delegate Function HookCallback(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
  Private Shared HookDelegate As HookCallback
  Private Shared HookId As Integer
  Private Const Wh_Keyboard_LL As Integer = 13
  Private Const Vk_Tab As Integer = 9
  Private Const Vk_Escape As Integer = 27
  Private Shared Function KeyBoardHookProc(ByVal nCode As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    'All keyboard events will be sent here.
    'Don't process just pass along.
    If nCode < 0 Then
      Return CallNextHookEx(HookId, nCode, wParam, lParam)
    End If
    'Extract the keyboard structure from the lparam
    'This will contain the virtual key and any flags.
    'This is using the my.computer.keyboard to get the
    'flags instead
    Dim KeyboardSruct As KBDLLHOOKSTRUCT = Marshal.PtrToStructure(lParam, GetType(KBDLLHOOKSTRUCT))
    If KeyboardSruct.vkCode = Vk_Tab And My.Computer.Keyboard.AltKeyDown Then
      'Alt Tab
      Return 1
    ElseIf KeyboardSruct.vkCode = Vk_Escape And My.Computer.Keyboard.CtrlKeyDown Then
      'Control Escape
      Return 1
    End If
    'Send the message along 
    Return CallNextHookEx(HookId, nCode, wParam, lParam)
  End Function
  Public Shared Sub Jam()
    'Add the low level keyboard hook
    If HookId = 0 Then
      HookDelegate = AddressOf KeyBoardHookProc
      HookId = SetWindowsHookEx(Wh_Keyboard_LL, HookDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly.GetModules()(0)), 0)
      If HookId = 0 Then
        'error
      End If
    End If
  End Sub
  Public Shared Sub UnJam()
    'Remove the hook
    UnhookWindowsHookEx(HookId)
  End Sub
  <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
  Private Shared Function CallNextHookEx(
     ByVal idHook As Integer,
     ByVal nCode As Integer,
     ByVal wParam As IntPtr,
     ByVal lParam As IntPtr) As Integer
  End Function
  <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)>
  Private Shared Function SetWindowsHookEx(
         ByVal idHook As Integer,
         ByVal HookProc As HookCallback,
         ByVal hInstance As IntPtr,
         ByVal wParam As Integer) As Integer
  End Function
  <DllImport("user32.dll", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall, SetLastError:=True)>
  Private Shared Function UnhookWindowsHookEx(ByVal idHook As Integer) As Integer
  End Function
  Private Structure KBDLLHOOKSTRUCT
    Public vkCode As Integer
    Public scanCode As Integer
    Public flags As Integer
    Public time As Integer
    Public dwExtraInfo As IntPtr
  End Structure

End Class
