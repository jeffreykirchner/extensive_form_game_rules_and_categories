Public Class frmMain
    'Public WithEvents mobjSocketClient As TCPConnection
    Delegate Sub SetTextCallback(ByVal [text] As String)
    Delegate Sub SetTextCallback2()

    Public mainScreen As screen

    Public diceImages(6) As Image
    Public diceQ As Image = Image.FromFile(System.Windows.Forms.Application.StartupPath & "\graphics\DiceQ.png")

    'graphics
    Public f1 As New Font("Calibri", 40, FontStyle.Bold)
    Public f2 As New Font("Calibri", 16, FontStyle.Bold)
    Public f2Underline As New Font("Calibri", 16, FontStyle.Bold Or FontStyle.Underline)
    Public f3 As New Font("Calibri", 10, FontStyle.Bold)
    Public f18 As New Font("Calibri", 18, FontStyle.Bold)

    Public fmt As New StringFormat 'center alignment

    Public p1 As New Pen(Brushes.Black, 8)             'black arrow
    Public p2 As New Pen(Brushes.CornflowerBlue, 8)    'blue arrow, type 1 
    Public p3 As New Pen(Brushes.Coral, 8)             'orange arrow, type 2 
    Public p4 As New Pen(Brushes.Black, 8)             'black triangle cap
    Public p5 As New Pen(Brushes.LightGray, 8)
    Public pYellow16 As New Pen(Brushes.Yellow, 16)
    Public pYellow6 As New Pen(Brushes.Yellow, 6)
    Public pRed6 As New Pen(Brushes.Crimson, 6)
    Public pYellow4 As New Pen(Brushes.Yellow, 4)
    Public pYellow2 As New Pen(Brushes.Yellow, 2)
    Public pGreen6 As New Pen(Brushes.LightGreen, 6)
    Public pYellowArrow4 As New Pen(Brushes.Yellow, 4)


    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            'if ALT+K are pressed kill the client
            'if ALT+Q are pressed bring up connection box
            If e.Alt = True Then
                If CInt(e.KeyValue) = CInt(Keys.K) Then
                    If MessageBox.Show("Close Program?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
                    modMain.closing = True
                    Me.Close()
                ElseIf CInt(e.KeyValue) = CInt(Keys.Q) Then
                    frmConnect.Show()
                End If
            End If
        Catch ex As Exception
            appEventLog_Write("error frmChat_KeyDown:", ex)
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            sfile = System.Windows.Forms.Application.StartupPath & "\client.ini"

            'take IP from command line
            Dim commandLine As String = Command()

            If commandLine <> "" Then
                writeINI(sfile, "Settings", "ip", commandLine)
            End If

            'connect
            myIPAddress = getINI(sfile, "Settings", "ip")
            myPortNumber = getINI(sfile, "Settings", "port")
            connect()

            mainScreen = New screen(pnlMain, New Rectangle(0, 0, pnlMain.Width, pnlMain.Height))

            For i As Integer = 1 To 6
                diceImages(i) = Image.FromFile(System.Windows.Forms.Application.StartupPath & "\graphics\Dice" & CStr(i) & ".png")
            Next

            fmt.Alignment = StringAlignment.Center
            p1.EndCap = Drawing2D.LineCap.ArrowAnchor
            p1.Alignment = Drawing2D.PenAlignment.Center

            p2.EndCap = Drawing2D.LineCap.ArrowAnchor
            p2.Alignment = Drawing2D.PenAlignment.Center

            p3.EndCap = Drawing2D.LineCap.ArrowAnchor
            p3.Alignment = Drawing2D.PenAlignment.Center

            p4.Alignment = Drawing2D.PenAlignment.Center
            p4.EndCap = Drawing2D.LineCap.Triangle

            p5.Alignment = Drawing2D.PenAlignment.Center
            p5.EndCap = Drawing2D.LineCap.ArrowAnchor

            pYellow16.Alignment = Drawing2D.PenAlignment.Center
            pYellow16.EndCap = Drawing2D.LineCap.Triangle

            pYellow6.Alignment = Drawing2D.PenAlignment.Center
            pYellow6.EndCap = Drawing2D.LineCap.Triangle

            pYellow4.Alignment = Drawing2D.PenAlignment.Center
            pYellow4.EndCap = Drawing2D.LineCap.Triangle

            pYellow2.Alignment = Drawing2D.PenAlignment.Center
            pYellow2.EndCap = Drawing2D.LineCap.Triangle

            pGreen6.Alignment = Drawing2D.PenAlignment.Center
            pGreen6.EndCap = Drawing2D.LineCap.Triangle

            pYellowArrow4.EndCap = Drawing2D.LineCap.DiamondAnchor
            pYellowArrow4.Alignment = Drawing2D.PenAlignment.Center

        Catch ex As Exception
            appEventLog_Write("errorfrmChat_Load :", ex)
        End Try

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If frmInstructions.Visible Then
                drawScreenInstructions()
            Else
                drawScreen()
            End If

        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub

    Public Sub drawScreenInstructions()
        Try
            mainScreen.erase1()
            Dim g As Graphics = mainScreen.GetGraphics

            'If selection <> "" Then
            '    g.FillRectangle(Brushes.Yellow,
            '                  New Rectangle(selectionPt.X - 38,
            '                                selectionPt.Y - 38,
            '                                76,
            '                                76))
            'End If

            'For i As Integer = 1 To nodeCountInstructions(currentPeriodInstruction)
            '    If nodeListInstructions(i, currentPeriodInstruction).subNode1Id > 0 Then
            '        nodeListInstructions(i, currentPeriodInstruction).pt2 = nodeListInstructions(nodeListInstructions(i, currentPeriodInstruction).subNode1Id, currentPeriodInstruction).pt1
            '    End If

            '    If nodeListInstructions(i, currentPeriodInstruction).subNode2Id > 0 Then
            '        nodeListInstructions(i, currentPeriodInstruction).pt3 = nodeListInstructions(nodeListInstructions(i, currentPeriodInstruction).subNode2Id, currentPeriodInstruction).pt1
            '    End If
            'Next

            For i As Integer = 1 To nodeCountInstructions(currentPeriodInstruction)
                If nodeListInstructions(i, currentPeriodInstruction) IsNot Nothing Then
                    nodeListInstructions(i, currentPeriodInstruction).drawNodeArrows(g)
                End If
            Next

            For i As Integer = 1 To nodeCountInstructions(currentPeriodInstruction)
                If nodeListInstructions(i, currentPeriodInstruction) IsNot Nothing Then
                    nodeListInstructions(i, currentPeriodInstruction).drawNode(g)
                End If
            Next

            mainScreen.flip()
        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub


    Public Sub drawScreen()
        Try
            mainScreen.erase1()
            Dim g As Graphics = mainScreen.GetGraphics

            If phase = periodPhase.treeTransition Or phase = periodPhase.waitAfterTreeTransition Then
                g.TranslateTransform(pnlMain.Width / 2, pnlMain.Height / 2)

                g.DrawString("The decision problem will" & vbCrLf & "change next period.", f1, Brushes.Black, 0, -100, fmt)

                g.ResetTransform()
            Else
                For i As Integer = 1 To nodeCount(currentPeriod)
                    If nodeList(i, currentPeriod) IsNot Nothing Then
                        nodeList(i, currentPeriod).drawSelection(g)
                    End If
                Next

                For i As Integer = 1 To nodeCount(currentPeriod)
                    If nodeList(i, currentPeriod) IsNot Nothing Then
                        nodeList(i, currentPeriod).drawNodeArrows(g)
                    End If
                Next

                For i As Integer = 1 To nodeCount(currentPeriod)
                    If nodeList(i, currentPeriod) IsNot Nothing Then
                        nodeList(i, currentPeriod).drawNode(g)
                    End If
                Next

                'key

                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                Dim keyWidth As Integer = 200
                Dim keyHeight As Integer = 125
                Dim keySpace As Integer = 40
                Dim top As Integer = 30

                If phase = periodPhase.submit Or phase = periodPhase.waitAfterSubmit Then
                    keyHeight = 125
                    keyWidth = 200
                    keySpace = 40
                    top = 30

                    g.TranslateTransform(25, pnlMain.Height - keyHeight - 25)

                    drawRoundedBox(g, 0, 0, keyWidth, keyHeight, 3, 1, Brushes.DimGray)

                    g.DrawString("Your options", f2, Brushes.Black, 10, top - 28)
                    If myType(currentPeriod) = 1 Then
                        g.DrawLine(p2, 10, top, keyWidth - 20, top)
                    Else
                        g.DrawLine(p3, 10, top, keyWidth - 20, top)
                    End If

                    top += keySpace + 3

                    g.DrawString("Your selection(s)", f2, Brushes.Black, 10, top - 32)
                    If myType(currentPeriod) = 1 Then

                        g.DrawLine(pYellow16, 10, top, keyWidth - 25, top)
                        g.DrawLine(p2, 10, top, keyWidth - 20, top)
                    Else
                        g.DrawLine(pYellow16, 10, top, keyWidth - 25, top)
                        g.DrawLine(p3, 10, top, keyWidth - 20, top)
                    End If

                    top += keySpace
                    g.DrawString("Other's options", f2, Brushes.Black, 10, top - 28)
                    g.DrawLine(p1, 10, top, keyWidth - 20, top)
                ElseIf phase = periodPhase.summaryResults Or phase = periodPhase.waitAfterSummaryResults Then
                    keyHeight = 140
                    keyWidth = 325
                    keySpace = 40
                    top = 5

                    g.TranslateTransform(25, pnlMain.Height - keyHeight - 25)

                    g.DrawString("Feedback", f18, Brushes.Black, 5, top - 32)

                    drawRoundedBox(g, 0, 0, keyWidth, keyHeight, 3, 1, Brushes.DimGray)

                    g.DrawString("Your selection" & If(myType(currentPeriod) = 1, "s", "") & ":", f2, Brushes.Black, 10, top)
                    top += 15
                    g.DrawLine(p4, 165, top, keyWidth - 20, top)
                    g.DrawLine(pYellow6, 165, top, keyWidth - 20, top)

                    Dim choiceLabelString1 As String = "The most popular choice" & If(myType(currentPeriod) = 1, "", "s")
                    Dim choiceLabelString2 As String = "among " & modalPoolSize & " randomly chosen"
                    Dim choiceLabelString3 As String = "Person " & If(myType(currentPeriod) = 1, "2", "1") & "s:"

                    top += 20
                    g.DrawString(choiceLabelString1, f2, Brushes.Black, 10, top)
                    top += 20
                    g.DrawString(choiceLabelString2, f2, Brushes.Black, 15, top)
                    top += 20
                    g.DrawString(choiceLabelString3, f2, Brushes.Black, 15, top)
                    top += 15
                    g.DrawLine(p4, 165, top, keyWidth - 20, top)
                    g.DrawLine(pRed6, 165, top, keyWidth - 20, top)
                    top += 10

                    g.DrawString("The Person " & If(myType(currentPeriod) = 1, "2", "1") & " you are interacting with this period " & vbCrLf & " may have made different choices.", f3, Brushes.DimGray, keyWidth / 2, top, fmt)

                ElseIf phase = periodPhase.finalResults Or phase = periodPhase.waitAfterFinalResults Then

                    g.DrawString("The Experiment is Over.", f18, Brushes.Black, pnlMain.Width / 2, 20, fmt)

                    keyHeight = 85
                    keyWidth = 250
                    keySpace = 40
                    top = 30

                    g.TranslateTransform(25, pnlMain.Height - keyHeight - 25)
                    drawRoundedBox(g, 0, 0, keyWidth, keyHeight, 3, 1, Brushes.DimGray)

                    g.DrawString("Your joint decision path", f2, Brushes.Black, 10, top - 28)
                    g.DrawLine(p4, 10, top, keyWidth - 20, top)
                    g.DrawLine(pGreen6, 10, top, keyWidth - 20, top)

                    top += keySpace
                    g.DrawString("Your selection(s)", f2, Brushes.Black, 10, top - 28)
                    g.DrawLine(pYellow6, 10, top, keyWidth - 20, top)

                    g.SmoothingMode = Drawing2D.SmoothingMode.None
                End If
                g.ResetTransform()
            End If



            mainScreen.flip()
        Catch ex As Exception
            appEventLog_Write("error Timer1_Tick:", ex)
        End Try
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            If cmdSubmit.Visible Or cmdForward.Visible Then
                If cmdSubmit.BackColor = Color.Gainsboro Then
                    cmdSubmit.BackColor = Color.LightGreen
                    cmdForward.BackColor = Color.LightGreen
                Else
                    cmdSubmit.BackColor = Color.Gainsboro
                    cmdForward.BackColor = Color.Gainsboro
                End If
            End If

            Dim go As Boolean = True

            'Dim tempNode As node

            'If currentNode = 0 Then Exit Sub

            'If showInstructions Then
            '    If currentNode > nodeCountInstructions(currentPeriodInstruction) Then Exit Sub

            '    tempNode = nodeListInstructions(currentNode, currentPeriodInstruction)
            'Else
            '    If currentNode > nodeCount(currentPeriod) Then Exit Sub

            '    tempNode = nodeList(currentNode, currentPeriod)
            'End If

            'Do While go

            If showInstructions Then
                For j As Integer = 1 To nodeListInstructionsCount
                    For i As Integer = 1 To nodeCountInstructions(j)
                        nodeListInstructions(i, j).doTickTock()
                    Next
                Next
            Else
                For i As Integer = 1 To nodeCount(currentPeriod)
                    nodeList(i, currentPeriod).doTickTock()
                Next
            End If




        Catch ex As Exception
            appEventLog_Write("error Timer2_Tick client:", ex)
        End Try
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not modMain.closing Then e.Cancel = True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub pnlMain_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlMain.MouseClick
        Try
            '  If frmInstructions.Visible Then
            '     pnlMainClickActionInstructions(e.X, e.Y)
            '  Else
            pnlMainClickAction(e.X, e.Y)
            '  End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub pnlMainClickActionInstructions(x As Integer, y As Integer)
        Try
            If cmdSubmit.Visible = False Then Exit Sub

            'If nodeListInstructions(currentNode, currentPeriodInstruction).isOverPT(x, y, nodeListInstructions(currentNode, currentPeriodInstruction).pt2) Then
            '    selectionPt = nodeListInstructions(currentNode, currentPeriodInstruction).pt2
            '    selection = "down"
            'ElseIf nodeListInstructions(currentNode, currentPeriodInstruction).isOverPT(x, y, nodeListInstructions(currentNode, currentPeriodInstruction).pt3) Then
            '    selectionPt = nodeListInstructions(currentNode, currentPeriodInstruction).pt3
            '    selection = "right"
            'End If

            drawScreenInstructions()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub pnlMainClickAction(x As Integer, y As Integer)
        Try
            If cmdSubmit.Visible = False Then Exit Sub
            If cmdSubmit.Text <> "Submit" Then Exit Sub

            Dim tempNode As node
            Dim tempNodeList(100, 100) As node
            Dim tempPeriod As Integer

            If showInstructions Then
                'tempNode = nodeListInstructions(currentNode, currentPeriodInstruction)
                tempNodeList = nodeListInstructions

                tempPeriod = 0

                If currentInstruction = 7 Then
                    tempPeriod = 1
                ElseIf currentInstruction = 8 Then
                    tempPeriod = 2
                ElseIf currentInstruction = 9 Then
                    tempPeriod = 3
                End If

                If tempPeriod = 0 Then Exit Sub

                For i As Integer = 1 To nodeCountInstructions(tempPeriod)
                    tempNode = nodeListInstructions(i, tempPeriod)
                    tempNodeList = nodeListInstructions
                    'tempPeriod = currentPeriod

                    'check over payoffs
                    If pnlMainClickAction2(tempNode, tempNodeList, tempPeriod, x, y) Then Exit For

                Next
            Else
                For i As Integer = 1 To nodeCount(currentPeriod)
                    tempNode = nodeList(i, currentPeriod)
                    tempNodeList = nodeList
                    tempPeriod = currentPeriod

                    'check over payoffs
                    If pnlMainClickAction2(tempNode, tempNodeList, tempPeriod, x, y) Then Exit For

                Next
            End If

            'update screen
            If showInstructions Then
                drawScreenInstructions()
            Else
                drawScreen()
            End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function pnlMainClickAction2(ByRef tempNode As node,
                                        ByRef tempNodeList(,) As node,
                                        ByRef tempPeriod As Integer,
                                        ByRef x As Integer,
                                        ByRef y As Integer) As Boolean
        Try
            If tempNode.owner = myType(currentPeriod) Then

                If tempNode.payoff11 >= 0 Then
                    If tempNode.isOverPT(x, y, tempNode.pt3) Or
                       tempNode.isOverArrow(x, y, tempNode.pt1, tempNode.pt3) Then

                        tempNode.selectionPt = tempNode.pt3
                        tempNode.selection = "pay1"
                        tempNode.tickTock = 1

                        Return True
                    End If
                End If

                If tempNode.payoff21 >= 0 Then
                    If tempNode.isOverPT(x, y, tempNode.pt2) Or
                       tempNode.isOverArrow(x, y, tempNode.pt1, tempNode.pt2) Then

                        tempNode.selectionPt = tempNode.pt2
                        tempNode.selection = "pay2"
                        tempNode.tickTock = 1

                        Return True
                    End If
                End If

                If tempNode.payoff31 >= 0 Then
                    If tempNode.isOverPT(x, y, tempNode.pt4) Or
                       tempNode.isOverArrow(x, y, tempNode.pt1, tempNode.pt4) Then

                        tempNode.selectionPt = tempNode.pt4
                        tempNode.selection = "pay3"
                        tempNode.tickTock = 1

                        Return True
                    End If
                End If

                'check over sub nodes
                If tempNode.subNode1Id > 0 Then
                    If tempNode.isOverPT(x, y, tempNodeList(tempNode.subNode1Id, tempPeriod).pt1) Or
                       tempNode.isOverArrow(x, y, tempNode.pt1, tempNodeList(tempNode.subNode1Id, tempPeriod).pt1) Then

                        tempNode.selectionPt = tempNodeList(tempNode.subNode1Id, tempPeriod).pt1
                        tempNode.selection = "sub1"
                        tempNode.tickTock = 1

                        Return True
                    End If
                End If

                If tempNode.subNode2Id > 0 Then
                    If tempNode.isOverPT(x, y, tempNodeList(tempNode.subNode2Id, tempPeriod).pt1) Or
                       tempNode.isOverArrow(x, y, tempNode.pt1, tempNodeList(tempNode.subNode2Id, tempPeriod).pt1) Then

                        tempNode.selectionPt = tempNodeList(tempNode.subNode2Id, tempPeriod).pt1
                        tempNode.selection = "sub2"
                        tempNode.tickTock = 1

                        Return True
                    End If
                End If

                If tempNode.subNode3Id > 0 Then
                    If tempNode.isOverPT(x, y, tempNodeList(tempNode.subNode3Id, tempPeriod).pt1) Or
                       tempNode.isOverArrow(x, y, tempNode.pt1, tempNodeList(tempNode.subNode3Id, tempPeriod).pt1) Then

                        tempNode.selectionPt = tempNodeList(tempNode.subNode3Id, tempPeriod).pt1
                        tempNode.selection = "sub3"
                        tempNode.tickTock = 1

                        Return True
                    End If
                End If
            End If

            Return False
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function


    Private Sub cmdSubmit_Click(sender As System.Object, e As System.EventArgs) Handles cmdSubmit.Click
        Try
            If Not cmdSubmit.Visible Then Exit Sub

            If frmInstructions.Visible = True Then
                cmdSubmitActionInstruction()
            Else
                cmdSubmitAction()
            End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub cmdSubmitActionInstruction()
        Try
            'If selection = "" Then Exit Sub

            If frmInstructions.pageDone(currentInstruction) Then Exit Sub

            If currentInstruction = instructionExamplePage1 Then

                If nodeListInstructions(1, 1).selection <> "sub1" Then
                    MessageBox.Show("Please take the requested action at the top node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If nodeListInstructions(3, 1).selection <> "pay3" And nodeListInstructions(3, 1).selection <> "pay2" Then
                    MessageBox.Show("Please take the requested action at the bottom node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                nodeListInstructions(2, 1).selection = "sub1"

                For i As Integer = 1 To nodeCountInstructions(1)
                    nodeListInstructions(i, 1).status = nodeListInstructions(i, 1).selection
                Next

                frmInstructions.results(1, 1) = nodeListInstructions(1, 1).selection
                frmInstructions.results(2, 1) = nodeListInstructions(2, 1).selection
                frmInstructions.results(3, 1) = nodeListInstructions(3, 1).selection

                frmInstructions.pageDone(currentInstruction) = True
            ElseIf currentInstruction = instructionExamplePage2 Then

                If nodeListInstructions(1, 2).selection <> "pay3" Then
                    MessageBox.Show("Please take the requested action at the top node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If nodeListInstructions(3, 2).selection <> "pay3" And nodeListInstructions(3, 2).selection <> "pay2" Then
                    MessageBox.Show("Please take the requested action at the bottom node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                nodeListInstructions(2, 2).selection = "pay3"

                nodeListInstructions(1, 2).status = "pay3"
                nodeListInstructions(2, 2).status = "dead"
                nodeListInstructions(3, 2).status = "dead"

                frmInstructions.results(1, 2) = nodeListInstructions(1, 1).selection
                frmInstructions.results(2, 2) = nodeListInstructions(2, 1).selection
                frmInstructions.results(3, 2) = nodeListInstructions(3, 1).selection

                frmInstructions.pageDone(currentInstruction) = True
            ElseIf currentInstruction = instructionExamplePage3 Then

                If nodeListInstructions(2, 3).selection <> "pay3" Then
                    MessageBox.Show("Please take the requested action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                nodeListInstructions(1, 3).selection = "sub1"
                nodeListInstructions(3, 3).selection = "pay3"

                nodeListInstructions(1, 3).status = "sub1"
                nodeListInstructions(2, 3).status = "pay3"
                nodeListInstructions(3, 3).status = "dead"

                frmInstructions.results(1, 3) = nodeListInstructions(1, 1).selection
                frmInstructions.results(2, 3) = nodeListInstructions(2, 1).selection
                frmInstructions.results(3, 3) = nodeListInstructions(3, 1).selection

                frmInstructions.pageDone(currentInstruction) = True
                'If selection = "sub1" Then
                '    currentNode = 3
                'Else
                '    currentNode = 2
                'End If

                'nodeListInstructions(1, 3).status = selection
            End If

            cmdSubmit.Visible = False
            'frmInstructions.pageDone(currentInstruction) = True
            'frmInstructions.results(currentInstruction) = selection
            frmInstructions.nextInstruction()

            pnlMain.Enabled = False
            'selection = ""

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Public Sub cmdSubmitAction()
        Try



            'If cmdSubmit.Text = "Submit" Then
            '    If selection = "" Then Exit Sub
            'End If



            If cmdSubmit.Text = "Submit" Then
                For i As Integer = 1 To nodeCount(currentPeriod)
                    If nodeList(i, currentPeriod).selection = "" And nodeList(i, currentPeriod).owner = myType(currentPeriod) Then
                        Exit Sub
                    End If
                Next

                cmdSubmit.Visible = False
                txtMessages.Text = "Waiting for others."

                Dim ts As TimeSpan
                ts = Now - decisionStart

                Dim outstr As String = ""
                Dim count As Integer = 0

                For i As Integer = 1 To nodeCount(currentPeriod)
                    If nodeList(i, currentPeriod).owner = myType(currentPeriod) Then

                        outstr &= i & ";"
                        outstr &= nodeList(i, currentPeriod).selection & ";"

                        count += 1
                    End If
                Next

                outstr = count & ";" & outstr
                outstr &= ts.TotalMilliseconds & ";"

                phase = periodPhase.waitAfterSubmit

                wskClient.Send("04", outstr)
            Else
                Dim outstr As String = ""
                cmdSubmit.Visible = False
                txtMessages.Text = "Waiting for others."

                If phase = periodPhase.summaryResults Then
                    phase = periodPhase.waitAfterSummaryResults
                ElseIf phase = periodPhase.finalResults Then
                    phase = periodPhase.waitAfterFinalResults
                ElseIf phase = periodPhase.treeTransition Then
                    phase = periodPhase.waitAfterTreeTransition
                End If

                cmdSubmit.Text = "Submit"

                wskClient.Send("05", outstr)
            End If

            'Dim a As Integer = 1
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub Timer3_Tick(sender As System.Object, e As System.EventArgs) Handles Timer3.Tick
        Try
            Timer3.Interval = rand(1500, 500)

            If frmInstructions.Visible Then
                doTestModeInstructions()
            Else
                doTestMode()
            End If


        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub CmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Try
            If Not gbResults.Visible Then Exit Sub

            If currentPeriod > finalResultsStartBlock Then currentPeriod -= 1

            updateFinalResults()
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Sub CmdForward_Click(sender As Object, e As EventArgs) Handles cmdForward.Click
        Try
            If Not gbResults.Visible Then Exit Sub

            If currentPeriod <finalResultsEndBlock Then currentPeriod += 1

            updateFinalResults()
            drawScreen()
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class
