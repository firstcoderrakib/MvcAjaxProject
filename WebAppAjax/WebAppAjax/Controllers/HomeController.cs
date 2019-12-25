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
        public JsonResult SaveDataInDatabase(StudentViewModel model)
        {
            var result = false;
            try
            {
                if(model.StudentId > 0)
                {
                    tblStudent Stu = db.tblStudents.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == model.StudentId);
                    Stu.StudentName = model.StudentName;
                    Stu.Email = model.Email;
                    Stu.DepartmentId = model.DepartmentId;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    tblStudent Stu = new tblStudent();
                    Stu.StudentName = model.StudentName;
                    Stu.Email = model.Email;
                    Stu.DepartmentId = model.DepartmentId;
                    Stu.IsDeleted = false;
                    db.tblStudents.Add(Stu);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       public JsonResult DeleteStudentRecord(int StudentId)
        {
            bool result = false;
            tblStudent Stu = db.tblStudents.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == StudentId);
            if(Stu != null)
            {
                Stu.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
