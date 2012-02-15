Imports System.IO
Imports System.Xml
Public Class Text2XML
    Private opIncluirVacios As Boolean = False
    Private opIncluirUltimoRenglon As Boolean = False
    Private opLineaHead As Integer = 0
    Private opLineaTail As Integer = 1
    Private opTagRenglon As String = "Renglon"
    Private opTagInicio As String = "OrdenDeCompra"
    Private opTagToken As String = "Col"
    Private opNumerarTokens As Boolean = True
    Private opSeparadorRenglones As String = vbCrLf
    Private opSeparadorColumnas As String = ","

    Private Sub New()

    End Sub

    Private Function Parse2(ByVal path As String) As String
        Dim str As String = ""
        Dim sr As New StreamReader(path)
        Dim lin As String
        Dim tokens() As String
        Dim incluir As Boolean
        Dim primera As Boolean = True
        Dim ultimaLinea As Boolean = False
        Dim numToken As Integer = 1
        Dim xmlDoc As New XmlDocument()
        Dim nodoRaiz, nodoRenglon, nodoToken, nodoUltRenglon As XmlNode
        nodoRaiz = xmlDoc.CreateElement(Me.opTagInicio)
        xmlDoc.AppendChild(nodoRaiz)

        nodoUltRenglon = Nothing
        nodoRaiz = xmlDoc.DocumentElement
        While Not sr.EndOfStream
            lin = sr.ReadLine()
            numToken = 1
            If sr.EndOfStream Then
                ultimaLinea = True
            End If
            If primera And Me.opLineaHead = 1 Then
                incluir = False
            ElseIf ultimaLinea And Me.opLineaTail = 1 Then
                incluir = False
            ElseIf (lin = "" Or lin Is Nothing) Then
                incluir = Me.opIncluirVacios
            Else
                incluir = True
            End If
            If incluir Then
                nodoRenglon = xmlDoc.CreateElement(Me.opTagRenglon)
                tokens = lin.Split(Me.opSeparadorColumnas)
                For Each s As String In tokens
                    If Me.opNumerarTokens Then
                        nodoToken = xmlDoc.CreateElement(Me.opTagToken & numToken)
                        numToken += 1
                    Else
                        nodoToken = xmlDoc.CreateElement(Me.opTagToken)
                    End If
                    nodoToken.InnerText = s
                    nodoRenglon.AppendChild(nodoToken)
                Next
                nodoRaiz.AppendChild(nodoRenglon)
                nodoUltRenglon = nodoRenglon
            End If
            primera = False
        End While
        If Not Me.opIncluirUltimoRenglon And nodoUltRenglon IsNot Nothing Then
            nodoRaiz.RemoveChild(nodoUltRenglon)
        End If


        Return "<?xml version=""1.0"" encoding=""ISO-8859-1""?>" & xmlDoc.OuterXml
    End Function

    Private Function Parse(ByVal path As String) As String
        Dim str As String = ""
        Dim sr As New StreamReader(path)
        Dim lin As String
        Dim tokens() As String
        Dim incluir As Boolean
        Dim primera As Boolean = True
        Dim ultima As Boolean = False
        Dim numToken As Integer = 1

        str &= "<?xml version=""1.0"" encoding=""ISO-8859-1""?>"
        str &= "<" & Me.opTagInicio & ">"
        While Not sr.EndOfStream
            lin = sr.ReadLine()
            numToken = 1
            If sr.EndOfStream Then
                ultima = True
            End If
            If primera And Me.opLineaHead = 1 Then
                incluir = False
            ElseIf ultima And Me.opLineaTail = 1 Then
                incluir = False
            ElseIf (lin = "" Or lin Is Nothing) Then
                incluir = Me.opIncluirVacios
            Else
                incluir = True
            End If
            If incluir Then
                str &= "<" & Me.opTagRenglon & ">"
                tokens = lin.Split(",")
                For Each s As String In tokens
                    If Me.opNumerarTokens Then
                        str &= "<" & Me.opTagToken & numToken.ToString() & ">"
                        str &= s
                        str &= "</" & Me.opTagToken & numToken.ToString() & ">"
                        numToken += 1
                    Else
                        str &= "<" & Me.opTagToken & ">"
                        str &= s
                        str &= "</" & Me.opTagToken & ">"

                    End If

                Next
                str &= "</" & Me.opTagRenglon & ">"
            End If
            primera = False
        End While
        str &= "</" & Me.opTagInicio & ">"

        Return str
    End Function

    Public Shared Function ParseText(ByVal path As String) As String
        Dim obj As New Text2XML

        'Return obj.Parse(path)
        Return obj.Parse2(path)
    End Function

End Class
