using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cielo24;
using Cielo24.JSON;
using System.Collections.Generic;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class ActionsTest
    {
        private Actions actions = new Actions("http://sandbox-dev.cielo24.com");
        private String username = "testscript";
        private String password = "testscript2";
        private String newPassword = "testscript3";
        private Guid apiToken = Guid.Empty;
        private Guid jobId = Guid.Empty;
        private Guid taskId = Guid.Empty;
        private Guid secureKey = Guid.Empty;
        private Uri uri = new Uri("http://lesmoralesphotography.com/cielo24/test_suite/End_to_End_Regression/media_short_2327da9786d44a9a9c62242853593059.mp4");

        [TestMethod]
        public void testOptions()
        {

            CaptionOptions co = new CaptionOptions();
            co.CaptionBySentence = true;
            co.ForceCase = Case.upper;
            String[] array = new String[] { "build_url=true", "dfxp_header=header" };
            co.PopulateFromArray(array);
            Assert.AreEqual("build_url=true&caption_by_sentence=true&dfxp_header=header&force_case=upper", co.ToQuery().ToLower());
        }

        [TestMethod]
        public void testLoginAndLogout()
        {
            // Username, password, no headers
            apiToken = actions.Login(username, password, false);
            Assert.AreEqual(32, apiToken.ToString("N").Length);
            // Username, password, headers
            apiToken = actions.Login(username, password, true);
            Assert.AreEqual(32, apiToken.ToString("N").Length);
            // Username, secure key, no headers
            secureKey = actions.GenerateAPIKey(apiToken, username, false);
            apiToken = actions.Login(username, secureKey, false);
            Assert.AreEqual(32, apiToken.ToString("N").Length);
            // Username, secure key, headers
            apiToken = actions.Login(username, secureKey, true);
            Assert.AreEqual(32, apiToken.ToString("N").Length);
            // Logout
            actions.Logout(apiToken);

            apiToken = Guid.Empty;
        }

        [TestMethod]
        public void testGenerateAndRemoveApiKey()
        {
            secureKey = actions.GenerateAPIKey(apiToken, username, false);
            Assert.AreEqual(32, secureKey.ToString("N").Length);
            secureKey = actions.GenerateAPIKey(apiToken, username, true);
            Assert.AreEqual(32, secureKey.ToString("N").Length);
            actions.RemoveAPIKey(apiToken, secureKey);
        }

        [TestMethod]
        public void testUpdatePassword()
        {

            actions.UpdatePassword(apiToken, newPassword);
            actions.UpdatePassword(apiToken, password);

        }

        [TestMethod]
        public void testCreateJob()
        {
            CreateJobResult result = actions.CreateJob(apiToken, "testname", "en");
            Assert.AreEqual(32, result.JobId.ToString("N").Length);
            Assert.AreEqual(32, result.TaskId.ToString("N").Length);
        }

        [TestMethod]
        public void testAuthorizeJob()
        {
            actions.AuthorizeJob(apiToken, jobId);

            jobId = Guid.Empty;
        }

        [TestMethod]
        public void testDeleteJob()
        {

            taskId = actions.DeleteJob(apiToken, jobId);
            Assert.AreEqual(32, taskId.ToString("N").Length);

            jobId = Guid.Empty;
        }

        [TestMethod]
        public void testGets()
        {

            Console.WriteLine("testing getJobInfo()");
            JobInfo info = actions.GetJobInfo(apiToken, jobId);
            Console.WriteLine("testing getJobList()");
            JobList list = actions.GetJobList(apiToken);
            Console.WriteLine("testing getElementList()");
            ElementList list2 = actions.GetElementList(apiToken, jobId);
            Console.WriteLine("testing getListOfElementLists()");
            List<ElementListVersion> list3 = actions.GetListOfElementLists(apiToken, jobId);
            Console.WriteLine("testing getMedia()");
            Uri uri = actions.GetMedia(apiToken, jobId);

        }

        [TestMethod]
        public void testGetText()
        {
            actions.GetCaption(apiToken, jobId, CaptionFormat.SRT);
            actions.GetTranscript(apiToken, jobId);

        }

        [TestMethod]
        public void testPerformTranscription()
        {
            actions.AddMediaToJob(apiToken, jobId, uri);
            taskId = actions.PerformTranscription(apiToken, jobId, Fidelity.STANDARD, Priority.STANDARD);
            Assert.AreEqual(32, taskId.ToString("N").Length);

        }

        [TestMethod]
        public void testAddData()
        {
            string smallFile = "C:\\Users\\Evgeny\\Videos\\small.mp4";
            string bigFile = "C:\\Users\\Evgeny\\Videos\\The_Hobbit_480p.mov";
            FileStream fs = new FileStream(smallFile, FileMode.Open);
            taskId = actions.AddMediaToJob(apiToken, jobId, uri);
            Assert.AreEqual(32, taskId.ToString("N").Length);
            jobId = actions.CreateJob(apiToken).JobId;
            taskId = actions.AddEmbeddedMediaToJob(apiToken, jobId, uri);
            Assert.AreEqual(32, taskId.ToString("N").Length);
            jobId = actions.CreateJob(apiToken).JobId;
            taskId = actions.AddMediaToJob(apiToken, jobId, fs);
            Assert.AreEqual(32, taskId.ToString("N").Length);
        }

        [TestInitialize]
        public void Initialize()
        {
            Console.WriteLine("TestMethodInit");
            if (this.apiToken.Equals(Guid.Empty))
            {
                this.apiToken = actions.Login(username, password, true);
            }
            if (this.jobId.Equals(Guid.Empty))
            {
                this.jobId = actions.CreateJob(apiToken).JobId;
            }
        }
    }
}