using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAjax.Models;

namespace WebAppAjax.Controllers
{
    public class HomeController : Controller
    {
        MVCTUTORIALSEntities db = new MVCTUTORIALSEntities();

        public ActionResult Index()
        {
            List<tblDepartment> DetptList = db.tblDepartments.ToList();
            ViewBag.ListOfDeartment = new SelectList(DetptList, "DepartmentId", "DepartmentName");
            return View();
        }

        public JsonResult GetStudentList()
        {
            List<StudentViewModel> StuList = db.tblStudents.Where(x => x.IsDeleted == false).Select(x => new StudentViewModel
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Email = x.Email,
                DepartmentName = x.tblDepartment.DepartmentName

            }).ToList();
            return Json(StuList, JsonRequestBehavior.AllowGet);
        }

      public JsonResult GetStudentById(int StudentId)
      {
            tblStudent model = db.tblStudents.Where(x => x.StudentId == StudentId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}
