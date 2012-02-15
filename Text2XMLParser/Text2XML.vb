Imports System.IO
Imports System.Xml
Public Class Text2XML
    Public opIncluirVacios As Boolean = False
    Public opIncluirUltimoElemRenglon As Boolean = False
    Public opLineaHead As Integer = 0
    Public opLineaTail As Integer = 1
    Public opTagRenglon As String = "Renglon"
    Public opTagRaiz As String = "OrdenDeCompra"
    Public opTagToken As String = "Col"
    Public opNumerarTokens As Boolean = True
    Public opSeparadorRenglones As String = vbCrLf
    Public opSeparadorColumnas As String = ","

    Public Sub New()

    End Sub

    Public Function Parse(ByVal path As String) As String
        Dim str As String = ""
        Dim sr As New StreamReader(Path)
        Dim lin As String
        Dim tokens() As String
        Dim incluir As Boolean
        Dim primera As Boolean = True
        Dim ultimaLinea As Boolean = False
        Dim numToken As Integer = 1
        Dim xmlDoc As New XmlDocument()
        Dim nodoRaiz, nodoRenglon, nodoToken, nodoUltimoRenglon As XmlNode
        nodoRaiz = xmlDoc.CreateElement(Me.opTagRaiz)
        xmlDoc.AppendChild(nodoRaiz)

        nodoUltimoRenglon = Nothing
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
                nodoUltimoRenglon = nodoRenglon
            End If
            primera = False
        End While
        If Not Me.opIncluirUltimoElemRenglon And nodoUltimoRenglon IsNot Nothing Then
            nodoRaiz.RemoveChild(nodoUltimoRenglon)
        End If

        Return "<?xml version=""1.0"" encoding=""ISO-8859-1""?>" & xmlDoc.OuterXml
    End Function

    Private Function _Parse(ByVal path As String) As String
        Dim str As String = ""
        Dim sr As New StreamReader(path)
        Dim lin As String
        Dim tokens() As String
        Dim incluir As Boolean
        Dim primera As Boolean = True
        Dim ultima As Boolean = False
        Dim numToken As Integer = 1

        str &= "<?xml version=""1.0"" encoding=""ISO-8859-1""?>"
        str &= "<" & Me.opTagRaiz & ">"
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
        str &= "</" & Me.opTagRaiz & ">"

        Return str
    End Function
End Class
