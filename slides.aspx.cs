using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.IO.Compression;
using System.Globalization;
using System.Text;
using System.IO;

public partial class slides : BaseClass
{
    public string navPath;
    public string moduleId;
    public string functionId;
    #region role management variables
    public bool canAdd;
    public bool canEdit;
    public bool canDelete;
    public bool canAppr;
    public bool canView;
    public string scriptString = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["role"] == null)
        {
            Response.Redirect("Login.aspx", true);
        }
        functionInfo fInfo = new functionInfo(this.Page.ToString());
        navPath = fInfo.navInfo;
        moduleId = fInfo.moduleId;
        functionId = fInfo.functionId;
        bdcommon.getpermissions(Session["role"].ToString(),
        Session["permissions"].ToString(), functionId, out canAdd,
        out canEdit, out canDelete, out canView, out scriptString,out canAppr);

        if ((!canAdd) && (!canEdit) && (!canDelete) && (!canView))
        {
            Response.Redirect("/");
        }
        //navPath = this.Page.ToString();

    }

    [WebMethod]
    public static string GetSlidesData()
    {
        //send id=0 for all data
        var data = jdataTable.getSlides();

        HttpContext context = HttpContext.Current;
        context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
        HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
        HttpContext.Current.Response.ContentType = "application/json charset=utf-8";

        return data;
    }

    [WebMethod]
    public static string GetSliderData()
    {
        //send id=0 for all data
        var data = BAL.Select_Resort(0);

        HttpContext context = HttpContext.Current;
        context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
        HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
        HttpContext.Current.Response.ContentType = "application/json charset=utf-8";

        return data;
    }

    [WebMethod]
    public static string GetSliderDataByID(int id)
    {
        //send id=0 for all data
        
        var data = BAL.Select_Slides(id);

        HttpContext context = HttpContext.Current;
        context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
        HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
        HttpContext.Current.Response.ContentType = "application/json charset=utf-8";

        return data;
    }

    [WebMethod]
    public static string AddSliderData
        (string en_heading, string en_sub_heading, string en_action_title, string en_action_url,
        string es_heading, string es_sub_heading, string es_action_title, string es_action_url,
        string fr_heading, string fr_sub_heading, string fr_action_title, string fr_action_url, 
        string image_url, int active ,int resortid, int mainpage)
    {

        var data = BAL.AddEdit_sliders
            (en_heading, en_sub_heading, en_action_title, en_action_url, es_heading, es_sub_heading,
            es_action_title, es_action_url, fr_heading, fr_sub_heading, fr_action_title, fr_action_url,
            image_url, active, resortid, mainpage, 0, 1);

        HttpContext context = HttpContext.Current;
        context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
        HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
        HttpContext.Current.Response.ContentType = "application/json charset=utf-8";

        return data;
    }

    [WebMethod]
    public static string EditSliderData
        (string en_heading, string en_sub_heading, string en_action_title, string en_action_url, string es_heading,
        string es_sub_heading, string es_action_title, string es_action_url, string fr_heading, string fr_sub_heading, 
        string fr_action_title, string fr_action_url, string image_url, int active , int resortid, int mainpage, int id)
    {

        var data = BAL.AddEdit_sliders
            (en_heading, en_sub_heading, en_action_title, en_action_url, es_heading, es_sub_heading,
            es_action_title, es_action_url, fr_heading, fr_sub_heading, fr_action_title, fr_action_url, 
            image_url, active, resortid, mainpage, id, 2);

        HttpContext context = HttpContext.Current;
        context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
        HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
        HttpContext.Current.Response.ContentType = "application/json charset=utf-8";

        return data;
    }

    [WebMethod]
    public static string DeleteDataByID(int id)
    {
        //send id=0 for all data
        var data = BAL.Delete_Slides(id);

        HttpContext context = HttpContext.Current;
        context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
        HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
        HttpContext.Current.Response.ContentType = "application/json charset=utf-8";

        return data;
    }
}