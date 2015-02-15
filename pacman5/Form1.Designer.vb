<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pacman5
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
        Me.components = New System.ComponentModel.Container()
        Me.pacmanmove = New System.Windows.Forms.Timer(Me.components)
        Me.invalidation = New System.Windows.Forms.Timer(Me.components)
        Me.pacmanstagesadjust = New System.Windows.Forms.Timer(Me.components)
        Me.blinkymove = New System.Windows.Forms.Timer(Me.components)
        Me.pinkymove = New System.Windows.Forms.Timer(Me.components)
        Me.inkymove = New System.Windows.Forms.Timer(Me.components)
        Me.suemove = New System.Windows.Forms.Timer(Me.components)
        Me.runtimer = New System.Windows.Forms.Timer(Me.components)
        Me.ending = New System.Windows.Forms.Timer(Me.components)
        Me.scatter = New System.Windows.Forms.Timer(Me.components)
        Me.starting = New System.Windows.Forms.Timer(Me.components)
        Me.begin_label = New System.Windows.Forms.Label()
        Me.score_label = New System.Windows.Forms.Label()
        Me.fruit_timer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'pacmanmove
        '
        Me.pacmanmove.Enabled = True
        Me.pacmanmove.Interval = 20
        '
        'invalidation
        '
        Me.invalidation.Interval = 1
        '
        'pacmanstagesadjust
        '
        Me.pacmanstagesadjust.Enabled = True
        Me.pacmanstagesadjust.Interval = 2
        '
        'blinkymove
        '
        Me.blinkymove.Interval = 32
        '
        'pinkymove
        '
        Me.pinkymove.Interval = 35
        '
        'inkymove
        '
        Me.inkymove.Interval = 35
        '
        'suemove
        '
        Me.suemove.Interval = 35
        '
        'runtimer
        '
        Me.runtimer.Interval = 333
        '
        'ending
        '
        '
        'scatter
        '
        Me.scatter.Enabled = True
        Me.scatter.Interval = 1000
        '
        'starting
        '
        Me.starting.Interval = 35
        '
        'begin_label
        '
        Me.begin_label.BackColor = System.Drawing.Color.Transparent
        Me.begin_label.Font = New System.Drawing.Font("Impact", 15.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.begin_label.ForeColor = System.Drawing.Color.Yellow
        Me.begin_label.Location = New System.Drawing.Point(255, 395)
        Me.begin_label.Name = "begin_label"
        Me.begin_label.Size = New System.Drawing.Size(90, 30)
        Me.begin_label.TabIndex = 0
        Me.begin_label.Text = "Ready?"
        Me.begin_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'score_label
        '
        Me.score_label.BackColor = System.Drawing.Color.Transparent
        Me.score_label.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.score_label.ForeColor = System.Drawing.Color.White
        Me.score_label.Location = New System.Drawing.Point(64, 9)
        Me.score_label.Name = "score_label"
        Me.score_label.Size = New System.Drawing.Size(460, 19)
        Me.score_label.TabIndex = 1
        Me.score_label.Text = "Use the arrow keys to move.  The game will start when you begin to move."
        Me.score_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'fruit_timer
        '
        Me.fruit_timer.Interval = 9333
        '
        'pacman5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(594, 724)
        Me.Controls.Add(Me.score_label)
        Me.Controls.Add(Me.begin_label)
        Me.DoubleBuffered = True
        Me.Name = "pacman5"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pacman!"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pacmanmove As System.Windows.Forms.Timer
    Friend WithEvents invalidation As System.Windows.Forms.Timer
    Friend WithEvents pacmanstagesadjust As System.Windows.Forms.Timer
    Friend WithEvents blinkymove As System.Windows.Forms.Timer
    Friend WithEvents pinkymove As System.Windows.Forms.Timer
    Friend WithEvents inkymove As System.Windows.Forms.Timer
    Friend WithEvents suemove As System.Windows.Forms.Timer
    Friend WithEvents runtimer As System.Windows.Forms.Timer
    Friend WithEvents ending As System.Windows.Forms.Timer
    Friend WithEvents scatter As System.Windows.Forms.Timer
    Friend WithEvents starting As System.Windows.Forms.Timer
    Friend WithEvents begin_label As System.Windows.Forms.Label
    Friend WithEvents score_label As System.Windows.Forms.Label
    Friend WithEvents fruit_timer As System.Windows.Forms.Timer

End Class
