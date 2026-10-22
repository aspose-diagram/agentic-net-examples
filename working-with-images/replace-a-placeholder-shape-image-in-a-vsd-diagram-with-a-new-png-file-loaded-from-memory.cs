using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input VSD diagram, PNG image, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <pngImagePath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string pngImagePath = args[1];
        string outputDiagramPath = args[2];

        // Guard: ensure input diagram file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: ensure PNG image file exists
        if (!File.Exists(pngImagePath))
        {
            Console.Error.WriteLine($"PNG image file not found: {pngImagePath}");
            return;
        }

        try
        {
            // Load PNG image into memory
            byte[] pngBytes = File.ReadAllBytes(pngImagePath);

            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify picture shapes by checking for Foreign type
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Replace the existing image data with the new PNG bytes
                            shape.ForeignData.Value = pngBytes;
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Image replacement completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}