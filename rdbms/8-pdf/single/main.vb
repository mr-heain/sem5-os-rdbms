Imports System.Data.OleDb

Public Class main
    ' Form-level connection to Oracle 10g
    Dim cn As New OleDbConnection("Provider=MSDAORA;Data Source=XE;User ID=system;Password=system;")

    ' --- 1. ADD BOOK ---
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        ' 1. Check if any of your textboxes are empty first
        If TextBox5.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Please fill in all fields before adding a book.")
            Exit Sub ' Stops the code here so Oracle doesn't crash
        End If

        Try
            cn.Open()
            Dim sql As String = "INSERT INTO book VALUES (" & TextBox5.Text & ", '" & TextBox2.Text & "', '" & TextBox3.Text & "', " & TextBox4.Text & ")"
            Dim cmd As New OleDbCommand(sql, cn)
            cmd.ExecuteNonQuery()

            MsgBox("Book Added Successfully!")

            ' Clear textboxes after adding
            TextBox5.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        Catch ex As Exception
            MsgBox("Error adding book: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    ' --- 2. ISSUE BOOK ---
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        ' We use InputBox here so you don't have to fill out the textboxes just to issue a book
        Dim bid As String = InputBox("Enter the Book ID to Issue:")
        If bid = "" Then Exit Sub

        Try
            cn.Open()
            Dim sql As String = "UPDATE book SET quan = quan - 1 WHERE bookid = " & bid
            Dim cmd As New OleDbCommand(sql, cn)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MsgBox("Book Issued Successfully!")
            Else
                MsgBox("No record found with ID: " & bid)
            End If
        Catch ex As Exception
            MsgBox("Error issuing book: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    ' --- 3. RETURN BOOK ---
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Dim bid As String = InputBox("Enter the Book ID to Return:")
        If bid = "" Then Exit Sub

        Try
            cn.Open()
            Dim sql As String = "UPDATE book SET quan = quan + 1 WHERE bookid = " & bid
            Dim cmd As New OleDbCommand(sql, cn)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MsgBox("Book Returned Successfully!")
            Else
                MsgBox("No record found with ID: " & bid)
            End If
        Catch ex As Exception
            MsgBox("Error returning book: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    ' --- 4. VIEW REPORT (Updates the DataGridView at the bottom) ---
    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Try
            Dim sql As String = "SELECT * FROM book"
            ' DataAdapter grabs the data without needing to manually open/close the connection
            Dim da As New OleDbDataAdapter(sql, cn)
            Dim dt As New DataTable()

            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox("Error loading data: " & ex.Message)
        End Try
    End Sub
End Class
