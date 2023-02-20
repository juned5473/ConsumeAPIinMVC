using AutoMapper;
using KTMVCAPP1.Data;
using KTMVCAPP1.Models;
using KTMVCAPP1.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KTMVCAPP1.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        private readonly IEmployeeRepository  _empRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository empRespositary, IMapper mapper)
        {
           _empRepository = empRespositary;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            var employees = _empRepository.GetAll();
            var empList = _mapper.Map<List<EmployeeDTO>>(employees);
            return View(empList);
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            Employee emp = _empRepository.GetEmployeeByID(id);
            return View(emp);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeDTO emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(emp);
                    _empRepository.InsertEmployee(employee);
                    return RedirectToAction("index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");
        
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employees = _empRepository.GetEmployeeByID(id);
            var employeeList = _mapper.Map<EmployeeDTO>(employees);
            return View(employeeList);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeDTO emp)
        {
            int empId = emp.Id;
            var employeeList = _empRepository.GetEmployeeByID(empId);
            _mapper.Map(emp, employeeList);
            try
            {
                if (ModelState.IsValid)
                {
                    _empRepository.UpdateEmployee(employeeList);                    /*ViewBag.Messsage = "Record Updated Successfully";*/
                    return RedirectToAction("index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Employee emp = _empRepository.GetEmployeeByID(Id);
            return View(emp);
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            

            try
            {

                _empRepository.DeleteEmployee(employee);
                return RedirectToAction("index");

            }
            catch (Exception err)
            {
                TempData["errorMessage"] = err.Message;
                return View();
            }
        }


    }
}
