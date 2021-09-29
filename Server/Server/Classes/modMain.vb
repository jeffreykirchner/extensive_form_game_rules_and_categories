'Programmed by Jeffrey Kirchner and Your Name Here
'kirchner@chapman.edu/jkirchner@gmail.com
'Economic Science Institute, Chapman University 2008-2010 ©

Imports System.IO

Module modMain
#Region " General Variables "
    Public playerList(100) As player                  'array of players
    Public playerCount As Integer                    'number of players connected
    Public numberOfPlayers As Integer                'number of desired players
    Public sfile As String                           'location of intialization file  
    Public checkin As Integer                        'global counter 
    Public connectionCount As Integer                'total number of connections made since server start 
    Public portNumber As Integer                     'port number sockect traffic is operation on 
    Public nodeDataDf As StreamWriter                'data file
    Public summaryDf As StreamWriter                 'data file
    Public replayDf As StreamWriter                  'data file
    Public recruiterDf As StreamWriter               'recruiter earnings upload file  
    Public frmServer As New frmMain                  'main form 
    Public filename As String                        'location of data file
    Public filename2 As String                       'location of data file
    Public showInstructions As Boolean               'show client instructions  
    Public currentInstruction As Integer             'current page of instructions 
#End Region

    Public phase As periodPhase = periodPhase.submit

    Enum periodPhase
        submit                'subjects submit their choices
        summaryResults        'subjects view summary of all choices made
        treeTransition        'the tree has changed 
        finalResults          'subjects view induvidiual results for current tree block
        endGame               'submit subject names
    End Enum


    'global variables here
    Public numberOfPeriods As Integer     'number of periods
    Public currentPeriod As Integer       'current period 

    Public instructionX As Integer            'start up locations of windows
    Public instructionY As Integer
    Public windowX As Integer
    Public windowY As Integer

    Public payoffMode As String
    Public testMode As String
    Public periodStart As Date

    Public nodeListInstructions(3, 3) As node
    Public nodeCountInstructions(3) As Integer
    Public nodeListInstructionsCount As Integer = 3

    Public iPage8Text As String

    Public instructionCount As Integer = 8

    Public regimeList(100) As String

    Public nodeListPeriod(100, 100) As node  'ID/Period
    Public nodeCountPeriod(100) As Integer

    Public nodeListBase(100, 100) As node  'ID/Tree
    Public nodeCountBase(100) As Integer

    Public periodList(100) As period
    Public treeCount As Integer
    Public modalPoolSize As Integer
    Public payOffDistance As Integer

#Region " General Functions "
    Public Sub main(ByVal args() As String)
        connectionCount = 0

        AppEventLog_Init()
        appEventLog_Write("Load")

        ToggleScreenSaverActive(False)

        Application.EnableVisualStyles()
        Application.Run(frmServer)

        ToggleScreenSaverActive(True)

        appEventLog_Write("Exit")
        AppEventLog_Close()
    End Sub

    Public Sub takeIP(ByVal sinstr As String, ByVal index As Integer)
        Try
            Dim msgtokens() As String = sinstr.Split(";")
            Dim nextToken As Integer = 0

            playerList(index).computerName = msgtokens(nextToken)
            nextToken += 1
        Catch ex As Exception
            appEventLog_Write("error takeIP:", ex)
        End Try
    End Sub

    Public Function roundUp(ByVal value As Double) As Integer
        Try
            Dim msgtokens() As String

            If InStr(CStr(value), ".") Then
                msgtokens = CStr(value).Split(".")

                roundUp = msgtokens(0)
                roundUp += 1
            Else
                roundUp = value
            End If
        Catch ex As Exception
            Return CInt(value)
            appEventLog_Write("error roundUp:", ex)
        End Try
    End Function

    Public Function getMyColor(ByVal index As Integer) As Color
        Try
            'appEventLog_Write("get color")

            Select Case index
                Case 1
                    getMyColor = Color.Blue
                Case 2
                    getMyColor = Color.Red
                Case 3
                    getMyColor = Color.Teal
                Case 4
                    getMyColor = Color.Green
                Case 5
                    getMyColor = Color.Purple
                Case 6
                    getMyColor = Color.Orange
                Case 7
                    getMyColor = Color.Brown
                Case 8
                    getMyColor = Color.Gray
            End Select
        Catch ex As Exception
            appEventLog_Write("error getMyColor:", ex)
        End Try
    End Function

    Public Function colorToId(ByVal str As String) As Integer
        Try
            Dim i As Integer

            'appEventLog_Write("color to id :" & str)

            For i = 1 To numberOfPlayers
                If str = playerList(i).colorName Then
                    colorToId = i
                    Exit Function
                End If
            Next

            colorToId = -1
        Catch ex As Exception
            Return 0
            appEventLog_Write("error colorToId:", ex)
        End Try
    End Function
