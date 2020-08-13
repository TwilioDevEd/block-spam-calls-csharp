using System.IO;
using BlockSpamCalls.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twilio.AspNet.Common;

namespace BlockSpamCalls.Test
{
    [TestClass]
    [DeploymentItem("Fixtures")]
    public class VoiceControllerTest
    {
        [TestMethod]
        public void TestSuccessfulWithoutAddons()
        {
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), null);
            Assert.IsFalse(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestSuccessfulWithMarchex()
        {
            var addOns = File.ReadAllText("successful_marchex.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsFalse(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestBlockedWithMarchex()
        {
            var addOns = File.ReadAllText("spam_marchex.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsTrue(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestSuccessfulWithNomoRobo()
        {
            var addOns = File.ReadAllText("successful_nomorobo.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsFalse(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestBlockedWithNomoRobo()
        {
            var addOns = File.ReadAllText("spam_nomorobo.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsTrue(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestSuccessfulWithEkata()
        {
            var addOns = File.ReadAllText("successful_ekata.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsFalse(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestBlockedWithEkata()
        {
            var addOns = File.ReadAllText("spam_ekata.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsTrue(result.Data.ToString().Contains("<Reject"));
        }

        [TestMethod]
        public void TestSuccessfulWithNomoRoboApiFailure()
        {
            var addOns = File.ReadAllText("failed_nomorobo.json");
            var controller = new VoiceController();
            var result = controller.Index(new VoiceRequest(), addOns);
            Assert.IsFalse(result.Data.ToString().Contains("<Reject"));
        }
    }
}