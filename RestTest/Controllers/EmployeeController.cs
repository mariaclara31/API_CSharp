using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestTest.Models;
using RestTest.Repository;
using RestTest.Models.DataManager;
using System.Collections.Generic;

namespace RestTest.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataRepository<Employee> _dataRepository;

        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //Get all employees
        //api/employee
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = _dataRepository.GetAll();
            return Ok(employees);
        }

        //Get employee by Id 
        //Get: api/employee/2
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            Employee employee = _dataRepository.Get(id);

            if (employee == null)
            {
                return NotFound("Employee record could not be found");
            }

            return Ok(employee);
        }

        //Post: api/employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee is Null");
            }

            _dataRepository.Add(employee);
            return CreatedAtRoute(
                "Get",
                new { Id = employee.ID },
                employee);
        }

        //PUT: /employee/3
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Employee employee) 
        {
            if (employee == null)
            {
                return BadRequest("Employee is Null");
            }

            Employee employeeToUpdate = _dataRepository.Get(id);
            if(employeeToUpdate == null) 
            {
                return NotFound("Employee record could not be found");
            }

            _dataRepository.Update(employeeToUpdate, employee);
            return Ok(employee);
        }

        //Delete: /employee/2
        [HttpDelete("{id}")]
        public IActionResult Delete(long id) 
        {
            Employee employee = _dataRepository.Get(id);
            if(employee == null)
            { 
                return NotFound("Employee record could not be found");
            }

            _dataRepository.Delete(employee);
            return Ok(employee);
        }
    }
}
