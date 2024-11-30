Imports System.Web.UI.WebControls
Imports System.Data.SqlClient

Public Class managemember
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Michael\Visual Studio Projects\OOP SYSTEM(GYMMEMBERSHIP) 1\OOP SYSTEM(GYMMEMBERSHIP) - Copy - Copy\OOP SYSTEM(GYMMEMBERSHIP) - Copy\OOP SYSTEM(GYMMEMBERSHIP)\GymDb.mdf;Integrated Security=True;Connect Timeout=30")
    Public Sub load_data()
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim Str As String = "SELECT * FROM addTbl"

            Dim Search As New SqlDataAdapter(Str, Con)
            Dim ds As DataSet = New DataSet
            Search.Fill(ds, "addTbl")
            Guna2DataGridView1.DataSource = ds.Tables("addTbl")
            Con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            Con.Close()
        End Try
    End Sub

    Private Sub managemember_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_data()
    End Sub

    Private Sub Guna2TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Check if the input field is empty
            If String.IsNullOrWhiteSpace(Guna2TextBox1.Text) Then
                MessageBox.Show("Please input an ID number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                ' Perform the search
                Try
                    Me.AddTblTableAdapter.searchid(Me.GymDbDataSetmember.addTbl, Guna2TextBox1.Text)

                    ' Check if any records are found
                    If Me.GymDbDataSetmember.addTbl.Rows.Count = 0 Then
                        MessageBox.Show("No records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch ex As Exception
                    ' Display an error message if something goes wrong
                    MessageBox.Show("An error occurred while searching: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            ' Prevent the Enter key from triggering unintended actions
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Dim index As Integer = e.RowIndex
        Dim selectedRow As DataGridViewRow = Guna2DataGridView1.Rows(index)
        nameTb.Text = selectedRow.Cells(1).Value.ToString
        Guna2TextBox2.Text = selectedRow.Cells(8).Value.ToString
        Guna2TextBox3.Text = selectedRow.Cells(11).Value.ToString
        Guna2TextBox4.Text = selectedRow.Cells(10).Value.ToString
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
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
            Dim updatedPlan As String = Guna2TextBox2.Text
            Dim updatedStatus As String = Guna2TextBox3.Text
            Dim updatedEnd As String = Guna2TextBox4.Text

            If String.IsNullOrWhiteSpace(updatedName) OrElse String.IsNullOrWhiteSpace(updatedPlan) OrElse String.IsNullOrWhiteSpace(updatedStatus) OrElse String.IsNullOrWhiteSpace(updatedEnd) Then
                MessageBox.Show("All fields must be filled out", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim query As String = "UPDATE addTbl SET Fullname = @name, Membershipplan = @plan, Membershipstatus = @status, Endofmembership = @end WHERE Id = @ID"
            Dim cmd As New SqlCommand(query, Con)

            cmd.Parameters.AddWithValue("@name", updatedName)
            cmd.Parameters.AddWithValue("@plan", updatedPlan)
            cmd.Parameters.AddWithValue("@status", updatedStatus)
            cmd.Parameters.AddWithValue("@end", updatedEnd)
            cmd.Parameters.AddWithValue("@ID", id)

            Dim result As Integer = cmd.ExecuteNonQuery()

            If result > 0 Then
                MessageBox.Show("Record updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                load_data()

                nameTb.Text = String.Empty
                Guna2TextBox2.Text = String.Empty
                Guna2TextBox3.Text = String.Empty
                Guna2TextBox4.Text = String.Empty
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

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim selectedRow As DataGridViewRow = Guna2DataGridView1.CurrentRow
            If selectedRow Is Nothing Then
                MessageBox.Show("No record selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim id As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)

                Dim query As String = "DELETE FROM addTbl WHERE ID = @ID"
                Dim cmd As New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@ID", id)

                Dim result As Integer = cmd.ExecuteNonQuery()

                If result > 0 Then
                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    load_data()

                    ' Clear input fields
                    nameTb.Clear()
                    Guna2TextBox2.Clear()
                    Guna2TextBox3.Clear()
                    Guna2TextBox4.Clear()

                Else
                    MessageBox.Show("Error: Record could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Sub

End Class
