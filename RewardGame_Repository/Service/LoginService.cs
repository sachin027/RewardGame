using RewardGame_Helpers;
using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using RewardGame_Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Repository.Service
{
    public class LoginService : ILoginInterface
    {
        SachinKanzariya_452Entities _DBContext = new SachinKanzariya_452Entities();
        public UserTable UserRegistration(UserModel _userModel)
        {
            try
            {

                bool IsEmailExist = _DBContext.UserTable.Any(x => x.EmailId == _userModel.EmailId);

                if (!IsEmailExist)
                {
                    UserTable _User = LoginHelper.ConvertUserModelToContext(_userModel);
                    _DBContext.UserTable.Add(_User);
                    _DBContext.SaveChanges();

                    if (_User != null)
                    {
                        AddAmountIntoWallet(_User.UserID);
                    }

                    return _User;
                }
                else
                {
                    return LoginHelper.ConvertUserModelToContext(_userModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddAmountIntoWallet(int userId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@userid" , userId)
                };
                List<WalletModel> wallet = _DBContext.Database.SqlQuery<WalletModel>("exec SP_AddAmountIntoWallet @userid", sqlParameters).ToList();
                if (wallet != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
