using LinkedIN.Application.Model;
using LinkedIN.Model.OAuth;
using LinkedIN.Model.People;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace LinkedIN.Application.Controllers
{
	public class HomeController : Controller
	{
        private const string consumerKey = "6e1g30dpzrp9";
        private const string consumerSecret = "OvzgUSvvbF6qeLXv";
		public ActionResult Index()
		{
			var accessToken = (AccessToken) Session["accessToken"];
			if (accessToken != null)
			{
				// create the client
				var client = new LinkedINRestClient(consumerKey, consumerSecret, accessToken);
                
              
				// retrieve the profile
				try
				{
					//var profile = client.RetrieveCurrentMemberProfile(ProfileField.All);


                    var all = "id,first-name,last-name,maiden-name,formatted-name,phonetic-first-name,phonetic-last-name,";
                    all += "formatted-phonetic-name,headline,location:(name,country:(code)),industry,distance,relation-to-viewer:(distance),";
                    all += "last-modified-timestamp,current-share,network,connections,num-connections,num-connections-capped,summary,";
                    all += "specialties,proposal-comments,associations,honors,interests,positions,publications,patents,languages,skills,";
                    all += "certifications,educations,courses,volunteer,three-current-positions,three-past-positions,num-recommenders,";
                    all += "recommendations-received,phone-numbers,im-accounts,twitter-accounts,primary-twitter-account,bound-account-types,";
                    all += "mfeed-rss-url,following,job-bookmarks,group-memberships,suggestions,date-of-birth,main-address,member-url-resources,";
                    all += "picture-url,public-profile-url,related-profile-views";
                    var fields = new[] { new ProfileField(all) };
                    var recent_updates = client.RetrieveCurrentMemberUpdates("STAT");
                    
                    var l_profile = client.RetrieveCurrentMemberProfile(fields);


                    //var idonly = "headline,first-name,last-name";
                    //var id_field = new[] { new ProfileField(idonly) };
                    //                    var profile_Connections = client.RetrieveCurrentMemberConnections(null);
                    //                  l_profile.NumberOfConnections = l_profile.Connections.Total;
                    //var ns = client.RetrieveCurrentMemberNetworkStats(fields);
                    Profile profile = new Profile(l_profile);
                    profile.processPercentages();
                    var ns = l_profile.NetworkStats;
                    profile.firstDegreeConnections = ns.properties[0].Value;
                    profile.secondDegreeConnections = ns.properties[1].Value;
                    profile.likes = new int[100];
                    profile.comments = new int[100];
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;

                    var current_week = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                    for (int i = 0; i < recent_updates.Count; i++)
                    {
                        var update = recent_updates[i];
                        //configure week
                         
                        DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        double MaxUnixSeconds = (DateTime.MaxValue - UnixEpoch).TotalSeconds;
                        double unixTimeStamp = update.TimeStamp;

                        DateTime dd = unixTimeStamp > MaxUnixSeconds
                           ? UnixEpoch.AddMilliseconds(unixTimeStamp).ToLocalTime()
                           : UnixEpoch.AddSeconds(unixTimeStamp).ToLocalTime();


                        var update_week = cal.GetWeekOfYear(dd, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                        
                        long ticks = DateTime.Now.Ticks/TimeSpan.TicksPerSecond - update.TimeStamp;
                        long weekLength = 1000 * 60 * 60 * 24 * 7;
                        long reminder;
                        int bweek = (int)Math.DivRem(ticks , weekLength, out reminder);
                      

                        int week = current_week - update_week;
                        if (week >= 12)
                        {
                            continue;
                        }
                        if (update.IsLikeable)
                        {
                        }
                        profile.likes[week] += update.NumLikes;
                        if (update.IsCommentable)
                        {
                            profile.comments[week] += update.UpdateComments.Total;
                        }                        
                    }
                      
					// return the view
					return View("Profile", profile);
				}
				catch (LinkedINUnauthorizedException)
				{
					// clear the access token
					Session.Remove("accessToken");

					// return the unauthorized view
					return View("Login");
				}
			}

			// return the unauthorized view
			return View("Login");
		}
		public ActionResult Logout()
		{
			// delete the access token from the session
			Session.Remove("accessToken");

			// redirect to home/index
			return new RedirectResult(Url.RouteUrl("Default", new {Action = "Index"}, Request.Url.Scheme));
		}

		public ActionResult Login()
		{
			// define the callback
			var callbackUri = new Uri(Url.RouteUrl("Default", new {Action = "Callback"}, Request.Url.Scheme));

			// create the client
			var client = new LinkedINRestClient(consumerKey, consumerSecret);

			// Phase 1: acquire request token
			RequestToken requestToken;
			var redirectUri = client.RequestAuthorizationToken(callbackUri, out requestToken);

			// store the request token in the session for later use
			Session["requestToken"] = requestToken;

			// send the redirect
			return new RedirectResult(redirectUri.ToString());
		}
		public ActionResult Callback()
		{
			// get the request uri
			var requestUri = Request.Url;

			// get the request token from the session
			var requestToken = (RequestToken) Session["requestToken"];
			Session.Remove("requestToken");

			// create the client
			var client = new LinkedINRestClient(consumerKey, consumerSecret);

			// Fase 2: acquire access token
			var accessToken = client.ExchangeCodeForAccessToken(requestUri, requestToken);

			// store the access token in the session
			Session["accessToken"] = accessToken;

			// redirect to home/index
            return new RedirectResult(Url.RouteUrl("Default", new { Action = "Index" }, Request.Url.Scheme));
		}
	}
}