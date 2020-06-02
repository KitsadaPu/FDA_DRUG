﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/POPUP.Master" CodeBehind="FRM_SUBSTITUTE_TABEAN_PREVIEW.aspx.vb" Inherits="FDA_DRUG.FRM_SUBSTITUTE_TABEAN_PREVIEW" %>
<%@ Register Src="~/UC/UC_GRID_ATTACH.ascx" TagPrefix="uc1" TagName="UC_GRID_ATTACH" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" >
        $(document).ready(function () {
            $(window).load(function () {
                $.ajax({
                    type: 'POST',
                    data: { submit: true },
                    success: function (result) {
                        //    $('#spinner').fadeOut('slow');
                    }
                });
            });

            function CloseSpin() {
                $('#spinner').toggle('slow');
            }

            $('#ContentPlaceHolder1_btn_upload').click(function () {

                $('#spinner').toggle('slow');
                Popups('POPUP_LCN_UPLOAD.aspx');
                return false;
            });

            $('#ContentPlaceHolder1_btn_download').click(function () {
                $('#spinner').fadeIn('slow');
                Popups('POPUP_LCN_DOWNLOAD.aspx');
                return false;
            });

            function Popups(url) { // สำหรับทำ Div Popup
                $('#myModal').modal('toggle'); // เป็นคำสั่งเปิดปิด
                var i = $('#f1'); // ID ของ iframe   
                i.attr("src", url); //  url ของ form ที่จะเปิด
            }

            function close_modal() { // คำสั่งสั่งปิด PopUp
                $('#myModal').modal('hide');
                $('#ContentPlaceHolder1_btn_reload').click(); // ตัวอย่างให้คำสั่งปุ่มที่ซ่อนอยู่ Click
            }
        });

        function spin_space() { // คำสั่งสั่งปิด PopUp
            //    alert('123456');
            $('#spinner').toggle('slow');
            //$('#myModal').modal('hide');
            //$('#ContentPlaceHolder1_Button2').click(); // ตัวอย่างให้คำสั่งปุ่มที่ซ่อนอยู่ Click

        }
        function closespinner() {
            $('#spinner').fadeOut('slow');
            alert('Download Success');
            Loaddata();
        }
        </script> 
  <div id="spinner" style=" background-color:transparent;display:none; " >
  <img src="../imgs/spinner.gif" alt="Loading" style="position: absolute; top: 120px; left: 293px; height: 185px; width: 207px;" />
                
</div>

    <div>

         <asp:HyperLink ID="hl_reader" runat="server" Target="_blank" CssClass="btn-control" >
                 <input type="button" value="เปิดจาก acrobat reader"   class="btn-lg"   style="  Width:70%;" />
                       </asp:HyperLink>
         <asp:HiddenField ID="HiddenField1" runat="server" />
         <asp:HiddenField ID="HiddenField2" runat="server" />
    </div>
    <table style="width:100%;height:500px;">
        <tr>
            <td rowspan="2" style="width:70%;">

                <%--<uc1:UC_CONFIRM ID="UC_CONFIRM1" runat="server" />--%>
                <div >
                     <asp:Literal ID="lr_preview" runat="server" ></asp:Literal>
    </div>
            </td>
             <td style="padding-left:10%;height:50%;">

                 <table class="table" style="width:90%"> 
                     <tr><td>
                         <asp:DropDownList ID="ddl_template" runat="server" Width="80%" AutoPostBack="True" DataTextField="NAME_TEMPLATE" DataValueField="ROWS">
                           <%--  <asp:ListItem Value="0">---เลือกแบบ pdf---</asp:ListItem>
                             <asp:ListItem Value="1">แบบที่ 1</asp:ListItem>
                             <asp:ListItem Value="2">แบบที่ 2</asp:ListItem>
                             <asp:ListItem Value="3">แบบที่ 3</asp:ListItem>
                             <asp:ListItem Value="4">แบบที่ 4</asp:ListItem>
                             <asp:ListItem Value="5">แบบที่ 5</asp:ListItem>--%>
                         </asp:DropDownList>
                         
                         </td></tr>
                     <tr><td>
                                                  <asp:Button ID="btn_load" runat="server" Text="Download PDF" CssClass="btn-lg"   Width="80%" />
                         
                         </td></tr>
<%--                     <tr><td>
                         <asp:Label ID="Label1" runat="server" Text="กรอกเลขทะเบียน" style="display:none;"></asp:Label >   <asp:TextBox ID="txt_rgtno" runat="server" Width="80%" style="display:none;"></asp:TextBox>
                         <br />
                         <asp:Label ID="Label2" runat="server" Text="(เช่น 1/62)" style="display:none;"></asp:Label>
                         
                         </td></tr>--%>
                     <tr><td>&nbsp;</td></tr>
                     <tr><td> &nbsp;</td></tr>
                     <tr><td>  &nbsp;</td></tr>
                     <tr><td>  &nbsp;</td></tr>
                     <tr><td>  &nbsp;</td></tr>

                 </table>
                 


             </td>
        </tr>
        <tr>
             <td style="width:30%;height:50%;padding-left:10%">
                
                 &nbsp;</td>
        </tr>
        </table>

</asp:Content>
