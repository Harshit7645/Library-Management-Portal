Imports System.Data.SqlClient
Public Class IssuedBooksForm

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click

        Dim dataTable As DataTable = DatabaseHelper.ShowBooksLessThan15Days()

        DataGridView1.DataSource = dataTable
        DataGridView1.Visible = True

        ' Rename columns for better display
        DataGridView1.Columns("BookName").HeaderText = "Book Name"
        DataGridView1.Columns("BookAuthor").HeaderText = "Author"
        DataGridView1.Columns("IssuedBy").HeaderText = "Issued By"
        DataGridView1.Columns("IssueDate").HeaderText = "Issue Date"

    End Sub

    Private Sub panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panel1.Paint

    End Sub

    Private Sub label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles label4.Click

    End Sub

    Private Sub label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim add_user As New Form4()
        Me.Hide()
        add_user.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim add_book As New AddBookForm()
        Me.Hide()
        add_book.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim search As New SearchBookFormPvt()
        Me.Hide()
        search.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim return_book As New ReturnBookForm()
        Me.Hide()
        return_book.Show()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim issue As New IssueBookForm()
        Me.Hide()
        issue.Show()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim renew As New RenewBookForm()
        Me.Hide()
        renew.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim removeBook As New removeBookForm()
        Me.Hide()
        removeBook.Show()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim removeUser As New RemoveUserForm()
        Me.Hide()
        removeUser.Show()
    End Sub

    Private Sub IssuedBooksForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim his As New LogsForm()
        Me.Hide()
        his.Show()
    End Sub
End Class
