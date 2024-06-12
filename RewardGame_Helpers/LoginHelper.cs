using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Helpers
{
    public class LoginHelper
    {
        public static UserTable ConvertUserModelToContext(UserModel _userModel)
        {
            try
            {
                UserTable _UserContext = new UserTable();
                if (_userModel != null)
                {
                    _UserContext.UserID = _userModel.UserID;
                    _UserContext.Username = _userModel.Username;
                    _UserContext.EmailId = _userModel.EmailId;
                    _UserContext.Password = _userModel.Password;
                }
                return _UserContext;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        
        public static UserModel ConvertUserToUserModel(UserTable _user)
        {
            try
            {
                if (_user != null)
                {
                UserModel _userModel = new UserModel();
                    _userModel.UserID = _user.UserID;
                    _userModel.Username = _user.Username;
                    _userModel.EmailId = _user.EmailId;
                    _userModel.Password = _user.Password;
                return _userModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
