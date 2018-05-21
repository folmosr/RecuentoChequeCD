<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Micr
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtCtaCorriente = New System.Windows.Forms.TextBox()
        Me.LblCodPlza = New System.Windows.Forms.Label()
        Me.TxtCodPlza = New System.Windows.Forms.TextBox()
        Me.LblCodBco = New System.Windows.Forms.Label()
        Me.TxtCodBco = New System.Windows.Forms.TextBox()
        Me.LblNCheque = New System.Windows.Forms.Label()
        Me.BtnCMC = New System.Windows.Forms.Button()
        Me.TxtNCheque = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.TxtCtaCorriente)
        Me.Panel1.Controls.Add(Me.LblCodPlza)
        Me.Panel1.Controls.Add(Me.TxtCodPlza)
        Me.Panel1.Controls.Add(Me.LblCodBco)
        Me.Panel1.Controls.Add(Me.TxtCodBco)
        Me.Panel1.Controls.Add(Me.LblNCheque)
        Me.Panel1.Controls.Add(Me.BtnCMC)
        Me.Panel1.Controls.Add(Me.TxtNCheque)
        Me.Panel1.Location = New System.Drawing.Point(12, 11)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(606, 174)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(346, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Cta. Corriente"
        '
        'TxtCtaCorriente
        '
        Me.TxtCtaCorriente.Location = New System.Drawing.Point(351, 65)
        Me.TxtCtaCorriente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TxtCtaCorriente.MaxLength = 11
        Me.TxtCtaCorriente.Multiline = True
        Me.TxtCtaCorriente.Name = "TxtCtaCorriente"
        Me.TxtCtaCorriente.Size = New System.Drawing.Size(235, 39)
        Me.TxtCtaCorriente.TabIndex = 3
        '
        'LblCodPlza
        '
        Me.LblCodPlza.AutoSize = True
        Me.LblCodPlza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCodPlza.Location = New System.Drawing.Point(249, 35)
        Me.LblCodPlza.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCodPlza.Name = "LblCodPlza"
        Me.LblCodPlza.Size = New System.Drawing.Size(95, 20)
        Me.LblCodPlza.TabIndex = 7
        Me.LblCodPlza.Text = "Cod. Plza."
        '
        'TxtCodPlza
        '
        Me.TxtCodPlza.Location = New System.Drawing.Point(254, 65)
        Me.TxtCodPlza.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TxtCodPlza.MaxLength = 4
        Me.TxtCodPlza.Multiline = True
        Me.TxtCodPlza.Name = "TxtCodPlza"
        Me.TxtCodPlza.Size = New System.Drawing.Size(88, 39)
        Me.TxtCodPlza.TabIndex = 2
        '
        'LblCodBco
        '
        Me.LblCodBco.AutoSize = True
        Me.LblCodBco.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCodBco.Location = New System.Drawing.Point(152, 35)
        Me.LblCodBco.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblCodBco.Name = "LblCodBco"
        Me.LblCodBco.Size = New System.Drawing.Size(91, 20)
        Me.LblCodBco.TabIndex = 6
        Me.LblCodBco.Text = "Cod. Bco."
        '
        'TxtCodBco
        '
        Me.TxtCodBco.Location = New System.Drawing.Point(156, 65)
        Me.TxtCodBco.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TxtCodBco.MaxLength = 3
        Me.TxtCodBco.Multiline = True
        Me.TxtCodBco.Name = "TxtCodBco"
        Me.TxtCodBco.Size = New System.Drawing.Size(88, 39)
        Me.TxtCodBco.TabIndex = 1
        '
        'LblNCheque
        '
        Me.LblNCheque.AutoSize = True
        Me.LblNCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNCheque.Location = New System.Drawing.Point(24, 35)
        Me.LblNCheque.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblNCheque.Name = "LblNCheque"
        Me.LblNCheque.Size = New System.Drawing.Size(113, 20)
        Me.LblNCheque.TabIndex = 5
        Me.LblNCheque.Text = "Nro. Cheque"
        '
        'BtnCMC
        '
        Me.BtnCMC.Location = New System.Drawing.Point(213, 122)
        Me.BtnCMC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnCMC.Name = "BtnCMC"
        Me.BtnCMC.Size = New System.Drawing.Size(194, 35)
        Me.BtnCMC.TabIndex = 4
        Me.BtnCMC.Text = "Guardar"
        Me.BtnCMC.UseVisualStyleBackColor = True
        '
        'TxtNCheque
        '
        Me.TxtNCheque.Location = New System.Drawing.Point(18, 65)
        Me.TxtNCheque.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TxtNCheque.MaxLength = 7
        Me.TxtNCheque.Multiline = True
        Me.TxtNCheque.Name = "TxtNCheque"
        Me.TxtNCheque.Size = New System.Drawing.Size(128, 39)
        Me.TxtNCheque.TabIndex = 0
        '
        'Micr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(630, 198)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Micr"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Corregir Código CMC7"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents BtnCMC As Button
    Friend WithEvents TxtNCheque As TextBox
    Friend WithEvents LblNCheque As Label
    Friend WithEvents LblCodBco As Label
    Friend WithEvents TxtCodBco As TextBox
    Friend WithEvents LblCodPlza As Label
    Friend WithEvents TxtCodPlza As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtCtaCorriente As TextBox
End Class
