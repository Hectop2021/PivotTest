using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PivotTest.Data;
using PivotTest.Models;

namespace PivotTest.Controllers
{
    public class ResponseReportPivotController : Controller
    {
        private readonly AppDbContext _context;

        public ResponseReportPivotController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ResponseReportPivots
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResponseReportPivot.ToListAsync());
        }

        // GET: ResponseReportPivots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResponseReportPivots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Venue,EventYear")] ResponseReportPivot responseReportPivot)
        {
            if (ModelState.IsValid)
            {
                responseReportPivot.Id = Guid.NewGuid();
                _context.Add(responseReportPivot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responseReportPivot);
        }

        public IActionResult PivotGrid()
        {
            ViewBag.ExportName = "VenuePivotGrid-" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx";
            IEnumerable<ResponseReportPivot> objResponseList = _context.ResponseReportPivot;
            var sortedResponseList = objResponseList.OrderBy(s => s.Venue).ThenBy(s => s.EventYear);
            return View(sortedResponseList);
        }

        [HttpPost]
        public ActionResult _ExcelExport(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        //public ActionResult ReadData()
        public ActionResult _ReadData([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<ResponseReportPivot> objResponseList = _context.ResponseReportPivot;
            var sortedResponseList = objResponseList.OrderBy(s => s.Venue).ThenBy(s => s.EventYear);
            return Json(sortedResponseList);
        }
    }
}
