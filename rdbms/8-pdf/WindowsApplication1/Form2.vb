Imports System.Data.OleDb

Public Class Form2
    Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=lib.mdb")

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            cn.Open()
            ' Maps to Text1 (ID), Text2 (Name), Text3 (Author), Text4 (Quantity)
            Dim sql As String = "INSERT INTO book (bookid, bookname, author, quan) VALUES ('" & Text1.Text & "', '" & Text2.Text & "', '" & Text3.Text & "', " & Text4.Text & ")"
            Dim cmd As New OleDbCommand(sql, cn)

            cmd.ExecuteNonQuery()

            MsgBox("Successfully Inserted")
            Text1.Text = ""
            Text2.Text = ""
            Text3.Text = ""
            Text4.Text = ""

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class
