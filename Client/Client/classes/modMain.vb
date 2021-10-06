'Programmed by Jeffrey Kirchner and Your Name Here
'kirchner@chapman.edu/jkirchner@gmail.com
'Economic Science Institute, Chapman University 2008-2010 ©

Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Module modMain
    Public sfile As String

    Public inumber As Integer                  'client ID number
    Public numberOfPlayers As Integer          'number of total players in experiment
    Public currentPeriod As Integer            'current period of experiment
    Public myIPAddress As String               'IP address of client 
    Public myPortNumber As String              'port number of client      
    Public exchangeRate As Integer             'client's exchange rate
    Public currentInstruction As Integer       'current instruction
    Public numberOfPeriods As Integer          'number of periods  
    Public showInstructions As Boolean         'wether to show instructions to subject

    Public WithEvents wskClient As Winsock
    Public closing As Boolean = False

    Public instructionX As Integer            'start up locations of windows
    Public instructionY As Integer
    Public windowX As Integer
    Public windowY As Integer

    Public nodeList(100, 100) As node  'ID/Period
    Public nodeCount(100) As Integer
    'Public currentNode As Integer
    Public myType(100) As Integer
    Public cumlativeEarnings(100) As Double      'period

    Public payoffMode As String
    Public decisionStart As Date
    Public testMode As String

    Public nodeListInstructions(10, 3) As node      'count, page number
    Public nodeCountInstructions(3) As Integer
    Public instructionExamplePage1 As Integer = 7
    Public instructionExamplePage2 As Integer = 8
    Public instructionExamplePage3 As Integer = 9

    Public nodeListInstructionsCount As Integer = 1

    Public currentPeriodInstruction As Integer

    Public iPage8Text As String
    Public treeCount As Integer
    Public modalPoolSize As Integer
    Public payOffDistance As Integer

    Public phase As periodPhase = periodPhase.submit

    Public finalResultsStartBlock As Integer
    Public finalResultsEndBlock As Integer



    Enum periodPhase
        submit
        waitAfterSubmit
        summaryResults
        waitAfterSummaryResults
        treeTransition
        waitAfterTreeTransition
        finalResults
        waitAfterFinalResults
    End Enum

#Region " Winsock Code "
    Private Sub wskClient_DataArrival(ByVal sender As Object, ByVal e As WinsockDataArrivalEventArgs) Handles wskClient.DataArrival
        Try
            Dim buf As String = Nothing
            CType(sender, Winsock).Get(buf)

            Dim msgtokens() As String = buf.Split("#")
            Dim i As Integer

            'appEventLog_Write("data arrival: " & buf)

            For i = 1 To msgtokens.Length - 1
                takeMessage(msgtokens(i - 1))
            Next

        Catch ex As Exception
            appEventLog_Write("error wskClient_DataArrival:", ex)
        End Try
    End Sub

    Private Sub wskClient_ErrorReceived(ByVal sender As System.Object, ByVal e As WinsockErrorEventArgs) Handles wskClient.ErrorReceived
        ' Log("Error: " & e.Message)
    End Sub

    Private Sub wskClient_StateChanged(ByVal sender As Object, ByVal e As WinsockStateChangingEventArgs) Handles wskClient.StateChanged
        Try
            'appEventLog_Write("state changed")

            If e.New_State = WinsockStates.Closed Then
                frmConnect.Show()
            End If
        Catch ex As Exception
            appEventLog_Write("error wskClient_StateChanged:", ex)
        End Try

    End Sub

    Public Sub connect()
        Try

            wskClient = New Winsock
            wskClient.BufferSize = 8192
            wskClient.LegacySupport = False
            wskClient.LocalPort = 8080
            wskClient.MaxPendingConnections = 1
            wskClient.Protocol = WinsockProtocols.Tcp
            wskClient.RemotePort = myPortNumber
            wskClient.RemoteServer = myIPAddress
            wskClient.SynchronizingObject = frmMain

            wskClient.Connect()
        Catch
            frmMain.Hide()
            frmConnect.Show()
        End Try
    End Sub

#End Region

