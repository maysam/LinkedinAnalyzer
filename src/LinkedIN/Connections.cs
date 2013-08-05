using System;
using System.Collections.Generic;
using LinkedIN.Model;
using LinkedIN.Model.People;
using RestSharp;

namespace LinkedIN
{
	/// <summary>
	/// Implements the LinkedIN connections API part for the LinkedIN rest client.
	/// </summary>
	/// <seealso href="https://developer.linkedin.com/documents/connections-api" />
	public partial class LinkedINRestClient
    {
        #region Retrieve Profile Methods
        /// <summary>
        /// Returns a list of <see cref="Connections"/> for the current member.
        /// </summary>
        /// <param name="fields">The fields which to select from the person. See LinkedIN for an list of fields.</param>
        /// <param name="start">Starting location within the result set for paginated returns. Ranges are specified with a starting index and a number of results (count) to return. The default value for this parameter is 0.</param>
        /// <param name="count">Ranges are specified with a starting index and a number of results to return. You may specify any number. Default and max page size is 500. Implement pagination to retrieve more than 500 connections.</param>
        /// <returns>Returns the <see cref="Connections"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="fields"/> is null.</exception>
        /// <exception cref="LinkedINHttpResponseException">Thrown when the an unexepcted response was returned from LinkedIN.</exception>
        /// <exception cref="LinkedINUnauthorizedException">Thrown when an request was made to an protected resource without the proper authorization.</exception>
        public Connections RetrieveCurrentMemberConnections(IEnumerable<ProfileField> fields, int start = 0, int count = 500)
        {
            // validate arguments
            //	if ( fields == null )
            //	throw new ArgumentNullException( "fields" );

            // retrieve the profile
            return RetrieveConnections("~", fields, start, count);
        }
        /// <summary>
        /// Returns a list of <see cref="Connections"/> for the member identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID of the person which to retrieve.</param>
        /// <param name="fields">The <see cref="ProfileField"/>s  which to load into the connections.</param>
        /// <param name="start">Starting location within the result set for paginated returns. Ranges are specified with a starting index and a number of results (count) to return. The default value for this parameter is 0.</param>
        /// <param name="count">Ranges are specified with a starting index and a number of results to return. You may specify any number. Default and max page size is 500. Implement pagination to retrieve more than 500 connections.</param>
        /// <returns>Returns the <see cref="Connections"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either <paramref name="id"/> or <paramref name="fields"/> is null.</exception>
        /// <exception cref="LinkedINHttpResponseException">Thrown when the an unexepcted response was returned from LinkedIN.</exception>
        /// <exception cref="LinkedINUnauthorizedException">Thrown when an request was made to an protected resource without the proper authorization.</exception>
        public Connections RetrieveMemberConnections(string id, IEnumerable<ProfileField> fields, int start = 0, int count = 500)
        {
            // validate arguments
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");
            //if ( fields == null )
            //		throw new ArgumentNullException( "fields" );

            // retrieve the profile
            return RetrieveConnections("id=" + id, fields, start, count);
        }
        /// <summary>
        /// Returns a list of <see cref="Connections"/> for the member identified by <paramref name="idPart"/>.
        /// </summary>
        /// <param name="idPart">The identifying part.</param>
        /// <param name="fields">The <see cref="ProfileField"/>s  which to load into the connections.</param>
        /// <param name="start">Starting location within the result set for paginated returns. Ranges are specified with a starting index and a number of results (count) to return. The default value for this parameter is 0.</param>
        /// <param name="count">Ranges are specified with a starting index and a number of results to return. You may specify any number. Default and max page size is 500. Implement pagination to retrieve more than 500 connections.</param>
        /// <returns>Returns the <see cref="Connections"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="idPart"/> or <paramref name="fields"/> is null.</exception>
        /// <exception cref="LinkedINHttpResponseException">Thrown when the an unexepcted response was returned from LinkedIN.</exception>
        /// <exception cref="LinkedINUnauthorizedException">Thrown when an request was made to an protected resource without the proper authorization.</exception>
        protected Connections RetrieveConnections(string idPart, IEnumerable<ProfileField> fields, int start, int count)
        {
            // validate arguments
            if (string.IsNullOrEmpty(idPart))
                throw new ArgumentNullException("idPart");
            //	if ( fields == null )
            //	throw new ArgumentNullException( "fields" );

            // create the request
            var request = new RestRequest("people/" + idPart + "/connections" + FieldSelector.ToFieldSelector(fields));

            // set the parameters
            request.AddParameter("start", start);
            request.AddParameter("count", count);

            // execute the request
            return ExecuteRequest<Connections>(request);
        }
        #endregion
        #region Retrieve Network Stats Methods

