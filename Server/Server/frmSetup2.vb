Public Class frmSetup2

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try

            For i As Integer = 1 To numberOfPeriods
                Dim outstr As String = ""

                For j As Integer = 2 To DataGridView2.ColumnCount
                    outstr &= DataGridView2(j - 1, i - 1).Value & ";"
                Next

                writeINI(sfile, "periods", CStr(i), outstr)
            Next

                Me.Close()
        Catch ex As Exception
            appEventLog_Write("error:", ex)
        End Try
    End Sub

    Private Sub frmPeriodSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            DataGridView2.RowCount = numberOfPeriods

            For i As Integer = 1 To numberOfPeriods
                DataGridView2.Rows(i - 1).Cells(0).Value = i

                Dim msgTokens() As String = getINI(sfile, "periods", CStr(i)).Split(";")
                Dim nextToken As Integer = 0

                If msgTokens.Count > 1 Then
                    DataGridView2(1, i - 1).Value = msgTokens(nextToken)
                    nextToken += 1
                End If
            Next

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try

    End Sub


    Private Sub cmdCopyDown_Click(sender As System.Object, e As System.EventArgs) Handles cmdCopyDown.Click
        Try
            Dim tempValue As String = DataGridView2.CurrentCell.Value
            Dim tempR As Integer = DataGridView2.CurrentCell.RowIndex
            Dim tempC As Integer = DataGridView2.CurrentCell.ColumnIndex

            For i As Integer = tempR + 1 To DataGridView2.RowCount - 1
                DataGridView2(tempC, i).Value = tempValue
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class