using Microsoft.VisualStudio.TestTools.UnitTesting;
using TogglAPI;
using System;
using System.IO;

namespace TogglAPITests
{
    [TestClass()]
    public class ApiTests
    {
        private Api _api;

        [TestInitialize]
        public void Setup()
        {
            _api = new Api(new FileInfo("apikey.txt"));
        }

        [TestMethod]
        public void GetWorkspacesTest()
        {
            var workspaces = _api.GetWorkspaces();
            Assert.IsNotNull(workspaces);
            Assert.IsTrue(workspaces.Length > 0);
        }

        [TestMethod]
        public void GetDetailReportTest()
        {
            var workspaces = _api.GetWorkspaces();
            long workspaceId = workspaces[0].Id;
            
            var projects = _api.GetProjects(workspaces[0].Id);
            long projectId = projects[0].Id;
            var detailedReport = _api.GetMonthsDetailedReport(new DateTime(2019, 12, 1), workspaceId, new []{projectId});
            Assert.IsTrue(detailedReport.Data.Length > 0);
        }
    }
}