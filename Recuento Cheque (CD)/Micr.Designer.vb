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
        Me.TxtNCheque = New System.Windows.Forms.TextBox()
        Me.BtnCMC = New System.Windows.Forms.Button()
        Me.LblNCheque = New System.Windows.Forms.Label()
        Me.TxtCodBco = New System.Windows.Forms.TextBox()
        Me.LblCodBco = New System.Windows.Forms.Label()
        Me.LblCodPlza = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.LblCodPlza)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.LblCodBco)
        Me.Panel1.Controls.Add(Me.TxtCodBco)
        Me.Panel1.Controls.Add(Me.LblNCheque)
        Me.Panel1.Controls.Add(Me.BtnCMC)
        Me.Panel1.Controls.Add(Me.TxtNCheque)
        Me.Panel1.Location = New System.Drawing.Point(8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 114)
        Me.Panel1.TabIndex = 0
        '
        'TxtNCheque
        '
        Me.TxtNCheque.Location = New System.Drawing.Point(12, 42)
        Me.TxtNCheque.MaxLength = 7
        Me.TxtNCheque.Multiline = True
        Me.TxtNCheque.Name = "TxtNCheque"
        Me.TxtNCheque.Size = New System.Drawing.Size(87, 27)
        Me.TxtNCheque.TabIndex = 0
        '
        'BtnCMC
        '
        Me.BtnCMC.Location = New System.Drawing.Point(142, 79)
        Me.BtnCMC.Name = "BtnCMC"
        Me.BtnCMC.Size = New System.Drawing.Size(129, 23)
        Me.BtnCMC.TabIndex = 4
        Me.BtnCMC.Text = "Guardar"
        Me.BtnCMC.UseVisualStyleBackColor = True
        '
        'LblNCheque
        '
        Me.LblNCheque.AutoSize = True
        Me.LblNCheque.Enabled = False
        Me.LblNCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNCheque.Location = New System.Drawing.Point(16, 23)
        Me.LblNCheque.Name = "LblNCheque"
        Me.LblNCheque.Size = New System.Drawing.Size(78, 13)
        Me.LblNCheque.TabIndex = 2
        Me.LblNCheque.Text = "Nro. Cheque"
        '
        'TxtCodBco
        '
        Me.TxtCodBco.Location = New System.Drawing.Point(104, 42)
        Me.TxtCodBco.MaxLength = 3
        Me.TxtCodBco.Multiline = True
        Me.TxtCodBco.Name = "TxtCodBco"
        Me.TxtCodBco.Size = New System.Drawing.Size(60, 27)
        Me.TxtCodBco.TabIndex = 1
        '
        'LblCodBco
        '
        Me.LblCodBco.AutoSize = True
        Me.LblCodBco.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCodBco.Location = New System.Drawing.Point(101, 23)
        Me.LblCodBco.Name = "LblCodBco"
        Me.LblCodBco.Size = New System.Drawing.Size(63, 13)
        Me.LblCodBco.TabIndex = 7
        Me.LblCodBco.Text = "Cod. Bco."
        '
        'LblCodPlza
        '
        Me.LblCodPlza.AutoSize = True
        Me.LblCodPlza.Enabled = False
        Me.LblCodPlza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCodPlza.Location = New System.Drawing.Point(166, 23)
        Me.LblCodPlza.Name = "LblCodPlza"
        Me.LblCodPlza.Size = New System.Drawing.Size(65, 13)
        Me.LblCodPlza.TabIndex = 9
        Me.LblCodPlza.Text = "Cod. Plza."
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(169, 42)
        Me.TextBox1.MaxLength = 4
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(60, 27)
        Me.TextBox1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(231, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Cta. Corriente"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(234, 42)
        Me.TextBox2.MaxLength = 11
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(158, 27)
        Me.TextBox2.TabIndex = 3
        '
        'Micr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 129)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Micr"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Corregir Código CMC7"
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
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
End Class
