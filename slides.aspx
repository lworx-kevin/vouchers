<%@ Page Title="" Language="C#" MasterPageFile="~/VGC.master" AutoEventWireup="true" CodeFile="slides.aspx.cs" Inherits="slides" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContainer" Runat="Server">
     <div class="row">
	<ol class="breadcrumb">
		<li><a href="/"><svg class="glyph stroked home"><use xlink:href="#stroked-home"/></svg></a></li>
        <%=navPath %>
	</ol>
    </div>
    <br />

      
    <div class="panel panel-info">
        <div class="panel-heading">
            <div class="row">
                <div class="col-sm-8"> <h4 class="text-primary">Slides</h4></div>
                <div class="col-sm-4 text-right"><%if (canAdd){ %>
                     <button type="button" class="btn btn-primary" onclick="CreateNew();">Create New</button>    
                <%} %> </div>
            </div>                          
                  
        </div>
        <div class="panel-body">
            <div class="col-md-12">
                <table id="tblUserList" class="table table-bordered table-hover table-condensed table-responsive">
                    <thead>
                        <tr>
                            <th data-priority="-11">ID</th>
                            <th style="width:30% !important" data-priority="1">Title</th>
                            <th style="width:30% !important" data-priority="1" >Active</th> 
                          
	                    <th style="width:30% !important" data-priority="1" >Actions</th>              
                        </tr>
                    </thead>
                    <tbody></tbody>                    
                </table>
            </div>
        </div>
        </div>
    

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"> Slider Master Form in <label id="lblentrymode"></label> Mode</h4>
                </div>
                <div class="modal-body">
                    <div id="dverror"></div>
                    <form class="form-horizontal" role="form" id="frmMaster">

                        <input type="hidden" id="id" name="id" />
                        <input type="hidden" id="EntryMode" name="EntryMode" />
                        <input type="hidden" id="imgsrc" name="imgsrc" />
                       
                        <div class="row">
                            <div class="col-sm-12">

                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="resort_name">Title:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="en_heading" name="en_heading" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="resort_code">Heading :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="en_sub_heading" name="en_sub_heading" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_code">en_action_title :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="en_action_title" name="en_action_title" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_sub_code">en_action_url :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="en_action_url" name="en_action_url" placeholder="" required>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="resort_name">es_heading:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="es_heading" name="es_heading" placeholder=" " required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="resort_code">es_sub_heading :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="es_sub_heading" name="es_sub_heading" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_code">es_action_title :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="es_action_title" name="es_action_title" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_sub_code">es_action_url :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="es_action_url" name="es_action_url" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="rfrort_name">fr_heading:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="fr_heading" name="fr_heading" placeholder=" " required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="rfrort_code">fr_sub_heading :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="fr_sub_heading" name="fr_sub_heading" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_code">fr_action_title :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="fr_action_title" name="fr_action_title" placeholder="" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_sub_code">fr_action_url :</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="fr_action_url" name="fr_action_url" placeholder="" required>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="image_url">image_url:</label>
                                    <div class="col-sm-6">
                                        <%-- <asp:FileUpload ID="logo" runat="server" />--%>
                                        <input type="file" id="image_url" />
                                    </div>
                                      <div class="col-sm-2"><image id="imgurl" src="" style="height:50px;width:70px"></image> </div>
                                </div>
                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="pms_active"> Active :</label>
                                    <div class="col-sm-6">
                                         <input type="checkbox" name="active" id="active" value="1"  data-on-color="success" data-off-color="warning" data-size="small"  data-on-text="Yes" data-off-text="No" class="bootcheckbox" >
                                    </div>
                                </div>
                                  <div class="form-group">
                                    <label class="control-label col-sm-4" for="resort_id">Select  Resort :</label>
                                    <div class="col-sm-6">
                                        <select id="resort_id" name="resort_id" class="select2 form-control " style="width:100% ">                                           
                                           <option value="">--Select --</option>                                       
                                        </select>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="control-label col-sm-4" for="main_page"> Main :</label>
                                    <div class="col-sm-6">
                                         <input type="checkbox" name="main_page" id="main_page" value="1"  data-on-color="success" data-off-color="warning" data-size="small"  data-on-text="Yes" data-off-text="No" class="bootcheckbox" >
                                    </div>
                                </div>
                            </div>
                            
                            </div>
                        <div class="modal-footer">
                           <button id="btnSubmit" type="button" class="btn btn-default" onclick="SaveEntry();">Save</button>
                            <button id="btnCancel" type="button" class="btn btn-default" onclick="hideModel()">Cancel</button>
                        </div>
                    </form>
                </div>

            </div>

        </div>
    </div>


     <script>
         var currPage = "module<%=moduleId%>";
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ctlscript" Runat="Server">
      
    <script type="text/javascript">
        function hideModel() {
            $('#myModal').modal('hide');
        }
        $(document).ready(function () {
            $("#image_url").on("change", function () {
                var files = !!this.files ? this.files : [];
                if (!files.length || !window.FileReader) return;// no file selected, or no FileReader support                
                var reader = new FileReader(); // instance of the FileReader
                reader.readAsDataURL(files[0]); // read the local file
                reader.onloadend = function (e) { // set image data as background of div
                    $("#imgurl").attr("src", e.target.result);
                }
            });
        });
       
        $(document).ready(function () {
            $("#main_page").on('switchChange.bootstrapSwitch', function (event, state) {

                if ($("#main_page").is(":checked")) { $("#resort_id").val(0).trigger('change'); }
                
            });
        });


        $(document).ready(function () {
            var json;
            var $b = $('#resort_id');
            $.ajax({
                type: "POST",
                url: "resorts.aspx/GetResortData",
                dataType: "json",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    json = JSON.parse(msg.d);
                    $b.empty();
                    $b.append("<option value= 0 >--Select--</option>");
                    for (var i = 0; i < json.length ; i++) {
                        $b.append("<option value=" + json[i].resort_id + ">" + json[i].resort_name + "</option>").trigger('change');
                    }
                }
            });
        });

        function BindTable() {
            $("#tblUserList").dataTable().fnDestroy();
            $.ajax({
                type: "POST",
                url: "slides.aspx/GetSlidesData",
                dataType: "json",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {                 
                    $('#tblUserList').dataTable({
                        "responsive": true,"autoWidth": false,
                        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
                        data: JSON.parse(msg.d),
                        columns: [
                                  { data: "id", "visible": false },
                                  { data: "en_heading","width":"30%" },
                                  { data: "status", "width": "30%" },
                                  
                                  {
                                      data: 'sid', "width": "30%", "bSearchable": true, "bSortable": true, "mRender": function (data, type, data)
                                      {
                                          return '<%if (canEdit||canView)
               {%><a href="#" onclick="EditEntry(' + data.id + ',1)"><img src="/imgs/icon_portal_view.png" class="actionIcon"/></a>&nbsp;<%}%> <%if (canEdit)
               {%><a href="#" onclick="EditEntry(' + data.id + ',0)"><img src="/imgs/icon_portal_edit.png" class="actionIcon"/></a>&nbsp;<%}%> <%if (canDelete)
             {%><a href="#" onclick="DeleteEntry(' + data.id + ')"><img src="/imgs/icon_portal_delete.png" class="actionIcon"/></a><%}%> ';
                                      }
                                  },
                                  
                        ]
                    });
                }
            })
        }

        $(document).ready(function () {                
            BindTable();
        });

        function CreateNew() {
            $("#lblentrymode").html("Create");
            $("#EntryMode").val('Add');         
            $("#dverror").empty();
            $("#en_heading").val('');
            $("#en_sub_heading").val('');
            $("#en_action_title").val('');
            $("#en_action_url").val('');
            $("#es_heading").val('');
            $("#es_sub_heading").val('');
            $("#es_action_title").val('');
            $("#es_action_url").val('');
            $("#fr_heading").val('');
            $("#fr_sub_heading").val('');
            $("#fr_action_title").val('');
            $("#fr_action_url").val('');
            $("#image_url").val('');
            $("#active").bootstrapSwitch('state', false);
            $("#main_page").bootstrapSwitch('state', false);
            $("#imgurl").attr("src", '');
            $("#imgsrc").val(' ');
            $('#myModal').modal('show');
        }

        function EditEntry(editid, view) {
            //$("#lblentrymode").html("Modify");            
            $("#dverror").empty();
            $("#EntryMode").val('Edit');

            if (view == 1)
            {
                $("#lblentrymode").html("View");
                $("#frmMaster :input").prop("disabled", true);
                $("#btnSubmit").hide();
                $("#btnCancel").prop("disabled", false)
            }
            else {
                $("#lblentrymode").html("Modify"); $("#frmMaster :input").prop("disabled", false); $("#btnSubmit").show();
            }

            var json;
            $.ajax({
                type: "POST",
                url: "slides.aspx/GetSliderDataByID",
                dataType: "json",
                data: JSON.stringify({ 'id': editid }),
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (res) {
                    json = JSON.parse(res.d);

                    $("#en_heading").val(json[0].en_heading);
                    $("#en_sub_heading").val(json[0].en_sub_heading);
                    $("#en_action_title").val(json[0].en_action_title);
                    $("#en_action_url").val(json[0].en_action_url);
                    $("#es_heading").val(json[0].es_heading);
                    $("#es_sub_heading").val(json[0].es_sub_heading);
                    $("#es_action_title").val(json[0].es_action_title);
                    $("#es_action_url").val(json[0].es_action_url);
                    $("#fr_heading").val(json[0].fr_heading);
                    $("#fr_sub_heading").val(json[0].fr_sub_heading);
                    $("#fr_action_title").val(json[0].fr_action_title);
                    $("#fr_action_url").val(json[0].fr_action_url);
                    $("#image_url").val('');                   
                    $("#imgsrc").val(json[0].image_url);
                    var thumb_img = "http://services.bdagentrewards.com/media/" + json[0].image_url;
                    $("#imgurl").attr("src", thumb_img);
                    if (json[0].active == 1) { $("#active").bootstrapSwitch('state', true); } else { $("#active").bootstrapSwitch('state', false); }
                    $("#resort_id").val(json[0].resort_id).trigger('change');
                    if (json[0].main_page == 1) { $("#main_page").bootstrapSwitch('state', true); } else { $("#main_page").bootstrapSwitch('state', false); }
                    $("#id").val(json[0].id);
                }
            });
            
            $('#myModal').modal('show');
        }

        function SaveEntry() {
           
            var en_heading = $("#en_heading").val();
            var en_sub_heading = $("#en_sub_heading").val();
            var en_action_title = $("#en_action_title").val();
            var en_action_url = $("#en_action_url").val();
            var es_heading = $("#es_heading").val();
            var es_sub_heading = $("#es_sub_heading").val();
            var es_action_title = $("#es_action_title").val();
            var es_action_url = $("#es_action_url").val();
            var fr_heading = $("#fr_heading").val();
            var fr_sub_heading = $("#fr_sub_heading").val();
            var fr_action_title = $("#fr_action_title").val();
            var fr_action_url = $("#fr_action_url").val();

           
            if (window.FormData !== undefined) {
                var data = new FormData();
                var files = $("#image_url").get(0).files;
                if (files.length > 0) {
                    data.append("logoimg", files[0]);

                    $.ajax({
                        url: "http://bdagentrewards.com/upload.ashx",
                        type: "POST",
                        async: false,
                        data: data,
                        contentType: false,
                        processData: false,
                        success: function (result) { $("#imgsrc").val(result); },
                        error: function (err) {
                            alert(err.statusText)
                        }
                    });
                }
            } else { return false; }
           


            var image_url = $("#imgsrc").val();
            var active = 0;
            if ($("#active").is(":checked")) { active = 1; }
            var resortid = $("#resort_id").val();
            var mainpage = 0;
            if ($("#main_page").is(":checked")) { mainpage = 1; }

            var id = $("#id").val();

            if (Validate())
            {
                var url = '';
                if ($("#EntryMode").val() == 'Add') {
                    url = 'slides.aspx/AddSliderData';
                } else {
                    url = 'slides.aspx/EditSliderData';
                }

                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: "json",
                    data: JSON.stringify({
                        'en_heading': en_heading,
                        'en_sub_heading': en_sub_heading,
                        'en_action_title': en_action_title,
                        'en_action_url': en_action_url,
                        'es_heading': es_heading,
                        'es_sub_heading': es_sub_heading,
                        'es_action_title': es_action_title,
                        'es_action_url': es_action_url,
                        'fr_heading': fr_heading,
                        'fr_sub_heading': fr_sub_heading,
                        'fr_action_title': fr_action_title,
                        'fr_action_url': fr_action_url,
                        'image_url': image_url,
                        'active': active,
                        'resortid': resortid,
                        'mainpage': mainpage,
                        'id': id
                    }),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (msg) {
                        $('#myModal').modal('hide');
                        BindTable();
                        $.notify({
                            title: '',
                            message: 'Record Save Succesfully.'
                        }, { type: 'success' });
                    }
                });
            }
           
        }

      
        function Validate() {           
            

            var errortext = "";
                      
            if (errortext != "") {
                var err = "<div class='alert alert-danger'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a><ul>" + errortext + "</ul></div>";
                $("#dverror").empty().append(err);
                return false;
            }
            else {
                return true;
            }


        }

        $(document).ready(function () {
            $("#ip_address").keypress(function (event) {
                return isNumber(event, this)
            });           
        });

        function isNumber(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (
                //(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // "-" CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') == 4) &&      // "." CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57) &&
                (charCode != 8))
                return false;

            return true;
        }
        function DeleteEntry(editid) {
            swal({
                title: "Are you sure?",
                text: "Your will not be able to recover this record!",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes, delete it!",
                closeOnConfirm: false
            },
            function () {
                var json;
                $.ajax({
                    type: "POST",
                    url: "slides.aspx/DeleteDataByID",
                    dataType: "json",
                    data: JSON.stringify({ 'id': editid }),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    success: function (res) {
                        BindTable();
                        swal("Deleted!", "Your record has been deleted.", "success");
                    }
                });

            });

        }

        </script>
</asp:Content>
