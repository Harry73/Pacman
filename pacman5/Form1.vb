Public Class pacman5
    'pacman
    Dim pacman As Rectangle 'the square to draw pacman in
    Dim y As Integer = 26 'pacman's y
    Dim x As Integer = 14 'pacman's x
    Dim boxnum As Integer = 1 'boxnum calculated using x and y

    Dim pacstage As Integer 'keeps track of what stage of mouth opening/closing pacman's in
    Dim updown As Boolean = False 'is pacman's mouth opening or closing


    Dim dot(246) As Rectangle 'all those dots
    Dim powerdot(4) As Rectangle 'the 4 powadots
    Dim xcounter As Integer = 0 'counters record when pacman reaches edge of a box and update the x/y a needed
    Dim ycounter As Integer = 0
    Dim direction As String = "" 'holds the direction pacman's going - used to move him
    Dim inputdirection As String = "" 'records the direction user presses
    Dim currentdirection As String = "" 'generally equals "direction" but can be changed if you hit a wall
    Dim mypen As System.Drawing.Pen = Pens.Blue 'needed so the map can flash orangy if you win


    'ghosts
    Dim blinky As Rectangle 'squares to draw ghosts in
    Dim pinky As Rectangle
    Dim inky As Rectangle
    Dim sue As Rectangle

    'I really should've made a structure for this since each ghost has the same variables
    'blinky
    Dim blinkyboxnum As Integer 'like pacman's box num
    Dim blinkydirection As String = "right" 'like pacman's direction
    Dim blinkyX As Integer = 14 'like pacman's x and y
    Dim blinkyY As Integer = 14
    Dim blinkytargetx As Integer 'each ghost has a target square. these two define that square
    Dim blinkytargety As Integer
    Dim blinkyxcounter As Integer = 10 'like pacman's counters
    Dim blinkyycounter As Integer = 0
    Dim blinkynewdirection As Integer 'used in frightened mode to pick a random direction
    Dim blinkyitwillwork As Boolean = False 'determines if ghost can turn
    Dim oldblinkydirection As String
    Dim blinkynotalloweddirections As String 'makes a list of directions ghost is not allowed to move in
    Dim blinkymode As String = "begin" 'ghosts have multiple modes. this records the current mode. changing this variable to "chase" and adding me.invalidate somewhere will prevent the ghosts from moving
    Dim blinkyflash As Boolean = False 'helps the drawing function to know if the ghost is flashing or not at the end of frightened mode


    'pinky
    Dim pinkyboxnum As Integer
    Dim pinkydirection As String = "up"
    Dim pinkyX As Integer = 14
    Dim pinkyY As Integer = 14
    Dim pinkytargetx As Integer
    Dim pinkytargety As Integer
    Dim pinkyxcounter As Integer = 10
    Dim pinkyycounter As Integer = 0
    Dim pinkynewdirection As Integer
    Dim pinkyitwillwork As Boolean = False
    Dim oldpinkydirection As String
    Dim pinkynotalloweddirections As String
    Dim pinkymode As String = "begin"
    Dim pinkyflash As Boolean = False

    'inky
    Dim inkyboxnum As Integer
    Dim inkydirection As String = "right"
    Dim inkyX As Integer = 14
    Dim inkyY As Integer = 14
    Dim inkytargetx As Integer
    Dim inkytargety As Integer
    Dim inkyxcounter As Integer = 10
    Dim inkyycounter As Integer = 0
    Dim inkynewdirection As Integer
    Dim inkyitwillwork As Boolean = False
    Dim oldinkydirection As String
    Dim inkypicturestage As Integer = 1
    Dim inkynotalloweddirections As String
    Dim inkymode As String = "begin"
    Dim inkyflash As Boolean = False

    'sue
    Dim sueboxnum As Integer
    Dim suedirection As String = "left"
    Dim sueX As Integer = 14
    Dim sueY As Integer = 14
    Dim suetargetx As Integer
    Dim suetargety As Integer
    Dim suexcounter As Integer = 10
    Dim sueycounter As Integer = 0
    Dim suenewdirection As Integer
    Dim sueitwillwork As Boolean = False
    Dim oldsuedirection As String
    Dim suenotalloweddirections As String
    Dim suemode As String = "begin"
    Dim sueflash As Boolean = False

    Dim runcounter As Integer = 0 'used to determine when frightened mode should end

    Dim endcounter As Integer = 0 'used to count during winning sequence

    Dim scattercounter As Integer = 0 'determines when ghosts should go into scatter mode or chase mode

    Dim startcounter As Integer = 0 'used to get the ghosts out of the box in the beginning

    Dim fruit As Rectangle = New Rectangle(285, 395, 30, 30)
    Dim dotseatencounter As Integer = 0 'as it says, counts the dots you've eaten. 
    Dim fruitpresent As Boolean = False 'used to know when to draw the cherries

    Dim score As Integer = 0 'records the score
    Dim ghostseatencounter As Integer = 2 'records the ghosts eaten during each frightened phase
    
    Private Sub pacman5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'this is pretty obvious isn't it?
        If e.KeyCode = Keys.Up Then
            inputdirection = "up"
            If blinkymode = "begin" Then 'starts things up the first time you press a direction
                blinkymove.Start()
                starting.Start()
                blinkymode = "scatter"
                begin_label.Visible = False
                invalidation.Start()
            End If
        End If
        If e.KeyCode = Keys.Down Then
            inputdirection = "down"
            If blinkymode = "begin" Then
                blinkymove.Start()
                starting.Start()
                blinkymode = "scatter"
                begin_label.Visible = False
                invalidation.Start()
            End If
        End If
        If e.KeyCode = Keys.Left Then
            inputdirection = "left"
            If blinkymode = "begin" Then
                blinkymove.Start()
                starting.Start()
                blinkymode = "scatter"
                begin_label.Visible = False
                invalidation.Start()
            End If
        End If
        If e.KeyCode = Keys.Right Then
            inputdirection = "right"
            If blinkymode = "begin" Then
                blinkymove.Start()
                starting.Start()
                blinkymode = "scatter"
                begin_label.Visible = False
                invalidation.Start()
            End If
        End If
    End Sub
    Private Sub pacman5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'define all the things
        pacman = New Rectangle(275, 515, 30, 30)
        blinky = New Rectangle(285, 275, 30, 30)
        pinky = New Rectangle(285, 340, 30, 30)
        inky = New Rectangle(250, 340, 30, 30)
        sue = New Rectangle(320, 340, 30, 30)
        'r1
        For i As Integer = 1 To 12
            dot(i) = New Rectangle(i * 20 + 30, 90, 2, 2)
        Next
        For i As Integer = 13 To 24
            dot(i) = New Rectangle((i - 12) * 20 + 310, 90, 2, 2)
        Next
        'r2
        dot(25) = New Rectangle(50, 110, 2, 2)
        dot(26) = New Rectangle(150, 110, 2, 2)
        dot(27) = New Rectangle(270, 110, 2, 2)
        dot(28) = New Rectangle(330, 110, 2, 2)
        dot(29) = New Rectangle(450, 110, 2, 2)
        dot(30) = New Rectangle(550, 110, 2, 2)
        'r3
        dot(31) = New Rectangle(50, 130, 2, 2)
        dot(32) = New Rectangle(150, 130, 2, 2)
        dot(33) = New Rectangle(270, 130, 2, 2)
        dot(34) = New Rectangle(330, 130, 2, 2)
        dot(35) = New Rectangle(450, 130, 2, 2)
        dot(36) = New Rectangle(550, 130, 2, 2)
        'r4
        dot(37) = New Rectangle(50, 150, 2, 2)
        dot(38) = New Rectangle(150, 150, 2, 2)
        dot(39) = New Rectangle(270, 150, 2, 2)
        dot(40) = New Rectangle(330, 150, 2, 2)
        dot(41) = New Rectangle(450, 150, 2, 2)
        dot(42) = New Rectangle(550, 150, 2, 2)
        'r5
        For i As Integer = 43 To 68
            dot(i) = New Rectangle((i - 42) * 20 + 30, 170, 2, 2)
        Next
        'r6
        dot(69) = New Rectangle(50, 190, 2, 2)
        dot(70) = New Rectangle(150, 190, 2, 2)
        dot(71) = New Rectangle(210, 190, 2, 2)
        dot(72) = New Rectangle(390, 190, 2, 2)
        dot(73) = New Rectangle(450, 190, 2, 2)
        dot(74) = New Rectangle(550, 190, 2, 2)
        'r7
        dot(75) = New Rectangle(50, 210, 2, 2)
        dot(76) = New Rectangle(150, 210, 2, 2)
        dot(77) = New Rectangle(210, 210, 2, 2)
        dot(78) = New Rectangle(390, 210, 2, 2)
        dot(79) = New Rectangle(450, 210, 2, 2)
        dot(80) = New Rectangle(550, 210, 2, 2)
        '8
        dot(81) = New Rectangle(50, 230, 2, 2)
        dot(82) = New Rectangle(70, 230, 2, 2)
        dot(83) = New Rectangle(90, 230, 2, 2)
        dot(84) = New Rectangle(110, 230, 2, 2)
        dot(85) = New Rectangle(130, 230, 2, 2)
        dot(86) = New Rectangle(150, 230, 2, 2)
        dot(87) = New Rectangle(210, 230, 2, 2)
        dot(88) = New Rectangle(230, 230, 2, 2)
        dot(89) = New Rectangle(250, 230, 2, 2)
        dot(90) = New Rectangle(270, 230, 2, 2)
        dot(91) = New Rectangle(330, 230, 2, 2)
        dot(92) = New Rectangle(350, 230, 2, 2)
        dot(93) = New Rectangle(370, 230, 2, 2)
        dot(94) = New Rectangle(390, 230, 2, 2)
        dot(95) = New Rectangle(450, 230, 2, 2)
        dot(96) = New Rectangle(470, 230, 2, 2)
        dot(97) = New Rectangle(490, 230, 2, 2)
        dot(98) = New Rectangle(510, 230, 2, 2)
        dot(99) = New Rectangle(530, 230, 2, 2)
        dot(100) = New Rectangle(550, 230, 2, 2)
        '9
        dot(101) = New Rectangle(150, 250, 2, 2)
        dot(102) = New Rectangle(450, 250, 2, 2)
        '10
        dot(103) = New Rectangle(150, 270, 2, 2)
        dot(104) = New Rectangle(450, 270, 2, 2)
        '11
        dot(105) = New Rectangle(150, 290, 2, 2)
        dot(106) = New Rectangle(450, 290, 2, 2)
        '12
        dot(107) = New Rectangle(150, 310, 2, 2)
        dot(108) = New Rectangle(450, 310, 2, 2)
        '13
        dot(109) = New Rectangle(150, 330, 2, 2)
        dot(110) = New Rectangle(450, 330, 2, 2)
        '14
        dot(111) = New Rectangle(150, 350, 2, 2)
        dot(112) = New Rectangle(450, 350, 2, 2)
        '15
        dot(113) = New Rectangle(150, 370, 2, 2)
        dot(114) = New Rectangle(450, 370, 2, 2)
        '16
        dot(115) = New Rectangle(150, 390, 2, 2)
        dot(116) = New Rectangle(450, 390, 2, 2)
        '17
        dot(117) = New Rectangle(150, 410, 2, 2)
        dot(118) = New Rectangle(450, 410, 2, 2)
        '18
        dot(119) = New Rectangle(150, 430, 2, 2)
        dot(120) = New Rectangle(450, 430, 2, 2)
        '19
        dot(121) = New Rectangle(150, 450, 2, 2)
        dot(122) = New Rectangle(450, 450, 2, 2)
        '20
        For i As Integer = 123 To 134
            dot(i) = New Rectangle((i - 122) * 20 + 30, 470, 2, 2)
        Next
        For i As Integer = 135 To 146
            dot(i) = New Rectangle((i - 134) * 20 + 310, 470, 2, 2)
        Next
        '21
        dot(147) = New Rectangle(50, 490, 2, 2)
        dot(148) = New Rectangle(150, 490, 2, 2)
        dot(149) = New Rectangle(270, 490, 2, 2)
        dot(150) = New Rectangle(330, 490, 2, 2)
        dot(151) = New Rectangle(450, 490, 2, 2)
        dot(152) = New Rectangle(550, 490, 2, 2)
        '22
        dot(153) = New Rectangle(50, 510, 2, 2)
        dot(154) = New Rectangle(150, 510, 2, 2)
        dot(155) = New Rectangle(270, 510, 2, 2)
        dot(156) = New Rectangle(330, 510, 2, 2)
        dot(157) = New Rectangle(450, 510, 2, 2)
        dot(158) = New Rectangle(550, 510, 2, 2)
        '23
        dot(159) = New Rectangle(50, 530, 2, 2)
        dot(160) = New Rectangle(70, 530, 2, 2)
        dot(161) = New Rectangle(90, 530, 2, 2)
        For i As Integer = 162 To 177
            dot(i) = New Rectangle((i - 161) * 20 + 130, 530, 2, 2)
        Next
        dot(178) = New Rectangle(510, 530, 2, 2)
        dot(179) = New Rectangle(530, 530, 2, 2)
        dot(180) = New Rectangle(550, 530, 2, 2)
        '24
        dot(181) = New Rectangle(90, 550, 2, 2)
        dot(182) = New Rectangle(150, 550, 2, 2)
        dot(183) = New Rectangle(210, 550, 2, 2)
        dot(184) = New Rectangle(390, 550, 2, 2)
        dot(185) = New Rectangle(450, 550, 2, 2)
        dot(186) = New Rectangle(510, 550, 2, 2)
        '25
        dot(187) = New Rectangle(90, 570, 2, 2)
        dot(188) = New Rectangle(150, 570, 2, 2)
        dot(189) = New Rectangle(210, 570, 2, 2)
        dot(190) = New Rectangle(390, 570, 2, 2)
        dot(191) = New Rectangle(450, 570, 2, 2)
        dot(192) = New Rectangle(510, 570, 2, 2)
        '26
        dot(193) = New Rectangle(50, 590, 2, 2)
        dot(194) = New Rectangle(70, 590, 2, 2)
        dot(195) = New Rectangle(90, 590, 2, 2)
        dot(196) = New Rectangle(110, 590, 2, 2)
        dot(197) = New Rectangle(130, 590, 2, 2)
        dot(198) = New Rectangle(150, 590, 2, 2)
        dot(199) = New Rectangle(210, 590, 2, 2)
        dot(200) = New Rectangle(230, 590, 2, 2)
        dot(201) = New Rectangle(250, 590, 2, 2)
        dot(202) = New Rectangle(270, 590, 2, 2)
        dot(203) = New Rectangle(330, 590, 2, 2)
        dot(204) = New Rectangle(350, 590, 2, 2)
        dot(205) = New Rectangle(370, 590, 2, 2)
        dot(206) = New Rectangle(390, 590, 2, 2)
        dot(207) = New Rectangle(450, 590, 2, 2)
        dot(208) = New Rectangle(470, 590, 2, 2)
        dot(209) = New Rectangle(490, 590, 2, 2)
        dot(210) = New Rectangle(510, 590, 2, 2)
        dot(211) = New Rectangle(530, 590, 2, 2)
        dot(212) = New Rectangle(550, 590, 2, 2)
        '27
        dot(213) = New Rectangle(50, 610, 2, 2)
        dot(214) = New Rectangle(270, 610, 2, 2)
        dot(215) = New Rectangle(330, 610, 2, 2)
        dot(216) = New Rectangle(550, 610, 2, 2)
        '28
        dot(217) = New Rectangle(50, 630, 2, 2)
        dot(218) = New Rectangle(270, 630, 2, 2)
        dot(219) = New Rectangle(330, 630, 2, 2)
        dot(220) = New Rectangle(550, 630, 2, 2)
        '29
        For i As Integer = 221 To 246
            dot(i) = New Rectangle((i - 220) * 20 + 30, 650, 2, 2)
        Next
        'power dots
        powerdot(1) = New Rectangle(50 - 7, 130 - 7, 16, 16)
        powerdot(2) = New Rectangle(550 - 7, 130 - 7, 16, 16)
        powerdot(3) = New Rectangle(50 - 7, 530 - 7, 16, 16)
        powerdot(4) = New Rectangle(550 - 7, 530 - 7, 16, 16)
    End Sub
    Private Sub invalidation_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles invalidation.Tick
        Me.Invalidate() 'all powerful
        If dotseatencounter = 70 Or dotseatencounter = 170 Then 'showing the fruit if its time
            fruitpresent = True
            fruit_timer.Start()
        End If
        score_label.Text = "Score = " & score
        'i wanted to check for intersections more frequently so i put that in here 
        'i think it worked out alright.  intersection may not be perfect though
        blinkyboxnum = blinkyY * 28 + blinkyX
        pinkyboxnum = pinkyY * 28 + pinkyX
        inkyboxnum = inkyY * 28 + inkyX
        sueboxnum = sueY * 28 + sueX
        If blinkyboxnum = boxnum Then 'losing sequence
            If blinkymode = "chase" Or blinkymode = "scatter" Then
                pacmanmove.Stop()
                pacmanstagesadjust.Stop()
                blinkymove.Stop()
                pinkymove.Stop()
                inkymove.Stop()
                suemove.Stop()
                starting.Stop()
                invalidation.Stop()
                MessageBox.Show("Game Over!" & vbCrLf & "Your score was: " & score)
                Me.Close()
            ElseIf blinkymode = "frightened" Then 'unless the ghosts are frightened in which case the ghost is eaten
                blinkymode = "eyes"
                blinkymove.Interval = 10
                score += ghostseatencounter * 100
                ghostseatencounter = ghostseatencounter * 2
            End If
        End If
        If pinkyboxnum = boxnum Then 'losing sequence
            If pinkymode = "chase" Or pinkymode = "scatter" Then
                pacmanmove.Stop()
                pacmanstagesadjust.Stop()
                blinkymove.Stop()
                pinkymove.Stop()
                inkymove.Stop()
                suemove.Stop()
                invalidation.Stop()
                starting.Stop()
                MessageBox.Show("Game Over!" & vbCrLf & "Your score was: " & score)
                Me.Close()
            ElseIf pinkymode = "frightened" Then 'unless the ghosts are frightened in which case the ghost is eaten
                pinkymode = "eyes"
                pinkymove.Interval = 10
                score += ghostseatencounter * 100
                ghostseatencounter = ghostseatencounter * 2
            End If
        End If
        If inkyboxnum = boxnum Then 'losing sequence
            If inkymode = "chase" Or inkymode = "scatter" Then
                pacmanmove.Stop()
                pacmanstagesadjust.Stop()
                blinkymove.Stop()
                pinkymove.Stop()
                inkymove.Stop()
                suemove.Stop()
                invalidation.Stop()
                starting.Stop()
                MessageBox.Show("Game Over!" & vbCrLf & "Your score was: " & score)
                Me.Close()
            ElseIf inkymode = "frightened" Then 'unless the ghosts are frightened in which case the ghost is eaten
                inkymode = "eyes"
                inkymove.Interval = 10
                score += ghostseatencounter * 100
                ghostseatencounter = ghostseatencounter * 2
            End If
        End If
        If sueboxnum = boxnum Then 'losing sequence
            If suemode = "chase" Or suemode = "scatter" Then
                pacmanmove.Stop()
                pacmanstagesadjust.Stop()
                blinkymove.Stop()
                pinkymove.Stop()
                inkymove.Stop()
                suemove.Stop()
                invalidation.Stop()
                starting.Stop()
                MessageBox.Show("Game Over!" & vbCrLf & "Your score was: " & score)
                Me.Close()
            ElseIf suemode = "frightened" Then 'unless the ghosts are frightened in which case the ghost is eaten
                suemode = "eyes"
                suemove.Interval = 10
                score += ghostseatencounter * 100
                ghostseatencounter = ghostseatencounter * 2
            End If
        End If
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim g As Graphics = e.Graphics
        If direction = "right" Then 'drawing pacman
            If Math.Abs(pacstage) = 1 Then
                g.DrawImage(My.Resources.pacmanrights1, pacman)
            ElseIf Math.Abs(pacstage) = 2 Then
                g.DrawImage(My.Resources.pacmanrights2, pacman)
            ElseIf Math.Abs(pacstage) = 3 Then
                g.DrawImage(My.Resources.pacmanrights3, pacman)
            ElseIf Math.Abs(pacstage) = 4 Then
                g.DrawImage(My.Resources.pacmanrights4, pacman)
            ElseIf Math.Abs(pacstage) = 5 Then
                g.DrawImage(My.Resources.pacmanrights5, pacman)
            ElseIf Math.Abs(pacstage) = 6 Then
                g.DrawImage(My.Resources.pacmanrights6, pacman)
            ElseIf Math.Abs(pacstage) = 7 Then
                g.DrawImage(My.Resources.pacmanrights7, pacman)
            ElseIf Math.Abs(pacstage) = 0 Then
                g.DrawImage(My.Resources.pacmanclosed, pacman)
            End If
        ElseIf direction = "left" Then
            If Math.Abs(pacstage) = 1 Then
                g.DrawImage(My.Resources.pacmanlefts1, pacman)
            ElseIf Math.Abs(pacstage) = 2 Then
                g.DrawImage(My.Resources.pacmanlefts2, pacman)
            ElseIf Math.Abs(pacstage) = 3 Then
                g.DrawImage(My.Resources.pacmanlefts3, pacman)
            ElseIf Math.Abs(pacstage) = 4 Then
                g.DrawImage(My.Resources.pacmanlefts4, pacman)
            ElseIf Math.Abs(pacstage) = 5 Then
                g.DrawImage(My.Resources.pacmanlefts5, pacman)
            ElseIf Math.Abs(pacstage) = 6 Then
                g.DrawImage(My.Resources.pacmanlefts6, pacman)
            ElseIf Math.Abs(pacstage) = 7 Then
                g.DrawImage(My.Resources.pacmanlefts7, pacman)
            ElseIf Math.Abs(pacstage) = 0 Then
                g.DrawImage(My.Resources.pacmanclosed, pacman)
            End If
        ElseIf direction = "down" Then
            If Math.Abs(pacstage) = 1 Then
                g.DrawImage(My.Resources.pacmandowns1, pacman)
            ElseIf Math.Abs(pacstage) = 2 Then
                g.DrawImage(My.Resources.pacmandowns2, pacman)
            ElseIf Math.Abs(pacstage) = 3 Then
                g.DrawImage(My.Resources.pacmandowns3, pacman)
            ElseIf Math.Abs(pacstage) = 4 Then
                g.DrawImage(My.Resources.pacmandowns4, pacman)
            ElseIf Math.Abs(pacstage) = 5 Then
                g.DrawImage(My.Resources.pacmandowns5, pacman)
            ElseIf Math.Abs(pacstage) = 6 Then
                g.DrawImage(My.Resources.pacmandowns6, pacman)
            ElseIf Math.Abs(pacstage) = 7 Then
                g.DrawImage(My.Resources.pacmandowns7, pacman)
            ElseIf Math.Abs(pacstage) = 0 Then
                g.DrawImage(My.Resources.pacmanclosed, pacman)
            End If
        ElseIf direction = "up" Then
            If Math.Abs(pacstage) = 1 Then
                g.DrawImage(My.Resources.pacmanups1, pacman)
            ElseIf Math.Abs(pacstage) = 2 Then
                g.DrawImage(My.Resources.pacmanups2, pacman)
            ElseIf Math.Abs(pacstage) = 3 Then
                g.DrawImage(My.Resources.pacmanups3, pacman)
            ElseIf Math.Abs(pacstage) = 4 Then
                g.DrawImage(My.Resources.pacmanups4, pacman)
            ElseIf Math.Abs(pacstage) = 5 Then
                g.DrawImage(My.Resources.pacmanups5, pacman)
            ElseIf Math.Abs(pacstage) = 6 Then
                g.DrawImage(My.Resources.pacmanups6, pacman)
            ElseIf Math.Abs(pacstage) = 7 Then
                g.DrawImage(My.Resources.pacmanups7, pacman)
            ElseIf Math.Abs(pacstage) = 0 Then
                g.DrawImage(My.Resources.pacmanclosed, pacman)
            End If
        ElseIf direction = "" Then
            g.DrawImage(My.Resources.pacmanclosed, pacman)
        End If
        'draw all those lines
        g.DrawLine(mypen, 80, 110, 120, 110)
        g.DrawLine(mypen, 80, 150, 120, 150)
        g.DrawLine(mypen, 70, 120, 70, 140)
        g.DrawLine(mypen, 130, 120, 130, 140)
        g.DrawArc(mypen, 70, 110, 20, 20, 180, 90)
        g.DrawArc(mypen, 110, 110, 20, 20, 270, 90)
        g.DrawArc(mypen, 110, 130, 20, 20, 0, 90)
        g.DrawArc(mypen, 70, 130, 20, 20, 90, 90)

        g.DrawLine(mypen, 180, 110, 240, 110)
        g.DrawLine(mypen, 180, 150, 240, 150)
        g.DrawLine(mypen, 170, 120, 170, 140)
        g.DrawLine(mypen, 250, 120, 250, 140)
        g.DrawArc(mypen, 170, 110, 20, 20, 180, 90)
        g.DrawArc(mypen, 230, 110, 20, 20, 270, 90)
        g.DrawArc(mypen, 230, 130, 20, 20, 0, 90)
        g.DrawArc(mypen, 170, 130, 20, 20, 90, 90)

        g.DrawLine(mypen, 360, 110, 420, 110)
        g.DrawLine(mypen, 360, 150, 420, 150)
        g.DrawLine(mypen, 350, 120, 350, 140)
        g.DrawLine(mypen, 430, 120, 430, 140)
        g.DrawArc(mypen, 350, 110, 20, 20, 180, 90)
        g.DrawArc(mypen, 410, 110, 20, 20, 270, 90)
        g.DrawArc(mypen, 410, 130, 20, 20, 0, 90)
        g.DrawArc(mypen, 350, 130, 20, 20, 90, 90)

        g.DrawLine(mypen, 480, 110, 520, 110)
        g.DrawLine(mypen, 480, 150, 520, 150)
        g.DrawLine(mypen, 470, 120, 470, 140)
        g.DrawLine(mypen, 530, 120, 530, 140)
        g.DrawArc(mypen, 470, 110, 20, 20, 180, 90)
        g.DrawArc(mypen, 510, 110, 20, 20, 270, 90)
        g.DrawArc(mypen, 510, 130, 20, 20, 0, 90)
        g.DrawArc(mypen, 470, 130, 20, 20, 90, 90)

        g.DrawLine(mypen, 80, 190, 120, 190)
        g.DrawLine(mypen, 80, 210, 120, 210)
        g.DrawArc(mypen, 70, 190, 20, 20, 90, 180)
        g.DrawArc(mypen, 110, 190, 20, 20, 270, 180)

        g.DrawLine(mypen, 170, 200, 170, 320)
        g.DrawLine(mypen, 190, 280, 190, 320)
        g.DrawLine(mypen, 190, 200, 190, 240)
        g.DrawLine(mypen, 200, 250, 240, 250)
        g.DrawLine(mypen, 200, 270, 240, 270)
        g.DrawArc(mypen, 170, 190, 20, 20, 180, 180)
        g.DrawArc(mypen, 170, 310, 20, 20, 0, 180)
        g.DrawArc(mypen, 230, 250, 20, 20, 270, 180)
        g.DrawArc(mypen, 190, 230, 20, 20, 90, 90)
        g.DrawArc(mypen, 190, 270, 20, 20, 180, 90)

        g.DrawLine(mypen, 240, 190, 360, 190)
        g.DrawLine(mypen, 240, 210, 280, 210)
        g.DrawLine(mypen, 320, 210, 360, 210)
        g.DrawLine(mypen, 290, 220, 290, 260)
        g.DrawLine(mypen, 310, 220, 310, 260)
        g.DrawArc(mypen, 230, 190, 20, 20, 90, 180)
        g.DrawArc(mypen, 350, 190, 20, 20, 270, 180)
        g.DrawArc(mypen, 290, 250, 20, 20, 0, 180)
        g.DrawArc(mypen, 270, 210, 20, 20, 270, 90)
        g.DrawArc(mypen, 310, 210, 20, 20, 180, 90)

        g.DrawLine(mypen, 430, 200, 430, 320)
        g.DrawLine(mypen, 410, 280, 410, 320)
        g.DrawLine(mypen, 410, 200, 410, 240)
        g.DrawLine(mypen, 360, 250, 400, 250)
        g.DrawLine(mypen, 360, 270, 400, 270)
        g.DrawArc(mypen, 410, 190, 20, 20, 180, 180)
        g.DrawArc(mypen, 410, 310, 20, 20, 0, 180)
        g.DrawArc(mypen, 350, 250, 20, 20, 90, 180)
        g.DrawArc(mypen, 390, 230, 20, 20, 0, 90)
        g.DrawArc(mypen, 390, 270, 20, 20, 270, 90)

        g.DrawLine(mypen, 480, 190, 520, 190)
        g.DrawLine(mypen, 480, 210, 520, 210)
        g.DrawArc(mypen, 470, 190, 20, 20, 90, 180)
        g.DrawArc(mypen, 510, 190, 20, 20, 270, 180)

        g.DrawLine(mypen, 170, 380, 170, 440)
        g.DrawLine(mypen, 190, 380, 190, 440)
        g.DrawArc(mypen, 170, 370, 20, 20, 180, 180)
        g.DrawArc(mypen, 170, 430, 20, 20, 0, 180)

        g.DrawLine(mypen, 410, 380, 410, 440)
        g.DrawLine(mypen, 430, 380, 430, 440)
        g.DrawArc(mypen, 410, 370, 20, 20, 180, 180)
        g.DrawArc(mypen, 410, 430, 20, 20, 0, 180)

        g.DrawLine(mypen, 240, 430, 360, 430)
        g.DrawLine(mypen, 240, 450, 280, 450)
        g.DrawLine(mypen, 320, 450, 360, 450)
        g.DrawLine(mypen, 290, 460, 290, 500)
        g.DrawLine(mypen, 310, 460, 310, 500)
        g.DrawArc(mypen, 230, 430, 20, 20, 90, 180)
        g.DrawArc(mypen, 350, 430, 20, 20, 270, 180)
        g.DrawArc(mypen, 290, 490, 20, 20, 0, 180)
        g.DrawArc(mypen, 270, 450, 20, 20, 270, 90)
        g.DrawArc(mypen, 310, 450, 20, 20, 180, 90)

        g.DrawLine(mypen, 230, 310, 370, 310)
        g.DrawLine(mypen, 230, 390, 370, 390)
        g.DrawLine(mypen, 230, 310, 230, 390)
        g.DrawLine(mypen, 370, 310, 370, 390)
        g.DrawLine(mypen, 240, 320, 360, 320)
        g.DrawLine(mypen, 240, 380, 360, 380)
        g.DrawLine(mypen, 240, 320, 240, 380)
        g.DrawLine(mypen, 360, 320, 360, 380)
        g.FillRectangle(Brushes.Bisque, 280, 310, 40, 10)

        g.DrawLine(mypen, 240, 550, 360, 550)
        g.DrawLine(mypen, 240, 570, 280, 570)
        g.DrawLine(mypen, 320, 570, 360, 570)
        g.DrawLine(mypen, 290, 580, 290, 620)
        g.DrawLine(mypen, 310, 580, 310, 620)
        g.DrawArc(mypen, 230, 550, 20, 20, 90, 180)
        g.DrawArc(mypen, 350, 550, 20, 20, 270, 180)
        g.DrawArc(mypen, 290, 610, 20, 20, 0, 180)
        g.DrawArc(mypen, 270, 570, 20, 20, 270, 90)
        g.DrawArc(mypen, 310, 570, 20, 20, 180, 90)

        g.DrawLine(mypen, 180, 490, 240, 490)
        g.DrawLine(mypen, 180, 510, 240, 510)
        g.DrawArc(mypen, 170, 490, 20, 20, 90, 180)
        g.DrawArc(mypen, 230, 490, 20, 20, 270, 180)

        g.DrawLine(mypen, 360, 490, 420, 490)
        g.DrawLine(mypen, 360, 510, 420, 510)
        g.DrawArc(mypen, 350, 490, 20, 20, 90, 180)
        g.DrawArc(mypen, 410, 490, 20, 20, 270, 180)

        g.DrawLine(mypen, 80, 490, 120, 490)
        g.DrawLine(mypen, 80, 510, 100, 510)
        g.DrawLine(mypen, 130, 500, 130, 560)
        g.DrawLine(mypen, 110, 520, 110, 560)
        g.DrawArc(mypen, 70, 490, 20, 20, 90, 180)
        g.DrawArc(mypen, 90, 510, 20, 20, 270, 90)
        g.DrawArc(mypen, 110, 490, 20, 20, 270, 90)
        g.DrawArc(mypen, 110, 550, 20, 20, 0, 180)

        g.DrawLine(mypen, 480, 490, 520, 490)
        g.DrawLine(mypen, 500, 510, 520, 510)
        g.DrawLine(mypen, 470, 500, 470, 560)
        g.DrawLine(mypen, 490, 520, 490, 560)
        g.DrawArc(mypen, 510, 490, 20, 20, 270, 180)
        g.DrawArc(mypen, 490, 510, 20, 20, 180, 90)
        g.DrawArc(mypen, 470, 490, 20, 20, 180, 90)
        g.DrawArc(mypen, 470, 550, 20, 20, 0, 180)

        g.DrawLine(mypen, 80, 610, 160, 610)
        g.DrawLine(mypen, 200, 610, 240, 610)
        g.DrawLine(mypen, 80, 630, 240, 630)
        g.DrawLine(mypen, 170, 560, 170, 600)
        g.DrawLine(mypen, 190, 560, 190, 600)
        g.DrawArc(mypen, 70, 610, 20, 20, 90, 180)
        g.DrawArc(mypen, 230, 610, 20, 20, 270, 180)
        g.DrawArc(mypen, 150, 590, 20, 20, 0, 90)
        g.DrawArc(mypen, 190, 590, 20, 20, 90, 90)
        g.DrawArc(mypen, 170, 550, 20, 20, 180, 180)

        g.DrawLine(mypen, 440, 610, 520, 610)
        g.DrawLine(mypen, 360, 610, 400, 610)
        g.DrawLine(mypen, 360, 630, 520, 630)
        g.DrawLine(mypen, 410, 560, 410, 600)
        g.DrawLine(mypen, 430, 560, 430, 600)
        g.DrawArc(mypen, 350, 610, 20, 20, 90, 180)
        g.DrawArc(mypen, 510, 610, 20, 20, 270, 180)
        g.DrawArc(mypen, 390, 590, 20, 20, 0, 90)
        g.DrawArc(mypen, 430, 590, 20, 20, 90, 90)
        g.DrawArc(mypen, 410, 550, 20, 20, 180, 180)

        g.DrawLine(mypen, 40, 70, 280, 70)
        g.DrawLine(mypen, 290, 80, 290, 140)
        g.DrawLine(mypen, 310, 80, 310, 140)
        g.DrawLine(mypen, 320, 70, 560, 70)
        g.DrawLine(mypen, 570, 80, 570, 240)
        g.DrawLine(mypen, 560, 250, 480, 250)
        g.DrawLine(mypen, 470, 260, 470, 320)
        g.DrawLine(mypen, 480, 330, 610, 330)
        g.DrawLine(mypen, 480, 370, 610, 370)
        g.DrawLine(mypen, 470, 380, 470, 440)
        g.DrawLine(mypen, 480, 450, 560, 450)
        g.DrawLine(mypen, 570, 460, 570, 540)
        g.DrawLine(mypen, 560, 550, 540, 550)
        g.DrawLine(mypen, 560, 570, 540, 570)
        g.DrawLine(mypen, 570, 580, 570, 660)
        g.DrawLine(mypen, 40, 670, 560, 670)
        g.DrawLine(mypen, 30, 660, 30, 580)
        g.DrawLine(mypen, 40, 570, 60, 570)
        g.DrawLine(mypen, 40, 550, 60, 550)
        g.DrawLine(mypen, 30, 540, 30, 460)
        g.DrawLine(mypen, 40, 450, 120, 450)
        g.DrawLine(mypen, 130, 440, 130, 380)
        g.DrawLine(mypen, 120, 370, 0, 370)
        g.DrawLine(mypen, 120, 330, 0, 330)
        g.DrawLine(mypen, 130, 320, 130, 260)
        g.DrawLine(mypen, 40, 250, 120, 250)
        g.DrawLine(mypen, 30, 240, 30, 80)
        g.DrawArc(mypen, 30, 70, 20, 20, 180, 90)
        g.DrawArc(mypen, 270, 70, 20, 20, 270, 90)
        g.DrawArc(mypen, 290, 130, 20, 20, 0, 180)
        g.DrawArc(mypen, 310, 70, 20, 20, 180, 90)
        g.DrawArc(mypen, 550, 70, 20, 20, 270, 90)
        g.DrawArc(mypen, 550, 230, 20, 20, 0, 90)
        g.DrawArc(mypen, 470, 250, 20, 20, 180, 90)
        g.DrawArc(mypen, 470, 310, 20, 20, 90, 90)
        g.DrawArc(mypen, 470, 370, 20, 20, 180, 90)
        g.DrawArc(mypen, 470, 430, 20, 20, 90, 90)
        g.DrawArc(mypen, 550, 450, 20, 20, 270, 90)
        g.DrawArc(mypen, 550, 530, 20, 20, 0, 90)
        g.DrawArc(mypen, 530, 550, 20, 20, 90, 180)
        g.DrawArc(mypen, 550, 570, 20, 20, 270, 90)
        g.DrawArc(mypen, 550, 650, 20, 20, 0, 90)
        g.DrawArc(mypen, 30, 650, 20, 20, 90, 90)
        g.DrawArc(mypen, 30, 570, 20, 20, 180, 90)
        g.DrawArc(mypen, 50, 550, 20, 20, 270, 180)
        g.DrawArc(mypen, 30, 530, 20, 20, 90, 90)
        g.DrawArc(mypen, 30, 450, 20, 20, 180, 90)
        g.DrawArc(mypen, 110, 430, 20, 20, 0, 90)
        g.DrawArc(mypen, 110, 370, 20, 20, 270, 90)
        g.DrawArc(mypen, 110, 310, 20, 20, 0, 90)
        g.DrawArc(mypen, 110, 250, 20, 20, 270, 90)
        g.DrawArc(mypen, 30, 230, 20, 20, 90, 90)

        g.DrawLine(mypen, 30, 60, 570, 60)
        g.DrawLine(mypen, 580, 70, 580, 250)
        g.DrawLine(mypen, 570, 260, 490, 260)
        g.DrawLine(mypen, 480, 270, 480, 310)
        g.DrawLine(mypen, 490, 320, 610, 320)
        g.DrawLine(mypen, 490, 380, 610, 380)
        g.DrawLine(mypen, 480, 390, 480, 430)
        g.DrawLine(mypen, 490, 440, 570, 440)
        g.DrawLine(mypen, 580, 450, 580, 670)
        g.DrawLine(mypen, 30, 680, 570, 680)
        g.DrawLine(mypen, 20, 450, 20, 670)
        g.DrawLine(mypen, 30, 440, 110, 440)
        g.DrawLine(mypen, 120, 430, 120, 390)
        g.DrawLine(mypen, 0, 380, 110, 380)
        g.DrawLine(mypen, 0, 320, 110, 320)
        g.DrawLine(mypen, 120, 310, 120, 270)
        g.DrawLine(mypen, 30, 260, 110, 260)
        g.DrawLine(mypen, 20, 250, 20, 70)
        g.DrawArc(mypen, 20, 60, 20, 20, 180, 90)
        g.DrawArc(mypen, 560, 60, 20, 20, 270, 90)
        g.DrawArc(mypen, 560, 240, 20, 20, 0, 90)
        g.DrawArc(mypen, 480, 260, 20, 20, 180, 90)
        g.DrawArc(mypen, 480, 300, 20, 20, 90, 90)
        g.DrawArc(mypen, 480, 380, 20, 20, 180, 90)
        g.DrawArc(mypen, 480, 420, 20, 20, 90, 90)
        g.DrawArc(mypen, 560, 440, 20, 20, 270, 90)
        g.DrawArc(mypen, 560, 660, 20, 20, 0, 90)
        g.DrawArc(mypen, 20, 660, 20, 20, 90, 90)
        g.DrawArc(mypen, 20, 440, 20, 20, 180, 90)
        g.DrawArc(mypen, 100, 420, 20, 20, 0, 90)
        g.DrawArc(mypen, 100, 380, 20, 20, 270, 90)
        g.DrawArc(mypen, 100, 300, 20, 20, 0, 90)
        g.DrawArc(mypen, 100, 260, 20, 20, 270, 90)
        g.DrawArc(mypen, 20, 240, 20, 20, 90, 90)





        'dots
        For i As Integer = 1 To 246
            g.FillEllipse(Brushes.White, dot(i))
        Next
        'powerdots()
        g.FillEllipse(Brushes.White, powerdot(1))
        g.FillEllipse(Brushes.White, powerdot(2))
        g.FillEllipse(Brushes.White, powerdot(3))
        g.FillEllipse(Brushes.White, powerdot(4))
        'ghosts
        If blinkymode = "chase" Or blinkymode = "scatter" Or blinkymode = "begin" Or blinkymode = "start" Then
            If blinkydirection = "up" Then
                g.DrawImage(My.Resources.blinkyup, blinky)
            ElseIf blinkydirection = "down" Then
                g.DrawImage(My.Resources.blinkydown, blinky)
            ElseIf blinkydirection = "left" Then
                g.DrawImage(My.Resources.blinkyleft, blinky)
            ElseIf blinkydirection = "right" Then
                g.DrawImage(My.Resources.blinkyright, blinky)
            End If
        ElseIf blinkymode = "eyes" Or blinkymode = "restart" Then
            If blinkydirection = "up" Then
                g.DrawImage(My.Resources.eyes_up, blinky)
            ElseIf blinkydirection = "down" Then
                g.DrawImage(My.Resources.eyes_down, blinky)
            ElseIf blinkydirection = "left" Then
                g.DrawImage(My.Resources.eyes_left, blinky)
            ElseIf blinkydirection = "right" Then
                g.DrawImage(My.Resources.eyes_right, blinky)
            End If
        Else
            If blinkyflash = True Then
                g.DrawImage(My.Resources.flashingghost1, blinky)
            Else
                g.DrawImage(My.Resources.blueghost1, blinky)
            End If
        End If
        If pinkymode = "chase" Or pinkymode = "scatter" Or pinkymode = "begin" Or pinkymode = "start" Then
            If pinkydirection = "up" Then
                g.DrawImage(My.Resources.pinkyup, pinky)
            ElseIf pinkydirection = "down" Then
                g.DrawImage(My.Resources.pinkydown, pinky)
            ElseIf pinkydirection = "left" Then
                g.DrawImage(My.Resources.pinkyleft, pinky)
            ElseIf pinkydirection = "right" Then
                g.DrawImage(My.Resources.pinkyright, pinky)
            End If
        ElseIf pinkymode = "eyes" Or pinkymode = "restart" Then
            If pinkydirection = "up" Then
                g.DrawImage(My.Resources.eyes_up, pinky)
            ElseIf pinkydirection = "down" Then
                g.DrawImage(My.Resources.eyes_down, pinky)
            ElseIf pinkydirection = "left" Then
                g.DrawImage(My.Resources.eyes_left, pinky)
            ElseIf pinkydirection = "right" Then
                g.DrawImage(My.Resources.eyes_right, pinky)
            End If
        Else
            If pinkyflash = True Then
                g.DrawImage(My.Resources.flashingghost1, pinky)
            Else
                g.DrawImage(My.Resources.blueghost1, pinky)
            End If
        End If
        If inkymode = "chase" Or inkymode = "scatter" Or inkymode = "begin" Or inkymode = "start" Then
            If inkydirection = "up" Then
                g.DrawImage(My.Resources.inkyup, inky)
            ElseIf inkydirection = "down" Then
                g.DrawImage(My.Resources.inkydown, inky)
            ElseIf inkydirection = "left" Then
                g.DrawImage(My.Resources.inkyleft, inky)
            ElseIf inkydirection = "right" Then
                g.DrawImage(My.Resources.inkyright, inky)
            End If
        ElseIf inkymode = "eyes" Or inkymode = "restart" Then
            If inkydirection = "up" Then
                g.DrawImage(My.Resources.eyes_up, inky)
            ElseIf inkydirection = "down" Then
                g.DrawImage(My.Resources.eyes_down, inky)
            ElseIf inkydirection = "left" Then
                g.DrawImage(My.Resources.eyes_left, inky)
            ElseIf inkydirection = "right" Then
                g.DrawImage(My.Resources.eyes_right, inky)
            End If
        Else
            If inkyflash = True Then
                g.DrawImage(My.Resources.flashingghost1, inky)
            Else
                g.DrawImage(My.Resources.blueghost1, inky)
            End If
        End If
        If suemode = "chase" Or suemode = "scatter" Or suemode = "begin" Or suemode = "start" Then
            If suedirection = "up" Then
                g.DrawImage(My.Resources.sueup, sue)
            ElseIf suedirection = "down" Then
                g.DrawImage(My.Resources.suedown, sue)
            ElseIf suedirection = "left" Then
                g.DrawImage(My.Resources.sueleft, sue)
            ElseIf suedirection = "right" Then
                g.DrawImage(My.Resources.sueright, sue)
            End If
        ElseIf suemode = "eyes" Or suemode = "restart" Then
            If suedirection = "up" Then
                g.DrawImage(My.Resources.eyes_up, sue)
            ElseIf suedirection = "down" Then
                g.DrawImage(My.Resources.eyes_down, sue)
            ElseIf suedirection = "left" Then
                g.DrawImage(My.Resources.eyes_left, sue)
            ElseIf suedirection = "right" Then
                g.DrawImage(My.Resources.eyes_right, sue)
            End If
        Else
            If sueflash = True Then
                g.DrawImage(My.Resources.flashingghost1, sue)
            Else
                g.DrawImage(My.Resources.blueghost1, sue)
            End If
        End If
        'draw fruit
        If fruitpresent = True Then
            g.DrawImage(My.Resources.cherries, fruit)
        End If
    End Sub
    Private Sub pacmanmove_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pacmanmove.Tick
        boxnum = y * 28 + x
        Dim direction2 As String = inputdirection 'included so that the inputted direction will be tried every tick without being modified
        If ((pacman.X + 5) / 20 = x) And ((pacman.Y + 5) / 20 = y) Then 'should check if you're lined up at an intersection
            If (boxnum >= 114 And boxnum <= 126) Or (boxnum >= 128 And boxnum <= 140) Or (boxnum >= 227 And boxnum <= 230) Or (boxnum >= 232 And boxnum <= 236) Or (boxnum >= 238 And boxnum <= 239) Or (boxnum >= 241 And boxnum <= 245) Or (boxnum >= 247 And boxnum <= 250) Or (boxnum >= 311 And boxnum <= 314) Or (boxnum >= 319 And boxnum <= 321) Or (boxnum >= 324 And boxnum <= 326) Or (boxnum >= 331 And boxnum <= 334) Or (boxnum >= 402 And boxnum <= 404) Or (boxnum >= 406 And boxnum <= 407) Or (boxnum >= 409 And boxnum <= 411) Or (boxnum >= 477 And boxnum <= 482) Or (boxnum >= 484 And boxnum <= 485) Or (boxnum >= 496 And boxnum <= 497) Or (boxnum >= 499 And boxnum <= 504) Or (boxnum >= 571 And boxnum <= 578) Or (boxnum >= 646 And boxnum <= 650) Or (boxnum >= 652 And boxnum <= 653) Or (boxnum >= 655 And boxnum <= 657) Or (boxnum >= 660 And boxnum <= 662) Or (boxnum >= 664 And boxnum <= 665) Or (boxnum >= 667 And boxnum <= 671) Or (boxnum >= 731 And boxnum <= 732) Or (boxnum >= 736 And boxnum <= 740) Or (boxnum >= 742 And boxnum <= 743) Or (boxnum >= 745 And boxnum <= 749) Or (boxnum >= 753 And boxnum <= 754) Or (boxnum >= 814 And boxnum <= 815) Or (boxnum >= 817 And boxnum <= 818) Or (boxnum >= 823 And boxnum <= 825) Or (boxnum >= 828 And boxnum <= 830) Or (boxnum >= 835 And boxnum <= 836) Or (boxnum >= 838 And boxnum <= 839) Or (boxnum >= 899 And boxnum <= 908) Or (boxnum >= 910 And boxnum <= 911) Or (boxnum >= 913 And boxnum <= 922) Then
                If direction2 = "up" Then 'can't go up in these boxes
                    direction2 = ""
                End If
                If currentdirection = "up" Then
                    currentdirection = ""
                End If
            End If
            If (boxnum >= 115 And boxnum <= 118) Or (boxnum >= 120 And boxnum <= 124) Or (boxnum >= 129 And boxnum <= 133) Or (boxnum >= 135 And boxnum <= 138) Or (boxnum >= 227 And boxnum <= 230) Or (boxnum >= 232 And boxnum <= 233) Or (boxnum >= 235 And boxnum <= 242) Or (boxnum >= 244 And boxnum <= 245) Or (boxnum >= 247 And boxnum <= 250) Or (boxnum >= 310 And boxnum <= 314) Or (boxnum >= 318 And boxnum <= 320) Or (boxnum >= 325 And boxnum <= 327) Or (boxnum >= 331 And boxnum <= 335) Or (boxnum >= 403 And boxnum <= 410) Or (boxnum >= 477 And boxnum <= 482) Or (boxnum >= 484 And boxnum <= 485) Or (boxnum >= 496 And boxnum <= 497) Or (boxnum >= 499 And boxnum <= 504) Or (boxnum >= 571 And boxnum <= 578) Or (boxnum >= 647 And boxnum <= 650) Or (boxnum >= 652 And boxnum <= 656) Or (boxnum >= 661 And boxnum <= 665) Or (boxnum >= 667 And boxnum <= 670) Or (boxnum >= 730 And boxnum <= 731) Or (boxnum >= 736 And boxnum <= 737) Or (boxnum >= 739 And boxnum <= 746) Or (boxnum >= 748 And boxnum <= 749) Or (boxnum >= 754 And boxnum <= 755) Or (boxnum >= 815 And boxnum <= 819) Or (boxnum >= 822 And boxnum <= 824) Or (boxnum >= 829 And boxnum <= 831) Or (boxnum >= 834 And boxnum <= 838) Or (boxnum >= 898 And boxnum <= 923) Then
                If direction2 = "down" Then 'can't go down in these boxes
                    direction2 = ""
                End If
                If currentdirection = "down" Then
                    currentdirection = ""
                End If
            End If
            If boxnum = 142 Or boxnum = 170 Or boxnum = 198 Or boxnum = 254 Or boxnum = 282 Or boxnum = 674 Or boxnum = 702 Or boxnum = 842 Or boxnum = 870 Or boxnum = 732 Or boxnum = 760 Or boxnum = 788 Or boxnum = 147 Or boxnum = 175 Or boxnum = 203 Or boxnum = 259 Or boxnum = 287 Or boxnum = 315 Or boxnum = 343 Or boxnum = 371 Or boxnum = 399 Or boxnum = 427 Or boxnum = 455 Or boxnum = 511 Or boxnum = 539 Or boxnum = 567 Or boxnum = 595 Or boxnum = 623 Or boxnum = 679 Or boxnum = 707 Or boxnum = 763 Or boxnum = 791 Or boxnum = 819 Or boxnum = 262 Or boxnum = 290 Or boxnum = 430 Or boxnum = 458 Or boxnum = 486 Or boxnum = 514 Or boxnum = 542 Or boxnum = 598 Or boxnum = 626 Or boxnum = 766 Or boxnum = 794 Or boxnum = 125 Or boxnum = 153 Or boxnum = 181 Or boxnum = 209 Or boxnum = 321 Or boxnum = 349 Or boxnum = 377 Or boxnum = 657 Or boxnum = 685 Or boxnum = 713 Or boxnum = 825 Or boxnum = 853 Or boxnum = 881 Or boxnum = 156 Or boxnum = 184 Or boxnum = 212 Or boxnum = 352 Or boxnum = 380 Or boxnum = 688 Or boxnum = 716 Or boxnum = 856 Or boxnum = 884 Or boxnum = 271 Or boxnum = 299 Or boxnum = 327 Or boxnum = 411 Or boxnum = 439 Or boxnum = 467 Or boxnum = 523 Or boxnum = 551 Or boxnum = 579 Or boxnum = 607 Or boxnum = 635 Or boxnum = 775 Or boxnum = 803 Or boxnum = 831 Or boxnum = 139 Or boxnum = 167 Or boxnum = 195 Or boxnum = 223 Or boxnum = 251 Or boxnum = 279 Or boxnum = 307 Or boxnum = 335 Or boxnum = 671 Or boxnum = 699 Or boxnum = 727 Or boxnum = 755 Or boxnum = 839 Or boxnum = 867 Or boxnum = 895 Or boxnum = 923 Or boxnum = 781 Or boxnum = 809 Or boxnum = 162 Or boxnum = 190 Or boxnum = 218 Or boxnum = 274 Or boxnum = 302 Or boxnum = 358 Or boxnum = 386 Or boxnum = 414 Or boxnum = 442 Or boxnum = 470 Or boxnum = 526 Or boxnum = 554 Or boxnum = 582 Or boxnum = 610 Or boxnum = 638 Or boxnum = 694 Or boxnum = 722 Or boxnum = 750 Or boxnum = 778 Or boxnum = 806 Then
                If direction2 = "right" Then 'can't go right in these boxes
                    direction2 = ""
                End If
                If currentdirection = "right" Then
                    currentdirection = ""
                End If
            End If
            If boxnum = 114 Or boxnum = 142 Or boxnum = 170 Or boxnum = 198 Or boxnum = 226 Or boxnum = 254 Or boxnum = 282 Or boxnum = 310 Or boxnum = 646 Or boxnum = 674 Or boxnum = 702 Or boxnum = 730 Or boxnum = 814 Or boxnum = 842 Or boxnum = 870 Or boxnum = 898 Or boxnum = 760 Or boxnum = 788 Or boxnum = 147 Or boxnum = 175 Or boxnum = 203 Or boxnum = 259 Or boxnum = 287 Or boxnum = 343 Or boxnum = 371 Or boxnum = 399 Or boxnum = 427 Or boxnum = 455 Or boxnum = 511 Or boxnum = 539 Or boxnum = 567 Or boxnum = 595 Or boxnum = 623 Or boxnum = 679 Or boxnum = 707 Or boxnum = 735 Or boxnum = 763 Or boxnum = 791 Or boxnum = 262 Or boxnum = 290 Or boxnum = 318 Or boxnum = 402 Or boxnum = 430 Or boxnum = 458 Or boxnum = 514 Or boxnum = 542 Or boxnum = 570 Or boxnum = 598 Or boxnum = 626 Or boxnum = 766 Or boxnum = 794 Or boxnum = 822 Or boxnum = 153 Or boxnum = 181 Or boxnum = 209 Or boxnum = 349 Or boxnum = 377 Or boxnum = 685 Or boxnum = 713 Or boxnum = 853 Or boxnum = 881 Or boxnum = 128 Or boxnum = 156 Or boxnum = 184 Or boxnum = 212 Or boxnum = 324 Or boxnum = 352 Or boxnum = 380 Or boxnum = 660 Or boxnum = 688 Or boxnum = 716 Or boxnum = 828 Or boxnum = 856 Or boxnum = 884 Or boxnum = 271 Or boxnum = 299 Or boxnum = 439 Or boxnum = 467 Or boxnum = 495 Or boxnum = 523 Or boxnum = 551 Or boxnum = 607 Or boxnum = 635 Or boxnum = 775 Or boxnum = 803 Or boxnum = 162 Or boxnum = 190 Or boxnum = 218 Or boxnum = 274 Or boxnum = 302 Or boxnum = 330 Or boxnum = 358 Or boxnum = 386 Or boxnum = 414 Or boxnum = 442 Or boxnum = 470 Or boxnum = 526 Or boxnum = 554 Or boxnum = 582 Or boxnum = 610 Or boxnum = 638 Or boxnum = 694 Or boxnum = 722 Or boxnum = 778 Or boxnum = 806 Or boxnum = 834 Or boxnum = 753 Or boxnum = 781 Or boxnum = 809 Or boxnum = 167 Or boxnum = 195 Or boxnum = 223 Or boxnum = 279 Or boxnum = 307 Or boxnum = 699 Or boxnum = 727 Or boxnum = 867 Or boxnum = 895 Then
                If direction2 = "left" Then 'can't go left in these boxes
                    direction2 = ""
                End If
                If currentdirection = "left" Then
                    currentdirection = ""
                End If
            End If
            If direction2 = "" And currentdirection = "" Then
                direction = ""
            ElseIf direction2 = "" And currentdirection <> "" Then
                direction = currentdirection
            ElseIf direction2 <> "" Then
                direction = direction2
            End If
        End If
        'movement based on direction
        If direction = "up" Then
            ycounter -= 5
            pacman.Offset(0, -5)
            currentdirection = "up"
        ElseIf direction = "down" Then
            ycounter += 5
            pacman.Offset(0, 5)
            currentdirection = "down"
        ElseIf direction = "left" Then
            If boxnum = 477 Then
                pacman.X = 555
                x = 28
            Else
                xcounter -= 5
                pacman.Offset(-5, 0)
                currentdirection = "left"
            End If
        ElseIf direction = "right" Then
            If boxnum = 504 Then
                pacman.X = 15
                x = 1
            Else
                xcounter += 5
                pacman.Offset(5, 0)
                currentdirection = "right"
            End If
        End If

        'counts to the edge of a box and changes x and y when necessary
        If xcounter = 20 Then
            x += 1
            xcounter = 0
        End If
        If xcounter = -20 Then
            x -= 1
            xcounter = 0
        End If
        If ycounter = 20 Then
            y += 1
            ycounter = 0
        End If
        If ycounter = -20 Then
            y -= 1
            ycounter = 0
        End If

        For i As Integer = 1 To 246 'deletes a dot if pacman touches it
            If pacman.IntersectsWith(dot(i)) Then
                dot(i) = Nothing 'delete the dot
                score += 10
                dotseatencounter += 1 'doesn't count powerdots cause theres a small dot hidden under each powerdot
            End If
        Next
        For i As Integer = 1 To 4 'works with powerdots
            If pacman.IntersectsWith(powerdot(i)) Then
                score += 50
                ghostseatencounter = 2
                powerdot(i) = Nothing 'delete the powerdot
                If blinkymode <> "eyes" Then 'put ghosts into frightened mode unless they're already eyes
                    blinkymode = "frightened"
                    blinkymove.Interval = 60 'slow them down
                End If
                If pinkymode <> "eyes" Then
                    pinkymode = "frightened"
                    pinkymove.Interval = 60
                End If
                If inkymode <> "eyes" Then
                    inkymode = "frightened"
                    inkymove.Interval = 60
                End If
                If suemode <> "eyes" Then
                    suemode = "frightened"
                    suemove.Interval = 60
                End If
                runtimer.Stop()
                runtimer.Start()
                scatter.Stop()
                runcounter = 0
                blinkyflash = False
                pinkyflash = False
                inkyflash = False
                sueflash = False
            End If
        Next
        If pacman.IntersectsWith(fruit) And fruitpresent = True Then
            fruitpresent = False
        End If
        If dotseatencounter = 246 Then 'if all the dots were eaten, end game
            pacmanmove.Stop()
            pacmanstagesadjust.Stop()
            blinkymove.Stop()
            pinkymove.Stop()
            inkymove.Stop()
            suemove.Stop()
            starting.Stop()
            ending.Start()
        End If
    End Sub
    Private Sub pacmanstagesadjust_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pacmanstagesadjust.Tick
        If updown = True Then 'deals with the opening and closing of pacman's mouth
            pacstage += 1
        ElseIf updown = False Then
            pacstage -= 1
        End If
        If pacstage = 7 Or pacstage = -7 Then
            updown = Not updown
        End If
    End Sub
    Private Sub blinkymove_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blinkymove.Tick
        Dim blinkydistances(3) As Double
        For i As Integer = 0 To 3
            blinkydistances(i) = 999999999999999
        Next
        blinkyboxnum = blinkyY * 28 + blinkyX
        blinkynotalloweddirections = ""
        'makes sure that blinky cannot reverse direction at wall
        oldblinkydirection = blinkydirection
        If oldblinkydirection = "up" Then
            blinkynotalloweddirections = blinkynotalloweddirections & "down"
        End If
        If oldblinkydirection = "down" Then
            blinkynotalloweddirections = blinkynotalloweddirections & "up"
        End If
        If oldblinkydirection = "left" Then
            blinkynotalloweddirections = blinkynotalloweddirections & "right"
        End If
        If oldblinkydirection = "right" Then
            blinkynotalloweddirections = blinkynotalloweddirections & "left"
        End If
        'determines where he can go based on block he's in
        If (blinkyboxnum >= 114 And blinkyboxnum <= 126) Or (blinkyboxnum >= 128 And blinkyboxnum <= 140) Or (blinkyboxnum >= 227 And blinkyboxnum <= 230) Or (blinkyboxnum >= 232 And blinkyboxnum <= 236) Or (blinkyboxnum >= 238 And blinkyboxnum <= 239) Or (blinkyboxnum >= 241 And blinkyboxnum <= 245) Or (blinkyboxnum >= 247 And blinkyboxnum <= 251) Or (blinkyboxnum >= 311 And blinkyboxnum <= 314) Or (blinkyboxnum >= 319 And blinkyboxnum <= 321) Or (blinkyboxnum >= 324 And blinkyboxnum <= 326) Or (blinkyboxnum >= 331 And blinkyboxnum <= 334) Or (blinkyboxnum >= 402 And blinkyboxnum <= 411) Or (blinkyboxnum >= 477 And blinkyboxnum <= 482) Or (blinkyboxnum >= 484 And blinkyboxnum <= 485) Or (blinkyboxnum >= 496 And blinkyboxnum <= 497) Or (blinkyboxnum >= 499 And blinkyboxnum <= 504) Or (blinkyboxnum >= 571 And blinkyboxnum <= 578) Or (blinkyboxnum >= 646 And blinkyboxnum <= 650) Or (blinkyboxnum >= 652 And blinkyboxnum <= 653) Or (blinkyboxnum >= 655 And blinkyboxnum <= 657) Or (blinkyboxnum >= 660 And blinkyboxnum <= 662) Or (blinkyboxnum >= 664 And blinkyboxnum <= 665) Or (blinkyboxnum >= 667 And blinkyboxnum <= 671) Or (blinkyboxnum >= 731 And blinkyboxnum <= 732) Or (blinkyboxnum >= 736 And blinkyboxnum <= 749) Or (blinkyboxnum >= 753 And blinkyboxnum <= 754) Or (blinkyboxnum >= 814 And blinkyboxnum <= 815) Or (blinkyboxnum >= 817 And blinkyboxnum <= 818) Or (blinkyboxnum >= 823 And blinkyboxnum <= 825) Or (blinkyboxnum >= 828 And blinkyboxnum <= 830) Or (blinkyboxnum >= 835 And blinkyboxnum <= 836) Or (blinkyboxnum >= 838 And blinkyboxnum <= 839) Or (blinkyboxnum >= 899 And blinkyboxnum <= 908) Or (blinkyboxnum >= 910 And blinkyboxnum <= 911) Or (blinkyboxnum >= 913 And blinkyboxnum <= 922) Then
            blinkynotalloweddirections = blinkynotalloweddirections & "up "
        End If
        If (blinkyboxnum >= 115 And blinkyboxnum <= 118) Or (blinkyboxnum >= 120 And blinkyboxnum <= 124) Or (blinkyboxnum >= 129 And blinkyboxnum <= 133) Or (blinkyboxnum >= 135 And blinkyboxnum <= 138) Or (blinkyboxnum >= 227 And blinkyboxnum <= 230) Or (blinkyboxnum >= 232 And blinkyboxnum <= 233) Or (blinkyboxnum >= 235 And blinkyboxnum <= 242) Or (blinkyboxnum >= 244 And blinkyboxnum <= 245) Or (blinkyboxnum >= 247 And blinkyboxnum <= 250) Or (blinkyboxnum >= 310 And blinkyboxnum <= 314) Or (blinkyboxnum >= 318 And blinkyboxnum <= 320) Or (blinkyboxnum >= 325 And blinkyboxnum <= 327) Or (blinkyboxnum >= 331 And blinkyboxnum <= 335) Or (blinkyboxnum >= 403 And blinkyboxnum <= 410) Or (blinkyboxnum >= 477 And blinkyboxnum <= 482) Or (blinkyboxnum >= 484 And blinkyboxnum <= 485) Or (blinkyboxnum >= 496 And blinkyboxnum <= 497) Or (blinkyboxnum >= 499 And blinkyboxnum <= 504) Or (blinkyboxnum >= 571 And blinkyboxnum <= 578) Or (blinkyboxnum >= 647 And blinkyboxnum <= 650) Or (blinkyboxnum >= 652 And blinkyboxnum <= 656) Or (blinkyboxnum >= 661 And blinkyboxnum <= 665) Or (blinkyboxnum >= 667 And blinkyboxnum <= 670) Or (blinkyboxnum >= 730 And blinkyboxnum <= 731) Or (blinkyboxnum >= 736 And blinkyboxnum <= 737) Or (blinkyboxnum >= 739 And blinkyboxnum <= 746) Or (blinkyboxnum >= 748 And blinkyboxnum <= 749) Or (blinkyboxnum >= 754 And blinkyboxnum <= 755) Or (blinkyboxnum >= 815 And blinkyboxnum <= 819) Or (blinkyboxnum >= 822 And blinkyboxnum <= 824) Or (blinkyboxnum >= 829 And blinkyboxnum <= 831) Or (blinkyboxnum >= 834 And blinkyboxnum <= 838) Or (blinkyboxnum >= 898 And blinkyboxnum <= 923) Then
            blinkynotalloweddirections = blinkynotalloweddirections & "down "
        End If
        If blinkyboxnum = 142 Or blinkyboxnum = 170 Or blinkyboxnum = 198 Or blinkyboxnum = 254 Or blinkyboxnum = 282 Or blinkyboxnum = 674 Or blinkyboxnum = 702 Or blinkyboxnum = 842 Or blinkyboxnum = 870 Or blinkyboxnum = 732 Or blinkyboxnum = 760 Or blinkyboxnum = 788 Or blinkyboxnum = 147 Or blinkyboxnum = 175 Or blinkyboxnum = 203 Or blinkyboxnum = 259 Or blinkyboxnum = 287 Or blinkyboxnum = 315 Or blinkyboxnum = 343 Or blinkyboxnum = 371 Or blinkyboxnum = 399 Or blinkyboxnum = 427 Or blinkyboxnum = 455 Or blinkyboxnum = 511 Or blinkyboxnum = 539 Or blinkyboxnum = 567 Or blinkyboxnum = 595 Or blinkyboxnum = 623 Or blinkyboxnum = 679 Or blinkyboxnum = 707 Or blinkyboxnum = 763 Or blinkyboxnum = 791 Or blinkyboxnum = 819 Or blinkyboxnum = 262 Or blinkyboxnum = 290 Or blinkyboxnum = 430 Or blinkyboxnum = 458 Or blinkyboxnum = 486 Or blinkyboxnum = 514 Or blinkyboxnum = 542 Or blinkyboxnum = 598 Or blinkyboxnum = 626 Or blinkyboxnum = 766 Or blinkyboxnum = 794 Or blinkyboxnum = 125 Or blinkyboxnum = 153 Or blinkyboxnum = 181 Or blinkyboxnum = 209 Or blinkyboxnum = 321 Or blinkyboxnum = 349 Or blinkyboxnum = 377 Or blinkyboxnum = 657 Or blinkyboxnum = 685 Or blinkyboxnum = 713 Or blinkyboxnum = 825 Or blinkyboxnum = 853 Or blinkyboxnum = 881 Or blinkyboxnum = 156 Or blinkyboxnum = 184 Or blinkyboxnum = 212 Or blinkyboxnum = 352 Or blinkyboxnum = 380 Or blinkyboxnum = 688 Or blinkyboxnum = 716 Or blinkyboxnum = 856 Or blinkyboxnum = 884 Or blinkyboxnum = 271 Or blinkyboxnum = 299 Or blinkyboxnum = 327 Or blinkyboxnum = 411 Or blinkyboxnum = 439 Or blinkyboxnum = 467 Or blinkyboxnum = 523 Or blinkyboxnum = 551 Or blinkyboxnum = 579 Or blinkyboxnum = 607 Or blinkyboxnum = 635 Or blinkyboxnum = 775 Or blinkyboxnum = 803 Or blinkyboxnum = 831 Or blinkyboxnum = 139 Or blinkyboxnum = 167 Or blinkyboxnum = 195 Or blinkyboxnum = 223 Or blinkyboxnum = 251 Or blinkyboxnum = 279 Or blinkyboxnum = 307 Or blinkyboxnum = 335 Or blinkyboxnum = 671 Or blinkyboxnum = 699 Or blinkyboxnum = 727 Or blinkyboxnum = 755 Or blinkyboxnum = 839 Or blinkyboxnum = 867 Or blinkyboxnum = 895 Or blinkyboxnum = 923 Or blinkyboxnum = 781 Or blinkyboxnum = 809 Or blinkyboxnum = 162 Or blinkyboxnum = 190 Or blinkyboxnum = 218 Or blinkyboxnum = 274 Or blinkyboxnum = 302 Or blinkyboxnum = 358 Or blinkyboxnum = 386 Or blinkyboxnum = 414 Or blinkyboxnum = 442 Or blinkyboxnum = 470 Or blinkyboxnum = 526 Or blinkyboxnum = 554 Or blinkyboxnum = 582 Or blinkyboxnum = 610 Or blinkyboxnum = 638 Or blinkyboxnum = 694 Or blinkyboxnum = 722 Or blinkyboxnum = 750 Or blinkyboxnum = 778 Or blinkyboxnum = 806 Then
            blinkynotalloweddirections = blinkynotalloweddirections & "right "
        End If
        If blinkyboxnum = 114 Or blinkyboxnum = 142 Or blinkyboxnum = 170 Or blinkyboxnum = 198 Or blinkyboxnum = 226 Or blinkyboxnum = 254 Or blinkyboxnum = 282 Or blinkyboxnum = 310 Or blinkyboxnum = 646 Or blinkyboxnum = 674 Or blinkyboxnum = 702 Or blinkyboxnum = 730 Or blinkyboxnum = 814 Or blinkyboxnum = 842 Or blinkyboxnum = 870 Or blinkyboxnum = 898 Or blinkyboxnum = 760 Or blinkyboxnum = 788 Or blinkyboxnum = 147 Or blinkyboxnum = 175 Or blinkyboxnum = 203 Or blinkyboxnum = 259 Or blinkyboxnum = 287 Or blinkyboxnum = 343 Or blinkyboxnum = 371 Or blinkyboxnum = 399 Or blinkyboxnum = 427 Or blinkyboxnum = 455 Or blinkyboxnum = 511 Or blinkyboxnum = 539 Or blinkyboxnum = 567 Or blinkyboxnum = 595 Or blinkyboxnum = 623 Or blinkyboxnum = 679 Or blinkyboxnum = 707 Or blinkyboxnum = 735 Or blinkyboxnum = 763 Or blinkyboxnum = 791 Or blinkyboxnum = 262 Or blinkyboxnum = 290 Or blinkyboxnum = 318 Or blinkyboxnum = 402 Or blinkyboxnum = 430 Or blinkyboxnum = 458 Or blinkyboxnum = 514 Or blinkyboxnum = 542 Or blinkyboxnum = 570 Or blinkyboxnum = 598 Or blinkyboxnum = 626 Or blinkyboxnum = 766 Or blinkyboxnum = 794 Or blinkyboxnum = 822 Or blinkyboxnum = 153 Or blinkyboxnum = 181 Or blinkyboxnum = 209 Or blinkyboxnum = 349 Or blinkyboxnum = 377 Or blinkyboxnum = 685 Or blinkyboxnum = 713 Or blinkyboxnum = 853 Or blinkyboxnum = 881 Or blinkyboxnum = 128 Or blinkyboxnum = 156 Or blinkyboxnum = 184 Or blinkyboxnum = 212 Or blinkyboxnum = 324 Or blinkyboxnum = 352 Or blinkyboxnum = 380 Or blinkyboxnum = 660 Or blinkyboxnum = 688 Or blinkyboxnum = 716 Or blinkyboxnum = 828 Or blinkyboxnum = 856 Or blinkyboxnum = 884 Or blinkyboxnum = 271 Or blinkyboxnum = 299 Or blinkyboxnum = 439 Or blinkyboxnum = 467 Or blinkyboxnum = 495 Or blinkyboxnum = 523 Or blinkyboxnum = 551 Or blinkyboxnum = 607 Or blinkyboxnum = 635 Or blinkyboxnum = 775 Or blinkyboxnum = 803 Or blinkyboxnum = 162 Or blinkyboxnum = 190 Or blinkyboxnum = 218 Or blinkyboxnum = 274 Or blinkyboxnum = 302 Or blinkyboxnum = 330 Or blinkyboxnum = 358 Or blinkyboxnum = 386 Or blinkyboxnum = 414 Or blinkyboxnum = 442 Or blinkyboxnum = 470 Or blinkyboxnum = 526 Or blinkyboxnum = 554 Or blinkyboxnum = 582 Or blinkyboxnum = 610 Or blinkyboxnum = 638 Or blinkyboxnum = 694 Or blinkyboxnum = 722 Or blinkyboxnum = 778 Or blinkyboxnum = 806 Or blinkyboxnum = 834 Or blinkyboxnum = 753 Or blinkyboxnum = 781 Or blinkyboxnum = 809 Or blinkyboxnum = 167 Or blinkyboxnum = 195 Or blinkyboxnum = 223 Or blinkyboxnum = 279 Or blinkyboxnum = 307 Or blinkyboxnum = 699 Or blinkyboxnum = 727 Or blinkyboxnum = 867 Or blinkyboxnum = 895 Then
            blinkynotalloweddirections = blinkynotalloweddirections & "left "
        End If
        'determines if blinky can turn (is perfectly lined up to turn)
        blinkyitwillwork = False
        For i As Integer = 0 To 35
            For t As Integer = 0 To 35
                If (blinky.X + 5) / 20 = i And (blinky.Y + 5) / 20 = t Then
                    blinkyitwillwork = True
                End If
            Next
        Next
        'targeting
        If blinkymode = "chase" Then
            blinkytargetx = x
            blinkytargety = y
        ElseIf blinkymode = "scatter" Then
            blinkytargetx = 27
            blinkytargety = 0
        ElseIf blinkymode = "eyes" Then
            blinkytargetx = 14
            blinkytargety = 14
        End If
        'if he not frightened and can turn then...
        If blinkymode = "chase" Or blinkymode = "scatter" Or blinkymode = "eyes" Then
            If blinkyitwillwork = True Then
                If blinkynotalloweddirections.Contains("up") = False Then
                    blinkydistances(0) = Math.Sqrt((blinkyX - blinkytargetx) ^ 2 + (blinkyY - 1 - blinkytargety) ^ 2)
                End If
                If blinkynotalloweddirections.Contains("down") = False Then
                    blinkydistances(1) = Math.Sqrt((blinkyX - blinkytargetx) ^ 2 + (blinkyY + 1 - blinkytargety) ^ 2)
                End If
                If blinkynotalloweddirections.Contains("left") = False Then
                    blinkydistances(2) = Math.Sqrt((blinkyX - 1 - blinkytargetx) ^ 2 + (blinkyY - blinkytargety) ^ 2)
                End If
                If blinkynotalloweddirections.Contains("right") = False Then
                    blinkydistances(3) = Math.Sqrt((blinkyX + 1 - blinkytargetx) ^ 2 + (blinkyY - blinkytargety) ^ 2)
                End If
                If blinkydistances.Min = blinkydistances(0) Then
                    blinkydirection = "up"
                End If
                If blinkydistances.Min = blinkydistances(1) Then
                    blinkydirection = "down"
                End If
                If blinkydistances.Min = blinkydistances(2) Then
                    blinkydirection = "left"
                End If
                If blinkydistances.Min = blinkydistances(3) Then
                    blinkydirection = "right"
                End If
            End If
            If blinkymode = "eyes" And blinkyX = 14 And blinkyY = 14 Then 'reached the home box
                blinkymode = "restart"
                blinky.X = 285
                blinky.Y = 275
                blinkymove.Stop()
                blinkydirection = "down"
            End If
            'frightened mode direction selection
        ElseIf blinkymode = "frightened" Then
            If blinkyitwillwork = True Then
                blinkydirection = ""
                While blinkydirection = ""
                    Randomize()
                    blinkynewdirection = Int(Rnd() * 6 + 1)
                    If blinkynewdirection = 1 Then
                        blinkydirection = "up"
                    ElseIf blinkynewdirection = 2 Then
                        blinkydirection = "right"
                    ElseIf blinkynewdirection = 3 Then
                        blinkydirection = "down"
                    ElseIf blinkynewdirection = 4 Then
                        blinkydirection = "left"
                    End If
                    If blinkynewdirection = 5 Or blinkynewdirection = 6 Then
                        blinkydirection = oldblinkydirection
                    End If
                    'can't reverse direction at wall
                    If (oldblinkydirection = "up" And blinkydirection = "down") Or (oldblinkydirection = "down" And blinkydirection = "up") Or (oldblinkydirection = "left" And blinkydirection = "right") Or (oldblinkydirection = "right" And blinkydirection = "left") Then
                        blinkydirection = ""
                    End If
                    If (blinkyboxnum >= 114 And blinkyboxnum <= 126) Or (blinkyboxnum >= 128 And blinkyboxnum <= 140) Or (blinkyboxnum >= 227 And blinkyboxnum <= 230) Or (blinkyboxnum >= 232 And blinkyboxnum <= 236) Or (blinkyboxnum >= 238 And blinkyboxnum <= 239) Or (blinkyboxnum >= 241 And blinkyboxnum <= 245) Or (blinkyboxnum >= 247 And blinkyboxnum <= 251) Or (blinkyboxnum >= 311 And blinkyboxnum <= 314) Or (blinkyboxnum >= 319 And blinkyboxnum <= 321) Or (blinkyboxnum >= 324 And blinkyboxnum <= 326) Or (blinkyboxnum >= 331 And blinkyboxnum <= 334) Or (blinkyboxnum >= 402 And blinkyboxnum <= 411) Or (blinkyboxnum >= 477 And blinkyboxnum <= 482) Or (blinkyboxnum >= 484 And blinkyboxnum <= 485) Or (blinkyboxnum >= 496 And blinkyboxnum <= 497) Or (blinkyboxnum >= 499 And blinkyboxnum <= 504) Or (blinkyboxnum >= 571 And blinkyboxnum <= 578) Or (blinkyboxnum >= 646 And blinkyboxnum <= 650) Or (blinkyboxnum >= 652 And blinkyboxnum <= 653) Or (blinkyboxnum >= 655 And blinkyboxnum <= 657) Or (blinkyboxnum >= 660 And blinkyboxnum <= 662) Or (blinkyboxnum >= 664 And blinkyboxnum <= 665) Or (blinkyboxnum >= 667 And blinkyboxnum <= 671) Or (blinkyboxnum >= 731 And blinkyboxnum <= 732) Or (blinkyboxnum >= 736 And blinkyboxnum <= 749) Or (blinkyboxnum >= 753 And blinkyboxnum <= 754) Or (blinkyboxnum >= 814 And blinkyboxnum <= 815) Or (blinkyboxnum >= 817 And blinkyboxnum <= 818) Or (blinkyboxnum >= 823 And blinkyboxnum <= 825) Or (blinkyboxnum >= 828 And blinkyboxnum <= 830) Or (blinkyboxnum >= 835 And blinkyboxnum <= 836) Or (blinkyboxnum >= 838 And blinkyboxnum <= 839) Or (blinkyboxnum >= 899 And blinkyboxnum <= 908) Or (blinkyboxnum >= 910 And blinkyboxnum <= 911) Or (blinkyboxnum >= 913 And blinkyboxnum <= 922) Then
                        If blinkydirection = "up" Then
                            blinkydirection = ""
                        End If
                    End If
                    If (blinkyboxnum >= 115 And blinkyboxnum <= 118) Or (blinkyboxnum >= 120 And blinkyboxnum <= 124) Or (blinkyboxnum >= 129 And blinkyboxnum <= 133) Or (blinkyboxnum >= 135 And blinkyboxnum <= 138) Or (blinkyboxnum >= 227 And blinkyboxnum <= 230) Or (blinkyboxnum >= 232 And blinkyboxnum <= 233) Or (blinkyboxnum >= 235 And blinkyboxnum <= 242) Or (blinkyboxnum >= 244 And blinkyboxnum <= 245) Or (blinkyboxnum >= 247 And blinkyboxnum <= 250) Or (blinkyboxnum >= 310 And blinkyboxnum <= 314) Or (blinkyboxnum >= 318 And blinkyboxnum <= 320) Or (blinkyboxnum >= 325 And blinkyboxnum <= 327) Or (blinkyboxnum >= 331 And blinkyboxnum <= 335) Or (blinkyboxnum >= 403 And blinkyboxnum <= 410) Or (blinkyboxnum >= 477 And blinkyboxnum <= 482) Or (blinkyboxnum >= 484 And blinkyboxnum <= 485) Or (blinkyboxnum >= 496 And blinkyboxnum <= 497) Or (blinkyboxnum >= 499 And blinkyboxnum <= 504) Or (blinkyboxnum >= 571 And blinkyboxnum <= 578) Or (blinkyboxnum >= 647 And blinkyboxnum <= 650) Or (blinkyboxnum >= 652 And blinkyboxnum <= 656) Or (blinkyboxnum >= 661 And blinkyboxnum <= 665) Or (blinkyboxnum >= 667 And blinkyboxnum <= 670) Or (blinkyboxnum >= 730 And blinkyboxnum <= 731) Or (blinkyboxnum >= 736 And blinkyboxnum <= 737) Or (blinkyboxnum >= 739 And blinkyboxnum <= 746) Or (blinkyboxnum >= 748 And blinkyboxnum <= 749) Or (blinkyboxnum >= 754 And blinkyboxnum <= 755) Or (blinkyboxnum >= 815 And blinkyboxnum <= 819) Or (blinkyboxnum >= 822 And blinkyboxnum <= 824) Or (blinkyboxnum >= 829 And blinkyboxnum <= 831) Or (blinkyboxnum >= 834 And blinkyboxnum <= 838) Or (blinkyboxnum >= 898 And blinkyboxnum <= 923) Then
                        If blinkydirection = "down" Then
                            blinkydirection = ""
                        End If
                    End If
                    If blinkyboxnum = 142 Or blinkyboxnum = 170 Or blinkyboxnum = 198 Or blinkyboxnum = 254 Or blinkyboxnum = 282 Or blinkyboxnum = 674 Or blinkyboxnum = 702 Or blinkyboxnum = 842 Or blinkyboxnum = 870 Or blinkyboxnum = 732 Or blinkyboxnum = 760 Or blinkyboxnum = 788 Or blinkyboxnum = 147 Or blinkyboxnum = 175 Or blinkyboxnum = 203 Or blinkyboxnum = 259 Or blinkyboxnum = 287 Or blinkyboxnum = 315 Or blinkyboxnum = 343 Or blinkyboxnum = 371 Or blinkyboxnum = 399 Or blinkyboxnum = 427 Or blinkyboxnum = 455 Or blinkyboxnum = 511 Or blinkyboxnum = 539 Or blinkyboxnum = 567 Or blinkyboxnum = 595 Or blinkyboxnum = 623 Or blinkyboxnum = 679 Or blinkyboxnum = 707 Or blinkyboxnum = 763 Or blinkyboxnum = 791 Or blinkyboxnum = 819 Or blinkyboxnum = 262 Or blinkyboxnum = 290 Or blinkyboxnum = 430 Or blinkyboxnum = 458 Or blinkyboxnum = 486 Or blinkyboxnum = 514 Or blinkyboxnum = 542 Or blinkyboxnum = 598 Or blinkyboxnum = 626 Or blinkyboxnum = 766 Or blinkyboxnum = 794 Or blinkyboxnum = 125 Or blinkyboxnum = 153 Or blinkyboxnum = 181 Or blinkyboxnum = 209 Or blinkyboxnum = 321 Or blinkyboxnum = 349 Or blinkyboxnum = 377 Or blinkyboxnum = 657 Or blinkyboxnum = 685 Or blinkyboxnum = 713 Or blinkyboxnum = 825 Or blinkyboxnum = 853 Or blinkyboxnum = 881 Or blinkyboxnum = 156 Or blinkyboxnum = 184 Or blinkyboxnum = 212 Or blinkyboxnum = 352 Or blinkyboxnum = 380 Or blinkyboxnum = 688 Or blinkyboxnum = 716 Or blinkyboxnum = 856 Or blinkyboxnum = 884 Or blinkyboxnum = 271 Or blinkyboxnum = 299 Or blinkyboxnum = 327 Or blinkyboxnum = 411 Or blinkyboxnum = 439 Or blinkyboxnum = 467 Or blinkyboxnum = 523 Or blinkyboxnum = 551 Or blinkyboxnum = 579 Or blinkyboxnum = 607 Or blinkyboxnum = 635 Or blinkyboxnum = 775 Or blinkyboxnum = 803 Or blinkyboxnum = 831 Or blinkyboxnum = 139 Or blinkyboxnum = 167 Or blinkyboxnum = 195 Or blinkyboxnum = 223 Or blinkyboxnum = 251 Or blinkyboxnum = 279 Or blinkyboxnum = 307 Or blinkyboxnum = 335 Or blinkyboxnum = 671 Or blinkyboxnum = 699 Or blinkyboxnum = 727 Or blinkyboxnum = 755 Or blinkyboxnum = 839 Or blinkyboxnum = 867 Or blinkyboxnum = 895 Or blinkyboxnum = 923 Or blinkyboxnum = 781 Or blinkyboxnum = 809 Or blinkyboxnum = 162 Or blinkyboxnum = 190 Or blinkyboxnum = 218 Or blinkyboxnum = 274 Or blinkyboxnum = 302 Or blinkyboxnum = 358 Or blinkyboxnum = 386 Or blinkyboxnum = 414 Or blinkyboxnum = 442 Or blinkyboxnum = 470 Or blinkyboxnum = 526 Or blinkyboxnum = 554 Or blinkyboxnum = 582 Or blinkyboxnum = 610 Or blinkyboxnum = 638 Or blinkyboxnum = 694 Or blinkyboxnum = 722 Or blinkyboxnum = 750 Or blinkyboxnum = 778 Or blinkyboxnum = 806 Then
                        If blinkydirection = "right" Then
                            blinkydirection = ""
                        End If
                    End If
                    If blinkyboxnum = 114 Or blinkyboxnum = 142 Or blinkyboxnum = 170 Or blinkyboxnum = 198 Or blinkyboxnum = 226 Or blinkyboxnum = 254 Or blinkyboxnum = 282 Or blinkyboxnum = 310 Or blinkyboxnum = 646 Or blinkyboxnum = 674 Or blinkyboxnum = 702 Or blinkyboxnum = 730 Or blinkyboxnum = 814 Or blinkyboxnum = 842 Or blinkyboxnum = 870 Or blinkyboxnum = 898 Or blinkyboxnum = 760 Or blinkyboxnum = 788 Or blinkyboxnum = 147 Or blinkyboxnum = 175 Or blinkyboxnum = 203 Or blinkyboxnum = 252 Or blinkyboxnum = 287 Or blinkyboxnum = 343 Or blinkyboxnum = 371 Or blinkyboxnum = 399 Or blinkyboxnum = 427 Or blinkyboxnum = 455 Or blinkyboxnum = 511 Or blinkyboxnum = 539 Or blinkyboxnum = 567 Or blinkyboxnum = 595 Or blinkyboxnum = 623 Or blinkyboxnum = 679 Or blinkyboxnum = 707 Or blinkyboxnum = 735 Or blinkyboxnum = 763 Or blinkyboxnum = 791 Or blinkyboxnum = 262 Or blinkyboxnum = 290 Or blinkyboxnum = 318 Or blinkyboxnum = 402 Or blinkyboxnum = 430 Or blinkyboxnum = 458 Or blinkyboxnum = 514 Or blinkyboxnum = 542 Or blinkyboxnum = 570 Or blinkyboxnum = 598 Or blinkyboxnum = 626 Or blinkyboxnum = 766 Or blinkyboxnum = 794 Or blinkyboxnum = 822 Or blinkyboxnum = 153 Or blinkyboxnum = 181 Or blinkyboxnum = 209 Or blinkyboxnum = 349 Or blinkyboxnum = 377 Or blinkyboxnum = 685 Or blinkyboxnum = 713 Or blinkyboxnum = 853 Or blinkyboxnum = 881 Or blinkyboxnum = 128 Or blinkyboxnum = 156 Or blinkyboxnum = 184 Or blinkyboxnum = 212 Or blinkyboxnum = 324 Or blinkyboxnum = 352 Or blinkyboxnum = 380 Or blinkyboxnum = 660 Or blinkyboxnum = 688 Or blinkyboxnum = 716 Or blinkyboxnum = 828 Or blinkyboxnum = 856 Or blinkyboxnum = 884 Or blinkyboxnum = 271 Or blinkyboxnum = 299 Or blinkyboxnum = 439 Or blinkyboxnum = 467 Or blinkyboxnum = 495 Or blinkyboxnum = 523 Or blinkyboxnum = 551 Or blinkyboxnum = 607 Or blinkyboxnum = 635 Or blinkyboxnum = 775 Or blinkyboxnum = 803 Or blinkyboxnum = 162 Or blinkyboxnum = 190 Or blinkyboxnum = 218 Or blinkyboxnum = 274 Or blinkyboxnum = 302 Or blinkyboxnum = 330 Or blinkyboxnum = 358 Or blinkyboxnum = 386 Or blinkyboxnum = 414 Or blinkyboxnum = 442 Or blinkyboxnum = 470 Or blinkyboxnum = 526 Or blinkyboxnum = 554 Or blinkyboxnum = 582 Or blinkyboxnum = 610 Or blinkyboxnum = 638 Or blinkyboxnum = 694 Or blinkyboxnum = 722 Or blinkyboxnum = 778 Or blinkyboxnum = 806 Or blinkyboxnum = 834 Or blinkyboxnum = 753 Or blinkyboxnum = 781 Or blinkyboxnum = 809 Or blinkyboxnum = 167 Or blinkyboxnum = 195 Or blinkyboxnum = 223 Or blinkyboxnum = 279 Or blinkyboxnum = 307 Or blinkyboxnum = 699 Or blinkyboxnum = 727 Or blinkyboxnum = 867 Or blinkyboxnum = 895 Then
                        If blinkydirection = "left" Then
                            blinkydirection = ""
                        End If
                    End If
                End While
            End If
        End If
        'movement control
        If blinkydirection = "up" Then
            blinkyycounter -= 5
            blinky.Offset(0, -5)
        ElseIf blinkydirection = "down" Then
            blinkyycounter += 5
            blinky.Offset(0, 5)
        ElseIf blinkydirection = "left" Then
            If blinkyboxnum = 477 Then
                blinky.X = 555
                blinkyX = 28
            Else
                blinkyxcounter -= 5
                blinky.Offset(-5, 0)
            End If
        ElseIf blinkydirection = "right" Then
            If blinkyboxnum = 504 Then
                blinky.X = 15
                blinkyX = 1
            Else
                blinkyxcounter += 5
                blinky.Offset(5, 0)
            End If
        End If

        'finds edge of box
        If blinkyxcounter = 20 Then
            blinkyX += 1
            blinkyxcounter = 0
        End If
        If blinkyxcounter = -20 Then
            blinkyX -= 1
            blinkyxcounter = 0
        End If
        If blinkyycounter = 20 Then
            blinkyY += 1
            blinkyycounter = 0
        End If
        If blinkyycounter = -20 Then
            blinkyY -= 1
            blinkyycounter = 0
        End If
        blinkyboxnum = blinkyY * 28 + blinkyX
    End Sub
    Private Sub pinkymove_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pinkymove.Tick
        Dim pinkydistances(3) As Double
        For i As Integer = 0 To 3
            pinkydistances(i) = 999999999999999
        Next
        pinkyboxnum = pinkyY * 28 + pinkyX
        pinkynotalloweddirections = ""
        'makes sure that pinky cannot reverse direction at wall
        oldpinkydirection = pinkydirection
        If oldpinkydirection = "up" Then
            pinkynotalloweddirections = pinkynotalloweddirections & "down"
        End If
        If oldpinkydirection = "down" Then
            pinkynotalloweddirections = pinkynotalloweddirections & "up"
        End If
        If oldpinkydirection = "left" Then
            pinkynotalloweddirections = pinkynotalloweddirections & "right"
        End If
        If oldpinkydirection = "right" Then
            pinkynotalloweddirections = pinkynotalloweddirections & "left"
        End If
        'determines where he can go based on block he's in
        If (pinkyboxnum >= 114 And pinkyboxnum <= 126) Or (pinkyboxnum >= 128 And pinkyboxnum <= 140) Or (pinkyboxnum >= 227 And pinkyboxnum <= 230) Or (pinkyboxnum >= 232 And pinkyboxnum <= 236) Or (pinkyboxnum >= 238 And pinkyboxnum <= 239) Or (pinkyboxnum >= 241 And pinkyboxnum <= 245) Or (pinkyboxnum >= 247 And pinkyboxnum <= 251) Or (pinkyboxnum >= 311 And pinkyboxnum <= 314) Or (pinkyboxnum >= 319 And pinkyboxnum <= 321) Or (pinkyboxnum >= 324 And pinkyboxnum <= 326) Or (pinkyboxnum >= 331 And pinkyboxnum <= 334) Or (pinkyboxnum >= 402 And pinkyboxnum <= 411) Or (pinkyboxnum >= 477 And pinkyboxnum <= 482) Or (pinkyboxnum >= 484 And pinkyboxnum <= 485) Or (pinkyboxnum >= 496 And pinkyboxnum <= 497) Or (pinkyboxnum >= 499 And pinkyboxnum <= 504) Or (pinkyboxnum >= 571 And pinkyboxnum <= 578) Or (pinkyboxnum >= 646 And pinkyboxnum <= 650) Or (pinkyboxnum >= 652 And pinkyboxnum <= 653) Or (pinkyboxnum >= 655 And pinkyboxnum <= 657) Or (pinkyboxnum >= 660 And pinkyboxnum <= 662) Or (pinkyboxnum >= 664 And pinkyboxnum <= 665) Or (pinkyboxnum >= 667 And pinkyboxnum <= 671) Or (pinkyboxnum >= 731 And pinkyboxnum <= 732) Or (pinkyboxnum >= 736 And pinkyboxnum <= 749) Or (pinkyboxnum >= 753 And pinkyboxnum <= 754) Or (pinkyboxnum >= 814 And pinkyboxnum <= 815) Or (pinkyboxnum >= 817 And pinkyboxnum <= 818) Or (pinkyboxnum >= 823 And pinkyboxnum <= 825) Or (pinkyboxnum >= 828 And pinkyboxnum <= 830) Or (pinkyboxnum >= 835 And pinkyboxnum <= 836) Or (pinkyboxnum >= 838 And pinkyboxnum <= 839) Or (pinkyboxnum >= 899 And pinkyboxnum <= 908) Or (pinkyboxnum >= 910 And pinkyboxnum <= 911) Or (pinkyboxnum >= 913 And pinkyboxnum <= 922) Then
            pinkynotalloweddirections = pinkynotalloweddirections & "up "
        End If
        If (pinkyboxnum >= 115 And pinkyboxnum <= 118) Or (pinkyboxnum >= 120 And pinkyboxnum <= 124) Or (pinkyboxnum >= 129 And pinkyboxnum <= 133) Or (pinkyboxnum >= 135 And pinkyboxnum <= 138) Or (pinkyboxnum >= 227 And pinkyboxnum <= 230) Or (pinkyboxnum >= 232 And pinkyboxnum <= 233) Or (pinkyboxnum >= 235 And pinkyboxnum <= 242) Or (pinkyboxnum >= 244 And pinkyboxnum <= 245) Or (pinkyboxnum >= 247 And pinkyboxnum <= 250) Or (pinkyboxnum >= 310 And pinkyboxnum <= 314) Or (pinkyboxnum >= 318 And pinkyboxnum <= 320) Or (pinkyboxnum >= 325 And pinkyboxnum <= 327) Or (pinkyboxnum >= 331 And pinkyboxnum <= 335) Or (pinkyboxnum >= 403 And pinkyboxnum <= 410) Or (pinkyboxnum >= 477 And pinkyboxnum <= 482) Or (pinkyboxnum >= 484 And pinkyboxnum <= 485) Or (pinkyboxnum >= 496 And pinkyboxnum <= 497) Or (pinkyboxnum >= 499 And pinkyboxnum <= 504) Or (pinkyboxnum >= 571 And pinkyboxnum <= 578) Or (pinkyboxnum >= 647 And pinkyboxnum <= 650) Or (pinkyboxnum >= 652 And pinkyboxnum <= 656) Or (pinkyboxnum >= 661 And pinkyboxnum <= 665) Or (pinkyboxnum >= 667 And pinkyboxnum <= 670) Or (pinkyboxnum >= 730 And pinkyboxnum <= 731) Or (pinkyboxnum >= 736 And pinkyboxnum <= 737) Or (pinkyboxnum >= 739 And pinkyboxnum <= 746) Or (pinkyboxnum >= 748 And pinkyboxnum <= 749) Or (pinkyboxnum >= 754 And pinkyboxnum <= 755) Or (pinkyboxnum >= 815 And pinkyboxnum <= 819) Or (pinkyboxnum >= 822 And pinkyboxnum <= 824) Or (pinkyboxnum >= 829 And pinkyboxnum <= 831) Or (pinkyboxnum >= 834 And pinkyboxnum <= 838) Or (pinkyboxnum >= 898 And pinkyboxnum <= 923) Then
            pinkynotalloweddirections = pinkynotalloweddirections & "down "
        End If
        If pinkyboxnum = 142 Or pinkyboxnum = 170 Or pinkyboxnum = 198 Or pinkyboxnum = 254 Or pinkyboxnum = 282 Or pinkyboxnum = 674 Or pinkyboxnum = 702 Or pinkyboxnum = 842 Or pinkyboxnum = 870 Or pinkyboxnum = 732 Or pinkyboxnum = 760 Or pinkyboxnum = 788 Or pinkyboxnum = 147 Or pinkyboxnum = 175 Or pinkyboxnum = 203 Or pinkyboxnum = 259 Or pinkyboxnum = 287 Or pinkyboxnum = 315 Or pinkyboxnum = 343 Or pinkyboxnum = 371 Or pinkyboxnum = 399 Or pinkyboxnum = 427 Or pinkyboxnum = 455 Or pinkyboxnum = 511 Or pinkyboxnum = 539 Or pinkyboxnum = 567 Or pinkyboxnum = 595 Or pinkyboxnum = 623 Or pinkyboxnum = 679 Or pinkyboxnum = 707 Or pinkyboxnum = 763 Or pinkyboxnum = 791 Or pinkyboxnum = 819 Or pinkyboxnum = 262 Or pinkyboxnum = 290 Or pinkyboxnum = 430 Or pinkyboxnum = 458 Or pinkyboxnum = 486 Or pinkyboxnum = 514 Or pinkyboxnum = 542 Or pinkyboxnum = 598 Or pinkyboxnum = 626 Or pinkyboxnum = 766 Or pinkyboxnum = 794 Or pinkyboxnum = 125 Or pinkyboxnum = 153 Or pinkyboxnum = 181 Or pinkyboxnum = 209 Or pinkyboxnum = 321 Or pinkyboxnum = 349 Or pinkyboxnum = 377 Or pinkyboxnum = 657 Or pinkyboxnum = 685 Or pinkyboxnum = 713 Or pinkyboxnum = 825 Or pinkyboxnum = 853 Or pinkyboxnum = 881 Or pinkyboxnum = 156 Or pinkyboxnum = 184 Or pinkyboxnum = 212 Or pinkyboxnum = 352 Or pinkyboxnum = 380 Or pinkyboxnum = 688 Or pinkyboxnum = 716 Or pinkyboxnum = 856 Or pinkyboxnum = 884 Or pinkyboxnum = 271 Or pinkyboxnum = 299 Or pinkyboxnum = 327 Or pinkyboxnum = 411 Or pinkyboxnum = 439 Or pinkyboxnum = 467 Or pinkyboxnum = 523 Or pinkyboxnum = 551 Or pinkyboxnum = 579 Or pinkyboxnum = 607 Or pinkyboxnum = 635 Or pinkyboxnum = 775 Or pinkyboxnum = 803 Or pinkyboxnum = 831 Or pinkyboxnum = 139 Or pinkyboxnum = 167 Or pinkyboxnum = 195 Or pinkyboxnum = 223 Or pinkyboxnum = 251 Or pinkyboxnum = 279 Or pinkyboxnum = 307 Or pinkyboxnum = 335 Or pinkyboxnum = 671 Or pinkyboxnum = 699 Or pinkyboxnum = 727 Or pinkyboxnum = 755 Or pinkyboxnum = 839 Or pinkyboxnum = 867 Or pinkyboxnum = 895 Or pinkyboxnum = 923 Or pinkyboxnum = 781 Or pinkyboxnum = 809 Or pinkyboxnum = 162 Or pinkyboxnum = 190 Or pinkyboxnum = 218 Or pinkyboxnum = 274 Or pinkyboxnum = 302 Or pinkyboxnum = 358 Or pinkyboxnum = 386 Or pinkyboxnum = 414 Or pinkyboxnum = 442 Or pinkyboxnum = 470 Or pinkyboxnum = 526 Or pinkyboxnum = 554 Or pinkyboxnum = 582 Or pinkyboxnum = 610 Or pinkyboxnum = 638 Or pinkyboxnum = 694 Or pinkyboxnum = 722 Or pinkyboxnum = 750 Or pinkyboxnum = 778 Or pinkyboxnum = 806 Then
            pinkynotalloweddirections = pinkynotalloweddirections & "right "
        End If
        If pinkyboxnum = 114 Or pinkyboxnum = 142 Or pinkyboxnum = 170 Or pinkyboxnum = 198 Or pinkyboxnum = 226 Or pinkyboxnum = 254 Or pinkyboxnum = 282 Or pinkyboxnum = 310 Or pinkyboxnum = 646 Or pinkyboxnum = 674 Or pinkyboxnum = 702 Or pinkyboxnum = 730 Or pinkyboxnum = 814 Or pinkyboxnum = 842 Or pinkyboxnum = 870 Or pinkyboxnum = 898 Or pinkyboxnum = 760 Or pinkyboxnum = 788 Or pinkyboxnum = 147 Or pinkyboxnum = 175 Or pinkyboxnum = 203 Or pinkyboxnum = 259 Or pinkyboxnum = 287 Or pinkyboxnum = 343 Or pinkyboxnum = 371 Or pinkyboxnum = 399 Or pinkyboxnum = 427 Or pinkyboxnum = 455 Or pinkyboxnum = 511 Or pinkyboxnum = 539 Or pinkyboxnum = 567 Or pinkyboxnum = 595 Or pinkyboxnum = 623 Or pinkyboxnum = 679 Or pinkyboxnum = 707 Or pinkyboxnum = 735 Or pinkyboxnum = 763 Or pinkyboxnum = 791 Or pinkyboxnum = 262 Or pinkyboxnum = 290 Or pinkyboxnum = 318 Or pinkyboxnum = 402 Or pinkyboxnum = 430 Or pinkyboxnum = 458 Or pinkyboxnum = 514 Or pinkyboxnum = 542 Or pinkyboxnum = 570 Or pinkyboxnum = 598 Or pinkyboxnum = 626 Or pinkyboxnum = 766 Or pinkyboxnum = 794 Or pinkyboxnum = 822 Or pinkyboxnum = 153 Or pinkyboxnum = 181 Or pinkyboxnum = 209 Or pinkyboxnum = 349 Or pinkyboxnum = 377 Or pinkyboxnum = 685 Or pinkyboxnum = 713 Or pinkyboxnum = 853 Or pinkyboxnum = 881 Or pinkyboxnum = 128 Or pinkyboxnum = 156 Or pinkyboxnum = 184 Or pinkyboxnum = 212 Or pinkyboxnum = 324 Or pinkyboxnum = 352 Or pinkyboxnum = 380 Or pinkyboxnum = 660 Or pinkyboxnum = 688 Or pinkyboxnum = 716 Or pinkyboxnum = 828 Or pinkyboxnum = 856 Or pinkyboxnum = 884 Or pinkyboxnum = 271 Or pinkyboxnum = 299 Or pinkyboxnum = 439 Or pinkyboxnum = 467 Or pinkyboxnum = 495 Or pinkyboxnum = 523 Or pinkyboxnum = 551 Or pinkyboxnum = 607 Or pinkyboxnum = 635 Or pinkyboxnum = 775 Or pinkyboxnum = 803 Or pinkyboxnum = 162 Or pinkyboxnum = 190 Or pinkyboxnum = 218 Or pinkyboxnum = 274 Or pinkyboxnum = 302 Or pinkyboxnum = 330 Or pinkyboxnum = 358 Or pinkyboxnum = 386 Or pinkyboxnum = 414 Or pinkyboxnum = 442 Or pinkyboxnum = 470 Or pinkyboxnum = 526 Or pinkyboxnum = 554 Or pinkyboxnum = 582 Or pinkyboxnum = 610 Or pinkyboxnum = 638 Or pinkyboxnum = 694 Or pinkyboxnum = 722 Or pinkyboxnum = 778 Or pinkyboxnum = 806 Or pinkyboxnum = 834 Or pinkyboxnum = 753 Or pinkyboxnum = 781 Or pinkyboxnum = 809 Or pinkyboxnum = 167 Or pinkyboxnum = 195 Or pinkyboxnum = 223 Or pinkyboxnum = 279 Or pinkyboxnum = 307 Or pinkyboxnum = 699 Or pinkyboxnum = 727 Or pinkyboxnum = 867 Or pinkyboxnum = 895 Then
            pinkynotalloweddirections = pinkynotalloweddirections & "left "
        End If
        'determines if pinky can turn (is perfectly lined up to turn)
        pinkyitwillwork = False
        For i As Integer = 0 To 35
            For t As Integer = 0 To 35
                If (pinky.X + 5) / 20 = i And (pinky.Y + 5) / 20 = t Then
                    pinkyitwillwork = True
                End If
            Next
        Next
        'targeting
        If pinkymode = "chase" Then
            If currentdirection = "up" Then
                pinkytargetx = x
                pinkytargety = y - 4
            ElseIf currentdirection = "down" Then
                pinkytargetx = x
                pinkytargety = y + 4
            ElseIf currentdirection = "right" Then
                pinkytargetx = x + 4
                pinkytargety = y
            ElseIf currentdirection = "left" Then
                pinkytargetx = x - 4
                pinkytargety = y
            End If
        ElseIf pinkymode = "scatter" Then
            pinkytargetx = 3
            pinkytargety = 0
        ElseIf pinkymode = "eyes" Then
            pinkytargetx = 14
            pinkytargety = 14
        End If
        'if he not frightened and can turn then...
        If pinkymode = "chase" Or pinkymode = "scatter" Or pinkymode = "eyes" Then
            If pinkyitwillwork = True Then
                If pinkynotalloweddirections.Contains("up") = False Then
                    pinkydistances(0) = Math.Sqrt((pinkyX - pinkytargetx) ^ 2 + (pinkyY - 1 - pinkytargety) ^ 2)
                End If
                If pinkynotalloweddirections.Contains("down") = False Then
                    pinkydistances(1) = Math.Sqrt((pinkyX - pinkytargetx) ^ 2 + (pinkyY + 1 - pinkytargety) ^ 2)
                End If
                If pinkynotalloweddirections.Contains("left") = False Then
                    pinkydistances(2) = Math.Sqrt((pinkyX - 1 - pinkytargetx) ^ 2 + (pinkyY - pinkytargety) ^ 2)
                End If
                If pinkynotalloweddirections.Contains("right") = False Then
                    pinkydistances(3) = Math.Sqrt((pinkyX + 1 - pinkytargetx) ^ 2 + (pinkyY - pinkytargety) ^ 2)
                End If
                If pinkydistances.Min = pinkydistances(0) Then
                    pinkydirection = "up"
                End If
                If pinkydistances.Min = pinkydistances(1) Then
                    pinkydirection = "down"
                End If
                If pinkydistances.Min = pinkydistances(2) Then
                    pinkydirection = "left"
                End If
                If pinkydistances.Min = pinkydistances(3) Then
                    pinkydirection = "right"
                End If
            End If
            If pinkymode = "eyes" And pinkyX = pinkytargetx And pinkyY = pinkytargety Then 'reached the home box
                pinkymode = "restart"
                pinky.X = 285
                pinky.Y = 275
                pinkymove.Stop()
                pinkydirection = "down"
            End If
            'frightened mode direction selection
        ElseIf pinkymode = "frightened" Then
            If pinkyitwillwork = True Then
                pinkydirection = ""
                While pinkydirection = ""
                    Randomize()
                    pinkynewdirection = Int(Rnd() * 6 + 1)
                    If pinkynewdirection = 1 Then
                        pinkydirection = "up"
                    ElseIf pinkynewdirection = 2 Then
                        pinkydirection = "right"
                    ElseIf pinkynewdirection = 3 Then
                        pinkydirection = "down"
                    ElseIf pinkynewdirection = 4 Then
                        pinkydirection = "left"
                    End If
                    If pinkynewdirection = 5 Or pinkynewdirection = 6 Then
                        pinkydirection = oldpinkydirection
                    End If
                    'can't reverse direction at wall
                    If (oldpinkydirection = "up" And pinkydirection = "down") Or (oldpinkydirection = "down" And pinkydirection = "up") Or (oldpinkydirection = "left" And pinkydirection = "right") Or (oldpinkydirection = "right" And pinkydirection = "left") Then
                        pinkydirection = ""
                    End If
                    If (pinkyboxnum >= 114 And pinkyboxnum <= 126) Or (pinkyboxnum >= 128 And pinkyboxnum <= 140) Or (pinkyboxnum >= 227 And pinkyboxnum <= 230) Or (pinkyboxnum >= 232 And pinkyboxnum <= 236) Or (pinkyboxnum >= 238 And pinkyboxnum <= 239) Or (pinkyboxnum >= 241 And pinkyboxnum <= 245) Or (pinkyboxnum >= 247 And pinkyboxnum <= 251) Or (pinkyboxnum >= 311 And pinkyboxnum <= 314) Or (pinkyboxnum >= 319 And pinkyboxnum <= 321) Or (pinkyboxnum >= 324 And pinkyboxnum <= 326) Or (pinkyboxnum >= 331 And pinkyboxnum <= 334) Or (pinkyboxnum >= 402 And pinkyboxnum <= 411) Or (pinkyboxnum >= 477 And pinkyboxnum <= 482) Or (pinkyboxnum >= 484 And pinkyboxnum <= 485) Or (pinkyboxnum >= 496 And pinkyboxnum <= 497) Or (pinkyboxnum >= 499 And pinkyboxnum <= 504) Or (pinkyboxnum >= 571 And pinkyboxnum <= 578) Or (pinkyboxnum >= 646 And pinkyboxnum <= 650) Or (pinkyboxnum >= 652 And pinkyboxnum <= 653) Or (pinkyboxnum >= 655 And pinkyboxnum <= 657) Or (pinkyboxnum >= 660 And pinkyboxnum <= 662) Or (pinkyboxnum >= 664 And pinkyboxnum <= 665) Or (pinkyboxnum >= 667 And pinkyboxnum <= 671) Or (pinkyboxnum >= 731 And pinkyboxnum <= 732) Or (pinkyboxnum >= 736 And pinkyboxnum <= 749) Or (pinkyboxnum >= 753 And pinkyboxnum <= 754) Or (pinkyboxnum >= 814 And pinkyboxnum <= 815) Or (pinkyboxnum >= 817 And pinkyboxnum <= 818) Or (pinkyboxnum >= 823 And pinkyboxnum <= 825) Or (pinkyboxnum >= 828 And pinkyboxnum <= 830) Or (pinkyboxnum >= 835 And pinkyboxnum <= 836) Or (pinkyboxnum >= 838 And pinkyboxnum <= 839) Or (pinkyboxnum >= 899 And pinkyboxnum <= 908) Or (pinkyboxnum >= 910 And pinkyboxnum <= 911) Or (pinkyboxnum >= 913 And pinkyboxnum <= 922) Then
                        If pinkydirection = "up" Then
                            pinkydirection = ""
                        End If
                    End If
                    If (pinkyboxnum >= 115 And pinkyboxnum <= 118) Or (pinkyboxnum >= 120 And pinkyboxnum <= 124) Or (pinkyboxnum >= 129 And pinkyboxnum <= 133) Or (pinkyboxnum >= 135 And pinkyboxnum <= 138) Or (pinkyboxnum >= 227 And pinkyboxnum <= 230) Or (pinkyboxnum >= 232 And pinkyboxnum <= 233) Or (pinkyboxnum >= 235 And pinkyboxnum <= 242) Or (pinkyboxnum >= 244 And pinkyboxnum <= 245) Or (pinkyboxnum >= 247 And pinkyboxnum <= 250) Or (pinkyboxnum >= 310 And pinkyboxnum <= 314) Or (pinkyboxnum >= 318 And pinkyboxnum <= 320) Or (pinkyboxnum >= 325 And pinkyboxnum <= 327) Or (pinkyboxnum >= 331 And pinkyboxnum <= 335) Or (pinkyboxnum >= 403 And pinkyboxnum <= 410) Or (pinkyboxnum >= 477 And pinkyboxnum <= 482) Or (pinkyboxnum >= 484 And pinkyboxnum <= 485) Or (pinkyboxnum >= 496 And pinkyboxnum <= 497) Or (pinkyboxnum >= 499 And pinkyboxnum <= 504) Or (pinkyboxnum >= 571 And pinkyboxnum <= 578) Or (pinkyboxnum >= 647 And pinkyboxnum <= 650) Or (pinkyboxnum >= 652 And pinkyboxnum <= 656) Or (pinkyboxnum >= 661 And pinkyboxnum <= 665) Or (pinkyboxnum >= 667 And pinkyboxnum <= 670) Or (pinkyboxnum >= 730 And pinkyboxnum <= 731) Or (pinkyboxnum >= 736 And pinkyboxnum <= 737) Or (pinkyboxnum >= 739 And pinkyboxnum <= 746) Or (pinkyboxnum >= 748 And pinkyboxnum <= 749) Or (pinkyboxnum >= 754 And pinkyboxnum <= 755) Or (pinkyboxnum >= 815 And pinkyboxnum <= 819) Or (pinkyboxnum >= 822 And pinkyboxnum <= 824) Or (pinkyboxnum >= 829 And pinkyboxnum <= 831) Or (pinkyboxnum >= 834 And pinkyboxnum <= 838) Or (pinkyboxnum >= 898 And pinkyboxnum <= 923) Then
                        If pinkydirection = "down" Then
                            pinkydirection = ""
                        End If
                    End If
                    If pinkyboxnum = 142 Or pinkyboxnum = 170 Or pinkyboxnum = 198 Or pinkyboxnum = 254 Or pinkyboxnum = 282 Or pinkyboxnum = 674 Or pinkyboxnum = 702 Or pinkyboxnum = 842 Or pinkyboxnum = 870 Or pinkyboxnum = 732 Or pinkyboxnum = 760 Or pinkyboxnum = 788 Or pinkyboxnum = 147 Or pinkyboxnum = 175 Or pinkyboxnum = 203 Or pinkyboxnum = 259 Or pinkyboxnum = 287 Or pinkyboxnum = 315 Or pinkyboxnum = 343 Or pinkyboxnum = 371 Or pinkyboxnum = 399 Or pinkyboxnum = 427 Or pinkyboxnum = 455 Or pinkyboxnum = 511 Or pinkyboxnum = 539 Or pinkyboxnum = 567 Or pinkyboxnum = 595 Or pinkyboxnum = 623 Or pinkyboxnum = 679 Or pinkyboxnum = 707 Or pinkyboxnum = 763 Or pinkyboxnum = 791 Or pinkyboxnum = 819 Or pinkyboxnum = 262 Or pinkyboxnum = 290 Or pinkyboxnum = 430 Or pinkyboxnum = 458 Or pinkyboxnum = 486 Or pinkyboxnum = 514 Or pinkyboxnum = 542 Or pinkyboxnum = 598 Or pinkyboxnum = 626 Or pinkyboxnum = 766 Or pinkyboxnum = 794 Or pinkyboxnum = 125 Or pinkyboxnum = 153 Or pinkyboxnum = 181 Or pinkyboxnum = 209 Or pinkyboxnum = 321 Or pinkyboxnum = 349 Or pinkyboxnum = 377 Or pinkyboxnum = 657 Or pinkyboxnum = 685 Or pinkyboxnum = 713 Or pinkyboxnum = 825 Or pinkyboxnum = 853 Or pinkyboxnum = 881 Or pinkyboxnum = 156 Or pinkyboxnum = 184 Or pinkyboxnum = 212 Or pinkyboxnum = 352 Or pinkyboxnum = 380 Or pinkyboxnum = 688 Or pinkyboxnum = 716 Or pinkyboxnum = 856 Or pinkyboxnum = 884 Or pinkyboxnum = 271 Or pinkyboxnum = 299 Or pinkyboxnum = 327 Or pinkyboxnum = 411 Or pinkyboxnum = 439 Or pinkyboxnum = 467 Or pinkyboxnum = 523 Or pinkyboxnum = 551 Or pinkyboxnum = 579 Or pinkyboxnum = 607 Or pinkyboxnum = 635 Or pinkyboxnum = 775 Or pinkyboxnum = 803 Or pinkyboxnum = 831 Or pinkyboxnum = 139 Or pinkyboxnum = 167 Or pinkyboxnum = 195 Or pinkyboxnum = 223 Or pinkyboxnum = 251 Or pinkyboxnum = 279 Or pinkyboxnum = 307 Or pinkyboxnum = 335 Or pinkyboxnum = 671 Or pinkyboxnum = 699 Or pinkyboxnum = 727 Or pinkyboxnum = 755 Or pinkyboxnum = 839 Or pinkyboxnum = 867 Or pinkyboxnum = 895 Or pinkyboxnum = 923 Or pinkyboxnum = 781 Or pinkyboxnum = 809 Or pinkyboxnum = 162 Or pinkyboxnum = 190 Or pinkyboxnum = 218 Or pinkyboxnum = 274 Or pinkyboxnum = 302 Or pinkyboxnum = 358 Or pinkyboxnum = 386 Or pinkyboxnum = 414 Or pinkyboxnum = 442 Or pinkyboxnum = 470 Or pinkyboxnum = 526 Or pinkyboxnum = 554 Or pinkyboxnum = 582 Or pinkyboxnum = 610 Or pinkyboxnum = 638 Or pinkyboxnum = 694 Or pinkyboxnum = 722 Or pinkyboxnum = 750 Or pinkyboxnum = 778 Or pinkyboxnum = 806 Then
                        If pinkydirection = "right" Then
                            pinkydirection = ""
                        End If
                    End If
                    If pinkyboxnum = 114 Or pinkyboxnum = 142 Or pinkyboxnum = 170 Or pinkyboxnum = 198 Or pinkyboxnum = 226 Or pinkyboxnum = 254 Or pinkyboxnum = 282 Or pinkyboxnum = 310 Or pinkyboxnum = 646 Or pinkyboxnum = 674 Or pinkyboxnum = 702 Or pinkyboxnum = 730 Or pinkyboxnum = 814 Or pinkyboxnum = 842 Or pinkyboxnum = 870 Or pinkyboxnum = 898 Or pinkyboxnum = 760 Or pinkyboxnum = 788 Or pinkyboxnum = 147 Or pinkyboxnum = 175 Or pinkyboxnum = 203 Or pinkyboxnum = 252 Or pinkyboxnum = 287 Or pinkyboxnum = 343 Or pinkyboxnum = 371 Or pinkyboxnum = 399 Or pinkyboxnum = 427 Or pinkyboxnum = 455 Or pinkyboxnum = 511 Or pinkyboxnum = 539 Or pinkyboxnum = 567 Or pinkyboxnum = 595 Or pinkyboxnum = 623 Or pinkyboxnum = 679 Or pinkyboxnum = 707 Or pinkyboxnum = 735 Or pinkyboxnum = 763 Or pinkyboxnum = 791 Or pinkyboxnum = 262 Or pinkyboxnum = 290 Or pinkyboxnum = 318 Or pinkyboxnum = 402 Or pinkyboxnum = 430 Or pinkyboxnum = 458 Or pinkyboxnum = 514 Or pinkyboxnum = 542 Or pinkyboxnum = 570 Or pinkyboxnum = 598 Or pinkyboxnum = 626 Or pinkyboxnum = 766 Or pinkyboxnum = 794 Or pinkyboxnum = 822 Or pinkyboxnum = 153 Or pinkyboxnum = 181 Or pinkyboxnum = 209 Or pinkyboxnum = 349 Or pinkyboxnum = 377 Or pinkyboxnum = 685 Or pinkyboxnum = 713 Or pinkyboxnum = 853 Or pinkyboxnum = 881 Or pinkyboxnum = 128 Or pinkyboxnum = 156 Or pinkyboxnum = 184 Or pinkyboxnum = 212 Or pinkyboxnum = 324 Or pinkyboxnum = 352 Or pinkyboxnum = 380 Or pinkyboxnum = 660 Or pinkyboxnum = 688 Or pinkyboxnum = 716 Or pinkyboxnum = 828 Or pinkyboxnum = 856 Or pinkyboxnum = 884 Or pinkyboxnum = 271 Or pinkyboxnum = 299 Or pinkyboxnum = 439 Or pinkyboxnum = 467 Or pinkyboxnum = 495 Or pinkyboxnum = 523 Or pinkyboxnum = 551 Or pinkyboxnum = 607 Or pinkyboxnum = 635 Or pinkyboxnum = 775 Or pinkyboxnum = 803 Or pinkyboxnum = 162 Or pinkyboxnum = 190 Or pinkyboxnum = 218 Or pinkyboxnum = 274 Or pinkyboxnum = 302 Or pinkyboxnum = 330 Or pinkyboxnum = 358 Or pinkyboxnum = 386 Or pinkyboxnum = 414 Or pinkyboxnum = 442 Or pinkyboxnum = 470 Or pinkyboxnum = 526 Or pinkyboxnum = 554 Or pinkyboxnum = 582 Or pinkyboxnum = 610 Or pinkyboxnum = 638 Or pinkyboxnum = 694 Or pinkyboxnum = 722 Or pinkyboxnum = 778 Or pinkyboxnum = 806 Or pinkyboxnum = 834 Or pinkyboxnum = 753 Or pinkyboxnum = 781 Or pinkyboxnum = 809 Or pinkyboxnum = 167 Or pinkyboxnum = 195 Or pinkyboxnum = 223 Or pinkyboxnum = 279 Or pinkyboxnum = 307 Or pinkyboxnum = 699 Or pinkyboxnum = 727 Or pinkyboxnum = 867 Or pinkyboxnum = 895 Then
                        If pinkydirection = "left" Then
                            pinkydirection = ""
                        End If
                    End If
                End While
            End If
        End If
        'movement control
        If pinkydirection = "up" Then
            pinkyycounter -= 5
            pinky.Offset(0, -5)
        ElseIf pinkydirection = "down" Then
            pinkyycounter += 5
            pinky.Offset(0, 5)
        ElseIf pinkydirection = "left" Then
            If pinkyboxnum = 477 Then
                pinky.X = 555
                pinkyX = 28
            Else
                pinkyxcounter -= 5
                pinky.Offset(-5, 0)
            End If
        ElseIf pinkydirection = "right" Then
            If pinkyboxnum = 504 Then
                pinky.X = 15
                pinkyX = 1
            Else
                pinkyxcounter += 5
                pinky.Offset(5, 0)
            End If
        End If

        'finds edge of box
        If pinkyxcounter = 20 Then
            pinkyX += 1
            pinkyxcounter = 0
        End If
        If pinkyxcounter = -20 Then
            pinkyX -= 1
            pinkyxcounter = 0
        End If
        If pinkyycounter = 20 Then
            pinkyY += 1
            pinkyycounter = 0
        End If
        If pinkyycounter = -20 Then
            pinkyY -= 1
            pinkyycounter = 0
        End If
        pinkyboxnum = pinkyY * 28 + pinkyX
    End Sub
    Private Sub inkymove_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles inkymove.Tick
        Dim inkydistances(3) As Double
        For i As Integer = 0 To 3
            inkydistances(i) = 999999999999999
        Next
        inkyboxnum = inkyY * 28 + inkyX
        inkynotalloweddirections = ""
        'makes sure that inky cannot reverse direction at wall
        oldinkydirection = inkydirection
        If oldinkydirection = "up" Then
            inkynotalloweddirections = inkynotalloweddirections & "down"
        End If
        If oldinkydirection = "down" Then
            inkynotalloweddirections = inkynotalloweddirections & "up"
        End If
        If oldinkydirection = "left" Then
            inkynotalloweddirections = inkynotalloweddirections & "right"
        End If
        If oldinkydirection = "right" Then
            inkynotalloweddirections = inkynotalloweddirections & "left"
        End If
        'determines where he can go based on block he's in
        If (inkyboxnum >= 114 And inkyboxnum <= 126) Or (inkyboxnum >= 128 And inkyboxnum <= 140) Or (inkyboxnum >= 227 And inkyboxnum <= 230) Or (inkyboxnum >= 232 And inkyboxnum <= 236) Or (inkyboxnum >= 238 And inkyboxnum <= 239) Or (inkyboxnum >= 241 And inkyboxnum <= 245) Or (inkyboxnum >= 247 And inkyboxnum <= 251) Or (inkyboxnum >= 311 And inkyboxnum <= 314) Or (inkyboxnum >= 319 And inkyboxnum <= 321) Or (inkyboxnum >= 324 And inkyboxnum <= 326) Or (inkyboxnum >= 331 And inkyboxnum <= 334) Or (inkyboxnum >= 402 And inkyboxnum <= 411) Or (inkyboxnum >= 477 And inkyboxnum <= 482) Or (inkyboxnum >= 484 And inkyboxnum <= 485) Or (inkyboxnum >= 496 And inkyboxnum <= 497) Or (inkyboxnum >= 499 And inkyboxnum <= 504) Or (inkyboxnum >= 571 And inkyboxnum <= 578) Or (inkyboxnum >= 646 And inkyboxnum <= 650) Or (inkyboxnum >= 652 And inkyboxnum <= 653) Or (inkyboxnum >= 655 And inkyboxnum <= 657) Or (inkyboxnum >= 660 And inkyboxnum <= 662) Or (inkyboxnum >= 664 And inkyboxnum <= 665) Or (inkyboxnum >= 667 And inkyboxnum <= 671) Or (inkyboxnum >= 731 And inkyboxnum <= 732) Or (inkyboxnum >= 736 And inkyboxnum <= 749) Or (inkyboxnum >= 753 And inkyboxnum <= 754) Or (inkyboxnum >= 814 And inkyboxnum <= 815) Or (inkyboxnum >= 817 And inkyboxnum <= 818) Or (inkyboxnum >= 823 And inkyboxnum <= 825) Or (inkyboxnum >= 828 And inkyboxnum <= 830) Or (inkyboxnum >= 835 And inkyboxnum <= 836) Or (inkyboxnum >= 838 And inkyboxnum <= 839) Or (inkyboxnum >= 899 And inkyboxnum <= 908) Or (inkyboxnum >= 910 And inkyboxnum <= 911) Or (inkyboxnum >= 913 And inkyboxnum <= 922) Then
            inkynotalloweddirections = inkynotalloweddirections & "up "
        End If
        If (inkyboxnum >= 115 And inkyboxnum <= 118) Or (inkyboxnum >= 120 And inkyboxnum <= 124) Or (inkyboxnum >= 129 And inkyboxnum <= 133) Or (inkyboxnum >= 135 And inkyboxnum <= 138) Or (inkyboxnum >= 227 And inkyboxnum <= 230) Or (inkyboxnum >= 232 And inkyboxnum <= 233) Or (inkyboxnum >= 235 And inkyboxnum <= 242) Or (inkyboxnum >= 244 And inkyboxnum <= 245) Or (inkyboxnum >= 247 And inkyboxnum <= 250) Or (inkyboxnum >= 310 And inkyboxnum <= 314) Or (inkyboxnum >= 318 And inkyboxnum <= 320) Or (inkyboxnum >= 325 And inkyboxnum <= 327) Or (inkyboxnum >= 331 And inkyboxnum <= 335) Or (inkyboxnum >= 403 And inkyboxnum <= 410) Or (inkyboxnum >= 477 And inkyboxnum <= 482) Or (inkyboxnum >= 484 And inkyboxnum <= 485) Or (inkyboxnum >= 496 And inkyboxnum <= 497) Or (inkyboxnum >= 499 And inkyboxnum <= 504) Or (inkyboxnum >= 571 And inkyboxnum <= 578) Or (inkyboxnum >= 647 And inkyboxnum <= 650) Or (inkyboxnum >= 652 And inkyboxnum <= 656) Or (inkyboxnum >= 661 And inkyboxnum <= 665) Or (inkyboxnum >= 667 And inkyboxnum <= 670) Or (inkyboxnum >= 730 And inkyboxnum <= 731) Or (inkyboxnum >= 736 And inkyboxnum <= 737) Or (inkyboxnum >= 739 And inkyboxnum <= 746) Or (inkyboxnum >= 748 And inkyboxnum <= 749) Or (inkyboxnum >= 754 And inkyboxnum <= 755) Or (inkyboxnum >= 815 And inkyboxnum <= 819) Or (inkyboxnum >= 822 And inkyboxnum <= 824) Or (inkyboxnum >= 829 And inkyboxnum <= 831) Or (inkyboxnum >= 834 And inkyboxnum <= 838) Or (inkyboxnum >= 898 And inkyboxnum <= 923) Then
            inkynotalloweddirections = inkynotalloweddirections & "down "
        End If
        If inkyboxnum = 142 Or inkyboxnum = 170 Or inkyboxnum = 198 Or inkyboxnum = 254 Or inkyboxnum = 282 Or inkyboxnum = 674 Or inkyboxnum = 702 Or inkyboxnum = 842 Or inkyboxnum = 870 Or inkyboxnum = 732 Or inkyboxnum = 760 Or inkyboxnum = 788 Or inkyboxnum = 147 Or inkyboxnum = 175 Or inkyboxnum = 203 Or inkyboxnum = 259 Or inkyboxnum = 287 Or inkyboxnum = 315 Or inkyboxnum = 343 Or inkyboxnum = 371 Or inkyboxnum = 399 Or inkyboxnum = 427 Or inkyboxnum = 455 Or inkyboxnum = 511 Or inkyboxnum = 539 Or inkyboxnum = 567 Or inkyboxnum = 595 Or inkyboxnum = 623 Or inkyboxnum = 679 Or inkyboxnum = 707 Or inkyboxnum = 763 Or inkyboxnum = 791 Or inkyboxnum = 819 Or inkyboxnum = 262 Or inkyboxnum = 290 Or inkyboxnum = 430 Or inkyboxnum = 458 Or inkyboxnum = 486 Or inkyboxnum = 514 Or inkyboxnum = 542 Or inkyboxnum = 598 Or inkyboxnum = 626 Or inkyboxnum = 766 Or inkyboxnum = 794 Or inkyboxnum = 125 Or inkyboxnum = 153 Or inkyboxnum = 181 Or inkyboxnum = 209 Or inkyboxnum = 321 Or inkyboxnum = 349 Or inkyboxnum = 377 Or inkyboxnum = 657 Or inkyboxnum = 685 Or inkyboxnum = 713 Or inkyboxnum = 825 Or inkyboxnum = 853 Or inkyboxnum = 881 Or inkyboxnum = 156 Or inkyboxnum = 184 Or inkyboxnum = 212 Or inkyboxnum = 352 Or inkyboxnum = 380 Or inkyboxnum = 688 Or inkyboxnum = 716 Or inkyboxnum = 856 Or inkyboxnum = 884 Or inkyboxnum = 271 Or inkyboxnum = 299 Or inkyboxnum = 327 Or inkyboxnum = 411 Or inkyboxnum = 439 Or inkyboxnum = 467 Or inkyboxnum = 523 Or inkyboxnum = 551 Or inkyboxnum = 579 Or inkyboxnum = 607 Or inkyboxnum = 635 Or inkyboxnum = 775 Or inkyboxnum = 803 Or inkyboxnum = 831 Or inkyboxnum = 139 Or inkyboxnum = 167 Or inkyboxnum = 195 Or inkyboxnum = 223 Or inkyboxnum = 251 Or inkyboxnum = 279 Or inkyboxnum = 307 Or inkyboxnum = 335 Or inkyboxnum = 671 Or inkyboxnum = 699 Or inkyboxnum = 727 Or inkyboxnum = 755 Or inkyboxnum = 839 Or inkyboxnum = 867 Or inkyboxnum = 895 Or inkyboxnum = 923 Or inkyboxnum = 781 Or inkyboxnum = 809 Or inkyboxnum = 162 Or inkyboxnum = 190 Or inkyboxnum = 218 Or inkyboxnum = 274 Or inkyboxnum = 302 Or inkyboxnum = 358 Or inkyboxnum = 386 Or inkyboxnum = 414 Or inkyboxnum = 442 Or inkyboxnum = 470 Or inkyboxnum = 526 Or inkyboxnum = 554 Or inkyboxnum = 582 Or inkyboxnum = 610 Or inkyboxnum = 638 Or inkyboxnum = 694 Or inkyboxnum = 722 Or inkyboxnum = 750 Or inkyboxnum = 778 Or inkyboxnum = 806 Then
            inkynotalloweddirections = inkynotalloweddirections & "right "
        End If
        If inkyboxnum = 114 Or inkyboxnum = 142 Or inkyboxnum = 170 Or inkyboxnum = 198 Or inkyboxnum = 226 Or inkyboxnum = 254 Or inkyboxnum = 282 Or inkyboxnum = 310 Or inkyboxnum = 646 Or inkyboxnum = 674 Or inkyboxnum = 702 Or inkyboxnum = 730 Or inkyboxnum = 814 Or inkyboxnum = 842 Or inkyboxnum = 870 Or inkyboxnum = 898 Or inkyboxnum = 760 Or inkyboxnum = 788 Or inkyboxnum = 147 Or inkyboxnum = 175 Or inkyboxnum = 203 Or inkyboxnum = 259 Or inkyboxnum = 287 Or inkyboxnum = 343 Or inkyboxnum = 371 Or inkyboxnum = 399 Or inkyboxnum = 427 Or inkyboxnum = 455 Or inkyboxnum = 511 Or inkyboxnum = 539 Or inkyboxnum = 567 Or inkyboxnum = 595 Or inkyboxnum = 623 Or inkyboxnum = 679 Or inkyboxnum = 707 Or inkyboxnum = 735 Or inkyboxnum = 763 Or inkyboxnum = 791 Or inkyboxnum = 262 Or inkyboxnum = 290 Or inkyboxnum = 318 Or inkyboxnum = 402 Or inkyboxnum = 430 Or inkyboxnum = 458 Or inkyboxnum = 514 Or inkyboxnum = 542 Or inkyboxnum = 570 Or inkyboxnum = 598 Or inkyboxnum = 626 Or inkyboxnum = 766 Or inkyboxnum = 794 Or inkyboxnum = 822 Or inkyboxnum = 153 Or inkyboxnum = 181 Or inkyboxnum = 209 Or inkyboxnum = 349 Or inkyboxnum = 377 Or inkyboxnum = 685 Or inkyboxnum = 713 Or inkyboxnum = 853 Or inkyboxnum = 881 Or inkyboxnum = 128 Or inkyboxnum = 156 Or inkyboxnum = 184 Or inkyboxnum = 212 Or inkyboxnum = 324 Or inkyboxnum = 352 Or inkyboxnum = 380 Or inkyboxnum = 660 Or inkyboxnum = 688 Or inkyboxnum = 716 Or inkyboxnum = 828 Or inkyboxnum = 856 Or inkyboxnum = 884 Or inkyboxnum = 271 Or inkyboxnum = 299 Or inkyboxnum = 439 Or inkyboxnum = 467 Or inkyboxnum = 495 Or inkyboxnum = 523 Or inkyboxnum = 551 Or inkyboxnum = 607 Or inkyboxnum = 635 Or inkyboxnum = 775 Or inkyboxnum = 803 Or inkyboxnum = 162 Or inkyboxnum = 190 Or inkyboxnum = 218 Or inkyboxnum = 274 Or inkyboxnum = 302 Or inkyboxnum = 330 Or inkyboxnum = 358 Or inkyboxnum = 386 Or inkyboxnum = 414 Or inkyboxnum = 442 Or inkyboxnum = 470 Or inkyboxnum = 526 Or inkyboxnum = 554 Or inkyboxnum = 582 Or inkyboxnum = 610 Or inkyboxnum = 638 Or inkyboxnum = 694 Or inkyboxnum = 722 Or inkyboxnum = 778 Or inkyboxnum = 806 Or inkyboxnum = 834 Or inkyboxnum = 753 Or inkyboxnum = 781 Or inkyboxnum = 809 Or inkyboxnum = 167 Or inkyboxnum = 195 Or inkyboxnum = 223 Or inkyboxnum = 279 Or inkyboxnum = 307 Or inkyboxnum = 699 Or inkyboxnum = 727 Or inkyboxnum = 867 Or inkyboxnum = 895 Then
            inkynotalloweddirections = inkynotalloweddirections & "left "
        End If
        'determines if inky can turn (is perfectly lined up to turn)
        inkyitwillwork = False
        For i As Integer = 0 To 35
            For t As Integer = 0 To 35
                If (inky.X + 5) / 20 = i And (inky.Y + 5) / 20 = t Then
                    inkyitwillwork = True
                End If
            Next
        Next
        'targeting
        If inkymode = "chase" Then
                Dim f As Integer
                If direction = "up" Then
                    f = x - blinkyX
                    inkytargetx = x + f
                    f = (y - 2) - blinkyY
                    inkytargety = y - 1 + f
                ElseIf direction = "down" Then
                    f = x - blinkyX
                    inkytargetx = x + f
                    f = (y + 2) - blinkyY
                    inkytargety = y + 1 + f
                ElseIf direction = "right" Then
                    f = (x + 2) - blinkyX
                    inkytargetx = x + 1 + f
                    f = y - blinkyY
                    inkytargety = y + f
                ElseIf direction = "left" Then
                    f = (x - 2) - blinkyX
                    inkytargetx = x - 1 + f
                    f = y - blinkyY
                    inkytargety = y + f
                End If
        ElseIf inkymode = "scatter" Then
            inkytargetx = 27
            inkytargety = 33
        ElseIf inkymode = "eyes" Then
            inkytargetx = 14
            inkytargety = 14
        End If
        'if he not frightened and can turn then...
        If inkymode = "chase" Or inkymode = "scatter" Or inkymode = "eyes" Then
            If inkyitwillwork = True Then
                If inkynotalloweddirections.Contains("up") = False Then
                    inkydistances(0) = Math.Sqrt((inkyX - inkytargetx) ^ 2 + (inkyY - 1 - inkytargety) ^ 2)
                End If
                If inkynotalloweddirections.Contains("down") = False Then
                    inkydistances(1) = Math.Sqrt((inkyX - inkytargetx) ^ 2 + (inkyY + 1 - inkytargety) ^ 2)
                End If
                If inkynotalloweddirections.Contains("left") = False Then
                    inkydistances(2) = Math.Sqrt((inkyX - 1 - inkytargetx) ^ 2 + (inkyY - inkytargety) ^ 2)
                End If
                If inkynotalloweddirections.Contains("right") = False Then
                    inkydistances(3) = Math.Sqrt((inkyX + 1 - inkytargetx) ^ 2 + (inkyY - inkytargety) ^ 2)
                End If
                If inkydistances.Min = inkydistances(0) Then
                    inkydirection = "up"
                End If
                If inkydistances.Min = inkydistances(1) Then
                    inkydirection = "down"
                End If
                If inkydistances.Min = inkydistances(2) Then
                    inkydirection = "left"
                End If
                If inkydistances.Min = inkydistances(3) Then
                    inkydirection = "right"
                End If
            End If
            If inkymode = "eyes" And inkyX = inkytargetx And inkyY = inkytargety Then 'reached the home box
                inkymode = "restart"
                inky.X = 285
                inky.Y = 275
                inkymove.Stop()
                inkydirection = "down"
            End If
            'frightened mode direction selection
        ElseIf inkymode = "frightened" Then
            If inkyitwillwork = True Then
                inkydirection = ""
                While inkydirection = ""
                    Randomize()
                    inkynewdirection = Int(Rnd() * 6 + 1)
                    If inkynewdirection = 1 Then
                        inkydirection = "up"
                    ElseIf inkynewdirection = 2 Then
                        inkydirection = "right"
                    ElseIf inkynewdirection = 3 Then
                        inkydirection = "down"
                    ElseIf inkynewdirection = 4 Then
                        inkydirection = "left"
                    End If
                    If inkynewdirection = 5 Or inkynewdirection = 6 Then
                        inkydirection = oldinkydirection
                    End If
                    'can't reverse direction at wall
                    If (oldinkydirection = "up" And inkydirection = "down") Or (oldinkydirection = "down" And inkydirection = "up") Or (oldinkydirection = "left" And inkydirection = "right") Or (oldinkydirection = "right" And inkydirection = "left") Then
                        inkydirection = ""
                    End If
                    If (inkyboxnum >= 114 And inkyboxnum <= 126) Or (inkyboxnum >= 128 And inkyboxnum <= 140) Or (inkyboxnum >= 227 And inkyboxnum <= 230) Or (inkyboxnum >= 232 And inkyboxnum <= 236) Or (inkyboxnum >= 238 And inkyboxnum <= 239) Or (inkyboxnum >= 241 And inkyboxnum <= 245) Or (inkyboxnum >= 247 And inkyboxnum <= 251) Or (inkyboxnum >= 311 And inkyboxnum <= 314) Or (inkyboxnum >= 319 And inkyboxnum <= 321) Or (inkyboxnum >= 324 And inkyboxnum <= 326) Or (inkyboxnum >= 331 And inkyboxnum <= 334) Or (inkyboxnum >= 402 And inkyboxnum <= 411) Or (inkyboxnum >= 477 And inkyboxnum <= 482) Or (inkyboxnum >= 484 And inkyboxnum <= 485) Or (inkyboxnum >= 496 And inkyboxnum <= 497) Or (inkyboxnum >= 499 And inkyboxnum <= 504) Or (inkyboxnum >= 571 And inkyboxnum <= 578) Or (inkyboxnum >= 646 And inkyboxnum <= 650) Or (inkyboxnum >= 652 And inkyboxnum <= 653) Or (inkyboxnum >= 655 And inkyboxnum <= 657) Or (inkyboxnum >= 660 And inkyboxnum <= 662) Or (inkyboxnum >= 664 And inkyboxnum <= 665) Or (inkyboxnum >= 667 And inkyboxnum <= 671) Or (inkyboxnum >= 731 And inkyboxnum <= 732) Or (inkyboxnum >= 736 And inkyboxnum <= 749) Or (inkyboxnum >= 753 And inkyboxnum <= 754) Or (inkyboxnum >= 814 And inkyboxnum <= 815) Or (inkyboxnum >= 817 And inkyboxnum <= 818) Or (inkyboxnum >= 823 And inkyboxnum <= 825) Or (inkyboxnum >= 828 And inkyboxnum <= 830) Or (inkyboxnum >= 835 And inkyboxnum <= 836) Or (inkyboxnum >= 838 And inkyboxnum <= 839) Or (inkyboxnum >= 899 And inkyboxnum <= 908) Or (inkyboxnum >= 910 And inkyboxnum <= 911) Or (inkyboxnum >= 913 And inkyboxnum <= 922) Then
                        If inkydirection = "up" Then
                            inkydirection = ""
                        End If
                    End If
                    If (inkyboxnum >= 115 And inkyboxnum <= 118) Or (inkyboxnum >= 120 And inkyboxnum <= 124) Or (inkyboxnum >= 129 And inkyboxnum <= 133) Or (inkyboxnum >= 135 And inkyboxnum <= 138) Or (inkyboxnum >= 227 And inkyboxnum <= 230) Or (inkyboxnum >= 232 And inkyboxnum <= 233) Or (inkyboxnum >= 235 And inkyboxnum <= 242) Or (inkyboxnum >= 244 And inkyboxnum <= 245) Or (inkyboxnum >= 247 And inkyboxnum <= 250) Or (inkyboxnum >= 310 And inkyboxnum <= 314) Or (inkyboxnum >= 318 And inkyboxnum <= 320) Or (inkyboxnum >= 325 And inkyboxnum <= 327) Or (inkyboxnum >= 331 And inkyboxnum <= 335) Or (inkyboxnum >= 403 And inkyboxnum <= 410) Or (inkyboxnum >= 477 And inkyboxnum <= 482) Or (inkyboxnum >= 484 And inkyboxnum <= 485) Or (inkyboxnum >= 496 And inkyboxnum <= 497) Or (inkyboxnum >= 499 And inkyboxnum <= 504) Or (inkyboxnum >= 571 And inkyboxnum <= 578) Or (inkyboxnum >= 647 And inkyboxnum <= 650) Or (inkyboxnum >= 652 And inkyboxnum <= 656) Or (inkyboxnum >= 661 And inkyboxnum <= 665) Or (inkyboxnum >= 667 And inkyboxnum <= 670) Or (inkyboxnum >= 730 And inkyboxnum <= 731) Or (inkyboxnum >= 736 And inkyboxnum <= 737) Or (inkyboxnum >= 739 And inkyboxnum <= 746) Or (inkyboxnum >= 748 And inkyboxnum <= 749) Or (inkyboxnum >= 754 And inkyboxnum <= 755) Or (inkyboxnum >= 815 And inkyboxnum <= 819) Or (inkyboxnum >= 822 And inkyboxnum <= 824) Or (inkyboxnum >= 829 And inkyboxnum <= 831) Or (inkyboxnum >= 834 And inkyboxnum <= 838) Or (inkyboxnum >= 898 And inkyboxnum <= 923) Then
                        If inkydirection = "down" Then
                            inkydirection = ""
                        End If
                    End If
                    If inkyboxnum = 142 Or inkyboxnum = 170 Or inkyboxnum = 198 Or inkyboxnum = 254 Or inkyboxnum = 282 Or inkyboxnum = 674 Or inkyboxnum = 702 Or inkyboxnum = 842 Or inkyboxnum = 870 Or inkyboxnum = 732 Or inkyboxnum = 760 Or inkyboxnum = 788 Or inkyboxnum = 147 Or inkyboxnum = 175 Or inkyboxnum = 203 Or inkyboxnum = 259 Or inkyboxnum = 287 Or inkyboxnum = 315 Or inkyboxnum = 343 Or inkyboxnum = 371 Or inkyboxnum = 399 Or inkyboxnum = 427 Or inkyboxnum = 455 Or inkyboxnum = 511 Or inkyboxnum = 539 Or inkyboxnum = 567 Or inkyboxnum = 595 Or inkyboxnum = 623 Or inkyboxnum = 679 Or inkyboxnum = 707 Or inkyboxnum = 763 Or inkyboxnum = 791 Or inkyboxnum = 819 Or inkyboxnum = 262 Or inkyboxnum = 290 Or inkyboxnum = 430 Or inkyboxnum = 458 Or inkyboxnum = 486 Or inkyboxnum = 514 Or inkyboxnum = 542 Or inkyboxnum = 598 Or inkyboxnum = 626 Or inkyboxnum = 766 Or inkyboxnum = 794 Or inkyboxnum = 125 Or inkyboxnum = 153 Or inkyboxnum = 181 Or inkyboxnum = 209 Or inkyboxnum = 321 Or inkyboxnum = 349 Or inkyboxnum = 377 Or inkyboxnum = 657 Or inkyboxnum = 685 Or inkyboxnum = 713 Or inkyboxnum = 825 Or inkyboxnum = 853 Or inkyboxnum = 881 Or inkyboxnum = 156 Or inkyboxnum = 184 Or inkyboxnum = 212 Or inkyboxnum = 352 Or inkyboxnum = 380 Or inkyboxnum = 688 Or inkyboxnum = 716 Or inkyboxnum = 856 Or inkyboxnum = 884 Or inkyboxnum = 271 Or inkyboxnum = 299 Or inkyboxnum = 327 Or inkyboxnum = 411 Or inkyboxnum = 439 Or inkyboxnum = 467 Or inkyboxnum = 523 Or inkyboxnum = 551 Or inkyboxnum = 579 Or inkyboxnum = 607 Or inkyboxnum = 635 Or inkyboxnum = 775 Or inkyboxnum = 803 Or inkyboxnum = 831 Or inkyboxnum = 139 Or inkyboxnum = 167 Or inkyboxnum = 195 Or inkyboxnum = 223 Or inkyboxnum = 251 Or inkyboxnum = 279 Or inkyboxnum = 307 Or inkyboxnum = 335 Or inkyboxnum = 671 Or inkyboxnum = 699 Or inkyboxnum = 727 Or inkyboxnum = 755 Or inkyboxnum = 839 Or inkyboxnum = 867 Or inkyboxnum = 895 Or inkyboxnum = 923 Or inkyboxnum = 781 Or inkyboxnum = 809 Or inkyboxnum = 162 Or inkyboxnum = 190 Or inkyboxnum = 218 Or inkyboxnum = 274 Or inkyboxnum = 302 Or inkyboxnum = 358 Or inkyboxnum = 386 Or inkyboxnum = 414 Or inkyboxnum = 442 Or inkyboxnum = 470 Or inkyboxnum = 526 Or inkyboxnum = 554 Or inkyboxnum = 582 Or inkyboxnum = 610 Or inkyboxnum = 638 Or inkyboxnum = 694 Or inkyboxnum = 722 Or inkyboxnum = 750 Or inkyboxnum = 778 Or inkyboxnum = 806 Then
                        If inkydirection = "right" Then
                            inkydirection = ""
                        End If
                    End If
                    If inkyboxnum = 114 Or inkyboxnum = 142 Or inkyboxnum = 170 Or inkyboxnum = 198 Or inkyboxnum = 226 Or inkyboxnum = 254 Or inkyboxnum = 282 Or inkyboxnum = 310 Or inkyboxnum = 646 Or inkyboxnum = 674 Or inkyboxnum = 702 Or inkyboxnum = 730 Or inkyboxnum = 814 Or inkyboxnum = 842 Or inkyboxnum = 870 Or inkyboxnum = 898 Or inkyboxnum = 760 Or inkyboxnum = 788 Or inkyboxnum = 147 Or inkyboxnum = 175 Or inkyboxnum = 203 Or inkyboxnum = 252 Or inkyboxnum = 287 Or inkyboxnum = 343 Or inkyboxnum = 371 Or inkyboxnum = 399 Or inkyboxnum = 427 Or inkyboxnum = 455 Or inkyboxnum = 511 Or inkyboxnum = 539 Or inkyboxnum = 567 Or inkyboxnum = 595 Or inkyboxnum = 623 Or inkyboxnum = 679 Or inkyboxnum = 707 Or inkyboxnum = 735 Or inkyboxnum = 763 Or inkyboxnum = 791 Or inkyboxnum = 262 Or inkyboxnum = 290 Or inkyboxnum = 318 Or inkyboxnum = 402 Or inkyboxnum = 430 Or inkyboxnum = 458 Or inkyboxnum = 514 Or inkyboxnum = 542 Or inkyboxnum = 570 Or inkyboxnum = 598 Or inkyboxnum = 626 Or inkyboxnum = 766 Or inkyboxnum = 794 Or inkyboxnum = 822 Or inkyboxnum = 153 Or inkyboxnum = 181 Or inkyboxnum = 209 Or inkyboxnum = 349 Or inkyboxnum = 377 Or inkyboxnum = 685 Or inkyboxnum = 713 Or inkyboxnum = 853 Or inkyboxnum = 881 Or inkyboxnum = 128 Or inkyboxnum = 156 Or inkyboxnum = 184 Or inkyboxnum = 212 Or inkyboxnum = 324 Or inkyboxnum = 352 Or inkyboxnum = 380 Or inkyboxnum = 660 Or inkyboxnum = 688 Or inkyboxnum = 716 Or inkyboxnum = 828 Or inkyboxnum = 856 Or inkyboxnum = 884 Or inkyboxnum = 271 Or inkyboxnum = 299 Or inkyboxnum = 439 Or inkyboxnum = 467 Or inkyboxnum = 495 Or inkyboxnum = 523 Or inkyboxnum = 551 Or inkyboxnum = 607 Or inkyboxnum = 635 Or inkyboxnum = 775 Or inkyboxnum = 803 Or inkyboxnum = 162 Or inkyboxnum = 190 Or inkyboxnum = 218 Or inkyboxnum = 274 Or inkyboxnum = 302 Or inkyboxnum = 330 Or inkyboxnum = 358 Or inkyboxnum = 386 Or inkyboxnum = 414 Or inkyboxnum = 442 Or inkyboxnum = 470 Or inkyboxnum = 526 Or inkyboxnum = 554 Or inkyboxnum = 582 Or inkyboxnum = 610 Or inkyboxnum = 638 Or inkyboxnum = 694 Or inkyboxnum = 722 Or inkyboxnum = 778 Or inkyboxnum = 806 Or inkyboxnum = 834 Or inkyboxnum = 753 Or inkyboxnum = 781 Or inkyboxnum = 809 Or inkyboxnum = 167 Or inkyboxnum = 195 Or inkyboxnum = 223 Or inkyboxnum = 279 Or inkyboxnum = 307 Or inkyboxnum = 699 Or inkyboxnum = 727 Or inkyboxnum = 867 Or inkyboxnum = 895 Then
                        If inkydirection = "left" Then
                            inkydirection = ""
                        End If
                    End If
                End While
            End If
        End If
        'movement control
        If inkydirection = "up" Then
            inkyycounter -= 5
            inky.Offset(0, -5)
        ElseIf inkydirection = "down" Then
            inkyycounter += 5
            inky.Offset(0, 5)
        ElseIf inkydirection = "left" Then
            If inkyboxnum = 477 Then
                inky.X = 555
                inkyX = 28
            Else
                inkyxcounter -= 5
                inky.Offset(-5, 0)
            End If
        ElseIf inkydirection = "right" Then
            If inkyboxnum = 504 Then
                inky.X = 15
                inkyX = 1
            Else
                inkyxcounter += 5
                inky.Offset(5, 0)
            End If
        End If

        'finds edge of box
        If inkyxcounter = 20 Then
            inkyX += 1
            inkyxcounter = 0
        End If
        If inkyxcounter = -20 Then
            inkyX -= 1
            inkyxcounter = 0
        End If
        If inkyycounter = 20 Then
            inkyY += 1
            inkyycounter = 0
        End If
        If inkyycounter = -20 Then
            inkyY -= 1
            inkyycounter = 0
        End If
        inkyboxnum = inkyY * 28 + inkyX
    End Sub
    Private Sub suemove_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles suemove.Tick
        Dim suedistances(3) As Double
        For i As Integer = 0 To 3
            suedistances(i) = 999999999999999
        Next
        sueboxnum = sueY * 28 + sueX
        suenotalloweddirections = ""
        'makes sure that sue cannot reverse direction at wall
        oldsuedirection = suedirection
        If oldsuedirection = "up" Then
            suenotalloweddirections = suenotalloweddirections & "down"
        End If
        If oldsuedirection = "down" Then
            suenotalloweddirections = suenotalloweddirections & "up"
        End If
        If oldsuedirection = "left" Then
            suenotalloweddirections = suenotalloweddirections & "right"
        End If
        If oldsuedirection = "right" Then
            suenotalloweddirections = suenotalloweddirections & "left"
        End If
        'determines where he can go based on block he's in
        If (sueboxnum >= 114 And sueboxnum <= 126) Or (sueboxnum >= 128 And sueboxnum <= 140) Or (sueboxnum >= 227 And sueboxnum <= 230) Or (sueboxnum >= 232 And sueboxnum <= 236) Or (sueboxnum >= 238 And sueboxnum <= 239) Or (sueboxnum >= 241 And sueboxnum <= 245) Or (sueboxnum >= 247 And sueboxnum <= 251) Or (sueboxnum >= 311 And sueboxnum <= 314) Or (sueboxnum >= 319 And sueboxnum <= 321) Or (sueboxnum >= 324 And sueboxnum <= 326) Or (sueboxnum >= 331 And sueboxnum <= 334) Or (sueboxnum >= 402 And sueboxnum <= 411) Or (sueboxnum >= 477 And sueboxnum <= 482) Or (sueboxnum >= 484 And sueboxnum <= 485) Or (sueboxnum >= 496 And sueboxnum <= 497) Or (sueboxnum >= 499 And sueboxnum <= 504) Or (sueboxnum >= 571 And sueboxnum <= 578) Or (sueboxnum >= 646 And sueboxnum <= 650) Or (sueboxnum >= 652 And sueboxnum <= 653) Or (sueboxnum >= 655 And sueboxnum <= 657) Or (sueboxnum >= 660 And sueboxnum <= 662) Or (sueboxnum >= 664 And sueboxnum <= 665) Or (sueboxnum >= 667 And sueboxnum <= 671) Or (sueboxnum >= 731 And sueboxnum <= 732) Or (sueboxnum >= 736 And sueboxnum <= 749) Or (sueboxnum >= 753 And sueboxnum <= 754) Or (sueboxnum >= 814 And sueboxnum <= 815) Or (sueboxnum >= 817 And sueboxnum <= 818) Or (sueboxnum >= 823 And sueboxnum <= 825) Or (sueboxnum >= 828 And sueboxnum <= 830) Or (sueboxnum >= 835 And sueboxnum <= 836) Or (sueboxnum >= 838 And sueboxnum <= 839) Or (sueboxnum >= 899 And sueboxnum <= 908) Or (sueboxnum >= 910 And sueboxnum <= 911) Or (sueboxnum >= 913 And sueboxnum <= 922) Then
            suenotalloweddirections = suenotalloweddirections & "up "
        End If
        If (sueboxnum >= 115 And sueboxnum <= 118) Or (sueboxnum >= 120 And sueboxnum <= 124) Or (sueboxnum >= 129 And sueboxnum <= 133) Or (sueboxnum >= 135 And sueboxnum <= 138) Or (sueboxnum >= 227 And sueboxnum <= 230) Or (sueboxnum >= 232 And sueboxnum <= 233) Or (sueboxnum >= 235 And sueboxnum <= 242) Or (sueboxnum >= 244 And sueboxnum <= 245) Or (sueboxnum >= 247 And sueboxnum <= 250) Or (sueboxnum >= 310 And sueboxnum <= 314) Or (sueboxnum >= 318 And sueboxnum <= 320) Or (sueboxnum >= 325 And sueboxnum <= 327) Or (sueboxnum >= 331 And sueboxnum <= 335) Or (sueboxnum >= 403 And sueboxnum <= 410) Or (sueboxnum >= 477 And sueboxnum <= 482) Or (sueboxnum >= 484 And sueboxnum <= 485) Or (sueboxnum >= 496 And sueboxnum <= 497) Or (sueboxnum >= 499 And sueboxnum <= 504) Or (sueboxnum >= 571 And sueboxnum <= 578) Or (sueboxnum >= 647 And sueboxnum <= 650) Or (sueboxnum >= 652 And sueboxnum <= 656) Or (sueboxnum >= 661 And sueboxnum <= 665) Or (sueboxnum >= 667 And sueboxnum <= 670) Or (sueboxnum >= 730 And sueboxnum <= 731) Or (sueboxnum >= 736 And sueboxnum <= 737) Or (sueboxnum >= 739 And sueboxnum <= 746) Or (sueboxnum >= 748 And sueboxnum <= 749) Or (sueboxnum >= 754 And sueboxnum <= 755) Or (sueboxnum >= 815 And sueboxnum <= 819) Or (sueboxnum >= 822 And sueboxnum <= 824) Or (sueboxnum >= 829 And sueboxnum <= 831) Or (sueboxnum >= 834 And sueboxnum <= 838) Or (sueboxnum >= 898 And sueboxnum <= 923) Then
            suenotalloweddirections = suenotalloweddirections & "down "
        End If
        If sueboxnum = 142 Or sueboxnum = 170 Or sueboxnum = 198 Or sueboxnum = 254 Or sueboxnum = 282 Or sueboxnum = 674 Or sueboxnum = 702 Or sueboxnum = 842 Or sueboxnum = 870 Or sueboxnum = 732 Or sueboxnum = 760 Or sueboxnum = 788 Or sueboxnum = 147 Or sueboxnum = 175 Or sueboxnum = 203 Or sueboxnum = 259 Or sueboxnum = 287 Or sueboxnum = 315 Or sueboxnum = 343 Or sueboxnum = 371 Or sueboxnum = 399 Or sueboxnum = 427 Or sueboxnum = 455 Or sueboxnum = 511 Or sueboxnum = 539 Or sueboxnum = 567 Or sueboxnum = 595 Or sueboxnum = 623 Or sueboxnum = 679 Or sueboxnum = 707 Or sueboxnum = 763 Or sueboxnum = 791 Or sueboxnum = 819 Or sueboxnum = 262 Or sueboxnum = 290 Or sueboxnum = 430 Or sueboxnum = 458 Or sueboxnum = 486 Or sueboxnum = 514 Or sueboxnum = 542 Or sueboxnum = 598 Or sueboxnum = 626 Or sueboxnum = 766 Or sueboxnum = 794 Or sueboxnum = 125 Or sueboxnum = 153 Or sueboxnum = 181 Or sueboxnum = 209 Or sueboxnum = 321 Or sueboxnum = 349 Or sueboxnum = 377 Or sueboxnum = 657 Or sueboxnum = 685 Or sueboxnum = 713 Or sueboxnum = 825 Or sueboxnum = 853 Or sueboxnum = 881 Or sueboxnum = 156 Or sueboxnum = 184 Or sueboxnum = 212 Or sueboxnum = 352 Or sueboxnum = 380 Or sueboxnum = 688 Or sueboxnum = 716 Or sueboxnum = 856 Or sueboxnum = 884 Or sueboxnum = 271 Or sueboxnum = 299 Or sueboxnum = 327 Or sueboxnum = 411 Or sueboxnum = 439 Or sueboxnum = 467 Or sueboxnum = 523 Or sueboxnum = 551 Or sueboxnum = 579 Or sueboxnum = 607 Or sueboxnum = 635 Or sueboxnum = 775 Or sueboxnum = 803 Or sueboxnum = 831 Or sueboxnum = 139 Or sueboxnum = 167 Or sueboxnum = 195 Or sueboxnum = 223 Or sueboxnum = 251 Or sueboxnum = 279 Or sueboxnum = 307 Or sueboxnum = 335 Or sueboxnum = 671 Or sueboxnum = 699 Or sueboxnum = 727 Or sueboxnum = 755 Or sueboxnum = 839 Or sueboxnum = 867 Or sueboxnum = 895 Or sueboxnum = 923 Or sueboxnum = 781 Or sueboxnum = 809 Or sueboxnum = 162 Or sueboxnum = 190 Or sueboxnum = 218 Or sueboxnum = 274 Or sueboxnum = 302 Or sueboxnum = 358 Or sueboxnum = 386 Or sueboxnum = 414 Or sueboxnum = 442 Or sueboxnum = 470 Or sueboxnum = 526 Or sueboxnum = 554 Or sueboxnum = 582 Or sueboxnum = 610 Or sueboxnum = 638 Or sueboxnum = 694 Or sueboxnum = 722 Or sueboxnum = 750 Or sueboxnum = 778 Or sueboxnum = 806 Then
            suenotalloweddirections = suenotalloweddirections & "right "
        End If
        If sueboxnum = 114 Or sueboxnum = 142 Or sueboxnum = 170 Or sueboxnum = 198 Or sueboxnum = 226 Or sueboxnum = 254 Or sueboxnum = 282 Or sueboxnum = 310 Or sueboxnum = 646 Or sueboxnum = 674 Or sueboxnum = 702 Or sueboxnum = 730 Or sueboxnum = 814 Or sueboxnum = 842 Or sueboxnum = 870 Or sueboxnum = 898 Or sueboxnum = 760 Or sueboxnum = 788 Or sueboxnum = 147 Or sueboxnum = 175 Or sueboxnum = 203 Or sueboxnum = 259 Or sueboxnum = 287 Or sueboxnum = 343 Or sueboxnum = 371 Or sueboxnum = 399 Or sueboxnum = 427 Or sueboxnum = 455 Or sueboxnum = 511 Or sueboxnum = 539 Or sueboxnum = 567 Or sueboxnum = 595 Or sueboxnum = 623 Or sueboxnum = 679 Or sueboxnum = 707 Or sueboxnum = 735 Or sueboxnum = 763 Or sueboxnum = 791 Or sueboxnum = 262 Or sueboxnum = 290 Or sueboxnum = 318 Or sueboxnum = 402 Or sueboxnum = 430 Or sueboxnum = 458 Or sueboxnum = 514 Or sueboxnum = 542 Or sueboxnum = 570 Or sueboxnum = 598 Or sueboxnum = 626 Or sueboxnum = 766 Or sueboxnum = 794 Or sueboxnum = 822 Or sueboxnum = 153 Or sueboxnum = 181 Or sueboxnum = 209 Or sueboxnum = 349 Or sueboxnum = 377 Or sueboxnum = 685 Or sueboxnum = 713 Or sueboxnum = 853 Or sueboxnum = 881 Or sueboxnum = 128 Or sueboxnum = 156 Or sueboxnum = 184 Or sueboxnum = 212 Or sueboxnum = 324 Or sueboxnum = 352 Or sueboxnum = 380 Or sueboxnum = 660 Or sueboxnum = 688 Or sueboxnum = 716 Or sueboxnum = 828 Or sueboxnum = 856 Or sueboxnum = 884 Or sueboxnum = 271 Or sueboxnum = 299 Or sueboxnum = 439 Or sueboxnum = 467 Or sueboxnum = 495 Or sueboxnum = 523 Or sueboxnum = 551 Or sueboxnum = 607 Or sueboxnum = 635 Or sueboxnum = 775 Or sueboxnum = 803 Or sueboxnum = 162 Or sueboxnum = 190 Or sueboxnum = 218 Or sueboxnum = 274 Or sueboxnum = 302 Or sueboxnum = 330 Or sueboxnum = 358 Or sueboxnum = 386 Or sueboxnum = 414 Or sueboxnum = 442 Or sueboxnum = 470 Or sueboxnum = 526 Or sueboxnum = 554 Or sueboxnum = 582 Or sueboxnum = 610 Or sueboxnum = 638 Or sueboxnum = 694 Or sueboxnum = 722 Or sueboxnum = 778 Or sueboxnum = 806 Or sueboxnum = 834 Or sueboxnum = 753 Or sueboxnum = 781 Or sueboxnum = 809 Or sueboxnum = 167 Or sueboxnum = 195 Or sueboxnum = 223 Or sueboxnum = 279 Or sueboxnum = 307 Or sueboxnum = 699 Or sueboxnum = 727 Or sueboxnum = 867 Or sueboxnum = 895 Then
            suenotalloweddirections = suenotalloweddirections & "left "
        End If
        'determines if sue can turn (is perfectly lined up to turn)
        sueitwillwork = False
        For i As Integer = 0 To 35
            For t As Integer = 0 To 35
                If (sue.X + 5) / 20 = i And (sue.Y + 5) / 20 = t Then
                    sueitwillwork = True
                End If
            Next
        Next
        'targeting
        If suemode = "chase" Then
            If Math.Sqrt(Math.Pow(sue.X - pacman.X, 2) + Math.Pow(sue.Y - pacman.Y, 2)) <= 200 Or suemode = "scatter" Then
                suetargetx = 1
                suetargety = 33
            Else
                suetargetx = x
                suetargety = y
            End If
        ElseIf suemode = "eyes" Then
            suetargetx = 14
            suetargety = 14
        End If
        'if he not frightened and can turn then...
        If suemode = "chase" Or suemode = "scatter" Or suemode = "eyes" Then
            If sueitwillwork = True Then
                If suenotalloweddirections.Contains("up") = False Then
                    suedistances(0) = Math.Sqrt((sueX - suetargetx) ^ 2 + (sueY - 1 - suetargety) ^ 2)
                End If
                If suenotalloweddirections.Contains("down") = False Then
                    suedistances(1) = Math.Sqrt((sueX - suetargetx) ^ 2 + (sueY + 1 - suetargety) ^ 2)
                End If
                If suenotalloweddirections.Contains("left") = False Then
                    suedistances(2) = Math.Sqrt((sueX - 1 - suetargetx) ^ 2 + (sueY - suetargety) ^ 2)
                End If
                If suenotalloweddirections.Contains("right") = False Then
                    suedistances(3) = Math.Sqrt((sueX + 1 - suetargetx) ^ 2 + (sueY - suetargety) ^ 2)
                End If
                If suedistances.Min = suedistances(0) Then
                    suedirection = "up"
                End If
                If suedistances.Min = suedistances(1) Then
                    suedirection = "down"
                End If
                If suedistances.Min = suedistances(2) Then
                    suedirection = "left"
                End If
                If suedistances.Min = suedistances(3) Then
                    suedirection = "right"
                End If
            End If
            If suemode = "eyes" And sueX = suetargetx And sueY = suetargety Then 'reached the home box
                suemode = "restart"
                sue.X = 285
                sue.Y = 275
                suemove.Stop()
                suedirection = "down"
            End If
            'frightened mode direction selection
        ElseIf suemode = "frightened" Then
            If sueitwillwork = True Then
                suedirection = ""
                While suedirection = ""
                    Randomize()
                    suenewdirection = Int(Rnd() * 6 + 1)
                    If suenewdirection = 1 Then
                        suedirection = "up"
                    ElseIf suenewdirection = 2 Then
                        suedirection = "right"
                    ElseIf suenewdirection = 3 Then
                        suedirection = "down"
                    ElseIf suenewdirection = 4 Then
                        suedirection = "left"
                    End If
                    If suenewdirection = 5 Or suenewdirection = 6 Then
                        suedirection = oldsuedirection
                    End If
                    'can't reverse direction at wall
                    If (oldsuedirection = "up" And suedirection = "down") Or (oldsuedirection = "down" And suedirection = "up") Or (oldsuedirection = "left" And suedirection = "right") Or (oldsuedirection = "right" And suedirection = "left") Then
                        suedirection = ""
                    End If
                    If (sueboxnum >= 114 And sueboxnum <= 126) Or (sueboxnum >= 128 And sueboxnum <= 140) Or (sueboxnum >= 227 And sueboxnum <= 230) Or (sueboxnum >= 232 And sueboxnum <= 236) Or (sueboxnum >= 238 And sueboxnum <= 239) Or (sueboxnum >= 241 And sueboxnum <= 245) Or (sueboxnum >= 247 And sueboxnum <= 251) Or (sueboxnum >= 311 And sueboxnum <= 314) Or (sueboxnum >= 319 And sueboxnum <= 321) Or (sueboxnum >= 324 And sueboxnum <= 326) Or (sueboxnum >= 331 And sueboxnum <= 334) Or (sueboxnum >= 402 And sueboxnum <= 411) Or (sueboxnum >= 477 And sueboxnum <= 482) Or (sueboxnum >= 484 And sueboxnum <= 485) Or (sueboxnum >= 496 And sueboxnum <= 497) Or (sueboxnum >= 499 And sueboxnum <= 504) Or (sueboxnum >= 571 And sueboxnum <= 578) Or (sueboxnum >= 646 And sueboxnum <= 650) Or (sueboxnum >= 652 And sueboxnum <= 653) Or (sueboxnum >= 655 And sueboxnum <= 657) Or (sueboxnum >= 660 And sueboxnum <= 662) Or (sueboxnum >= 664 And sueboxnum <= 665) Or (sueboxnum >= 667 And sueboxnum <= 671) Or (sueboxnum >= 731 And sueboxnum <= 732) Or (sueboxnum >= 736 And sueboxnum <= 749) Or (sueboxnum >= 753 And sueboxnum <= 754) Or (sueboxnum >= 814 And sueboxnum <= 815) Or (sueboxnum >= 817 And sueboxnum <= 818) Or (sueboxnum >= 823 And sueboxnum <= 825) Or (sueboxnum >= 828 And sueboxnum <= 830) Or (sueboxnum >= 835 And sueboxnum <= 836) Or (sueboxnum >= 838 And sueboxnum <= 839) Or (sueboxnum >= 899 And sueboxnum <= 908) Or (sueboxnum >= 910 And sueboxnum <= 911) Or (sueboxnum >= 913 And sueboxnum <= 922) Then
                        If suedirection = "up" Then
                            suedirection = ""
                        End If
                    End If
                    If (sueboxnum >= 115 And sueboxnum <= 118) Or (sueboxnum >= 120 And sueboxnum <= 124) Or (sueboxnum >= 129 And sueboxnum <= 133) Or (sueboxnum >= 135 And sueboxnum <= 138) Or (sueboxnum >= 227 And sueboxnum <= 230) Or (sueboxnum >= 232 And sueboxnum <= 233) Or (sueboxnum >= 235 And sueboxnum <= 242) Or (sueboxnum >= 244 And sueboxnum <= 245) Or (sueboxnum >= 247 And sueboxnum <= 250) Or (sueboxnum >= 310 And sueboxnum <= 314) Or (sueboxnum >= 318 And sueboxnum <= 320) Or (sueboxnum >= 325 And sueboxnum <= 327) Or (sueboxnum >= 331 And sueboxnum <= 335) Or (sueboxnum >= 403 And sueboxnum <= 410) Or (sueboxnum >= 477 And sueboxnum <= 482) Or (sueboxnum >= 484 And sueboxnum <= 485) Or (sueboxnum >= 496 And sueboxnum <= 497) Or (sueboxnum >= 499 And sueboxnum <= 504) Or (sueboxnum >= 571 And sueboxnum <= 578) Or (sueboxnum >= 647 And sueboxnum <= 650) Or (sueboxnum >= 652 And sueboxnum <= 656) Or (sueboxnum >= 661 And sueboxnum <= 665) Or (sueboxnum >= 667 And sueboxnum <= 670) Or (sueboxnum >= 730 And sueboxnum <= 731) Or (sueboxnum >= 736 And sueboxnum <= 737) Or (sueboxnum >= 739 And sueboxnum <= 746) Or (sueboxnum >= 748 And sueboxnum <= 749) Or (sueboxnum >= 754 And sueboxnum <= 755) Or (sueboxnum >= 815 And sueboxnum <= 819) Or (sueboxnum >= 822 And sueboxnum <= 824) Or (sueboxnum >= 829 And sueboxnum <= 831) Or (sueboxnum >= 834 And sueboxnum <= 838) Or (sueboxnum >= 898 And sueboxnum <= 923) Then
                        If suedirection = "down" Then
                            suedirection = ""
                        End If
                    End If
                    If sueboxnum = 142 Or sueboxnum = 170 Or sueboxnum = 198 Or sueboxnum = 254 Or sueboxnum = 282 Or sueboxnum = 674 Or sueboxnum = 702 Or sueboxnum = 842 Or sueboxnum = 870 Or sueboxnum = 732 Or sueboxnum = 760 Or sueboxnum = 788 Or sueboxnum = 147 Or sueboxnum = 175 Or sueboxnum = 203 Or sueboxnum = 259 Or sueboxnum = 287 Or sueboxnum = 315 Or sueboxnum = 343 Or sueboxnum = 371 Or sueboxnum = 399 Or sueboxnum = 427 Or sueboxnum = 455 Or sueboxnum = 511 Or sueboxnum = 539 Or sueboxnum = 567 Or sueboxnum = 595 Or sueboxnum = 623 Or sueboxnum = 679 Or sueboxnum = 707 Or sueboxnum = 763 Or sueboxnum = 791 Or sueboxnum = 819 Or sueboxnum = 262 Or sueboxnum = 290 Or sueboxnum = 430 Or sueboxnum = 458 Or sueboxnum = 486 Or sueboxnum = 514 Or sueboxnum = 542 Or sueboxnum = 598 Or sueboxnum = 626 Or sueboxnum = 766 Or sueboxnum = 794 Or sueboxnum = 125 Or sueboxnum = 153 Or sueboxnum = 181 Or sueboxnum = 209 Or sueboxnum = 321 Or sueboxnum = 349 Or sueboxnum = 377 Or sueboxnum = 657 Or sueboxnum = 685 Or sueboxnum = 713 Or sueboxnum = 825 Or sueboxnum = 853 Or sueboxnum = 881 Or sueboxnum = 156 Or sueboxnum = 184 Or sueboxnum = 212 Or sueboxnum = 352 Or sueboxnum = 380 Or sueboxnum = 688 Or sueboxnum = 716 Or sueboxnum = 856 Or sueboxnum = 884 Or sueboxnum = 271 Or sueboxnum = 299 Or sueboxnum = 327 Or sueboxnum = 411 Or sueboxnum = 439 Or sueboxnum = 467 Or sueboxnum = 523 Or sueboxnum = 551 Or sueboxnum = 579 Or sueboxnum = 607 Or sueboxnum = 635 Or sueboxnum = 775 Or sueboxnum = 803 Or sueboxnum = 831 Or sueboxnum = 139 Or sueboxnum = 167 Or sueboxnum = 195 Or sueboxnum = 223 Or sueboxnum = 251 Or sueboxnum = 279 Or sueboxnum = 307 Or sueboxnum = 335 Or sueboxnum = 671 Or sueboxnum = 699 Or sueboxnum = 727 Or sueboxnum = 755 Or sueboxnum = 839 Or sueboxnum = 867 Or sueboxnum = 895 Or sueboxnum = 923 Or sueboxnum = 781 Or sueboxnum = 809 Or sueboxnum = 162 Or sueboxnum = 190 Or sueboxnum = 218 Or sueboxnum = 274 Or sueboxnum = 302 Or sueboxnum = 358 Or sueboxnum = 386 Or sueboxnum = 414 Or sueboxnum = 442 Or sueboxnum = 470 Or sueboxnum = 526 Or sueboxnum = 554 Or sueboxnum = 582 Or sueboxnum = 610 Or sueboxnum = 638 Or sueboxnum = 694 Or sueboxnum = 722 Or sueboxnum = 750 Or sueboxnum = 778 Or sueboxnum = 806 Then
                        If suedirection = "right" Then
                            suedirection = ""
                        End If
                    End If
                    If sueboxnum = 114 Or sueboxnum = 142 Or sueboxnum = 170 Or sueboxnum = 198 Or sueboxnum = 226 Or sueboxnum = 254 Or sueboxnum = 282 Or sueboxnum = 310 Or sueboxnum = 646 Or sueboxnum = 674 Or sueboxnum = 702 Or sueboxnum = 730 Or sueboxnum = 814 Or sueboxnum = 842 Or sueboxnum = 870 Or sueboxnum = 898 Or sueboxnum = 760 Or sueboxnum = 788 Or sueboxnum = 147 Or sueboxnum = 175 Or sueboxnum = 203 Or sueboxnum = 252 Or sueboxnum = 287 Or sueboxnum = 343 Or sueboxnum = 371 Or sueboxnum = 399 Or sueboxnum = 427 Or sueboxnum = 455 Or sueboxnum = 511 Or sueboxnum = 539 Or sueboxnum = 567 Or sueboxnum = 595 Or sueboxnum = 623 Or sueboxnum = 679 Or sueboxnum = 707 Or sueboxnum = 735 Or sueboxnum = 763 Or sueboxnum = 791 Or sueboxnum = 262 Or sueboxnum = 290 Or sueboxnum = 318 Or sueboxnum = 402 Or sueboxnum = 430 Or sueboxnum = 458 Or sueboxnum = 514 Or sueboxnum = 542 Or sueboxnum = 570 Or sueboxnum = 598 Or sueboxnum = 626 Or sueboxnum = 766 Or sueboxnum = 794 Or sueboxnum = 822 Or sueboxnum = 153 Or sueboxnum = 181 Or sueboxnum = 209 Or sueboxnum = 349 Or sueboxnum = 377 Or sueboxnum = 685 Or sueboxnum = 713 Or sueboxnum = 853 Or sueboxnum = 881 Or sueboxnum = 128 Or sueboxnum = 156 Or sueboxnum = 184 Or sueboxnum = 212 Or sueboxnum = 324 Or sueboxnum = 352 Or sueboxnum = 380 Or sueboxnum = 660 Or sueboxnum = 688 Or sueboxnum = 716 Or sueboxnum = 828 Or sueboxnum = 856 Or sueboxnum = 884 Or sueboxnum = 271 Or sueboxnum = 299 Or sueboxnum = 439 Or sueboxnum = 467 Or sueboxnum = 495 Or sueboxnum = 523 Or sueboxnum = 551 Or sueboxnum = 607 Or sueboxnum = 635 Or sueboxnum = 775 Or sueboxnum = 803 Or sueboxnum = 162 Or sueboxnum = 190 Or sueboxnum = 218 Or sueboxnum = 274 Or sueboxnum = 302 Or sueboxnum = 330 Or sueboxnum = 358 Or sueboxnum = 386 Or sueboxnum = 414 Or sueboxnum = 442 Or sueboxnum = 470 Or sueboxnum = 526 Or sueboxnum = 554 Or sueboxnum = 582 Or sueboxnum = 610 Or sueboxnum = 638 Or sueboxnum = 694 Or sueboxnum = 722 Or sueboxnum = 778 Or sueboxnum = 806 Or sueboxnum = 834 Or sueboxnum = 753 Or sueboxnum = 781 Or sueboxnum = 809 Or sueboxnum = 167 Or sueboxnum = 195 Or sueboxnum = 223 Or sueboxnum = 279 Or sueboxnum = 307 Or sueboxnum = 699 Or sueboxnum = 727 Or sueboxnum = 867 Or sueboxnum = 895 Then
                        If suedirection = "left" Then
                            suedirection = ""
                        End If
                    End If
                End While
            End If
        End If
        'movement control
        If suedirection = "up" Then
            sueycounter -= 5
            sue.Offset(0, -5)
        ElseIf suedirection = "down" Then
            sueycounter += 5
            sue.Offset(0, 5)
        ElseIf suedirection = "left" Then
            If sueboxnum = 477 Then
                sue.X = 555
                sueX = 28
            Else
                suexcounter -= 5
                sue.Offset(-5, 0)
            End If
        ElseIf suedirection = "right" Then
            If sueboxnum = 504 Then
                sue.X = 15
                sueX = 1
            Else
                suexcounter += 5
                sue.Offset(5, 0)
            End If
        End If

        'finds edge of box
        If suexcounter = 20 Then
            sueX += 1
            suexcounter = 0
        End If
        If suexcounter = -20 Then
            sueX -= 1
            suexcounter = 0
        End If
        If sueycounter = 20 Then
            sueY += 1
            sueycounter = 0
        End If
        If sueycounter = -20 Then
            sueY -= 1
            sueycounter = 0
        End If
        sueboxnum = sueY * 28 + sueX
        
    End Sub
    Private Sub runtimer_Tick(sender As System.Object, e As System.EventArgs) Handles runtimer.Tick
        'controls what happens when ghosts become frightened
        'ghost flash near the end of frightened mode
        If runcounter = 18 Then
            runtimer.Stop()
            scatter.Start()
            blinkyflash = False
            pinkyflash = False
            inkyflash = False
            sueflash = False
            If blinkymode = "frightened" Then
                blinkymode = "chase"
                blinkymove.Interval = 32
            End If
            If pinkymode = "frightened" Then
                pinkymode = "chase"
                pinkymove.Interval = 35
            End If
            If inkymode = "frightened" Then
                inkymode = "chase"
                inkymove.Interval = 35
            End If
            If suemode = "frightened" Then
                suemode = "chase"
                suemove.Interval = 35
            End If
            runcounter = 0
        End If
        If runcounter = 13 Or runcounter = 15 Or runcounter = 17 Then
            If blinkymode = "frightened" Then
                blinkyflash = True
            End If
            If pinkymode = "frightened" Then
                pinkyflash = True
            End If
            If inkymode = "frightened" Then
                inkyflash = True
            End If
            If suemode = "frightened" Then
                sueflash = True
            End If
        ElseIf runcounter = 14 Or runcounter = 16 Then
            blinkyflash = False
            pinkyflash = False
            inkyflash = False
            sueflash = False
        End If
        runcounter += 1
    End Sub
    Private Sub ending_Tick(sender As System.Object, e As System.EventArgs) Handles ending.Tick
        'at the end the maze flashes. this does that.
        If endcounter = 14 Or endcounter = 18 Or endcounter = 22 Or endcounter = 26 Then
            mypen = Pens.BlanchedAlmond
        ElseIf endcounter = 16 Or endcounter = 20 Or endcounter = 24 Or endcounter = 28 Then
            mypen = Pens.Blue
        ElseIf endcounter = 30 Then
            ending.Stop()
            MessageBox.Show("You Win!" & vbCrLf & "Your score was: " & score & " out of a possible 14860.")
            Me.Close()
        End If
        endcounter += 1
    End Sub
    Private Sub scatter_Tick(sender As System.Object, e As System.EventArgs) Handles scatter.Tick
        'the ghosts go into scatter mode every so often where their target square changes to a corner.  this controls when they go into scatter mode.
        If scattercounter < 5 Then
            If blinkymode <> "eyes" And blinkymode <> "begin" And blinkymode <> "restart" And blinkymode <> "start" Then 'makes sure to allow the "eye" and "begin" sequences to complete before changing it to scatter
                blinkymode = "scatter"
            End If
            If pinkymode <> "eyes" And pinkymode <> "begin" And pinkymode <> "restart" And pinkymode <> "start" Then
                pinkymode = "scatter"
            End If
            If inkymode <> "eyes" And inkymode <> "begin" And inkymode <> "restart" And inkymode <> "start" Then
                inkymode = "scatter"
            End If
            If suemode <> "eyes" And suemode <> "begin" And suemode <> "restart" And suemode <> "start" Then
                suemode = "scatter"
            End If
        ElseIf scattercounter < 25 Then
            If blinkymode <> "eyes" And blinkymode <> "begin" And blinkymode <> "restart" And blinkymode <> "start" Then
                blinkymode = "chase"
            End If
            If pinkymode <> "eyes" And pinkymode <> "begin" And pinkymode <> "restart" And pinkymode <> "start" Then
                pinkymode = "chase"
            End If
            If inkymode <> "eyes" And inkymode <> "begin" And inkymode <> "restart" And inkymode <> "start" Then
                inkymode = "chase"
            End If
            If suemode <> "eyes" And suemode <> "begin" And suemode <> "restart" And suemode <> "start" Then
                suemode = "chase"
            End If
        ElseIf scattercounter < 30 Then
            If blinkymode <> "eyes" And blinkymode <> "begin" And blinkymode <> "restart" And blinkymode <> "start" Then
                blinkymode = "scatter"
            End If
            If pinkymode <> "eyes" And pinkymode <> "begin" And pinkymode <> "restart" And pinkymode <> "start" Then
                pinkymode = "scatter"
            End If
            If inkymode <> "eyes" And inkymode <> "begin" And inkymode <> "restart" And inkymode <> "start" Then
                inkymode = "scatter"
            End If
            If suemode <> "eyes" And suemode <> "begin" And suemode <> "restart" And suemode <> "start" Then
                suemode = "scatter"
            End If
        ElseIf scattercounter < 50 Then
            If blinkymode <> "eyes" And blinkymode <> "begin" And blinkymode <> "restart" And blinkymode <> "start" Then
                blinkymode = "chase"
            End If
            If pinkymode <> "eyes" And pinkymode <> "begin" And pinkymode <> "restart" And pinkymode <> "start" Then
                pinkymode = "chase"
            End If
            If inkymode <> "eyes" And inkymode <> "begin" And inkymode <> "restart" And inkymode <> "start" Then
                inkymode = "chase"
            End If
            If suemode <> "eyes" And suemode <> "begin" And suemode <> "restart" And suemode <> "start" Then
                suemode = "chase"
            End If
        ElseIf scattercounter < 55 Then
            If blinkymode <> "eyes" And blinkymode <> "begin" And blinkymode <> "restart" And blinkymode <> "start" Then
                blinkymode = "scatter"
            End If
            If pinkymode <> "eyes" And pinkymode <> "begin" And pinkymode <> "restart" And pinkymode <> "start" Then
                pinkymode = "scatter"
            End If
            If inkymode <> "eyes" And inkymode <> "begin" And inkymode <> "restart" And inkymode <> "start" Then
                inkymode = "scatter"
            End If
            If suemode <> "eyes" And suemode <> "begin" And suemode <> "restart" And suemode <> "start" Then
                suemode = "scatter"
            End If
        ElseIf scattercounter > 55 Then
            If blinkymode <> "eyes" And blinkymode <> "begin" And blinkymode <> "restart" And blinkymode <> "start" Then
                blinkymode = "chase"
            End If
            If pinkymode <> "eyes" And pinkymode <> "begin" And pinkymode <> "restart" And pinkymode <> "start" Then
                pinkymode = "chase"
            End If
            If inkymode <> "eyes" And inkymode <> "begin" And inkymode <> "restart" And inkymode <> "start" Then
                inkymode = "chase"
            End If
            If suemode <> "eyes" And suemode <> "begin" And suemode <> "restart" And suemode <> "start" Then
                suemode = "chase"
            End If
        End If
        If scattercounter = 5 Or scattercounter = 25 Or scattercounter = 30 Or scattercounter = 50 Or scattercounter = 55 Then
            If blinkydirection = "up" Then 'reverse ghost directions when they go into scatter mode
                blinkydirection = "down"
            ElseIf blinkydirection = "down" Then
                blinkydirection = "up"
            ElseIf blinkydirection = "left" Then
                blinkydirection = "right"
            ElseIf blinkydirection = "right" Then
                blinkydirection = "left"
            End If
            If pinkydirection = "up" Then
                pinkydirection = "down"
            ElseIf pinkydirection = "down" Then
                pinkydirection = "up"
            ElseIf pinkydirection = "left" Then
                pinkydirection = "right"
            ElseIf pinkydirection = "right" Then
                pinkydirection = "left"
            End If
            If inkydirection = "up" Then
                inkydirection = "down"
            ElseIf inkydirection = "down" Then
                inkydirection = "up"
            ElseIf inkydirection = "left" Then
                inkydirection = "right"
            ElseIf inkydirection = "right" Then
                inkydirection = "left"
            End If
            If suedirection = "up" Then
                suedirection = "down"
            ElseIf suedirection = "down" Then
                suedirection = "up"
            ElseIf suedirection = "left" Then
                suedirection = "right"
            ElseIf suedirection = "right" Then
                suedirection = "left"
            End If
        End If
        If blinkymode <> "frightened" Or pinkymode <> "frightened" Or inkymode <> "frightened" Or suemode <> "frightened" Then
            scattercounter += 1
        End If
    End Sub
    Private Sub starting_Tick(sender As System.Object, e As System.EventArgs) Handles starting.Tick
        If inkymode = "begin" Then 'begin mode is when the ghosts come out of the box
            If startcounter > 0 And startcounter < 8 Then 'move to the side if necessary
                inky.Offset(5, 0)
            ElseIf startcounter >= 8 Then 'move up until hes in the right place
                inkydirection = "up"
                If (inky.Y + 5) / 20 <> inkyY Then
                    inky.Offset(0, -5)
                Else
                    inkymove.Enabled = True 'when in the right place, start him moving
                    inky.X = 285 'put him in place just to be safe
                    inky.Y = 275
                    inkymove.Start()
                    inkymove.Interval = 35
                    inkyxcounter = 10
                    inkydirection = "left"
                    inkyycounter = 0
                    inkyX = 14
                    inkyY = 14
                    inkyflash = False
                    inkyitwillwork = False
                    inkymode = "chase"
                End If
            End If
        ElseIf inkymode = "restart" Then 'going back into the box after turning into eyes
            inkydirection = "down"
            If inky.Y <> 340 Then
                inky.Offset(0, 5) 'moves down until in the right place
            Else
                inkymode = "start" 'when placed, change to start
            End If
        ElseIf inkymode = "start" Then
            inkydirection = "up"
            If (inky.Y + 5) / 20 <> 14 Then 'moving up to the right place
                inky.Offset(0, -5)
            Else
                inkymove.Start() 'start everything
                inky.X = 285
                inky.Y = 275
                inkymove.Interval = 35
                inkyxcounter = 10
                inkydirection = "left"
                inkyycounter = 0
                inkyX = 14
                inkyY = 14
                inkyflash = False
                inkyitwillwork = False
                inkymode = "chase"
            End If
        End If
        If suemode = "begin" Then 'same as inky
            If startcounter > 20 And startcounter < 28 Then
                sue.Offset(-5, 0)
            ElseIf startcounter >= 28 Then
                suedirection = "up"
                If (sue.Y + 5) / 20 <> sueY Then
                    sue.Offset(0, -5)
                Else
                    suemove.Enabled = True
                    sue.X = 285
                    sue.Y = 275
                    suemove.Start()
                    suemove.Interval = 35
                    suexcounter = 10
                    suedirection = "right"
                    sueycounter = 0
                    sueX = 14
                    sueY = 14
                    sueflash = False
                    sueitwillwork = False
                    suemode = "chase"
                End If
            End If
        ElseIf suemode = "restart" Then
            suedirection = "down"
            If sue.Y <> 340 Then
                sue.Offset(0, 5)
            Else
                suemode = "start"
            End If
        ElseIf suemode = "start" Then
            suedirection = "up"
            If (sue.Y + 5) / 20 <> 14 Then
                sue.Offset(0, -5)
            Else
                suemove.Start()
                sue.X = 285
                sue.Y = 275
                suemove.Interval = 35
                suexcounter = 10
                suedirection = "right"
                sueycounter = 0
                sueX = 14
                sueY = 14
                sueflash = False
                sueitwillwork = False
                suemode = "chase"
            End If
        End If
        If blinkymode = "restart" Then 'same as inky but doesn't have the begin mode cause blinky's already out of the box
            blinkydirection = "down"
            If blinky.Y <> 340 Then
                blinky.Offset(0, 5)
            Else
                blinkymode = "start"
            End If
        ElseIf blinkymode = "start" Then
            blinkydirection = "up"
            If (blinky.Y + 5) / 20 <> 14 Then
                blinky.Offset(0, -5)
            Else
                blinkymove.Start()
                blinky.X = 285
                blinky.Y = 275
                blinkymove.Interval = 35
                blinkyxcounter = 10
                blinkydirection = "left"
                blinkyycounter = 0
                blinkyX = 14
                blinkyY = 14
                blinkyflash = False
                blinkyitwillwork = False
                blinkymode = "chase"
            End If
        End If
        If pinkymode = "begin" Then 'same as inky but doesnt have pinky move up in begin phase cause pinky's centered
            If (pinky.Y + 5) / 20 <> pinkyY Then
                pinky.Offset(0, -5)
            Else
                pinkymove.Enabled = True
                pinky.X = 285
                pinky.Y = 275
                pinkymove.Start()
                pinkymove.Interval = 35
                pinkyxcounter = 10
                pinkydirection = "right"
                pinkyycounter = 0
                pinkyX = 14
                pinkyY = 14
                pinkyflash = False
                pinkyitwillwork = False
                pinkymode = "chase"
            End If
        ElseIf pinkymode = "restart" Then
            pinkydirection = "down"
            If pinky.Y <> 340 Then
                pinky.Offset(0, 5)
            Else
                pinkymode = "start"
            End If
        ElseIf pinkymode = "start" Then
            pinkydirection = "up"
            If (pinky.Y + 5) / 20 <> 14 Then
                pinky.Offset(0, -5)
            Else
                pinkymove.Start()
                pinky.X = 285
                pinky.Y = 275
                pinkymove.Interval = 35
                pinkyxcounter = 10
                pinkydirection = "right"
                pinkyycounter = 0
                pinkyX = 14
                pinkyY = 14
                pinkyflash = False
                pinkyitwillwork = False
                pinkymode = "chase"
            End If
        End If
        If startcounter < 50 Then
            startcounter += 1
        End If
    End Sub
    Private Sub fruit_timer_Tick(sender As Object, e As EventArgs) Handles fruit_timer.Tick
        fruitpresent = False
        fruit_timer.Stop()
    End Sub
End Class