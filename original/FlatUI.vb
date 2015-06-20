Imports System.Drawing.Drawing2D, System.ComponentModel, System.Windows.Forms

''' <summary>
''' Flat UI Theme
''' Creator: iSynthesis (HF)
''' Version: 1.0.4
''' Date Created: 17/06/2013
''' Date Changed: 26/06/2013
''' UID: 374648
''' For any bugs / errors, PM me.
''' </summary>
''' <remarks></remarks>

Module Helpers

#Region " Variables"
    Friend G As Graphics, B As Bitmap
    Friend _FlatColor As Color = Color.FromArgb(35, 168, 109)
    Friend NearSF As New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near}
    Friend CenterSF As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
#End Region

#Region " Functions"

    Public Function RoundRec(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function

    Public Function RoundRect(x!, y!, w!, h!, Optional r! = 0.3, Optional TL As Boolean = True, Optional TR As Boolean = True, Optional BR As Boolean = True, Optional BL As Boolean = True) As GraphicsPath
        Dim d! = Math.Min(w, h) * r, xw = x + w, yh = y + h
        RoundRect = New GraphicsPath

        With RoundRect
            If TL Then .AddArc(x, y, d, d, 180, 90) Else .AddLine(x, y, x, y)
            If TR Then .AddArc(xw - d, y, d, d, 270, 90) Else .AddLine(xw, y, xw, y)
            If BR Then .AddArc(xw - d, yh - d, d, d, 0, 90) Else .AddLine(xw, yh, xw, yh)
            If BL Then .AddArc(x, yh - d, d, d, 90, 90) Else .AddLine(x, yh, x, yh)

            .CloseFigure()
        End With
    End Function

    '-- Credit: AeonHack
    Public Function DrawArrow(x As Integer, y As Integer, flip As Boolean) As GraphicsPath
        Dim GP As New GraphicsPath()

        Dim W As Integer = 12
        Dim H As Integer = 6

        If flip Then
            GP.AddLine(x + 1, y, x + W + 1, y)
            GP.AddLine(x + W, y, x + H, y + H - 1)
        Else
            GP.AddLine(x, y + H, x + W, y + H)
            GP.AddLine(x + W, y + H, x + H, y)
        End If

        GP.CloseFigure()
        Return GP
    End Function

#End Region

End Module

#Region " Mouse States"

Enum MouseState As Byte
    None = 0
    Over = 1
    Down = 2
    Block = 3
End Enum

#End Region

Class FormSkin : Inherits ContainerControl

#Region " Variables"

    Private W, H As Integer
    Private Cap As Boolean = False
    Private _HeaderMaximize As Boolean = False
    Private MousePoint As New Point(0, 0)
    Private MoveHeight = 50

#End Region

#Region " Properties"

#Region " Colors"

    <Category("Colors")> _
    Public Property HeaderColor() As Color
        Get
            Return _HeaderColor
        End Get
        Set(value As Color)
            _HeaderColor = value
        End Set
    End Property
    <Category("Colors")> _
    Public Property BaseColor() As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property
    <Category("Colors")> _
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
        End Set
    End Property
    <Category("Colors")> _
    Public Property FlatColor() As Color
        Get
            Return _FlatColor
        End Get
        Set(value As Color)
            _FlatColor = value
        End Set
    End Property

#End Region

#Region " Options"

    <Category("Options")> _
    Public Property HeaderMaximize As Boolean
        Get
            Return _HeaderMaximize
        End Get
        Set(value As Boolean)
            _HeaderMaximize = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(0, 0, Width, MoveHeight).Contains(e.Location) Then
            Cap = True
            MousePoint = e.Location
        End If
    End Sub

    Private Sub FormSkin_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        If HeaderMaximize Then
            If e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(0, 0, Width, MoveHeight).Contains(e.Location) Then
                If FindForm.WindowState = FormWindowState.Normal Then
                    FindForm.WindowState = FormWindowState.Maximized : FindForm.Refresh()
                ElseIf FindForm.WindowState = FormWindowState.Maximized Then
                    FindForm.WindowState = FormWindowState.Normal : FindForm.Refresh()
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e) : Cap = False
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Cap Then
            Parent.Location = MousePosition - MousePoint
        End If
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        ParentForm.FormBorderStyle = FormBorderStyle.None
        ParentForm.AllowTransparency = False
        ParentForm.TransparencyKey = Color.Fuchsia
        ParentForm.FindForm.StartPosition = FormStartPosition.CenterScreen
        Dock = DockStyle.Fill
        Invalidate()
    End Sub

#End Region

#Region " Colors"

#Region " Dark Colors"

    Private _HeaderColor As Color = Color.FromArgb(45, 47, 49)
    Private _BaseColor As Color = Color.FromArgb(60, 70, 73)
    Private _BorderColor As Color = Color.FromArgb(53, 58, 60)
    Private TextColor As Color = Color.FromArgb(234, 234, 234)

#End Region

#Region " Light Colors"

    Private _HeaderLight As Color = Color.FromArgb(171, 171, 172)
    Private _BaseLight As Color = Color.FromArgb(196, 199, 200)
    Public TextLight As Color = Color.FromArgb(45, 47, 49)

#End Region

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.White
        Font = New Font("Segoe UI", 12)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim Base As New Rectangle(0, 0, W, H), Header As New Rectangle(0, 0, W, 50)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)

            '-- Header
            .FillRectangle(New SolidBrush(_HeaderColor), Header)

            '-- Logo
            .FillRectangle(New SolidBrush(Color.FromArgb(243, 243, 243)), New Rectangle(8, 16, 4, 18))
            .FillRectangle(New SolidBrush(_FlatColor), 16, 16, 4, 18)
            .DrawString(Text, Font, New SolidBrush(TextColor), New Rectangle(26, 15, W, H), NearSF)

            '-- Border
            .DrawRectangle(New Pen(_BorderColor), Base)
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatClose : Inherits Control

#Region " Variables"

    Private State As MouseState = MouseState.None
    Private x As Integer

#End Region

#Region " Properties"

#Region " Mouse States"

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Environment.Exit(0)
    End Sub

#End Region
    
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(18, 18)
    End Sub

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

#End Region

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(168, 35, 35)
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.White
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Font = New Font("Marlett", 10)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)

            '-- X
            .DrawString("r", Font, New SolidBrush(TextColor), New Rectangle(0, 0, Width, Height), CenterSF)

            '-- Hover/down
            Select Case State
                Case MouseState.Over
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                Case MouseState.Down
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatMax : Inherits Control

#Region " Variables"

    Private State As MouseState = MouseState.None
    Private x As Integer

#End Region

#Region " Properties"

#Region " Mouse States"

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Select Case FindForm.WindowState
            Case FormWindowState.Maximized
                FindForm.WindowState = FormWindowState.Normal
            Case FormWindowState.Normal
                FindForm.WindowState = FormWindowState.Maximized
        End Select
    End Sub

#End Region

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(18, 18)
    End Sub

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.White
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Font = New Font("Marlett", 12)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)

            '-- Maximize
            If FindForm.WindowState = FormWindowState.Maximized Then
                .DrawString("1", Font, New SolidBrush(TextColor), New Rectangle(1, 1, Width, Height), CenterSF)
            ElseIf FindForm.WindowState = FormWindowState.Normal Then
                .DrawString("2", Font, New SolidBrush(TextColor), New Rectangle(1, 1, Width, Height), CenterSF)
            End If

            '-- Hover/down
            Select Case State
                Case MouseState.Over
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                Case MouseState.Down
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatMini : Inherits Control

#Region " Variables"

    Private State As MouseState = MouseState.None
    Private x As Integer

#End Region

#Region " Properties"

#Region " Mouse States"

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Select Case FindForm.WindowState
            Case FormWindowState.Normal
                FindForm.WindowState = FormWindowState.Minimized
            Case FormWindowState.Maximized
                FindForm.WindowState = FormWindowState.Minimized
        End Select
    End Sub

#End Region

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(18, 18)
    End Sub

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.White
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Font = New Font("Marlett", 12)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)

            '-- Minimize
            .DrawString("0", Font, New SolidBrush(TextColor), New Rectangle(2, 1, Width, Height), CenterSF)

            '-- Hover/down
            Select Case State
                Case MouseState.Over
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                Case MouseState.Down
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatColorPalette : Inherits Control

