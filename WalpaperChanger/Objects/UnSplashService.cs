using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WalpaperChanger.Enums;
using WalpaperChanger.Interfaces;
using WalpaperChanger.Interfaces.Objects;
using WalpaperChanger.JsonObjects;

namespace WalpaperChanger.Objects
{
    public class UnSplashService : IUnSplashService
    {
        private string _appSecret;

        public UnSplashService(string AppSecret)
        {
            _appSecret = AppSecret;
        }

        public IPicture GetRandomImage()
        {
            SetProtocol();

            var rc = CreateServiceClient();

            var rq = new RestRequest("photos/random", Method.GET);
            rq.AddQueryParameter("featured", "1");
            rq.AddQueryParameter("query", "architecture");
            rq.AddQueryParameter("client_id", _appSecret);


            var response = rc.Execute(rq);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"An error occured in unsplash service: {response.Content}");
            }

            var obj = JsonConvert.DeserializeObject<UnSplashResponse>(response.Content);

            return new UnSplashPicture(obj.links.download);
        }

        public IEnumerable<IPicture> GetRandomImages(int imageCount = 30, string query = null)
        {
            SetProtocol();

            var rc = CreateServiceClient();

            var rq = new RestRequest("photos/random", Method.GET);
            rq.AddQueryParameter("featured", "1");
            if (query != null)
            {
                rq.AddQueryParameter("query", query);
            }
            rq.AddQueryParameter("client_id", _appSecret);
            rq.AddQueryParameter("count", imageCount.ToString());


            var response = rc.Execute(rq);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"An error occured in unsplash service: {response.Content}");
            }

            var obj = JsonConvert.DeserializeObject<IEnumerable<UnSplashResponse>>(response.Content);

            return obj.Select(x => new UnSplashPicture(x.links.download, x.links.html, x.user.name)).ToList();
        }

        public IEnumerable<IPicture> GetRandomImages(int imageCount = 30, Category? category = null)
        {
            SetProtocol();

            var rc = CreateServiceClient();

            var rq = new RestRequest("photos/random", Method.GET);
            rq.AddQueryParameter("featured", "1");
            if (category.HasValue)
            {
                rq.AddQueryParameter("query", category.Value.ToString());
            }
            rq.AddQueryParameter("client_id", _appSecret);
            rq.AddQueryParameter("count", imageCount.ToString());


            var response = rc.Execute(rq);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"An error occured in unsplash service: {response.Content}");
            }

            var obj = JsonConvert.DeserializeObject<IEnumerable<UnSplashResponse>>(response.Content);

            return obj.Select(x => new UnSplashPicture(x.links.download, x.links.html, x.user.name)).ToList();
        }

        private RestClient CreateServiceClient()
        {
            return new RestClient("https://api.unsplash.com/");
        }

        private void SetProtocol()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}
