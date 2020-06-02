﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/Main_E_Tracking.Master" CodeBehind="FRM_E_TRACKING_WORK_TOP5V2.aspx.vb" Inherits="FDA_DRUG.FRM_E_TRACKING_WORK_TOP5V2" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/css_radgrid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/jquery-1.8.3.js"></script>
    <link href="../assets/prettify/prettify.css" rel="stylesheet" />
    <script src="../Charts/FusionCharts.js"></script>
    <script src="../assets/prettify/prettify.js"></script>
    <script src="../assets/ui/js/json2.js"></script>
    <script src="../assets/ui/js/lib.js"></script>

     <script type="text/javascript" >
         $(document).ready(function () {
             $(window).load(function () {
                 $.ajax({
                     type: 'POST',
                     data: { submit: true },
                     success: function (result) {
                         // $('#spinner').fadeOut('slow');
                     }
                 });
             });

             function CloseSpin() {
                 $('#spinner').toggle('slow');
             }

             $('#ContentPlaceHolder1_btn_add').click(function () {

                 //  $('#spinner').toggle('slow');
                 Popups('FRM_E_TRACKING_STAFF_ASIGN_INSERT.aspx');
                 return false;
             });

             $('#ContentPlaceHolder1_btn_download').click(function () {
                 $('#spinner').fadeIn('slow');

             });

             function Popups(url) { // สำหรับทำ Div Popup
                 $('#myModal').modal('toggle'); // เป็นคำสั่งเปิดปิด
                 var i = $('#f1'); // ID ของ iframe   
                 i.attr("src", url); //  url ของ form ที่จะเปิด
             }


         });

         function Popups2(url) { // สำหรับทำ Div Popup
             $('#myModal').modal('toggle'); // เป็นคำสั่งเปิดปิด
             var i = $('#f1'); // ID ของ iframe   
             i.attr("src", url); //  url ของ form ที่จะเปิด
         }
         function close_modal() { // คำสั่งสั่งปิด PopUp
             $('#myModal').modal('hide');
             $('#ContentPlaceHolder1_btn_reload').click(); // ตัวอย่างให้คำสั่งปุ่มที่ซ่อนอยู่ Click
         }
         function spin_space() { // คำสั่งสั่งปิด PopUp
             //    alert('123456');
             $('#spinner').toggle('slow');
             //$('#myModal').modal('hide');
             //$('#ContentPlaceHolder1_Button2').click(); // ตัวอย่างให้คำสั่งปุ่มที่ซ่อนอยู่ Click
         }
         function closespinner() {
             $('#spinner').fadeOut('slow');
             alert('Download Success');
             $('#ContentPlaceHolder1_Button1').click();

         }
        </script> 
    <div id="spinner" style="background-color: transparent;display:none;">
        <img src="../imgs/spinner.gif" alt="Loading" style="position: absolute; top: 120px; left: 293px; height: 185px; width: 207px;" />
    </div>
    <div>
        <div class="panel-heading panel-title" style="padding-left: 5%;">
            <h2> รายงานคำขอที่รอการพิจารณา Top 5</h2>

        </div>

    </div>
    
   <hr />
   <div>
       <table width="100%">
           <tr>
               <td align="right">
                   <asp:Button ID="btn_export" runat="server" Text="EXPORT TO EXCEL" />
                   
               </td>
           </tr>
           <tr>
               <td>
              <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="15">
                  <MasterTableView>
                      <Columns>
                          <telerik:GridBoundColumn DataField="IDA" DataType="System.Int32" FilterControlAltText="Filter IDA column"
                           HeaderText="IDA" ReadOnly="True" SortExpression="IDA" UniqueName="IDA" Display="false">
                       </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn DataField="rcvno" FilterControlAltText="Filter rcvno column"
                           HeaderText="เลขที่รับ" SortExpression="rcvno" UniqueName="rcvno">
                       </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn DataField="wrkuntnm" FilterControlAltText="Filter wrkuntnm column"
                           HeaderText="กลุ่มงาน" SortExpression="wrkuntnm" UniqueName="wrkuntnm">
                       </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn DataField="stfthanm" FilterControlAltText="Filter stfthanm column"
                           HeaderText="ชื่อ-นามสกุลเจ้าหน้าที" SortExpression="stfthanm" UniqueName="stfthanm">
                       </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn DataField="WORK_NAME2" FilterControlAltText="Filter WORK_NAME2 column"
                           HeaderText="ประเภท" SortExpression="WORK_NAME2" UniqueName="WORK_NAME2">
                       </telerik:GridBoundColumn>
                          <telerik:GridBoundColumn DataField="rqt_type" FilterControlAltText="Filter rqt_type column"
                           HeaderText="การรับคำขอ" SortExpression="rqt_type" UniqueName="rqt_type">
                       </telerik:GridBoundColumn>
                      </Columns>
                  </MasterTableView>
              </telerik:RadGrid>
               </td>
           </tr>
       </table>
        </div>
   <div class=" modal fade" id="myModal">              
               <div class="panel panel-info" style="width:100%;">
                   <div class="panel-heading  text-center"><h1>เพิ่มเจ้าหน้าที่ </h1></div>
                   <button type="button" class="btn btn-default pull-right" data-dismiss="modal">ปิดหน้านี้</button>
                   <div class="panel-body">
                             <iframe id="f1"  style="width:100%; height:550px;" ></iframe>
                   </div>
                   <div class="panel-footer"></div>
               </div>       
</div>
</asp:Content>
