using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the document title; use a fallback if it's empty
            string title = diagram.DocumentProps.Title;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "Untitled Diagram";
            }

            // Set global header and footer.
            // HeaderLeft will display the diagram title on every page.
            diagram.HeaderFooter.HeaderLeft = title;

            // FooterRight uses the Visio field code '&p' to insert the current page number.
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
