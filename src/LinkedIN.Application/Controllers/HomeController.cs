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
                    var id_field = new[] { new ProfileField("id") };
                    var tiny_user_profile = client.RetrieveCurrentMemberProfile(id_field);
                    var USERID = tiny_user_profile.Id;

                    var all = "id,first-name,last-name,maiden-name,formatted-name,phonetic-first-name,phonetic-last-name,";
                    all += "formatted-phonetic-name,headline,location:(name,country:(code)),industry,distance,relation-to-viewer:(distance),";
                    all += "last-modified-timestamp,current-share,network,connections,num-connections,num-connections-capped,summary,";
                    all += "specialties,proposal-comments,associations,honors,interests,positions,publications,patents,languages,skills,";
                    all += "certifications,educations,courses,volunteer,three-current-positions,three-past-positions,num-recommenders,";
                    all += "recommendations-received,phone-numbers,im-accounts,twitter-accounts,primary-twitter-account,bound-account-types,";
                    all += "mfeed-rss-url,following,job-bookmarks,group-memberships,suggestions,date-of-birth,main-address,member-url-resources,";
                    all += "picture-url,public-profile-url,related-profile-views";
                    var fields = new[] { new ProfileField(all) };

                    //  USERID = "GP6NLJzhwV";  maysam
                    //  USERID = "DDlt4Qzq2Q";  martin

                    var user_profile = client.RetrieveProfileById(USERID, fields);
                    Profile profile = new Profile(user_profile);
                    var gms = client.RetrieveGroups(USERID);
                    foreach (GroupMembership gm in gms)
                    {
                        try
                        {
                            var group_posts = client.RetrieveGroupPostsByID(USERID, gm.Key);
                            foreach (var post in group_posts)
                            {
                                profile.add(
                                    new GroupPost(
                                        post.title,
                                        post.likes.Total,
                                        post.Comments.Total,
                                        post.SiteGroupPostUrl,
                                        post.CreationTimestamp)
                                    );
                            }
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("ERROR:");
                            System.Diagnostics.Debug.WriteLine(e);
                        }
                        try
                        {
                            var group_posts = client.RetrieveGroupPostCommentsByID(USERID, gm.Key);
                            foreach (var post in group_posts)
                            {
                                profile.add(
                                    new GroupPostComment(
                                        post.title,
                                        post.SiteGroupPostUrl,
                                        post.CreationTimestamp
                                        )
                                    );
                            }
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("ERROR:");
                            System.Diagnostics.Debug.WriteLine(e);
                        }
                    }
                    //  "VIRL" is something that I liked, clearly out of our scope here
                    var types = new[] { "SHAR", "PRFX" };
                    foreach (var type in types)
                    {
                        var recent_updates = client.RetrieveUpdatesById(USERID, type);
                        foreach (var update in recent_updates)
                        {

                            try
                            {
                                String content = type + " !!! " + update.ToString();
                                if (update.UpdateContent.Person.CurrentShare == null)
                                {
                                    content = type + " ??? " + update.ToString();
                                }
                                else
                                {
                                    content = update.UpdateContent.Person.CurrentShare.Comment;
                                    if (content == "" || content == null)
                                        content = update.UpdateContent.Person.CurrentShare.Content.title; // || update.UpdateContent.Person.CurrentShare.Comment;
                                }
                                var likes_count = update.NumLikes;
                                if (update.Likes != null)
                                    likes_count = update.Likes.Total;
                                var comments_count = 0;
                                if (update.UpdateComments != null)
                                    comments_count = update.UpdateComments.Total;

                                profile.add(
                                    new UserPost(
                                        content,
                                        likes_count,
                                        comments_count,
                                        update.UpdateUrl,
                                        update.TimeStamp
                                        )
                                );
                            }
                            catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine("ERROR: on line " + e.Source);
                                System.Diagnostics.Debug.WriteLine(e);
                            }
                        }
                    }
                    profile.processPercentages();
                    return View("Profile", profile);
                }
                catch (LinkedINUnauthorizedException)
                {
                    // clear the access token
                    Session.Remove("accessToken");

                    // return the unauthorized view
                    return View("Login");
                }
                catch (Exception e)
                {
                    return View("Error", e);
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
            Uri redirectUri;
            try
            {
                redirectUri = client.RequestAuthorizationToken(callbackUri, out requestToken);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
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