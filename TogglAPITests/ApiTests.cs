using Microsoft.VisualStudio.TestTools.UnitTesting;
using TogglAPI;
using System;

namespace TogglAPITests
{
    [TestClass()]
    public class ApiTests
    {
        private Api api;

        [TestInitialize]
        public void Setup()
        {
            api = new Api("2930ab95e37f0dd05b2d2b447da929a2");
        }


        [TestMethod]
        public void GetWorkspacesTest()
        {
            var workspaces = api.GetWorkspaces();
            Assert.IsNotNull(workspaces);
            Assert.IsTrue(workspaces.Length > 0);
        }

        [TestMethod]
        public void GetDetailReportTest()
        {
            var workspaces = api.GetWorkspaces();
            long workspaceId = workspaces[0].Id;
            
            var projects = api.GetProjects(workspaces[0].Id);
            long projectId = projects[0].Id;
            var detailedReport = api.GetMonthsDetailedReport(new DateTime(2019, 12, 1), workspaceId, projectId);
            Assert.IsTrue(detailedReport.Data.Length > 0);
        }
    }
}