﻿Public Class FRM_STAFF_NYM_CONSIDER
    Inherits System.Web.UI.Page

    Private _TR_ID As Integer
    Private _IDA As Integer
    Private _CLS As New CLS_SESSION
    Public Property _process As String
    ' Private _type As String

    Private Sub runQuery()
        If Session("CLS") Is Nothing Then
            Response.Redirect("http://privus.fda.moph.go.th/")
        Else
            _TR_ID = Request.QueryString("TR_ID")
            _IDA = Request.QueryString("IDA")
            _process = Request.QueryString("process")
            _CLS = Session("CLS")
            ' _type = "1"
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        runQuery()
        If Not IsPostBack Then
            TextBox1.Text = Date.Now.ToShortDateString()
            txt_app_date.Text = Date.Now.ToShortDateString()
            Bind_ddl_staff_offer()
        End If
    End Sub

    Public Sub Bind_ddl_staff_offer()
        Dim bao As New BAO.ClsDBSqlcommand
        Dim dt As New DataTable
        bao.SP_STAFF_OFFER_DDL()

        ddl_staff_offer.DataSource = bao.dt
        ddl_staff_offer.DataBind()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            Dim dao_up As New DAO_DRUG.ClsDBTRANSACTION_UPLOAD
            Dim bao As New BAO.GenNumber
            Dim dao_p As New DAO_DRUG.ClsDBPROCESS_NAME
            If _process = 1026 Then
                Dim dao As New DAO_DRUG.ClsDBDRUG_PROJECT_SUM
                dao.GetDataby_IDA(_IDA)
                dao_up.GetDataby_IDA(dao.fields.TR_ID)
                AddLogStatus(6, _process, _CLS.CITIZEN_ID, _IDA)
                'AddLogStatus(6, dao_up.fields.PROCESS_ID, _CLS.CITIZEN_ID, _IDA)

                'Dim PROCESS_ID As Integer = dao_up.fields.PROCESS_ID
                Dim PROCESS_ID As Integer = _process

                dao_p.GetDataby_Process_ID(_process)
                Dim GROUP_NUMBER As Integer = dao_p.fields.PROCESS_ID

                Dim CONSIDER_DATE As Date = CDate(TextBox1.Text)
                dao.fields.REMARK = Txt_Remark.Text
                dao.fields.STATUS_ID = 6
                dao.fields.CONSIDER_DATE = CONSIDER_DATE

                dao.fields.FK_STAFF_OFFER_IDA = ddl_staff_offer.SelectedValue
                Try
                    dao.fields.appdate = CDate(txt_app_date.Text)
                Catch ex As Exception

                End Try
                dao.update()


                alert("บันทึกข้อมูลเรียบร้อย")
            Else
                Dim dao As New DAO_DRUG.ClsDBdrsamp
                dao.GetDataby_IDA(_IDA)
                dao_up.GetDataby_IDA(dao.fields.TR_ID)

                AddLogStatus(6, dao_up.fields.PROCESS_ID, _CLS.CITIZEN_ID, _IDA)

                Dim PROCESS_ID As Integer = dao_up.fields.PROCESS_ID


                dao_p.GetDataby_Process_ID(PROCESS_ID)
                Dim GROUP_NUMBER As Integer = dao_p.fields.PROCESS_ID

                Dim CONSIDER_DATE As Date = CDate(TextBox1.Text)
                dao.fields.REMARK = Txt_Remark.Text
                dao.fields.STATUS_ID = 6
                dao.fields.CONSIDER_DATE = CONSIDER_DATE

                dao.fields.FK_STAFF_OFFER_IDA = ddl_staff_offer.SelectedValue
                Try
                    dao.fields.appdate = CDate(txt_app_date.Text)
                Catch ex As Exception

                End Try
                dao.update()


                alert("บันทึกข้อมูลเรียบร้อย")
            End If

        Catch ex As Exception
            Response.Write("<script type='text/javascript'>alert('ตรวจสอบการใส่วันที่');</script> ")
        End Try

    End Sub

    Sub alert(ByVal text As String)
        Response.Write("<script type='text/javascript'>window.parent.alert('" + text + "');parent.close_modal();</script> ")
    End Sub
    Sub alert_reload(ByVal text As String)
        Response.Write("<script type='text/javascript'>window.parent.alert('" + text + "');</script> ")
        Response.Redirect("FRM_NYM_CONFIRM.aspx?IDA=" & _IDA & "&TR_ID=" & _TR_ID & "&process=" & _process)

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("FRM_NYM_CONFIRM.aspx?IDA=" & _IDA & "&TR_ID=" & _TR_ID & "&process=" & _process)
    End Sub
End Class