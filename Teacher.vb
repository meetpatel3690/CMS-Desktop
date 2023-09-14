Imports System.Data.SqlClient
Public Class Teacher
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=M:\VBprgs\Project\CollgeManagementSystem\CollgeManagementSystem\CollgeVbDb.mdf;Integrated Security=True")

    Private Sub FillDepartment()
        Con.Open()
        Dim query = "Select * from DepartmentTbl"
        Dim cmd As New SqlCommand(query, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        TDepcmb.DataSource = Tbl
        TDepcmb.DisplayMember = "DepName"
        TDepcmb.ValueMember = "DepName"
        Con.Close()
    End Sub
    Private Sub Display()

        Con.Open()
        Dim query = "Select * from TeacherTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)

        TeacherGRD.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Clear()
        TnameTb.Text = ""
        TaddTb.Text = ""
        TGencmb.Text = ""
        TphoneTb.Text = ""
        TGencmb.SelectedIndex = 0
        TDepcmb.SelectedIndex = 0
    End Sub
    Private Sub Savebtn_Click(sender As Object, e As EventArgs) Handles Savebtn.Click
        If TnameTb.Text = "" Or TaddTb.Text = "" Or TGencmb.Text = "" Or TphoneTb.Text = "" Or TGencmb.SelectedIndex = -1 Or TDepcmb.SelectedIndex = -1 Then
            MsgBox("Missing Information")

        ElseIf TphoneTb.Text.Length.ToString() < 10 Then
            MsgBox("Please Enter Valid Phone No And Try Again.")
        ElseIf TGencmb.SelectedIndex = -1 Then
            MsgBox("You Forgot to select Gender of Teacher Sorry!...Try Again.")
        ElseIf TDepCmb.SelectedIndex = -1 Then
            MsgBox("You Forgot to select Department of Teacher Sorry!...Try Again.")
        Else
            Dim CurYear, EnYear, Diff As Integer
            CurYear = Convert.ToInt32(Now.ToString("yyyy"))

            EnYear = Convert.ToInt32(TDOB.Value.ToString("yyyy"))

            Diff = CurYear - EnYear
            If EnYear <= CurYear Then
                If Diff < 53 Then
                    Try
                        Con.Open()
                        Dim query = "insert into TeacherTbl values('" & TnameTb.Text & "','" & TGencmb.SelectedItem.ToString() & "','" & TDOB.Value.ToString("yyyy-MM-dd") & "','" & TphoneTb.Text & "','" & TDepcmb.SelectedValue.ToString() & "','" & TaddTb.Text & "')"
                        Dim cmd As SqlCommand
                        cmd = New SqlCommand(query, Con)
                        cmd.ExecuteNonQuery()
                        MsgBox("Teacher Saved")
                        Con.Close()
                        Display()
                        Clear()
                    Catch ex As Exception
                        MsgBox(ex.Message)

                    End Try
                Else
                    MsgBox("Teacher  Age is more than 53 Years so , Teacher should be retired ....")
                End If
            Else
                MsgBox("You can not Add the Teacher is Future....")
            End If

        End If
    End Sub

    Private Sub Teacher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
        FillDepartment()
    End Sub

    Private Sub Resetbtn_Click(sender As Object, e As EventArgs) Handles Resetbtn.Click
        Clear()
    End Sub

    Private Sub PictureBoxclose_Click(sender As Object, e As EventArgs) Handles PictureBoxclose.Click
        Application.Exit()
    End Sub

    Private Sub Deletebtn_Click(sender As Object, e As EventArgs) Handles Deletebtn.Click
        If key = 0 Then
            MsgBox("Select The Teacher")
        Else
            Try
                Con.Open()
                Dim query = "Delete from TeacherTbl where TId = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Teacher Deleted")
                Con.Close()
                Display()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub
    Dim key = 0
    Private Sub TeacherGRD_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles TeacherGRD.CellMouseClick

        Dim row As DataGridViewRow = TeacherGRD.Rows(e.RowIndex)
        TnameTb.Text = row.Cells(1).Value.ToString
        TGencmb.SelectedItem = row.Cells(2).Value.ToString
        TDOB.Text = row.Cells(3).Value.ToString
        TphoneTb.Text = row.Cells(4).Value.ToString
        TDepcmb.SelectedValue = row.Cells(5).Value.ToString
        TaddTb.Text = row.Cells(6).Value.ToString

        If TnameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub Editbtn_Click(sender As Object, e As EventArgs) Handles Editbtn.Click
        If TnameTb.Text = "" Or TaddTb.Text = "" Or TGencmb.Text = "" Or TphoneTb.Text = "" Or TGencmb.SelectedIndex = -1 Or TDepcmb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "Update TeacherTbl 
                                set TName = '" & TnameTb.Text & "', 
                                TGender = '" & TGencmb.Text & "',   
                                TDOB = '" & TDOB.Text & "',   
                                TPhone =' " & TphoneTb.Text & "',   
                                TDep ='" & TDepcmb.SelectedValue.ToString() & "',  
                                TAdd ='" & TaddTb.Text & "'    
                                 where TId = '" & key & "' "
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Teacher Updated")
                Con.Close()
                Display()
                Reset()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If


    End Sub


    Private Sub TeacherToLogin_Click(sender As Object, e As EventArgs) Handles TeacherToLogin.Click
        Login.Show()
        Me.Hide()

    End Sub

    Private Sub TeacherDesLbl_Click(sender As Object, e As EventArgs) Handles TeacherDesLbl.Click
        DashBoard.Show()
        Me.Hide()

    End Sub

    Private Sub TeacherFeesLbl_Click(sender As Object, e As EventArgs) Handles TeacherFeesLbl.Click
        Fees.Show()
        Me.Hide()


    End Sub

    Private Sub TeacherDepLbl_Click(sender As Object, e As EventArgs) Handles TeacherDepLbl.Click
        Department.Show()
        Me.Hide()


    End Sub

    Private Sub TeacherStudLbl_Click(sender As Object, e As EventArgs) Handles TeacherStudLbl.Click
        Student.Show()
        Me.Hide()

    End Sub
End Class