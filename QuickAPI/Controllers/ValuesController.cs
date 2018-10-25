using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickAPI.Controllers.Model;
using QuickAPI.Model;

namespace QuickAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly ScoreContext _context;

		public ValuesController(ScoreContext context)
		{
			_context = context;
		}



		// GET api/values
		[EnableCors("Wildermuth")] //Used with cookie authentication
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var scores = _context.Scores.Select(c => c);
			return Ok(await scores.ToListAsync());
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
