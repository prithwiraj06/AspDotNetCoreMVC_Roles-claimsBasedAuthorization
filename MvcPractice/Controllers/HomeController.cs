using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MvcPractice.Models;
using MvcPractice.ViewModels;

namespace MvcPractice.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IHostingEnvironment _hostingEnvironment;
        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            var employee = _employeeRepository.GetEmployee(id);
            if(employee == null)
            {
                return View("EmployeeNotFound", id);
            }
            else
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel()
                {
                    Employee = employee,
                    PageTitle = "Employee Data"
                };
                return View(employeeViewModel);
            }
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(model.Photo != null)
                {
                    uniqueFileName = SaveUploadedPhoto(model);
                }
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.CreateEmployee(newEmployee);    
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(editEmployeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel updatedEmployeeData)
        {
            Employee employee = _employeeRepository.GetEmployee(updatedEmployeeData.Id);
            if (ModelState.IsValid)
            {                
                employee.Name = updatedEmployeeData.Name;
                employee.Email = updatedEmployeeData.Email;
                employee.Department = updatedEmployeeData.Department;
                if(updatedEmployeeData.Photo != null)
                {
                    if(updatedEmployeeData.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedEmployeePhotos", updatedEmployeeData.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = SaveUploadedPhoto(updatedEmployeeData);
                }
            }
            _employeeRepository.Update(employee);
            return RedirectToAction("Index");
        }

        public string SaveUploadedPhoto(CreateEmployeeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string UploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedEmployeePhotos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string FilePath = Path.Combine(UploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);

                }
            }
            return uniqueFileName;
        }


    }
}
