Imports System.Data.SqlClient

Public Class forgetpass
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Guna2HtmlLabel1_Click(sender As Object, e As EventArgs) Handles Guna2HtmlLabel1.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        Form1.Show()

    End Sub

    Private Sub Guna2PictureBox3_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Guna2PictureBox5_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox5.Click
        For Each frm As Form In Application.OpenForms
            ' Check the current state of the form and toggle between Normal and Maximized
            If frm.WindowState = FormWindowState.Normal Then
                frm.WindowState = FormWindowState.Maximized
            Else
                frm.WindowState = FormWindowState.Normal
            End If
        Next
    End Sub

    Private Sub Guna2PictureBox4_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox4.Click
        Application.Exit()
    End Sub

    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click
        passwordTb.UseSystemPasswordChar = True
        cpasswordTb.UseSystemPasswordChar = True

        'GUNA LABEL
        Me.Guna2HtmlLabel1.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel2.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel3.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel4.Parent = Me.Guna2PictureBox1

        Me.Label1.Parent = Me.Guna2PictureBox1

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles resetbtn.Click
        If passwordTb.Text <> cpasswordTb.Text Then
            MessageBox.Show("Confirm Password Do not Match!")
        Else

            Con.Open()


            Dim query As String = "update loginTbl set Password = '" & passwordTb.Text & "' where Username = '" & usernameTb.Text & "' "
            Dim cmd As New SqlCommand(query, Con)

            Dim result As Integer = cmd.ExecuteNonQuery()
            If result > 0 Then
                MessageBox.Show("Update Successful!")

            Else

                MessageBox.Show("Invalid Username or Password!")
            End If
            Con.Close()
        End If
    End Sub

    Private Sub Guna2ImageButton1_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton1.Click
        If passwordTb.UseSystemPasswordChar = True Then
            ' Show the password
            passwordTb.UseSystemPasswordChar = False
            Guna2ImageButton1.Image = My.Resources.open_eye ' Icon for "show password"
        Else
            ' Hide the password
            passwordTb.UseSystemPasswordChar = True
            Guna2ImageButton1.Image = My.Resources.eye ' Icon for "hide password"
        End If
    End Sub

    Private Sub Guna2ImageButton2_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton2.Click
        If cpasswordTb.UseSystemPasswordChar = True Then
            ' Show the password
            cpasswordTb.UseSystemPasswordChar = False
            Guna2ImageButton2.Image = My.Resources.open_eye ' Icon for "show password"
        Else
            ' Hide the password
            cpasswordTb.UseSystemPasswordChar = True
            Guna2ImageButton2.Image = My.Resources.eye ' Icon for "hide password"
        End If
    End Sub

    Private Sub cpasswordTb_KeyDown(sender As Object, e As KeyEventArgs) Handles cpasswordTb.KeyDown
        If e.KeyCode = Keys.Enter Then
            resetbtn.PerformClick() ' Replace 'loginButton' with the name of your login button
        End If

    End Sub

    Private Sub cpasswordTb_TextChanged(sender As Object, e As EventArgs) Handles cpasswordTb.TextChanged

    End Sub
End Class