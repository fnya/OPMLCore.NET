# OPMLCore.NET
OPML Parser in .NET Core (C#)

# What's OPMLCore.NET ?
OPMLCore.NET is OPML Parser which is easy to use. 

It is written in C# for .NET Core 2.0.

# Usage
 How to use is below.

```
using OPMLCore.NET; //Add

Opml opml = new Opml("opmlFilePath");

foreach (Outline outline in opml.Body.Outlines) 
{
    //Output outline node
    Console.WriteLine(outline.Text);
    Console.WriteLine(outline.XMLUrl);
    
    //output child's output node
    foreach (Outline childOutline in outline.Outlines)
    {
        Console.WriteLine(childOutline.Text);
        Console.WriteLine(childOutline.XMLUrl);                    
    }
}

```

For more detail, show test code or source code.

# License
This Project's Lisense is MIT License.