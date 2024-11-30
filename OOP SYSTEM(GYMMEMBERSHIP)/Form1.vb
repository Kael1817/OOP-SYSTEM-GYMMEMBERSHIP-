Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Guna.UI2.WinForms

Public Class Form1
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click

        passwordTb.UseSystemPasswordChar = True

        'GUNA LABEL
        Me.Guna2HtmlLabel1.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel2.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel3.Parent = Me.Guna2PictureBox1
        Me.Guna2HtmlLabel4.Parent = Me.Guna2PictureBox1

        'LABEL
        Label1.Parent = Me.Guna2PictureBox1
        Label2.Parent = Me.Guna2PictureBox1

        'TEXTBOX
        Me.passwordTb.Parent = Guna2PictureBox1
        Me.usernameTb.Parent = Guna2PictureBox1

        'BUTTON
        Me.logbtn.Parent = Guna2PictureBox1
        'PICBOX

        Guna2PictureBox2.Parent = Me.Guna2PictureBox1

        Me.Guna2PictureBox1.Refresh()
    End Sub

    Private Sub Guna2PictureBox4_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox4.Click
        Application.Exit()
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

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.Hide()
        signup.Show()

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles logbtn.Click
        Try
            If String.IsNullOrWhiteSpace(usernameTb.Text) Or String.IsNullOrWhiteSpace(passwordTb.Text) Then
                MessageBox.Show("Username and Password cannot be empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return ' Exit the method early
            End If

            ' Create separate connections for the staff and admin databases
            Dim staffCon As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")
            Dim adminCon As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")

            ' Open connections
            staffCon.Open()
            adminCon.Open()

            ' Query to check if the username and password match in the staff database
            Dim staffQuery As String = "SELECT COUNT(*) FROM loginTbl WHERE Username = @Username AND Password = @Password"
            Dim staffCmd As New SqlCommand(staffQuery, staffCon)
            staffCmd.Parameters.AddWithValue("@Username", usernameTb.Text)
            staffCmd.Parameters.AddWithValue("@Password", passwordTb.Text)

            ' Query to check if the username and password match in the admin database
            Dim adminQuery As String = "SELECT COUNT(*) FROM adminloginTbl WHERE Username = @Username AND Password = @Password"
            Dim adminCmd As New SqlCommand(adminQuery, adminCon)
            adminCmd.Parameters.AddWithValue("@Username", usernameTb.Text)
            adminCmd.Parameters.AddWithValue("@Password", passwordTb.Text)

            ' Execute the queries and check the results
            Dim staffResult As Integer = Convert.ToInt32(staffCmd.ExecuteScalar())
            Dim adminResult As Integer = Convert.ToInt32(adminCmd.ExecuteScalar())

            ' Check if the login is successful for either staff or admin
            If staffResult > 0 Then
                MessageBox.Show("Staff Login Successfully!")
                Dim dashboard As New dashboard() ' Replace with your staff dashboard form
                dashboard.Show()
                Me.Hide()
            ElseIf adminResult > 0 Then
                MessageBox.Show("Admin Login Successfully!")
                Dim dashboard As New dashboard() ' Replace with your admin dashboard form
                dashboard.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid Username or Password!")
            End If

            ' Close connections
            staffCon.Close()
            adminCon.Close()

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        End Try
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        forgetpass.Show()


    End Sub

    Private Sub Guna2PictureBox6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Guna2ImageButton1_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton1.Click
        ' Toggle the UseSystemPasswordChar property
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

    Private Sub passwordTb_KeyDown(sender As Object, e As KeyEventArgs) Handles passwordTb.KeyDown
        If e.KeyCode = Keys.Enter Then
            logbtn.PerformClick()
        End If

    End Sub

    Private Sub passwordTb_TextChanged(sender As Object, e As EventArgs) Handles passwordTb.TextChanged

    End Sub
End Class
