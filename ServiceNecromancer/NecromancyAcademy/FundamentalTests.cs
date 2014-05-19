
using System.Collections.Generic;
using NUnit.Framework;

namespace NecromancyAcademy
{
    public class FundamentalTests
    {
        [Test]
        public void CreateCurrentServiceList()
        {
            List<string> currentServices = Enchiridion.Scrolls.GetServicesList();

            Assert.IsNotNull(currentServices);
        }

        [Test]
        public void CheckIfListContainsService()
        {
            bool containsService = Enchiridion.Scrolls.CheckIfServiceInList("TeamViewer9");

            Assert.IsTrue(containsService);
        }

        [Test]
        public void CheckIfServiceIsRunning()
        {
            string isrunning = Enchiridion.Scrolls.CheckServiceCondition("TeamViewer9");

            Assert.AreEqual("Running", isrunning);
        }

        [Test]
        public void StartService()
        {
            string hasStarted = Enchiridion.Scrolls.StartService("TeamViewer9");


            Assert.AreEqual("Running", hasStarted);

            Assert.AreEqual("Starting", hasStarted);
        }

        [Test]
        public void StopService()
        {
            string hasStopped = Enchiridion.Scrolls.StopService("TeamViewer9");

            Assert.AreEqual("Stopping", hasStopped);

            Assert.AreEqual("Stopped", hasStopped);
        }
    }
}
