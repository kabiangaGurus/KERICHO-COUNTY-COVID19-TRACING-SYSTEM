using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Tracing.Models;
using Fuela.DBContext;
using Twilio.Rest.Api.V2010.Account;
using Twilio;

namespace Covid19Tracing.Controllers
{
    public class Covid_statusController : Controller
    {
        private readonly ApplicationDBContext _context;

        public Covid_statusController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Covid_status
        public async Task<IActionResult> Index(int id)
        {
            return View(await _context.Covid_status.Where(i => i.Patients_id == id).ToListAsync());
        }

        // GET: Covid_status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid_status = await _context.Covid_status
                .FirstOrDefaultAsync(m => m.ID == id);
            if (covid_status == null)
            {
                return NotFound();
            }

            return View(covid_status);
        }

        // GET: Covid_status/Create
        public IActionResult Create(int p_id ,Covid_status Cs)
        {
            string selectedValue = Cs.Status;
            List<Status> DepartList = new List<Models.Status>();
            DepartList = (from product in _context.Status select product).ToList();
            //idList.Insert(0, new Rental_Owners { National_id=0,Full_names})
            DepartList.Insert(0, new Status { ID = 0, C_status = "--Select status--" });
            ViewBag.IDlist = DepartList;
            ViewBag.Role = "1";
            ViewData["p_id"] = p_id;

            return View();

            
        }

        // POST: Covid_status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Patients_id,Status,Date")] Covid_status covid_status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(covid_status);
                await _context.SaveChangesAsync();
                ViewData["p.id"] = covid_status.Patients_id;
                return RedirectToAction(nameof(Index));
            }
            return View(covid_status);

        }

        // GET: Covid_status/Edit/5
        
        public void SendMessage(String number, String message, String ssid,String Token)
        {
            //InitTwilio();

            TwilioClient.Init(ssid,Token);

            var message1 = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber("+12057820532"),
                to: new Twilio.Types.PhoneNumber(number)
            );

            Console.WriteLine(message1.Sid);



        }
        public async Task<IActionResult> Edit(int? id)
        {
            var covid_status = await _context.Covid_status
              .FirstOrDefaultAsync(m => m.ID == id);

            if (id == null)
            {

                return NotFound();

            }


            //var covid_status = await _context.Covid_status.FindAsync(id);
            if (covid_status == null)
            {
                return NotFound();
            }
            //string selectedValue = Cs.Status;
            List<Status> DepartList = new List<Models.Status>();
            DepartList = (from product in _context.Status select product).ToList();
            //idList.Insert(0, new Rental_Owners { National_id=0,Full_names})
            DepartList.Insert(0, new Status { ID = 0, C_status = "--Select status--" });
            ViewBag.IDlist = DepartList;
            ViewBag.Role = "1";
            return View(covid_status);
        }
        // POST: Covid_status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ID,Patients_id,Status,Date")] Covid_status covid_status)
        {
            if (id != covid_status.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(covid_status);
                    await _context.SaveChangesAsync();
                    var result = _context.Patients.Where(i =>i.ID == id).FirstOrDefault();
                    String status1 = status(int.Parse(covid_status.Status));
                    //var Phone = from p in _context.Patients.Local
                    //            where p.ID == id
                    //            select p.Phone_number;
                    //var query = from p in _context.Patients
                    //            where p.ID == id
                    //            select p;

                    // This will raise an exception if entity not found
                    // Use SingleOrDefault instead
                    //var professor = query.Single();







                    String phone = "+254713045562";
                    String message = "Greetings from Kericho County Hospital,You are informed the results for your covid 19 test is: " + status1+". Further information will be communicated to you.";
                    SendMessage(phone, message, "ACcf10cfe7c3984d1961e961c7f2531f72", "01e49c30ddec944c6f665a4d46e5b7b9");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Covid_statusExists(covid_status.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(covid_status);
        }

        public string status(int state)
        {
            if (state == 1)
            {
                return "Positive";

            }
            else if(state==2)
            {
                return "Negative";

            }
            else
            {
                return "Pending";

            }
        }
        // GET: Covid_status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var covid_status = await _context.Covid_status
                .FirstOrDefaultAsync(m => m.ID == id);
            if (covid_status == null)
            {
                return NotFound();
            }

            return View(covid_status);
        }

        // POST: Covid_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var covid_status = await _context.Covid_status.FindAsync(id);
            _context.Covid_status.Remove(covid_status);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Covid_statusExists(int id)
        {
            return _context.Covid_status.Any(e => e.ID == id);
        }
    }
}