#Region " Variables"

    Private W, H As Integer

#End Region

#Region " Properties"

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Width = 180
        Height = 80
    End Sub

#Region " Colors"

    <Category("Colors")> _
    Public Property Red As Color
        Get
            Return _Red
        End Get
        Set(value As Color)
            _Red = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property Cyan As Color
        Get
            Return _Cyan
        End Get
        Set(value As Color)
            _Cyan = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property Blue As Color
        Get
            Return _Blue
        End Get
        Set(value As Color)
            _Blue = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property LimeGreen As Color
        Get
            Return _LimeGreen
        End Get
        Set(value As Color)
            _LimeGreen = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property Orange As Color
        Get
            Return _Orange
        End Get
        Set(value As Color)
            _Orange = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property Purple As Color
        Get
            Return _Purple
        End Get
        Set(value As Color)
            _Purple = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property Black As Color
        Get
            Return _Black
        End Get
        Set(value As Color)
            _Black = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property Gray As Color
        Get
            Return _Gray
        End Get
        Set(value As Color)
            _Gray = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property White As Color
        Get
            Return _White
        End Get
        Set(value As Color)
            _White = value
        End Set
    End Property

#End Region

#End Region

#Region " Colors"

    Private _Red As Color = Color.FromArgb(220, 85, 96)
    Private _Cyan As Color = Color.FromArgb(10, 154, 157)
    Private _Blue As Color = Color.FromArgb(0, 128, 255)
    Private _LimeGreen As Color = Color.FromArgb(35, 168, 109)
    Private _Orange As Color = Color.FromArgb(253, 181, 63)
    Private _Purple As Color = Color.FromArgb(155, 88, 181)
    Private _Black As Color = Color.FromArgb(45, 47, 49)
    Private _Gray As Color = Color.FromArgb(63, 70, 73)
    Private _White As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.FromArgb(60, 70, 73)
        Size = New Size(160, 80)
        Font = New Font("Segoe UI", 12)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Colors 
            .FillRectangle(New SolidBrush(_Red), New Rectangle(0, 0, 20, 40))
            .FillRectangle(New SolidBrush(_Cyan), New Rectangle(20, 0, 20, 40))
            .FillRectangle(New SolidBrush(_Blue), New Rectangle(40, 0, 20, 40))
            .FillRectangle(New SolidBrush(_LimeGreen), New Rectangle(60, 0, 20, 40))
            .FillRectangle(New SolidBrush(_Orange), New Rectangle(80, 0, 20, 40))
            .FillRectangle(New SolidBrush(_Purple), New Rectangle(100, 0, 20, 40))
            .FillRectangle(New SolidBrush(_Black), New Rectangle(120, 0, 20, 40))
            .FillRectangle(New SolidBrush(_Gray), New Rectangle(140, 0, 20, 40))
            .FillRectangle(New SolidBrush(_White), New Rectangle(160, 0, 20, 40))

            '-- Text
            .DrawString("Color Palette", Font, New SolidBrush(_White), New Rectangle(0, 22, W, H), CenterSF)
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatGroupBox : Inherits ContainerControl

#Region " Variables"

    Private W, H As Integer
    Private _ShowText As Boolean = True

#End Region

#Region " Properties"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    Public Property ShowText As Boolean
        Get
            Return _ShowText
        End Get
        Set(value As Boolean)
            _ShowText = value
        End Set
    End Property

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(60, 70, 73)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.Transparent
        Size = New Size(240, 180)
        Font = New Font("Segoe ui", 10)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim GP, GP2, GP3 As New GraphicsPath
        Dim Base As New Rectangle(8, 8, W - 16, H - 16)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            GP = Helpers.RoundRec(Base, 8)
            .FillPath(New SolidBrush(_BaseColor), GP)

            '-- Arrows
            GP2 = Helpers.DrawArrow(28, 2, False)
            .FillPath(New SolidBrush(_BaseColor), GP2)
            GP3 = Helpers.DrawArrow(28, 8, True)
            .FillPath(New SolidBrush(Color.FromArgb(60, 70, 73)), GP3)

            '-- if ShowText
            If ShowText Then
                .DrawString(Text, Font, New SolidBrush(_FlatColor), New Rectangle(16, 16, W, H), NearSF)
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatButton : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private _Rounded As Boolean = False
    Private State As MouseState = MouseState.None

#End Region

#Region " Properties"

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

    <Category("Options")> _
    Public Property Rounded As Boolean
        Get
            Return _Rounded
        End Get
        Set(value As Boolean)
            _Rounded = value
        End Set
    End Property

#End Region

#Region " Mouse States"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

#End Region

#End Region

#Region " Colors"

    Private _BaseColor As Color = _FlatColor
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Size = New Size(106, 32)
        BackColor = Color.Transparent
        Font = New Font("Segoe UI", 12)
        Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim GP As New GraphicsPath
        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            Select Case State
                Case MouseState.None
                    If Rounded Then
                        '-- Base
                        GP = Helpers.RoundRec(Base, 6)
                        .FillPath(New SolidBrush(_BaseColor), GP)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    Else
                        '-- Base
                        .FillRectangle(New SolidBrush(_BaseColor), Base)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    End If
                Case MouseState.Over
                    If Rounded Then
                        '-- Base
                        GP = Helpers.RoundRec(Base, 6)
                        .FillPath(New SolidBrush(_BaseColor), GP)
                        .FillPath(New SolidBrush(Color.FromArgb(20, Color.White)), GP)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    Else
                        '-- Base
                        .FillRectangle(New SolidBrush(_BaseColor), Base)
                        .FillRectangle(New SolidBrush(Color.FromArgb(20, Color.White)), Base)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    End If
                Case MouseState.Down
                    If Rounded Then
                        '-- Base
                        GP = Helpers.RoundRec(Base, 6)
                        .FillPath(New SolidBrush(_BaseColor), GP)
                        .FillPath(New SolidBrush(Color.FromArgb(20, Color.Black)), GP)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    Else
                        '-- Base
                        .FillRectangle(New SolidBrush(_BaseColor), Base)
                        .FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), Base)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    End If
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

<DefaultEvent("CheckedChanged")> Class FlatToggle : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private O As _Options
    Private _Checked As Boolean = False
    Private State As MouseState = MouseState.None

#End Region

#Region " Properties"
    Public Event CheckedChanged(ByVal sender As Object)

    <Flags()> _
    Enum _Options
        Style1
        Style2
        Style3
        Style4 '-- TODO: New Style
        Style5 '-- TODO: New Style
    End Enum

#Region " Options"

    <Category("Options")> _
    Public Property Options As _Options
        Get
            Return O
        End Get
        Set(value As _Options)
            O = value
        End Set
    End Property

    <Category("Options")> _
    Public Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Width = 76
        Height = 33
    End Sub

#Region " Mouse States"

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        _Checked = Not _Checked
        RaiseEvent CheckedChanged(Me)
    End Sub

#End Region

#End Region

