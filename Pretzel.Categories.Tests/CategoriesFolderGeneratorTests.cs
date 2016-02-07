using NUnit.Framework;
using Pretzel.Logic;
using Pretzel.Logic.Extensibility.Extensions;
using Pretzel.Logic.Extensions;
using Pretzel.Logic.Templating.Context;
using System.Collections.Generic;
using System.IO;
using Moq;
using NDesk.Options;
using System.Linq;

namespace Pretzel.Categories.Tests
{
    [TestFixture]
    internal class CategoriesFolderGeneratorTests
    {
        private const string Folder = @"C:\website";
        private const string OutputFolder = @"C:\website\_site";

        [Test]
        public void Transform_LayoutIsSpecified_PageHasExpectedContentAndBag()
        {
            var mock = new Mock<IConfiguration>();
            mock.Setup(config => config.ContainsKey("category_pages_layout")).Returns(true);
            mock.Setup(config => config["category_pages_layout"]).Returns("some_layout");

            var context = CreateContext(new[] { "C#" });
            context.Config = mock.Object;

            new CategoriesFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].Content, Is.EqualTo($"---\r\n layout: some_layout \r\n category: C# \r\n---\r\n"));
            Assert.That(context.Pages[0].Bag, Is.EqualTo($"---\r\n layout: some_layout \r\n category: C# \r\n---\r\n".YamlHeader()));
        }

        [Test]
        public void Transform_NoLayoutIsSpecified_PageHasExpectedContentAndBag()
        {
            var context = CreateContext(new[] { "C#" });

            new CategoriesFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].Content, Is.EqualTo($"---\r\n layout: layout \r\n category: C# \r\n---\r\n"));
            Assert.That(context.Pages[0].Bag, Is.EqualTo($"---\r\n layout: layout \r\n category: C# \r\n---\r\n".YamlHeader()));
        }

        private static SiteContext CreateContext(string[] categoryList)
        {
            return new SiteContext
            {
                SourceFolder = Folder,
                OutputFolder = OutputFolder,
                Categories = categoryList.Select(t => new Category { Name = t }),
                Pages = new List<Page>()
            };
        }

        [Test]
        public void Transform_SwitchtIsPassedToCommandLine_NoPageIsGenerated()
        {
            var context = CreateContext(new[] { "C#" });

            var categoryFolderGenerator = new CategoriesFolderGenerator();

            var options = new OptionSet();
            categoryFolderGenerator.UpdateOptions(options);
            options.Parse(new[] { "-ncategory" });

            categoryFolderGenerator.Transform(context);

            Assert.That(context.Pages.Count, Is.EqualTo(0));
        }

        [Test]
        public void Transform_CategoryIsPresent_PageHasExpectedProperties()
        {
            var context = CreateContext(new[] { "C#" });

            new CategoriesFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].File, Is.EqualTo(Path.Combine(context.SourceFolder, "category", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[0].Filepath, Is.EqualTo(Path.Combine(context.OutputFolder, "category", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[0].OutputFile, Is.EqualTo(Path.Combine(context.OutputFolder, "category", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[0].Url, Is.EqualTo(@"/category/csharp/index.html"));
        }

        [Test]
        public void Transform_CategoryArePresent_OnePageForEachTagIsGenerated()
        {
            var context = CreateContext(new[] { "C#", "category" });

            new CategoriesFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].File, Is.EqualTo(Path.Combine(context.SourceFolder, "category", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[1].File, Is.EqualTo(Path.Combine(context.SourceFolder, "category", SlugifyFilter.Slugify("category"), "index.html")));
        }
    }
}