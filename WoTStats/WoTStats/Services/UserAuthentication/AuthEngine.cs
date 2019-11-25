using System.Net.Security;
using System.Threading.Tasks;
using WoTStats.Models.DatabaseModels;
using WoTStats.Models.RestModels.WoT.PlayerBasicInfo;
using WoTStats.Services.RestServices.WoT;
using Xamarin.Forms;

namespace WoTStats.Services.UserAuthentication
{
    public class AuthEngine
    {
        private PlayerBasicInfoRestService apiService;

        public AuthEngine()
        {
            apiService = new PlayerBasicInfoRestService();
        }

        public async Task<bool> Authenticate(string nickname, WoTServer wotServer)
        {
            PlayerBasicInfo playerBasicInfo = await apiService.GetPlayerBasicInfoAsync(nickname, wotServer);

            if (playerBasicInfo.Meta.Count > 0)
            {
                User user = new User
                {
                    Nickname = playerBasicInfo.Datas[0].Nickname,
                    AccountId = playerBasicInfo.Datas[0].AccountId,
                    WoTServer = wotServer
                };

                var allUsers = await App.Database.GetUsersAsync();
                if (allUsers.Count > 0)
                {
                    var userToBeDeleted = allUsers[0];
                    await App.Database.DeleteUserAsync(userToBeDeleted);
                }

                await App.Database.InsertUserAsync(user);

                return true; // new user inserted
            }
            return false;
        }
    }
}