using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExportCSV.DAL;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace ExportCSV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployeeDetails()
        {
            EmployeeDetailsDataContext EmployeeContext = new EmployeeDetailsDataContext();
            var Data = EmployeeContext.EmployeeDetails.ToList();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult UploadDetails()
        {
            ViewBag.Message = "Please Upload Employee Details";

            return View();
        }

        
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            StringBuilder strValidations = new StringBuilder(string.Empty);
            try
            {
                if (uploadFile.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                    Path.GetFileName(uploadFile.FileName));
                    uploadFile.SaveAs(filePath);
                    DataSet ds = new DataSet();

                    string ConnectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""", HttpContext.Server.MapPath("../Uploads"));
                    using (OleDbConnection conn = new System.Data.OleDb.OleDbConnection(ConnectionString))
                    {
                        conn.Open();
                        using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                        {
                            string query = "SELECT * FROM "+uploadFile.FileName;
                            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                            //DataSet ds = new DataSet();
                            adapter.Fill(ds, "Items");
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    using (EmployeeDetailsDataContext DataContext = new EmployeeDetailsDataContext())
                                    {
                                        IList<EmployeeDetail> EmpList = new List<EmployeeDetail>();
                                        EmployeeDetail emp;
                                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                        {
                                            try
                                            {
                                                emp = new EmployeeDetail();

                                                emp.FirstName = ds.Tables[0].Rows[j]["first_name"].ToString();
                                                emp.LastName = ds.Tables[0].Rows[j]["last_name"].ToString();
                                                emp.CompanyName = ds.Tables[0].Rows[j]["company_name"].ToString();
                                                emp.Address = ds.Tables[0].Rows[j]["address"].ToString();
                                                emp.City = ds.Tables[0].Rows[j]["city"].ToString();
                                                emp.County = ds.Tables[0].Rows[j]["county"].ToString();
                                                emp.Postal = ds.Tables[0].Rows[j]["postal"].ToString();
                                                emp.Phone1 = ds.Tables[0].Rows[j]["phone1"].ToString();
                                                emp.Phone2 = ds.Tables[0].Rows[j]["phone2"].ToString();
                                                emp.Email = ds.Tables[0].Rows[j]["email"].ToString();
                                                emp.Web = ds.Tables[0].Rows[j]["web"].ToString();
                                                EmpList.Add(emp);
                                            }
                                            catch (Exception exp)
                                            { }

                                        }
                                        DataContext.EmployeeDetails.AddRange(EmpList);
                                        DataContext.SaveChanges();
                                    }
                                    
                                }
                            }
                            
                        }
                    }
                    RedirectToAction("Index", "Home");
                }
                
            }
            catch (Exception ex) { }
            return View("Index");
        }
    }
}