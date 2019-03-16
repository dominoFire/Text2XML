
Public Class Form1

    Private Sub btnCarga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarga.Click
        Dim opf As New OpenFileDialog
        Dim obj As Text2Xml.Text2Xml

        Dim res As String
        If opf.ShowDialog() = Windows.Forms.DialogResult.OK Then
            obj = New Text2Xml.Text2Xml()
            res = obj.Parse(opf.FileName)
            System.IO.File.WriteAllText("salida.xml", res)
        End If
        Shell("notepad salida.xml")
    End Sub
End Class
