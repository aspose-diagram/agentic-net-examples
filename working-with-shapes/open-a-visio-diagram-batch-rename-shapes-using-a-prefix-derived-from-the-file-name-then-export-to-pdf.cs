using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = @"C:\Diagrams\SampleDiagram.vsdx";

            // Load the diagram using the provided constructor rule
            Diagram diagram = new Diagram(inputPath);

            // Create a prefix from the file name (without extension) followed by an underscore
            string prefix = Path.GetFileNameWithoutExtension(inputPath) + "_";

            // Iterate through all pages and their shapes to rename them
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Preserve the original name and prepend the prefix
                    string originalName = shape.NameU;
                    shape.NameU = prefix + originalName;
                }
            }

            // Define the output PDF file path (same folder, same base name, .pdf extension)
            string outputPdfPath = Path.ChangeExtension(inputPath, ".pdf");

            // Export the modified diagram to PDF using the provided Save method rule
            diagram.Save(outputPdfPath, SaveFileFormat.Pdf);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
