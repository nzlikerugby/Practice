using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Model;
using CMS.Repository;
using CMS.Infrastructure;

namespace CMS.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Services services;
        public CustomersController(Services services)
        {
            this.services = services;
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(services.GetCustomers());
        }

        // GET: Customers/Details/5
        public IActionResult Details(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var customer = services.GetCustomerDetails(Id);
            return View(customer);
        }

        public IActionResult Notes(string Id)
        {
            var notes = services.GetNotes().Where(p => p.CustomerId == Guid.Parse(Id));
            ViewBag.CustomerId = Id;
            return View(notes);
        }

        public IActionResult CreateNote(string Id)
        {
            ViewBag.CustomerId = Id;
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNote(string Id, string content)
        {
            var note = new Note();
            if (ModelState.IsValid)
            {
                note.Id = Guid.NewGuid();
                note.CustomerId = Guid.Parse(Id);
                note.Content = content;
                if (!string.IsNullOrEmpty(content))
                {
                    services.AddNote(note);
                }

                return View(note);
            }
            return RedirectToAction("CreateNote");
        }

        public IActionResult EditNote(string Id)
        {
            var note = services.GetNote(Guid.Parse(Id));
            ViewBag.CustomerId = note.CustomerId.ToString();
            return View(note);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNote(string Id, string content)
        {
            var note = services.GetNote(Guid.Parse(Id));
            ViewBag.CustomerId = note.CustomerId.ToString();
            if (ModelState.IsValid)
            {
                note.Content = content;
                services.UpdateNote(note);
                return View(note);
            }
            return RedirectToAction("EditNote");
        }

        public IActionResult DetailsNote(Guid Id)
        {
            var note = services.GetNote(Id);
            ViewBag.CustomerId = note.CustomerId.ToString();
            return View(note);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Status,CreateDate,Contact,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                services.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var customer = services.GetCustomerDetails(Id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Edit(Guid id, [Bind("Id,Status")] Customer customer)
        public IActionResult Edit(Guid Id, [Bind("Id,Status")]Customer customer, Contact contact, Address address)
        {
            if (Id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                services.EditCustomer(Id, customer, contact, address);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = services.GetCustomer(id);
            return View(customer);
        }


        public IActionResult Search(string keyword)
        {
            var customers = services.Search(keyword);
            return View("Index", customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var customer = services.GetCustomer(id);
            services.DeleteCustomer(customer);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(Guid id)
        {
            var customer = services.GetCustomer(id);
            return customer == null ? false : true;
        }
    }
}