        /// <summary>
        /// Returns a list of something
        /// </summary>
        public NetworkStats RetrieveCurrentMemberNetworkStats(IEnumerable<ProfileField> fields, int start = 0, int count = 500)
        {
            // validate arguments
            //	if ( fields == null )
            //	throw new ArgumentNullException( "fields" );

            // retrieve the profile
            return RetrieveNetworkStats("~", fields, start, count);
        }
        /// <summary>
        /// Returns a list of something
        /// </summary>
        public NetworkStats RetrieveMemberNetworkStats(string id, IEnumerable<ProfileField> fields, int start = 0, int count = 500)
        {
            // validate arguments
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");
            //if ( fields == null )
            //		throw new ArgumentNullException( "fields" );

            // retrieve the profile
            return RetrieveNetworkStats("id=" + id, fields, start, count);
        }
        /// <summary>
        /// Returns a list of something
        /// </summary>
        protected NetworkStats RetrieveNetworkStats(string idPart, IEnumerable<ProfileField> fields, int start, int count)
        {
            // validate arguments
            if (string.IsNullOrEmpty(idPart))
                throw new ArgumentNullException("idPart");
            //	if ( fields == null )
            //	throw new ArgumentNullException( "fields" );

            // create the request
            var request = new RestRequest("people/" + idPart + "/network/network-stats");

            // execute the request
            return ExecuteRequest<NetworkStats>(request);
        }
        #endregion

        /// <summary>
        /// </summary>
        public List<Update> RetrieveCurrentMemberUpdates(string type)
        {
            var idPart = "~";
            return RetrieveUpdates(idPart, type);
        }

        public List<Update> RetrieveUpdatesById(string USERID, string type)
        {
            var idPart = "id=" + USERID;
            return RetrieveUpdates(idPart, type);
        }

        public List<Update> RetrieveUpdates(string idPart, string type)
        {

            // create the request
            var request = new RestRequest("people/" + idPart + "/network/updates");
            request.AddParameter("scope", "self");
            if (type != null)
                request.AddParameter("type", type);
            //request.AddParameter("start", "0");
            request.AddParameter("count", "250");
            //  request.AddParameter("after", "34234");
            // &after=987867465 12 weeks ago

            // execute the request
            return ExecuteRequest<List<Update>>(request);
        }

        /// <summary>
        /// getting the list of user group memberships
        /// </summary>
        public List<GroupMembership> RetrieveCurrentMemberGroups()
        {
            return RetrieveGroups("~");
        }

        /// <summary>
        /// getting the list of user group memberships
        /// </summary>
        public List<GroupMembership> RetrieveGroupsById(string id)
        {
            return RetrieveGroups("id=" + id);
        }
        
        /// <summary>
        /// getting the list of user group memberships
        /// </summary>
        public List<GroupMembership> RetrieveGroups(string idPart)
        {
            var request = new RestRequest("people/" + idPart + "/group-memberships:(group:(id,name,counts-by-category),membership-state,show-group-logo-in-profile,allow-messages-from-members,email-digest-frequency,email-announcements-from-managers,email-for-every-new-post)");
        
            request.AddParameter("membership-state", "member");
            request.AddParameter("start", "0");
            request.AddParameter("count", "250");
            // execute the request
            return ExecuteRequest<List<GroupMembership>>(request);
        }

        /// <summary>
        /// </summary>
        public List<Post> RetrieveCurrentMemberGroupPosts(string group)
        {
            return RetrieveGroupPosts("~", group);
        }

        public List<Post> RetrieveGroupPostsByID(string USERID, string group)
        {
            return RetrieveGroupPosts("id=" + USERID, group);
        }

        public List<Post> RetrieveGroupPosts(string idPart, string group)
        {
            var request = new RestRequest("people/" + idPart + "/group-memberships/" + group + "/posts:(creator,title,summary,creation-timestamp,likes,comments,site-group-post-url)");
            request.AddParameter("role", "creator");
            //     request.AddParameter("category", "discussion");
            request.AddParameter("start", "0");
            request.AddParameter("count", "250");
            // execute the request
            return ExecuteRequest<List<Post>>(request);
        }

        /// <summary>
        /// </summary>
        public List<Post> RetrieveCurrentMemberGroupComments(string group)
        {
            var idPart = "~";
            return RetrieveGroupPostComments(idPart, group);
        }

        public List<Post> RetrieveGroupPostCommentsByID(string USERID, string group)
        {
            return RetrieveGroupPostComments("id=" + USERID, group);
        }

        public List<Post> RetrieveGroupPostComments(string idPart, string group)
        {
            var request = new RestRequest("people/" + idPart + "/group-memberships/" + group + "/posts:(creator,title,summary,creation-timestamp,likes,comments,site-group-post-url)");
            request.AddParameter("role", "commenter");
            //            request.AddParameter("category", "discussion");
            request.AddParameter("start", "0");
            request.AddParameter("count", "250");
            // execute the request
            return ExecuteRequest<List<Post>>(request);
        }

    }
}