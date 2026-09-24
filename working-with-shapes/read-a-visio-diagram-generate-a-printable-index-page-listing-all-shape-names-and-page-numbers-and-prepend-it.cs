using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the existing Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Collect shape names with their page numbers
        List<string> indexLines = new List<string>();
        int pageNumber = 1;
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                string shapeName = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : shape.Name;
                if (string.IsNullOrEmpty(shapeName))
                    shapeName = "<Unnamed Shape>";
                indexLines.Add($"{shapeName} - Page {pageNumber}");
            }
            pageNumber++;
        }

        // Build the index text
        StringBuilder sb = new StringBuilder();
        foreach (string line in indexLines)
        {
            sb.AppendLine(line);
        }
        string indexText = sb.ToString();

        // Determine a new unique page ID
        int maxPageId = 0;
        foreach (Page p in diagram.Pages)
        {
            if (p.ID > maxPageId)
                maxPageId = p.ID;
        }

        // Create the index page
        Page indexPage = new Page(maxPageId + 1);
        indexPage.Name = "Index";
        diagram.Pages.Add(indexPage);
        // Move it to the first position
        indexPage.MoveTo(0);

        // Get page dimensions for positioning the text shape
        double pageWidth = indexPage.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = indexPage.PageSheet.PageProps.PageHeight.Value;

        double pinX = pageWidth / 2.0;
        double pinY = pageHeight / 2.0;
        double txtWidth = pageWidth - 1.0;   // small margin
        double txtHeight = pageHeight - 1.0; // small margin

        // Add a text shape containing the index
        Shape indexShape = indexPage.AddText(pinX, pinY, txtWidth, txtHeight, indexText);

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
