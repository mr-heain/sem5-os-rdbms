Imports System.Data.OleDb

Public Class Form3

    'Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=lib.mdb")
    Dim cn As New OleDbConnection("Provider=MSDAORA; Data Source=XE;User Id=system;Password=system;")

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Disables editing of these fields just like the manual
        Text1.Enabled = False
        Text2.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button1.Click
        Dim bid As String = InputBox("Enter the Book ID :")
        If bid = "" Then Exit Sub

        Try
            cn.Open()

            ' First, find the book to display its name in the TextBoxes
            Dim selectSql As String = "SELECT * FROM book WHERE bookid = " & bid
            Dim selectCmd As New OleDbCommand(selectSql, cn)
            Dim reader As OleDbDataReader = selectCmd.ExecuteReader()

            If reader.Read() Then
                Text1.Text = reader("bookid").ToString()
                Text2.Text = reader("bookname").ToString()
                reader.Close() ' You must close the reader before running an update

                ' Update the quantity
                Dim updateSql As String = "UPDATE book SET quan = quan - 1 WHERE bookid = " & bid
                Dim updateCmd As New OleDbCommand(updateSql, cn)
                updateCmd.ExecuteNonQuery()

                MsgBox("Book Issued")
            Else
                MsgBox("No record found with ID: " & bid)
                reader.Close()
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Text1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Text1.TextChanged

    End Sub
End Class
