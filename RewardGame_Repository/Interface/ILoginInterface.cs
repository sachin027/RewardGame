using RewardGame_Model.DBContext;
using RewardGame_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardGame_Repository.Interface
{
    public interface ILoginInterface
    {
        UserTable UserRegistration(UserModel _userModel);
    }
}
