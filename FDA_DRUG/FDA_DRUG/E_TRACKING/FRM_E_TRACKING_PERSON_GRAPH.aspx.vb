﻿Imports FDA_DRUG.Graph3DMultiple
Imports System.Web.Script.Serialization
Public Class FRM_E_TRACKING_PERSON_GRAPH
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'bind_graph()
            bind_graph_new()
        End If
    End Sub
    Sub bind_graph_new()
        Dim bao As New BAO.ClsDBSqlcommand
        'Dim dt As New DataTable
        dt = bao.SP_E_TRACKING_PERSON_WORK_BY_GROUP_NEW


        Dim bao_list As New BAO.ClsDBSqlcommand
        Dim dt_list As New DataTable
        'dt_list = bao_list.SP_E_TRACKING_WORK_LIST_ALL()
        dt_list = bao_list.SP_E_TRACKING_WORK_LIST_ALL_BY_GROUP(Request.QueryString("g"))
        dt_list.Columns.Add("date_last", GetType(Date))
        dt_list.Columns.Add("day_cal", GetType(Integer))



        'For Each dr As DataRow In dt_list.Rows
        '    Dim ws As New WS_GETDATE_WORKING.Service1
        '    Dim date_result As Date
        '    ws.GETDATE_WORKING(CDate(dr("rcvdate")), True, dr("WORK_DAYS"), True, date_result, True)
        '    'date_result = date_result.ToLongDateString()
        '    dr("date_last") = date_result
        '    Dim days As Double = 0

        '    Try
        '        days = DateDiff(DateInterval.Day, date_result, CDate(dr("appdate")))
        '    Catch ex As Exception
        '        days = DateDiff(DateInterval.Day, date_result, Date.Now)
        '    End Try

        '    dr("day_cal") = days
        'Next
        For Each dr As DataRow In dt_list.Rows

            Dim days As Double = 0
            Dim date_t As Date
            Try
                If Year(CDate(dr("date_real_cp"))) > 3000 Then
                    date_t = CDate(dr("date_real_cp")).AddYears(-543)
                Else
                    date_t = CDate(dr("date_real_cp"))
                End If
            Catch ex As Exception

            End Try
            Try
                days = DateDiff(DateInterval.Day, date_t, CDate(dr("appdate")))
            Catch ex As Exception
                days = DateDiff(DateInterval.Day, date_t, Date.Now)
            End Try

            dr("day_cal") = days
        Next

        Dim bao2 As New BAO.ClsDBSqlcommand
        Dim dt2 As New DataTable
        Dim dao As New DAO_DRUG.TB_MAS_E_TRACKING_GAP
        dao.GetDataAll()
        Try
            dt2 = bao2.TBL_E_TRACKING_GAP(dao.fields.GAP_SET)
        Catch ex As Exception
            dt2 = bao2.TBL_E_TRACKING_GAP(120)
        End Try
        Dim gid As String = ""
        gid = Request.QueryString("gid")
        Dim col4 As String = Request.QueryString("col4")
        Dim col5 As String = Request.QueryString("col5")

        For Each dr As DataRow In dt.Rows


            'dt_list.Select("day_cal > '" & dt(0)("max_gap") & "'")
            dr("less120") = dt_list.Compute("count(IDA)", "day_cal > '" & dt2(0)("max_gap") & "' and wrkuntcd='" & gid & "' and COL4='" & col4 & "' and COL5='(" & col5 & ")' and ctzid='" & dr("ctzid") & "'")
            dr("less60_to_120") = dt_list.Compute("count(IDA)", "day_cal > '" & dt2(1)("gap") & "' and day_cal <= '" & dt2(1)("max_gap") & "' and wrkuntcd='" & gid & "' and COL4='" & col4 & "' and COL5='(" & col5 & ")' and ctzid='" & dr("ctzid") & "'")
            dr("less0_to_60") = dt_list.Compute("count(IDA)", "day_cal >= '" & dt2(2)("gap") & "' and day_cal < '" & dt2(2)("max_gap") & "' and wrkuntcd='" & gid & "' and COL4='" & col4 & "' and COL5='(" & col5 & ")' and ctzid='" & dr("ctzid") & "'")
            dr("more0_to_60") = dt_list.Compute("count(IDA)", "day_cal >= '" & dt2(3)("max_gap") & "' and day_cal < '" & dt2(3)("gap") & "' and wrkuntcd='" & gid & "' and COL4='" & col4 & "' and COL5='(" & col5 & ")' and ctzid='" & dr("ctzid") & "'")
            dr("more60_to_120") = dt_list.Compute("count(IDA)", "day_cal < '" & dt2(4)("gap") & "' and day_cal >= '" & dt2(4)("max_gap") & "' and wrkuntcd='" & gid & "' and COL4='" & col4 & "' and COL5='(" & col5 & ")' and ctzid='" & dr("ctzid") & "'")
            dr("more120") = dt_list.Compute("count(IDA)", "day_cal < '" & dt2(5)("max_gap") & "' and wrkuntcd='" & gid & "' and COL4='" & col4 & "' and COL5='(" & col5 & ")' and ctzid='" & dr("ctzid") & "'")


        Next
        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            Dim dr As DataRow = dt.Rows(i)
            If dr("less120") = "0" And dr("less60_to_120") = "0" And dr("less0_to_60") = "0" And dr("more0_to_60") = "0" And dr("more60_to_120") = "0" And dr("more120") = "0" Then
                'dr.Delete()
                dt.Rows.RemoveAt(i)
            End If
        Next

        If dt.Rows.Count > 0 Then
            Dim rootobject As New Graph3DMultiple.Rootobject ' Rootobject

            Dim cha As New Graph3DMultiple.Chart
            cha.caption = "รายงานทะเบียนตามรายบุคคล"
            cha.yaxisname = ""
            cha.canvasbgcolor = "FEFEFE"
            cha.canvasbasecolor = "FEFEFE"
            cha.tooltipbgcolor = "DEDEBE"
            cha.tooltipborder = "889E6D"
            cha.divlinecolor = "999999"
            cha.showcolumnshadow = "0"
            cha.divlineisdashed = "1"
            cha.divlinedashlen = "1"
            cha.divlinedashgap = "2"
            cha.numberprefix = ""
            cha.numbersuffix = ""
            cha.showborder = "0"
            cha.formatnumberscale = "0"
            rootobject.chart = cha

            Dim category As New Category
            For Each dr As DataRow In dt.Rows
                Dim cat As New Category1
                cat.label = dr("stfthanm")
                category.category.Add(cat)
            Next

            rootobject.categories.Add(category)

            Dim datase As New Dataset
            datase.seriesname = "จำนวนคำขอเหลือวันมากกว่า " & dt2(0)("max_gap") & "วันขึ้นไป"
            datase.color = "189100"

            Dim datase2 As New Dataset
            datase2.seriesname = "จำนวนคำขอเหลือวันตั้งแต่ " & dt2(1)("gap") & "-" & dt2(1)("max_gap") & "วัน"
            datase2.color = "2ccf0c"

            Dim datase3 As New Dataset
            datase3.seriesname = "จำนวนคำขอเหลือวันตั้งแต่ 1-60 วัน"
            datase3.color = "a2ff00"

            Dim datase4 As New Dataset
            datase4.seriesname = "จำนวนคำขอรออนุมัติ " & dt2(2)("gap") & "-" & dt2(2)("max_gap") & " วัน"
            datase4.color = "f2fa0d"

            Dim datase5 As New Dataset
            datase5.seriesname = "จำนวนคำขอรออนุมัติ " & dt2(2)("gap") & "-" & dt2(2)("max_gap") & " วัน"
            datase5.color = "ffbc2d"

            Dim datase6 As New Dataset
            datase6.seriesname = "จำนวนคำขอรออนุมัติมากกว่า " & dt2(0)("max_gap") & "วันขึ้นไป"
            datase6.color = "ff2d2d"

            For Each dr As DataRow In dt.Rows
                Dim datum As New Datum
                datum.value = dr("less120")
                datum.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=1"
                datase.data.Add(datum)

                Dim datum2 As New Datum
                datum2.value = dr("less60_to_120")
                datum2.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=2"
                datase2.data.Add(datum2)

                Dim datum3 As New Datum
                datum3.value = dr("less0_to_60")
                datum3.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=3"
                datase3.data.Add(datum3)

                Dim datum4 As New Datum
                datum4.value = dr("more0_to_60")
                datum4.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=4"
                datase4.data.Add(datum4)

                Dim datum5 As New Datum
                datum5.value = dr("more60_to_120")
                datum5.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=5"
                datase5.data.Add(datum5)

                Dim datum6 As New Datum
                datum6.value = dr("more120")
                datum6.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=6"
                datase6.data.Add(datum6)
            Next

            rootobject.dataset.Add(datase)
            rootobject.dataset.Add(datase2)
            rootobject.dataset.Add(datase3)
            rootobject.dataset.Add(datase4)
            rootobject.dataset.Add(datase5)
            rootobject.dataset.Add(datase6)

            Dim serializer As New JavaScriptSerializer()
            Dim serializedResult = serializer.Serialize(rootobject)

            HiddenField1.Value = serializedResult
        Else
            HiddenField1.Value = ""
        End If
    End Sub
    Sub bind_grid()

        'Dim bao As New BAO.ClsDBSqlcommand
        'dt = bao.SP_E_TRACKING_WORK_GROUP_ALL
        ''If dt.Rows.Count = 0 Then
        ''    HiddenField1.Value = ""
        ''    Response.Write("<script type='text/javascript'>alert('ไม่พบข้อมูล');</script> ")
        ''Else
        'RadGrid1.DataSource = dt
        'RadGrid1.Rebind()

        bind_graph()

    End Sub
    Sub bind_graph()
        Dim bao As New BAO.ClsDBSqlcommand
        dt = bao.SP_E_TRACKING_PERSON_WORK_BY_GROUP(Request.QueryString("gid"), Request.QueryString("col4"), "(" & Request.QueryString("col5") & ")", Request.QueryString("t"))
        If dt.Rows.Count > 0 Then
            Dim rootobject As New Graph3DMultiple.Rootobject ' Rootobject

            Dim cha As New Graph3DMultiple.Chart
            cha.caption = "รายงานคำขอตามกลุ่มงานย่อย"
            cha.yaxisname = ""
            cha.canvasbgcolor = "FEFEFE"
            cha.canvasbasecolor = "FEFEFE"
            cha.tooltipbgcolor = "DEDEBE"
            cha.tooltipborder = "889E6D"
            cha.divlinecolor = "999999"
            cha.showcolumnshadow = "0"
            cha.divlineisdashed = "1"
            cha.divlinedashlen = "1"
            cha.divlinedashgap = "2"
            cha.numberprefix = ""
            cha.numbersuffix = ""
            cha.showborder = "0"
            cha.formatnumberscale = "0"
            rootobject.chart = cha

            Dim category As New Category
            For Each dr As DataRow In dt.Rows
                Dim cat As New Category1
                cat.label = dr("stfthanm")
                category.category.Add(cat)
            Next

            rootobject.categories.Add(category)

            Dim datase As New Dataset
            datase.seriesname = "จำนวนคำขอเหลือวันมากกว่า 120 วันขึ้นไป"
            datase.color = "189100"

            Dim datase2 As New Dataset
            datase2.seriesname = "จำนวนคำขอเหลือวันตั้งแต่ 60-120 วัน"
            datase2.color = "2ccf0c"

            Dim datase3 As New Dataset
            datase3.seriesname = "จำนวนคำขอเหลือวันตั้งแต่ 1-60 วัน"
            datase3.color = "a2ff00"

            Dim datase4 As New Dataset
            datase4.seriesname = "จำนวนคำขอรออนุมัติ 1-60 วัน"
            datase4.color = "f2fa0d"

            Dim datase5 As New Dataset
            datase5.seriesname = "จำนวนคำขอรออนุมัติ 60-120 วัน"
            datase5.color = "ffbc2d"

            Dim datase6 As New Dataset
            datase6.seriesname = "จำนวนคำขอรออนุมัติมากกว่า 120 วัน"
            datase6.color = "ff2d2d"

            For Each dr As DataRow In dt.Rows
         
                Dim datum As New Datum
                datum.value = dr("less120")
                datum.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=1"
                datase.data.Add(datum)

                Dim datum2 As New Datum
                datum2.value = dr("less60_to_120")
                datum2.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=2"
                datase2.data.Add(datum2)

                Dim datum3 As New Datum
                datum3.value = dr("less0_to_60")
                datum3.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=3"
                datase3.data.Add(datum3)

                Dim datum4 As New Datum
                datum4.value = dr("more0_to_60")
                datum4.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=4"
                datase4.data.Add(datum4)

                Dim datum5 As New Datum
                datum5.value = dr("more60_to_120")
                datum5.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=5"
                datase5.data.Add(datum5)

                Dim datum6 As New Datum
                datum6.value = dr("more120")
                datum6.link = "../E_TRACKING/FRM_E_TRACKING_PERSON_DETAIL_TABLE.aspx?gid=" & Request.QueryString("gid") & "&col4=" & Request.QueryString("col4") _
                    & "&col5=" & Request.QueryString("col5") & "&iden=" & dr("ctzid") & "&t=6"
                datase6.data.Add(datum6)
            Next

            rootobject.dataset.Add(datase)
            rootobject.dataset.Add(datase2)
            rootobject.dataset.Add(datase3)
            rootobject.dataset.Add(datase4)
            rootobject.dataset.Add(datase5)
            rootobject.dataset.Add(datase6)

            Dim serializer As New JavaScriptSerializer()
            Dim serializedResult = serializer.Serialize(rootobject)

            HiddenField1.Value = serializedResult
        Else
            HiddenField1.Value = ""
        End If

    End Sub
End Class