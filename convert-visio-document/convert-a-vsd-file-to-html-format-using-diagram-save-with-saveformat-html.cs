using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (VSD)
            string inputPath = "sample.vsd";

            // Path for the generated HTML file
            string outputPath = "sample.html";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options (default PNG images are used)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Save the diagram as HTML
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully converted to HTML: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
