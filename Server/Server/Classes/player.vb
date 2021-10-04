Imports System.Drawing.Drawing2D

Public Class player
    Public inumber As Integer            'ID number
    Public sname As String               'name of person
    Public sid As String                 'student id number of person
    Public socketNumber As String        'winsock ID number
    Public relativeNumber As Integer     'either buyer or seller number
    Public earnings As Double            'experimental earnings
    Public ipAddress As String           'IP address of player's machine 
    Public myIPAddress As String         'IP address of player's machine 
    Public computerName As String        'computer anme of player's machine  
    Public roundEarnings As Integer      'earnings for a induvidual round/period
    Public exchangeRate As Integer       'conversion rate from experimental dollars to $
    Public colorName As String           'colorName of player

    Public nodeList(100, 100) As node  'ID/Period
    Public partnerList(100) As Integer
    Public myType(100) As Integer         'period
    'Public currentNode As Integer
    Public finalNode(100) As Integer

    Public sawModalResponse(100, 100) As Boolean  'id/period subjects involved in modal summary
    Public modalResponse(100) As String           'summary of modal shown  

    Public periodEarnings(100) As Double
    Public cumlativeEarnings(100) as Double
    Public instructionLength(100) As Double

    Public lastIDSent As String
    Public lastMessageSent As String

    Public Sub player()

    End Sub

    Public Sub sendBegin()
        Try
            With frmServer

                For i As Integer = 1 To numberOfPlayers
                    For j As Integer = 1 To numberOfPeriods
                        sawModalResponse(i, j) = False
                    Next
                Next

                'If inumber <= numberOfPlayers / 2 Then
                '    myType(currentPeriod) = 1
                'Else
                '    myType(currentPeriod) = 2
                'End If

                For i As Integer = 1 To instructionCount
                    instructionLength(i) = 0
                Next

                'singal to clients to start the experiment

                '.DataGridView1.Rows(inumber - 1).Cells(4).Value = partnerList(1)

                'currentNode = 1

                'winsock can send character strings to the clients
                Dim outstr As String = ""

                'create parseable string to send to clients by putting ";" between each value
                outstr = numberOfPeriods & ";"
                outstr &= numberOfPlayers & ";"
                outstr &= showInstructions & ";"

                outstr &= instructionX & ";"
                outstr &= instructionY & ";"
                outstr &= windowX & ";"
                outstr &= windowY & ";"

                outstr &= payoffMode & ";"
                outstr &= testMode & ";"

                outstr &= iPage8Text & ";"
                outstr &= treeCount & ";"
                outstr &= modalPoolSize & ";"
                outstr &= payOffDistance & ";"

                For i As Integer = 1 To numberOfPeriods
                    outstr &= nodeCountPeriod(i) & ";"
                    outstr &= myType(i) & ";"

                    For j As Integer = 1 To nodeCountPeriod(i)

                        outstr &= nodeList(j, i).id & ";"
                        outstr &= nodeList(j, i).myPeriod & ";"

                        outstr &= nodeList(j, i).status & ";"

                        outstr &= nodeList(j, i).owner & ";"
                        outstr &= nodeList(j, i).payoff11 & ";"
                        outstr &= nodeList(j, i).payoff12 & ";"
                        outstr &= nodeList(j, i).payoff21 & ";"
                        outstr &= nodeList(j, i).payoff22 & ";"
                        outstr &= nodeList(j, i).payoff31 & ";"
                        outstr &= nodeList(j, i).payoff32 & ";"

                        outstr &= nodeList(j, i).pt1.ToString & ";"
                        outstr &= nodeList(j, i).pt2.ToString & ";"
                        outstr &= nodeList(j, i).pt3.ToString & ";"
                        outstr &= nodeList(j, i).pt4.ToString & ";"

                        outstr &= nodeList(j, i).subNode1Id & ";"
                        outstr &= nodeList(j, i).subNode2Id & ";"
                        outstr &= nodeList(j, i).subNode3Id & ";"

                        outstr &= nodeListPeriod(j, i).natureRightText & ";"
                        outstr &= nodeListPeriod(j, i).natureDownText & ";"
                        outstr &= nodeListPeriod(j, i).natureLeftText & ";"

                        outstr &= nodeListPeriod(j, i).natureRightSuccessX & ";"
                        outstr &= nodeListPeriod(j, i).natureRightSuccessY & ";"
                        outstr &= nodeListPeriod(j, i).natureDownSuccessX & ";"
                        outstr &= nodeListPeriod(j, i).natureDownSuccessY & ";"
                        outstr &= nodeListPeriod(j, i).natureLeftSuccessX & ";"
                        outstr &= nodeListPeriod(j, i).natureLeftSuccessY & ";"

                        outstr &= nodeListPeriod(j, i).natureRightSubText & ";"
                        outstr &= nodeListPeriod(j, i).natureDownSubText & ";"
                        outstr &= nodeListPeriod(j, i).natureLeftSubText & ";"

                        outstr &= nodeListPeriod(j, i).natureRightSubSuccessX & ";"
                        outstr &= nodeListPeriod(j, i).natureRightSubSuccessY & ";"
                        outstr &= nodeListPeriod(j, i).natureDownSubSuccessX & ";"
                        outstr &= nodeListPeriod(j, i).natureDownSubSuccessY & ";"
                        outstr &= nodeListPeriod(j, i).natureLeftSubSuccessX & ";"
                        outstr &= nodeListPeriod(j, i).natureLeftSubSuccessY & ";"

                    Next
                Next

                outstr &= nodeListInstructionsCount & ";"

                For i As Integer = 1 To nodeListInstructionsCount
                    outstr &= nodeCountInstructions(i) & ";"

                    For j As Integer = 1 To nodeCountInstructions(i)

                        outstr &= nodeListInstructions(j, i).id & ";"
                        outstr &= nodeListInstructions(j, i).myPeriod & ";"

                        outstr &= nodeListInstructions(j, i).status & ";"

                        outstr &= nodeListInstructions(j, i).owner & ";"
                        outstr &= nodeListInstructions(j, i).payoff11 & ";"
                        outstr &= nodeListInstructions(j, i).payoff12 & ";"
                        outstr &= nodeListInstructions(j, i).payoff21 & ";"
                        outstr &= nodeListInstructions(j, i).payoff22 & ";"
                        outstr &= nodeListInstructions(j, i).payoff31 & ";"
                        outstr &= nodeListInstructions(j, i).payoff32 & ";"

                        outstr &= nodeListInstructions(j, i).pt1.ToString & ";"
                        outstr &= nodeListInstructions(j, i).pt2.ToString & ";"
                        outstr &= nodeListInstructions(j, i).pt3.ToString & ";"
                        outstr &= nodeListInstructions(j, i).pt4.ToString & ";"

                        outstr &= nodeListInstructions(j, i).subNode1Id & ";"
                        outstr &= nodeListInstructions(j, i).subNode2Id & ";"
                        outstr &= nodeListInstructions(j, i).subNode3Id & ";"
                    Next
                Next

                'call the send command (message ID found in takeMessage function,winsock ID,data) 
                sendMessageToClient("02", outstr)
            End With

        Catch ex As Exception
            appEventLog_Write("error player begin:", ex)
        End Try
    End Sub

    Public Sub sendMessageToClient(messageId As String, messageString As String)
        With frmServer
            lastMessageSent = messageString
            lastIDSent = messageId

            .wsk_Col.Send(messageId, socketNumber, messageString)
        End With
    End Sub

    Public Sub resendLastMessage()
        Try
            With frmServer
                sendMessageToClient(lastIDSent, lastMessageSent)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function setupNodes() As Boolean
        Try
            For i As Integer = 1 To numberOfPeriods
                Dim tempTree As Integer = periodList(i).tree

                For j As Integer = 1 To nodeCountPeriod(i)
                    nodeList(j, i) = New node
                    nodeList(j, i).loadFromFile(sfile, j, i, tempTree)
                Next
            Next

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)

            Return False
        End Try
    End Function

    Public Sub resetClient()
        Try
            'kill client
            With frmServer
                '.wsk_Col.Send("01", socketNumber, "")
                sendMessageToClient("01", "")
            End With
        Catch ex As Exception
            appEventLog_Write("error resetClient:", ex)
        End Try
    End Sub

    Public Sub requsetIP(ByVal count As Integer)
        Try
            'request the client send it's IP address
            With frmServer
                '.wsk_Col.Send("05", socketNumber, CStr(count))
                sendMessageToClient("05", CStr(count))
            End With
        Catch ex As Exception
            appEventLog_Write("error requsetIP:", ex)
        End Try
    End Sub

    Public Sub endGame()
        Try
            'tell clients to end the game
            With frmServer
                Dim outstr As String = ""

                ' .wsk_Col.Send("06", socketNumber, outstr)
                sendMessageToClient("06", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error endGame:", ex)
        End Try
    End Sub

    Public Sub takeName(ByVal sname As String, ByRef sid As String)
        Try
            'get the subject's name

            With frmServer
                Me.sname = sname
                Me.sid = sid
                .DataGridView1.Rows(inumber - 1).Cells(1).Value = sname
            End With
        Catch ex As Exception
            appEventLog_Write("error takeName:", ex)
        End Try
    End Sub

    Public Sub endEarly()
        Try
            'end experiment early

            With frmServer
                Dim outstr As String

                outstr = numberOfPeriods & ";"
                '.wsk_Col.Send("12", socketNumber, outstr)

                sendMessageToClient("12", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error endEarly:", ex)
        End Try
    End Sub

    Public Sub finishedInstructions()
        Try
            With frmServer

                ' .wsk_Col.Send("04", socketNumber, "")
                sendMessageToClient("04", "")
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    'Public Sub sendChoice()
    '    Try
    '        With frmServer
    '            Dim outstr As String = ""

    '            If myType(currentPeriod) = 1 Then
    '                outstr = currentNode & ";"
    '                outstr &= returnTreeStatus()

    '                If currentNode = 0 Then
    '                    .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
    '                Else
    '                    If nodeList(currentNode, currentPeriod).owner = myType(currentPeriod) Then
    '                        .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Playing"
    '                    Else
    '                        .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
    '                    End If
    '                End If

    '            Else
    '                outstr = playerList(partnerList(currentPeriod)).currentNode & ";"
    '                outstr &= playerList(partnerList(currentPeriod)).returnTreeStatus()

    '                If playerList(partnerList(currentPeriod)).currentNode = 0 Then
    '                    .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
    '                Else
    '                    If playerList(partnerList(currentPeriod)).nodeList(playerList(partnerList(currentPeriod)).currentNode, currentPeriod).owner = myType(currentPeriod) Then
    '                        .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Playing"
    '                    Else
    '                        .DataGridView1.Rows(inumber - 1).Cells(2).Value = "Waiting"
    '                    End If
    '                End If
    '            End If

    '            '.wsk_Col.Send("07", socketNumber, outstr)
    '            sendMessageToClient("07", outstr)
    '        End With
    '    Catch ex As Exception
    '        appEventLog_Write("error :", ex)
    '    End Try
    'End Sub

    Public Sub sendPeriodResults()
        Try
            With frmServer
                Dim outstr As String = ""

                'calc modal responses

                Dim tempModalPoolSize As Integer = Math.Min(numberOfPlayers / 2, modalPoolSize)

                'randomly pick players of opposite type for modal results
                For i As Integer = 1 To tempModalPoolSize
                    Dim go As Boolean = True

                    Do While go
                        Dim p As Integer = rand(numberOfPlayers, 1)

                        If playerList(p).myType(currentPeriod) <> myType(currentPeriod) And Not sawModalResponse(p, currentPeriod) Then
                            go = False
                            sawModalResponse(p, currentPeriod) = True
                        End If
                    Loop
                Next

                For i As Integer = 1 To nodeCountPeriod(currentPeriod)

                    For j As Integer = 1 To numberOfPlayers
                        If sawModalResponse(j, currentPeriod) Then
                            Dim tempDecisionType As String = ""
                            Dim tempDecisionInfo As String = ""
                            takeChoice2(playerList(j).nodeList(i, currentPeriod).selection, i, tempDecisionType, tempDecisionInfo, nodeList)
                        End If
                    Next
                Next

                calcChoiceAggregates(nodeList)

                'clear choices from my type
                For i As Integer = 1 To nodeCountPeriod(currentPeriod)
                    If nodeList(i, currentPeriod).owner = myType(currentPeriod) Then
                        nodeList(i, currentPeriod).status = nodeList(i, currentPeriod).selection
                    Else
                        If modalResponse(currentPeriod) <> "" Then modalResponse(currentPeriod) &= ", "
                        modalResponse(currentPeriod) &= "Node " & i & ": " & nodeList(i, currentPeriod).status
                    End If
                Next

                outstr &= returnTreeStatus(currentPeriod)

                '.wsk_Col.Send("08", socketNumber, outstr)

                sendMessageToClient("08", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub writeSummaryData(period As Integer)
        Try
            With frmServer
                Dim outstr As String = ""

                outstr &= .tempTime & ","
                outstr &= period & ","
                outstr &= periodList(period).tree & ","
                outstr &= inumber & ","
                outstr &= partnerList(period) & ","

                If myType(period) = 1 Then
                    outstr &= finalNode(period) & ","
                    outstr &= nodeList(finalNode(period), period).status & ","
                Else
                    outstr &= finalNode(period) & ","
                    outstr &= playerList(partnerList(period)).nodeList(finalNode(period), period).status & ","
                End If

                outstr &= periodEarnings(period) & ","
                outstr &= playerList(partnerList(period)).periodEarnings(period) & ","
                outstr &= myType(period) & ","

                If myType(period) = 1 Then
                    If nodeList(finalNode(period), period).owner = myType(period) Then
                        outstr &= "True,"
                    Else
                        outstr &= "False,"
                    End If
                Else
                    If playerList(partnerList(period)).nodeList(finalNode(period), period).owner = myType(period) Then
                        outstr &= "True,"
                    Else
                        outstr &= "False,"
                    End If
                End If

                For i As Integer = 1 To numberOfPlayers
                    outstr &= sawModalResponse(i, period) & ","
                Next

                outstr &= """" & modalResponse(period) & """" & ","

                summaryDf.WriteLine(outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendStartNextPeriod()
        Try
            With frmServer

                '.DataGridView1.Rows(inumber - 1).Cells(4).Value = partnerList(currentPeriod)

                'currentNode = 1

                Dim outstr As String = ""

                outstr &= currentPeriod & ";"
                outstr &= myType(currentPeriod) & ";"

                '.wsk_Col.Send("09", socketNumber, outstr)
                sendMessageToClient("09", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendFinalResults(startBlock As Integer, endBlock As Integer)
        Try
            With frmServer
                Dim outstr As String = ""

                For i As Integer = startBlock To endBlock
                    earnings += periodEarnings(i)
                Next

                If payoffMode = "dollars" Or payoffMode = "pounds" Then
                    .DataGridView1.Rows(inumber - 1).Cells(3).Value = FormatCurrency(earnings)
                Else
                    .DataGridView1.Rows(inumber - 1).Cells(3).Value = FormatCurrency(earnings / 100)
                End If

                outstr &= earnings & ";"

                outstr &= startBlock & ";"
                outstr &= endBlock & ";"

                For i As Integer = startBlock To endBlock
                    If myType(i) = 1 Then
                        outstr &= returnTreeStatus(i)
                    Else
                        outstr &= playerList(partnerList(i)).returnTreeStatus(i)
                    End If

                    outstr &= cumlativeEarnings(i) & ";"
                Next

                sendMessageToClient("10", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub sendTreeTransition()
        Try
            With frmServer
                Dim outstr As String = ""

                sendMessageToClient("11", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub killPath(ByRef tempN As node, period As Integer)
        Try
            tempN.status = "dead"

            If tempN.subNode1Id >= 1 Then
                killPath(nodeList(tempN.subNode1Id, period), period)
            End If

            If tempN.subNode2Id >= 1 Then
                killPath(nodeList(tempN.subNode2Id, period), period)
            End If

            If tempN.subNode3Id >= 1 Then
                killPath(nodeList(tempN.subNode3Id, period), period)
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function returnTreeStatus(index As Integer) As String
        Try
            Dim outstr As String = ""

            For i As Integer = 1 To nodeCountPeriod(index)
                outstr &= nodeList(i, index).status & ";"
            Next

            Return outstr
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function

    Public Sub handleNode(ByRef n As node, period As Integer)
        Try

            If n.owner = 0 Then
                'nature
                n.doNatureDraw()

                Dim tempDecisionType As String = ""
                Dim tempDecisionInfo As String = ""

                Dim tempChoice As String = n.selection
                Dim tempNode As Integer = n.id

                takeChoice2(tempChoice, tempNode, tempDecisionType, tempDecisionInfo, nodeListPeriod)

                'write summary data for nature
                Dim str As String = ""

                str = period & ","
                str &= periodList(period).tree & ","
                str &= "C" & ","

                str &= inumber & ","
                str &= partnerList(period) & ","

                str &= "C" & ","
                str &= tempNode & ","
                str &= tempDecisionType & ","
                str &= tempChoice & ","
                str &= tempDecisionInfo & ","

                str &= ","

                nodeDataDf.WriteLine(str)
            End If

            n.status = n.selection

            If InStr(n.status, "sub") Then
                Dim s As Integer = 1

                If n.status = "sub1" Then
                    s = n.subNode1Id

                    If n.subNode2Id > 0 Then
                        killPath(nodeList(n.subNode2Id, period), period)
                    End If

                    If n.subNode3Id > 0 Then
                        killPath(nodeList(n.subNode3Id, period), period)
                    End If

                    'nodeListPeriod(n.subNode1Id, period).count += 1
                ElseIf n.status = "sub2" Then
                    s = n.subNode2Id

                    If n.subNode1Id > 0 Then
                        killPath(nodeList(n.subNode1Id, period), period)
                    End If

                    If n.subNode3Id > 0 Then
                        killPath(nodeList(n.subNode3Id, period), period)
                    End If

                    'nodeListPeriod(n.subNode2Id, period).count += 1
                ElseIf n.status = "sub3" Then
                    s = n.subNode3Id

                    If n.subNode1Id > 0 Then
                        killPath(nodeList(n.subNode1Id, period), period)
                    End If

                    If n.subNode2Id > 0 Then
                        killPath(nodeList(n.subNode2Id, period), period)
                    End If

                    'nodeListPeriod(n.subNode3Id, period).count += 1
                End If

                handleNode(nodeList(s, period), period)

            Else
                Dim tempPayoff1 As Double
                Dim tempPayoff2 As Double

                If n.status = "pay1" Then

                    tempPayoff1 = n.payoff11
                    tempPayoff2 = n.payoff12

                ElseIf n.status = "pay2" Then

                    tempPayoff1 = n.payoff21
                    tempPayoff2 = n.payoff22
                ElseIf n.status = "pay3" Then

                    tempPayoff1 = n.payoff31
                    tempPayoff2 = n.payoff32
                End If

                Dim tempP2 As Integer = partnerList(period)
                periodEarnings(period) = tempPayoff1
                playerList(tempP2).periodEarnings(period) = tempPayoff2

                If period = 1 Then
                    cumlativeEarnings(period) = tempPayoff1
                    playerList(tempP2).cumlativeEarnings(period) = tempPayoff2
                Else
                    cumlativeEarnings(period) = tempPayoff1 + cumlativeEarnings(period - 1)
                    playerList(tempP2).cumlativeEarnings(period) = tempPayoff2 + playerList(tempP2).cumlativeEarnings(period - 1)
                End If

                finalNode(period) = n.id
                playerList(tempP2).finalNode(period) = n.id

                If n.subNode1Id > 0 Then
                    killPath(nodeList(n.subNode1Id, period), period)
                End If

                If n.subNode2Id > 0 Then
                    killPath(nodeList(n.subNode2Id, period), period)
                End If

                If n.subNode3Id > 0 Then
                    killPath(nodeList(n.subNode3Id, period), period)
                End If
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub copyTreeStatus1to2()
        Try
            If myType(currentPeriod) = 1 Then
                For i As Integer = 1 To nodeCountPeriod(currentPeriod)
                    playerList(partnerList(currentPeriod)).nodeList(i, currentPeriod).status = nodeList(i, currentPeriod).status
                Next
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class
