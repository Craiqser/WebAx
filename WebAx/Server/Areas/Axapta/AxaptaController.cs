using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAx.Areas.Axapta;

namespace WebAx.Server.Areas.Axapta
{
	[ApiController]
	public class AxaptaController : ControllerBase
	{
		/*
		[HttpGet(ApiAxapta.Base)]
		[Authorize]
		public async Task<IActionResult> Axapta()
		{
			return Ok(); // new List<Company>(new[] { new Company("OP", "OP Company test", false) });
		}
		*/

		/* [HttpGet]
		public async Task<ActionResult> GetDepartments()
		{
			try { return Ok(await departmentRepository.GetDepartments()); }
			catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database"); }
		} */
	}
}
