Imports System.Data.SqlClient

Public Class addnewmember
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private memberForm As managemember

    Public Sub New(member As managemember)
        ' This call is required by the designer.
        InitializeComponent()
        memberForm = member
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Con.Open()

        If String.IsNullOrWhiteSpace(nameTb.Text) OrElse
           dateTb.Value = Nothing OrElse
           String.IsNullOrWhiteSpace(ageTb.Text) OrElse
           String.IsNullOrWhiteSpace(Gender.Text) OrElse
           String.IsNullOrWhiteSpace(addressTb.Text) OrElse
           String.IsNullOrWhiteSpace(contactTb.Text) OrElse
           String.IsNullOrWhiteSpace(emergencyTb.Text) OrElse
           String.IsNullOrWhiteSpace(membershipPlan.Text) OrElse
           startMembership.Value = Nothing OrElse
           endMembership.Value = Nothing OrElse
           String.IsNullOrWhiteSpace(memStatus.Text) Then

            MessageBox.Show("All fields must be filled out before submitting!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Con.Close()
            Exit Sub
        End If

        If Not IsNumeric(contactTb.Text) OrElse contactTb.Text.Length <> 11 Then
            MessageBox.Show("Contact number must be exactly 11 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Con.Close()
            Exit Sub
        End If

        If Not IsNumeric(emergencyTb.Text) OrElse emergencyTb.Text.Length <> 11 Then
            MessageBox.Show("Emergency contact must be exactly 11 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Con.Close()
            Exit Sub
        End If

        Dim query As String = "INSERT INTO addTbl VALUES ('" & nameTb.Text & "', '" & dateTb.Value.ToString & "' , '" & ageTb.Text & "' , '" & Gender.Text & "' , '" & addressTb.Text & "' , '" & contactTb.Text & "' , '" & emergencyTb.Text & "' , '" & membershipPlan.Text & "' , '" & startMembership.Value.ToString & "' , '" & endMembership.Value.ToString & "', '" & memStatus.Text & "')"
        Dim cmd As New SqlCommand(query, Con)

        Dim result As Integer = cmd.ExecuteNonQuery()
        If result > 0 Then
            MessageBox.Show("Data Added")
            memberForm.load_data()

            ' Clear all inputs manually
            nameTb.Text = String.Empty
            ageTb.Text = String.Empty
            addressTb.Text = String.Empty
            contactTb.Text = String.Empty
            emergencyTb.Text = String.Empty
            membershipPlan.Text = String.Empty
            memStatus.Text = String.Empty
            Gender.SelectedIndex = -1
            dateTb.Value = DateTime.Now
            startMembership.Value = DateTime.Now
            endMembership.Value = DateTime.Now
        Else
            MessageBox.Show("Unsuccessful")
        End If

        Con.Close()
    End Sub

    Private Sub Guna2TextBox7_TextChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub memStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles memStatus.SelectedIndexChanged
    End Sub

    Private Sub emergencyTb_TextChanged(sender As Object, e As EventArgs) Handles emergencyTb.TextChanged
        Dim input = contactTb.Text

        If Not System.Text.RegularExpressions.Regex.IsMatch(input, "^[0-9]*$") Then
            MessageBox.Show("Only numbers are allowed in the Phone Number field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            contactTb.Text = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "")
            contactTb.SelectionStart = contactTb.Text.Length
            Return
        End If

        If input.Length > 11 Then
            MessageBox.Show("Phone number must be exactly 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            contactTb.Text = input.Substring(0, 11)
            contactTb.SelectionStart = contactTb.Text.Length
        End If
    End Sub

    Private Sub contactTb_TextChanged(sender As Object, e As EventArgs) Handles contactTb.TextChanged
        Dim input = contactTb.Text

        If Not System.Text.RegularExpressions.Regex.IsMatch(input, "^[0-9]*$") Then
            MessageBox.Show("Only numbers are allowed in the Phone Number field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            contactTb.Text = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "")
            contactTb.SelectionStart = contactTb.Text.Length
            Return
        End If

        If input.Length > 11 Then
            MessageBox.Show("Phone number must be exactly 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            contactTb.Text = input.Substring(0, 11)
            contactTb.SelectionStart = contactTb.Text.Length
        End If
    End Sub

    Private Sub nameTb_TextChanged(sender As Object, e As EventArgs) Handles nameTb.TextChanged
        Dim regex As New System.Text.RegularExpressions.Regex("^[a-zA-Z\s]*$")

        If Not regex.IsMatch(nameTb.Text) Then
            MessageBox.Show("Only letters and spaces are allowed in the Name field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            nameTb.Text = System.Text.RegularExpressions.Regex.Replace(nameTb.Text, "[^a-zA-Z\s]", "")
            nameTb.SelectionStart = nameTb.Text.Length
        End If
    End Sub

    Private Sub ageTb_TextChanged(sender As Object, e As EventArgs) Handles ageTb.TextChanged
        Dim input = ageTb.Text

        If Not System.Text.RegularExpressions.Regex.IsMatch(input, "^[0-9]*$") Then
            MessageBox.Show("Only numbers are allowed in the Age field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ageTb.Text = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "")
            ageTb.SelectionStart = ageTb.Text.Length
            Return
        End If

        If ageTb.Text.Length > 3 Then
            MessageBox.Show("Age must be exactly 3 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ageTb.Text = ageTb.Text.Substring(0, 3)
            ageTb.SelectionStart = ageTb.Text.Length
        End If
    End Sub
End Class
