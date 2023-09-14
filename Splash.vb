Public Class Splash
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ProgressBar1.Value >= 100 Then
            Timer1.Stop()
        Else
            ProgressBar1.Value = ProgressBar1.Value + 5
            ControlShowLbl.Text = ProgressBar1.Value & " % Completing...."

            If ProgressBar1.Value = 100 Then
                Login.Show()
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
    End Sub


End Class
