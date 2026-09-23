using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to validate event formulas
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // The EventValidate cell is not directly exposed; use a generic check for the Event section
                    // Ensure the Event object exists before accessing its members
                    if (shape.Event != null)
                    {
                        // Example validation: check that the EventDblClick formula is not empty
                        // (Replace with the appropriate event cell if EventValidate becomes available)
                        string formula = shape.Event.EventDblClick?.Ufe?.F ?? string.Empty;

                        // Throw an exception if the formula is empty or whitespace
                        if (string.IsNullOrWhiteSpace(formula))
                        {
                            throw new Exception(
                                $"Shape ID {shape.ID} on page \"{page.Name}\" has an empty EventDblClick formula.");
                        }
                    }
                }
            }

            // Save the diagram after successful validation
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}