#Region " Colors"

    Private BaseColor As Color = _FlatColor
    Private BaseColorRed As Color = Color.FromArgb(220, 85, 96)
    Private BGColor As Color = Color.FromArgb(84, 85, 86)
    Private ToggleColor As Color = Color.FromArgb(45, 47, 49)
    Private TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.Transparent
        Size = New Size(44, Height + 1)
        Cursor = Cursors.Hand
        Font = New Font("Segoe UI", 10)
        Size = New Size(76, 33)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim GP, GP2 As New GraphicsPath
        Dim Base As New Rectangle(0, 0, W, H), Toggle As New Rectangle(CInt(W \ 2), 0, 38, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            Select Case O
                Case _Options.Style1   '-- Style 1
                    '-- Base
                    GP = Helpers.RoundRec(Base, 6)
                    GP2 = Helpers.RoundRec(Toggle, 6)
                    .FillPath(New SolidBrush(BGColor), GP)
                    .FillPath(New SolidBrush(ToggleColor), GP2)

                    '-- Text
                    .DrawString("OFF", Font, New SolidBrush(BGColor), New Rectangle(19, 1, W, H), CenterSF)

                    If Checked Then
                        '-- Base
                        GP = Helpers.RoundRec(Base, 6)
                        GP2 = Helpers.RoundRec(New Rectangle(CInt(W \ 2), 0, 38, H), 6)
                        .FillPath(New SolidBrush(ToggleColor), GP)
                        .FillPath(New SolidBrush(BaseColor), GP2)

                        '-- Text
                        .DrawString("ON", Font, New SolidBrush(BaseColor), New Rectangle(8, 7, W, H), NearSF)
                    End If
                Case _Options.Style2   '-- Style 2
                    '-- Base
                    GP = Helpers.RoundRec(Base, 6)
                    Toggle = New Rectangle(4, 4, 36, H - 8)
                    GP2 = Helpers.RoundRec(Toggle, 4)
                    .FillPath(New SolidBrush(BaseColorRed), GP)
                    .FillPath(New SolidBrush(ToggleColor), GP2)

                    '-- Lines
                    .DrawLine(New Pen(BGColor), 18, 20, 18, 12)
                    .DrawLine(New Pen(BGColor), 22, 20, 22, 12)
                    .DrawLine(New Pen(BGColor), 26, 20, 26, 12)

                    '-- Text
                    .DrawString("r", New Font("Marlett", 8), New SolidBrush(TextColor), New Rectangle(19, 2, Width, Height), CenterSF)

                    If Checked Then
                        GP = Helpers.RoundRec(Base, 6)
                        Toggle = New Rectangle(CInt(W \ 2) - 2, 4, 36, H - 8)
                        GP2 = Helpers.RoundRec(Toggle, 4)
                        .FillPath(New SolidBrush(BaseColor), GP)
                        .FillPath(New SolidBrush(ToggleColor), GP2)

                        '-- Lines
                        .DrawLine(New Pen(BGColor), CInt(W \ 2) + 12, 20, CInt(W \ 2) + 12, 12)
                        .DrawLine(New Pen(BGColor), CInt(W \ 2) + 16, 20, CInt(W \ 2) + 16, 12)
                        .DrawLine(New Pen(BGColor), CInt(W \ 2) + 20, 20, CInt(W \ 2) + 20, 12)

                        '-- Text
                        .DrawString("ü", New Font("Wingdings", 14), New SolidBrush(TextColor), New Rectangle(8, 7, Width, Height), NearSF)
                    End If
                Case _Options.Style3   '-- Style 3
                    '-- Base
                    GP = Helpers.RoundRec(Base, 16)
                    Toggle = New Rectangle(W - 28, 4, 22, H - 8)
                    GP2.AddEllipse(Toggle)
                    .FillPath(New SolidBrush(ToggleColor), GP)
                    .FillPath(New SolidBrush(BaseColorRed), GP2)

                    '-- Text
                    .DrawString("OFF", Font, New SolidBrush(BaseColorRed), New Rectangle(-12, 2, W, H), CenterSF)

                    If Checked Then
                        '-- Base
                        GP = Helpers.RoundRec(Base, 16)
                        Toggle = New Rectangle(6, 4, 22, H - 8)
                        GP2.Reset()
                        GP2.AddEllipse(Toggle)
                        .FillPath(New SolidBrush(ToggleColor), GP)
                        .FillPath(New SolidBrush(BaseColor), GP2)

                        '-- Text
                        .DrawString("ON", Font, New SolidBrush(BaseColor), New Rectangle(12, 2, W, H), CenterSF)
                    End If
                Case _Options.Style4
                    '-- TODO: New Styles
                    If Checked Then
                        '--
                    End If
                Case _Options.Style5
                    '-- TODO: New Styles
                    If Checked Then
                        '--
                    End If
            End Select

        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

<DefaultEvent("CheckedChanged")> Class RadioButton : Inherits Control

#Region " Variables"

    Private State As MouseState = MouseState.None
    Private W, H As Integer
    Private O As _Options
    Private _Checked As Boolean

#End Region

#Region " Properties"
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
            InvalidateControls()
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property
    Event CheckedChanged(ByVal sender As Object)
    Protected Overrides Sub OnClick(e As EventArgs)
        If Not _Checked Then Checked = True
        MyBase.OnClick(e)
    End Sub
    Private Sub InvalidateControls()
        If Not IsHandleCreated OrElse Not _Checked Then Return
        For Each C As Control In Parent.Controls
            If C IsNot Me AndAlso TypeOf C Is RadioButton Then
                DirectCast(C, RadioButton).Checked = False
                Invalidate()
            End If
        Next
    End Sub
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        InvalidateControls()
    End Sub

    <Flags> _
    Enum _Options
        Style1
        Style2
    End Enum

    <Category("Options")> _
    Public Property Options As _Options
        Get
            Return O
        End Get
        Set(value As _Options)
            O = value
        End Set
    End Property

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 22
    End Sub

#Region " Mouse States"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

#End Region
#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _BorderColor As Color = _FlatColor
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                   ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Cursor = Cursors.Hand
        Size = New Size(100, 22)
        BackColor = Color.FromArgb(60, 70, 73)
        Font = New Font("Segoe UI", 10)
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim Base As New Rectangle(0, 2, Height - 5, Height - 5), Dot As New Rectangle(4, 6, H - 12, H - 12)

        With G
            .SmoothingMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            Select Case O
                Case _Options.Style1
                    '-- Base
                    .FillEllipse(New SolidBrush(_BaseColor), Base)

                    Select Case State
                        Case MouseState.Over
                            .DrawEllipse(New Pen(_BorderColor), Base)
                        Case MouseState.Down
                            .DrawEllipse(New Pen(_BorderColor), Base)
                    End Select

                    '-- If Checked 
                    If Checked Then
                        .FillEllipse(New SolidBrush(_BorderColor), Dot)
                    End If
                Case _Options.Style2
                    '-- Base
                    .FillEllipse(New SolidBrush(_BaseColor), Base)

                    Select Case State
                        Case MouseState.Over
                            '-- Base
                            .DrawEllipse(New Pen(_BorderColor), Base)
                            .FillEllipse(New SolidBrush(Color.FromArgb(118, 213, 170)), Base)
                        Case MouseState.Down
                            '-- Base
                            .DrawEllipse(New Pen(_BorderColor), Base)
                            .FillEllipse(New SolidBrush(Color.FromArgb(118, 213, 170)), Base)
                    End Select

                    '-- If Checked
                    If Checked Then
                        '-- Base
                        .FillEllipse(New SolidBrush(_BorderColor), Dot)
                    End If
            End Select

            .DrawString(Text, Font, New SolidBrush(_TextColor), New Rectangle(20, 2, W, H), NearSF)
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

<DefaultEvent("CheckedChanged")> Class FlatCheckBox : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private State As MouseState = MouseState.None
    Private O As _Options
    Private _Checked As Boolean

#End Region

#Region " Properties"
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

    Event CheckedChanged(ByVal sender As Object)
    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        _Checked = Not _Checked
        RaiseEvent CheckedChanged(Me)
        MyBase.OnClick(e)
    End Sub

    <Flags> _
    Enum _Options
        Style1
        Style2
    End Enum

    <Category("Options")> _
    Public Property Options As _Options
        Get
            Return O
        End Get
        Set(value As _Options)
            O = value
        End Set
    End Property

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 22
    End Sub

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
        End Set
    End Property

#End Region

#Region " Mouse States"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

#End Region

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _BorderColor As Color = _FlatColor
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.FromArgb(60, 70, 73)
        Cursor = Cursors.Hand
        Font = New Font("Segoe UI", 10)
        Size = New Size(112, 22)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim Base As New Rectangle(0, 2, Height - 5, Height - 5)

        With G
            .SmoothingMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)
            Select Case O
                Case _Options.Style1 '-- Style 1
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), Base)

                    Select Case State
                        Case MouseState.Over
                            '-- Base
                            .DrawRectangle(New Pen(_BorderColor), Base)
                        Case MouseState.Down
                            '-- Base
                            .DrawRectangle(New Pen(_BorderColor), Base)
                    End Select

                    '-- If Checked
                    If Checked Then
                        .DrawString("ü", New Font("Wingdings", 18), New SolidBrush(_BorderColor), New Rectangle(5, 7, H - 9, H - 9), CenterSF)
                    End If

                    '-- If Enabled
                    If Me.Enabled = False Then
                        .FillRectangle(New SolidBrush(Color.FromArgb(54, 58, 61)), Base)
                        .DrawString(Text, Font, New SolidBrush(Color.FromArgb(140, 142, 143)), New Rectangle(20, 2, W, H), NearSF)
                    End If

                    '-- Text
                    .DrawString(Text, Font, New SolidBrush(_TextColor), New Rectangle(20, 2, W, H), NearSF)
                Case _Options.Style2 '-- Style 2
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), Base)

                    Select Case State
                        Case MouseState.Over
                            '-- Base
                            .DrawRectangle(New Pen(_BorderColor), Base)
                            .FillRectangle(New SolidBrush(Color.FromArgb(118, 213, 170)), Base)
                        Case MouseState.Down
                            '-- Base
                            .DrawRectangle(New Pen(_BorderColor), Base)
                            .FillRectangle(New SolidBrush(Color.FromArgb(118, 213, 170)), Base)
                    End Select

                    '-- If Checked
                    If Checked Then
                        .DrawString("ü", New Font("Wingdings", 18), New SolidBrush(_BorderColor), New Rectangle(5, 7, H - 9, H - 9), CenterSF)
                    End If

                    '-- If Enabled
                    If Me.Enabled = False Then
                        .FillRectangle(New SolidBrush(Color.FromArgb(54, 58, 61)), Base)
                        .DrawString(Text, Font, New SolidBrush(Color.FromArgb(48, 119, 91)), New Rectangle(20, 2, W, H), NearSF)
                    End If

                    '-- Text
                    .DrawString(Text, Font, New SolidBrush(_TextColor), New Rectangle(20, 2, W, H), NearSF)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

