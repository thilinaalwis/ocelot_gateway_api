using EmployeeAPI.Core.Funtions;
using EmployeeAPI.Core.IConfiguration;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                Employee.Id = Guid.NewGuid();
                Employee.DtStamp = DateTime.UtcNow;

                List<string> erros = EmployeeFunctions.ValidateEmployee(Employee);

                if(erros != null && erros.Count>0) 
                    return new JsonResult(JsonConvert.SerializeObject(erros)) { StatusCode = 500 };

                await _unitOfWork.Employees.Add(Employee);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { Employee.Id }, Employee);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var Employee = await _unitOfWork.Employees.GetById(id);

            if (Employee == null)
                return NotFound(); // 404 http status code 

            return Ok(Employee);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Employees = await _unitOfWork.Employees.All();

            return Ok(Employees);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, Employee Employee)
        {
            if (id != Employee.Id)
                return BadRequest();

            List<string> erros = EmployeeFunctions.ValidateEmployee(Employee);

            if (erros != null && erros.Count > 0)
                return new JsonResult(JsonConvert.SerializeObject(erros)) { StatusCode = 500 };

            await _unitOfWork.Employees.Update(Employee);
            await _unitOfWork.CompleteAsync();

            return Ok(Employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Employees.GetById(id);

            if (item == null)
                return new JsonResult("No record Found") { StatusCode = 500 };

            await _unitOfWork.Employees.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
