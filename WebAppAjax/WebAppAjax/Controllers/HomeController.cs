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

      
    }
}