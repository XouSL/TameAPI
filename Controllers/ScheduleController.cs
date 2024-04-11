using TameAPI.Context;
using TameAPI.Controllers.DTO;
using TameAPI.Replicates;
using Microsoft.AspNetCore.Mvc;

namespace TameAPI.Controllers
{
    public class ScheduleController:BaseController
    {
        public ScheduleController(ApplicationContext _appContext):base(_appContext) { }

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
            res.schedule = ApplicationContext.ScheduleManager.Schedules.Select(it => new ScheduleModel(it));
            return Send(true, res);
        }


        [HttpGet("[controller]/[action]")]
        public JsonResult Get(int id)
        {
            var res = GetCommon();
            res.schedules = new ScheduleModel(ApplicationContext.ScheduleManager.Schedules.FirstOrDefault(it => it.Id == id));
            return Send(true, res);
        }

        [HttpPost("[controller]/[action]")]
        public JsonResult Create([FromBody] ScheduleModel model)
        {
            Schedule schedule = ApplicationContext.ScheduleManager.Create(model);

            var res = GetCommon();
            res.schedules = new ScheduleModel(schedule);
            return Send(true, res);
        }

        [HttpPut("[controller]/[action]")]
        public JsonResult Update([FromBody] ScheduleModel model)
        {

            Schedule schedule = ApplicationContext.ScheduleManager.Update(model);

            var res = GetCommon();
            res.schedule = new ScheduleModel(schedule);
            return Send(true, res);
        }

        [HttpGet("[controller]/[action]")]
        public JsonResult GetUsers(int scheduleId)
        {

            Users[] users = ApplicationContext.ScheduleManager.GetUsers(scheduleId);

            var res = GetCommon();
            res.users = users.Select(it => new UsersModel(it));
            return Send(true, res);
        }
        [HttpPost("[controller]/[action]")]
        public JsonResult AttachUsers(int scheduleId, int userId)
        {

            Users[] users = ApplicationContext.ScheduleManager.AttachUser(scheduleId, userId);

            var res = GetCommon();
            res.users = users.Select(it => new UsersModel(it));
            return Send(true, res);
        }
        [HttpPost("[controller]/[action]")]
        public JsonResult DettachUsers(int scheduleId, int userId)
        {

            Users[] user = ApplicationContext.ScheduleManager.DettachUsers(scheduleId, userId);

            var res = GetCommon();
            res.users = user.Select(it => new UsersModel(it));
            return Send(true, res);
        }

        [HttpDelete("[controller]/[action]")]
        public JsonResult Delete(int id)
        {
            ApplicationContext.ScheduleManager.Delete(id);
            var res = GetCommon();
            res.schedules = ApplicationContext.ScheduleManager.Schedules.Select(it => new ScheduleModel(it));
            return Send(true, res);
        }
    }
}