<DefaultEvent("TextChanged")> Class FlatTextBox : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private State As MouseState = MouseState.None
    Private WithEvents TB As Windows.Forms.TextBox

#End Region

#Region " Properties"

#Region " TextBox Properties"

    Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left
    <Category("Options")> _
    Property TextAlign() As HorizontalAlignment
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            _TextAlign = value
            If TB IsNot Nothing Then
                TB.TextAlign = value
            End If
        End Set
    End Property
    Private _MaxLength As Integer = 32767
    <Category("Options")> _
    Property MaxLength() As Integer
        Get
            Return _MaxLength
        End Get
        Set(ByVal value As Integer)
            _MaxLength = value
            If TB IsNot Nothing Then
                TB.MaxLength = value
            End If
        End Set
    End Property
    Private _ReadOnly As Boolean
    <Category("Options")> _
    Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If TB IsNot Nothing Then
                TB.ReadOnly = value
            End If
        End Set
    End Property
    Private _UseSystemPasswordChar As Boolean
    <Category("Options")> _
    Property UseSystemPasswordChar() As Boolean
        Get
            Return _UseSystemPasswordChar
        End Get
        Set(ByVal value As Boolean)
            _UseSystemPasswordChar = value
            If TB IsNot Nothing Then
                TB.UseSystemPasswordChar = value
            End If
        End Set
    End Property
    Private _Multiline As Boolean
    <Category("Options")> _
    Property Multiline() As Boolean
        Get
            Return _Multiline
        End Get
        Set(ByVal value As Boolean)
            _Multiline = value
            If TB IsNot Nothing Then
                TB.Multiline = value

                If value Then
                    TB.Height = Height - 11
                Else
                    Height = TB.Height + 11
                End If

            End If
        End Set
    End Property
    <Category("Options")> _
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            If TB IsNot Nothing Then
                TB.Text = value
            End If
        End Set
    End Property
    <Category("Options")> _
    Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            If TB IsNot Nothing Then
                TB.Font = value
                TB.Location = New Point(3, 5)
                TB.Width = Width - 6

                If Not _Multiline Then
                    Height = TB.Height + 11
                End If
            End If
        End Set
    End Property

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If Not Controls.Contains(TB) Then
            Controls.Add(TB)
        End If
    End Sub
    Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
        Text = TB.Text
    End Sub
    Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then
            TB.SelectAll()
            e.SuppressKeyPress = True
        End If
        If e.Control AndAlso e.KeyCode = Keys.C Then
            TB.Copy()
            e.SuppressKeyPress = True
        End If
    End Sub
    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        TB.Location = New Point(5, 5)
        TB.Width = Width - 10

        If _Multiline Then
            TB.Height = Height - 11
        Else
            Height = TB.Height + 11
        End If

        MyBase.OnResize(e)
    End Sub

#End Region

#Region " Colors"

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

    Public Overrides Property ForeColor() As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

#End Region

#Region " Mouse States"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : TB.Focus() : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : TB.Focus() : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

#End Region

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _TextColor As Color = Color.FromArgb(192, 192, 192)
    Private _BorderColor As Color = _FlatColor

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True

        BackColor = Color.Transparent

        TB = New Windows.Forms.TextBox
        TB.Font = New Font("Segoe UI", 10)
        TB.Text = Text
        TB.BackColor = _BaseColor
        TB.ForeColor = _TextColor
        TB.MaxLength = _MaxLength
        TB.Multiline = _Multiline
        TB.ReadOnly = _ReadOnly
        TB.UseSystemPasswordChar = _UseSystemPasswordChar
        TB.BorderStyle = BorderStyle.None
        TB.Location = New Point(5, 5)
        TB.Width = Width - 10

        TB.Cursor = Cursors.IBeam

        If _Multiline Then
            TB.Height = Height - 11
        Else
            Height = TB.Height + 11
        End If

        AddHandler TB.TextChanged, AddressOf OnBaseTextChanged
        AddHandler TB.KeyDown, AddressOf OnBaseKeyDown
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Colors
            TB.BackColor = _BaseColor
            TB.ForeColor = _TextColor

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

Class FlatTabControl : Inherits TabControl

#Region " Variables"

    Private W, H As Integer

#End Region

#Region " Properties"

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        Alignment = TabAlignment.Top
    End Sub

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property ActiveColor As Color
        Get
            Return _ActiveColor
        End Get
        Set(value As Color)
            _ActiveColor = value
        End Set
    End Property

#End Region
    
#End Region

