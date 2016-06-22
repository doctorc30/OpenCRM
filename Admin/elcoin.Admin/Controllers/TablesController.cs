using System.Web.Mvc;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Table;
using bbom.Admin.Core.ViewModels.Tables;
using bbom.Data.ContentModel;

namespace elcoin.Admin.Controllers
{
    public class TablesController : Controller
    {
        [HttpGet]
        public JsonResult GetTableModel(string table)
        {
            var tm = TableGeneratorsMenager.GetGenerator<Post>().GetTableModel();
            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public virtual ActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        [OnlyAdminAuthorize]
        public virtual ActionResult Create(CreateViewModel model)
        {
            //await _postsRepository.InsertAsync(new Post
            //{
            //    Date = DateTime.Now,
            //    TypeId = int.Parse(postJson.typeId),
            //    Text = postJson.text,
            //    Title = postJson.title,
            //    UserId = User.GetUserId()
            //});
            return View("Index");
        }

    }
}