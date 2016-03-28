using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Contacts.Models;

namespace contacts.Controllers
{
    public class ContactListController : Controller
    {
		private ContactDBContext db = new ContactDBContext();
		// GET: Contacts
		/*public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }*/
		public JsonResult updateList()
		{
			return Json(db.Contacts.ToList(), JsonRequestBehavior.AllowGet);
		}
		public ActionResult Index(string searchString)
		{
			var contacts = from c in db.Contacts
						   select c;
			if (!String.IsNullOrEmpty(searchString))
			{
				contacts = contacts.Where(s => s.Name.Contains(searchString));
			}
			return View(contacts);
		}
		public ActionResult New()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult New([Bind(Include = "Name,PhoneNum,Email")] Contact contact)
		{
			if(ModelState.IsValid)
			{
				db.Entry(contact).State = EntityState.Added;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}
		public ActionResult Edit(int? id)
		{
			if (id == null )
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Contact contact = db.Contacts.Find(id);
			if(contact == null)
			{
				return HttpNotFound();
			}
			return View(contact);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Name,PhoneNum,Email")] Contact contact)
		{
			if (ModelState.IsValid)
			{
				db.Entry(contact).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(contact);
		}
		public ActionResult Contact()
		{
			return View();
		}
		public ActionResult Delete(int? id)
		{
			if(id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Contact contact = db.Contacts.Find(id);
			if(contact == null)
			{
				return HttpNotFound();
			}
			return View(contact);
		}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			Contact contact = db.Contacts.Find(id);
			db.Contacts.Remove(contact);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}