using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using SAMSCommon.Classes;
using CrystalDecisions.Shared;

public partial class Forms_frmtesting : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument CrpReport = (ReportDocument)this.Session["CrpReport"];
        if (!Page.IsPostBack)
        {

            try
            {

                CrystalReportViewer1.ReportSource = CrpReport;



            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
      //  CrpReport.Close();
      //  CrpReport.Dispose();
        this.Session.Remove("CrpReport");
        GC.Collect();
    }
   
}
 