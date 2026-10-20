Public Class frmsplash
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Hide()   ' ← إخفاء splash قبل فتح login
        Dim loginForm As New frmLogin()
        loginForm.IsInitialLogin = True
        loginForm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub frmsplash_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub
End Class