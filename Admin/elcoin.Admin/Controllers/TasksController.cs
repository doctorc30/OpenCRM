using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.Tasks;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;

namespace elcoin.Admin.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IRepository<TaskToDo> _tasksRepository;
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<UserComplitedTask> _userComTaskRepository;

        public TasksController(IRepository<TaskToDo> tasksRepository, IRepository<AspNetUser> usersRepository,
            IRepository<UserComplitedTask> userComTaskRepository)
        {
            _tasksRepository = tasksRepository;
            _usersRepository = usersRepository;
            _userComTaskRepository = userComTaskRepository;
        }

        // GET: GetAll
        [HttpGet]
        public ActionResult GetAll()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var tasks = _tasksRepository.GetAll();
            var userTasks = user.TaskToDoes;
            var tasksJson = new List<TasksJson>();
            foreach (var task in tasks)
            {
                var taskJson = new TasksJson
                {
                    taskId = task.Id.ToString(),
                    name = task.Name,
                    state = userTasks.Contains(task)
                };
                tasksJson.Add(taskJson);
            }
            return Json(tasksJson, JsonRequestBehavior.AllowGet);
        }

        // POST: Select
        [HttpPost]
        public async Task<JsonResult> Select(int taskId, bool state)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var task = _tasksRepository.GetById(taskId);
            if (state)
            {
                await _userComTaskRepository.InsertAsync(new UserComplitedTask
                {
                    UserId = user.Id,
                    TaskId = task.Id,
                    TaskToDo = task
                });
            }
            else
            {
                var uct =
                    _userComTaskRepository.GetAll()
                        .FirstOrDefault(uctSer => uctSer.TaskId == taskId && uctSer.UserId == user.Id);
                if (uct!= null)
                    await _userComTaskRepository.DeleteAsync(uct);
            }
            await _userComTaskRepository.SaveChangesAsync();
            return Json(Alert.Success, JsonRequestBehavior.AllowGet);
        }
    }
}