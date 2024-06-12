using Newtonsoft.Json;
using RewardGame.APIs.JWTAuthentication;
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
using System.Timers;
using System.IdentityModel.Tokens.Jwt;

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
        public async Task<ActionResult> Login(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string content = JsonConvert.SerializeObject(login);

                    string response = await WebHelpers.HttpRequestResponce("api/LoginAPI/LoginUser", content);

                    LoginModel CheckAddUser = JsonConvert.DeserializeObject<LoginModel>(response);
                    var usr = _DBContext.UserTable.Where(u => u.EmailId == login.EmailId && u.Password == login.Password).FirstOrDefault();
                    if (CheckAddUser != null)
                    {
                        var cookie = new HttpCookie("jwt", CheckAddUser.Token)
                        {
                            HttpOnly = true,
                            Secure = true, // Assuming your application is running over HTTPS
                            Expires = DateTime.UtcNow.AddMinutes(1)
                        };

                        HttpContext.Response.Cookies.Add(cookie);
                        SessionHelper.Username = usr.Username;
                        SessionHelper.UserID = usr.UserID;
                        SessionHelper.Email = usr.EmailId;
                        Session["UserId"] = usr.UserID;
                        TempData["success"] = "Login Successfully";

                        return RedirectToAction("GameDashboard", "Game");
                    }
                    else
                    {
                        TempData["error"] = "Something went wrong!";
                        return View();
                    }
                }
                else
                {
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
                //if (Request.Cookies["jwt"] != null)
                //{
                //    var cookie = new HttpCookie("jwt")
                //    {
                //        Expires = DateTime.UtcNow.AddMinutes(-1),
                //        HttpOnly = true,
                //        Secure = true // If your site is running on HTTPS
                //    };
                //    HttpContext.Response.Cookies.Add(cookie);
                //}
                TempData["success"] = "Logout successfully.";
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool IsJwtValid()
        {
            var cookie = Request.Cookies["jwt"];
            if (cookie == null) return false;

            var token = cookie.Value;
            if (string.IsNullOrEmpty(token)) return false;

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null) return false;

                return jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}