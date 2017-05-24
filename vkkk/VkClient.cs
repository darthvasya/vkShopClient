using System;
using System.Collections.Generic;
using Citrina;
using Citrina.StandardApi.Models;

namespace vkkk
{
    public class VkClient
    {
        private readonly UserAccessToken utoken;
        private readonly CitrinaClient client;
        public VkClient()
        {
            utoken = new UserAccessToken("73364910a57d813fd86be4c4836ff008d1ae245f18b5013d4b7ff", 3600, 1234567, 7654321);
            client = new CitrinaClient();
            var token = new GroupAccessToken("ed68226b75f7707143580d284c40910bb794cfd2f1d750efb3ad", 123123123, 7654321);


        }

        public IEnumerable<int> GetFriends()
        {
            var call = client.Friends.Get(new FriendsGetRequest() { AccessToken = utoken });

            return call.Result.Response.Items;
        }
    }
}
