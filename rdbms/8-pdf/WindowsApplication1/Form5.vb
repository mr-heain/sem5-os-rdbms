Imports System.Data.OleDb

Public Class Form5
    'Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=lib.MDB")
    Dim cn As New OleDbConnection("Provider=MSDAORA; Data Source=XE;User Id=system;Password=system;")

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cn.Open()
            Dim cmd As New OleDbCommand("SELECT * FROM book", cn)
            Dim adapter As New OleDbDataAdapter(cmd)
            Dim table As New DataTable()

            adapter.Fill(table)

            ' Fills the DataGridView with the database table
            DataGridView1.DataSource = table

        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

End Class
