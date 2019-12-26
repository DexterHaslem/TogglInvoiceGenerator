using Microsoft.VisualStudio.TestTools.UnitTesting;
using TogglAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}