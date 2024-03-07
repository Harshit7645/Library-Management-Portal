Imports System.Data.SqlClient
Public Class SearchBookForm

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click

        Dim bookName As String = name.Text
        Dim authorName As String = author.Text

        Dim dataTable As DataTable = DatabaseHelper.SearchBooks(bookName, authorName)

        DataGridView1.DataSource = dataTable
        DataGridView1.Visible = True

        DataGridView1.Columns("BookName").HeaderText = "Book Name"
        DataGridView1.Columns("BookAuthor").HeaderText = "Author"

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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim loginForm As New Form1()
        Me.Hide()
        loginForm.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

    End Sub
End Class
