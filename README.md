MVC Container for Web Forms
=================

Container to allow Web Forms pages to act like MVC Controllers and call into Controllers via route names

Suggested Usage:

	In Base Page:
	
		protected Lazy<MvcContainer> _mvc = new Lazy<MvcContainer>(() => new MvcContainer(HttpContext.Current));
		
		public MvcContainer Mvc
        {
            get { return _mvc.Value; }
        }
		
		public override void Dispose()
		{
			base.Dispose();

			if (_mvc.IsValueCreated)
			{
				_mvc.Value.Dispose();
			}
		}
	
	In .aspx page:
		
		<div id="mainContents">
			<%=Mvc.Html.Action("#Action#", "#Controller#", new { Area = "#Area#" }) %>
		</div>