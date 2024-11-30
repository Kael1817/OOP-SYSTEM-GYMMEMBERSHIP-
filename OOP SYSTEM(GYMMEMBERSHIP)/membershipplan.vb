Public Class membershipplan

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Guna2HtmlLabel1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub membershipplan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'GymDbDataSet2.plan' table. You can move, or remove it, as needed.
        Me.PlanTableAdapter1.Fill(Me.GymDbDataSet2.plan)
        'TODO: This line of code loads data into the 'GymDbDataSet1.plan' table. You can move, or remove it, as needed.
        Me.PlanTableAdapter.Fill(Me.GymDbDataSet1.plan)


    End Sub
End Class