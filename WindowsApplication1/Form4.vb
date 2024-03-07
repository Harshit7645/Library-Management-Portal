Imports System.Data.SqlClient
Public Class Form4

    Private Sub button3_Click(sender As System.Object, e As System.EventArgs) Handles button3.Click

        Dim userName As String = Name.Text
        Dim userMobile As String = mobile.Text
        Dim userID As String = uid.Text
        Dim password As String = pass.Text

        DatabaseHelper.AddUser(userName, userMobile, userID, password)

    End Sub

    Private Sub panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles panel1.Paint

    End Sub

    Private Sub label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label4.Click

    End Sub

    Private Sub label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label5.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uid.TextChanged

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim addBook As New AddBookForm()
        Me.Hide()
        addBook.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim issuedBooksForm As New IssuedBooksForm()
        Me.Hide()
        issuedBooksForm.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim searchBooksForm As New SearchBookFormPvt()
        Me.Hide()
        searchBooksForm.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim returnBookForm As New ReturnBookForm()
        Me.Hide()
        returnBookForm.Show()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim issueBookForm As New IssueBookForm()
        Me.Hide()
        issueBookForm.Show()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim renewBookForm As New RenewBookForm()
        Me.Hide()
        renewBookForm.Show()
    End Sub

    Private Sub name_TextChanged(sender As System.Object, e As System.EventArgs) Handles Name.TextChanged

    End Sub

    Private Sub pictureBox2_Click(sender As System.Object, e As System.EventArgs) Handles pictureBox2.Click

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim removebook As New removeBookForm()
        Me.Hide()
        removebook.Show()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim removeUser As New RemoveUserForm()
        Me.Hide()
        RemoveUserForm.Show()
    End Sub

    Private Sub panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panel2.Paint

    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim his As New LogsForm()
        Me.Hide()
        his.Show()
    End Sub
End Class
