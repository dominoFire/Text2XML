<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
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
        Me.BtnFileLoad = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnFileLoad
        '
        Me.BtnFileLoad.Location = New System.Drawing.Point(90, 42)
        Me.BtnFileLoad.Name = "BtnFileLoad"
        Me.BtnFileLoad.Size = New System.Drawing.Size(120, 30)
        Me.BtnFileLoad.TabIndex = 0
        Me.BtnFileLoad.Text = "Load File..."
        Me.BtnFileLoad.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(302, 113)
        Me.Controls.Add(Me.BtnFileLoad)
        Me.Name = "FrmMain"
        Me.Text = "Text2Xml GUI Demo"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnFileLoad As System.Windows.Forms.Button

End Class
