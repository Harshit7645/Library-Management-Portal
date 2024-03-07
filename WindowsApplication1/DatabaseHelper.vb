Imports System.Data.SqlClient

Public Class DatabaseHelper
    Private Shared connectionString As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Lenovo\Desktop\Library Management\WindowsApplication1\WindowsApplication1\WindowsApplication1\Library.mdf;Integrated Security=True;User Instance=True"

    Private Shared Function EncryptData(ByVal strToHash As String) As String
        Dim Result As String = ""
        Dim OSha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider

        'Step 1
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        'Step 2
        bytesToHash = OSha1.ComputeHash(bytesToHash)

        'Step 3
        For Each item As Byte In bytesToHash
            Result += item.ToString("x2")
        Next
        Return Result

    End Function

    Public Shared Function AddUser(ByVal userName As String, ByVal mobile As String, ByVal userID As String, ByVal pass As String) As Boolean
        Dim cmd As New SqlCommand
        Using con As SqlConnection = New SqlConnection(connectionString)
            con.Open()

            Dim stmnt As String = "SELECT * FROM Login WHERE UserID = @UserID"
            cmd = New SqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@UserID", userID)

            Dim reader As SqlDataReader = cmd.ExecuteReader

            If reader.Read() Then
                MessageBox.Show("UserID already exists")
                Return False
            Else
                reader.Close()

                ' Begin a transaction
                Using transaction As SqlTransaction = con.BeginTransaction()
                    Try
                        ' Insert into UserData
                        stmnt = "INSERT INTO UserData (Name, Mobile, UserID) VALUES (@Name, @Mobile, @UserID)"
                        cmd = New SqlCommand(stmnt, con, transaction)
                        cmd.Parameters.AddWithValue("@Name", userName)
                        cmd.Parameters.AddWithValue("@Mobile", mobile)
                        cmd.Parameters.AddWithValue("@UserID", userID)
                        cmd.ExecuteNonQuery()

                        ' Insert into Login
                        stmnt = "INSERT INTO Login (UserID, UserPassHash) VALUES (@UserID, @UserPassHash)"
                        cmd = New SqlCommand(stmnt, con, transaction)
                        cmd.Parameters.AddWithValue("@UserID", userID)
                        cmd.Parameters.AddWithValue("@UserPassHash", EncryptData(pass))
                        cmd.ExecuteNonQuery()

                        ' Commit the transaction if everything is successful
                        transaction.Commit()

                        MessageBox.Show("Added successfully.")
                        Return True
                    Catch ex As Exception
                        ' An error occurred, rollback the transaction
                        transaction.Rollback()
                        MessageBox.Show("Error: " & ex.Message)
                        Return False
                    End Try
                End Using
            End If
        End Using
    End Function

    Public Shared Function Login(ByVal userID As String, ByVal pass As String) As Boolean
        Dim cmd As New SqlCommand
        Using con As SqlConnection = New SqlConnection(connectionString)
            con.Open()

            Dim stmnt As String = "SELECT * FROM Login where UserID = @UserID AND UserPassHash = @UserPassHash"
            cmd = New SqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@UserID", userID)
            cmd.Parameters.AddWithValue("@UserPassHash", EncryptData(pass))

            Dim reader As SqlDataReader = cmd.ExecuteReader

            If reader.Read Then
                reader.Close()

                stmnt = "SELECT Name FROM UserData WHERE UserID = @UserID"
                cmd = New SqlCommand(stmnt, con)
                cmd.Parameters.AddWithValue("@UserID", userID)
                Dim result As Object = cmd.ExecuteScalar()
                Dim userName As String = result.ToString()

                MessageBox.Show("Welcome back, " & userName)
                Return True
            Else
                MessageBox.Show("ID or Password Invalid")
                Return False
            End If
        End Using
    End Function

    Public Shared Function AddBook(ByVal BookName As String, ByVal BookAuthor As String, ByVal ISBN As String) As Boolean
        Dim cmd As New SqlCommand
        Using con As SqlConnection = New SqlConnection(connectionString)

            Dim issueDate As DateTime = DateTime.Now

            Dim stmnt As String = "INSERT INTO BookDetails (BookName, BookAuthor, ISBN) VALUES (@BookName, @BookAuthor, @ISBN)"
            ' Dim stmnt As String = "INSERT INTO BookDetails (BookName, BookAuthor, ISBN, IssueDate) VALUES (@BookName, @BookAuthor, @ISBN, @IssueDate)"

            cmd = New SqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@BookName", BookName)
            cmd.Parameters.AddWithValue("@BookAuthor", BookAuthor)
            cmd.Parameters.AddWithValue("@ISBN", ISBN)
            ' cmd.Parameters.AddWithValue("@IssueDate", issueDate)

            Try
                con.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Book added successfully.")
                Return True
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
                Return False
            End Try

        End Using
    End Function

    Public Shared Function TEST(ByVal BookName As String, ByVal BookAuthor As String, ByVal ISBN As String, ByVal bookId As Integer, ByVal rating As Double, ByVal pub As String) As Boolean
        Dim cmd As New SqlCommand
        Using con As SqlConnection = New SqlConnection(connectionString)

            Dim issueDate As DateTime = DateTime.Now

            Dim stmnt As String = "INSERT INTO BookDetails (BookName, BookAuthor, ISBN, PublicationDate, AverageRating) VALUES (@BookName, @BookAuthor, @ISBN, @PublicationDate, @AverageRating)"
            ' Dim stmnt As String = "INSERT INTO BookDetails (BookName, BookAuthor, ISBN, IssueDate) VALUES (@BookName, @BookAuthor, @ISBN, @IssueDate)"

            cmd = New SqlCommand(stmnt, con)
            cmd.Parameters.AddWithValue("@BookName", BookName)
            cmd.Parameters.AddWithValue("@BookAuthor", BookAuthor)
            cmd.Parameters.AddWithValue("@ISBN", ISBN)
            cmd.Parameters.AddWithValue("@PublicationDate", pub)
            cmd.Parameters.AddWithValue("@AverageRating", rating)

            Try
                con.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
                Return False
            End Try

        End Using
    End Function

    Public Shared Function ShowBooksLessThan15Days() As DataTable
        Dim dataTable As New DataTable()
        Try
            Using con As SqlConnection = New SqlConnection(connectionString)
                con.Open()

                Dim sql As String = "SELECT BookID as ID, BookName, BookAuthor, CASE WHEN IssuerID IS NULL THEN 'Not Issued' ELSE IssuerID END AS IssuedBy, CASE WHEN IssuerID IS NULL THEN NULL ELSE IssueDate END AS IssueDate FROM BookDetails WHERE DATEDIFF(DAY, IssueDate, GETDATE()) > 15"
                Using cmd As New SqlCommand(sql, con)
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        dataTable.Load(reader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        Return dataTable
    End Function

    Public Shared Function SearchBooks(ByVal searchBookName As String, ByVal searchAuthorName As String) As DataTable
        Dim dataTable As New DataTable()
        Try
            Using con As SqlConnection = New SqlConnection(connectionString)
                con.Open()

                Dim sql As String = "SELECT BookID as ID, BookName, BookAuthor, ISBN, PublicationDate, AverageRating, CASE WHEN IssuerID IS NULL THEN 'Not Issued' ELSE IssuerID END AS Issue FROM BookDetails WHERE (LOWER(BookName) LIKE LOWER(@SearchBookName) OR @SearchBookName IS NULL) AND (LOWER(BookAuthor) LIKE LOWER(@SearchAuthorName) OR @SearchAuthorName IS NULL)"

                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.AddWithValue("@SearchBookName", If(String.IsNullOrEmpty(searchBookName), DBNull.Value, "%" & searchBookName.ToLower() & "%"))
                    cmd.Parameters.AddWithValue("@SearchAuthorName", If(String.IsNullOrEmpty(searchAuthorName), DBNull.Value, "%" & searchAuthorName.ToLower() & "%"))

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        dataTable.Load(reader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        Return dataTable
    End Function

    Public Shared Sub ReturnBook(ByVal bookId As Integer)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Check if the book is issued and fetch issuer ID
                Dim query As String = "SELECT IssuerID, IssueDate, BookName, BookAuthor FROM BookDetails WHERE BookID = @BookID AND IssuerID IS NOT NULL"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@BookID", bookId)

                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' Book is issued, fetch issuer ID
                        Dim issuerId As String = reader("IssuerID")
                        Dim issueDate As DateTime = Convert.ToDateTime(reader("IssueDate"))
                        Dim bookName As String = reader("BookName").ToString()
                        Dim bookAuthor As String = reader("BookAuthor").ToString()
                        reader.Close()

                        Dim currentDate As DateTime = DateTime.Now
                        Dim daysIssued As Integer = CInt((currentDate - issueDate).TotalDays)

                        ' Calculate fine as per the rule: max(datesIssued - 15, 0) * 2
                        Dim fine As Integer = Math.Max(daysIssued - 15, 0) * 2

                        ' Confirm with the user before returning the book
                        Dim confirmationMessage As String = "Book Name: " & bookName & Environment.NewLine & "Author: " & bookAuthor & Environment.NewLine & "Issuer ID: " & issuerId & Environment.NewLine & "Fine: INR " & fine & Environment.NewLine & "Do you want to return the book?"
                        Dim confirmationResult As DialogResult = MessageBox.Show(confirmationMessage, "Return Book Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If confirmationResult = DialogResult.Yes Then

                            ' Update the record to mark the book as returned
                            Dim updateQuery As String = "UPDATE BookDetails SET IssuerID = NULL, IssueDate = NULL WHERE BookID = @BookID"
                            Using updateCmd As New SqlCommand(updateQuery, con)
                                updateCmd.Parameters.AddWithValue("@BookID", bookId)
                                updateCmd.ExecuteNonQuery()

                                LogTransaction(issuerId, "Return", bookId, fine)
                                MessageBox.Show("Book returned successfully.", "Return Book Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End Using
                        Else
                            MessageBox.Show("Returning canceled by the user.", "Return Book Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        ' Book is not issued, show a dialog box
                        reader.Close()
                        MessageBox.Show("This book is not issued.", "Return Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Return Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub IssueBook(ByVal bookId As Integer, ByVal userId As String)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Check if the book is available and not already issued
                Dim availabilityQuery As String = "SELECT BookName, BookAuthor FROM BookDetails WHERE BookID = @BookID AND IssuerID IS NULL"
                Using availabilityCmd As New SqlCommand(availabilityQuery, con)
                    availabilityCmd.Parameters.AddWithValue("@BookID", bookId)

                    Dim availabilityReader As SqlDataReader = availabilityCmd.ExecuteReader()

                    If availabilityReader.Read() Then
                        ' Book is available, proceed with issuing
                        Dim bookName As String = availabilityReader("BookName").ToString()
                        Dim bookAuthor As String = availabilityReader("BookAuthor").ToString()
                        availabilityReader.Close()

                        ' Confirm with the user before issuing the book
                        Dim confirmationMessage As String = "Do you want to issue the book '" & bookName & "' by " & bookAuthor & " to Issuer ID: " & userId & "?"
                        Dim confirmationResult As DialogResult = MessageBox.Show(confirmationMessage, "Issue Book Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If confirmationResult = DialogResult.Yes Then

                            ' Update the record to mark the book as issued
                            Dim issueQuery As String = "UPDATE BookDetails SET IssuerID = @UserID, IssueDate = GETDATE() WHERE BookID = @BookID"
                            Using issueCmd As New SqlCommand(issueQuery, con)
                                issueCmd.Parameters.AddWithValue("@BookID", bookId)
                                issueCmd.Parameters.AddWithValue("@UserID", userId)

                                Dim rowsAffected As Integer = issueCmd.ExecuteNonQuery()

                                If rowsAffected > 0 Then
                                    Dim message As String = "Book '" & bookName & "' by " & bookAuthor & " issued successfully." & Environment.NewLine & "Issuer ID: " & userId
                                    LogTransaction(userId, "Issue", bookId, 0)
                                    MessageBox.Show(message, "Issue Book Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    MessageBox.Show("Failed to issue the book.", "Issue Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End Using
                        Else
                            MessageBox.Show("Issuing canceled by the user.", "Issue Book Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        ' Book is not available or already issued
                        availabilityReader.Close()
                        MessageBox.Show("This book is not available for issuance.", "Issue Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Issue Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub RenewBook(ByVal bookId As Integer)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Check if the book is issued
                Dim issuedQuery As String = "SELECT BookName, BookAuthor, IssuerID, IssueDate FROM BookDetails WHERE BookID = @BookID AND IssuerID IS NOT NULL"
                Using issuedCmd As New SqlCommand(issuedQuery, con)
                    issuedCmd.Parameters.AddWithValue("@BookID", bookId)

                    Dim reader As SqlDataReader = issuedCmd.ExecuteReader()

                    If reader.Read() Then
                        ' Book is issued, fetch book and issuer information
                        Dim bookName As String = reader("BookName").ToString()
                        Dim bookAuthor As String = reader("BookAuthor").ToString()
                        Dim issuerId As String = reader("IssuerID")
                        Dim issueDate As DateTime = Convert.ToDateTime(reader("IssueDate"))
                        reader.Close()

                        ' Calculate fine as per the rule: max(datesIssued - 15, 0) * 2
                        Dim currentDate As DateTime = DateTime.Now
                        Dim daysIssued As Integer = CInt((currentDate - issueDate).TotalDays)
                        Dim fine As Integer = Math.Max(daysIssued - 15, 0) * 2

                        ' Show a dialog box with book details, issuer ID, and fine information
                        Dim confirmationMessage As String = "Book '" & bookName & "' by " & bookAuthor & " issued to Issuer ID: " & issuerId & Environment.NewLine & "Fine: INR " & fine & Environment.NewLine & "Do you want to renew the book?"
                        Dim confirmationResult As DialogResult = MessageBox.Show(confirmationMessage, "Renew Book Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If confirmationResult = DialogResult.Yes Then
                            ' Update the record to mark the book as renewed
                            Dim renewQuery As String = "UPDATE BookDetails SET IssueDate = GETDATE() WHERE BookID = @BookID"
                            Using renewCmd As New SqlCommand(renewQuery, con)
                                renewCmd.Parameters.AddWithValue("@BookID", bookId)
                                renewCmd.ExecuteNonQuery()
                                LogTransaction(issuerId, "Renew", bookId, fine)
                                MessageBox.Show("Book renewed successfully.", "Renew Book Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End Using
                        Else
                            MessageBox.Show("Renewal canceled.", "Renew Book Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        ' Book is not issued
                        reader.Close()
                        MessageBox.Show("This book is not issued.", "Renew Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Renew Book Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub RemoveBook(ByVal bookId As Integer)
        Dim con As SqlConnection
        Dim deleteCmd As SqlCommand

        Try
            con = New SqlConnection(connectionString)
            con.Open()

            Dim deleteQuery As String
            deleteQuery = "DELETE FROM BookDetails where BookID=" & bookId.ToString()
            deleteCmd = New SqlCommand(deleteQuery, con)
            Dim rowsAffected As Integer = deleteCmd.ExecuteNonQuery()
            If( rowsAffected > 0 )
                MessageBox.Show("Book removed successfully from the database")
            Else
                MessageBox.Show("Book not found in the database")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
    Public Shared Sub RemoveUser(ByVal userID As String)
        Dim con As SqlConnection
        Dim deleteCmd As SqlCommand

        Try
            con = New SqlConnection(connectionString)
            con.Open()

            Dim deleteQuery As String
            deleteQuery = "DELETE FROM UserData where UserID='" & userID & "'"
            deleteCmd = New SqlCommand(deleteQuery, con)
            Dim rowsAffected As Integer = deleteCmd.ExecuteNonQuery()
            If (rowsAffected > 0) Then
                deleteQuery = "DELETE FROM Login where UserID='" & userID & "'"
                deleteCmd = New SqlCommand(deleteQuery, con)
                Dim rowsChanged As Integer = deleteCmd.ExecuteNonQuery()
                MessageBox.Show("User access revoked successfully from the Library")
            Else
                MessageBox.Show("User not a member of the Library")
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub
    Public Shared Sub LogTransaction(ByVal userId As String, ByVal operation As String, ByVal bookId As Integer, ByVal fine As Integer)
        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Get current time
                Dim currentTime As DateTime = DateTime.Now

                ' Insert a new entry into the Logs table
                Dim insertQuery As String = "INSERT INTO Logs (UserID, Operation, BookID, Fine, Time) VALUES (@UserID, @Operation, @BookID, @Fine, @Time)"

                Using cmd As New SqlCommand(insertQuery, con)
                    cmd.Parameters.AddWithValue("@UserID", userId)
                    cmd.Parameters.AddWithValue("@Operation", operation)
                    cmd.Parameters.AddWithValue("@BookID", bookId)
                    cmd.Parameters.AddWithValue("@Fine", fine)
                    cmd.Parameters.AddWithValue("@Time", currentTime)

                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Log Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function GetTransactionsByUserID(ByVal userId As String) As DataTable
        Dim transactionsDataTable As New DataTable()

        Try
            Using con As New SqlConnection(connectionString)
                con.Open()

                ' Select relevant columns and join Logs with BookDetails on BookID
                Dim selectQuery As String = "SELECT Logs.Time as 'When', Logs.Operation, Logs.Fine, Logs.BookID, BookDetails.BookName as 'Book Name', BookDetails.BookAuthor as 'Author' " &
                                            "FROM Logs " &
                                            "INNER JOIN BookDetails ON Logs.BookID = BookDetails.BookID " &
                                            "WHERE Logs.UserID = @UserID"

                Using cmd As New SqlCommand(selectQuery, con)
                    cmd.Parameters.AddWithValue("@UserID", userId)

                    ' Use a SqlDataAdapter to fill the DataTable
                    Dim adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(transactionsDataTable)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Get Transactions Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return transactionsDataTable
    End Function

    Public Shared Function FineCalculator(ByVal dataTable As DataTable) As String
        Dim totalFine As Object = dataTable.Compute("SUM(Fine)", "")
        Dim outputFine As String
        If totalFine IsNot DBNull.Value Then
            Dim totalSum As Integer = Convert.ToInt32(totalFine)
            outputFine = totalSum.ToString("0")
        Else
            outputFine = "No Data"
        End If
        Return outputFine
    End Function
' Add more methods for other database operations
End Class
