Imports System.Data.SqlClient
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click

        Dim userID As String = uid.Text
        Dim password As String = pass.Text

        Dim transfer As New Form4()
        If (DatabaseHelper.Login(userID, password)) Then
            Me.Hide()
            transfer.Show()
        End If
    End Sub

    Private Sub uid_TextChanged(sender As System.Object, e As System.EventArgs) Handles uid.TextChanged

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim search As New SearchBookForm()
        Me.Hide()
        search.Show()
    End Sub

    Private Sub pass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pass.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    End Sub
End Class
