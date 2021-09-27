Public Class frmSetup

    Private Sub frmSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'load paremeters into text boxes from server.ini
            txtPlayers.Text = getINI(sfile, "gameSettings", "numberOfPlayers")
            chkShowInstructions.Checked = getINI(sfile, "gameSettings", "showInstructions")
            txtPort.Text = getINI(sfile, "gameSettings", "port")
            txtPeriods.Text = getINI(sfile, "gameSettings", "numberOfPeriods")

            txtInstructionX.Text = getINI(sfile, "gameSettings", "instructionX")
            txtInstructionY.Text = getINI(sfile, "gameSettings", "instructionY")
            txtWindowX.Text = getINI(sfile, "gameSettings", "windowX")
            txtWindowY.Text = getINI(sfile, "gameSettings", "windowY")

            txtIpage8.Text = getINI(sfile, "gameSettings", "iPage8Text")
            txtTreeCount.Text = getINI(sfile, "gameSettings", "treeCount")

            txtModalPoolSize.Text = getINI(sfile, "gameSettings", "modalPoolSize")
            txtPayoffDistance.Text = getINI(sfile, "gameSettings", "payOffDistance")

            If getINI(sfile, "gameSettings", "payoffMode") = "dollars" Then
                rbDollars.Checked = True
            ElseIf getINI(sfile, "gameSettings", "payoffMode") = "cents" Then
                rbCents.Checked = True
            Else
                rbPounds.Checked = True
            End If

            chkTestMode.Checked = getINI(sfile, "gameSettings", "testMode")

        Catch ex As Exception
            appEventLog_Write("error frmSetup_Load:", ex)
        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            'write parameter from text boxes into server.ini

            If Not checkValidText(txtIpage8.Text) Then Exit Sub

            writeINI(sfile, "gameSettings", "numberOfPlayers", txtPlayers.Text)
            writeINI(sfile, "gameSettings", "showInstructions", chkShowInstructions.Checked)
            writeINI(sfile, "gameSettings", "port", txtPort.Text)
            writeINI(sfile, "gameSettings", "numberOfPeriods", txtPeriods.Text)

            writeINI(sfile, "gameSettings", "instructionX", txtInstructionX.Text)
            writeINI(sfile, "gameSettings", "instructionY", txtInstructionY.Text)
            writeINI(sfile, "gameSettings", "windowX", txtWindowX.Text)
            writeINI(sfile, "gameSettings", "windowY", txtWindowY.Text)

            writeINI(sfile, "gameSettings", "iPage8Text", txtIpage8.Text)
            writeINI(sfile, "gameSettings", "treeCount", txtTreeCount.Text)

            writeINI(sfile, "gameSettings", "modalPoolSize", txtModalPoolSize.Text)
            writeINI(sfile, "gameSettings", "payOffDistance", txtPayoffDistance.Text)

            If rbDollars.Checked Then
                writeINI(sfile, "gameSettings", "payoffMode", "dollars")
            ElseIf rbCents.Checked Then
                writeINI(sfile, "gameSettings", "payoffMode", "cents")
            Else
                writeINI(sfile, "gameSettings", "payoffMode", "pounds")
            End If

            writeINI(sfile, "gameSettings", "testMode", chkTestMode.Checked)

            loadParameters()

            Me.Close()
        Catch ex As Exception
            appEventLog_Write("error cmdSave_Click:", ex)
        End Try
    End Sub
End Class