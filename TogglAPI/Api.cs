using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TogglAPI.Models;
using static System.String;

namespace TogglAPI
{
    public class Api
    {
        private readonly string _userAgent = "dexter@haslemtech.com";
        // O is iso 8601 which is what toggl wants but it includes time.. 
        private readonly string _monthFormat = "yyyy-MM-dd";
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

        public DetailReportResponse GetMonthsDetailedReport(DateTime month, long workspaceId, params long[] projectIds)
        {
            var projectIdsStr = Join(",", projectIds);
            var roundingStr = "on"; // TODO: make configurable
            var since = FormatMonthBegin(month);
            var until = FormatMonthEnd(month);
            var url =
                $"{_reportsDetailsURL}?workspace_id={workspaceId}&project_ids={projectIdsStr}&rounding={roundingStr}&since={since}&until={until}&user_agent={_userAgent}&page=1";
            return GetResponse<DetailReportResponse>(url);
        }

        private string FormatMonthEnd(DateTime month)
        {
            var lastDay = DateTime.DaysInMonth(month.Year, month.Month);
            var endOfMonth = new DateTime(month.Year, month.Month, lastDay);
            return endOfMonth.ToString(_monthFormat);
        }

        private string FormatMonthBegin(DateTime month)
        {
            var beginningMonth = new DateTime(month.Year, month.Month, 1);
            return beginningMonth.ToString(_monthFormat);
        }
    }
}
