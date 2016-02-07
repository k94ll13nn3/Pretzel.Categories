// Pretzel.Categories plugin
using System.Collections.Generic;
using Pretzel.Logic.Templating.Context;
using System.Linq;

namespace Pretzel.Categories
{
    public class TagsFolderGenerator : BaseFolderGenerator
    {
        public TagsFolderGenerator()
            : base("tag")
        {
        }

        protected override IEnumerable<string> GetNames(SiteContext siteContext) => siteContext.Tags.Select(t => t.Name);
    }
}