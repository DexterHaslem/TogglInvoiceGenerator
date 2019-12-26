using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TogglInvoiceGenerator
{
    public class Workspace
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Rounding { get; set; }
        public int RoundingMinutes { get; set; }
    }


    public class Project
    {
        public long Id { get; set; }
        public long Wid { get; set; }
        public long Cid { get; set; }
        public string Name { get; set; }
        public bool Billable { get; set; }
        public bool Active { get; set; }
    }


    public class TogglAPI
    {
        private readonly string _apiRootURL = "https://www.toggl.com/api/v8/";
        private readonly string _reportsDetailsURL = "https://toggl.com/reports/api/v2/details";
        public string ApiKey { get; }

        public TogglAPI(string apiKey)
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

        public Project[] GetProjects(long workspaceId)
        {
            var wc = AuthedWebClient();
            var projectsJson = wc.DownloadString(_apiRootURL + $"workspaces/{workspaceId}/projects");
            var projects = JsonConvert.DeserializeObject<Project[]>(projectsJson);
            return projects;
        }

        public Workspace[] GetWorkspaces()
        {
            var wc = AuthedWebClient();
            var workspacesJson = wc.DownloadString(_apiRootURL + "workspaces");
            var workspaces = JsonConvert.DeserializeObject<Workspace[]>(workspacesJson);
            return workspaces;
        }
    }
}
