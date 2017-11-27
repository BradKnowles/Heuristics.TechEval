using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System.Net;
using Heuristics.TechEval.Web.Extensions;
using System.Data.Entity.Core;
using System;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

		private readonly DataContext _context;

		public MembersController() {
			_context = new DataContext();
		}

		public ActionResult List() {
			var allMembers = _context.Members.ToList();

			return View(allMembers);
		}

		[HttpPost]
		public ActionResult New(NewMember data) {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var modelErrors = ModelState.AllErrors();
                return Json(modelErrors);
            }

			var newMember = new Member {
				Name = data.Name,
				Email = data.Email
			};

			try
			{
				_context.Members.Add(newMember);
				_context.SaveChanges();
			}
			catch (UpdateException ex)
			{
				Exception currentException = ex;
				while (currentException != null)
				{
					ModelState.AddModelError("EF", currentException.Message);
					currentException = currentException.InnerException;
				}
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var modelErrors = ModelState.AllErrors();
				return Json(modelErrors);
			}

			return Json(JsonConvert.SerializeObject(newMember));
		}

		[HttpPost]
		public ActionResult Edit(EditMember data)
		{
			if (!ModelState.IsValid)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				var modelErrors = ModelState.AllErrors();
				return Json(modelErrors);
			}

			var member = _context.Members.FirstOrDefault(x => x.Id == data.Id);
			if (member == null)
				return new EmptyResult();

			member.Name = data.Name;
			member.Email = data.Email;

			try
			{
				_context.SaveChanges();
			}
			catch (UpdateException ex)
			{
				Exception currentException = ex;
				while (currentException != null)
				{
					ModelState.AddModelError("EF", currentException.Message);
					currentException = currentException.InnerException;
				}
				Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				
				var modelErrors = ModelState.AllErrors();
				return Json(modelErrors);
			}

			return Json(JsonConvert.SerializeObject(data));
		}
	}
}