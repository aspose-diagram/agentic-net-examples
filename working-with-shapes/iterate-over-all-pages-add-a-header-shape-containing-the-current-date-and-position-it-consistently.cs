using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Current date string to be placed in the header.
                string dateText = DateTime.Now.ToString("d");

                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define header positioning constants.
                    double margin = 0.2;               // 0.2 inch margin from the top edge.
                    double headerHeight = 0.3;         // Height of the header text box.
                    double headerWidth = pageWidth - 2 * margin; // Width with side margins.
                    double pinX = margin;              // Left margin.
                    double pinY = pageHeight - margin - headerHeight; // Position from top.

                    // Add a text shape containing the date.
                    // The AddText method returns a long shape ID (not used further here).
                    page.AddText(pinX, pinY, headerWidth, headerHeight, dateText, "Arial", "#000000", 0.2);
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}