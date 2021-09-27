Public Class frmSetup3_1
    Public currentNode As Integer
    Public currentTree As Integer

    Private Sub cmdDone_Click(sender As System.Object, e As System.EventArgs) Handles cmdDone.Click
        Try
            With frmSetup3

                updateNodesConnections()
                updateNature()

            End With

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub updateNodesConnections()
        Try

            If currentNode = 0 Then Exit Sub
            If currentTree = 0 Then Exit Sub

            If validateInt(txtTop1.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).payoff11 = txtTop1.Text
            If validateInt(txtBottom1.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).payoff12 = txtBottom1.Text

            If validateInt(txtTop2.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).payoff21 = txtTop2.Text
            If validateInt(txtBottom2.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).payoff22 = txtBottom2.Text

            If validateInt(txtTop3.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).payoff31 = txtTop3.Text
            If validateInt(txtBottom3.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).payoff32 = txtBottom3.Text

            If validateInt(nudOwner.Value, 6, False, True) Then nodeListBase(currentNode, currentTree).owner = nudOwner.Value
            nodeListBase(currentNode, currentTree).id = currentNode


            nodeListBase(currentNode, currentTree).pt3 =
                New Point(nodeListBase(currentNode, currentTree).pt1.X + 100, nodeListBase(currentNode, currentTree).pt1.Y)

            If validateInt(txtSubNode1.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).subNode1Id = txtSubNode1.Text


            nodeListBase(currentNode, currentTree).pt2 =
                New Point(nodeListBase(currentNode, currentTree).pt1.X, nodeListBase(currentNode, currentTree).pt1.Y + 100)

            If validateInt(txtSubNode2.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).subNode2Id = txtSubNode2.Text

            nodeListBase(currentNode, currentTree).pt4 =
                New Point(nodeListBase(currentNode, currentTree).pt1.X - 100, nodeListBase(currentNode, currentTree).pt1.Y)

            If validateInt(txtSubNode3.Text, 6, False, True) Then nodeListBase(currentNode, currentTree).subNode3Id = txtSubNode3.Text
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub TxtNatureRightSuccessX_TextChanged(sender As Object, e As EventArgs) Handles txtNatureRightSuccessX.TextChanged, txtNatureRightSuccessY.TextChanged,
                                                                                             txtNatureDownSuccessX.TextChanged, txtNatureDownSuccessY.TextChanged,
                                                                                             txtNatureLeftSuccessX.TextChanged, txtNatureLeftSuccessY.TextChanged,
                                                                                             txtNatureRightSubSuccessX.TextChanged, txtNatureRightSubSuccessY.TextChanged,
                                                                                             txtNatureDownSubSuccessX.TextChanged, txtNatureDownSubSuccessY.TextChanged,
                                                                                             txtNatureLeftSubSuccessX.TextChanged, txtNatureLeftSubSuccessY.TextChanged,
                                                                                             txtNatureRightSuccessStart.TextChanged, txtNatureRightSuccessEnd.TextChanged,
                                                                                             txtNatureDownSuccessStart.TextChanged, txtNatureDownSuccessEnd.TextChanged,
                                                                                             txtNatureLeftSuccessStart.TextChanged, txtNatureLeftSuccessEnd.TextChanged,
                                                                                             txtNatureRightSubSuccessStart.TextChanged, txtNatureRightSubSuccessEnd.TextChanged,
                                                                                             txtNatureDownSubSuccessStart.TextChanged, txtNatureDownSubSuccessEnd.TextChanged,
                                                                                             txtNatureLeftSubSuccessStart.TextChanged, txtNatureLeftSubSuccessEnd.TextChanged
        Try
            If Not validateInt(txtNatureRightSuccessStart.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureRightSuccessEnd.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureRightSuccessX.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureRightSuccessY.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtNatureDownSuccessStart.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureDownSuccessEnd.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureDownSuccessX.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureDownSuccessY.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtNatureLeftSuccessStart.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureLeftSuccessEnd.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureLeftSuccessX.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureLeftSuccessY.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtNatureRightSubSuccessStart.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureRightSubSuccessEnd.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureRightSubSuccessX.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureRightSubSuccessY.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtNatureDownSubSuccessStart.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureDownSubSuccessEnd.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureDownSubSuccessX.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureDownSubSuccessY.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtNatureLeftSubSuccessStart.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureLeftSubSuccessEnd.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureLeftSubSuccessX.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtNatureLeftSubSuccessY.Text, 6, False, True) Then Exit Sub

            updateNature()
            If currentNode <> 0 And currentTree <> 0 Then nodeListBase(currentNode, currentTree).updateComputerLabelText()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub updateNature()
        Try
            If currentNode = 0 Or currentTree = 0 Then Exit Sub

            nodeListBase(currentNode, currentTree).natureRightSuccessStart = txtNatureRightSuccessStart.Text
            nodeListBase(currentNode, currentTree).natureRightSuccessEnd = txtNatureRightSuccessEnd.Text
            nodeListBase(currentNode, currentTree).natureDownSuccessStart = txtNatureDownSuccessStart.Text
            nodeListBase(currentNode, currentTree).natureDownSuccessEnd = txtNatureDownSuccessEnd.Text
            nodeListBase(currentNode, currentTree).natureLeftSuccessStart = txtNatureLeftSuccessStart.Text
            nodeListBase(currentNode, currentTree).natureLeftSuccessEnd = txtNatureLeftSuccessEnd.Text

            nodeListBase(currentNode, currentTree).natureRightSubSuccessStart = txtNatureRightSubSuccessStart.Text
            nodeListBase(currentNode, currentTree).natureRightSubSuccessEnd = txtNatureRightSubSuccessEnd.Text
            nodeListBase(currentNode, currentTree).natureDownSubSuccessStart = txtNatureDownSubSuccessStart.Text
            nodeListBase(currentNode, currentTree).natureDownSubSuccessEnd = txtNatureDownSubSuccessEnd.Text
            nodeListBase(currentNode, currentTree).natureLeftSubSuccessStart = txtNatureLeftSubSuccessStart.Text
            nodeListBase(currentNode, currentTree).natureLeftSubSuccessEnd = txtNatureLeftSubSuccessEnd.Text

            nodeListBase(currentNode, currentTree).natureDrawRangeStart = txtNatureDrawRangeStart.Text
            nodeListBase(currentNode, currentTree).natureDrawRangeEnd = txtNatureDrawRangeEnd.Text

            nodeListBase(currentNode, currentTree).natureRightSuccessX = txtNatureRightSuccessX.Text
            nodeListBase(currentNode, currentTree).natureRightSuccessY = txtNatureRightSuccessY.Text
            nodeListBase(currentNode, currentTree).natureDownSuccessX = txtNatureDownSuccessX.Text
            nodeListBase(currentNode, currentTree).natureDownSuccessY = txtNatureDownSuccessY.Text
            nodeListBase(currentNode, currentTree).natureLeftSuccessX = txtNatureLeftSuccessX.Text
            nodeListBase(currentNode, currentTree).natureLeftSuccessY = txtNatureLeftSuccessY.Text

            nodeListBase(currentNode, currentTree).natureRightSubSuccessX = txtNatureRightSubSuccessX.Text
            nodeListBase(currentNode, currentTree).natureRightSubSuccessY = txtNatureRightSubSuccessY.Text
            nodeListBase(currentNode, currentTree).natureDownSubSuccessX = txtNatureDownSubSuccessX.Text
            nodeListBase(currentNode, currentTree).natureDownSubSuccessY = txtNatureDownSubSuccessY.Text
            nodeListBase(currentNode, currentTree).natureLeftSubSuccessX = txtNatureLeftSubSuccessX.Text
            nodeListBase(currentNode, currentTree).natureLeftSubSuccessY = txtNatureLeftSubSuccessY.Text
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub TxtTop1_TextChanged(sender As Object, e As EventArgs) Handles txtTop1.TextChanged, txtTop2.TextChanged, txtTop3.TextChanged,
                                                                              txtBottom1.TextChanged, txtBottom2.TextChanged, txtBottom3.TextChanged,
                                                                              txtSubNode1.TextChanged, txtSubNode2.TextChanged, txtSubNode3.TextChanged
        Try
            If Not validateInt(txtTop1.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtTop2.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtTop3.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtBottom1.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtBottom2.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtBottom3.Text, 6, False, True) Then Exit Sub

            If Not validateInt(txtSubNode1.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtSubNode2.Text, 6, False, True) Then Exit Sub
            If Not validateInt(txtSubNode3.Text, 6, False, True) Then Exit Sub

            updateNodesConnections()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub NudOwner_ValueChanged(sender As Object, e As EventArgs) Handles nudOwner.ValueChanged
        Try
            If Not validateInt(nudOwner.Text, 6, False, True) Then Exit Sub

            updateNodesConnections()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class