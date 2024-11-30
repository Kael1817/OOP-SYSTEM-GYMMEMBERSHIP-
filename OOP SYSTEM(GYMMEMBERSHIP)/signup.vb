Imports System.Data.SqlClient

Public Class signup
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click

        'GUNA LABEL
        Me.Guna2HtmlLabel1.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel2.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel3.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel4.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel5.Parent = Me.Guna2PictureBox1

        'LABEL
        Label2.Parent = Me.Guna2PictureBox1
        Label3.Parent = Me.Guna2PictureBox1

        'txtbox
        Me.usernameTb.Parent = Guna2PictureBox1
        Me.passwordTb.Parent = Guna2PictureBox1
        Me.cpasswordTb.Parent = Guna2PictureBox1

    End Sub

    Private Sub Guna2PictureBox3_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Guna2PictureBox5_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox5.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Guna2PictureBox4_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox4.Click
        Application.Exit()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
        Form1.Show()

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles signbtn.Click
        If passwordTb.Text <> cpasswordTb.Text Then
            MessageBox.Show("Confirm Password Do not Match!")
        Else

            Con.Open()


            Dim query As String = "insert into loginTbl values ('" & usernameTb.Text & "', '" & passwordTb.Text & "')"
            Dim cmd As New SqlCommand(query, Con)

            Dim result As Integer = cmd.ExecuteNonQuery()
            If result > 0 Then
                MessageBox.Show("Sign up Successful!")

            Else

                MessageBox.Show("Invalid Username or Password!")
            End If
            Con.Close()
        End If
    End Sub

    Private Sub cpasswordTb_KeyDown(sender As Object, e As KeyEventArgs) Handles cpasswordTb.KeyDown
        If e.KeyCode = Keys.Enter Then
            signbtn.PerformClick() ' Replace 'loginButton' with the name of your login button
        End If

    End Sub
End Class