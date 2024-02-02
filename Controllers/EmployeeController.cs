using Crud_Task.Context;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Crud_Task.Controllers
{
    public class EmployeeController : Controller
    {
        MVCEntities2 db = new MVCEntities2();
        // GET: Employee
        public ActionResult Employee(Employee obj)
        {
            if (obj != null)
            {
                return View(obj);
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee empmodel)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Employee obj = new Employee();
                obj.Id = empmodel.Id;
                obj.Name = empmodel.Name;
                obj.Email = empmodel.Email;
                obj.Mobile = empmodel.Mobile;
                obj.Description = empmodel.Description;

                if (empmodel.Id == 0)
                {
                    db.Employees.Add(obj);
                }
                else
                {
                    db.Entry(obj).State = EntityState.Modified;
                }

                db.SaveChanges();

                ModelState.Clear();
            }

            return RedirectToAction("EmployeeList");
        }


        public ActionResult EmployeeList()
        {
            var res = db.Employees.ToList();
            return View(res);
        }

        public ActionResult ConfirmDelete(int id)
        {
            var res = db.Employees.Where(x => x.Id == id).First();
            db.Employees.Remove(res);
            db.SaveChanges();

            var list = db.Employees.ToList();
            return View("EmployeeList", list);
        }
    }
}