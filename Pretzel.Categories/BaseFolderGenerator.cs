// Pretzel.Categories plugin
using System.IO;
using NDesk.Options;
using Pretzel.Logic.Extensibility;
using Pretzel.Logic.Extensibility.Extensions;
using Pretzel.Logic.Extensions;
using Pretzel.Logic.Templating.Context;
using System.Collections.Generic;

namespace Pretzel.Categories
{
    public abstract class BaseFolderGenerator : IBeforeProcessingTransform, IHaveCommandLineArgs
    {
        private readonly string folderName = string.Empty;
        private bool stopFolderGeneration;

        protected BaseFolderGenerator(string folderToGenerate)
        {
            this.folderName = folderToGenerate;
        }

        public string[] GetArguments(string command)
        {
            return command == "taste" || command == "bake" ? new[] { $"n{this.folderName}" } : new string[0];
        }

        public void Transform(SiteContext siteContext)
        {
            var layout = "layout";
            var layoutConfigKey = $"{this.folderName}_pages_layout";

            if (this.stopFolderGeneration)
            {
                return;
            }

            if (siteContext.Config.ContainsKey(layoutConfigKey))
            {
                layout = siteContext.Config[layoutConfigKey].ToString();
            }

            foreach (var name in this.GetNames(siteContext))
            {
                var p = new Page
                {
                    Content = $"---\r\n layout: {layout} \r\n {this.folderName}: {name} \r\n---\r\n",
                    File = Path.Combine(siteContext.SourceFolder, this.folderName, SlugifyFilter.Slugify(name), "index.html"),
                    Filepath = Path.Combine(siteContext.OutputFolder, this.folderName, SlugifyFilter.Slugify(name), "index.html"),
                    OutputFile = Path.Combine(siteContext.OutputFolder, this.folderName, SlugifyFilter.Slugify(name), "index.html"),
                    Bag = $"---\r\n layout: {layout} \r\n {this.folderName}: {name} \r\n---\r\n".YamlHeader()
                };

                p.Url = new LinkHelper().EvaluateLink(siteContext, p);

                siteContext.Pages.Add(p);
            }
        }

        public void UpdateOptions(OptionSet options)
        {
            options.Add($"n{this.folderName}", $"Disable the {this.folderName} folder generation", v => this.stopFolderGeneration = v != null);
        }

        protected abstract IEnumerable<string> GetNames(SiteContext siteContext);
    }
}