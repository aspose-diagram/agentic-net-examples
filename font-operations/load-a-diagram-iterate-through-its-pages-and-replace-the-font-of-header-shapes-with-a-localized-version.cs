using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (replace with actual paths or pass via command line)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify header shapes by name (case‑insensitive check for the word "Header")
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Header", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Update the font for each character run in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Set the desired localized font name
                            ch.FontName.Value = "Arial Unicode MS";
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram processing completed successfully.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
