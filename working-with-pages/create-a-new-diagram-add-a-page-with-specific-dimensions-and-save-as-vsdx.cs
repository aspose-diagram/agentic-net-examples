using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page newPage = new Page();
        diagram.Pages.Add(newPage);

        // Define the desired page dimensions (in inches)
        double pageWidth = 11.0;   // Width of the page
        double pageHeight = 8.5;   // Height of the page

        // Apply the dimensions to the newly added page
        newPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
        newPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

        // Save the diagram as a VSDX file
        string outputPath = "output.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
