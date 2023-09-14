Imports System.Data.SqlClient

Public Class DashBoard

    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=M:\VBprgs\Project\CollgeManagementSystem\CollgeManagementSystem\CollgeVbDb.mdf;Integrated Security=True")

    Private Sub CountStud()
        Dim StNum As Integer
        Con.Open()
        Dim sql = "select count(*) from StudentTbl"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        StNum = cmd.ExecuteScalar
        StdLbl.Text = StNum
        Con.Close()

    End Sub

    Private Sub CountTechers()
        Dim TNum As Integer
        Con.Open()
        Dim sql = "select count(*) from TeacherTbl"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        TNum = cmd.ExecuteScalar
        TeacherLbl.Text = TNum
        Con.Close()

    End Sub


    Private Sub CountDepartment()
        Dim DepNum As Integer
        Con.Open()
        Dim sql = "select count(*) from DepartmentTbl"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        DepNum = cmd.ExecuteScalar
        DepLbl.Text = DepNum
        Con.Close()

    End Sub


    Private Sub SumFees()
        Dim FeesAmount As Integer
        Con.Open()
        Dim sql = "select Sum(Amount) from PaymentTbl"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(sql, Con)
        FeesAmount = cmd.ExecuteScalar

        Dim Am = Convert.ToString(FeesAmount)
        FeesLbl.Text = Am + " Rs."
        Con.Close()

    End Sub

    Private Sub DashBoard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SumFees()
        CountStud()
        CountTechers()
        CountDepartment()



    End Sub

    Private Sub PictureBoxclose_Click(sender As Object, e As EventArgs) Handles PictureBoxclose.Click
        Application.Exit()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles DashBoardToLogin.Click

        Login.Show()
        Me.Hide()


    End Sub

    Private Sub DashBoardTeacLbl_Click(sender As Object, e As EventArgs) Handles DashBoardTeacLbl.Click
        Teacher.Show()
        Me.Hide()

    End Sub

    Private Sub DashBoardStudLbl_Click(sender As Object, e As EventArgs) Handles DashBoardStudLbl.Click
        Student.Show()
        Me.Hide()

    End Sub

    Private Sub DashBoardFeesLbl_Click(sender As Object, e As EventArgs) Handles DashBoardFeesLbl.Click
        Fees.Show()
        Me.Hide()


    End Sub

    Private Sub DashBoardDepLbl_Click(sender As Object, e As EventArgs) Handles DashBoardDepLbl.Click
        Department.Show()
        Me.Hide()


    End Sub


End Class