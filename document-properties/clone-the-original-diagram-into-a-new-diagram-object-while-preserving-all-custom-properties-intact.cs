using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (use arguments if provided)
            string inputPath = args.Length > 0 ? args[0] : "original.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "clone.vsdx";

            // Load the original diagram
            using (Diagram original = new Diagram(inputPath))
            {
                // Create a new empty diagram
                using (Diagram clone = new Diagram())
                {
                    // Merge all pages, masters, styles, and custom properties from the original
                    clone.Combine(original);

                    // Save the cloned diagram preserving all custom properties
                    clone.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
