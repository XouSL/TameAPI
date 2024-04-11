using TameAPI.Context;
using TameAPI.Controllers.DTO;
using TameAPI.Replicates;
using Microsoft.AspNetCore.Mvc;

namespace TameAPI.Controllers
{
    public class UsersController:BaseController
    {
        public UsersController(ApplicationContext _appContext):base(_appContext) { }


        [HttpGet("[controller]/[action]")]
        public JsonResult Init()
        {
            var res = GetCommon();
            return Send(true, res);
        }

        [HttpGet("[controller]/[action]")]
        public JsonResult GetAll()
        {
            var res = GetCommon();
            res.user = ApplicationContext.UsersManager.Users.Select(it => new UsersModel(it));
            return Send(true, res);
        }


        [HttpGet("[controller]/[action]")]
        public JsonResult Get(int id)
        {
            var res = GetCommon();
            res.users = new UsersModel (ApplicationContext.UsersManager.Users.FirstOrDefault(it => it.Id == id));
            return Send(true, res);
        }

        [HttpPost("[controller]/[action]")]
        public JsonResult Create([FromBody] UsersModel model)
        {
            Users schedule = ApplicationContext.UsersManager.Create(model);

            var res = GetCommon();
            res.users = new UsersModel(schedule);
            return Send(true, res);
        }

        [HttpPut("[controller]/[action]")]
        public JsonResult Update([FromBody] UsersModel model)
        {

            Users schedyle = ApplicationContext.UsersManager.Update(model);

            var res = GetCommon();
            res.Users = new UsersModel(schedyle);
            return Send(true, res);
        }

        [HttpDelete("[controller]/[action]")]
        public JsonResult Delete(int id)
        {
            ApplicationContext.UsersManager.Delete(id);
            var res = GetCommon();
            res.users = ApplicationContext.UsersManager.Users.Select(it => new UsersModel(it));
            return Send(true, res);
        }
    }
}
