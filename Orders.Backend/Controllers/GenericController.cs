using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    public class GenericController<T> :Controller where T : class
    {
        private readonly IGenericUnitOfWork<T> _unitOfWork;

        public GenericController(IGenericUnitOfWork<T> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]

        public  virtual async Task<IActionResult> GetAsync()

        {
            var action  = await _unitOfWork.GetAsync();

            if (action.wasSuccess)
            {

                return Ok(action.Result);
            }
            return BadRequest();
           
        }

        [HttpGet("{id}")]

        public virtual async Task<IActionResult> GetAsync(int id)

        {
            var action = await _unitOfWork.GetAsync(id);

            if (action.wasSuccess)
            {

                return Ok(action.Result);
            }
            return NotFound();

        }

        [HttpPost]

        public virtual async Task<IActionResult> PostAsync(T model)

        {
            var action = await _unitOfWork.AddAsync(model);

            if (action.wasSuccess)
            {

                return Ok(action.Result);
            }
            return BadRequest(action.Message);

        }

        [HttpPut]

        public virtual async Task<IActionResult> PutAsync(T model)

        {
            var action = await _unitOfWork.UpdateAsync(model);

            if (action.wasSuccess)
            {

                return Ok(action.Result);
            }
            return BadRequest(action.Message);

        }

        [HttpDelete("{id}")]

        public virtual async Task<IActionResult> DeleteAsync(int id)

        {
            var action = await _unitOfWork.DeleteAsync(id);

            if (action.wasSuccess)
            {

                return NoContent();
            }
            return BadRequest(action.Message);

        }
    }
}
