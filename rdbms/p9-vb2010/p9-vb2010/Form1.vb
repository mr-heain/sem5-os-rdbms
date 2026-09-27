Imports System.Data.OleDb

Public Class Form1
    ' Dim cn As New OleDbConnection("Provider=MSDAORA;Data Source=XE;User ID=system;Password=system;")
    Dim cn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=stud.mdb;")
    ' --- CALCULATE MARKS ---
    Private Sub Button1_click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        ' Calculate Total
        TextBox7.Text = (Val(TextBox3.Text) + Val(TextBox4.Text) + Val(TextBox5.Text) + Val(TextBox6.Text)).ToString()

        ' Determine PASS/FAIL (If any subject is <= 30, it is a FAIL)
        If Val(TextBox3.Text) <= 30 Or Val(TextBox4.Text) <= 30 Or Val(TextBox5.Text) <= 30 Or Val(TextBox6.Text) <= 30 Then
            TextBox8.Text = "FAIL"
        Else
            TextBox8.Text = "PASS"
        End If

        ' Calculate Percentage
        TextBox9.Text = (Val(TextBox7.Text) / 4).ToString()
    End Sub

    ' --- INSERT RECORD ---
    Private Sub btnInsert_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Try
            cn.Open()
            Dim sql As String = "INSERT INTO st VALUES (" & TextBox1.Text & ", '" & TextBox2.Text & "', " & TextBox3.Text & ", " & TextBox4.Text & ", " & TextBox5.Text & ", " & TextBox6.Text & ", " & TextBox7.Text & ", '" & TextBox8.Text & "', " & TextBox9.Text & ")"
            Dim cmd As New OleDbCommand(sql, cn)
            cmd.ExecuteNonQuery()

            MsgBox("Successfully Inserted")

            TextBox1.Clear() : TextBox2.Clear() : TextBox3.Clear()
            TextBox4.Clear() : TextBox5.Clear() : TextBox6.Clear()
            TextBox7.Clear() : TextBox8.Clear() : TextBox9.Clear()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    ' --- DELETE RECORD ---
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Dim studentID As String = InputBox("Enter the Student ID to delete:")
        If studentID = "" Then Exit Sub

        Try
            cn.Open()
            Dim sql As String = "DELETE FROM st WHERE stid = " & studentID
            Dim cmd As New OleDbCommand(sql, cn)
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MsgBox("Record with ID " & studentID & " deleted successfully!")
            Else
                MsgBox("No record found with ID: " & studentID)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cn.Close()
        End Try
    End Sub

    ' --- VIEW REPORT ---
    Private Sub btnView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Try
            Dim da As New OleDbDataAdapter("SELECT * FROM st", cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class