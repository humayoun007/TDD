using EFFIK.Tests.Entity;
using EFFIK.Tests.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EFFIK.Tests.Controller
{
    public class EmployeeController: System.Web.Mvc.Controller
    {
        IEmployeeRepository _repository;

        public EmployeeController() : this(new EmployeeRepository()) { }

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        ////  
        //// GET: /Employee/  
        public ViewResult Index()
        {
            ViewData["ControllerName"] = this.ToString();
            return View("Index", _repository.GetAllEmployee());
        }

        ////  
        //// GET: /Employee/Details/5  
        public ActionResult Details(int id = 0)
        {
            //int idx = id.HasValue ? (int)id : 0;  
            Employee cnt = _repository.GetEmployeeById(id);
            return View("Details", cnt);
        }

        //  
        // GET: /Employee/Create  

        public ActionResult Create()
        {
            return View("Create");
        }

        //  
        // POST: /Employee/Create  

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Employee employeeToCreate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.CreateNewEmployee(employeeToCreate);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
                ViewData["CreateError"] = "Unable to create; view innerexception";
            }

            return View("Create");
        }

        //  
        // GET: /Employee/Edit/5  
        public ActionResult Edit(int id = 0)
        {
            var employeeToEdit = _repository.GetEmployeeById(id);
            return View(employeeToEdit);
        }

        //  
        // GET: /Employee/Edit/5  
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Employee cnt = _repository.GetEmployeeById(id);
            try
            {
                if (TryUpdateModel(cnt))
                {
                    _repository.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ViewData["EditError"] = ex.InnerException.ToString();
                else
                    ViewData["EditError"] = ex.ToString();
            }
#if DEBUG
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    if (error.Exception != null)
                    {
                        throw modelState.Errors[0].Exception;
                    }
                }
            }
#endif
            return View(cnt);
        }


        //  
        // GET: /Employee/Delete/5  

        public ActionResult Delete(int id)
        {
            var conToDel = _repository.GetEmployeeById(id);
            return View(conToDel);
        }

        //  
        // POST: /Employee/Delete/5  

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repository.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
