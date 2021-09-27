<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetup))
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPeriods = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkShowInstructions = New System.Windows.Forms.CheckBox()
        Me.txtPlayers = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtWindowX = New System.Windows.Forms.TextBox()
        Me.txtWindowY = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtInstructionX = New System.Windows.Forms.TextBox()
        Me.txtInstructionY = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.rbCents = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbDollars = New System.Windows.Forms.RadioButton()
        Me.chkTestMode = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtIpage8 = New System.Windows.Forms.TextBox()
        Me.rbPounds = New System.Windows.Forms.RadioButton()
        Me.txtTreeCount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtModalPoolSize = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPayoffDistance = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtPort
        '
        Me.txtPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort.Location = New System.Drawing.Point(307, 76)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(162, 26)
        Me.txtPort.TabIndex = 50
        Me.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(7, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(204, 20)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "Port # (Requires restart)"
        '
        'txtPeriods
        '
        Me.txtPeriods.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPeriods.Location = New System.Drawing.Point(307, 44)
        Me.txtPeriods.Name = "txtPeriods"
        Me.txtPeriods.Size = New System.Drawing.Size(162, 26)
        Me.txtPeriods.TabIndex = 48
        Me.txtPeriods.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(157, 20)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Number of Periods"
        '
        'chkShowInstructions
        '
        Me.chkShowInstructions.AutoSize = True
        Me.chkShowInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowInstructions.Location = New System.Drawing.Point(298, 420)
        Me.chkShowInstructions.Name = "chkShowInstructions"
        Me.chkShowInstructions.Size = New System.Drawing.Size(172, 24)
        Me.chkShowInstructions.TabIndex = 46
        Me.chkShowInstructions.Text = "Show Instructions"
        Me.chkShowInstructions.UseVisualStyleBackColor = True
        '
        'txtPlayers
        '
        Me.txtPlayers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayers.Location = New System.Drawing.Point(308, 12)
        Me.txtPlayers.Name = "txtPlayers"
        Me.txtPlayers.Size = New System.Drawing.Size(162, 26)
        Me.txtPlayers.TabIndex = 45
        Me.txtPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(294, 20)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Number of Players (Even #, 30 Max)"
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(12, 467)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(457, 27)
        Me.cmdSave.TabIndex = 43
        Me.cmdSave.Text = "Save and Close"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(405, 143)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(21, 20)
        Me.Label23.TabIndex = 101
        Me.Label23.Text = "Y"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(332, 143)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(21, 20)
        Me.Label24.TabIndex = 100
        Me.Label24.Text = "X"
        '
        'txtWindowX
        '
        Me.txtWindowX.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWindowX.Location = New System.Drawing.Point(356, 140)
        Me.txtWindowX.Name = "txtWindowX"
        Me.txtWindowX.Size = New System.Drawing.Size(39, 26)
        Me.txtWindowX.TabIndex = 99
        Me.txtWindowX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWindowY
        '
        Me.txtWindowY.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWindowY.Location = New System.Drawing.Point(429, 140)
        Me.txtWindowY.Name = "txtWindowY"
        Me.txtWindowY.Size = New System.Drawing.Size(39, 26)
        Me.txtWindowY.TabIndex = 98
        Me.txtWindowY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(7, 143)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(228, 20)
        Me.Label25.TabIndex = 97
        Me.Label25.Text = "Main Window Start Position"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(405, 111)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(21, 20)
        Me.Label22.TabIndex = 96
        Me.Label22.Text = "Y"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(332, 111)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(21, 20)
        Me.Label21.TabIndex = 95
        Me.Label21.Text = "X"
        '
        'txtInstructionX
        '
        Me.txtInstructionX.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructionX.Location = New System.Drawing.Point(356, 108)
        Me.txtInstructionX.Name = "txtInstructionX"
        Me.txtInstructionX.Size = New System.Drawing.Size(39, 26)
        Me.txtInstructionX.TabIndex = 94
        Me.txtInstructionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtInstructionY
        '
        Me.txtInstructionY.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstructionY.Location = New System.Drawing.Point(429, 108)
        Me.txtInstructionY.Name = "txtInstructionY"
        Me.txtInstructionY.Size = New System.Drawing.Size(39, 26)
        Me.txtInstructionY.TabIndex = 93
        Me.txtInstructionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(7, 111)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(209, 20)
        Me.Label20.TabIndex = 92
        Me.Label20.Text = "Instruction Start Position"
        '
        'rbCents
        '
        Me.rbCents.AutoSize = True
        Me.rbCents.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbCents.Location = New System.Drawing.Point(396, 297)
        Me.rbCents.Name = "rbCents"
        Me.rbCents.Size = New System.Drawing.Size(74, 24)
        Me.rbCents.TabIndex = 103
        Me.rbCents.TabStop = True
        Me.rbCents.Text = "Cents"
        Me.rbCents.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 297)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(167, 20)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "Unit Denominations"
        '
        'rbDollars
        '
        Me.rbDollars.AutoSize = True
        Me.rbDollars.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbDollars.Location = New System.Drawing.Point(308, 297)
        Me.rbDollars.Name = "rbDollars"
        Me.rbDollars.Size = New System.Drawing.Size(83, 24)
        Me.rbDollars.TabIndex = 105
        Me.rbDollars.TabStop = True
        Me.rbDollars.Text = "Dollars"
        Me.rbDollars.UseVisualStyleBackColor = True
        '
        'chkTestMode
        '
        Me.chkTestMode.AutoSize = True
        Me.chkTestMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTestMode.Location = New System.Drawing.Point(76, 420)
        Me.chkTestMode.Name = "chkTestMode"
        Me.chkTestMode.Size = New System.Drawing.Size(112, 24)
        Me.chkTestMode.TabIndex = 106
        Me.chkTestMode.Text = "Test Mode"
        Me.chkTestMode.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 338)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(457, 23)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "Instruction Text on Round Length and Pairings (Page 8)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIpage8
        '
        Me.txtIpage8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIpage8.Location = New System.Drawing.Point(12, 364)
        Me.txtIpage8.Multiline = True
        Me.txtIpage8.Name = "txtIpage8"
        Me.txtIpage8.Size = New System.Drawing.Size(457, 47)
        Me.txtIpage8.TabIndex = 108
        Me.txtIpage8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'rbPounds
        '
        Me.rbPounds.AutoSize = True
        Me.rbPounds.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbPounds.Location = New System.Drawing.Point(219, 297)
        Me.rbPounds.Name = "rbPounds"
        Me.rbPounds.Size = New System.Drawing.Size(87, 24)
        Me.rbPounds.TabIndex = 111
        Me.rbPounds.TabStop = True
        Me.rbPounds.Text = "Pounds"
        Me.rbPounds.UseVisualStyleBackColor = True
        '
        'txtTreeCount
        '
        Me.txtTreeCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTreeCount.Location = New System.Drawing.Point(306, 172)
        Me.txtTreeCount.Name = "txtTreeCount"
        Me.txtTreeCount.Size = New System.Drawing.Size(162, 26)
        Me.txtTreeCount.TabIndex = 115
        Me.txtTreeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 20)
        Me.Label6.TabIndex = 114
        Me.Label6.Text = "Tree Count"
        '
        'txtModalPoolSize
        '
        Me.txtModalPoolSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModalPoolSize.Location = New System.Drawing.Point(306, 204)
        Me.txtModalPoolSize.Name = "txtModalPoolSize"
        Me.txtModalPoolSize.Size = New System.Drawing.Size(162, 26)
        Me.txtModalPoolSize.TabIndex = 117
        Me.txtModalPoolSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 207)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(207, 20)
        Me.Label2.TabIndex = 116
        Me.Label2.Text = "Modal pool size (players)"
        '
        'txtPayoffDistance
        '
        Me.txtPayoffDistance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayoffDistance.Location = New System.Drawing.Point(306, 236)
        Me.txtPayoffDistance.Name = "txtPayoffDistance"
        Me.txtPayoffDistance.Size = New System.Drawing.Size(162, 26)
        Me.txtPayoffDistance.TabIndex = 119
        Me.txtPayoffDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 239)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(198, 20)
        Me.Label5.TabIndex = 118
        Me.Label5.Text = "Payoff Distance (pixels)"
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 524)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtPayoffDistance)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtModalPoolSize)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTreeCount)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.rbPounds)
        Me.Controls.Add(Me.txtIpage8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkTestMode)
        Me.Controls.Add(Me.rbDollars)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.rbCents)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtWindowX)
        Me.Controls.Add(Me.txtWindowY)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtInstructionX)
        Me.Controls.Add(Me.txtInstructionY)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPeriods)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chkShowInstructions)
        Me.Controls.Add(Me.txtPlayers)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSetup"
        Me.Text = "Setup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPeriods As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkShowInstructions As System.Windows.Forms.CheckBox
    Friend WithEvents txtPlayers As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtWindowX As System.Windows.Forms.TextBox
    Friend WithEvents txtWindowY As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtInstructionX As System.Windows.Forms.TextBox
    Friend WithEvents txtInstructionY As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents rbCents As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rbDollars As System.Windows.Forms.RadioButton
    Friend WithEvents chkTestMode As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtIpage8 As System.Windows.Forms.TextBox
    Friend WithEvents rbPounds As System.Windows.Forms.RadioButton
    Friend WithEvents txtTreeCount As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtModalPoolSize As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPayoffDistance As TextBox
    Friend WithEvents Label5 As Label
End Class
