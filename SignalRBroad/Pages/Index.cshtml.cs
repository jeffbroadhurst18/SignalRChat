﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SignalRBroad.Pages
{
	public class IndexModel : PageModel
	{
		JeffBackgroundService _service;

		//public IndexModel(JeffBackgroundService service)
		//{
		//	_service = service;
		//}


		public void OnGet()
		{

		}
	}
}
