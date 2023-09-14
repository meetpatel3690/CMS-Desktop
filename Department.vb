Imports System.Data.SqlClient

Public Class Department
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=M:\VBprgs\Project\CollgeManagementSystem\CollgeManagementSystem\CollgeVbDb.mdf;Integrated Security=True")

    Private Sub Display()

        Con.Open()
        Dim query = "Select * from DepartmentTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        DepartmentGRD.DataSource = ds.Tables(0) '
        Con.Close()
    End Sub 'Display Code


    Private Sub Reset()
        DepNameTb.Text = ""
        DescTb.Text = ""
        DurationTb.Text = ""
    End Sub 'Reset Code


    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If DepNameTb.Text = "" Or DescTb.Text = "" Or DurationTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "insert into DepartmentTbl values('" & DepNameTb.Text & "','" & DescTb.Text & "','" & DurationTb.Text & "')"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Department Saved")
                Con.Close()
                Display()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub

    Private Sub PictureBoxclose_Click(sender As Object, e As EventArgs) Handles PictureBoxclose.Click
        Application.Exit()
    End Sub

    Private Sub Department_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
    End Sub


    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Select The Department")
        Else
            Try
                Con.Open()
                Dim query = "Delete from DepartmentTbl where DepId = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Department Deleted")
                Con.Close()
                Display()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub
    Dim key = 0

    Private Sub DepartmentGRD_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DepartmentGRD.CellMouseClick
        Dim row As DataGridViewRow = DepartmentGRD.Rows(e.RowIndex)
        DepNameTb.Text = row.Cells(1).Value.ToString
        DescTb.Text = row.Cells(2).Value.ToString
        DurationTb.Text = row.Cells(3).Value.ToString

        If DepNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If

    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If DepNameTb.Text = "" Or DescTb.Text = "" Or DurationTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim cmd As SqlCommand
                cmd = New SqlCommand("Update DepartmentTbl 
                                    set DepName = '" & DepNameTb.Text & "', 
                                    DepDesc = '" & DescTb.Text & "', 
                                    DepDur = " & DurationTb.Text & " 
                                    where DepId = " & key & " ", Con)
                cmd.ExecuteNonQuery()
                MsgBox("Department Updated")
                Con.Close()
                Display()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub

    Private Sub DepartmentGRD_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DepartmentGRD.CellContentClick

    End Sub

    Private Sub DepartmentToLogin_Click(sender As Object, e As EventArgs) Handles DepartmentToLogin.Click
        Login.Show()
        Me.Hide()


    End Sub

    Private Sub DepartmentTechLbl_Click(sender As Object, e As EventArgs) Handles DepartmentTechLbl.Click
        Teacher.Show()
        Me.Hide()


    End Sub

    Private Sub DepartmentStudLbl_Click(sender As Object, e As EventArgs) Handles DepartmentStudLbl.Click
        Student.Show()
        Me.Hide()



    End Sub

    Private Sub DepartmentFeesLbl_Click(sender As Object, e As EventArgs) Handles DepartmentFeesLbl.Click
        Fees.Show()
        Me.Hide()
    End Sub

    Private Sub DepartmentDashLbl_Click(sender As Object, e As EventArgs) Handles DepartmentDashLbl.Click
        DashBoard.Show()
        Me.Hide()

    End Sub
End Class