// Pretzel.Categories plugin
using System.Collections.Generic;
using System.Linq;
using Pretzel.Logic.Templating.Context;

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