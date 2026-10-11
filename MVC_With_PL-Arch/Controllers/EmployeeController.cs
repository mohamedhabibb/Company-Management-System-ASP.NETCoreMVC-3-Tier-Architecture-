using AutoMapper;
using BLL_Arch.Interfaces;
using BLL_Arch.Repositories;
using DAL_Arch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_With_PL_Arch.Helpers;
using MVC_With_PL_Arch.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_With_PL_Arch.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
       
        
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(UnitOfWork unitOfWork, IMapper mapper)
        { 
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employee;
            if(string.IsNullOrEmpty(SearchValue))
               employee =await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
                employee = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchValue);

            var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
            return View(MappedEmployee);
        }
        public IActionResult Create()
        {
            //ViewBag.Departments=_departmentRepository.GetAll();  
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeMV)
        {
            if (ModelState.IsValid)
            {
                #region Manul Mapper
                // 
                //var employeeMapper = new Employee()
                //{
                //    Name = employeeMV.Name,
                //    Age = employeeMV.Age,
                //    Salary = employeeMV.Salary,
                //    IsActive = employeeMV.IsActive,
                //    Address = employeeMV.Address,
                //};
                ////var employee = (Employee)new EmployeeViewModel();
                ///
                #endregion
                employeeMV.ImageName= DecumentSettings.UploadFile(employeeMV.Image, "Images");
                var MappedEmployee=_mapper.Map<EmployeeViewModel, Employee>(employeeMV);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeMV);
        }
        public async Task<IActionResult> Details(int? id, string ViewName)
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee=_mapper.Map<Employee ,EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null)
            //    return BadRequest(); 
            //var Employee = _employeeRepository.GetById(id.Value);
            //if (Employee is null)
            //    return NotFound();
            //return View(Employee);
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                    int Result =await _unitOfWork.CompleteAsync();
                    if(Result>0 && employeeVM.ImageName is not null)
                    {
                        DecumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                    }
                    return RedirectToAction(nameof(Index));

                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeVM);
                }
            }
            return View(employeeVM);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null)
            //    return BadRequest(); 
            //var Employee = _employeeRepository.GetById(id.Value);
            //if (Employee is null)
            //    return NotFound();
            //return View(Employee);
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    employeeVM.ImageName = DecumentSettings.UploadFile(employeeVM.Image, "Images");
                    var MappedEmployee=_mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);

        }
    }
}













//////////////////////////////////////////////////
//////////////////////////////////////////////////
//using AutoMapper;
//using BLL_Arch.Interfaces;
//using BLL_Arch.Repositories;
//using DAL_Arch.Models;
//using Microsoft.AspNetCore.Mvc;
//using MVC_With_PL_Arch.Helpers;
//using MVC_With_PL_Arch.ViewModels;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace MVC_With_PL_Arch.Controllers
//{
//    public class EmployeeController : Controller
//    {


//        private readonly UnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public EmployeeController(UnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }

//        public async Task<IActionResult> Index(string SearchValue)
//        {
//            IEnumerable<Employee> employee;
//            if (string.IsNullOrEmpty(SearchValue))
//                employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
//            else
//                employee = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchValue);

//            var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employee);
//            return View(MappedEmployee);
//        }
//        public IActionResult Create()
//        {
//            //ViewBag.Departments=_departmentRepository.GetAll();  
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create(EmployeeViewModel employeeMV)
//        {
//            if (ModelState.IsValid)
//            {
//                #region Manul Mapper
//                // 
//                //var employeeMapper = new Employee()
//                //{
//                //    Name = employeeMV.Name,
//                //    Age = employeeMV.Age,
//                //    Salary = employeeMV.Salary,
//                //    IsActive = employeeMV.IsActive,
//                //    Address = employeeMV.Address,
//                //};
//                ////var employee = (Employee)new EmployeeViewModel();
//                ///
//                #endregion
//                employeeMV.ImageName = DecumentSettings.UploadFile(employeeMV.Image, "Images");
//                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeMV);
//                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
//                await _unitOfWork.CompleteAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(employeeMV);
//        }
//        public async Task<IActionResult> Details(int? id, string ViewName)
//        {
//            if (id is null)
//                return BadRequest();
//            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
//            if (employee is null)
//                return NotFound();
//            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
//            return View(ViewName, MappedEmployee);
//        }
//        public async Task<IActionResult> Delete(int? id)
//        {
//            //if (id is null)
//            //    return BadRequest(); 
//            //var Employee = _employeeRepository.GetById(id.Value);
//            //if (Employee is null)
//            //    return NotFound();
//            //return View(Employee);
//            return await Details(id, "Delete");
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
//        {
//            if (id != employeeVM.Id)
//                return BadRequest();

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
//                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);
//                    int Result = await _unitOfWork.CompleteAsync();
//                    if (Result > 0 && employeeVM.ImageName is not null)
//                    {
//                        DecumentSettings.DeleteFile(employeeVM.ImageName, "Images");
//                    }
//                    return RedirectToAction(nameof(Index));

//                }
//                catch (System.Exception ex)
//                {
//                    ModelState.AddModelError(string.Empty, ex.Message);
//                    return View(employeeVM);
//                }
//            }
//            return View(employeeVM);

//        }

//        [HttpGet]
//        public async Task<IActionResult> Edit(int? id)
//        {
//            //if (id is null)
//            //    return BadRequest(); 
//            //var Employee = _employeeRepository.GetById(id.Value);
//            //if (Employee is null)
//            //    return NotFound();
//            //return View(Employee);
//            return await Details(id, "Edit");

//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
//        {
//            if (id != employeeVM.Id)
//                return BadRequest();

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    employeeVM.ImageName = DecumentSettings.UploadFile(employeeVM.Image, "Images");
//                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
//                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
//                    await _unitOfWork.CompleteAsync();
//                    return RedirectToAction(nameof(Index));

//                }
//                catch (System.Exception ex)
//                {
//                    ModelState.AddModelError(string.Empty, ex.Message);
//                }
//            }
//            return View(employeeVM);

//        }
//    }
//}
