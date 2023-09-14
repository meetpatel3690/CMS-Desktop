Imports System.Data.SqlClient
Public Class Fees

    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=M:\VBprgs\Project\CollgeManagementSystem\CollgeManagementSystem\CollgeVbDb.mdf;Integrated Security=True")

    Private Sub FillStudent()
        Con.Open()
        Dim query = "Select * from StudentTbl"
        Dim cmd As New SqlCommand(query, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        StIdcb.DataSource = Tbl
        StIdcb.DisplayMember = "StId"
        StIdcb.ValueMember = "StId"
        Con.Close()
    End Sub
    Private Sub Display()

        Con.Open()
        Dim query = "Select * from PaymentTbl"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, Con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)

        FeesGRD.DataSource = ds.Tables(0)
        Con.Close()
    End Sub

    Private Sub Clear()

        StName.Text = ""
        AmountTb.Text = ""
        StIdcb.SelectedIndex = -1
    End Sub


    Private Sub UpdateStudent()
        Try
            Con.Open()

            Dim query = "Update StudentTbl set StFees ='" & AmountTb.Text & "'    
                                 where StId = '" & StIdcb.SelectedValue.ToString() & "' "
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Student Updated")
            Con.Close()
            'Display()
            ' Reset()
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub
    Private Sub Paybtn_Click(sender As Object, e As EventArgs) Handles Paybtn.Click
        If StName.Text = "" Or AmountTb.Text = "" Then
            MsgBox("Missing Information")
        ElseIf Convert.ToInt32(AmountTb.Text) > 100000 Or
             Convert.ToInt32(AmountTb.Text) < 1 Then
            MsgBox("Amount Can not be nagative...")
        Else
            AmountTb.Text= AmountTb.Text
            Try
                Con.Open()
                Dim query = "insert into PaymentTbl values(" & StIdcb.SelectedValue.ToString() & ",'" & StName.Text & "','" & PeriodDate.Value.ToString("yyyy-MM-dd") & "'," & AmountTb.Text & ")"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Payment Successfully Done ")
                Con.Close()
                Display()
                UpdateStudent()
                Clear()
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try
        End If
    End Sub
    Private Sub GetStName()
        Con.Open()
        Dim query = "Select * from StudentTbl where StId = " & StIdcb.SelectedValue.ToString() & ""
        Dim cmd As New SqlCommand(query, Con)
        'Dim dt As DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()

        While reader.Read
            StName.Text = reader(1).ToString()
        End While
        Con.Close()
    End Sub
    Private Sub Fees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
        FillStudent()
    End Sub

    Private Sub StIdcb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles StIdcb.SelectionChangeCommitted
        GetStName()
    End Sub

    Private Sub PictureBoxclose_Click(sender As Object, e As EventArgs) Handles PictureBoxclose.Click
        Application.Exit()
    End Sub

    Private Sub FeesToLogin_Click(sender As Object, e As EventArgs) Handles FeesToLogin.Click
        Login.Show()
        Me.Hide()


    End Sub

    Private Sub AccountTeacLbl_Click(sender As Object, e As EventArgs) Handles AccountTeacLbl.Click
        Teacher.Show()
        Me.Hide()


    End Sub

    Private Sub AccountDepLbl_Click(sender As Object, e As EventArgs) Handles AccountDepLbl.Click
        Department.Show()
        Me.Hide()


    End Sub

    Private Sub AccountStudLbl_Click(sender As Object, e As EventArgs) Handles AccountStudLbl.Click
        Student.Show()
        Me.Hide()


    End Sub

    Private Sub AccountDashLbl_Click(sender As Object, e As EventArgs) Handles AccountDashLbl.Click
        DashBoard.Show()
        Me.Hide()

    End Sub
End Class