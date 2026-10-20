Public Class about
    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Me.Hide()
        ' إظهار النموذج الرئيسي إذا كان موجوداً، وإلا إنشاء جديد
        Dim main As FrmMain = Application.OpenForms.OfType(Of FrmMain).FirstOrDefault()
        If main Is Nothing Then
            main = New FrmMain()
        End If
        main.Show()
    End Sub

    Private Sub about_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

    End Sub

    Private Sub about_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Hide()
        End If
    End Sub



End Class