using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using RewardGame_Repository.Interface;
using RewardGame_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace RewardGame.APIs.Controllers
{
    public class LoginAPIController : ApiController
    {
        ILoginInterface loginInterface = new LoginService();
        // GET: LoginAPI
        [Route("api/LoginAPI/AddRegister")]
        [HttpPost]
        public UserTable AddRegister(UserModel registerModel)
        {
            try
            {
                UserTable _UserContext = loginInterface.UserRegistration(registerModel);
                return _UserContext;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}