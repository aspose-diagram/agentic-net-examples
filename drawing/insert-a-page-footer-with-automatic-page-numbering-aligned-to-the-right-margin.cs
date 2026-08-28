using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Ensure there is at least one page in the diagram
        diagram.Pages.Add(new Page());

        // Insert automatic page numbering aligned to the right margin
        diagram.HeaderFooter.FooterRight = "Page: &p";

        // Optional: set the distance of the footer from the page edge (in inches)
        diagram.HeaderFooter.FooterMargin.Value = 0.5;

        // Save the diagram (VSDX format)
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
