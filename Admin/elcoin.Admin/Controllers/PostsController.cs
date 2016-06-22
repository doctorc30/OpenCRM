using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.Table;
using bbom.Admin.Core.ViewModels.Post;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;

namespace elcoin.Admin.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IRepository<Post> _postsRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<PostType> _postTypesRepository;

        public PostsController(IRepository<Post> postsRepository, IMapper mapper,
            IRepository<AspNetUser> usersRepository, IRepository<PostType> postTypesRepository)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _postTypesRepository = postTypesRepository;
        }

        // GET: Posts
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public ActionResult Create()
        {
            SetPostViewBag();
            return View();
        }

        [HttpPost]
        [OnlyAdminAuthorize]
        public async Task<ViewResult> Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _postsRepository.InsertAsync(new Post
                {
                    Text = model.Text,
                    Date = DateTime.Now,
                    Title = model.Title,
                    TypeId = model.TypeId,
                    UserId = User.GetUserId()
                });
                return View("Index");
            }
            SetPostViewBag();
            return View();
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public ActionResult Update(int id)
        {
            var post = _postsRepository.GetById(id);
            var model = new PostCreateViewModel
            {
                Name = post.Title,
                Text = post.Text,
                TypeId = post.TypeId,
                Title = post.Title
            };
            SetPostViewBag();
            return View("Create", model);
        }

        [HttpPost]
        [OnlyAdminAuthorize]
        public async Task<ViewResult> Update(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = _postsRepository.GetById(model.Id);
                await _postsRepository.DeleteAsync(post);
                await _postsRepository.InsertAsync(new Post
                {
                    Text = model.Text,
                    Date = DateTime.Now,
                    Title = model.Title,
                    TypeId = model.TypeId,
                    UserId = User.GetUserId()
                });
                return View("Index");
            }
            SetPostViewBag();
            return View("Create", model);
        }

        [HttpGet]
        public JsonResult GetPostsByType(string typeId)
        {
            if (typeId != null)
            {
                var posts = _postsRepository.GetAll().Where(post => post.TypeId.ToString() == typeId);
                var postsJson = _mapper.Map<List<PostJson>>(posts);
                postsJson.Reverse();
                return Json(postsJson, JsonRequestBehavior.AllowGet);
            }
            return Json(Alert.Error, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var tm = TableGeneratorsMenager.GetGenerator<Post>().GetAll();
            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private void SetPostViewBag()
        {
            ViewBag.Types =
                _postTypesRepository.GetAll()
                    .ToList()
                    .Select(pt => new SelectListItem { Value = pt.Id.ToString(), Text = pt.Name });
        }
    }
}