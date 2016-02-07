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
    internal class TagFolderGeneratorTests
    {
        private const string Folder = @"C:\website";
        private const string OutputFolder = @"C:\website\_site";

        [Test]
        public void Transform_LayoutIsSpecified_PageHasExpectedContentAndBag()
        {
            var mock = new Mock<IConfiguration>();
            mock.Setup(config => config.ContainsKey("tag_pages_layout")).Returns(true);
            mock.Setup(config => config["tag_pages_layout"]).Returns("some_layout");

            var context = CreateContext(new[] { "C#" });
            context.Config = mock.Object;

            new TagsFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].Content, Is.EqualTo($"---\r\n layout: some_layout \r\n tag: C# \r\n---\r\n"));
            Assert.That(context.Pages[0].Bag, Is.EqualTo($"---\r\n layout: some_layout \r\n tag: C# \r\n---\r\n".YamlHeader()));
        }

        [Test]
        public void Transform_NoLayoutIsSpecified_PageHasExpectedContentAndBag()
        {
            var context = CreateContext(new[] { "C#" });

            new TagsFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].Content, Is.EqualTo($"---\r\n layout: layout \r\n tag: C# \r\n---\r\n"));
            Assert.That(context.Pages[0].Bag, Is.EqualTo($"---\r\n layout: layout \r\n tag: C# \r\n---\r\n".YamlHeader()));
        }

        private static SiteContext CreateContext(string[] tagList)
        {
            return new SiteContext
            {
                SourceFolder = Folder,
                OutputFolder = OutputFolder,
                Tags = tagList.Select(t => new Tag { Name = t }),
                Pages = new List<Page>()
            };
        }

        [Test]
        public void Transform_SwitchtIsPassedToCommandLine_NoPageIsGenerated()
        {
            var context = CreateContext(new[] { "C#" });

            var tagFolderGenerator = new TagsFolderGenerator();

            var options = new OptionSet();
            tagFolderGenerator.UpdateOptions(options);
            options.Parse(new[] { "-ntag" });

            tagFolderGenerator.Transform(context);

            Assert.That(context.Pages.Count, Is.EqualTo(0));
        }

        [Test]
        public void Transform_TagIsPresent_PageHasExpectedProperties()
        {
            var context = CreateContext(new[] { "C#" });

            new TagsFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].File, Is.EqualTo(Path.Combine(context.SourceFolder, "tag", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[0].Filepath, Is.EqualTo(Path.Combine(context.OutputFolder, "tag", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[0].OutputFile, Is.EqualTo(Path.Combine(context.OutputFolder, "tag", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[0].Url, Is.EqualTo(@"/tag/csharp/index.html"));
        }

        [Test]
        public void Transform_TagsArePresent_OnePageForEachTagIsGenerated()
        {
            var context = CreateContext(new[] { "C#", "tag" });

            new TagsFolderGenerator().Transform(context);

            Assert.That(context.Pages[0].File, Is.EqualTo(Path.Combine(context.SourceFolder, "tag", SlugifyFilter.Slugify("C#"), "index.html")));
            Assert.That(context.Pages[1].File, Is.EqualTo(Path.Combine(context.SourceFolder, "tag", SlugifyFilter.Slugify("tag"), "index.html")));
        }
    }
}