using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WoTStats.Models.DatabaseModels;

namespace WoTStats.Services.Rest
{
    abstract class BaseRestService
    {
        protected HttpClient client;

        private readonly string bareUrlFirstPart = "https://api.worldoftanks.";
        private readonly string bareUrlSecondPart = "/wot";
        protected string BareUrlSpecificPart;

        public BaseRestService()
        {
            client = new HttpClient();
        }

        protected abstract string GetFullUrl(string parameter, WoTServer server);

        protected string GetServerEndpoint(WoTServer server)
        {
            string urlServerRepresentation = null;

            switch (server)
            {
                case WoTServer.ru:
                    urlServerRepresentation = "ru";
                    break;
                case WoTServer.eu:
                    urlServerRepresentation = "eu";
                    break;
                case WoTServer.na:
                    urlServerRepresentation = "com";
                    break;
                case WoTServer.asia:
                    urlServerRepresentation = "asia";
                    break;
            }

            return $"{bareUrlFirstPart}{urlServerRepresentation}{bareUrlSecondPart}";
        }
    }
}
