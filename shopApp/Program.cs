using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.RequestParams;

namespace shopApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string pathToMainPhoto = @"C:\Users\darthvasya\Pictures\ot\4.jpg";
            ulong appID = 6042686;                      // ID приложения
            string email = "@";         // email или телефон
            string pass = "";               // пароль для авторизации
            Settings scope = Settings.Photos;    // Приложение имеет доступ к друзьям
            scope = Settings.Groups;

            var vk = new VkApi();
            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = appID,
                Login = email,
                Password = pass,
                Settings = Settings.All
            });


            var getMarketAlbumUploadServer = vk.Photo.GetMarketAlbumUploadServer(groupId: 147414782);
            Console.WriteLine(vk.UserId.Value);

            var categories = vk.Markets.GetCategories(1000, 0);

            var get = vk.Markets.Get(ownerId: -147414782);



            // Получить адрес сервера для загрузки.
            var uploadServer = vk.Photo.GetMarketUploadServer(groupId: 147414782, mainPhoto: true);
            // Загрузить фотографию.
            var wc = new WebClient();
            var responseImg = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, pathToMainPhoto));
            // Сохранить загруженную фотографию
            var photo = vk.Photo.SaveMarketPhoto(147414782, responseImg);
            vk.Markets.Add(new MarketProductParams
            {
                OwnerId = -147414782,
                CategoryId = categories.Select( x => x.Id).FirstOrDefault().Value,
                MainPhotoId = photo.FirstOrDefault().Id.Value,
                Deleted = false,
                Name = "Ведьма",
                Description = "Описание ведьмы",
                Price = 117744,
            });

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
