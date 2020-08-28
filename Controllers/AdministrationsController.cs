using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Tracing.Models;
using Fuela.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Covid19Tracing.Controllers
{
    public class AdministrationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AdministrationsController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        
        public Boolean CustomeSQL(String Sql)
        {
            using (SqlConnection connection = new SqlConnection(
                       Sql))
            {
                SqlCommand command = new SqlCommand(Sql, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;

                }
                command.Connection.Close();
            }
        }
        // GET: Administrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administration.ToListAsync());
        }
        public IActionResult Log_in()
        {
            return View();
        } 
        
        public IActionResult Log_out()
        {
            ViewData.Clear();
            //ViewBag.Clear();
            HttpContext.Session.SetString("Pass", "5"); 
            return RedirectToAction("Log_in", "Administrations");
        }

        public IActionResult search( int id)
        {
            var PatientId = id;

            var result = _context.Patients.Where(i => i.ID == PatientId);
            if (result == null)
            {
                //ViewBag.Log_in = "0";
                //return RedirectToAction(nameof(Index));
                //HttpContext.Session.SetString("Log_in", "0");
                ViewBag.Error = "Error! this patient does not exists";
                return RedirectToAction(nameof(Dashboard));


            }
            else
            {



                ViewBag.Error = null;
                var result1 = _context.Covid_status.Where(i => i.Patients_id == id);
                if (result1 == null)
                {
                    return RedirectToAction("Create", "Covid_status", new { @p_id = id });

                }
                else
                {
                    return RedirectToAction("Edit", "Covid_status", new { @id = id });
                }
               
               


                //RedirectToAction("Create", "Administrations");


            }

         //return   RedirectToAction("Create", "Covid_status", new { @niID = id });




        }
        [HttpPost]
        public void Search(Patients patient)
        {
            //var PatientId = patient.ID;

            //var result = _context.Patients.Where(i => i.ID == PatientId);
            //if (result == null)
            //{
            //    //ViewBag.Log_in = "0";
            //    //return RedirectToAction(nameof(Index));
            //    //HttpContext.Session.SetString("Log_in", "0");
            //    ViewBag.Error = "Error! this patient does not exists";
            //    RedirectToAction("Dashboard", "Administrations");

            //}
            //else
            //{

            //HttpContext.Session.SetString("Pass", "1");
            //ViewData["Log_in"] = "3";
            //HttpContext.Session.SetString("Log_in", "0");

            //return RedirectToAction(nameof(Dashboard));
            //ViewBag.Error = null;
            //RedirectToAction("Create", "Covid_status", new { @niID = patient.ID });
            RedirectToAction("Dashboard", "Administrations");

        }
        //RedirectToAction("Dashboard", "Administrations");

        //return View();
    

        public void SendMessage(String number, String message)
        {
            //InitTwilio();

            //TwilioClient.Init(Session["TwilioSid"].ToString(), Session["Auth"].ToString());

            //var message1 = MessageResource.Create(
            //    body: message,
            //    from: new Twilio.Types.PhoneNumber(Session["TwilioPhone"].ToString()),
            //    to: new Twilio.Types.PhoneNumber(number)
            //);

            //Console.WriteLine(message1.Sid);



        }

        public IActionResult Dashboard(int id,Administration ad)
        {

            if (HttpContext.Session.GetString("Pass") == "1")
            {
                
                ////var stafff = id;
                if (id == null)
                {
                    var stafff = id;
                    var result = _context.Administration.Where(i => i.staff_no == stafff).FirstOrDefault();
                    if (result == null)
                    {

                    }
                    else
                    {
                        ViewData["identity"] = result.Full_names;
                        ViewData["Roles"] = result.Role;
                    }
                }
                else
                {
                    var stafff = int.Parse(HttpContext.Session.GetString("Staff_no"));
                    var result = _context.Administration.Where(i => i.staff_no == stafff).FirstOrDefault();
                    if (result == null)
                    {

                    }
                    else
                    {
                        ViewData["identity"] = result.Full_names;
                        ViewData["Roles"] = result.Role;
                    }
                }
                //var password = ad.Password;
                


            }
            else
            {
                return RedirectToAction(nameof(Log_in));
            }
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Log_in(Administration model)
        {
            var stafff = model.staff_no;
            var password = model.Password;
            var result = _context.Administration.Where(i => i.staff_no == stafff && i.Password == password).FirstOrDefault();
            if (result == null)
            {
                //ViewBag.Log_in = "0";
                //return RedirectToAction(nameof(Index));
                //HttpContext.Session.SetString("Log_in", "0");
                ViewData["Log_in"] = "0";
            }
            else {

                HttpContext.Session.SetString("Pass", "1");
                //HttpContext.Session.SetString("Role", "1");
                ViewData["Log_in"] = "3";
                //HttpContext.Session.SetString("Role", model.Role.ToString());
                HttpContext.Session.SetString("Staff_no", result.staff_no.ToString());

                return RedirectToAction("Dashboard", "Administrations", new { @id=result.staff_no});


            }
            return View();
        }
        // GET: Administrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.staff_no == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // GET: Administrations/Create
        public IActionResult Create(Departments depart)
        {
            if (HttpContext.Session.GetString("Role") == "2")
            {
                string selectedValue = depart.Department;
                List<Departments> DepartList = new List<Models.Departments>();
                DepartList = (from product in _context.Departments select product).ToList();
                //idList.Insert(0, new Rental_Owners { National_id=0,Full_names})
                DepartList.Insert(0, new Departments { ID = 0, Department = "--Select Department--" });
                ViewBag.IDlist = DepartList;
                ViewBag.Role = "1";
                return View();


            }
            else
            {
                var result = _context.Administration.FirstOrDefault();

                if (result == null)
                {
                    string selectedValue = depart.Department;
                    List<Departments> DepartList = new List<Models.Departments>();
                    DepartList = (from product in _context.Departments select product).ToList();
                    //idList.Insert(0, new Rental_Owners { National_id=0,Full_names})
                    DepartList.Insert(0, new Departments { ID = 0, Department = "--Select Department--" });
                    ViewBag.IDlist = DepartList;
                    ViewBag.Role = "1";

                    return View();

                }
                else
                {


                    if (ViewData["pass"] == null)
                    {
                        ViewBag.Log_in = "1";
                        return RedirectToAction(nameof(Log_in));


                    }
                    else
                    {
                        return RedirectToAction(nameof(Dashboard));

                    }





                }
            }
        }
        // POST: Administrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("staff_no,Full_names,Phone_number,Email,Password,Role,Department")] Administration administration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administration);
                await _context.SaveChangesAsync();
                ViewData["Log_in"] = "1";


                return RedirectToAction(nameof(Log_in));

            }
            return View(administration);
        }

        // GET: Administrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration.FindAsync(id);
            if (administration == null)
            {
                return NotFound();
            }
            return View(administration);
        }

        // POST: Administrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("staff_no,Full_names,Phone_number,Email,Password,Role,Department")] Administration administration)
        {
            if (id != administration.staff_no)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrationExists(administration.staff_no))
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
            return View(administration);
        }

        // GET: Administrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.staff_no == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // POST: Administrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administration = await _context.Administration.FindAsync(id);
            _context.Administration.Remove(administration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrationExists(int id)
        {
            return _context.Administration.Any(e => e.staff_no == id);
        }
    }
}
