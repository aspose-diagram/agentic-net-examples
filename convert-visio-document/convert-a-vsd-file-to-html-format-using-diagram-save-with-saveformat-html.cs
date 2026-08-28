using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VsdToHtmlConverter
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputFile = "input.vsd";

            // Path where the HTML output will be saved
            string outputFile = "output.html";

            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(inputFile);

            // Save the diagram as HTML using the built‑in Save method with SaveFileFormat.Html
            diagram.Save(outputFile, SaveFileFormat.Html);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
