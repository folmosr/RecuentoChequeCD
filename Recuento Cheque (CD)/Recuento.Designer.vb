﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Recuento
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Recuento))
        Me.PpalLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.LblChcSerial = New System.Windows.Forms.Label()
        Me.LblChcCount = New System.Windows.Forms.Label()
        Me.InnerLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.FrontPictureBox = New System.Windows.Forms.PictureBox()
        Me.BackPictureBox = New System.Windows.Forms.PictureBox()
        Me.PanelContainer = New System.Windows.Forms.Panel()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnNext = New System.Windows.Forms.Button()
        Me.BtnFirst = New System.Windows.Forms.Button()
        Me.BtnLast = New System.Windows.Forms.Button()
        Me.TxtTotal = New System.Windows.Forms.TextBox()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.DtFecha = New System.Windows.Forms.DateTimePicker()
        Me.LblFecha = New System.Windows.Forms.Label()
        Me.TxtMonto = New System.Windows.Forms.TextBox()
        Me.LblMonto = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip3 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip4 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnGuardar = New System.Windows.Forms.Button()
        Me.PpalLayoutPanel.SuspendLayout()
        Me.InnerLayoutPanel.SuspendLayout()
        CType(Me.FrontPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BackPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'PpalLayoutPanel
        '
        Me.PpalLayoutPanel.ColumnCount = 2
        Me.PpalLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.89286!))
        Me.PpalLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.10714!))
        Me.PpalLayoutPanel.Controls.Add(Me.LblChcSerial, 0, 0)
        Me.PpalLayoutPanel.Controls.Add(Me.LblChcCount, 1, 0)
        Me.PpalLayoutPanel.Controls.Add(Me.InnerLayoutPanel, 0, 1)
        Me.PpalLayoutPanel.Controls.Add(Me.PanelContainer, 1, 1)
        Me.PpalLayoutPanel.Location = New System.Drawing.Point(4, 5)
        Me.PpalLayoutPanel.Name = "PpalLayoutPanel"
        Me.PpalLayoutPanel.RowCount = 2
        Me.PpalLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.210526!))
        Me.PpalLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.78947!))
        Me.PpalLayoutPanel.Size = New System.Drawing.Size(784, 475)
        Me.PpalLayoutPanel.TabIndex = 0
        '
        'LblChcSerial
        '
        Me.LblChcSerial.AutoSize = True
        Me.LblChcSerial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblChcSerial.Enabled = False
        Me.LblChcSerial.Location = New System.Drawing.Point(3, 0)
        Me.LblChcSerial.Name = "LblChcSerial"
        Me.LblChcSerial.Size = New System.Drawing.Size(589, 39)
        Me.LblChcSerial.TabIndex = 0
        Me.LblChcSerial.Text = "Cheque Serial"
        Me.LblChcSerial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblChcCount
        '
        Me.LblChcCount.AutoSize = True
        Me.LblChcCount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblChcCount.Enabled = False
        Me.LblChcCount.Location = New System.Drawing.Point(598, 0)
        Me.LblChcCount.Name = "LblChcCount"
        Me.LblChcCount.Size = New System.Drawing.Size(183, 39)
        Me.LblChcCount.TabIndex = 1
        Me.LblChcCount.Text = "1/N"
        Me.LblChcCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblChcCount.UseMnemonic = False
        '
        'InnerLayoutPanel
        '
        Me.InnerLayoutPanel.ColumnCount = 1
        Me.InnerLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.InnerLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.InnerLayoutPanel.Controls.Add(Me.FrontPictureBox, 0, 0)
        Me.InnerLayoutPanel.Controls.Add(Me.BackPictureBox, 0, 1)
        Me.InnerLayoutPanel.Location = New System.Drawing.Point(3, 42)
        Me.InnerLayoutPanel.Name = "InnerLayoutPanel"
        Me.InnerLayoutPanel.RowCount = 2
        Me.InnerLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.InnerLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.InnerLayoutPanel.Size = New System.Drawing.Size(589, 430)
        Me.InnerLayoutPanel.TabIndex = 2
        '
        'FrontPictureBox
        '
        Me.FrontPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.FrontPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FrontPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FrontPictureBox.Location = New System.Drawing.Point(3, 3)
        Me.FrontPictureBox.Name = "FrontPictureBox"
        Me.FrontPictureBox.Size = New System.Drawing.Size(583, 209)
        Me.FrontPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.FrontPictureBox.TabIndex = 0
        Me.FrontPictureBox.TabStop = False
        '
        'BackPictureBox
        '
        Me.BackPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BackPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BackPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BackPictureBox.Location = New System.Drawing.Point(3, 218)
        Me.BackPictureBox.Name = "BackPictureBox"
        Me.BackPictureBox.Size = New System.Drawing.Size(583, 209)
        Me.BackPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.BackPictureBox.TabIndex = 1
        Me.BackPictureBox.TabStop = False
        '
        'PanelContainer
        '
        Me.PanelContainer.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.PanelContainer.Controls.Add(Me.BtnGuardar)
        Me.PanelContainer.Controls.Add(Me.BtnBack)
        Me.PanelContainer.Controls.Add(Me.BtnNext)
        Me.PanelContainer.Controls.Add(Me.BtnFirst)
        Me.PanelContainer.Controls.Add(Me.BtnLast)
        Me.PanelContainer.Controls.Add(Me.TxtTotal)
        Me.PanelContainer.Controls.Add(Me.LblTotal)
        Me.PanelContainer.Controls.Add(Me.DtFecha)
        Me.PanelContainer.Controls.Add(Me.LblFecha)
        Me.PanelContainer.Controls.Add(Me.TxtMonto)
        Me.PanelContainer.Controls.Add(Me.LblMonto)
        Me.PanelContainer.Location = New System.Drawing.Point(598, 42)
        Me.PanelContainer.Name = "PanelContainer"
        Me.PanelContainer.Size = New System.Drawing.Size(183, 330)
        Me.PanelContainer.TabIndex = 3
        '
        'BtnBack
        '
        Me.BtnBack.Image = CType(resources.GetObject("BtnBack.Image"), System.Drawing.Image)
        Me.BtnBack.Location = New System.Drawing.Point(51, 229)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(42, 30)
        Me.BtnBack.TabIndex = 10
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnNext
        '
        Me.BtnNext.Image = CType(resources.GetObject("BtnNext.Image"), System.Drawing.Image)
        Me.BtnNext.Location = New System.Drawing.Point(91, 229)
        Me.BtnNext.Name = "BtnNext"
        Me.BtnNext.Size = New System.Drawing.Size(42, 30)
        Me.BtnNext.TabIndex = 9
        Me.BtnNext.UseVisualStyleBackColor = True
        '
        'BtnFirst
        '
        Me.BtnFirst.Image = CType(resources.GetObject("BtnFirst.Image"), System.Drawing.Image)
        Me.BtnFirst.Location = New System.Drawing.Point(131, 229)
        Me.BtnFirst.Name = "BtnFirst"
        Me.BtnFirst.Size = New System.Drawing.Size(42, 30)
        Me.BtnFirst.TabIndex = 8
        Me.BtnFirst.UseVisualStyleBackColor = True
        '
        'BtnLast
        '
        Me.BtnLast.Image = CType(resources.GetObject("BtnLast.Image"), System.Drawing.Image)
        Me.BtnLast.Location = New System.Drawing.Point(11, 229)
        Me.BtnLast.Name = "BtnLast"
        Me.BtnLast.Size = New System.Drawing.Size(42, 30)
        Me.BtnLast.TabIndex = 7
        Me.BtnLast.UseVisualStyleBackColor = True
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.TxtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotal.Location = New System.Drawing.Point(11, 189)
        Me.TxtTotal.Multiline = True
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(160, 25)
        Me.TxtTotal.TabIndex = 5
        Me.TxtTotal.Text = "0,0"
        '
        'LblTotal
        '
        Me.LblTotal.BackColor = System.Drawing.SystemColors.ControlLight
        Me.LblTotal.Enabled = False
        Me.LblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(11, 157)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(160, 25)
        Me.LblTotal.TabIndex = 4
        Me.LblTotal.Text = "TOTAL"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtFecha
        '
        Me.DtFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtFecha.Location = New System.Drawing.Point(12, 117)
        Me.DtFecha.Name = "DtFecha"
        Me.DtFecha.Size = New System.Drawing.Size(160, 20)
        Me.DtFecha.TabIndex = 3
        '
        'LblFecha
        '
        Me.LblFecha.BackColor = System.Drawing.SystemColors.ControlLight
        Me.LblFecha.Enabled = False
        Me.LblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFecha.Location = New System.Drawing.Point(12, 86)
        Me.LblFecha.Name = "LblFecha"
        Me.LblFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblFecha.Size = New System.Drawing.Size(160, 25)
        Me.LblFecha.TabIndex = 2
        Me.LblFecha.Text = "FECHA"
        Me.LblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtMonto
        '
        Me.TxtMonto.Location = New System.Drawing.Point(12, 45)
        Me.TxtMonto.Multiline = True
        Me.TxtMonto.Name = "TxtMonto"
        Me.TxtMonto.Size = New System.Drawing.Size(160, 25)
        Me.TxtMonto.TabIndex = 1
        '
        'LblMonto
        '
        Me.LblMonto.BackColor = System.Drawing.SystemColors.ControlLight
        Me.LblMonto.Enabled = False
        Me.LblMonto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMonto.Location = New System.Drawing.Point(12, 13)
        Me.LblMonto.Name = "LblMonto"
        Me.LblMonto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMonto.Size = New System.Drawing.Size(160, 25)
        Me.LblMonto.TabIndex = 0
        Me.LblMonto.Text = "MONTO"
        Me.LblMonto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnGuardar
        '
        Me.BtnGuardar.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.BtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.BtnGuardar.Location = New System.Drawing.Point(11, 278)
        Me.BtnGuardar.Name = "BtnGuardar"
        Me.BtnGuardar.Size = New System.Drawing.Size(159, 34)
        Me.BtnGuardar.TabIndex = 11
        Me.BtnGuardar.Text = "Guardar"
        Me.BtnGuardar.UseVisualStyleBackColor = True
        '
        'Recuento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(791, 485)
        Me.Controls.Add(Me.PpalLayoutPanel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Recuento"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.PpalLayoutPanel.ResumeLayout(False)
        Me.PpalLayoutPanel.PerformLayout()
        Me.InnerLayoutPanel.ResumeLayout(False)
        CType(Me.FrontPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BackPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelContainer.ResumeLayout(False)
        Me.PanelContainer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PpalLayoutPanel As TableLayoutPanel
    Private WithEvents LblChcSerial As Label
    Friend WithEvents LblChcCount As Label
    Friend WithEvents InnerLayoutPanel As TableLayoutPanel
    Friend WithEvents FrontPictureBox As PictureBox
    Friend WithEvents BackPictureBox As PictureBox
    Friend WithEvents PanelContainer As Panel
    Private WithEvents LblMonto As Label
    Friend WithEvents TxtMonto As TextBox
    Friend WithEvents DtFecha As DateTimePicker
    Private WithEvents LblFecha As Label
    Friend WithEvents TxtTotal As TextBox
    Private WithEvents LblTotal As Label
    Friend WithEvents BtnLast As Button
    Friend WithEvents BtnFirst As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnNext As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ToolTip2 As ToolTip
    Friend WithEvents ToolTip3 As ToolTip
    Friend WithEvents ToolTip4 As ToolTip
    Friend WithEvents BtnGuardar As Button
End Class
