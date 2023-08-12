using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using VehicleManager.Models;
using VehicleManagerApi.Controllers;
using VehicleManagerApi.Data;

namespace VehicleManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        private readonly IRepository<T> repo;

        public BaseController(IRepository<T> repo)
        {
            this.repo = repo;
        }

        // GET: api/Cars
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get(string? filter = null)
        {
            if (await repo.GetAllAsync() == null)
            {
                return NotFound();
            }
            if (filter == null)
            {
                return await repo.GetAllAsync();
            }

            var p = Expression.Parameter(typeof(T), "x");
            var e = (Expression)DynamicExpressionParser.ParseLambda(new[] { p }, null, filter);
            var typedExpression = (Expression<Func<T, bool>>)e;
            return await repo.GetAllAsync(typedExpression);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int id)
        {
            if (await repo.GetAllAsync() == null)
            {
                return NotFound();
            }
            var entity = await repo.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }


        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                await repo.UpdateAsync(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<T>> Post(T entity)
        {
            if (await repo.GetAllAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext' is null.");
            }
            await repo.CreateAsync(entity);

            //return CreatedAtAction($"Get{typeof(T)}", new { id = entity.Id }, entity);
            return Ok(entity);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await repo.GetAllAsync() == null)
            {
                return NotFound();
            }
            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            await repo.DeleteAsync(entity);

            return NoContent();
        }

        private bool Exists(int id)
        {
            return repo.GetByIdAsync(id) != null;
        }
    }
}