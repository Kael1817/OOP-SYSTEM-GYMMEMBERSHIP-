Public Class dashboard
    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As PaintEventArgs)

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

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Dim coach As New managecch()
        coach.TopLevel = False
        mainpnl.Controls.Add(coach)
        coach.BringToFront()
        coach.Show()




    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Dim member As New managemember()
        member.TopLevel = False
        mainpnl.Controls.Add(member)
        member.BringToFront()
        member.Show()

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Dim plan As New membershipplan()
        plan.TopLevel = False
        mainpnl.Controls.Add(plan)
        plan.BringToFront()
        plan.Show()

    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Dim add As New addnewmember(managemember)
        add.TopLevel = False
        mainpnl.Controls.Add(add)
        add.BringToFront()
        add.Show()
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Dim ep As New equipment()
        ep.TopLevel = False
        mainpnl.Controls.Add(ep)
        ep.BringToFront()
        ep.Show()
    End Sub
End Class