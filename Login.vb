Public Class Login
    Private Sub PictureBoxclose_Click(sender As Object, e As EventArgs) Handles PictureBoxclose.Click
        Application.Exit()
    End Sub


    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        UserNameTb.Text = ""
        PasswordTb.Text = ""
    End Sub

    Private Sub LoginBtn_Click(sender As Object, e As EventArgs) Handles LoginBtn.Click
        If UserNameTb.Text = "" Or PasswordTb.Text = "" Then
            MsgBox("Enter UserName And Password")
        ElseIf UserNameTb.Text = "Admin" And PasswordTb.Text = "Admin123" Then

            Student.Show()
            Me.Close()


        Else
            MsgBox("Wrong UserName Or Password")
            UserNameTb.Text = ""
            PasswordTb.Text = ""


        End If
    End Sub


End Class