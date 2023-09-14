Imports System.Data.SqlClient

Public Class Student
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=M:\VBprgs\Project\CollgeManagementSystem\CollgeManagementSystem\CollgeVbDb.mdf;Integrated Security=True")

    Private Sub FillDepartment()
        Con.Open()
        Dim query = "Select * from DepartmentTbl"
        Dim cmd As New SqlCommand(query, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        DepCmb.DataSource = Tbl
        DepCmb.DisplayMember = "DepName"
        DepCmb.ValueMember = "DepName"
        Con.Close()
    End Sub
    Private Sub Display()

        Con.Open()
        Dim query = "Select * from StudentTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)

        StudentGRD.DataSource = ds.Tables(0)
        Con.Close()
    End Sub 'Display Code
    Private Sub Clear()
        StnameTb.Text = ""
        FeesTb.Text = ""
        STDOB.Text = ""
        phoneTb.Text = ""
        Gencmb.SelectedIndex = 0
        DepCmb.SelectedIndex = 0

    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles Savebtn.Click
        If StnameTb.Text = "" Or FeesTb.Text = "" Or STDOB.Text = "" Or phoneTb.Text = "" Then
            MsgBox("Missing Information")
        ElseIf FeesTb.Text < 0 Then
            MsgBox("Please Enter Valid Salary And Try Again.")
        ElseIf phoneTb.Text.Length.ToString() < 10 Then
            MsgBox("Please Enter Valid Phone No And Try Again.")
        ElseIf Gencmb.SelectedIndex = -1 Then
            MsgBox("You Forgot to select Gender of student Sorry!.")
        ElseIf DepCmb.SelectedIndex = -1 Then
            MsgBox("You Forgot to select Department of student Sorry!.")
        Else
            Dim CurYear, EnYear, Diff As Integer
            CurYear = Convert.ToInt32(Now.ToString("yyyy"))

            EnYear = Convert.ToInt32(STDOB.Value.ToString("yyyy"))

            Diff = CurYear - EnYear
            If Diff > 18 And Diff < 26 Then
                Try
                    Con.Open()
                    Dim query = "insert into StudentTbl values('" & StnameTb.Text & "','" & Gencmb.SelectedItem.ToString() & "','" & STDOB.Value.ToString("yyyy-MM-dd") & "','" & phoneTb.Text & "','" & DepCmb.SelectedValue.ToString() & "','" & FeesTb.Text & "')"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(query, Con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Student Saved")
                    Con.Close()
                    Display()
                    Clear()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Else
                If CurYear = EnYear Then
                    MsgBox("Birth year is Not Accepted as Current Year")
                ElseIf CurYear < EnYear Then
                    MsgBox("Student Is Not Born yet Sorry !....")
                Else
                    MsgBox("Invalid Year (Student is not Properly aged to enter in college)..")
                End If

            End If
        End If
    End Sub

    Private Sub Student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillDepartment()
        Display()
    End Sub

    Private Sub PictureBoxclose_Click(sender As Object, e As EventArgs) Handles PictureBoxclose.Click
        Application.Exit()
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Clear()
    End Sub

    Dim key = 0
    Private Sub StudentGRD_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles StudentGRD.CellMouseClick

        Dim row As DataGridViewRow = StudentGRD.Rows(e.RowIndex)
        StnameTb.Text = row.Cells(1).Value.ToString
        Gencmb.SelectedItem = row.Cells(2).Value.ToString
        STDOB.Text = row.Cells(3).Value.ToString
        phoneTb.Text = row.Cells(4).Value.ToString
        DepCmb.SelectedValue = row.Cells(5).Value.ToString
        FeesTb.Text = row.Cells(6).Value.ToString

        If StnameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If

    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles Deletebtn.Click
        If key = 0 Then
            MsgBox("Select The Student")
        Else
            Try
                Con.Open()
                Dim query = "Delete from StudentTbl where StId = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Student Deleted")
                Con.Close()
                Display()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles Editbtn.Click
        If StnameTb.Text = "" Or FeesTb.Text = "" Or STDOB.Text = "" Or phoneTb.Text = "" Or DepCmb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        ElseIf FeesTb.Text < 0 Then
            MsgBox("Please Enter Valid Salary And Try Again.")
        ElseIf phoneTb.Text.Length.ToString() < 10 Then
            MsgBox("Please Enter Valid Phone No And Try Again.")
        Else
            Try
                Con.Open()
                Dim query = "Update StudentTbl 
                                set StName = '" & StnameTb.Text & "', 
                                STDOB = '" & STDOB.Text & "',   
                                StPhone =' " & phoneTb.Text & "',   
                                StDep ='" & DepCmb.SelectedValue.ToString() & "',  
                                StFees ='" & FeesTb.Text & "'    
                                 where StId = '" & key & "' "
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Student Updated")
                Con.Close()
                Display()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub

    Private Sub NoDueList()

        Con.Open()
        Dim query = "Select * from StudentTbl where StFees >= 100000"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)

        StudentGRD.DataSource = ds.Tables(0)
        Con.Close()
    End Sub

    Private Sub BtnDue_Click(sender As Object, e As EventArgs) Handles BtnDue.Click
        NoDueList()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Display()
    End Sub

    Private Sub StudentToLogin_Click(sender As Object, e As EventArgs) Handles StudentToLogin.Click
        'Login.Show()
        Me.Hide()

    End Sub

    Private Sub StudentTechLbl_Click(sender As Object, e As EventArgs) Handles StudentTechLbl.Click
        Teacher.Show()
        Me.Hide()

    End Sub

    Private Sub StudentDepLbl_Click(sender As Object, e As EventArgs) Handles StudentDepLbl.Click
        Department.Show()
        Me.Hide()

    End Sub

    Private Sub StudentFeesLbl_Click(sender As Object, e As EventArgs) Handles StudentFeesLbl.Click
        Fees.Show()
        Me.Hide()

    End Sub

    Private Sub StudentDashLbl_Click(sender As Object, e As EventArgs) Handles StudentDashLbl.Click

        DashBoard.Show()
        Me.Hide()

    End Sub
End Class