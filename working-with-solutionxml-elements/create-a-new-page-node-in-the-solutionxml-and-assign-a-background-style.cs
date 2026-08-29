using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram instance
        using (Diagram diagram = new Diagram())
        {
            // Create a SolutionXML element that represents a page node with a background style
            SolutionXML pageNode = new SolutionXML();
            pageNode.Name = "PageBackground";
            // Example XML defining a page with a background color (hex code)
            pageNode.XmlValue = "<Page id=\"1\" backgroundStyle=\"#ADD8E6\"/>";

            // Add the SolutionXML element to the diagram's collection
            diagram.SolutionXMLs.Add(pageNode);

            // Optionally add an actual page to the diagram and mark it as a background page
            Page backgroundPage = new Page();
            backgroundPage.Name = "BackgroundPage";
            backgroundPage.Background = BOOL.True; // Mark as background
            diagram.Pages.Add(backgroundPage);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
