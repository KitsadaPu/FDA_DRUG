﻿Public Class MAIN_NODE
    Inherits System.Web.UI.MasterPage
    Private _CLS As New CLS_SESSION
    Sub RunSession()
        Try
            _CLS = Session("CLS")
            '_thanm_customer = Session("thanm_customer")
            '    _thanm = Session("thanm")
        Catch ex As Exception
            Response.Redirect("http://privus.fda.moph.go.th/")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.QueryString("FK_IDA") <> "" Then
                'HyperLink1.NavigateUrl = "../MAIN/FRM_LCN_NEWS.aspx?FK_IDA=" & Request.QueryString("FK_IDA")
            Else
                'HyperLink1.NavigateUrl = "../MAIN/FRM_LCN_NEWS.aspx"
            End If
        Catch ex As Exception

        End Try
        RunSession()
        If Not IsPostBack Then

            hl_name.Text = "ชื่อผู้ใช้" & " " & _CLS.THANM
            hl_organization.Text = "ชื่อผู้ได้รับอนุญาต" & " " & _CLS.THANM_CUSTOMER
        End If
        UC_NODE_AUTO.group = 2
    End Sub
End Class