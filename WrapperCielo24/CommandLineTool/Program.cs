using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrapperCielo24;
using CommandLine;
using CommandLine.Text;
using WrapperCielo24.JSON;
using System.Net;
using System.Diagnostics;

namespace CommandLineTool
{
    class Program
    {
        static Options options = new Options();
        static Parser optionParser = new Parser();
        static Actions actions = new Actions();

        static void Main(string[] args)
        {
            //string[] argss = { "./prog.exe", "-a", "create", "-u", "testscript", "-p", "testscript2", "-m", "https://www.youtube.com/watch?v=n1JGzxvsRPg" };
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

                if (options.VerboseMode)
                {
                    Debug.Listeners.Add(new TextWriterTraceListener(System.Console.Out));
                }

                if (options.ActionName.Equals("login") || options.ActionName.Equals("logout"))
                {
                    CallAction(options.ActionName);
                }
                else
                {
                    if (TryLogin()) { CallAction(options.ActionName); }
                }
            }
            else
            {
                options.PrintDefaultUsage();
                return;
            }

            //Console.WriteLine("\nPress any key to exit...");
            //Console.ReadKey();
            return;
        }

        public static void CallAction(string actionName)
        {
            switch (actionName)
            {
                // ACCESS CONTROL //
                case "login":
                    Console.WriteLine("Logging in...");
                    if (options.ApiSecureKey.Equals(Guid.Empty)) { // Use password and username
                        TryAction(delegate() { return actions.Login(options.Username, options.Password, options.HeaderLogin); });
                    }
                    else { // Use secure key
                        TryAction(delegate() { return actions.Login(options.Username, options.ApiSecureKey, options.HeaderLogin); });
                    }
                    break;
                case "logout":
                    Console.WriteLine("Logging out...");
                    TryAction(delegate() { actions.Logout(options.ApiToken); return "Logged out successfully"; });
                    break;
                case "generate_api_key":
                    Console.WriteLine("Generating API key...");
                    TryAction(delegate() { return actions.GenerateAPIKey(options.ApiToken, options.Username, options.ForceNew); });
                    break;
                case "remove_api_key":
                    Console.WriteLine("Removing API key...");
                    TryAction(delegate() { actions.RemoveAPIKey(options.ApiToken, options.ApiSecureKey); return "API Key removed successfully"; });
                    break;
                case "update_password":
                    Console.WriteLine("Updating password...");
                    TryAction(delegate() { actions.UpdatePassword(options.ApiToken, options.Password); return "Password Updated Successfulyy"; });
                    break;
                // JOB CONTROL //
                case "create":
                    TryAction(delegate() {
                        Console.WriteLine("Creating job...");
                        Guid jobId = actions.CreateJob(options.ApiToken, options.JobName, options.SourceLanguage)[0];
                        Console.WriteLine("JobId: " + jobId.ToString());
                        Console.WriteLine("Adding media...");
                        if (options.MediaFile == null)
                        {
                            actions.AddMediaToJob(options.ApiToken, jobId, options.MediaUrl);
                        }
                        else
                        {
                            actions.AddMediaToJob(options.ApiToken, jobId, options.MediaFile);
                        }
                        Console.WriteLine("Performing transcrition...");
                        return actions.PerformTranscription(options.ApiToken, jobId, options.Fidelity, options.Priority, options.CallbackUrl, options.TurnaroundHours, options.TargetLanguage, new QueryOptions(options.JobConfig));
                    });
                    break;
                case "authorize":
                    Console.WriteLine("Authorizing job...");
                    TryAction(delegate() { actions.AuthorizeJob(options.ApiToken, options.JobId); return "Job Authorized Succesfully"; });
                    break;
                case "delete":
                    Console.WriteLine("Deleting job...");
                    TryAction(delegate() { return actions.DeleteJob(options.ApiToken, options.JobId); });
                    break;
                case "job_info":
                    Console.WriteLine("Getting job info...");
                    TryAction(delegate() { return actions.GetJobInfo(options.ApiToken, options.JobId); });
                    break;
                case "list":
                    Console.WriteLine("Listing jobs...");
                    TryAction(delegate() { return actions.GetJobList(options.ApiToken); });
                    break;
                case "add_media_to_job":
                    Console.WriteLine("Ading media to job...");
                    if (options.MediaUrl != null) { // Media Url
                        TryAction(delegate() { return actions.AddMediaToJob(options.ApiToken, options.JobId, options.MediaUrl); });
                    }
                    else { // Media File
                        TryAction(delegate() { return actions.AddMediaToJob(options.ApiToken, options.JobId, options.MediaFile); });
                    }
                    break;
                case "add_embedded_media_to_job":
                    Console.WriteLine("Adding embedded media to job...");
                    TryAction(delegate() { return actions.AddEmbeddedMediaToJob(options.ApiToken, options.JobId, options.MediaUrl); });
                    break;
                case "get_media":
                    Console.WriteLine("Getting media...");
                    TryAction(delegate() { return actions.GetMedia(options.ApiToken, options.JobId); });
                    break;
                case "get_transcript":
                    Console.WriteLine("Getting transcript...");
                    TryAction(delegate() { return actions.GetTranscript(options.ApiToken, options.JobId, new QueryOptions(options.CaptionOptions)); });
                    break;
                case "get_caption":
                    Console.WriteLine("Getting caption...");
                    TryAction(delegate() { return actions.GetCaption(options.ApiToken, options.JobId, options.CaptionFormat, new QueryOptions(options.CaptionOptions)); });
                    break;
                case "get_elementlist":
                    Console.WriteLine("Getting element list...");
                    TryAction(delegate() { return actions.GetElementList(options.ApiToken, options.JobId); });
                    break;
                case "list_elementlists":
                    Console.WriteLine("Listing element lists...");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n" + output);
                Console.ResetColor();
            }
            catch (Exception e)
            {
                //if (e is ArgumentException || e is TimeoutException || e is WebException || e is EnumWebException) // Known exceptions
                
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + e.Message);
                    Console.ResetColor();
                
                options.PrintActionHelp(options.ActionName);
            }
        }

        private static bool TryLogin()
        {
            if (options.ApiToken.Equals(Guid.Empty)) // Need to obtain api token
            {
                try
                {
                    options.ApiToken = actions.Login(options.Username, options.Password);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + e.Message);
                    Console.ResetColor();
                    options.PrintActionHelp(options.ActionName);
                    return false;
                }
            }
            return true;
        }
    }
}