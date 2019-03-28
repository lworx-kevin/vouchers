<%@ Page Title="" Language="C#" MasterPageFile="~/VGC.master" AutoEventWireup="true" CodeFile="PortalContent.aspx.cs" Inherits="PortalContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContainer" runat="Server">
    <div class="row">
        <ol class="breadcrumb">
            <li><a href="/">
                <svg class="glyph stroked home">
                    <use xlink:href="#stroked-home" />
                </svg></a></li>
            <%=navPath %>
        </ol>
    </div>

    <br />


    <div class="panel panel-info">
        <div class="panel-heading">
            <div class="row">
                <div class="col-sm-8">
                    <h4 class="text-primary">Portal Content</h4>
                </div>
                <div class="col-sm-4 text-right">
                    <%if (canAdd){ %> 
                    <button type="button" class="btn btn-primary" onclick="CreateNew();">Create New</button><%} %>
                </div>
            </div>

        </div>
        <div class="panel-body">
            <div class="col-md-12">
                <table id="tblUserList" class="table table-bordered table-hover table-condensed table-responsive">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Title</th>
                            <th>Status</th>
                           <th>Actions</th>
	

                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Asset Category Master Form in
                        <label id="lblentrymode"></label>
                        Mode</h4>
                </div>
                <div class="modal-body">
                    <div id="dverror"></div>
                    <form class="form-horizontal" role="form" id="frmMaster">
                        <input type="hidden" id="id" name="id" />
                        <input type="hidden" id="EntryMode" name="EntryMode" />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="control-label col-sm-3" for="title_en">en_title:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="title_en" name="title_en" placeholder="title_en" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3" for="title_fr">es_title:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="title_fr" name="title_fr" placeholder="title_fr" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3" for="title_es">fr_title:</label>
                                    <div class="col-sm-6">
                                        <input type="text" class="form-control" id="title_es" name="title_es" placeholder="title_es" required>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4" for="status">status :</label>
                                    <div class="col-sm-6">
                                        <input type="checkbox" name="status" id="status" value="1" data-on-color="success" data-off-color="warning" data-size="small" data-on-text="Active" data-off-text="Inactive" class="bootcheckbox">
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel with-nav-tabs panel-primary">
                                        <div class="panel-heading">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#content_en1" data-toggle="tab">content_en</a></li>
                                                <li><a href="#content_fr1" data-toggle="tab">content_fr</a></li>
                                                <li><a href="#content_es1" data-toggle="tab">content_es</a></li>
                                            </ul>
                                        </div>
                                        <div class="panel-body">
                                            <div class="tab-content">
                                                <div class="tab-pane fade in active" id="content_en1">
                                                    <textarea id="content_en" name="content_en" class="ckeditor"></textarea>
                                                </div>
                                                <div class="tab-pane fade" id="content_fr1">
                                                    <textarea id="content_fr" name="content_fr" class="ckeditor"></textarea>
                                                </div>
                                                <div class="tab-pane fade" id="content_es1">
                                                    <textarea id="content_es" name="content_es" class="ckeditor"></textarea>
                                                </div>
                                            </div>
                                        </div>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ctlscript" runat="Server">
    <script type="text/javascript" src="ckeditor/ckeditor.js"></script>
    <script type="text/javascript">

        //$(document).ready(function () {
        //    $("#icon").on("change", function () {
        //        var files = !!this.files ? this.files : [];
        //        if (!files.length || !window.FileReader) return;// no file selected, or no FileReader support                
        //        var reader = new FileReader(); // instance of the FileReader
        //        reader.readAsDataURL(files[0]); // read the local file
        //        reader.onloadend = function (e) { // set image data as background of div
        //            $("#iconurl").attr("src", e.target.result);
        //        }
        //    });

        //});






        function BindTable() {
            $("#tblUserList").dataTable().fnDestroy();
            $.ajax({
                type: "POST",
                url: "PortalContent.aspx/GetPortalContentData",
                dataType: "json",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (msg) {
                    $('#tblUserList').dataTable({
                        "responsive": true,
                        "lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
                        data: JSON.parse(msg.d),
                        columns: [
                             { data: "id", "visible": false },
                             { data: "title_en" },
                              { data: "status" }
                              ,
                              {
                                  data: 'id', "bSearchable": true, "bSortable": true, "mRender": function (data, type, data)
                                  {
              return '<%if (canEdit||canView){%><a href="#" onclick="EditEntry(' + data.id + ',1)"><img src="/imgs/icon_portal_view.png" class="actionIcon"/></a>&nbsp;<%}%><%if (canEdit)
           {%><a href="#" onclick="EditEntry(' + data.id + ',0)"><img src="/imgs/icon_portal_edit.png" class="actionIcon"/></a>&nbsp;<%}%> <%if (canDelete)
         {%><a href="#" onclick="DeleteEntry(' + data.id + ')"><img src="/imgs/icon_portal_delete.png" class="actionIcon"/></a><%}%> '; }
                              }

                        ]
                    });
                }
            });
        }


        $(document).ready(function () {
            BindTable();
        });

        function CreateNew() {
            $("#lblentrymode").html("Create");
            $("#EntryMode").val('Add');
            $("#id").val('0');
            $("#dverror").empty();
            $("#title_fr").val('');
            $("#title_en").val('');
            $("#title_es").val('');
            CKEDITOR.instances.content_en.setData('');
            CKEDITOR.instances.content_fr.setData('');
            CKEDITOR.instances.content_es.setData('');
            $('#myModal').modal('show');
        }

        function hideModel() {
            $('#myModal').modal('hide');
        }
        function EditEntry(editid, view) {
            if (view == 1) {
                $("#lblentrymode").html("View");
                $("#frmMaster :input").prop("disabled", true);
                $("#btnSubmit").hide();
                $("#btnCancel").prop("disabled", false)
            }
            else {
                $("#lblentrymode").html("Modify");
                $("#frmMaster :input").prop("disabled", false);
                $("#btnSubmit").show();
            }
            //$("#lblentrymode").html("Modify");
            $("#dverror").empty();
            $("#EntryMode").val('Edit');
            var json;
            $.ajax({
                type: "POST",
                url: "PortalContent.aspx/GetPortalContentDataByID",
                dataType: "json",
                data: JSON.stringify({ 'id': editid }),
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                success: function (res) {
                    json = JSON.parse(res.d);
                    $("#title_en").val(json[0].title_en);
                    $("#title_fr").val(json[0].title_fr);
                    $("#title_es").val(json[0].title_es);
                    CKEDITOR.instances.content_en.setData(json[0].content_en);
                    CKEDITOR.instances.content_fr.setData(json[0].content_fr);
                    CKEDITOR.instances.content_es.setData(json[0].content_es);
                    if (json[0].status == 1) { $("#status").bootstrapSwitch('state', true); } else { $("#status").bootstrapSwitch('state', false); }
                    $("#id").val(json[0].id);
                }
            });

            $('#myModal').modal('show');
        }

        function SaveEntry() {
            waitingDialog.show("Saving  Data Please Wait..");
            $.ajaxSetup({
                async: false
            });
            var title_en = $("#title_en").val();
            var title_fr = $("#title_fr").val();
            var title_es = $("#title_es").val();
            var content_en = CKEDITOR.instances.content_en.getData();
            var content_fr = CKEDITOR.instances.content_fr.getData();
            var content_es = CKEDITOR.instances.content_es.getData();
            var status = 0;
            if ($("#status").is(":checked")) { status = 1; }
            var id = $("#id").val();


            if (Validate()) {
                var url = '';
                if ($("#EntryMode").val() == 'Add') {
                    url = 'PortalContent.aspx/AddPortalContentData';
                } else {
                    url = 'PortalContent.aspx/EditPortalContentData';
                }

                $.ajax({
                    type: "POST",
                    url: url,
                    dataType: "json",
                    data: JSON.stringify({
                        'title_en': title_en,
                        'title_fr': title_fr,
                        'title_es': title_es,
                        'content_en': content_en,
                        'content_fr': content_fr,
                        'content_es': content_es,
                        'status': status,
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
            waitingDialog.hide();
        }

        function Validate() {
            var entitle = $("#title_en").val();
            var errortext = "";
            if (entitle == "") {
                errortext += "<li><label>Please Enter title_en Name</label></li>";

            }
            if (errortext != "") {
                var err = "<div class='alert alert-danger'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a><ul>" + errortext + "</ul></div>";
                $("#dverror").empty().append(err);
                return false;
            }
            else {
                return true;
            }


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
                    url: "PortalContent.aspx/DeleteDataByID",
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

