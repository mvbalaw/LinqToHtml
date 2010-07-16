A simple utility that lets you use LINQ syntax to query HTML.

## Samples

    const string html = "<html><head><TITLE>The Title</TITLE></head><body>Hello World</body></html>";
    var parsed = HTMLParser.Parse(html);
    
    parsed
        .DescendantTags
        .OfType("title").IgnoreCase()
        .Any()
        .ShouldBeTrue();

    parsed
        .DescendantTags
        .OfType("title")
        .Any()
        .ShouldBeFalse();

    parsed
        .ChildTags
        .Count()
        .ShouldBeEqualTo(2);    // head, body
        
    parsed
        .DescendantTags
        .OfType("title").IgnoreCase()
        .First()
        .Parent
        .TypeEqualsIgnoreCase("HEAD")
        .ShouldBeTrue();    

    parsed
        .DescendantTags
        .Count(x => x.Parent.TypeEqualsIgnoreCase("HTML"))
        .ShouldBeEqualTo(2);   // head, body
        
    parsed
        .Body
        .Content
        .ShouldBeEqualTo("Hello World");        
        
## License        

[MIT License][mitlicense]

This project is part of [MVBA Law Commons][mvbalawcommons].

[mvbalawcommons]: http://code.google.com/p/mvbalaw-commons/
[mitlicense]: http://www.opensource.org/licenses/mit-license.php   
