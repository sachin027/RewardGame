using RewardGame.APIs.JWTAuthentication;
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
        SachinKanzariya_452Entities _DBContext = new SachinKanzariya_452Entities();

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

        [JwtAuthentication]
        [Route("api/LoginAPI/LoginUser")]
        [HttpPost]
        [System.Web.Http.AllowAnonymous]
        public LoginModel LoginUser(LoginModel loginModel)
        {
            try
            {
                //UserModel userTable = loginInterface.LoginUser(loginModel);
                //return userTable;

                var email = loginModel.EmailId;
                var password = loginModel.Password;
                var keepLogin = true;
                bool keepLoginSession;

                keepLoginSession = keepLogin == true;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Please enter valid username and password");

                    return null;
                }
                var usr = _DBContext.UserTable.Where(u => u.EmailId == loginModel.EmailId && u.Password == loginModel.Password).FirstOrDefault();

                UserTable appUserInfo = _DBContext.UserTable.Where(u => u.EmailId == loginModel.EmailId && u.Password == loginModel.Password
                ).FirstOrDefault();


                // Jwt Authentication code

                if (appUserInfo != null)
                {
                    string encryptedPwd = password;

                    var userPassword = appUserInfo.Password;
                    var username = appUserInfo.EmailId;
                    if (encryptedPwd.Equals(userPassword) && username.Equals(email))
                    {
                        var role = appUserInfo.Username;
                        var jwtToken = Authentication.GenerateJWTAuthetication(email, role);
                        var validUserName = Authentication.ValidateToken(jwtToken);

                        if (string.IsNullOrEmpty(validUserName))
                        {
                            ModelState.AddModelError("", "Unauthorized login attempt ");

                            return null;
                        }

                        var cookie = new HttpCookie("jwt", jwtToken)
                        {
                            HttpOnly = true,
                            //Expires = DateTime.UtcNow.AddMinutes(1)
                            // Secure = true, // Uncomment this line if your application is running over HTTPS
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie);
                        loginModel.Token = jwtToken;
                        return loginModel;

                        //Session["UserID"] = appUserInfo.id.ToString();
                        //Session["UserName"] = appUserInfo.name.ToString();
                    }
                }


                return loginModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}