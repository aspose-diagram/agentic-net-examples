using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output_cleaned.vsdx";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an Event section before accessing it
                    if (shape.Event != null)
                    {
                        // The EventComment cell does not exist in Aspose.Diagram.
                        // If other event cells need to be cleared, they can be set here.
                        // Example (optional): shape.Event.EventDblClick.Ufe.F = "";
                    }
                }
            }

            // Save the modified diagram using the correct SaveFileFormat enum member
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}