#Region " General Functions "
    Public Sub main()
        AppEventLog_Init()
        appEventLog_Write("Begin")

        ToggleScreenSaverActive(False)

        Application.EnableVisualStyles()
        Application.Run(frmMain)

        ToggleScreenSaverActive(True)

        appEventLog_Write("End")
        AppEventLog_Close()
    End Sub

    Public Function timeConversion(ByVal sec As Integer) As String
        Try
            'appEventLog_Write("time conversion :" & sec)
            timeConversion = Format((sec \ 60), "00") & ":" & Format((sec Mod 60), "00")
        Catch ex As Exception
            appEventLog_Write("error timeConversion:", ex)
            timeConversion = ""
        End Try
    End Function

    Public Sub setID(ByVal sinstr As String)
        Try
            'appEventLog_Write("set id :" & sinstr)

            Dim msgtokens() As String

            msgtokens = sinstr.Split(";")

            inumber = msgtokens(0)

            appEventLog_Write("Client# = " & inumber)

        Catch ex As Exception
            appEventLog_Write("error setID:", ex)
        End Try
    End Sub


    Public Sub sendIPAddress(ByVal sinstr As String)
        Try
            'appEventLog_Write("send ip :" & sinstr)

            With frmMain
                Dim outstr As String

                inumber = sinstr

                appEventLog_Write("Client# = " & inumber)

                outstr = SystemInformation.ComputerName & ";"
                wskClient.Send("03", outstr)
            End With
        Catch ex As Exception
            appEventLog_Write("error sendIPAddress:", ex)
        End Try
    End Sub

    Public Function numberSuffix(ByVal sinstr As Integer) As String
        numberSuffix = sinstr
        Try
            Select Case sinstr
                Case 1
                    numberSuffix = sinstr & "st"
                Case 2
                    numberSuffix = sinstr & "nd"
                Case 3
                    numberSuffix = sinstr & "rd"
                Case Is >= 4
                    numberSuffix = sinstr & "th"
            End Select
        Catch ex As Exception
            appEventLog_Write("error numberSuffix:", ex)
        End Try
    End Function