#Region " Colors"

    Private BGColor As Color = Color.FromArgb(60, 70, 73)
    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _ActiveColor As Color = _FlatColor

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.FromArgb(60, 70, 73)

        Font = New Font("Segoe UI", 10)
        SizeMode = TabSizeMode.Fixed
        ItemSize = New Size(120, 40)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(_BaseColor)

            Try : SelectedTab.BackColor = BGColor : Catch : End Try

            For i = 0 To TabCount - 1
                Dim Base As New Rectangle(New Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y), New Size(GetTabRect(i).Width, GetTabRect(i).Height))
                Dim BaseSize As New Rectangle(Base.Location, New Size(Base.Width, Base.Height))

                If i = SelectedIndex Then
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), BaseSize)

                    '-- Gradiant
                    '.fill
                    .FillRectangle(New SolidBrush(_ActiveColor), BaseSize)

                    '-- ImageList
                    If ImageList IsNot Nothing Then
                        Try
                            If ImageList.Images(TabPages(i).ImageIndex) IsNot Nothing Then
                                '-- Image
                                .DrawImage(ImageList.Images(TabPages(i).ImageIndex), New Point(BaseSize.Location.X + 8, BaseSize.Location.Y + 6))
                                '-- Text
                                .DrawString("      " & TabPages(i).Text, Font, Brushes.White, BaseSize, CenterSF)
                            Else
                                '-- Text
                                .DrawString(TabPages(i).Text, Font, Brushes.White, BaseSize, CenterSF)
                            End If
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try
                    Else
                        '-- Text
                        .DrawString(TabPages(i).Text, Font, Brushes.White, BaseSize, CenterSF)
                    End If
                Else
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), BaseSize)

                    '-- ImageList
                    If ImageList IsNot Nothing Then
                        Try
                            If ImageList.Images(TabPages(i).ImageIndex) IsNot Nothing Then
                                '-- Image
                                .DrawImage(ImageList.Images(TabPages(i).ImageIndex), New Point(BaseSize.Location.X + 8, BaseSize.Location.Y + 6))
                                '-- Text
                                .DrawString("      " & TabPages(i).Text, Font, New SolidBrush(Color.White), BaseSize, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                            Else
                                '-- Text
                                .DrawString(TabPages(i).Text, Font, New SolidBrush(Color.White), BaseSize, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                            End If
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try
                    Else
                        '-- Text
                        .DrawString(TabPages(i).Text, Font, New SolidBrush(Color.White), BaseSize, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                    End If
                End If
            Next
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatAlertBox : Inherits Control

    ''' <summary>
    ''' How to use: FlatAlertBox.ShowControl(Kind, String, Interval)
    ''' </summary>
    ''' <remarks></remarks>

#Region " Variables"

    Private W, H As Integer
    Private K As _Kind
    Private _Text As String
    Private State As MouseState = MouseState.None
    Private X As Integer
    Private WithEvents T As Timer

#End Region

#Region " Properties"

    <Flags()> _
    Enum _Kind
        [Success]
        [Error]
        [Info]
    End Enum

#Region " Options"

    <Category("Options")> _
    Public Property kind As _Kind
        Get
            Return K
        End Get
        Set(value As _Kind)
            K = value
        End Set
    End Property

    <Category("Options")> _
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            If _Text IsNot Nothing Then
                _Text = value
            End If
        End Set
    End Property

    <Category("Options")> _
    Shadows Property Visible As Boolean
        Get
            Return MyBase.Visible = False
        End Get
        Set(value As Boolean)
            MyBase.Visible = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 42
    End Sub

    Public Sub ShowControl(Kind As _Kind, Str As String, Interval As Integer)
        K = Kind
        Text = Str
        Me.Visible = True
        T = New Timer
        T.Interval = Interval
        T.Enabled = True
    End Sub

    Private Sub T_Tick(sender As Object, e As EventArgs) Handles T.Tick
        Me.Visible = False
        T.Enabled = False
        T.Dispose()
    End Sub

#Region " Mouse States"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Me.Visible = False
    End Sub

#End Region

#End Region

#Region " Colors"

    Private SuccessColor As Color = Color.FromArgb(60, 85, 79)
    Private SuccessText As Color = Color.FromArgb(35, 169, 110)
    Private ErrorColor As Color = Color.FromArgb(87, 71, 71)
    Private ErrorText As Color = Color.FromArgb(254, 142, 122)
    Private InfoColor As Color = Color.FromArgb(70, 91, 94)
    Private InfoText As Color = Color.FromArgb(97, 185, 186)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.FromArgb(60, 70, 73)
        Size = New Size(576, 42)
        Location = New Point(10, 61)
        Font = New Font("Segoe UI", 10)
        Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            Select Case K
                Case _Kind.Success
                    '-- Base
                    .FillRectangle(New SolidBrush(SuccessColor), Base)

                    '-- Ellipse
                    .FillEllipse(New SolidBrush(SuccessText), New Rectangle(8, 9, 24, 24))
                    .FillEllipse(New SolidBrush(SuccessColor), New Rectangle(10, 11, 20, 20))

                    '-- Checked Sign
                    .DrawString("ü", New Font("Wingdings", 22), New SolidBrush(SuccessText), New Rectangle(7, 7, W, H), NearSF)
                    .DrawString(Text, Font, New SolidBrush(SuccessText), New Rectangle(48, 12, W, H), NearSF)

                    '-- X button
                    .FillEllipse(New SolidBrush(Color.FromArgb(35, Color.Black)), New Rectangle(W - 30, H - 29, 17, 17))
                    .DrawString("r", New Font("Marlett", 8), New SolidBrush(SuccessColor), New Rectangle(W - 28, 16, W, H), NearSF)

                    Select Case State ' -- Mouse Over
                        Case MouseState.Over
                            .DrawString("r", New Font("Marlett", 8), New SolidBrush(Color.FromArgb(25, Color.White)), New Rectangle(W - 28, 16, W, H), NearSF)
                    End Select

                Case _Kind.Error
                    '-- Base
                    .FillRectangle(New SolidBrush(ErrorColor), Base)

                    '-- Ellipse
                    .FillEllipse(New SolidBrush(ErrorText), New Rectangle(8, 9, 24, 24))
                    .FillEllipse(New SolidBrush(ErrorColor), New Rectangle(10, 11, 20, 20))

                    '-- X Sign
                    .DrawString("r", New Font("Marlett", 16), New SolidBrush(ErrorText), New Rectangle(6, 11, W, H), NearSF)
                    .DrawString(Text, Font, New SolidBrush(ErrorText), New Rectangle(48, 12, W, H), NearSF)

                    '-- X button
                    .FillEllipse(New SolidBrush(Color.FromArgb(35, Color.Black)), New Rectangle(W - 32, H - 29, 17, 17))
                    .DrawString("r", New Font("Marlett", 8), New SolidBrush(ErrorColor), New Rectangle(W - 30, 17, W, H), NearSF)

                    Select Case State
                        Case MouseState.Over ' -- Mouse Over
                            .DrawString("r", New Font("Marlett", 8), New SolidBrush(Color.FromArgb(25, Color.White)), New Rectangle(W - 30, 15, W, H), NearSF)
                    End Select

                Case _Kind.Info
                    '-- Base
                    .FillRectangle(New SolidBrush(InfoColor), Base)

                    '-- Ellipse
                    .FillEllipse(New SolidBrush(InfoText), New Rectangle(8, 9, 24, 24))
                    .FillEllipse(New SolidBrush(InfoColor), New Rectangle(10, 11, 20, 20))

                    '-- Info Sign
                    .DrawString("¡", New Font("Segoe UI", 20, FontStyle.Bold), New SolidBrush(InfoText), New Rectangle(12, -4, W, H), NearSF)
                    .DrawString(Text, Font, New SolidBrush(InfoText), New Rectangle(48, 12, W, H), NearSF)

                    '-- X button
                    .FillEllipse(New SolidBrush(Color.FromArgb(35, Color.Black)), New Rectangle(W - 32, H - 29, 17, 17))
                    .DrawString("r", New Font("Marlett", 8), New SolidBrush(InfoColor), New Rectangle(W - 30, 17, W, H), NearSF)

                    Select Case State
                        Case MouseState.Over ' -- Mouse Over
                            .DrawString("r", New Font("Marlett", 8), New SolidBrush(Color.FromArgb(25, Color.White)), New Rectangle(W - 30, 17, W, H), NearSF)
                    End Select
            End Select

        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

Class FlatProgressBar : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private _Value As Integer = 0
    Private _Maximum As Integer = 100

#End Region

#Region " Properties"

#Region " Control"

    <Category("Control")>
    Public Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(V As Integer)
            Select Case V
                Case Is < _Value
                    _Value = V
            End Select
            _Maximum = V
            Invalidate()
        End Set
    End Property

    <Category("Control")>
    Public Property Value() As Integer
        Get
            Select Case _Value
                Case 0
                    Return 0
                    Invalidate()
                Case Else
                    Return _Value
                    Invalidate()
            End Select
        End Get
        Set(V As Integer)
            Select Case V
                Case Is > _Maximum
                    V = _Maximum
                    Invalidate()
            End Select
            _Value = V
            Invalidate()
        End Set
    End Property

#End Region

#Region " Colors"

    <Category("Colors")>
    Public Property ProgressColor As Color
        Get
            Return _ProgressColor
        End Get
        Set(value As Color)
            _ProgressColor = value
        End Set
    End Property

    <Category("Colors")>
    Public Property DarkerProgress As Color
        Get
            Return _DarkerProgress
        End Get
        Set(value As Color)
            _DarkerProgress = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 42
    End Sub

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        Height = 42
    End Sub

    Public Sub Increment(ByVal Amount As Integer)
        Value += Amount
    End Sub

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _ProgressColor As Color = _FlatColor
    Private _DarkerProgress As Color = Color.FromArgb(23, 148, 92)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        BackColor = Color.FromArgb(60, 70, 73)
        Height = 42
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim Base As New Rectangle(0, 24, W, H)
        Dim GP, GP2, GP3 As New GraphicsPath

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Progress Value
            Dim iValue As Integer = CInt(_Value / _Maximum * Width)

            Select Case Value
                Case 0
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), Base)
                    '--Progress
                    .FillRectangle(New SolidBrush(_ProgressColor), New Rectangle(0, 24, iValue - 1, H - 1))
                Case 100
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), Base)
                    '--Progress
                    .FillRectangle(New SolidBrush(_ProgressColor), New Rectangle(0, 24, iValue - 1, H - 1))
                Case Else
                    '-- Base
                    .FillRectangle(New SolidBrush(_BaseColor), Base)

                    '--Progress
                    GP.AddRectangle(New Rectangle(0, 24, iValue - 1, H - 1))
                    .FillPath(New SolidBrush(_ProgressColor), GP)

                    '-- Hatch Brush
                    Dim HB As New HatchBrush(HatchStyle.Plaid, _DarkerProgress, _ProgressColor)
                    .FillRectangle(HB, New Rectangle(0, 24, iValue - 1, H - 1))

                    '-- Balloon
                    Dim Balloon As New Rectangle(iValue - 18, 0, 34, 16)
                    GP2 = Helpers.RoundRec(Balloon, 4)
                    .FillPath(New SolidBrush(_BaseColor), GP2)

                    '-- Arrow
                    GP3 = Helpers.DrawArrow(iValue - 9, 16, True)
                    .FillPath(New SolidBrush(_BaseColor), GP3)

                    '-- Value > You can add "%" > value & "%"
                    .DrawString(Value, New Font("Segoe UI", 10), New SolidBrush(_ProgressColor), New Rectangle(iValue - 11, -2, W, H), NearSF)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

