﻿Public Class removeBookForm

    Private Sub bookID_TextChanged(sender As System.Object, e As System.EventArgs) Handles bookID.TextChanged

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
        Dim Issued As New IssuedBooksForm()
        Me.Hide()
        Issued.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim Search As New SearchBookFormPvt()
        Me.Hide()
        Search.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim Return_book As New ReturnBookForm()
        Me.Hide()
        Return_book.Show()
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

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click
        Dim userInput As String = bookID.Text

        Try
            Dim bookId As Integer = Integer.Parse(userInput)
            DatabaseHelper.RemoveBook(bookId)
        Catch ex As FormatException
            MessageBox.Show("Invalid book ID. Please enter a valid integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        bookID.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim removeUser As New RemoveUserForm()
        Me.Hide()
        removeUser.Show()
    End Sub

    Private Sub removeBookForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        Dim his As New LogsForm()
        Me.Hide()
        his.Show()
    End Sub
End Class