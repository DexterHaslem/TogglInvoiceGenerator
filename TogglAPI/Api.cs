using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TogglAPI.Models;

namespace TogglAPI
{
    public class Api
    {
        private readonly string _userAgent = "dexter@haslemtech.com";
        private readonly string _apiRootURL = "https://www.toggl.com/api/v8/";
        private readonly string _reportsDetailsURL = "https://toggl.com/reports/api/v2/details";
        public string ApiKey { get; }

        public Api(string apiKey)
        {
            ApiKey = apiKey;
        }

        private WebClient AuthedWebClient()
        {
            var wc = new WebClient();
            var authChunk = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ApiKey}:api_token"));
            wc.Headers[HttpRequestHeader.Authorization] = $"Basic {authChunk}";// authStr;

            return wc;
        }

        private T GetResponse<T>(string url)
        {
            var wc = AuthedWebClient();
            var respJson = wc.DownloadString(url);
            var deserialized = JsonConvert.DeserializeObject<T>(respJson);
            return deserialized;
        }

        public Project[] GetProjects(long workspaceId)
        {
            var url = _apiRootURL + $"workspaces/{workspaceId}/projects";
            return GetResponse<Project[]>(url);
        }

        public Workspace[] GetWorkspaces()
        {
            return GetResponse<Workspace[]>(_apiRootURL + "workspaces");
        }

        public void GetMonthsDetailedReport(int monthNo, params long[] projectIds)
        {

        }
    }
}