Class FlatComboBox : Inherits Windows.Forms.ComboBox

#Region " Variables"

    Private W, H As Integer
    Private _StartIndex As Integer = 0
    Private x, y As Integer

#End Region

#Region " Properties"

#Region " Mouse States"

    Private State As MouseState = MouseState.None
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.Location.X
        y = e.Location.Y
        Invalidate()
        If e.X < Width - 41 Then Cursor = Cursors.IBeam Else Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        MyBase.OnDrawItem(e) : Invalidate()
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e) : Invalidate()
    End Sub

#End Region

#Region " Colors"

    <Category("Colors")> _
    Public Property HoverColor As Color
        Get
            Return _HoverColor
        End Get
        Set(value As Color)
            _HoverColor = value
        End Set
    End Property

#End Region

    Private Property StartIndex As Integer
        Get
            Return _StartIndex
        End Get
        Set(ByVal value As Integer)
            _StartIndex = value
            Try
                MyBase.SelectedIndex = value
            Catch
            End Try
            Invalidate()
        End Set
    End Property

    Sub DrawItem_(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        If e.Index < 0 Then Exit Sub
        e.DrawBackground()
        e.DrawFocusRectangle()

        e.Graphics.SmoothingMode = 2
        e.Graphics.PixelOffsetMode = 2
        e.Graphics.TextRenderingHint = 5
        e.Graphics.InterpolationMode = 7

        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            '-- Selected item
            e.Graphics.FillRectangle(New SolidBrush(_HoverColor), e.Bounds)
        Else
            '-- Not Selected
            e.Graphics.FillRectangle(New SolidBrush(_BaseColor), e.Bounds)
        End If

        '-- Text
        e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), New Font("Segoe UI", 8), _
                     Brushes.White, New Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height))


        e.Graphics.Dispose()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 18
    End Sub

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(25, 27, 29)
    Private _BGColor As Color = Color.FromArgb(45, 47, 49)
    Private _HoverColor As Color = Color.FromArgb(35, 168, 109)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True

        DrawMode = DrawMode.OwnerDrawFixed
        BackColor = Color.FromArgb(45, 45, 48)
        ForeColor = Color.White
        DropDownStyle = ComboBoxStyle.DropDownList
        Cursor = Cursors.Hand
        StartIndex = 0
        ItemHeight = 18
        Font = New Font("Segoe UI", 8, FontStyle.Regular)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim Base As New Rectangle(0, 0, W, H)
        Dim Button As New Rectangle(CInt(W - 40), 0, W, H)
        Dim GP, GP2 As New GraphicsPath

        With G
            .Clear(Color.FromArgb(45, 45, 48))
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5

            '-- Base
            .FillRectangle(New SolidBrush(_BGColor), Base)

            '-- Button
            GP.Reset()
            GP.AddRectangle(Button)
            .SetClip(GP)
            .FillRectangle(New SolidBrush(_BaseColor), Button)
            .ResetClip()

            '-- Lines
            .DrawLine(Pens.White, W - 10, 6, W - 30, 6)
            .DrawLine(Pens.White, W - 10, 12, W - 30, 12)
            .DrawLine(Pens.White, W - 10, 18, W - 30, 18)

            '-- Text
            .DrawString(Text, Font, Brushes.White, New Point(4, 6), NearSF)
        End With

        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatStickyButton : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private State As MouseState = MouseState.None
    Private _Rounded As Boolean = False

#End Region

#Region " Properties"

#Region " MouseStates"

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub

#End Region

#Region " Function"

    Private Function GetConnectedSides() As Boolean()
        Dim Bool = New Boolean(3) {False, False, False, False}

        For Each B As Control In Parent.Controls
            If TypeOf B Is FlatStickyButton Then
                If B Is Me Or Not Rect.IntersectsWith(Rect) Then Continue For
                Dim A = Math.Atan2(Left() - B.Left, Top - B.Top) * 2 / Math.PI
                If A \ 1 = A Then Bool(A + 1) = True
            End If
        Next

        Return Bool
    End Function

    Private ReadOnly Property Rect() As Rectangle
        Get
            Return New Rectangle(Left, Top, Width, Height)
        End Get
    End Property

#End Region

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

    <Category("Options")> _
    Public Property Rounded As Boolean
        Get
            Return _Rounded
        End Get
        Set(value As Boolean)
            _Rounded = value
        End Set
    End Property

#End Region

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        'Height = 32
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        'Size = New Size(112, 32)
    End Sub

#End Region

#Region " Colors"

    Private _BaseColor As Color = _FlatColor
    Private _TextColor As Color = Color.FromArgb(243, 243, 243)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
        ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
        ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Size = New Size(106, 32)
        BackColor = Color.Transparent
        Font = New Font("Segoe UI", 12)
        Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim GP As New GraphicsPath

        Dim GCS = GetConnectedSides()
        Dim RoundedBase = Helpers.RoundRect(0, 0, W, H, , Not (GCS(2) Or GCS(1)), Not (GCS(1) Or GCS(0)), Not (GCS(3) Or GCS(0)), Not (GCS(3) Or GCS(2)))
        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            Select Case State
                Case MouseState.None
                    If Rounded Then
                        '-- Base
                        GP = RoundedBase
                        .FillPath(New SolidBrush(_BaseColor), GP)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    Else
                        '-- Base
                        .FillRectangle(New SolidBrush(_BaseColor), Base)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    End If
                Case MouseState.Over
                    If Rounded Then
                        '-- Base
                        GP = RoundedBase
                        .FillPath(New SolidBrush(_BaseColor), GP)
                        .FillPath(New SolidBrush(Color.FromArgb(20, Color.White)), GP)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    Else
                        '-- Base
                        .FillRectangle(New SolidBrush(_BaseColor), Base)
                        .FillRectangle(New SolidBrush(Color.FromArgb(20, Color.White)), Base)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    End If
                Case MouseState.Down
                    If Rounded Then
                        '-- Base
                        GP = RoundedBase
                        .FillPath(New SolidBrush(_BaseColor), GP)
                        .FillPath(New SolidBrush(Color.FromArgb(20, Color.Black)), GP)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    Else
                        '-- Base
                        .FillRectangle(New SolidBrush(_BaseColor), Base)
                        .FillRectangle(New SolidBrush(Color.FromArgb(20, Color.Black)), Base)

                        '-- Text
                        .DrawString(Text, Font, New SolidBrush(_TextColor), Base, CenterSF)
                    End If
            End Select

        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

Class FlatNumeric : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private State As MouseState = MouseState.None
    Private x, y As Integer
    Private _Value, _Min, _Max As Long
    Private Bool As Boolean

#End Region

