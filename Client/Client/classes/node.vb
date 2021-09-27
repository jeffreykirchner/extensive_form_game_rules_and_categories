Public Class node
    Public id As Integer
    Public myPeriod As Integer

    Public myNodeList(100, 100) As node

    Public radius As Integer = 25

    Public subNode1Id As Integer
    Public subNode2Id As Integer
    Public subNode3Id As Integer

    Public payoff11 As Double
    Public payoff12 As Double
    Public payoff21 As Double
    Public payoff22 As Double
    Public payoff31 As Double
    Public payoff32 As Double

    Public owner As Integer
    Public pt1 As Point                'location of node
    Public pt2 As Point                'location of payoff 2  
    Public pt3 As Point                'location of payoff 1 
    Public pt4 As Point                'location of payoff 3 

    Public status As String
    Public myColor As Color

    'colors for player 1,2 and nature
    Dim b1 As Brush
    Dim b2 As Brush
    Dim bN As Brush

    Public tickTock As Integer = 0

    Public selectionPt As Point
    Public selection As String

    'nature success probabilty text/locations
    Public natureRightText As String
    Public natureDownText As String
    Public natureLeftText As String

    Public natureRightSuccessX As Integer
    Public natureRightSuccessY As Integer
    Public natureDownSuccessX As Integer
    Public natureDownSuccessY As Integer
    Public natureLeftSuccessX As Integer
    Public natureLeftSuccessY As Integer

    Public natureRightSubText As String
    Public natureDownSubText As String
    Public natureLeftSubText As String

    Public natureRightSubSuccessX As Integer
    Public natureRightSubSuccessY As Integer
    Public natureDownSubSuccessX As Integer
    Public natureDownSubSuccessY As Integer
    Public natureLeftSubSuccessX As Integer
    Public natureLeftSubSuccessY As Integer

    Public Sub New(ByRef myNodeList(,) As node)
        Try


            Me.myNodeList = myNodeList

            b1 = Brushes.CornflowerBlue
            b2 = Brushes.Coral
            bN = Brushes.White
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawNode(g As Graphics)
        Try
            With frmMain
                pt3 = New Point(pt1.X + payOffDistance, pt1.Y)
                pt2 = New Point(pt1.X, pt1.Y + payOffDistance)
                pt4 = New Point(pt1.X - payOffDistance, pt1.Y)

                Dim tempOffset As Integer = 30
                Dim tempF1 As Font = .f2
                Dim tempF2 As Font = .f2

                Dim resultsPhase As Boolean = False

                If phase = periodPhase.finalResults Or phase = periodPhase.waitAfterFinalResults Then
                    If myType(currentPeriod) = 1 Then
                        tempF1 = .f2Underline
                    Else
                        tempF2 = .f2Underline
                    End If
                    resultsPhase = True
                End If

                'payoffs
                If payoff11 >= 0 And payoff12 >= 0 Then

                    If status = "dead" Or
                        InStr(status, "sub") Or
                        status = "pay2" Or
                        status = "pay3" Then

                        drawPayoff(pt3, g, payoff11, payoff12, Brushes.LightGray, .f2, .f2, False)
                    Else
                        drawPayoff(pt3, g, payoff11, payoff12, Brushes.Black, tempF1, tempF2, If(resultsPhase, True, False))
                    End If
                End If

                If payoff21 >= 0 And payoff22 >= 0 Then

                    If status = "dead" Or
                       InStr(status, "sub") Or
                       status = "pay1" Or
                       status = "pay3" Then

                        drawPayoff(pt2, g, payoff21, payoff22, Brushes.LightGray, .f2, .f2, False)
                    Else

                        drawPayoff(pt2, g, payoff21, payoff22, Brushes.Black, tempF1, tempF2, If(resultsPhase, True, False))
                    End If
                End If

                If payoff31 >= 0 And payoff32 >= 0 Then

                    If status = "dead" Or
                       InStr(status, "sub") Or
                       status = "pay1" Or
                       status = "pay2" Then

                        drawPayoff(pt4, g, payoff31, payoff32, Brushes.LightGray, .f2, .f2, False)
                    Else

                        drawPayoff(pt4, g, payoff31, payoff32, Brushes.Black, tempF1, tempF2, If(resultsPhase, True, False))
                    End If
                End If

                'node
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                g.FillEllipse(New SolidBrush(myColor), New Rectangle(pt1.X - radius, pt1.Y - radius, radius * 2, radius * 2))
                g.DrawEllipse(Pens.Black, New Rectangle(pt1.X - radius, pt1.Y - radius, radius * 2, radius * 2))

                If owner <> 0 Then
                    'human controlled

                    g.SmoothingMode = Drawing2D.SmoothingMode.None

                    Dim tempO As String = owner.ToString
                    g.DrawString(tempO, .f2, Brushes.Black, pt1.X - g.MeasureString(owner, .f2).Width / 2, pt1.Y - 15)
                Else
                    'nature controlled
                    With frmMain
                        ' If status = "open" Then
                        g.DrawImage(.diceQ,
                                  New Rectangle(pt1.X - .diceQ.Width / 2 + 1,
                                                pt1.Y - .diceQ.Height / 2,
                                                .diceQ.Width,
                                                .diceQ.Height))
                        'Else
                        'g.DrawImage(currentDieImage,
                        '                              New Rectangle(pt1.X - currentDieImage.Width / 2 + 1,
                        '                                            pt1.Y - currentDieImage.Height / 2,
                        '                                            currentDieImage.Width,
                        '                                            currentDieImage.Height))
                        'End If

                        If natureRightSuccessX <> -1 Then
                            g.DrawString(natureRightText, .f3, Brushes.DimGray, New Point(pt1.X + natureRightSuccessX, pt1.Y + natureRightSuccessY))
                        End If

                        If natureDownSuccessX <> -1 Then
                            g.DrawString(natureDownText, .f3, Brushes.DimGray, New Point(pt1.X + natureDownSuccessX, pt1.Y + natureDownSuccessY))
                        End If

                        If natureLeftSuccessX <> -1 Then
                            g.DrawString(natureLeftText, .f3, Brushes.DimGray, New Point(pt1.X + natureLeftSuccessX, pt1.Y + natureLeftSuccessY))
                        End If

                        If natureRightSubSuccessX <> -1 Then
                            g.DrawString(natureRightSubText, .f3, Brushes.DimGray, New Point(pt1.X + natureRightSubSuccessX, pt1.Y + natureRightSubSuccessY))
                        End If

                        If natureDownSubSuccessX <> -1 Then
                            g.DrawString(natureDownSubText, .f3, Brushes.DimGray, New Point(pt1.X + natureDownSubSuccessX, pt1.Y + natureDownSubSuccessY))
                        End If

                        If natureLeftSubSuccessX <> -1 Then
                            g.DrawString(natureLeftSubText, .f3, Brushes.DimGray, New Point(pt1.X + natureLeftSubSuccessX, pt1.Y + natureLeftSubSuccessY))
                        End If
                    End With
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawConnection(startPt As Point, endPt As Point, g As Graphics, tempP As Pen)
        Try

            Dim d As Double = Math.Sqrt((endPt.X - startPt.X) ^ 2 + (endPt.Y - startPt.Y) ^ 2)
            Dim r As Double = 28 / d

            Dim x3 As Integer = Math.Round(r * startPt.X + (1 - r) * endPt.X)
            Dim y3 As Integer = Math.Round(r * startPt.Y + (1 - r) * endPt.Y)

            g.DrawLine(tempP, startPt.X, startPt.Y, x3, y3)

            'isOverArrow(1, 1, startPt, endPt, g)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawPayoff(startPt As Point, g As Graphics, payOffTop As String, payOffBottom As String, tempB As Brush, tempF1 As Font, tempF2 As Font, finalPayoff As Boolean)
        Try
            With frmMain
                If showInstructions Then
                    payOffTop = returnInstructionPayoff(payOffTop)
                    payOffBottom = returnInstructionPayoff(payOffBottom)
                End If

                Dim pp As String = getPayoffParenthesis(payOffTop, payOffBottom)

                If finalPayoff Then
                    Dim r As Rectangle = New Rectangle(startPt.X - g.MeasureString(pp, .f1).Width / 2,
                                                                      startPt.Y - g.MeasureString(pp, .f1).Height / 2,
                                                                      g.MeasureString(pp, .f1).Width,
                                                                      g.MeasureString(pp, .f1).Height)
                    g.FillRectangle(Brushes.LightGreen, r)
                    g.DrawRectangle(Pens.Black, r)
                End If

                g.DrawString(pp, .f1, tempB, startPt.X, startPt.Y - g.MeasureString(pp, .f1).Height / 2, .fmt)

                If payoffMode = "dollars" Then
                    payOffTop = "$" & payOffTop
                    payOffBottom = "$" & payOffBottom
                ElseIf payoffMode = "cents" Then
                    payOffTop = payOffTop & "¢"
                    payOffBottom = payOffBottom & "¢"
                Else
                    payOffTop = "£" & payOffTop
                    payOffBottom = "£" & payOffBottom
                End If

                g.DrawString(payOffTop, tempF1, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, .fmt)
                g.DrawString(payOffBottom, tempF2, Brushes.Coral, startPt.X, startPt.Y - 2, .fmt)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Private Function getPayoffParenthesis(payOffTop As String, payOffBottom As String) As String
        Try
            Dim tempN2 As Integer = Len(payOffTop)
            Dim tempN3 As Integer = Len(payOffBottom)

            If InStr(payOffTop, ".") > 0 Then
                tempN2 -= 1
            End If

            If InStr(payOffBottom, ".") > 0 Then
                tempN3 -= 1
            End If

            Dim tempN1 As Integer = Math.Max(tempN2, tempN3)

            Dim temps As String = "("

            For i As Integer = 1 To tempN1 + 1 'for $,cents sign
                temps &= " "
            Next

            temps &= ")"

            Return temps

        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function

    Public Sub drawSelection(g As Graphics)
        Try

            'If selection <> "" Then
            '    g.FillRectangle(Brushes.Yellow,
            '                      New Rectangle(selectionPt.X - 38,
            '                                    selectionPt.Y - 38,
            '                                    76,
            '                                    76))
            'End If

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub


    Public Sub drawNodeArrows(g As Graphics)
        Try
            With frmMain
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                Dim tempP As Pen
                If status = "open" And owner = myType(currentPeriod) Then
                    If myColor = Color.CornflowerBlue Then
                        tempP = .p2
                    Else
                        tempP = .p3
                    End If
                ElseIf status = "open" Then
                    tempP = .p1
                    'standard black
                Else
                    tempP = .p5
                End If

                Dim tempNodeCount As Integer

                If showInstructions Then
                    tempNodeCount = nodeCountInstructions(currentPeriodInstruction)
                Else
                    tempNodeCount = nodeCount(currentPeriod)
                End If

                'draw sub node arrows
                drawNodeArrowsSubNode(subNode1Id, tempNodeCount, pt1, g, 1, "sub1", tempP)
                drawNodeArrowsSubNode(subNode2Id, tempNodeCount, pt1, g, 1, "sub2", tempP)
                drawNodeArrowsSubNode(subNode3Id, tempNodeCount, pt1, g, 1, "sub3", tempP)


                'draw payoff arrows
                Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff11, payoff12), .f1).Width
                drawNodeArrowsPayoffs(payoff11, payoff12, g, "pay1", CInt(pt3.X - Math.Round(tempD / 2) + 10), pt3.Y, 1, tempP)

                drawNodeArrowsPayoffs(payoff21, payoff22, g, "pay2", pt2.X, pt2.Y - radius, 1, tempP)

                tempD = g.MeasureString(getPayoffParenthesis(payoff31, payoff32), .f1).Width
                drawNodeArrowsPayoffs(payoff31, payoff32, g, "pay3", CInt(pt4.X + Math.Round(tempD / 2) - 10), pt4.Y, 1, tempP)

                g.SmoothingMode = Drawing2D.SmoothingMode.None
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawNodeArrowsPayoffs(payoff1 As Double,
                                     payoff2 As Double,
                                     g As Graphics,
                                     statusValue As String,
                                     x As Integer,
                                     y As Integer,
                                     tickTockValue As Integer,
                                     tempP As Pen)
        Try
            With frmMain
                If payoff1 >= 0 And payoff2 >= 0 Then

                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff1, payoff2), .f1).Width

                    If status = statusValue Then
                        g.DrawLine(.p4, pt1.X, pt1.Y, x, y)

                        If (phase = periodPhase.summaryResults Or
                            phase = periodPhase.waitAfterSummaryResults) And
                            owner = myType(currentPeriod) Then

                            g.DrawLine(.pYellow6, pt1.X, pt1.Y, x, y)
                        ElseIf phase = periodPhase.finalResults Or
                               phase = periodPhase.waitAfterFinalResults Then

                            g.DrawLine(.pGreen6, pt1.X, pt1.Y, x, y)

                            If owner = myType(currentPeriod) Then
                                g.DrawLine(.pYellow2, pt1.X, pt1.Y, x, y)
                            End If
                        End If
                    ElseIf myType(currentPeriod) <> owner Then
                        g.DrawLine(tempP, pt1.X, pt1.Y, x, y)
                    Else
                        'If tickTock = tickTockValue Or tickTock = 0 Or myType(currentPeriod) <> owner Then
                        '    g.DrawLine(tempP, pt1.X, pt1.Y, x, y)
                        'End If

                        If tickTock = 0 Then
                            g.DrawLine(tempP, pt1.X, pt1.Y, x, y)

                            'final results selection
                            If (phase = periodPhase.finalResults Or
                                phase = periodPhase.waitAfterFinalResults) And
                                owner = myType(currentPeriod) And
                                selection = statusValue Then

                                g.DrawLine(.pYellow16, pt1.X, pt1.Y, x, y)
                                g.DrawLine(tempP, pt1.X, pt1.Y, x, y)
                            End If
                        ElseIf (phase = periodPhase.submit And tickTock = tickTockValue) Or phase = periodPhase.waitAfterSubmit Then


                            If selection = statusValue Then
                                g.DrawLine(.pYellow16, pt1.X, pt1.Y, x, y)
                            End If

                            g.DrawLine(tempP, pt1.X, pt1.Y, x, y)
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawNodeArrowsSubNode(subNodeId As Integer,
                                     tempNodeCount As Integer,
                                     pt As Point,
                                     g As Graphics,
                                     tickTockValue As Integer,
                                     statusValue As String,
                                     tempP As Pen)
        Try
            With frmMain
                If subNodeId > 0 Then
                    If tempNodeCount >= subNodeId Then

                        If status = statusValue Then
                            drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, .p4)

                            'hight light choice in yellow
                            If (phase = periodPhase.summaryResults Or
                                phase = periodPhase.waitAfterSummaryResults) And
                                owner = myType(currentPeriod) Then

                                drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, .pYellow6)

                            ElseIf phase = periodPhase.finalResults Or
                                   phase = periodPhase.waitAfterFinalResults Then

                                drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, .pGreen6)

                                If owner = myType(currentPeriod) Then
                                    drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, .pYellow2)
                                End If

                            End If
                        ElseIf myType(currentPeriod) <> owner Then
                            drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, tempP)
                        Else

                            If tickTock = 0 Then
                                drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, tempP)

                                'hight light selection
                                If (phase = periodPhase.finalResults Or
                                    phase = periodPhase.waitAfterFinalResults) And
                                    owner = myType(currentPeriod) And
                                    selection = statusValue Then

                                    drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, .pYellow16)
                                    drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, tempP)
                                End If
                            ElseIf (phase = periodPhase.submit And tickTock = tickTockValue) Or phase = periodPhase.waitAfterSubmit Then
                                If selection = statusValue Then
                                    drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, .pYellow16)
                                End If

                                drawConnection(pt, myNodeList(subNodeId, myPeriod).pt1, g, tempP)
                            End If

                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub doTickTock()
        Try
            Dim go As Boolean = True

            If phase = periodPhase.summaryResults Or
               phase = periodPhase.waitAfterSummaryResults Or
               phase = periodPhase.finalResults Or
               phase = periodPhase.waitAfterFinalResults Then

                tickTock = 0

            Else
                If selection <> "" Then
                    tickTock = 1
                Else
                    If tickTock = 1 Then
                        tickTock = 2
                    Else
                        tickTock = 1
                    End If
                End If

            End If

            'Do While go
            '    tickTock += 1

            '    If tickTock > 6 Then tickTock = 1

            '    Select Case tickTock
            '        Case 1
            '            If payoff11 >= 0 Then go = False
            '        Case 2
            '            If payoff21 >= 0 Then go = False
            '        Case 3
            '            If payoff31 >= 0 Then go = False
            '        Case 4
            '            If subNode1Id > 0 Then go = False
            '        Case 5
            '            If subNode2Id > 0 Then go = False
            '        Case 6
            '            If subNode3Id > 0 Then go = False
            '    End Select

            'Loop
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function isOverPT(x As Integer, y As Integer, pt As Point) As Boolean
        Try
            If x >= pt.X - radius And x <= pt.X + radius And y >= pt.Y - radius And y <= pt.Y + radius Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function

    Public Function isOverArrow(x As Integer, y As Integer, startPt As Point, endPt As Point, Optional g As Graphics = Nothing) As Boolean
        Try

            Dim dx As Double = endPt.X - startPt.X
            Dim dy As Double = endPt.Y - startPt.Y

            Dim increment, tempX, tempY As Double


            If Math.Abs(dx) > Math.Abs(dy) Then
                increment = Math.Abs(dx)
            Else
                increment = Math.Abs(dy)
            End If

            dx = dx / increment
            dy = dy / increment

            tempX = startPt.X
            tempY = startPt.Y

            Dim i As Integer = 1

            While i <= increment


                'g.FillEllipse(Brushes.Purple, New Rectangle(New Point(tempX - 4, tempY - 4), New Size(8, 8)))

                Dim r As New Rectangle(tempX - 6, tempY - 6, 12, 12)
                If r.Contains(x, y) And i > radius Then Return True

                tempX += dx
                tempY += dy
                i += 1
            End While

            'g.DrawLine(Pens.Purple, startPt, endPt)

            Return False
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function
End Class
