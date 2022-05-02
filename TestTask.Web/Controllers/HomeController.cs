using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.DAL.Models;
using TestTask.BLL.Services;

namespace TestTask.Web.Controllers
{
    public class HomeController : Controller
    {
        GeneralServices _generalservice = new GeneralServices();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTable()
        {
            return PartialView();
        }

        public IActionResult _GetTable()
        {
            return PartialView("Views/PartialViews/_PartialTable.cshtml");
        }

        public IActionResult _GetNewObject()
        {
            return PartialView("Views/PartialViews/_PartialObject.cshtml");
        }


        [HttpPost]
        public JsonResult SaveAllObjects([FromBody] List<GeneralTableModel> model)
        {
            return Json(_generalservice.InsertGeneral(model));
        }

        [HttpPost]
        public JsonResult SelectGeneralTable([FromBody] NavigateModel model)
        {
            return Json(_generalservice.SelectGeneral(model.topmin, model.topmax));
        }

        [HttpPost]
        public JsonResult GetGeneralCount()
        {
            return Json(_generalservice.GeneralCnt());
        }
    }
}
