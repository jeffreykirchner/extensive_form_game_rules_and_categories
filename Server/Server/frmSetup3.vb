Public Class frmSetup3

    Public mainScreen As Screen
    'Public nodeList(100, 100) As node  'ID/Period
    'Public nodeCount(100) As Integer
    Public currentTree As Integer = 1
    Public currentNode As Integer = 1

    Dim p1 As Pen

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Try
            For i As Integer = 1 To treeCount
                writeINI(sfile, "nodeCount", CStr(i), nodeCountBase(i))

                For j As Integer = 1 To nodeCountBase(i)
                    writeINI(sfile, "node" & i & "-" & j, "owner", nodeListBase(j, i).owner)
                    writeINI(sfile, "node" & i & "-" & j, "payoff11", nodeListBase(j, i).payoff11)
                    writeINI(sfile, "node" & i & "-" & j, "payoff12", nodeListBase(j, i).payoff12)
                    writeINI(sfile, "node" & i & "-" & j, "payoff21", nodeListBase(j, i).payoff21)
                    writeINI(sfile, "node" & i & "-" & j, "payoff22", nodeListBase(j, i).payoff22)
                    writeINI(sfile, "node" & i & "-" & j, "payoff31", nodeListBase(j, i).payoff31)
                    writeINI(sfile, "node" & i & "-" & j, "payoff32", nodeListBase(j, i).payoff32)

                    writeINI(sfile, "node" & i & "-" & j, "pt1", nodeListBase(j, i).pt1.ToString)
                    writeINI(sfile, "node" & i & "-" & j, "pt2", nodeListBase(j, i).pt2.ToString)
                    writeINI(sfile, "node" & i & "-" & j, "pt3", nodeListBase(j, i).pt3.ToString)
                    writeINI(sfile, "node" & i & "-" & j, "pt4", nodeListBase(j, i).pt4.ToString)

                    writeINI(sfile, "node" & i & "-" & j, "subNode1Id", nodeListBase(j, i).subNode1Id)
                    writeINI(sfile, "node" & i & "-" & j, "subNode2Id", nodeListBase(j, i).subNode2Id)
                    writeINI(sfile, "node" & i & "-" & j, "subNode3Id", nodeListBase(j, i).subNode3Id)

                    writeINI(sfile, "node" & i & "-" & j, "natureRightSuccessStart", nodeListBase(j, i).natureRightSuccessStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureRightSuccessEnd", nodeListBase(j, i).natureRightSuccessEnd)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSuccessStart", nodeListBase(j, i).natureDownSuccessStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSuccessEnd", nodeListBase(j, i).natureDownSuccessEnd)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSuccessStart", nodeListBase(j, i).natureLeftSuccessStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSuccessEnd", nodeListBase(j, i).natureLeftSuccessEnd)

                    writeINI(sfile, "node" & i & "-" & j, "natureRightSubSuccessStart", nodeListBase(j, i).natureRightSubSuccessStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureRightSubSuccessEnd", nodeListBase(j, i).natureRightSubSuccessEnd)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSubSuccessStart", nodeListBase(j, i).natureDownSubSuccessStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSubSuccessEnd", nodeListBase(j, i).natureDownSubSuccessEnd)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSubSuccessStart", nodeListBase(j, i).natureLeftSubSuccessStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSubSuccessEnd", nodeListBase(j, i).natureLeftSubSuccessEnd)

                    writeINI(sfile, "node" & i & "-" & j, "natureDrawRangeStart", nodeListBase(j, i).natureDrawRangeStart)
                    writeINI(sfile, "node" & i & "-" & j, "natureDrawRangeEnd", nodeListBase(j, i).natureDrawRangeEnd)

                    writeINI(sfile, "node" & i & "-" & j, "natureRightSuccessX", nodeListBase(j, i).natureRightSuccessX)
                    writeINI(sfile, "node" & i & "-" & j, "natureRightSuccessY", nodeListBase(j, i).natureRightSuccessY)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSuccessX", nodeListBase(j, i).natureDownSuccessX)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSuccessY", nodeListBase(j, i).natureDownSuccessY)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSuccessX", nodeListBase(j, i).natureLeftSuccessX)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSuccessY", nodeListBase(j, i).natureLeftSuccessY)

                    writeINI(sfile, "node" & i & "-" & j, "natureRightSubSuccessX", nodeListBase(j, i).natureRightSubSuccessX)
                    writeINI(sfile, "node" & i & "-" & j, "natureRightSubSuccessY", nodeListBase(j, i).natureRightSubSuccessY)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSubSuccessX", nodeListBase(j, i).natureDownSubSuccessX)
                    writeINI(sfile, "node" & i & "-" & j, "natureDownSubSuccessY", nodeListBase(j, i).natureDownSubSuccessY)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSubSuccessX", nodeListBase(j, i).natureLeftSubSuccessX)
                    writeINI(sfile, "node" & i & "-" & j, "natureLeftSubSuccessY", nodeListBase(j, i).natureLeftSubSuccessY)


                Next
            Next

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub frmSetup3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            With frmMain
                mainScreen = New Screen(pnlMain, New Rectangle(0, 0, pnlMain.Width, pnlMain.Height))
                Timer1.Enabled = True

                .setupNodesBase(sfile)

                p1 = New Pen(Brushes.Violet, 3)

                If nodeCountBase(currentTree) > 0 Then
                    currentNode = 1
                    lblCurrentNode.Text = "Current Node: 1"
                Else
                    currentNode = 0
                    lblCurrentNode.Text = "Current Node: --"
                End If

                currentTree = 1
                lblPeriod.Text = "Tree: " & currentTree
                cmdMinus.Visible = False
                If numberOfPeriods = 1 Then cmdPlus.Visible = False
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Public Sub drawScreen()
        Try
            mainScreen.erase1()
            Dim g As Graphics = mainScreen.GetGraphics

            'draw highlight
            If currentNode <= nodeCountBase(currentTree) And currentNode <> 0 And cbGrid.Checked Then
                g.FillRectangle(Brushes.Yellow,
                                New Rectangle(nodeListBase(currentNode, currentTree).pt1.X - 50,
                                              nodeListBase(currentNode, currentTree).pt1.Y - 50,
                                              100,
                                              100))
            End If

            'draw grid
            'halfs
            If cbGrid.Checked Then
                For i As Integer = 1 To 3

                    'vertical
                    If currentNode > 0 Then
                        If nodeListBase(currentNode, currentTree).pt1.X = CInt(pnlMain.Width / 4 * i) Then
                            g.DrawLine(Pens.Violet, CInt(pnlMain.Width / 4 * i), 0, CInt(pnlMain.Width / 4 * i), pnlMain.Height)
                        Else
                            g.DrawLine(Pens.LightGray, CInt(pnlMain.Width / 4 * i), 0, CInt(pnlMain.Width / 4 * i), pnlMain.Height)
                        End If
                    Else
                        g.DrawLine(Pens.LightGray, CInt(pnlMain.Width / 4 * i), 0, CInt(pnlMain.Width / 4 * i), pnlMain.Height)
                    End If


                    'horizontal
                    If currentNode > 0 Then
                        If nodeListBase(currentNode, currentTree).pt1.Y = CInt(pnlMain.Height / 4 * i) Then
                            g.DrawLine(Pens.Violet, 0, CInt(pnlMain.Height / 4 * i), pnlMain.Width, CInt(pnlMain.Height / 4 * i))
                        Else
                            g.DrawLine(Pens.LightGray, 0, CInt(pnlMain.Height / 4 * i), pnlMain.Width, CInt(pnlMain.Height / 4 * i))
                        End If
                    Else
                        g.DrawLine(Pens.LightGray, 0, CInt(pnlMain.Height / 4 * i), pnlMain.Width, CInt(pnlMain.Height / 4 * i))
                    End If

                Next
            End If

            'draw nodes
            For i As Integer = 1 To nodeCountBase(currentTree)
                If nodeListBase(i, currentTree) IsNot Nothing Then
                    nodeListBase(i, currentTree).drawNode(g, False, nodeListBase, currentTree, cbGrid.Checked, cbDistance.Checked)
                End If
            Next

            'draw alignment lines
            If cbGrid.Checked Then
                For i As Integer = 1 To nodeCountBase(currentTree)

                    If i <> currentNode Then
                        If nodeListBase(i, currentTree).pt1.X = nodeListBase(currentNode, currentTree).pt1.X Or
                           nodeListBase(i, currentTree).pt1.Y = nodeListBase(currentNode, currentTree).pt1.Y Then

                            g.DrawLine(p1, nodeListBase(currentNode, currentTree).pt1, nodeListBase(i, currentTree).pt1)

                        End If
                    End If
                Next
            End If


            mainScreen.flip()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdAddNode_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddNode.Click
        Try
            nodeCountBase(currentTree) += 1
            nodeListBase(nodeCountBase(currentTree), currentTree) = New node

            frmSetup3_1.Show()
            frmSetup3_1.currentTree = currentTree
            frmSetup3_1.currentNode = nodeCountBase(currentTree)

            currentNode = nodeCountBase(currentTree)

            lblCurrentNode.Text = "Current Node: " & currentNode

            nodeListBase(currentNode, currentTree).pt1 = New Point(100, 100)
            nodeListBase(currentNode, currentTree).pt2 = New Point(100, 200)
            nodeListBase(currentNode, currentTree).pt3 = New Point(200, 100)
            nodeListBase(currentNode, currentTree).pt4 = New Point(0, 200)

            nodeListBase(currentNode, currentTree).id = currentNode
            nodeListBase(currentNode, currentTree).myPeriod = currentTree
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Dim mouseIsDown = False
    Dim oldMouseX As Integer = 0
    Dim oldMouseY As Integer = 0

    Private Sub pnlMain_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseDown
        Try

            If nodeCountBase(currentTree) = 0 Then Exit Sub

            For i As Integer = 1 To nodeCountBase(currentTree)
                If nodeListBase(i, currentTree).isOver(e.X, e.Y) Then
                    currentNode = i
                    lblCurrentNode.Text = "Current Node: " & currentNode
                    Exit For
                End If
            Next

            mouseIsDown = True
            oldMouseX = e.X
            oldMouseY = e.Y

            pnlMain.Focus()

            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub pnlMain_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseUp
        Try
            mouseIsDown = False
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Private Sub pnlMain_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseMove
        Try
            If mouseIsDown And currentNode <= nodeCountBase(currentTree) Then

                Dim tempDifX As Integer = oldMouseX - e.X
                Dim tempDifY As Integer = oldMouseY - e.Y

                nodeListBase(currentNode, currentTree).pt1.X -= tempDifX
                nodeListBase(currentNode, currentTree).pt2.X -= tempDifX
                nodeListBase(currentNode, currentTree).pt3.X -= tempDifX
                nodeListBase(currentNode, currentTree).pt4.X -= tempDifX

                nodeListBase(currentNode, currentTree).pt1.Y -= tempDifY
                nodeListBase(currentNode, currentTree).pt2.Y -= tempDifY
                nodeListBase(currentNode, currentTree).pt3.Y -= tempDifY
                nodeListBase(currentNode, currentTree).pt4.Y -= tempDifY

                oldMouseX = e.X
                oldMouseY = e.Y
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub nudCurrentNode_ValueChanged(sender As System.Object, e As System.EventArgs)
        Try

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdEditNode_Click(sender As System.Object, e As System.EventArgs) Handles cmdEditNode.Click
        Try
            frmSetup3_1.Show()
            frmSetup3_1.currentTree = currentTree
            frmSetup3_1.currentNode = currentNode

            frmSetup3_1.txtTop1.Text = nodeListBase(currentNode, currentTree).payoff11
            frmSetup3_1.txtBottom1.Text = nodeListBase(currentNode, currentTree).payoff12

            frmSetup3_1.txtTop2.Text = nodeListBase(currentNode, currentTree).payoff21
            frmSetup3_1.txtBottom2.Text = nodeListBase(currentNode, currentTree).payoff22

            frmSetup3_1.txtTop3.Text = nodeListBase(currentNode, currentTree).payoff31
            frmSetup3_1.txtBottom3.Text = nodeListBase(currentNode, currentTree).payoff32

            frmSetup3_1.nudOwner.Value = nodeListBase(currentNode, currentTree).owner

            frmSetup3_1.txtSubNode1.Text = nodeListBase(currentNode, currentTree).subNode1Id
            frmSetup3_1.txtSubNode2.Text = nodeListBase(currentNode, currentTree).subNode2Id
            frmSetup3_1.txtSubNode3.Text = nodeListBase(currentNode, currentTree).subNode3Id

            frmSetup3_1.txtNatureRightSuccessStart.Text = nodeListBase(currentNode, currentTree).natureRightSuccessStart
            frmSetup3_1.txtNatureRightSuccessEnd.Text = nodeListBase(currentNode, currentTree).natureRightSuccessEnd
            frmSetup3_1.txtNatureDownSuccessStart.Text = nodeListBase(currentNode, currentTree).natureDownSuccessStart
            frmSetup3_1.txtNatureDownSuccessEnd.Text = nodeListBase(currentNode, currentTree).natureDownSuccessEnd
            frmSetup3_1.txtNatureLeftSuccessStart.Text = nodeListBase(currentNode, currentTree).natureLeftSuccessStart
            frmSetup3_1.txtNatureLeftSuccessEnd.Text = nodeListBase(currentNode, currentTree).natureLeftSuccessEnd

            frmSetup3_1.txtNatureRightSubSuccessStart.Text = nodeListBase(currentNode, currentTree).natureRightSubSuccessStart
            frmSetup3_1.txtNatureRightSubSuccessEnd.Text = nodeListBase(currentNode, currentTree).natureRightSubSuccessEnd
            frmSetup3_1.txtNatureDownSubSuccessStart.Text = nodeListBase(currentNode, currentTree).natureDownSubSuccessStart
            frmSetup3_1.txtNatureDownSubSuccessEnd.Text = nodeListBase(currentNode, currentTree).natureDownSubSuccessEnd
            frmSetup3_1.txtNatureLeftSubSuccessStart.Text = nodeListBase(currentNode, currentTree).natureLeftSubSuccessStart
            frmSetup3_1.txtNatureLeftSubSuccessEnd.Text = nodeListBase(currentNode, currentTree).natureLeftSubSuccessEnd

            'frmSetup3_1.txtNatureDrawRangeStart.Text = nodeListBase(currentNode, currentTree).natureDrawRangeStart
            'frmSetup3_1.txtNatureDrawRangeEnd.Text = nodeListBase(currentNode, currentTree).natureDrawRangeEnd

            frmSetup3_1.txtNatureRightSuccessX.Text = nodeListBase(currentNode, currentTree).natureRightSuccessX
            frmSetup3_1.txtNatureRightSuccessY.Text = nodeListBase(currentNode, currentTree).natureRightSuccessY
            frmSetup3_1.txtNatureDownSuccessX.Text = nodeListBase(currentNode, currentTree).natureDownSuccessX
            frmSetup3_1.txtNatureDownSuccessY.Text = nodeListBase(currentNode, currentTree).natureDownSuccessY
            frmSetup3_1.txtNatureLeftSuccessX.Text = nodeListBase(currentNode, currentTree).natureLeftSuccessX
            frmSetup3_1.txtNatureLeftSuccessY.Text = nodeListBase(currentNode, currentTree).natureLeftSuccessY

            frmSetup3_1.txtNatureRightSubSuccessX.Text = nodeListBase(currentNode, currentTree).natureRightSubSuccessX
            frmSetup3_1.txtNatureRightSubSuccessY.Text = nodeListBase(currentNode, currentTree).natureRightSubSuccessY
            frmSetup3_1.txtNatureDownSubSuccessX.Text = nodeListBase(currentNode, currentTree).natureDownSubSuccessX
            frmSetup3_1.txtNatureDownSubSuccessY.Text = nodeListBase(currentNode, currentTree).natureDownSubSuccessY
            frmSetup3_1.txtNatureLeftSubSuccessX.Text = nodeListBase(currentNode, currentTree).natureLeftSubSuccessX
            frmSetup3_1.txtNatureLeftSubSuccessY.Text = nodeListBase(currentNode, currentTree).natureLeftSubSuccessY

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdConnectNode_Click(sender As System.Object, e As System.EventArgs) Handles cmdConnectNode.Click
        Try
            frmSetup3_2.Show()

            For i As Integer = 1 To nodeCountBase(currentTree)
                frmSetup3_2.cboSource.Items.Add("Node " & i)
                frmSetup3_2.cboTarget.Items.Add("Node " & i)
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdDeleteNode_Click(sender As System.Object, e As System.EventArgs) Handles cmdDeleteNode.Click
        Try
            If MessageBox.Show("Delect Selected Node?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) =
               Windows.Forms.DialogResult.No Then Exit Sub

            For i As Integer = currentNode To nodeCountBase(currentTree) - 1
                nodeListBase(i, currentTree) = nodeListBase(i + 1, currentTree)

                nodeListBase(i, currentTree).id = i

                If nodeListBase(i, currentTree).subNode1Id > 0 Then nodeListBase(i, currentTree).subNode1Id -= 1
                If nodeListBase(i, currentTree).subNode2Id > 0 Then nodeListBase(i, currentTree).subNode2Id -= 1
            Next

            nodeCountBase(currentTree) -= 1
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdCopyPrevious_Click(sender As System.Object, e As System.EventArgs) Handles cmdCopyPrevious.Click
        Try
            If currentTree = 1 Then Exit Sub

            Dim originalTree = InputBox("Which tree do you want to copy?", "Copy Tree", currentTree - 1)

            If Not validateInt(originalTree, 2, False, False) Then Exit Sub

            If originalTree > treeCount Then Exit Sub

            For i As Integer = 1 To nodeCountBase(originalTree)
                nodeListBase(i, currentTree) = New node

                nodeListBase(i, currentTree).fromString(nodeListBase(i, originalTree).toString)

                nodeListBase(i, currentTree).myTree = currentTree
            Next

            nodeCountBase(currentTree) = nodeCountBase(originalTree)

            currentNode = 1
            lblCurrentNode.Text = "Current Node: " & currentNode
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        Try
            If MessageBox.Show("Delect ALL Nodes?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) =
               Windows.Forms.DialogResult.No Then Exit Sub

            nodeCountBase(currentTree) = 0
            currentNode = 0
            lblCurrentNode.Text = "Current Node: --"
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub frmSetup3_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try

            If nodeCountBase(currentTree) = 0 Then Exit Sub
            If currentNode = 0 Then Exit Sub

            If e.KeyCode = Keys.D Then
                nodeListBase(currentNode, currentTree).nudge(1, 0)
            ElseIf e.KeyCode = Keys.A Then
                nodeListBase(currentNode, currentTree).nudge(-1, 0)
            ElseIf e.KeyCode = Keys.W Then
                nodeListBase(currentNode, currentTree).nudge(0, -1)
            ElseIf e.KeyCode = Keys.S Then
                nodeListBase(currentNode, currentTree).nudge(0, 1)
            End If

            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdPlus_Click(sender As Object, e As EventArgs) Handles cmdPlus.Click
        Try
            currentTree += 1

            If currentTree = treeCount Then
                cmdPlus.Visible = False
            End If

            cmdMinus.Visible = True

            lblPeriod.Text = "Tree: " & currentTree
            currentNode = 1
            lblCurrentNode.Text = "Current Node: " & currentNode

            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub cmdMinus_Click(sender As Object, e As EventArgs) Handles cmdMinus.Click
        Try
            currentTree -= 1

            If currentTree = 1 Then
                cmdMinus.Visible = False
            End If

            cmdPlus.Visible = True

            lblPeriod.Text = "Tree: " & currentTree
            currentNode = 1
            lblCurrentNode.Text = "Current Node: " & currentNode

            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class