Imports System.Data.SqlClient
Public Class IssueBookForm

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click

        Dim bookIdInput As String = bookId.Text
        Dim issuer As String = issuerId.Text

        Try
            Dim bookId As Integer = Integer.Parse(bookIdInput)
            DatabaseHelper.IssueBook(bookId, issuer)
        Catch ex As FormatException
            MessageBox.Show("Invalid book ID. Please enter a valid integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panel1.Paint

    End Sub

    Private Sub label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label4.Click

    End Sub

    Private Sub label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label5.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim return_book As New ReturnBookForm()
        Me.Hide()
        return_book.Show()
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim Search As New SearchBookFormPvt()
        Me.Hide()
        Search.Show()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim issued As New IssuedBooksForm()
        Me.Hide()
        issued.Show()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim add_book As New AddBookForm()
        Me.Hide()
        add_book.Show()
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        Dim form4 As New Form4()
        Me.Hide()
        form4.Show()
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Dim renew As New RenewBookForm()
        Me.Hide()
        renew.Show()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim removeBook As New removeBookForm()
        Me.Hide()
        removeBook.Show()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim removeUser As New RemoveUserForm()
        Me.Hide()
        removeUser.Show()
    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim his As New LogsForm()
        Me.Hide()
        his.Show()
    End Sub
End Class
