// Pretzel.Categories plugin
using System.Collections.Generic;
using System.IO;
using NDesk.Options;
using Pretzel.Logic.Extensibility;
using Pretzel.Logic.Extensibility.Extensions;
using Pretzel.Logic.Extensions;
using Pretzel.Logic.Templating.Context;

namespace Pretzel.Categories
{
    public class TagFolderGenerator : IBeforeProcessingTransform, IHaveCommandLineArgs
    {
        private const string CategoriesFolder = "tag";
        private readonly ICollection<string> categories = new List<string>();
        private bool stopTagGeneration;

        public string[] GetArguments(string command)
        {
            return command == "taste" || command == "bake" ? new[] { "-ntag" } : new string[0];
        }

        public void Transform(SiteContext siteContext)
        {
            var layout = "layout";

            if (this.stopTagGeneration)
            {
                return;
            }

            if (siteContext.Config.ContainsKey("tag_pages_layout"))
            {
                layout = siteContext.Config["tag_pages_layout"].ToString();
            }

            foreach (var item in siteContext.Tags)
            {
                var p = new Page
                {
                    Content = $"---\r\n layout: {layout} \r\n tag: {item.Name} \r\n---\r\n",
                    File = Path.Combine(siteContext.SourceFolder, CategoriesFolder, SlugifyFilter.Slugify(item.Name), "index.html"),
                    Filepath = Path.Combine(siteContext.OutputFolder, CategoriesFolder, SlugifyFilter.Slugify(item.Name), "index.html"),
                    OutputFile = Path.Combine(siteContext.OutputFolder, CategoriesFolder, SlugifyFilter.Slugify(item.Name), "index.html"),
                    Bag = $"---\r\n layout: {layout} \r\n tag: {item.Name} \r\n---\r\n".YamlHeader()
                };

                p.Url = new LinkHelper().EvaluateLink(siteContext, p);

                siteContext.Pages.Add(p);
            }
        }

        public void UpdateOptions(OptionSet options)
        {
            options.Add("ntag", "Disable the tag folder generation", v => this.stopTagGeneration = v != null);
        }
    }
}