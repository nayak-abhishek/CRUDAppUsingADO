using CRUDAppUsingADO.Dto;
using CRUDAppUsingADO.Models;
using CRUDAppUsingADO.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAppUsingADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDataAccessLayer dal;
        public EmployeesController()
        {
            dal = new EmployeeDataAccessLayer();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var emps = await dal.GetAllEmployeesAsync();

            return Ok(emps);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeDto empDto)
        {
            await dal.AddEmployeeAsync(empDto);

            return Ok("New Employee Added Succesfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await dal.GetEmployeeByIdAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(int id, UpdateEmployeeDto empDto)
        {
            await dal.UpdateEmployeeAsync(id, empDto);

            return Ok("Employee Edited Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            await dal.DeleteEmployeeAsync(id);

            return Ok("Employee Deleted Successfully");
        }

    }
}