#Region " Properties"

    Public Property Value As Long
        Get
            Return _Value
        End Get
        Set(value As Long)
            If value <= _Max And value >= _Min Then _Value = value
            Invalidate()
        End Set
    End Property

    Public Property Maximum As Long
        Get
            Return _Max
        End Get
        Set(value As Long)
            If value > _Min Then _Max = value
            If _Value > _Max Then _Value = _Max
            Invalidate()
        End Set
    End Property

    Public Property Minimum As Long
        Get
            Return _Min
        End Get
        Set(value As Long)
            If value < _Max Then _Min = value
            If _Value < _Min Then _Value = Minimum
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.Location.X
        y = e.Location.Y
        Invalidate()
        If e.X < Width - 23 Then Cursor = Cursors.IBeam Else Cursor = Cursors.Hand
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If x > Width - 21 AndAlso x < Width - 3 Then
            If y < 15 Then
                If (Value + 1) <= _Max Then _Value += 1
            Else
                If (Value - 1) >= _Min Then _Value -= 1
            End If
        Else
            Bool = Not Bool
            Focus()
        End If
        Invalidate()
    End Sub

    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        Try
            If Bool Then _Value = CStr(CStr(_Value) & e.KeyChar.ToString())
            If _Value > _Max Then _Value = _Max
            Invalidate()
        Catch : End Try
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Back Then
            Value = 0
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 30
    End Sub

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property ButtonColor As Color
        Get
            Return _ButtonColor
        End Get
        Set(value As Color)
            _ButtonColor = value
        End Set
    End Property

#End Region

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _ButtonColor As Color = _FlatColor

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
        ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
        ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 10)
        BackColor = Color.FromArgb(60, 70, 73)
        ForeColor = Color.White
        _Min = 0
        _Max = 9999999
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(_BaseColor), Base)
            .FillRectangle(New SolidBrush(_ButtonColor), New Rectangle(Width - 24, 0, 24, H))

            '-- Add
            .DrawString("+", New Font("Segoe UI", 12), Brushes.White, New Point(Width - 12, 8), CenterSF)
            '-- Subtract
            .DrawString("-", New Font("Segoe UI", 10, FontStyle.Bold), Brushes.White, New Point(Width - 12, 22), CenterSF)

            '-- Text
            .DrawString(Value, Font, Brushes.White, New Rectangle(5, 1, W, H), New StringFormat() With {.LineAlignment = StringAlignment.Center})
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

Class FlatListBox : Inherits Control

#Region " Variables"

    Private WithEvents ListBx As New ListBox
    Private _items As String() = {""}

#End Region

#Region " Poperties"

    <Category("Options")> _
    Public Property items As String()
        Get
            Return _items
        End Get
        Set(value As String())
            _items = value
            ListBx.Items.Clear()
            ListBx.Items.AddRange(value)
            Invalidate()
        End Set
    End Property

    <Category("Colors")> _
    Public Property SelectedColor As Color
        Get
            Return _SelectedColor
        End Get
        Set(value As Color)
            _SelectedColor = value
        End Set
    End Property

    Public ReadOnly Property SelectedItem() As String
        Get
            Return ListBx.SelectedItem
        End Get
    End Property

    Public ReadOnly Property SelectedIndex() As Integer
        Get
            Return ListBx.SelectedIndex
            If ListBx.SelectedIndex < 0 Then Exit Property
        End Get
    End Property

    Public Sub Clear()
        ListBx.Items.Clear()
    End Sub

    Public Sub ClearSelected()
        For i As Integer = (ListBx.SelectedItems.Count - 1) To 0 Step -1
            ListBx.Items.Remove(ListBx.SelectedItems(i))
        Next
    End Sub

    Sub Drawitem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles ListBx.DrawItem
        If e.Index < 0 Then Exit Sub
        e.DrawBackground()
        e.DrawFocusRectangle()

        e.Graphics.SmoothingMode = 2
        e.Graphics.PixelOffsetMode = 2
        e.Graphics.InterpolationMode = 7
        e.Graphics.TextRenderingHint = 5

        If InStr(e.State.ToString, "Selected,") > 0 Then '-- if selected
            '-- Base
            e.Graphics.FillRectangle(New SolidBrush(_SelectedColor), New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))

            '-- Text
            e.Graphics.DrawString(" " & ListBx.Items(e.Index).ToString(), New Font("Segoe UI", 8), Brushes.White, e.Bounds.X, e.Bounds.Y + 2)
        Else
            '-- Base
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(51, 53, 55)), New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))

            '-- Text 
            e.Graphics.DrawString(" " & ListBx.Items(e.Index).ToString(), New Font("Segoe UI", 8), Brushes.White, e.Bounds.X, e.Bounds.Y + 2)
        End If

        e.Graphics.Dispose()
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        If Not Controls.Contains(ListBx) Then
            Controls.Add(ListBx)
        End If
    End Sub

    Sub AddRange(ByVal items As Object())
        ListBx.Items.Remove("")
        ListBx.Items.AddRange(items)
    End Sub

    Sub AddItem(ByVal item As Object)
        ListBx.Items.Remove("")
        ListBx.Items.Add(item)
    End Sub

#End Region

#Region " Colors"

    Private BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _SelectedColor As Color = _FlatColor

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
            ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True

        ListBx.DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ListBx.ScrollAlwaysVisible = False
        ListBx.HorizontalScrollbar = False
        ListBx.BorderStyle = BorderStyle.None
        ListBx.BackColor = BaseColor
        ListBx.ForeColor = Color.White
        ListBx.Location = New Point(3, 3)
        ListBx.Font = New Font("Segoe UI", 8)
        ListBx.ItemHeight = 20
        ListBx.Items.Clear()
        ListBx.IntegralHeight = False

        Size = New Size(131, 101)
        BackColor = BaseColor
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Size
            ListBx.Size = New Size(Width - 6, Height - 2)

            '-- Base
            .FillRectangle(New SolidBrush(BaseColor), Base)
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

Class FlatContextMenuStrip : Inherits ContextMenuStrip

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

    Sub New()
        MyBase.New()
        Renderer = New ToolStripProfessionalRenderer(New TColorTable())
        ShowImageMargin = False
        ForeColor = Color.White
        Font = New Font("Segoe UI", 8)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.TextRenderingHint = 5
    End Sub

    Class TColorTable : Inherits ProfessionalColorTable

#Region " Properties"

#Region " Colors"

        <Category("Colors")> _
        Public Property _BackColor As Color
            Get
                Return BackColor
            End Get
            Set(value As Color)
                BackColor = value
            End Set
        End Property

        <Category("Colors")> _
        Public Property _CheckedColor As Color
            Get
                Return CheckedColor
            End Get
            Set(value As Color)
                CheckedColor = value
            End Set
        End Property

        <Category("Colors")> _
        Public Property _BorderColor As Color
            Get
                Return BorderColor
            End Get
            Set(value As Color)
                BorderColor = value
            End Set
        End Property

#End Region

#End Region

#Region " Colors"

        Private BackColor As Color = Color.FromArgb(45, 47, 49)
        Private CheckedColor As Color = _FlatColor
        Private BorderColor As Color = Color.FromArgb(53, 58, 60)

#End Region

#Region " Overrides"

        Public Overrides ReadOnly Property ButtonSelectedBorder As Color
            Get
                Return BackColor
            End Get
        End Property
        Public Overrides ReadOnly Property CheckBackground() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property CheckPressedBackground() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property CheckSelectedBackground() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property ImageMarginGradientBegin() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property ImageMarginGradientEnd() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property ImageMarginGradientMiddle() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property MenuBorder() As Color
            Get
                Return BorderColor
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemBorder() As Color
            Get
                Return BorderColor
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemSelected() As Color
            Get
                Return CheckedColor
            End Get
        End Property
        Public Overrides ReadOnly Property SeparatorDark() As Color
            Get
                Return BorderColor
            End Get
        End Property
        Public Overrides ReadOnly Property ToolStripDropDownBackground() As Color
            Get
                Return BackColor
            End Get
        End Property

#End Region

    End Class

End Class

<DefaultEvent("Scroll")> Class FlatTrackBar : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private Val As Integer
    Private Bool As Boolean
    Private Track As Rectangle
    Private Knob As Rectangle
    Private Style_ As _Style

#End Region

