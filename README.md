LinqToHtml ReadMe
===
### Description

LinqToHtml is a simple utility that lets you use LINQ syntax to query HTML.

### Samples

    const string html = @"
    <html>
        <head>
            <TITLE>The Title</TITLE>
        </head>
        <body bgcolor='#ffffff'>
            <H1 id='hello' style='greeting' tabkey='1'>Hello</H1>
            <H2 id='world' style='greeting' tabkey='2'>World</H2>
        </body>
    </html>";

    var parsed = HTMLParser.Parse(html);

    parsed
        .Head
        .Content
        .ShouldBeEqualTo("The Title");

    parsed
        .ChildTags
        .Count()
        .ShouldBeEqualTo(2); // head, body

    parsed
        .DescendantTags
        .Count(x => x.Parent.TypeEqualsIgnoreCase("HTML"))
        .ShouldBeEqualTo(2); // head, body

    parsed
        .DescendantTags
        .OfType("title")
        .Any()
        .ShouldBeFalse();

    parsed
        .DescendantTags
        .OfType("title").IgnoreCase()
        .Any()
        .ShouldBeTrue();

    parsed
        .DescendantTags
        .OfType("title").IgnoreCase()
        .First()
        .Parent
        .TypeEqualsIgnoreCase("HEAD")
        .ShouldBeTrue();

    parsed
        .DescendantTags
        .WithAttributeNamed("style")
        .Count()
        .ShouldBeEqualTo(2); 

    parsed
        .DescendantTags
        .WithAttributeNamed("id")
        .HavingValue("world")
        .First()
        .Content
        .ShouldBeEqualTo("World");

mapping

    public class Text
    {
        public string Id { get; set; }
        public int TabKey { get; set; } 
    }
    
    var text = new Text();
    parsed
        .DescendantTags
        .OfType("H1")
        .First()
        .MapTo(text);

    text.Id.ShouldBeEqualTo("hello");
    text.TabKey.ShouldBeEqualTo(1);

note: the .Should... syntax comes from the [FluentAssert][FluentAssert] library.

### How To Build:

The build script requires Ruby with rake installed.

1. Run `InstallGems.bat` to get the ruby dependencies (only needs to be run once per computer)
1. open a command prompt to the root folder and type `rake` to execute rakefile.rb

If you do not have ruby:

1. You need to create a src\CommonAssemblyInfo.cs file. Go.bat will copy src\CommonAssemblyInfo.cs.default to src\CommonAssemblyInfo.cs
1. open src\StarterTree.sln with Visual Studio and build the solution
        
### License        

[MIT License][mitlicense]

This project is part of [MVBA's Open Source Projects][MvbaLawGithub].

[FluentAssert]: http://github.com/mvba/FluentAssert/
[MvbaLawGithub]: http://mvbalaw.github.io/
[mitlicense]: http://www.opensource.org/licenses/mit-license.php   