#End Region

    Public Sub takeMessage(ByVal sinstr As String)
        'when a message is received from a client it is parsed here
        'msgtokens(1) has type of message sent, having different types of messages allows you to send different formats for different actions.
        'msgtokens(2) has the semicolon delimited data that is to be parsed and acted upon.  
        'index has the client ID that sent the data.  Client ID is assigned by connection order, indexed from 1.

        Try
            With frmServer
                Dim msgtokens() As String

                msgtokens = sinstr.Split("|")

                Dim index As Integer
                index = msgtokens(0)

                Application.DoEvents()

                Select Case msgtokens(1) 'case statement to handle each of the different types of messages
                    Case "01"
                        updateInstructionDisplay(msgtokens(2), index)
                    Case "02"
                        takeFinishedInstructions(msgtokens(2), index)
                    Case "03"
                        takeIP(msgtokens(2), index)
                    Case "04"
                        takeChoice(msgtokens(2), index)
                    Case "05"
                        takeFinishedReviewingResults(msgtokens(2), index)
                    Case "06"

                    Case "07"
                        takeNames(msgtokens(2), index)
                    Case "08"

                    Case "09"

                    Case "10"

                    Case "11"

                    Case "12"

                    Case "13"


                End Select

                Application.DoEvents()

            End With
            'all subs/functions should have an error trap
        Catch ex As Exception
            appEventLog_Write("error takeMessage: " & sinstr & " : ", ex)
        End Try

    End Sub

    Public Sub takeFinishedInstructions(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                checkin += 1
                .DataGridView1.Rows(index - 1).Cells(2).Value = "Waiting"

                playerList(index).instructionLength(instructionCount) += msgtokens(0)

                If checkin = numberOfPlayers Then
                    showInstructions = False
                    checkin = 0

                    MessageBox.Show("Begin Game.", "Start", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    periodStart = Now
                    currentPeriod = 1

                    For i As Integer = 1 To numberOfPlayers
                        playerList(i).sendStartNextPeriod()

                        .DataGridView1.Rows(i - 1).Cells(2).Value = "Playing"
                        .DataGridView1.Rows(i - 1).Cells(4).Value = playerList(i).partnerList(currentPeriod)
                    Next i

                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Sub takeNames(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim outstr As String = ""
                Dim msgtokens() As String = sinstr.Split(";")

                playerList(index).takeName(msgtokens(0), msgtokens(1))
                checkin += 1

                If checkin = numberOfPlayers Then

                    summaryDf.WriteLine("")

                    summaryDf.WriteLine("Earnings")
                    outstr = "Name,Earnings,"
                    summaryDf.WriteLine(outstr)
                    For i As Integer = 1 To numberOfPlayers

                        outstr = .DataGridView1.Rows(i - 1).Cells(1).Value & ","
                        outstr &= .DataGridView1.Rows(i - 1).Cells(3).Value & ","
                        summaryDf.WriteLine(outstr)

                        'recruiter data file
                        outstr = playerList(i).sid & ","
                        outstr &= .DataGridView1.Rows(i - 1).Cells(3).Value

                        recruiterDf.WriteLine(outstr)
                    Next

                    summaryDf.WriteLine("")
                    summaryDf.WriteLine("Instruction Length")
                    outstr = "Player,"
                    For i As Integer = 1 To instructionCount
                        outstr &= "Page " & i & ","
                    Next

                    summaryDf.WriteLine(outstr)

                    For i As Integer = 1 To numberOfPlayers
                        outstr = i & ",N\A,"

                        For j As Integer = 2 To instructionCount
                            outstr &= Math.Round(playerList(i).instructionLength(j), 1) & ","
                        Next

                        summaryDf.WriteLine(outstr)
                    Next

                    summaryDf.Close()
                    nodeDataDf.Close()
                    replayDf.Close()
                    recruiterDf.Close()
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error: ", ex)
        End Try
    End Sub

    Public Sub loadParameters()
        Try
            'load parameters from server.ini

            numberOfPlayers = getINI(sfile, "gameSettings", "numberOfPlayers")
            numberOfPeriods = getINI(sfile, "gameSettings", "numberOfPeriods")
            showInstructions = getINI(sfile, "gameSettings", "showInstructions")
            portNumber = getINI(sfile, "gameSettings", "port")

            instructionX = getINI(sfile, "gameSettings", "instructionX")
            instructionY = getINI(sfile, "gameSettings", "instructionY")
            windowX = getINI(sfile, "gameSettings", "windowX")
            windowY = getINI(sfile, "gameSettings", "windowY")

            payoffMode = getINI(sfile, "gameSettings", "payoffMode")
            testMode = getINI(sfile, "gameSettings", "testMode")

            iPage8Text = getINI(sfile, "gameSettings", "iPage8Text")
            treeCount = getINI(sfile, "gameSettings", "treeCount")
            modalPoolSize = getINI(sfile, "gameSettings", "modalPoolSize")
            payOffDistance = getINI(sfile, "gameSettings", "payOffDistance")
        Catch ex As Exception
            appEventLog_Write("error loadParameters:", ex)
        End Try
    End Sub

    'Public Sub writeSummaryData(ByVal sinstr As String, ByVal index As Integer)
    '    Try
    '        'write data to output file
    '        Dim outstr As String = ""

    '        nodeDataDf.WriteLine(outstr)
    '    Catch ex As Exception
    '        appEventLog_Write("error write summary data:", ex)
    '    End Try
    'End Sub

    Public Sub updateInstructionDisplay(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                Dim tempPage As Integer = msgtokens(nextToken)
                nextToken += 1

                Dim tempLastPage As Integer = msgtokens(nextToken)
                nextToken += 1

                Dim tempTime As Double = msgtokens(nextToken)
                nextToken += 1

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Page " & tempPage

                playerList(index).instructionLength(tempLastPage) += tempTime
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function pointFromString(str As String) As Point
        Try

            Dim msgtokens() As String = str.Split({"{", "X", "=", ",", "Y", "}"}, StringSplitOptions.RemoveEmptyEntries)

            Return New Point(msgtokens(0), msgtokens(1))
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return New Point(0, 0)
        End Try
    End Function

    Public Sub takeChoice(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Waiting"

                Dim tempP1 As Integer
                Dim tempP2 As Integer
                If playerList(index).myType(currentPeriod) = 1 Then
                    tempP1 = index
                    tempP2 = playerList(index).partnerList(currentPeriod)
                Else
                    tempP1 = playerList(index).partnerList(currentPeriod)
                    tempP2 = index
                End If

                Dim count As Integer = msgtokens(nextToken)
                nextToken += 1

                For i As Integer = 1 To count
                    Dim tempDecisionType As String = ""
                    Dim tempDecisionInfo As String = ""

                    Dim tempNode As Integer = msgtokens(nextToken)
                    nextToken += 1

                    Dim tempChoice As String = msgtokens(nextToken)
                    nextToken += 1

                    playerList(tempP1).nodeList(tempNode, currentPeriod).selection = tempChoice
                    playerList(tempP2).nodeList(tempNode, currentPeriod).selection = tempChoice

                    takeChoice2(tempChoice, tempNode, tempDecisionType, tempDecisionInfo, nodeListPeriod)

                    '"Period,Tree,Player,Partner1,Partner2,PlayerType,DecisionNode,DecisionType,DecisionDirection,DecisionInfo,PeriodTime,"

                    Dim str As String = ""

                    str &= .tempTime & ","
                    str &= currentPeriod & ","
                    str &= periodList(currentPeriod).tree & ","
                    str &= index & ","
                    If playerList(index).myType(currentPeriod) = 1 Then
                        str &= ","
                        str &= playerList(index).partnerList(currentPeriod) & ","
                    Else
                        str &= playerList(index).partnerList(currentPeriod) & ","
                        str &= ","
                    End If

                    str &= playerList(index).myType(currentPeriod) & ","
                    str &= tempNode & ","
                    str &= tempDecisionType & ","
                    str &= tempChoice & ","
                    str &= tempDecisionInfo & ","

                    Dim ts As TimeSpan
                    ts = Now - periodStart
                    str &= ts.TotalMilliseconds & ","

                    nodeDataDf.WriteLine(str)
                Next

                'send results
                checkin += 1
                If checkin = numberOfPlayers Then
                    phase = periodPhase.summaryResults
                    calcChoiceAggregates(nodeListPeriod)

                    checkin = 0

                    For i As Integer = 1 To numberOfPlayers
                        playerList(i).sendPeriodResults()

                        .DataGridView1.Rows(i - 1).Cells(2).Value = "Reviewing Period Results"
                    Next
                End If

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeChoice2(tempChoice As String, tempNode As Integer, ByRef tempDecisionType As String, ByRef tempDecisionInfo As String, nodeList(,) As node)
        Try
            If tempChoice = "pay1" Then

                nodeList(tempNode, currentPeriod).countRight += 1

                tempDecisionType = "Payoff"
                tempDecisionInfo = nodeList(tempNode, currentPeriod).payoff11 & " | " & nodeList(tempNode, currentPeriod).payoff12
            ElseIf tempChoice = "pay2" Then

                nodeList(tempNode, currentPeriod).countDown += 1

                tempDecisionType = "Payoff"
                tempDecisionInfo = nodeList(tempNode, currentPeriod).payoff21 & " | " & nodeList(tempNode, currentPeriod).payoff22
            ElseIf tempChoice = "pay3" Then

                nodeList(tempNode, currentPeriod).countLeft += 1

                tempDecisionType = "Payoff"
                tempDecisionInfo = nodeList(tempNode, currentPeriod).payoff31 & " | " & nodeList(tempNode, currentPeriod).payoff32
            ElseIf tempChoice = "sub1" Then

                nodeList(tempNode, currentPeriod).countSubRight += 1
                nodeList(nodeList(tempNode, currentPeriod).subNode1Id, currentPeriod).count += 1

                tempDecisionType = "Node"
                tempDecisionInfo = nodeList(tempNode, currentPeriod).subNode1Id
            ElseIf tempChoice = "sub2" Then

                nodeList(tempNode, currentPeriod).countSubDown += 1
                nodeList(nodeList(tempNode, currentPeriod).subNode2Id, currentPeriod).count += 1

                tempDecisionType = "Node"
                tempDecisionInfo = nodeList(tempNode, currentPeriod).subNode2Id
            ElseIf tempChoice = "sub3" Then

                nodeList(tempNode, currentPeriod).countSubLeft += 1
                nodeList(nodeList(tempNode, currentPeriod).subNode3Id, currentPeriod).count += 1

                tempDecisionType = "Node"
                tempDecisionInfo = nodeList(tempNode, currentPeriod).subNode3Id
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeFinishedReviewingResults(ByVal sinstr As String, ByVal index As Integer)
        Try
            With frmServer
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                checkin += 1

                .DataGridView1.Rows(index - 1).Cells(2).Value = "Waiting"

                If checkin = numberOfPlayers Then
                    checkin = 0

                    If (phase <> periodPhase.finalResults And currentPeriod = numberOfPeriods) Then
                        phase = periodPhase.finalResults
                        'show results from block

                        handleNodes(1, numberOfPeriods)

                        For i As Integer = 1 To numberOfPlayers
                            .DataGridView1.Rows(i - 1).Cells(2).Value = "Reviewing Final Results"
                            playerList(i).sendFinalResults(1, numberOfPeriods)
                        Next

                        'Write Data
                        For i As Integer = 1 To numberOfPeriods
                            For j As Integer = 1 To numberOfPlayers
                                playerList(j).writeSummaryData(i)
                            Next
                        Next
                    ElseIf currentPeriod = numberOfPeriods Then
                        'end game
                        phase = periodPhase.endGame

                        For i As Integer = 1 To numberOfPlayers
                            playerList(i).endGame()
                        Next
                    ElseIf phase <> periodPhase.treeTransition And periodList(currentPeriod + 1).tree <> periodList(currentPeriod).tree Then

                        'notify subjects that the tree is about to change
                        phase = periodPhase.treeTransition

                        For i As Integer = 1 To numberOfPlayers
                            .DataGridView1.Rows(i - 1).Cells(2).Value = "Tree Change"
                            playerList(i).sendTreeTransition()
                        Next
                    Else

                        phase = periodPhase.submit
                        'advance to next period
                        currentPeriod += 1

                        'nodeListPeriod(1, currentPeriod).count = numberOfPlayers / 2

                        .txtPeriod.Text = currentPeriod
                        periodStart = Now

                        For i As Integer = 1 To numberOfPlayers
                            playerList(i).sendStartNextPeriod()

                            .DataGridView1.Rows(i - 1).Cells(2).Value = "Playing"
                            .DataGridView1.Rows(i - 1).Cells(4).Value = playerList(i).partnerList(currentPeriod)
                        Next


                    End If
                End If

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub handleNodes(startBlock As Integer, endBlock As Integer)
        Try
            For i As Integer = startBlock To endBlock 'period
                For j As Integer = 1 To numberOfPlayers
                    If playerList(j).myType(i) = 1 Then

                        playerList(j).handleNode(playerList(j).nodeList(1, i), i)
                        playerList(j).copyTreeStatus1to2()
                    End If
                Next
            Next
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function setupInstructionNodes() As Boolean
        Try
            nodeCountInstructions(1) = 3
            nodeCountInstructions(2) = 3
            nodeCountInstructions(3) = 3

            For i As Integer = 1 To nodeListInstructionsCount
                For j As Integer = 1 To nodeCountInstructions(i)
                    nodeListInstructions(j, i) = New node

                    nodeListInstructions(j, i).id = j
                    nodeListInstructions(j, i).myPeriod = i

                    nodeListInstructions(j, i).status = "open"

                    nodeListInstructions(j, i).owner = getINI(sfile, "nodeI" & i & "-" & j, "owner")

                    nodeListInstructions(j, i).payoff11 = getINI(sfile, "nodeI" & i & "-" & j, "payoff11")
                    nodeListInstructions(j, i).payoff12 = getINI(sfile, "nodeI" & i & "-" & j, "payoff12")
                    nodeListInstructions(j, i).payoff21 = getINI(sfile, "nodeI" & i & "-" & j, "payoff21")
                    nodeListInstructions(j, i).payoff22 = getINI(sfile, "nodeI" & i & "-" & j, "payoff22")
                    nodeListInstructions(j, i).payoff31 = getINI(sfile, "nodeI" & i & "-" & j, "payoff31")
                    nodeListInstructions(j, i).payoff32 = getINI(sfile, "nodeI" & i & "-" & j, "payoff32")

                    nodeListInstructions(j, i).pt1 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt1"))
                    nodeListInstructions(j, i).pt2 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt2"))
                    nodeListInstructions(j, i).pt3 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt3"))
                    nodeListInstructions(j, i).pt4 = pointFromString(getINI(sfile, "nodeI" & i & "-" & j, "pt4"))

                    nodeListInstructions(j, i).subNode1Id = getINI(sfile, "nodeI" & i & "-" & j, "subNode1Id")
                    nodeListInstructions(j, i).subNode2Id = getINI(sfile, "nodeI" & i & "-" & j, "subNode2Id")
                    nodeListInstructions(j, i).subNode3Id = getINI(sfile, "nodeI" & i & "-" & j, "subNode3Id")
                Next
            Next

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)

            Return False
        End Try
    End Function

    Public Function checkValidText(ByVal sinstr As String) As Boolean
        Try
            If InStr(sinstr, "|") > 0 Then
                MsgBox("Please do not use the ""|"" character.", MsgBoxStyle.Critical)
                sinstr = ""
                Return False
            End If

            If InStr(sinstr, "#") > 0 Then
                MsgBox("Please do not use the ""#"" character.", MsgBoxStyle.Critical)
                sinstr = ""
                Return False
            End If

            If InStr(sinstr, ";") > 0 Then
                MsgBox("Please do not use the "";"" character.", MsgBoxStyle.Critical)
                sinstr = ""
                Return False
            End If

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function

    Public Sub calcChoiceAggregates(nodeList(,) As node)
        Try
            'calc totals
            For i As Integer = 1 To nodeCountPeriod(currentPeriod)

                If nodeList(i, currentPeriod).owner = 0 Then
                    nodeList(i, currentPeriod).status = "open"
                Else
                    'change status to max
                    Dim tempS(6) As String        'list of highest paths 
                    Dim tempSCount As Integer = 1 'number tided at current value
                    Dim tempV As Integer = nodeList(i, currentPeriod).countRight        'value of current best 
                    tempS(1) = "pay1"

                    calcChoiceAggregates2(nodeList(i, currentPeriod).countDown, tempV, tempS, tempSCount, "pay2")
                    calcChoiceAggregates2(nodeList(i, currentPeriod).countLeft, tempV, tempS, tempSCount, "pay3")
                    calcChoiceAggregates2(nodeList(i, currentPeriod).countSubRight, tempV, tempS, tempSCount, "sub1")
                    calcChoiceAggregates2(nodeList(i, currentPeriod).countSubDown, tempV, tempS, tempSCount, "sub2")
                    calcChoiceAggregates2(nodeList(i, currentPeriod).countSubLeft, tempV, tempS, tempSCount, "sub3")

                    nodeList(i, currentPeriod).status = tempS(rand(tempSCount, 1))
                End If
            Next

        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub calcChoiceAggregates2(ByRef c As Integer, ByRef tempV As Integer, ByRef tempS() As String, ByRef tempSCount As Integer, tempPathName As String)
        Try

            If c > tempV Then
                tempSCount = 1
                tempS(tempSCount) = tempPathName
                tempV = c
            ElseIf c = tempV Then
                tempSCount += 1
                tempS(tempSCount) = tempPathName
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function returnTreeStatus() As String
        Try
            Dim outstr As String = ""

            For i As Integer = 1 To nodeCountPeriod(currentPeriod)
                outstr &= nodeListPeriod(i, currentPeriod).status & ";"
            Next

            Return outstr
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return ""
        End Try
    End Function
    Public Function validateInt(ByVal sinstr As String, ByVal maxLength As Integer,
                                ByVal allowDecimal As Boolean, ByVal allowNegative As Boolean) As Boolean
        Try
            If Not IsNumeric(sinstr) Then Return False

            If Not allowDecimal Then
                If InStr(sinstr, ".") Then
                    Return False
                End If
            End If

            Dim msgtokens() As String = sinstr.Split(".")
            If Len(msgtokens(0)) > maxLength Then
                Return False
            End If

            If Not allowNegative Then
                If CDbl(sinstr) < 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return False
        End Try
    End Function

    Public Function GetDistance(ByVal startPoint As PointF, ByVal endPoint As PointF) As Double
        Try


            Return Math.Round(Math.Sqrt((Math.Abs(endPoint.X - startPoint.X) ^ 2) +
                             (Math.Abs(endPoint.Y - startPoint.Y) ^ 2)), 2)

        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return 0
        End Try
    End Function

End Module