#Region " Properties"

#Region " Mouse States"

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Val = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (Width - 11))
            Track = New Rectangle(Val, 0, 10, 20)

            Bool = Track.Contains(e.Location)
        End If
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Bool AndAlso e.X > -1 AndAlso e.X < (Width + 1) Then
            Value = _Minimum + CInt((_Maximum - _Minimum) * (e.X / Width))
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e) : Bool = False
    End Sub

#End Region

#Region " Styles"

    <Flags> _
    Enum _Style
        Slider
        Knob
    End Enum

    Public Property Style As _Style
        Get
            Return Style_
        End Get
        Set(value As _Style)
            Style_ = value
        End Set
    End Property

#End Region

#Region " Colors"

    <Category("Colors")> _
    Public Property TrackColor As Color
        Get
            Return _TrackColor
        End Get
        Set(value As Color)
            _TrackColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property HatchColor As Color
        Get
            Return _HatchColor
        End Get
        Set(value As Color)
            _HatchColor = value
        End Set
    End Property

#End Region

    Event Scroll(ByVal sender As Object)
    Private _Minimum As Integer
    Public Property Minimum As Integer
        Get
            Return Minimum
        End Get
        Set(value As Integer)
            If value < 0 Then
            End If

            _Minimum = value

            If value > _Value Then _Value = value
            If value > _Maximum Then _Maximum = value
            Invalidate()
        End Set
    End Property
    Private _Maximum As Integer = 10
    Public Property Maximum As Integer
        Get
            Return _Maximum
        End Get
        Set(value As Integer)
            If value < 0 Then
            End If

            _Maximum = value
            If value < _Value Then _Value = value
            If value < _Minimum Then _Minimum = value
            Invalidate()
        End Set
    End Property
    Private _Value As Integer
    Public Property Value As Integer
        Get
            Return _Value
        End Get
        Set(value As Integer)
            If value = _Value Then Return

            If value > _Maximum OrElse value < _Minimum Then
            End If

            _Value = value
            Invalidate()
            RaiseEvent Scroll(Me)
        End Set
    End Property
    Private _ShowValue As Boolean = False
    Public Property ShowValue As Boolean
        Get
            Return _ShowValue
        End Get
        Set(value As Boolean)
            _ShowValue = value
        End Set
    End Property

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Subtract Then
            If Value = 0 Then Exit Sub
            Value -= 1
        ElseIf e.KeyCode = Keys.Add Then
            If Value = _Maximum Then Exit Sub
            Value += 1
        End If
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 23
    End Sub

#End Region

#Region " Colors"

    Private BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _TrackColor As Color = _FlatColor
    Private SliderColor As Color = Color.FromArgb(25, 27, 29)
    Private _HatchColor As Color = Color.FromArgb(23, 148, 92)

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Height = 18

        BackColor = Color.FromArgb(60, 70, 73)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width - 1 : H = Height - 1

        Dim Base As New Rectangle(1, 6, W - 2, 8)
        Dim GP, GP2 As New GraphicsPath

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Value
            Val = CInt((_Value - _Minimum) / (_Maximum - _Minimum) * (W - 10))
            Track = New Rectangle(Val, 0, 10, 20)
            Knob = New Rectangle(Val, 4, 11, 14)

            '-- Base
            GP.AddRectangle(Base)
            .SetClip(GP)
            .FillRectangle(New SolidBrush(BaseColor), New Rectangle(0, 7, W, 8))
            .FillRectangle(New SolidBrush(_TrackColor), New Rectangle(0, 7, Track.X + Track.Width, 8))
            .ResetClip()

            '-- Hatch Brush
            Dim HB As New HatchBrush(HatchStyle.Plaid, HatchColor, _TrackColor)
            .FillRectangle(HB, New Rectangle(-10, 7, Track.X + Track.Width, 8))

            '-- Slider/Knob
            Select Case Style
                Case _Style.Slider
                    GP2.AddRectangle(Track)
                    .FillPath(New SolidBrush(SliderColor), GP2)
                Case _Style.Knob
                    GP2.AddEllipse(Knob)
                    .FillPath(New SolidBrush(SliderColor), GP2)
            End Select

            '-- Show the value 
            If ShowValue Then
                .DrawString(Value, New Font("Segoe UI", 8), Brushes.White, New Rectangle(1, 6, W, H), New StringFormat() _
                            With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Far})
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatStatusBar : Inherits Control

#Region " Variables"

    Private W, H As Integer
    Private _ShowTimeDate As Boolean = False

#End Region

#Region " Properties"

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        Dock = DockStyle.Bottom
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

#Region " Colors"

    <Category("Colors")> _
    Public Property BaseColor As Color
        Get
            Return _BaseColor
        End Get
        Set(value As Color)
            _BaseColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property TextColor As Color
        Get
            Return _TextColor
        End Get
        Set(value As Color)
            _TextColor = value
        End Set
    End Property

    <Category("Colors")> _
    Public Property RectColor As Color
        Get
            Return _RectColor
        End Get
        Set(value As Color)
            _RectColor = value
        End Set
    End Property

#End Region

    Public Property ShowTimeDate As Boolean
        Get
            Return _ShowTimeDate
        End Get
        Set(value As Boolean)
            _ShowTimeDate = value
        End Set
    End Property

    Function GetTimeDate() As String
        Return DateTime.Now.Date & " " & DateTime.Now.Hour & ":" & DateTime.Now.Minute
    End Function

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _TextColor As Color = Color.White
    Private _RectColor As Color = _FlatColor

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 8)
        ForeColor = Color.White
        Size = New Size(Width, 20)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        W = Width : H = Height

        Dim Base As New Rectangle(0, 0, W, H)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BaseColor)

            '-- Base
            .FillRectangle(New SolidBrush(BaseColor), Base)

            '-- Text
            .DrawString(Text, Font, Brushes.White, New Rectangle(10, 4, W, H), NearSF)

            '-- Rectangle
            .FillRectangle(New SolidBrush(_RectColor), New Rectangle(4, 4, 4, 14))

            '-- TimeDate
            If ShowTimeDate Then
                .DrawString(GetTimeDate, Font, New SolidBrush(_TextColor), New Rectangle(-4, 2, W, H), New StringFormat() _
                            With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

Class FlatLabel : Inherits Label

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Font = New Font("Segoe UI", 8)
        ForeColor = Color.White
        BackColor = Color.Transparent
        Text = Text
    End Sub

End Class

Class FlatTreeView : Inherits TreeView

#Region " Variables"

    Private State As TreeNodeStates

#End Region

#Region " Properties"

    Protected Overrides Sub OnDrawNode(e As DrawTreeNodeEventArgs)
        Try
            Dim Bounds As New Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Width, e.Bounds.Height)
            'e.Node.Nodes.Item.
            Select Case State
                Case TreeNodeStates.Default
                    e.Graphics.FillRectangle(Brushes.Red, Bounds)
                    e.Graphics.DrawString(e.Node.Text, New Font("Segoe UI", 8), Brushes.LimeGreen, New Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), NearSF)
                    Invalidate()
                Case TreeNodeStates.Checked
                    e.Graphics.FillRectangle(Brushes.Green, Bounds)
                    e.Graphics.DrawString(e.Node.Text, New Font("Segoe UI", 8), Brushes.Black, New Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), NearSF)
                    Invalidate()
                Case TreeNodeStates.Selected
                    e.Graphics.FillRectangle(Brushes.Green, Bounds)
                    e.Graphics.DrawString(e.Node.Text, New Font("Segoe UI", 8), Brushes.Black, New Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), NearSF)
                    Invalidate()
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        MyBase.OnDrawNode(e)
    End Sub

#End Region

#Region " Colors"

    Private _BaseColor As Color = Color.FromArgb(45, 47, 49)
    Private _LineColor As Color = Color.FromArgb(25, 27, 29)

#End Region
    Sub New()

        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True

        BackColor = _BaseColor
        ForeColor = Color.White
        LineColor = _LineColor
        DrawMode = TreeViewDrawMode.OwnerDrawAll
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            .FillRectangle(New SolidBrush(_BaseColor), Base)
            .DrawString(Text, New Font("Segoe UI", 8), Brushes.Black, New Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), NearSF)

        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class