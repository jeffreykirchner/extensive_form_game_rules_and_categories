Public Class period
    Public tree As Integer

    Public Sub fromString(s)
        Try
            Dim msgTokens() As String = Split(s, ";")
            Dim nextToken As Integer = 0

            tree = msgTokens(nextToken)
            nextToken += 1
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub
End Class
