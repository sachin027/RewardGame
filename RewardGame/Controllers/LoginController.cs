using Newtonsoft.Json;
using RewardGame.CustomFilter;
using RewardGame.SessionHelper;
using RewardGame.WebHelper;
using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using RewardGame_Repository.Interface;
using RewardGame_Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace Sachin_452.Controllers
{
    public class LoginController : Controller
    {
        SachinKanzariya_452Entities _DBContext = new SachinKanzariya_452Entities();
        ILoginInterface _LoginInterface = new LoginService();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            try
            {
               
                var validUser = _DBContext.UserTable.FirstOrDefault(x => x.EmailId == user.EmailId && x.Password == user.Password);

                if (validUser != null)

                {
                    SessionHelper.Username = validUser.Username;
                    SessionHelper.UserID = validUser.UserID;
                    SessionHelper.Email = validUser.EmailId;
                    Session["UserId"]= validUser.UserID;
                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("GameDashboard", "Game");
                }
                else
                {
                    TempData["error"] = "Something went wrong!";
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string content = JsonConvert.SerializeObject(userModel);

                    string response = await WebHelpers.HttpRequestResponce("api/LoginAPI/AddRegister", content);

                    UserModel CheckAddUser = JsonConvert.DeserializeObject<UserModel>(response);
                    if (CheckAddUser.UserID>0)
                    {

                        TempData["success"] = "Registration Successfully";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["email"] = "Email already exist!";
                        return View(userModel);
                    }

                }
                else
                {
                    TempData["error"] = "Something went wrong!";
                    return View(userModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                TempData["success"] = "Logout successfully.";
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}