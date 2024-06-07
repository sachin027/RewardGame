using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RewardGame.SessionHelper
{
    public class SessionHelper
    {
        public static int UserID
        {
            get
            {
                return HttpContext.Current.Session["UserID"] == null ? 0 : (int)HttpContext.Current.Session["UserID"];
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }
        public static string Email
        {
            get
            {
                return HttpContext.Current.Session["Email"] == null ? "" : (string)HttpContext.Current.Session["Email"];
            }
            set
            {
                HttpContext.Current.Session["Email"] = value;
            }
        }
        public static string Username
        {
            get
            {
                return HttpContext.Current.Session["Username"] == null ? "" : (string)HttpContext.Current.Session["Username"];
            }
            set
            {
                HttpContext.Current.Session["Username"] = value;
            }
        }
    }
}