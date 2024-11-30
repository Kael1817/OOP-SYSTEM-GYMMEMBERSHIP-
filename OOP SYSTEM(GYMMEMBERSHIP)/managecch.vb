Imports System.Data.SqlClient
Imports Guna.UI2.WinForms

Public Class managecch
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")

    Public Sub load_data()
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim Str As String = "SELECT * FROM coach"

            Dim Search As New SqlDataAdapter(Str, Con)
            Dim ds As DataSet = New DataSet
            Search.Fill(ds, "coach")
            Guna2DataGridView1.DataSource = ds.Tables("coach")
            Con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            Con.Close()
        End Try
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim selectedRow As DataGridViewRow = Guna2DataGridView1.CurrentRow
            If selectedRow Is Nothing Then
                MessageBox.Show("No record selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim id As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)

            Dim updatedName As String = nameTb.Text
            Dim updatedGender As String = genderCb.Text
            Dim updatedAddress As String = addTb.Text
            Dim updatedPhone As String = phonenumTb.Text

            If String.IsNullOrWhiteSpace(updatedName) OrElse String.IsNullOrWhiteSpace(updatedGender) OrElse String.IsNullOrWhiteSpace(updatedAddress) OrElse String.IsNullOrWhiteSpace(updatedPhone) Then
                MessageBox.Show("All fields must be filled out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim query As String = "UPDATE coach SET name = @name, gender = @gender, address = @address, phonenumber = @phonenumber WHERE ID = @ID"
            Dim cmd As New SqlCommand(query, Con)

            cmd.Parameters.AddWithValue("@name", updatedName)
            cmd.Parameters.AddWithValue("@gender", updatedGender)
            cmd.Parameters.AddWithValue("@address", updatedAddress)
            cmd.Parameters.AddWithValue("@phonenumber", updatedPhone)
            cmd.Parameters.AddWithValue("@ID", id)

            Dim result As Integer = cmd.ExecuteNonQuery()

            If result > 0 Then
                MessageBox.Show("Record updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                load_data()

                nameTb.Text = String.Empty
                addTb.Text = String.Empty
                phonenumTb.Text = String.Empty
                genderCb.SelectedIndex = -1
            Else
                MessageBox.Show("Error: Record could not be updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Con.Open()

        Dim query As String = "INSERT INTO coach (name, address, gender, phonenumber) " &
                              "VALUES ('" & nameTb.Text & "', '" & addTb.Text & "', '" & genderCb.Text & "', '" & phonenumTb.Text & "')"

        Dim cmd As New SqlCommand(query, Con)

        If String.IsNullOrWhiteSpace(nameTb.Text) OrElse
           String.IsNullOrWhiteSpace(addTb.Text) OrElse
           String.IsNullOrWhiteSpace(phonenumTb.Text) OrElse
           String.IsNullOrWhiteSpace(genderCb.Text) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Con.Close()
            Exit Sub
        End If

        If Not IsNumeric(phonenumTb.Text) OrElse phonenumTb.Text.Length <> 11 Then
            MessageBox.Show("Phone number must be numeric and exactly 11 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Con.Close()
            Exit Sub
        End If

        Dim result As Integer = cmd.ExecuteNonQuery()
        Con.Close()
        load_data()

        If result > 0 Then
            MessageBox.Show("Data Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            nameTb.Text = String.Empty
            addTb.Text = String.Empty
            phonenumTb.Text = String.Empty
            genderCb.SelectedIndex = -1
        Else
            MessageBox.Show("Data Insertion Failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        Con.Close()
    End Sub

    Private Sub managecch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CoachTableAdapter.Fill(Me.GymDbDataSet.coach)
    End Sub

    Private Sub Guna2TextBox5_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrWhiteSpace(Guna2TextBox5.Text) Then
                MessageBox.Show("Please input an ID number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Try
                    Me.CoachTableAdapter.searchid(Me.GymDbDataSet.coach, Guna2TextBox5.Text)

                    If Me.GymDbDataSet.coach.Rows.Count = 0 Then
                        MessageBox.Show("No records found matching the ID.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Exception
                    MessageBox.Show("An error occurred while searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            e.SuppressKeyPress = True
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

    Private Sub phonenumTb_TextChanged(sender As Object, e As EventArgs) Handles phonenumTb.TextChanged
        Dim input = phonenumTb.Text

        If Not System.Text.RegularExpressions.Regex.IsMatch(input, "^[0-9]*$") Then
            MessageBox.Show("Only numbers are allowed in the Phone Number field.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            phonenumTb.Text = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "")
            phonenumTb.SelectionStart = phonenumTb.Text.Length
            Return
        End If

        If input.Length > 11 Then
            MessageBox.Show("Phone number must be exactly 11 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            phonenumTb.Text = input.Substring(0, 11)
            phonenumTb.SelectionStart = phonenumTb.Text.Length
        End If
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Dim index As Integer = e.RowIndex
        Dim selectedRow As DataGridViewRow = Guna2DataGridView1.Rows(index)
        nameTb.Text = selectedRow.Cells(1).Value.ToString
        genderCb.Text = selectedRow.Cells(2).Value.ToString
        addTb.Text = selectedRow.Cells(3).Value.ToString
        phonenumTb.Text = selectedRow.Cells(4).Value.ToString
    End Sub

    Private Sub delBtn_Click(sender As Object, e As EventArgs) Handles delBtn.Click
        Try
            Con.Open()

            If MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim selectedRow As DataGridViewRow = Guna2DataGridView1.CurrentRow
                Dim id As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)

                Dim query As String = "DELETE FROM coach WHERE ID = @ID"
                Dim cmd As New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@ID", id)

                Dim result As Integer = cmd.ExecuteNonQuery()

                If result > 0 Then
                    MessageBox.Show("Record deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    load_data()

                    nameTb.Text = String.Empty
                    addTb.Text = String.Empty
                    phonenumTb.Text = String.Empty
                    genderCb.SelectedIndex = -1
                Else
                    MessageBox.Show("Error: Record could not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub
End Class
