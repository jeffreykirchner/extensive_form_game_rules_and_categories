Public Class frmInstructions
    Dim tempS As Boolean
    Dim startPressed As Boolean
    Public numberOfPages As Integer = 11

    Public pageDone(20) As Boolean
    Public results(20, 20) As String        'node number, count

    Dim example1Text As String = "Person 2 chose the arrow pointing to Person 1." & vbCrLf & vbCrLf &
                                 "The result is that:" & vbCrLf & "Person 1 earns #payoff1#." & vbCrLf & "Person 2 earns #payoff2#."
    Dim example2Text As String = "Person 2 chose the payoff.  Person 1 also chose the payoff on the first node; therefore, Person 1 determined the payoffs.  Also, note that we did not use Person 1's choice on their second node, since we reached a payoff earlier in the decision problem." & vbCrLf & vbCrLf &
                                  "The result is that:" & vbCrLf & "Person 1 earns #payoff1#." & vbCrLf & "Person 2 earns #payoff2#."
    Dim example3Text As String = "Person 1 chose the arrow pointing to Person 2.  Person 2 chose the payoff; therefore, Person 2 determined the payoffs.  Also, note that we did not use Person 1's choice on their second node, since we reached a payoff earlier in the decision problem." & vbCrLf & vbCrLf &
                                 "The result is that:" & vbCrLf & "Person 1 earns #payoff1#." & vbCrLf & "Person 2 earns #payoff2#."
    Dim continueText As String = vbCrLf & vbCrLf & "Continue to the next page of instructions."

    Public startTimeOnPage As Date
    Public lastInstruction As Integer
    Public lastTimeOnPage As TimeSpan

    Public Sub nextInstruction()
        Try
            'load the next page of instructions

            RichTextBox1.LoadFile(System.Windows.Forms.Application.StartupPath &
                 "\instructions\page" & currentInstruction & ".rtf")

            variables()

            RichTextBox1.SelectionStart = 1
            RichTextBox1.ScrollToCaret()

            If Not startPressed Then wskClient.Send("01", currentInstruction & ";" & lastInstruction & ";" & lastTimeOnPage.TotalMilliseconds)

            If currentInstruction = 7 Or currentInstruction = 8 Or currentInstruction = 9 Then
                cmdReset.Visible = True
            Else
                cmdReset.Visible = False
            End If
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub

    Public Sub variables()
        Try
            'load variables into instructions

            Dim tempN As Integer = 0
            Dim outstr As String = ""
            Select Case currentInstruction
                Case 1
                    'Use the following command to insert varibles into the instructions.
                    ' RepRTBfield("playerCount-1", numberOfPlayers - 1)

                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 2

                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 3

                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 4

                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 5

                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 6

                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 7

                    myType(1) = 1
                    updateMyType()
                    currentPeriodInstruction = 1

                    If Not pageDone(currentInstruction) Then
                        phase = periodPhase.submit

                        RepRTBfield2("Result", " ")
                        RepRTBfield2("Continue", " ")
                        RepRTBfield2("done", " ")
                        frmMain.cmdSubmit.Visible = True
                        frmMain.pnlMain.Enabled = True

                        phase = periodPhase.submit
                    Else
                        RepRTBfield2("Result", example1Text)
                        RepRTBfield2("Continue", continueText)
                        RepRTBfield2("done", "(done)")

                        If results(3, 1) = "pay2" Then
                            RepRTBfield2("payoff1", returnInstructionPayoff(nodeListInstructions(3, 1).payoff21), Color.CornflowerBlue)
                            RepRTBfield2("payoff2", returnInstructionPayoff(nodeListInstructions(3, 1).payoff22), Color.Coral)
                        Else
                            RepRTBfield2("payoff1", returnInstructionPayoff(nodeListInstructions(3, 1).payoff31), Color.CornflowerBlue)
                            RepRTBfield2("payoff2", returnInstructionPayoff(nodeListInstructions(3, 1).payoff32), Color.Coral)
                        End If

                        phase = periodPhase.finalResults
                    End If
                Case 8

                    myType(1) = 1
                    updateMyType()
                    currentPeriodInstruction = 2

                    If Not pageDone(currentInstruction) Then
                        RepRTBfield2("Result", " ")
                        RepRTBfield2("Continue", " ")
                        RepRTBfield2("done", " ")

                        frmMain.cmdSubmit.Visible = True

                        frmMain.pnlMain.Enabled = True

                        phase = periodPhase.submit
                    Else
                        RepRTBfield2("Result", example2Text)
                        RepRTBfield2("Continue", continueText)
                        RepRTBfield2("done", "(done)")

                        RepRTBfield2("payoff1", returnInstructionPayoff(nodeListInstructions(1, 2).payoff31), Color.CornflowerBlue)
                        RepRTBfield2("payoff2", returnInstructionPayoff(nodeListInstructions(1, 2).payoff32), Color.Coral)

                        phase = periodPhase.finalResults
                    End If
                Case 9

                    myType(1) = 2
                    updateMyType()
                    currentPeriodInstruction = 3

                    If Not pageDone(currentInstruction) Then
                        RepRTBfield2("Result", " ")
                        RepRTBfield2("Continue", " ")
                        RepRTBfield2("done", " ")

                        frmMain.cmdSubmit.Visible = True
                        frmMain.pnlMain.Enabled = True
                        phase = periodPhase.submit
                    Else
                        RepRTBfield2("Result", example3Text)
                        RepRTBfield2("Continue", continueText)
                        RepRTBfield2("done", "(done)")

                        RepRTBfield2("payoff1", returnInstructionPayoff(nodeListInstructions(2, 3).payoff31), Color.CornflowerBlue)
                        RepRTBfield2("payoff2", returnInstructionPayoff(nodeListInstructions(2, 3).payoff32), Color.Coral)

                        phase = periodPhase.finalResults
                    End If

                Case 10
                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                Case 11
                    If Not pageDone(currentInstruction) Then
                        pageDone(currentInstruction) = True
                    End If
                    ' RepRTBfield("iPage8Text", iPage8Text)
            End Select

            RepRTBfield2Color("person 1", Color.CornflowerBlue, False, False)
            RepRTBfield2Color("Person 1", Color.CornflowerBlue, False, False)
            RepRTBfield2Color("person 2", Color.Coral, False, False)
            RepRTBfield2Color("Person 2", Color.Coral, False, False)

            Me.Text = "Instructions " & currentInstruction & "/" & numberOfPages
        Catch ex As Exception
            appEventLog_Write("error variables:", ex)
        End Try
    End Sub

    Public Function RepRTBfield(ByVal sField As String, ByVal sValue As String, c As Color, useHash As Boolean) As Boolean
        Try
            'when the instructions are loaded into the rich text box control this function will
            'replace the variable place holders with variables.

            Dim s As String = ""

            If useHash Then
                s = "#" & sField & "#"
            Else
                s = sField
            End If

            If RichTextBox1.Find(s) = -1 Then
                RichTextBox1.DeselectAll()
                Return False
            End If

            RichTextBox1.SelectionColor = c
            RichTextBox1.SelectedText = sValue

            Return True
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
            Return False
        End Try
    End Function

    Public Sub RepRTBfield2(ByVal sField As String, ByVal sValue As String, Optional c As Color = Nothing, Optional useHash As Boolean = True)
        Try
            If c = Nothing Then c = Color.Black

            Do While (RepRTBfield(sField, sValue, c, useHash))

            Loop
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
        End Try
    End Sub

    Public Function RepRTBfieldColor(ByVal sField As String, ByVal c As Color, start As Integer, underLine As Boolean, bold As Boolean) As Integer
        Try
            'when the instructions are loaded into the rich text box control this function will
            'color the specified text the specified color

            If RichTextBox1.Find(sField, start, RichTextBoxFinds.None) = -1 Then
                RichTextBox1.DeselectAll()
                Return 0
            End If

            RichTextBox1.SelectionColor = c

            If underLine Or bold Then

                Dim fs As FontStyle

                If underLine Then fs += FontStyle.Underline
                If bold Then fs += FontStyle.Bold

                Dim f As New Font(RichTextBox1.SelectionFont, fs)
                RichTextBox1.SelectionFont = f
            End If

            Return RichTextBox1.SelectionStart
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
            Return False
        End Try
    End Function

    Public Sub RepRTBfield2Color(ByVal sField As String, ByVal c As Color, underLine As Boolean, bold As Boolean)
        Try

            Dim start As Integer = (RepRTBfieldColor(sField, c, 1, underLine, bold))

            Dim go As Boolean

            If start = 0 Then
                go = False
            Else
                go = True
                start += 1
            End If

            Do While go
                start = (RepRTBfieldColor(sField, c, start, underLine, bold))

                If start = 0 Then
                    go = False
                Else
                    start += 1
                End If
            Loop
        Catch ex As Exception
            appEventLog_Write("error RepRTBfield:", ex)
        End Try
    End Sub
    Private Sub frmInstructions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            For i As Integer = 1 To 20
                pageDone(i) = False
            Next

            startPressed = False
            currentInstruction = 1
            nextInstruction()
            tempS = False

        Catch ex As Exception
            appEventLog_Write("error frmInstructions_Load:", ex)
        End Try
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        Try
            startAction()
        Catch ex As Exception
            appEventLog_Write("error instructinos start:", ex)
        End Try
    End Sub

    Public Sub startAction()
        Try
            'client done with instructions
            Dim outstr As String = ""

            Dim ts As TimeSpan = Now - startTimeOnPage

            outstr = ts.TotalMilliseconds

            wskClient.Send("02", outstr)
            cmdStart.Visible = False
            startPressed = True
        Catch ex As Exception
            appEventLog_Write("error instructinos start:", ex)
        End Try
    End Sub

    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        Try
            cmdNextAction()
        Catch ex As Exception
            appEventLog_Write("error cmdNext_Click:", ex)
        End Try
    End Sub

    Public Sub cmdNextAction()
        Try
            'load next page of instructions


            If pageDone(currentInstruction) = False Then
                MessageBox.Show("Please take the required action before continuing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If currentInstruction = numberOfPages Then Exit Sub

            lastInstruction = currentInstruction
            lastTimeOnPage = Now - startTimeOnPage
            startTimeOnPage = Now

            currentInstruction += 1

            cmdBack.Visible = True

            If currentInstruction = numberOfPages Then cmdNext.Visible = False

            If currentInstruction = numberOfPages And Not tempS Then
                cmdStart.Visible = True
                tempS = True
            End If

            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error cmdNext_Click:", ex)
        End Try
    End Sub

    Private Sub cmdBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBack.Click
        Try
            'previous page of instructions

            cmdNext.Visible = True

            If currentInstruction = 1 Then
                Exit Sub
            End If

            lastInstruction = currentInstruction
            lastTimeOnPage = Now - startTimeOnPage
            startTimeOnPage = Now

            currentInstruction -= 1

            If currentInstruction = 1 Then cmdBack.Visible = False

            nextInstruction()
        Catch ex As Exception
            appEventLog_Write("error cmdBack_Click :", ex)
        End Try
    End Sub

    Private Sub cmdReset_Click(sender As System.Object, e As System.EventArgs) Handles cmdReset.Click
        Try

            Dim tempP As Integer = 0
            If currentInstruction = instructionExamplePage1 Then
                tempP = 1
            ElseIf currentInstruction = instructionExamplePage2 Then
                tempP = 2
            ElseIf currentInstruction = instructionExamplePage3 Then
                tempP = 3
            End If

            If tempP = 0 Then Exit Sub

            pageDone(currentInstruction) = False
            'selection = ""

            For i As Integer = 1 To 20
                results(currentInstruction, i) = ""
            Next

            For i As Integer = 1 To 10
                If nodeListInstructions(i, tempP) IsNot Nothing Then
                    nodeListInstructions(i, tempP).status = "open"
                    nodeListInstructions(i, tempP).selection = Nothing
                End If
            Next


            'currentNode = 1

            RichTextBox1.LoadFile(System.Windows.Forms.Application.StartupPath &
                 "\instructions\page" & currentInstruction & ".rtf")

            variables()

            RichTextBox1.SelectionStart = 1
            RichTextBox1.ScrollToCaret()

            frmMain.pnlMain.Enabled = True
        Catch ex As Exception
            appEventLog_Write("error :", ex)
        End Try
    End Sub
End Class