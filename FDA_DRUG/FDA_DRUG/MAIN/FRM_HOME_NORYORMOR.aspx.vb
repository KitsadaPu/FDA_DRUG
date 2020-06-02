﻿Public Class FRM_HOME_NORYORMOR
    Inherits System.Web.UI.Page
    Dim clsDBSqlCommand As New BAO.ClsDBSqlcommand
    Dim clsDBSyslcnsid As New DAO_CPN.clsDBsyslcnsid
    Dim clsDBSyslcnsnm As New DAO_CPN.clsDBsyslcnsnm

    Private _CLS As New CLS_SESSION
    Private _TOKEN As String

    Private Sub RunQuery()
        _TOKEN = Request("Token").ToString()
        '_TOKEN = "y2Ru9lRHLoc7x/h8c7AcdQUU" 'test
        '_TOKEN = "K1JtRwgdZD5oslzpr5dLKgUU" 'AOF
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            session_clear() 'ล้างค่า Session
            RunQuery()
            token()
        End If
    End Sub
    Sub session_clear()
        'Session("CLS") = Nothing
    End Sub

    Sub token()

        Dim token As String = _TOKEN

        Dim ws As New WS_AUTHENTICATION.Authentication
        Dim xml As String = ""


        If token = "" Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Codeblock", "alert('Not Found Token');window.location.href = 'http://privus.fda.moph.go.th';", True)
            Response.Redirect("http://privus.fda.moph.go.th")
        End If
        xml = ws.Authen_Login(token)

        Dim clsxml As New Cls_XML
        clsxml.ReadData(xml)
        _CLS.CITIZEN_ID = clsxml.Get_Value_XML("Citizen_ID")
        _CLS.CITIZEN_ID_AUTHORIZE = clsxml.Get_Value_XML("CITIEZEN_ID_AUTHORIZE")
        _CLS.TOKEN = _TOKEN
        _CLS.GROUPS = clsxml.Get_Value_XML("Groups")
        _CLS.SYSTEM_ID = clsxml.Get_Value_XML("System_ID")
        _CLS.PVCODE = clsxml.Get_Value_XML("pvcode")


        Dim bao As New BAO.information
        _CLS = bao.load_lcnsid_customer(_CLS)
        _CLS = bao.load_name(_CLS)


        Session("CLS") = _CLS


        Dim code As String = clsxml.Get_Value_XML("CODE")
        If code = "900" Then
            'Response.Redirect("../LCN/FRM_LCN_DRUG.aspx")
            'Response.Redirect("../LOCATION/FRM_LCN_LCT.aspx")
            Response.Redirect("../MAIN/FRM_MAIN_PAGE_PHESAJ.aspx")
        ElseIf code = "100" Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Codeblock", "alert('TOKEN Expire');window.location.href = 'http://privus.fda.moph.go.th';", True)
        ElseIf code = "101" Then
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Codeblock", "alert('TOKEN Expire');window.location.href = 'http://privus.fda.moph.go.th';", True)
        Else
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(Page), "Codeblock", "alert('Not Permission');window.location.href = 'http://privus.fda.moph.go.th';", True)
        End If

    End Sub

End Class