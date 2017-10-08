<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Buscar
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
        Me.BtnBuscar = New System.Windows.Forms.Button()
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
        Me.Panel1.Controls.Add(Me.BtnBuscar)
        Me.Panel1.Controls.Add(Me.TxtNCheque)
        Me.Panel1.Location = New System.Drawing.Point(8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 114)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(231, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Cta. Corriente"
        '
        'TxtCtaCorriente
        '
        Me.TxtCtaCorriente.Location = New System.Drawing.Point(234, 42)
        Me.TxtCtaCorriente.MaxLength = 11
        Me.TxtCtaCorriente.Multiline = True
        Me.TxtCtaCorriente.Name = "TxtCtaCorriente"
        Me.TxtCtaCorriente.Size = New System.Drawing.Size(158, 27)
        Me.TxtCtaCorriente.TabIndex = 3
        '
        'LblCodPlza
        '
        Me.LblCodPlza.AutoSize = True
        Me.LblCodPlza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCodPlza.Location = New System.Drawing.Point(166, 23)
        Me.LblCodPlza.Name = "LblCodPlza"
        Me.LblCodPlza.Size = New System.Drawing.Size(65, 13)
        Me.LblCodPlza.TabIndex = 7
        Me.LblCodPlza.Text = "Cod. Plza."
        '
        'TxtCodPlza
        '
        Me.TxtCodPlza.Location = New System.Drawing.Point(169, 42)
        Me.TxtCodPlza.MaxLength = 4
        Me.TxtCodPlza.Multiline = True
        Me.TxtCodPlza.Name = "TxtCodPlza"
        Me.TxtCodPlza.Size = New System.Drawing.Size(60, 27)
        Me.TxtCodPlza.TabIndex = 2
        '
        'LblCodBco
        '
        Me.LblCodBco.AutoSize = True
        Me.LblCodBco.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCodBco.Location = New System.Drawing.Point(101, 23)
        Me.LblCodBco.Name = "LblCodBco"
        Me.LblCodBco.Size = New System.Drawing.Size(63, 13)
        Me.LblCodBco.TabIndex = 6
        Me.LblCodBco.Text = "Cod. Bco."
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
        'LblNCheque
        '
        Me.LblNCheque.AutoSize = True
        Me.LblNCheque.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNCheque.Location = New System.Drawing.Point(16, 23)
        Me.LblNCheque.Name = "LblNCheque"
        Me.LblNCheque.Size = New System.Drawing.Size(78, 13)
        Me.LblNCheque.TabIndex = 5
        Me.LblNCheque.Text = "Nro. Cheque"
        '
        'BtnBuscar
        '
        Me.BtnBuscar.Location = New System.Drawing.Point(142, 79)
        Me.BtnBuscar.Name = "BtnBuscar"
        Me.BtnBuscar.Size = New System.Drawing.Size(129, 23)
        Me.BtnBuscar.TabIndex = 4
        Me.BtnBuscar.Text = "Buscar"
        Me.BtnBuscar.UseVisualStyleBackColor = True
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
        'Buscar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 129)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Buscar"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar por código CMC7"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents TxtNCheque As TextBox
    Friend WithEvents LblNCheque As Label
    Friend WithEvents LblCodBco As Label
    Friend WithEvents TxtCodBco As TextBox
    Friend WithEvents LblCodPlza As Label
    Friend WithEvents TxtCodPlza As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtCtaCorriente As TextBox
End Class
