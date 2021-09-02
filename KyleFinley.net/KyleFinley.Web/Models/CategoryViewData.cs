using _928.Web.Mvc;
using KyleFinley.Models;
using System.Collections.Generic;

namespace KyleFinley.Web.Models
{
    public class CategoryViewData : ViewData<Category>
    {
        public IList<ArticleSummary> ArticleSummaries { get; set; }
    }
}