// Pretzel.Categories plugin
using System.Collections.Generic;
using Pretzel.Logic.Templating.Context;
using System.Linq;

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