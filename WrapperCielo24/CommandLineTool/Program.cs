using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrapperCielo24;
using CommandLine;
using CommandLine.Text;
using WrapperCielo24.JSON;
using System.Net;

namespace CommandLineTool
{
    class Program
    {
        static Options options = new Options();
        static Parser optionParser = new Parser();
        static Actions actions = new Actions();

        static void Main(string[] args)
        {
            options = new Options();
            if (optionParser.ParseArguments(args, options)) // Parsing successful
            {
                if (options.Help != null) // Specific help requested
                {
                    options.PrintActionHelp(options.Help);
                    return;
                }
                else if (options.ActionName == null) // No action was entered
                {
                    options.PrintDefaultUsage();
                    return;
                }

                if (options.ActionName.Equals("login") || options.ActionName.Equals("logout"))
                {
                    CallAction(options.ActionName);
                }
                else
                {
                    TryLogin();
                    CallAction(options.ActionName);
                }
            }
            else
            {
                options.PrintDefaultUsage();
                return;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        public static void CallAction(string actionName)
        {
            switch (actionName)
            {
                // ACCESS CONTROL //
                case "login":
                    if (options.ApiSecureKey.Equals(Guid.Empty)) { // Use password and username
                        TryAction(delegate() { return actions.Login(options.Username, options.Password, options.HeaderLogin); });
                    }
                    else { // Use secure key
                        TryAction(delegate() { return actions.Login(options.Username, options.ApiSecureKey, options.HeaderLogin); });
                    }
                    break;
                case "logout":
                    TryAction(delegate() { actions.Logout(options.ApiToken); return "Logged out successfully"; });
                    break;
                case "generate_api_key":
                    TryAction(delegate() { return actions.GenerateAPIKey(options.ApiToken, options.Username, options.ForceNew); });
                    break;
                case "remove_api_key":
                    TryAction(delegate() { actions.RemoveAPIKey(options.ApiToken, options.ApiSecureKey); return "API Key removed successfully"; });
                    break;
                case "update_password":
                    TryAction(delegate() { actions.UpdatePassword(options.ApiToken, options.Password); return "Password Updated Successfulyy"; });
                    break;
                // JOB CONTROL //
                case "create":
                    TryAction(delegate() { return actions.CreateJob(options.ApiToken, options.JobName, options.SourceLanguage)[0]; });
                    break;
                case "authorize":
                    TryAction(delegate() { actions.AuthorizeJob(options.ApiToken, options.JobId); return "Job Authorized Succesfully"; });
                    break;
                case "delete":
                    TryAction(delegate() { return actions.DeleteJob(options.ApiToken, options.JobId); });
                    break;
                case "job_info":
                    TryAction(delegate() { return actions.GetJobInfo(options.ApiToken, options.JobId); });
                    break;
                case "list":
                    TryAction(delegate() { return actions.GetJobList(options.ApiToken); });
                    break;
                case "add_media_to_job":
                    if (options.MediaUrl != null) { // Media Url
                        TryAction(delegate() { return actions.AddMediaToJob(options.ApiToken, options.JobId, options.MediaUrl); });
                    }
                    else { // Media File
                        TryAction(delegate() { return actions.AddMediaToJob(options.ApiToken, options.JobId, options.MediaFile); });
                    }
                    break;
                case "add_embedded_media_to_job":
                    TryAction(delegate() { return actions.AddEmbeddedMediaToJob(options.ApiToken, options.JobId, options.MediaUrl); });
                    break;
                case "get_media":
                    TryAction(delegate() { return actions.GetMedia(options.ApiToken, options.JobId); });
                    break;
                case "get_transcript":
                    TryAction(delegate() { return actions.GetTranscript(options.ApiToken, options.JobId, null); });
                    break;
                case "get_caption":
                    TryAction(delegate() { return actions.GetCaption(options.ApiToken, options.JobId, options.CaptionFormat, null); });
                    break;
                case "get_elementlist":
                    TryAction(delegate() { return actions.GetElementList(options.ApiToken, options.JobId); });
                    break;
                case "list_elementlists":
                    TryAction(delegate() { return actions.GetListOfElementLists(options.ApiToken, options.JobId); });
                    break;
                default:
                    options.PrintDefaultUsage();
                    break;
            }
        }

        private static void TryAction(Func<object> action)
        {
            try
            {
                string output = action.Invoke().ToString();
                Console.WriteLine(output);
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is TimeoutException || e is WebException || e is EnumWebException) // Known exceptions
                {
                    Console.WriteLine(e.Message);
                }
                options.PrintActionHelp(options.ActionName);
            }
        }

        private static void TryLogin()
        {
            if (options.ApiToken.Equals(Guid.Empty)) // Need to obtain api token
            {
                options.ApiToken = actions.Login(options.Username, options.Password);
            }
        }
    }
}