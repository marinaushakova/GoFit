using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using GoFit.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GoFit.Tests.MockContexts
{
    public class MockContext
    {
        public Mock<RequestContext> RoutingRequestContext { get; set; }
        public Mock<HttpContextBase> Http { get; private set; }
        public Mock<HttpServerUtilityBase> Server { get; private set; }
        public Mock<HttpResponseBase> Response { get; private set; }
        public Mock<HttpRequestBase> Request { get; private set; }
        public Mock<HttpSessionStateBase> Session { get; private set; }
        public Mock<ActionExecutingContext> ActionExecuting { get; private set; }
        public HttpCookieCollection Cookies { get; private set; }
        public Mock<masterEntities> db { get; private set; }

        /// <summary>
        /// Basic mock context constructor
        /// </summary>
        public MockContext()
        {
            RoutingRequestContext = new Mock<RequestContext>(MockBehavior.Loose);
            ActionExecuting = new Mock<ActionExecutingContext>(MockBehavior.Loose);
            Http = new Mock<HttpContextBase>(MockBehavior.Loose);
            Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            Response = new Mock<HttpResponseBase>(MockBehavior.Loose);
            Request = new Mock<HttpRequestBase>(MockBehavior.Loose);
            Session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
            Cookies = new HttpCookieCollection();

            RoutingRequestContext.SetupGet(c => c.HttpContext).Returns(Http.Object);
            ActionExecuting.SetupGet(c => c.HttpContext).Returns(Http.Object);
            Http.SetupGet(c => c.Request).Returns(Request.Object);
            Http.SetupGet(c => c.Response).Returns(Response.Object);
            Http.SetupGet(c => c.Server).Returns(Server.Object);
            Http.SetupGet(c => c.Session).Returns(Session.Object);
            Request.Setup(c => c.Cookies).Returns(Cookies);
            Response.Setup(c => c.Cookies).Returns(Cookies);
        }

        /// <summary>
        /// Authentication mock context constructor
        /// </summary>
        /// <param name="username">The username to be given to the mocked currently signed in user</param>
        public MockContext(string username)
            : this()
        {
            Http.Setup(c => c.User.Identity.Name).Returns(username);
            Http.Setup(c => c.User.Identity.IsAuthenticated).Returns(true);
        }

        /// <summary>
        /// Static helper for contructing the basic context
        /// </summary>
        /// <returns>The mocked ControllerContext</returns>
        public static ControllerContext BasicContext()
        {
            return new ControllerContext { HttpContext = new MockContext().Http.Object };
        }

        /// <summary>
        /// Static helper for constructing the authentication mock context
        /// </summary>
        /// <param name="username">The mocked current user's username</param>
        /// <returns>The mocked ControllerContext</returns>
        public static ControllerContext AuthenticationContext(string username)
        {
            return new ControllerContext { HttpContext = new MockContext(username).Http.Object };
        }
    }
}
