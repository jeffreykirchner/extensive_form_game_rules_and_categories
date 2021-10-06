Public Class node
    Public id As Integer
    Public myPeriod As Integer
    Public myTree As Integer

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

    Public pt1 As Point             'node location
    Public pt2 As Point
    Public pt3 As Point
    Public pt4 As Point

    'nature
    Public natureRightSuccessStart As Integer
    Public natureRightSuccessEnd As Integer
    Public natureDownSuccessStart As Integer
    Public natureDownSuccessEnd As Integer
    Public natureLeftSuccessStart As Integer
    Public natureLeftSuccessEnd As Integer

    Public natureRightSubSuccessStart As Integer
    Public natureRightSubSuccessEnd As Integer
    Public natureDownSubSuccessStart As Integer
    Public natureDownSubSuccessEnd As Integer
    Public natureLeftSubSuccessStart As Integer
    Public natureLeftSubSuccessEnd As Integer

    Public natureDrawRangeStart As Integer = 1
    Public natureDrawRangeEnd As Integer = 6
    Public natureDraw As Integer

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

    'display counts
    Public count As Integer
    Public countRight As Integer
    Public countDown As Integer
    Public countLeft As Integer
    Public countSubRight As Integer
    Public countSubDown As Integer
    Public countSubLeft As Integer

    Public status As String

    Dim f1 As New Font("Calibri", 40, FontStyle.Bold)
    Dim f2 As New Font("Calibri", 16, FontStyle.Bold)
    Dim f2Underline As New Font("Calibri", 16, FontStyle.Bold Or FontStyle.Underline)
    Dim f3 As New Font("Calibri", 10, FontStyle.Bold)
    Dim fmt As New StringFormat 'center alignment

    'colors for player 1,2 and nature
    Dim b1 As Brush
    Dim b2 As Brush
    Dim bN As Brush

    Dim p1 As New Pen(Brushes.Black, 8)

    Public selectionPt As Point
    Public selection As String
    Public Sub New()
        Try
            fmt.Alignment = StringAlignment.Center

            count = 0

            countRight = 0
            countDown = 0
            countLeft = 0

            countSubRight = 0
            countSubDown = 0
            countSubLeft = 0


            p1.EndCap = Drawing2D.LineCap.ArrowAnchor
            p1.Alignment = Drawing2D.PenAlignment.Center

            b1 = Brushes.CornflowerBlue
            b2 = Brushes.Coral
            bN = Brushes.White
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Overrides Function toString() As String
        Try
            Dim outstr As String = ""

            outstr &= id & ";"
            outstr &= subNode1Id & ";"
            outstr &= subNode2Id & ";"
            outstr &= subNode3Id & ";"
            outstr &= payoff11 & ";"
            outstr &= payoff12 & ";"
            outstr &= payoff21 & ";"
            outstr &= payoff22 & ";"
            outstr &= payoff31 & ";"
            outstr &= payoff32 & ";"
            outstr &= owner & ";"
            outstr &= pt1.X & ";"
            outstr &= pt1.Y & ";"
            outstr &= pt2.X & ";"
            outstr &= pt2.Y & ";"
            outstr &= pt3.X & ";"
            outstr &= pt3.Y & ";"
            outstr &= pt4.X & ";"
            outstr &= pt4.Y & ";"

            outstr &= natureRightSuccessStart & ";"
            outstr &= natureRightSuccessEnd & ";"
            outstr &= natureDownSuccessStart & ";"
            outstr &= natureDownSuccessEnd & ";"
            outstr &= natureLeftSuccessStart & ";"
            outstr &= natureLeftSuccessEnd & ";"

            outstr &= natureRightSubSuccessStart & ";"
            outstr &= natureRightSubSuccessEnd & ";"
            outstr &= natureDownSubSuccessStart & ";"
            outstr &= natureDownSubSuccessEnd & ";"
            outstr &= natureLeftSubSuccessStart & ";"
            outstr &= natureLeftSubSuccessEnd & ";"

            outstr &= natureDrawRangeStart & ";"
            outstr &= natureDrawRangeEnd & ";"

            outstr &= natureRightSuccessX & ";"
            outstr &= natureRightSuccessY & ";"
            outstr &= natureDownSuccessX & ";"
            outstr &= natureDownSuccessY & ";"
            outstr &= natureLeftSuccessX & ";"
            outstr &= natureLeftSuccessY & ";"

            outstr &= natureRightSubSuccessX & ";"
            outstr &= natureRightSubSuccessY & ";"
            outstr &= natureDownSubSuccessX & ";"
            outstr &= natureDownSubSuccessY & ";"
            outstr &= natureLeftSubSuccessX & ";"
            outstr &= natureLeftSubSuccessY & ";"

            Return outstr
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function

    Public Sub fromString(ByVal sinstr As String)
        Try
            Dim msgtokens() As String = sinstr.Split(";")
            Dim nextToken As Integer = 0

            id = msgtokens(nextToken)
            nextToken += 1

            subNode1Id = msgtokens(nextToken)
            nextToken += 1

            subNode2Id = msgtokens(nextToken)
            nextToken += 1

            subNode3Id = msgtokens(nextToken)
            nextToken += 1

            payoff11 = msgtokens(nextToken)
            nextToken += 1

            payoff12 = msgtokens(nextToken)
            nextToken += 1

            payoff21 = msgtokens(nextToken)
            nextToken += 1

            payoff22 = msgtokens(nextToken)
            nextToken += 1

            payoff31 = msgtokens(nextToken)
            nextToken += 1

            payoff32 = msgtokens(nextToken)
            nextToken += 1

            owner = msgtokens(nextToken)
            nextToken += 1

            pt1.X = msgtokens(nextToken)
            nextToken += 1

            pt1.Y = msgtokens(nextToken)
            nextToken += 1

            pt2.X = msgtokens(nextToken)
            nextToken += 1

            pt2.Y = msgtokens(nextToken)
            nextToken += 1

            pt3.X = msgtokens(nextToken)
            nextToken += 1

            pt3.Y = msgtokens(nextToken)
            nextToken += 1

            pt4.X = msgtokens(nextToken)
            nextToken += 1

            pt4.Y = msgtokens(nextToken)
            nextToken += 1

            natureRightSuccessStart = msgtokens(nextToken)
            nextToken += 1

            natureRightSuccessEnd = msgtokens(nextToken)
            nextToken += 1

            natureDownSuccessStart = msgtokens(nextToken)
            nextToken += 1

            natureDownSuccessEnd = msgtokens(nextToken)
            nextToken += 1

            natureLeftSuccessStart = msgtokens(nextToken)
            nextToken += 1

            natureLeftSuccessEnd = msgtokens(nextToken)
            nextToken += 1

            natureRightSubSuccessStart = msgtokens(nextToken)
            nextToken += 1

            natureRightSubSuccessEnd = msgtokens(nextToken)
            nextToken += 1

            natureDownSubSuccessStart = msgtokens(nextToken)
            nextToken += 1

            natureDownSubSuccessEnd = msgtokens(nextToken)
            nextToken += 1

            natureLeftSubSuccessStart = msgtokens(nextToken)
            nextToken += 1

            natureLeftSubSuccessEnd = msgtokens(nextToken)
            nextToken += 1

            natureDrawRangeStart = msgtokens(nextToken)
            nextToken += 1

            natureDrawRangeEnd = msgtokens(nextToken)
            nextToken += 1

            natureRightSuccessX = msgtokens(nextToken)
            nextToken += 1

            natureRightSuccessY = msgtokens(nextToken)
            nextToken += 1

            natureDownSuccessX = msgtokens(nextToken)
            nextToken += 1

            natureDownSuccessY = msgtokens(nextToken)
            nextToken += 1

            natureLeftSuccessX = msgtokens(nextToken)
            nextToken += 1

            natureLeftSuccessY = msgtokens(nextToken)
            nextToken += 1

            natureRightSubSuccessX = msgtokens(nextToken)
            nextToken += 1

            natureRightSubSuccessY = msgtokens(nextToken)
            nextToken += 1

            natureDownSubSuccessX = msgtokens(nextToken)
            nextToken += 1

            natureDownSubSuccessY = msgtokens(nextToken)
            nextToken += 1

            natureLeftSubSuccessX = msgtokens(nextToken)
            nextToken += 1

            natureLeftSubSuccessY = msgtokens(nextToken)
            nextToken += 1

            updateComputerLabelText()

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub loadFromFile(tempFile As String, id As Integer, myPeriod As Integer, myTree As Integer)
        Try

            Me.id = id
            Me.myPeriod = myPeriod
            Me.myTree = myTree

            status = "open"

            If getINI(sfile, "node" & myTree & "-" & id, "owner") = "?" Then Exit Sub

            owner = getINI(sfile, "node" & myTree & "-" & id, "owner")
            payoff11 = getINI(sfile, "node" & myTree & "-" & id, "payoff11")
            payoff12 = getINI(sfile, "node" & myTree & "-" & id, "payoff12")
            payoff21 = getINI(sfile, "node" & myTree & "-" & id, "payoff21")
            payoff22 = getINI(sfile, "node" & myTree & "-" & id, "payoff22")
            payoff31 = getINI(sfile, "node" & myTree & "-" & id, "payoff31")
            payoff32 = getINI(sfile, "node" & myTree & "-" & id, "payoff32")

            pt1 = pointFromString(getINI(sfile, "node" & myTree & "-" & id, "pt1"))
            pt2 = pointFromString(getINI(sfile, "node" & myTree & "-" & id, "pt2"))
            pt3 = pointFromString(getINI(sfile, "node" & myTree & "-" & id, "pt3"))
            pt4 = pointFromString(getINI(sfile, "node" & myTree & "-" & id, "pt4"))

            subNode1Id = getINI(sfile, "node" & myTree & "-" & id, "subNode1Id")
            subNode2Id = getINI(sfile, "node" & myTree & "-" & id, "subNode2Id")
            subNode3Id = getINI(sfile, "node" & myTree & "-" & id, "subNode3Id")

            natureRightSuccessStart = getINI(sfile, "node" & myTree & "-" & id, "natureRightSuccessStart")
            natureRightSuccessEnd = getINI(sfile, "node" & myTree & "-" & id, "natureRightSuccessEnd")
            natureDownSuccessStart = getINI(sfile, "node" & myTree & "-" & id, "natureDownSuccessStart")
            natureDownSuccessEnd = getINI(sfile, "node" & myTree & "-" & id, "natureDownSuccessEnd")
            natureLeftSuccessStart = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSuccessStart")
            natureLeftSuccessEnd = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSuccessEnd")

            natureRightSubSuccessStart = getINI(sfile, "node" & myTree & "-" & id, "natureRightSubSuccessStart")
            natureRightSubSuccessEnd = getINI(sfile, "node" & myTree & "-" & id, "natureRightSubSuccessEnd")
            natureDownSubSuccessStart = getINI(sfile, "node" & myTree & "-" & id, "natureDownSubSuccessStart")
            natureDownSubSuccessEnd = getINI(sfile, "node" & myTree & "-" & id, "natureDownSubSuccessEnd")
            natureLeftSubSuccessStart = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSubSuccessStart")
            natureLeftSubSuccessEnd = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSubSuccessEnd")

            natureDrawRangeStart = getINI(sfile, "node" & myTree & "-" & id, "natureDrawRangeStart")
            natureDrawRangeEnd = getINI(sfile, "node" & myTree & "-" & id, "natureDrawRangeEnd")

            natureRightSuccessX = getINI(sfile, "node" & myTree & "-" & id, "natureRightSuccessX")
            natureRightSuccessY = getINI(sfile, "node" & myTree & "-" & id, "natureRightSuccessY")
            natureDownSuccessX = getINI(sfile, "node" & myTree & "-" & id, "natureDownSuccessX")
            natureDownSuccessY = getINI(sfile, "node" & myTree & "-" & id, "natureDownSuccessY")
            natureLeftSuccessX = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSuccessX")
            natureLeftSuccessY = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSuccessY")

            natureRightSubSuccessX = getINI(sfile, "node" & myTree & "-" & id, "natureRightSubSuccessX")
            natureRightSubSuccessY = getINI(sfile, "node" & myTree & "-" & id, "natureRightSubSuccessY")
            natureDownSubSuccessX = getINI(sfile, "node" & myTree & "-" & id, "natureDownSubSuccessX")
            natureDownSubSuccessY = getINI(sfile, "node" & myTree & "-" & id, "natureDownSubSuccessY")
            natureLeftSubSuccessX = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSubSuccessX")
            natureLeftSubSuccessY = getINI(sfile, "node" & myTree & "-" & id, "natureLeftSubSuccessY")
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub updateComputerLabelText()
        Try
            'pay outs
            If natureRightSuccessStart = -1 Then
                natureRightText = ""
            ElseIf natureRightSuccessStart = natureRightSuccessEnd Then
                natureRightText = "(" & natureRightSuccessStart & ")"
            Else
                natureRightText = "(" & natureRightSuccessStart & " to " & natureRightSuccessEnd & ")"
            End If

            If natureDownSuccessStart = -1 Then
                natureDownText = ""
            ElseIf natureDownSuccessStart = natureDownSuccessEnd Then
                natureDownText = "(" & natureDownSuccessStart & ")"
            Else
                natureDownText = "(" & natureDownSuccessStart & " to " & natureDownSuccessEnd & ")"
            End If

            If natureLeftSubSuccessStart = -1 Then
                natureLeftText = ""
            ElseIf natureLeftSubSuccessStart = natureLeftSubSuccessEnd Then
                natureLeftText = "(" & natureLeftSubSuccessStart & ")"
            Else
                natureLeftSubText = "(" & natureLeftSubSuccessStart & " to " & natureLeftSubSuccessStart & ")"
            End If

            'sub nodes
            If natureRightSubSuccessStart = -1 Then
                natureRightSubText = ""
            ElseIf natureRightSubSuccessStart = natureRightSubSuccessEnd Then
                natureRightSubText = "(" & natureRightSubSuccessStart & ")"
            Else
                natureRightSubText = "(" & natureRightSubSuccessStart & " to " & natureRightSubSuccessEnd & ")"
            End If

            If natureDownSubSuccessStart = -1 Then
                natureDownSubText = ""
            ElseIf natureDownSubSuccessStart = natureDownSubSuccessEnd Then
                natureDownSubText = "(" & natureDownSubSuccessStart & ")"
            Else
                natureDownSubText = "(" & natureDownSubSuccessStart & " to " & natureDownSubSuccessEnd & ")"
            End If

            If natureLeftSubSuccessStart = -1 Then
                natureLeftSubText = ""
            ElseIf natureLeftSubSuccessStart = natureLeftSubSuccessEnd Then
                natureLeftSubText = "(" & natureLeftSubSuccessStart & ")"
            Else
                natureLeftSubText = "(" & natureLeftSubSuccessStart & " to " & natureLeftSubSuccessStart & ")"
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawNode(g As Graphics, drawCount As Boolean, nodeList(,) As node, index As Integer, Optional drawId As Boolean = True, Optional showDistance As Boolean = False)
        Try
            With frmMain

                pt3 = New Point(pt1.X + payOffDistance, pt1.Y)
                pt2 = New Point(pt1.X, pt1.Y + payOffDistance)
                pt4 = New Point(pt1.X - payOffDistance, pt1.Y)

                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                'draw sub node arrows
                If subNode1Id > 0 Then
                    If subNode1Id > 0 Then
                        drawConnection(pt1, nodeList(subNode1Id, index).pt1, g, 28, showDistance)
                    End If
                End If

                If subNode2Id > 0 Then
                    If subNode2Id > 0 Then
                        drawConnection(pt1, nodeList(subNode2Id, index).pt1, g, 28, showDistance)
                    End If
                End If

                If subNode3Id > 0 Then
                    If subNode3Id > 0 Then
                        drawConnection(pt1, nodeList(subNode3Id, index).pt1, g, 28, showDistance)
                    End If
                End If

                'draw payoffs
                If payoff11 >= 0 And payoff12 >= 0 Then
                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff11, payoff12), f1).Width

                    ' g.DrawLine(p1, pt1.X, pt1.Y, CInt(pt3.X - Math.Round(tempD / 2) + 10), pt3.Y)
                    drawConnection(pt1, pt3, g, Math.Round(tempD / 2) - 10, showDistance)

                    drawPayoff(pt3, g, payoff11, payoff12, f2, f2)
                End If

                If payoff21 >= 0 And payoff22 >= 0 Then
                    'g.DrawLine(p1, pt1.X, pt1.Y, pt2.X, pt2.Y - 25)
                    drawConnection(pt1, pt2, g, 25, showDistance)

                    drawPayoff(pt2, g, payoff21, payoff22, f2, f2)
                End If

                If payoff31 >= 0 And payoff32 >= 0 Then
                    Dim tempD As Double = g.MeasureString(getPayoffParenthesis(payoff31, payoff32), f1).Width

                    'g.DrawLine(p1, pt1.X, pt1.Y, CInt(pt4.X + Math.Round(tempD / 2) - 10), pt4.Y)
                    drawConnection(pt1, pt4, g, Math.Round(tempD / 2) - 10, showDistance)

                    drawPayoff(pt4, g, payoff31, payoff32, f2, f2)
                End If

                g.SmoothingMode = Drawing2D.SmoothingMode.None

                If drawCount Then
                    If id > 1 Then
                        g.DrawString(count, f2, Brushes.DimGray, pt1.X - 30, pt1.Y - 40, fmt)
                    End If

                    If payoff11 >= 0 Then
                        g.DrawString(countRight, f2, Brushes.DimGray, pt3.X - 40, pt3.Y - 40, fmt)
                    End If

                    If payoff21 >= 0 Then
                        g.DrawString(countDown, f2, Brushes.DimGray, pt2.X - 40, pt2.Y - 40, fmt)
                    End If

                    If payoff31 >= 0 Then
                        g.DrawString(countLeft, f2, Brushes.DimGray, pt4.X - 40, pt4.Y - 40, fmt)
                    End If
                ElseIf drawId Then
                    g.DrawString("(" & id & ")", f3, Brushes.DimGray, pt1.X - 40, pt1.Y - 40)
                End If

                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                If owner = 1 Then
                    g.FillEllipse(b1, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))
                ElseIf owner = 2 Then
                    g.FillEllipse(b2, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))
                Else
                    g.FillEllipse(bN, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))
                End If

                Dim tempB As Brush
                If owner = 1 Then
                    tempB = b1
                ElseIf owner = 2 Then
                    tempB = b2
                Else
                    tempB = bN
                End If

                g.FillEllipse(tempB, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))

                g.DrawEllipse(Pens.Black, New Rectangle(pt1.X - 25, pt1.Y - 25, 50, 50))

                If owner <> 0 Then
                    'human controlled
                    g.SmoothingMode = Drawing2D.SmoothingMode.None

                    Dim tempO As String = owner.ToString
                    If owner = 0 Then tempO = "N"
                    g.DrawString(tempO, f2, Brushes.Black, pt1.X - g.MeasureString(owner, f2).Width / 2, pt1.Y - 15)
                Else
                    'nature controlled
                    g.DrawImage(.diceQ,
                             New Rectangle(pt1.X - .diceQ.Width / 2 + 1,
                                           pt1.Y - .diceQ.Height / 2,
                                           .diceQ.Width,
                                           .diceQ.Height))


                    If natureRightSuccessX <> -1 Then
                        g.DrawString(natureRightText, f3, Brushes.DimGray, New Point(pt1.X + natureRightSuccessX, pt1.Y + natureRightSuccessY))
                    End If

                    If natureDownSuccessX <> -1 Then
                        g.DrawString(natureDownText, f3, Brushes.DimGray, New Point(pt1.X + natureDownSuccessX, pt1.Y + natureDownSuccessY))
                    End If

                    If natureLeftSuccessX <> -1 Then
                        g.DrawString(natureLeftText, f3, Brushes.DimGray, New Point(pt1.X + natureLeftSuccessX, pt1.Y + natureLeftSuccessY))
                    End If

                    If natureRightSubSuccessX <> -1 Then
                        g.DrawString(natureRightSubText, f3, Brushes.DimGray, New Point(pt1.X + natureRightSubSuccessX, pt1.Y + natureRightSubSuccessY))
                    End If

                    If natureDownSubSuccessX <> -1 Then
                        g.DrawString(natureDownSubText, f3, Brushes.DimGray, New Point(pt1.X + natureDownSubSuccessX, pt1.Y + natureDownSubSuccessY))
                    End If

                    If natureLeftSubSuccessX <> -1 Then
                        g.DrawString(natureLeftSubText, f3, Brushes.DimGray, New Point(pt1.X + natureLeftSubSuccessX, pt1.Y + natureLeftSubSuccessY))
                    End If
                End If


            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawConnection(startPt As Point, endPt As Point, g As Graphics, standOff As Integer, Optional showDistance As Boolean = False)
        Try

            Dim d As Double = Math.Sqrt((endPt.X - startPt.X) ^ 2 + (endPt.Y - startPt.Y) ^ 2)
            Dim r As Double = standOff / d

            Dim x3 As Integer = Math.Round(r * startPt.X + (1 - r) * endPt.X)
            Dim y3 As Integer = Math.Round(r * startPt.Y + (1 - r) * endPt.Y)

            g.DrawLine(p1, startPt.X, startPt.Y, x3, y3)

            If showDistance Then
                g.DrawString(GetDistance(startPt, New Point(x3, y3)).ToString(), f3, Brushes.DimGray, New Point((startPt.X + x3) / 2, (startPt.Y + y3) / 2), fmt)
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub drawPayoff(startPt As Point, g As Graphics, payOffTop As String, payOffBottom As String, tempF1 As Font, tempF2 As Font)
        Try

            g.DrawString(getPayoffParenthesis(payOffTop, payOffBottom), f1, Brushes.Black, startPt.X, startPt.Y - g.MeasureString("(  )", f1).Height / 2, fmt)

            'If payoffMode = "dollars" Then
            '    g.DrawString("$" & payOffTop, f1, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
            '    g.DrawString("$" & payOffBottom, f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
            'ElseIf payoffMode = "cents" Then
            '    g.DrawString(payOffTop & "¢", f1, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
            '    g.DrawString(payOffBottom & "¢", f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
            'Else
            '    g.DrawString("£" & payOffTop, f1, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
            '    g.DrawString("£" & payOffBottom, f2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
            'End If

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

            g.DrawString(payOffTop, tempF1, Brushes.CornflowerBlue, startPt.X, startPt.Y - 22, fmt)
            g.DrawString(payOffBottom, tempF2, Brushes.Coral, startPt.X, startPt.Y - 2, fmt)
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function isOver(x As Integer, y As Integer) As Boolean

        If x <= pt1.X + 15 And x >= pt1.X - 15 And y <= pt1.Y + 15 And y >= pt1.Y - 15 Then
            Return True
        Else
            Return False
        End If

        Return False
    End Function

    Public Sub nudge(tempX As Integer, tempY As Integer)
        Try
            pt1.X += tempX
            pt2.X += tempX
            pt3.X += tempX
            pt4.X += tempX

            pt1.Y += tempY
            pt2.Y += tempY
            pt3.Y += tempY
            pt4.Y += tempY
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

    Public Sub doNatureDraw()
        Try

            natureDraw = rand(natureDrawRangeEnd, natureDrawRangeStart)

            If natureDraw >= natureRightSuccessStart And natureDraw <= natureRightSuccessEnd Then
                selection = "pay1"
            ElseIf natureDraw >= naturedownSuccessStart And natureDraw <= naturedownSuccessEnd Then
                selection = "pay2"
            ElseIf natureDraw >= natureleftSuccessStart And natureDraw <= natureleftSuccessEnd Then
                selection = "pay3"
            ElseIf natureDraw >= natureRightsubSuccessStart And natureDraw <= natureRightsubSuccessEnd Then
                selection = "sub1"
            ElseIf natureDraw >= natureDownsubSuccessStart And natureDraw <= natureDownsubSuccessEnd Then
                selection = "sub2"
            ElseIf natureDraw >= natureLeftsubSuccessStart And natureDraw <= natureLeftsubSuccessEnd Then
                selection = "sub3"
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class
