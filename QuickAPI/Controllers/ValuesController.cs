using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
			//Score sc = new Score
			//{
			//	HomeName = "Chester",
			//	HomeScore = "3",
			//	AwayName = "Alfreton",
			//	AwayScore = "2",
			//	Id = 999
			//};
			//MemoryStream stream1 = new MemoryStream();
			//DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Score));
			//ser.WriteObject(stream1, sc);
			//stream1.Position = 0;
			//StreamReader sr = new StreamReader(stream1);
			//var res = sr.ReadToEnd();
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
		[EnableCors("Wildermuth")]
		[HttpGet("{id}", Name = "ScoreGet")]
		public async Task<IActionResult> Get(int id)
		{
			var scores = await _context.Scores.FindAsync(id);
			return Ok(scores);
		}

		// POST api/values
		[EnableCors("Wildermuth")]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Score score)
		{
			Score newScore = new Score
			{
				HomeName = score.HomeName,
				HomeScore = score.HomeScore,
				AwayName = score.AwayName,
				AwayScore = score.AwayScore
			};

			_context.Scores.Add(newScore);
			var newId = await _context.SaveChangesAsync();
			
			var newUri = Url.Link("ScoreGet", new { id = newScore.Id });
			return Created(newUri, newScore); 
		}

		// PUT api/values/5
		[EnableCors("Wildermuth")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] Score score)
		{
			var existing = await _context.Scores.FindAsync(id);
			if (existing != null)
			{
				existing.HomeName = score.HomeName;
				existing.HomeScore = score.HomeScore;
				existing.AwayName = score.AwayName;
				existing.AwayScore = score.AwayScore;
				_context.Update(existing);
				var result = await _context.SaveChangesAsync();
				return Ok(existing);
			}
			return BadRequest("Cannot find this Score");
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
