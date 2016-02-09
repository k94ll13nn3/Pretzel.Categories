// Pretzel.Categories plugin
using System.Collections.Generic;
using System.Linq;
using Pretzel.Logic.Templating.Context;

namespace Pretzel.Categories
{
    public class CategoriesFolderGenerator : BaseFolderGenerator
    {
        public CategoriesFolderGenerator()
            : base("category")
        {
        }

        protected override IEnumerable<string> GetNames(SiteContext siteContext) => siteContext.Categories.Select(t => t.Name);
    }
}