#End Region

    Private Sub takeMessage(ByVal sinstr As String)
        Try
            'take message from server
            'msgtokens(0) has type of message sent, having different types of messages allows you to send different formats for different actions.
            'msgtokens(1) has the semicolon delimited data that is to be parsed and acted upon.  


            Dim msgtokens() As String
            msgtokens = sinstr.Split("|")

            Select Case msgtokens(0) 'case statement to handle each of the different types of messages
                Case "01"
                    'close client
                    closing = True

                    wskClient.Close()
                    frmMain.Close()

                Case "02"
                    takeBegin(msgtokens(1))
                Case "03"
                    setID(msgtokens(1))
                Case "04"
                    takeFinishedInstructions()
                Case "05"
                    sendIPAddress(msgtokens(1))
                Case "06"
                    takeEndGame(msgtokens(1))
                Case "07"
                    takeChoice(msgtokens(1))
                Case "08"
                    takePeriodResults(msgtokens(1))
                Case "09"
                    takeStartNextPeriod(msgtokens(1))
                Case "10"
                    takeFinalResults(msgtokens(1))
                Case "11"
                    takeTreeTransition(msgtokens(1))
                Case "12"
                    endEarly(msgtokens(1))
                Case "13"

                Case "14"

                Case "15"

            End Select
        Catch ex As Exception
            appEventLog_Write("error takeMessage:", ex)
        End Try

    End Sub


    Public Sub takeBegin(ByVal sinstr As String)
        With frmMain
            Try
                'server has signaled client to start experiment

                'parse incoming data string
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                numberOfPeriods = msgtokens(nextToken)
                nextToken += 1

                numberOfPlayers = msgtokens(nextToken)
                nextToken += 1

                showInstructions = msgtokens(nextToken)
                nextToken += 1

                instructionX = msgtokens(nextToken)
                nextToken += 1

                instructionY = msgtokens(nextToken)
                nextToken += 1

                windowX = msgtokens(nextToken)
                nextToken += 1

                windowY = msgtokens(nextToken)
                nextToken += 1

                payoffMode = msgtokens(nextToken)
                nextToken += 1

                testMode = msgtokens(nextToken)
                nextToken += 1

                iPage8Text = msgtokens(nextToken)
                nextToken += 1

                treeCount = msgtokens(nextToken)
                nextToken += 1

                modalPoolSize = msgtokens(nextToken)
                nextToken += 1

                payOffDistance = msgtokens(nextToken)
                nextToken += 1

                'experiment nodes
                For i As Integer = 1 To numberOfPeriods
                    nodeCount(i) = msgtokens(nextToken)
                    nextToken += 1

                    myType(i) = msgtokens(nextToken)
                    nextToken += 1

                    For j As Integer = 1 To nodeCount(i)
                        nodeList(j, i) = New node(nodeList)

                        nodeList(j, i).id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).myPeriod = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).status = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).owner = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff11 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff12 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff21 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff22 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff31 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).payoff32 = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).pt1 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).pt2 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).pt3 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).pt4 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeList(j, i).subNode1Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).subNode2Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).subNode3Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureRightText = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureDownText = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureLeftText = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureRightSuccessX = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureRightSuccessY = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureDownSuccessX = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureDownSuccessY = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureLeftSuccessX = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureLeftSuccessY = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureRightSubText = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureDownSubText = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureLeftSubText = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureRightSubSuccessX = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureRightSubSuccessY = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureDownSubSuccessX = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureDownSubSuccessY = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureLeftSubSuccessX = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).natureLeftSubSuccessY = msgtokens(nextToken)
                        nextToken += 1

                        nodeList(j, i).myColor = getMyColor(nodeList(j, i).owner)

                    Next
                Next

                nodeListInstructionsCount = msgtokens(nextToken)
                nextToken += 1

                'instruction nodes
                For i As Integer = 1 To nodeListInstructionsCount
                    nodeCountInstructions(i) = msgtokens(nextToken)
                    nextToken += 1

                    For j As Integer = 1 To nodeCountInstructions(i)
                        nodeListInstructions(j, i) = New node(nodeListInstructions)

                        nodeListInstructions(j, i).id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).myPeriod = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).status = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).owner = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff11 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff12 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff21 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff22 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff31 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).payoff32 = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).pt1 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).pt2 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).pt3 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).pt4 = pointFromString(msgtokens(nextToken))
                        nextToken += 1

                        nodeListInstructions(j, i).subNode1Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).subNode2Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).subNode3Id = msgtokens(nextToken)
                        nextToken += 1

                        nodeListInstructions(j, i).myColor = getMyColor(nodeListInstructions(j, i).owner)
                    Next
                Next

                phase = periodPhase.submit

                .Timer1.Enabled = True

                currentPeriod = 1

                If showInstructions Then

                    myType(1) = 1

                    frmInstructions.Show()
                    frmInstructions.Location = New System.Drawing.Point(instructionX, instructionY)
                    frmInstructions.startTimeOnPage = Now
                    frmInstructions.lastInstruction = 1

                    .Location = New System.Drawing.Point(windowX, windowY)
                    .cmdSubmit.Visible = False
                    currentPeriodInstruction = 1
                    .pnlMain.Enabled = False
                End If

                .Timer2.Enabled = True
                clearNodeSelections()

                decisionStart = Now

                .Text = "Client " & inumber

                If payoffMode = "dollars" Then
                    .lblEarnings.Text = "Cumulative" & vbCrLf & "Earnings($)"
                ElseIf payoffMode = "cents" Then
                    .lblEarnings.Text = "Cumulative" & vbCrLf & "Earnings(¢)"
                Else
                    .lblEarnings.Text = "Cumulative" & vbCrLf & "Earnings(£)"
                End If

                updateMyType()

                If showInstructions = False Then updateTxtMessages()

                If testMode Then
                    frmTestMode.Show()
                    .Timer3.Enabled = True

                    If showInstructions Then
                        frmTestMode.Location = New Point(frmInstructions.Location.X, frmInstructions.Location.Y + frmInstructions.Height + 10)
                    End If
                End If

            Catch ex As Exception
                appEventLog_Write("error begin:", ex)
            End Try

        End With
    End Sub

    Public Sub updateMyType()
        Try
            With frmMain
                If myType(currentPeriod) = 1 Then
                    .lblPerson.Text = "Person 1"
                    .lblPerson.ForeColor = Color.CornflowerBlue

                    .lblPayoff1.Text = "Your Payoff"
                    .lblPayoff2.Text = "Person 2's Payoff"
                Else
                    .lblPerson.Text = "Person 2"
                    .lblPerson.ForeColor = Color.Coral

                    .lblPayoff1.Text = "Person 1's Payoff"
                    .lblPayoff2.Text = "Your Payoff"
                End If
            End With
        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub

    Public Sub clearNodeSelections()
        Try
            For i As Integer = 1 To nodeCount(currentPeriod)
                nodeList(i, currentPeriod).selection = ""
            Next

        Catch ex As Exception
            appEventLog_Write("error begin:", ex)
        End Try
    End Sub


    Public Sub updateTxtMessages()
        Try
            With frmMain
                ' If nodeList(currentNode, currentPeriod).owner = myType Then
                .txtMessages.Text = "Click on your choice(s) then press Submit."
                .cmdSubmit.Visible = True
                .cmdSubmit.Text = "Submit"
                'Else
                '    If myType = 1 Then
                '        .txtMessages.Text = "Waiting for Person 2."
                '        .txtMessages.Find("Person 2")
                '        .txtMessages.SelectionColor = Color.Coral
                '    Else
                '        .txtMessages.Text = "Waiting for Person 1."
                '        .txtMessages.Find("Person 1")
                '        .txtMessages.SelectionColor = Color.CornflowerBlue
                '    End If

                '    .cmdSubmit.Visible = False
                'End If
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeStartNextPeriod(ByVal sinstr As String)
        Try
            With frmMain
                frmInstructions.Close()
                showInstructions = False

                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                currentPeriod = msgtokens(nextToken)
                nextToken += 1

                myType(currentPeriod) = msgtokens(nextToken)
                nextToken += 1

                clearNodeSelections()
                updateTxtMessages()
                updateMyType()

                decisionStart = Now

                phase = periodPhase.submit
                .gbResults.Visible = False
                .pnlMain.Enabled = True
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeEndGame(ByVal sinstr As String)
        Try
            'end the experiment
            With frmMain
                .txtMessages.Text = ""
                frmNames.Show()

            End With
        Catch ex As Exception
            appEventLog_Write("error endGame:", ex)
        End Try
    End Sub

    Public Sub endEarly(ByVal sinstr As String)
        Try
            'end experiment early
            Dim msgtokens() As String
            msgtokens = sinstr.Split(";")

            numberOfPeriods = msgtokens(0)
        Catch ex As Exception
            appEventLog_Write("error endEarly:", ex)
        End Try
    End Sub

    Public Sub periodResults(ByVal sinstr As String)
        Try
            'show the results at end of period
            With frmMain

                Dim msgtokens() As String = Split(sinstr, ";")
                Dim nextToken As Integer = 0

            End With
        Catch ex As Exception
            appEventLog_Write("error periodResults:", ex)
        End Try
    End Sub

    Public Sub takeFinishedInstructions()
        Try
            With frmMain
                'close the instructions and start experiment           

                frmInstructions.Close()
                showInstructions = False

                currentPeriod = 1

                decisionStart = Now

                .pnlMain.Enabled = True

                updateTxtMessages()

                If testMode Then .Timer3.Enabled = True
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

    Public Sub takeChoice(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                'currentNode = msgtokens(nextToken)
                'nextToken += 1

                'For i As Integer = 1 To nodeCount(currentPeriod)
                '    nodeList(i, currentPeriod).status = msgtokens(nextToken)
                '    nextToken += 1
                'Next

                'If currentNode = 0 Then
                '    .txtMessages.Text = "Waiting for Others."
                'Else
                '    updateTxtMessages()

                '    decisionStart = Now
                'End If


            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takePeriodResults(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0


                'currentNode = 0
                'nextToken += 1
                phase = periodPhase.summaryResults

                For i As Integer = 1 To nodeCount(currentPeriod)
                    nodeList(i, currentPeriod).status = msgtokens(nextToken)
                    nextToken += 1

                    nodeList(i, currentPeriod).tickTock = 0
                    nodeList(i, currentPeriod).noPathHere = True
                Next

                'calc valid path
                Dim currentNode As Integer = 1
                For i As Integer = 1 To nodeCount(currentPeriod)
                    Dim n As node = nodeList(currentNode, currentPeriod)
                    n.noPathHere = False

                    If n.status = "sub1" Then
                        currentNode = n.subNode1Id
                    ElseIf n.status = "sub2" Then
                        currentNode = n.subNode2Id
                    ElseIf n.status = "sub3" Then
                        currentNode = n.subNode3Id
                    Else
                        Exit For
                    End If
                Next

                .txtMessages.Text = "Press the ""Ready to Go On to Next Period"" Button."
                .cmdSubmit.Text = "Ready to Go On to Next Period"
                .cmdSubmit.Visible = True

                .drawScreen()
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeTreeTransition(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                phase = periodPhase.treeTransition

                .txtMessages.Text = "Press the ""Ready to Go On"" Button."
                .cmdSubmit.Text = "Ready to Go On to Next Period"
                .cmdSubmit.Visible = True

                .drawScreen()
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub takeFinalResults(ByVal sinstr As String)
        Try
            With frmMain
                Dim msgtokens() As String = sinstr.Split(";")
                Dim nextToken As Integer = 0

                Dim tempEarnings As Double = msgtokens(nextToken)
                '.txtProfit.Text = Format(tempEarnings, "0.00")
                nextToken += 1

                finalResultsStartBlock = msgtokens(nextToken)
                nextToken += 1

                finalResultsEndBlock = msgtokens(nextToken)
                nextToken += 1

                For i As Integer = finalResultsStartBlock To finalResultsEndBlock
                    For j As Integer = 1 To nodeCount(i)
                        nodeList(j, i).status = msgtokens(nextToken)
                        nextToken += 1
                    Next

                    cumlativeEarnings(i) = msgtokens(nextToken)
                    nextToken += 1
                Next

                currentPeriod = finalResultsStartBlock

                phase = periodPhase.finalResults

                updateFinalResults()
                .drawScreen()

                .gbEarnings.Visible = True
            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub doTestMode()
        Try
            With frmMain
                If showInstructions Then

                Else
                    If .cmdSubmit.Visible Then
                        If .cmdSubmit.Text = "Submit" Then

                            Dim go As Boolean = True
                            Dim currentNode As Integer = 0

                            For i As Integer = 1 To nodeCount(currentPeriod)
                                If nodeList(i, currentPeriod).owner = myType(currentPeriod) And nodeList(i, currentPeriod).selection = "" Then
                                    currentNode = i
                                    go = False
                                    Exit For
                                End If
                            Next

                            If go Then
                                .cmdSubmitAction()
                            Else
                                Select Case rand(6, 1)
                                    Case 1
                                        If nodeList(currentNode, currentPeriod).payoff11 >= 0 Then
                                            .pnlMainClickAction(nodeList(currentNode, currentPeriod).pt3.X, nodeList(currentNode, currentPeriod).pt3.Y)
                                        End If
                                    Case 2
                                        If nodeList(currentNode, currentPeriod).payoff21 >= 0 Then
                                            .pnlMainClickAction(nodeList(currentNode, currentPeriod).pt2.X, nodeList(currentNode, currentPeriod).pt2.Y)
                                        End If
                                    Case 3
                                        If nodeList(currentNode, currentPeriod).payoff31 >= 0 Then
                                            .pnlMainClickAction(nodeList(currentNode, currentPeriod).pt4.X, nodeList(currentNode, currentPeriod).pt4.Y)
                                        End If
                                    Case 4
                                        If nodeList(currentNode, currentPeriod).subNode1Id > 0 Then
                                            .pnlMainClickAction(nodeList(nodeList(currentNode, currentPeriod).subNode1Id, currentPeriod).pt1.X,
                                                                nodeList(nodeList(currentNode, currentPeriod).subNode1Id, currentPeriod).pt1.Y)
                                        End If
                                    Case 5
                                        If nodeList(currentNode, currentPeriod).subNode2Id > 0 Then
                                            .pnlMainClickAction(nodeList(nodeList(currentNode, currentPeriod).subNode2Id, currentPeriod).pt1.X,
                                                                nodeList(nodeList(currentNode, currentPeriod).subNode2Id, currentPeriod).pt1.Y)
                                        End If
                                    Case 6
                                        If nodeList(currentNode, currentPeriod).subNode3Id > 0 Then
                                            .pnlMainClickAction(nodeList(nodeList(currentNode, currentPeriod).subNode3Id, currentPeriod).pt1.X,
                                                                nodeList(nodeList(currentNode, currentPeriod).subNode3Id, currentPeriod).pt1.Y)
                                        End If
                                End Select
                            End If
                        Else
                            .cmdSubmitAction()
                        End If
                    ElseIf .cmdForward.Visible Then
                        .cmdForward.PerformClick()
                    ElseIf frmNames.Visible Then

                        Dim tempN As Integer = rand(20, 5)

                        frmNames.txtName1.Text = ""

                        For i As Integer = 1 To tempN
                            frmNames.txtName1.Text &= Chr(rand(122, 60))
                        Next

                        frmNames.txtName2.Text = rand(99999999, 1)

                        frmNames.cmdSubmitAction(False)
                    End If
                End If

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub doTestModeInstructions()
        Try
            With frmMain

                If frmInstructions.pageDone(currentInstruction) Then frmInstructions.cmdNextAction()

                Select Case currentInstruction
                    Case 1, 2, 3, 4, 5, 6, 10


                    Case instructionExamplePage1

                        If nodeListInstructions(1, 1).selection = Nothing Then

                            .pnlMainClickAction(nodeListInstructions(2, 1).pt1.X, nodeListInstructions(2, 1).pt1.Y)

                            If rand(2, 1) = 1 Then
                                .pnlMainClickAction(nodeListInstructions(3, 1).pt2.X, nodeListInstructions(3, 1).pt2.Y)
                            Else
                                .pnlMainClickAction(nodeListInstructions(3, 1).pt4.X, nodeListInstructions(3, 1).pt4.Y)
                            End If
                        ElseIf frmMain.cmdSubmit.Visible Then
                            frmMain.cmdSubmit.PerformClick()
                        End If

                    Case instructionExamplePage2

                        If nodeListInstructions(1, 2).selection = Nothing Then

                            .pnlMainClickAction(nodeListInstructions(1, 2).pt4.X, nodeListInstructions(1, 2).pt4.Y)

                            If rand(2, 1) = 1 Then
                                .pnlMainClickAction(nodeListInstructions(3, 2).pt2.X, nodeListInstructions(3, 2).pt2.Y)
                            Else
                                .pnlMainClickAction(nodeListInstructions(3, 2).pt4.X, nodeListInstructions(3, 2).pt4.Y)
                            End If
                        ElseIf frmMain.cmdSubmit.Visible Then
                            frmMain.cmdSubmit.PerformClick()
                        End If
                    Case instructionExamplePage3

                        If nodeListInstructions(2, 3).selection = Nothing Then

                            .pnlMainClickAction(nodeListInstructions(2, 3).pt4.X, nodeListInstructions(2, 3).pt4.Y)
                        ElseIf frmMain.cmdSubmit.Visible Then

                            frmMain.cmdSubmit.PerformClick()
                        End If
                    Case 11
                        '.Timer3.Enabled = False
                        If frmInstructions.cmdStart.Visible Then frmInstructions.cmdStart.PerformClick()

                End Select

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function getMyColor(id As Integer) As Color
        Try
            If id = 1 Then
                Return Color.CornflowerBlue
            ElseIf id = 2 Then
                Return Color.Coral
            Else
                Return Color.White
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Function

    Public Function returnInstructionPayoff(value As String) As String
        Try
            Select Case value
                Case "1"
                    Return "A"
                Case "2"
                    Return "B"
                Case "3"
                    Return "C"
                Case "4"
                    Return "D"
                Case "5"
                    Return "E"
                Case "6"
                    Return "F"
                Case "7"
                    Return "G"
                Case "8"
                    Return "H"
            End Select

            Return ""
        Catch ex As Exception
            appEventLog_Write("error :", ex)

            Return ""
        End Try
    End Function

    Public Sub updateFinalResults()
        Try
            With frmMain
                .gbResults.Visible = True

                .lblPeriod.Text = "Period " & currentPeriod & " Results"

                If currentPeriod = finalResultsEndBlock Then

                    If phase <> periodPhase.waitAfterFinalResults Then
                        .txtMessages.Text = "Press the ""Ready to Go On"" Button."
                        .cmdSubmit.Text = "Ready to Go On"
                        .cmdSubmit.Visible = True
                    End If
                    .cmdForward.Visible = False
                    .cmdBack.Visible = True
                ElseIf currentPeriod = finalResultsStartBlock Then
                    .cmdForward.Visible = True
                    .cmdBack.Visible = False
                Else
                    .cmdForward.Visible = True
                    .cmdBack.Visible = True
                End If

                'only one period of results
                If finalResultsStartBlock = finalResultsEndBlock Then
                    .cmdForward.Visible = False
                    .cmdBack.Visible = False
                End If

                .txtProfit.Text = cumlativeEarnings(currentPeriod)
                updateMyType()

            End With
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Function GetDistance(ByVal startPoint As PointF, ByVal endPoint As PointF) As Integer
        Try

            Return Math.Sqrt((Math.Abs(endPoint.X - startPoint.X) ^ 2) +
                             (Math.Abs(endPoint.Y - startPoint.Y) ^ 2))

        Catch ex As Exception
            appEventLog_Write("error :", ex)
            Return 0
        End Try
    End Function
End Module
