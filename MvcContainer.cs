using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarketOnce.Web.Common
{
    public class MvcContainer
    {
        private Controller _controller;
        private ViewPage _viewPage;

        public class WebFormsController : Controller { }

        public HtmlHelper Html { get; private set; }
        public UrlHelper Url { get; private set; }
        public dynamic ViewBag { get; private set; }

        [Obsolete("Please use an HttpContextWrapper or HttpContextBase to construct the MvcContainer")]
        public MvcContainer(HttpContext context) : this(new HttpContextWrapper(context))  { }

        
        public MvcContainer(HttpContextBase context)
        {
            this._controller = new WebFormsController();
            this._viewPage = new ViewPage();
            InitializeHtmlHelper(context);
        }

        private void InitializeHtmlHelper(HttpContextBase context)
        {
            var controllerContext = new ControllerContext(context, new RouteData(), _controller);
            var viewContext = new ViewContext(controllerContext, new WebFormView(controllerContext, "Views"), new ViewDataDictionary(), new TempDataDictionary(), TextWriter.Null);
            Html = new HtmlHelper(viewContext, _viewPage);
            Url = new UrlHelper(new RequestContext(context, RouteTable.Routes.GetRouteData(context) ?? new RouteData()));
            ViewBag = viewContext.ViewBag;
        }

        public void Dispose()
        {
            _controller.Dispose();
            _viewPage.Dispose();
        }
    }
}