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
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = "validated_output.vsdx";

        bool allShapesValid = true;

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the formula from a valid event cell (EventDblClick used as example)
                    // Note: Aspose.Diagram does not expose an EventValidate cell; using EventDblClick for validation
                    string eventFormula = shape.Event.EventDblClick.Ufe.F;

                    // Check if the formula is null, empty, or whitespace
                    if (string.IsNullOrWhiteSpace(eventFormula))
                    {
                        Console.WriteLine($"Validation error: Shape ID {shape.ID} on page \"{page.Name}\" has an empty event formula.");
                        allShapesValid = false;
                    }
                }
            }

            // If any shape failed validation, abort the save operation
            if (!allShapesValid)
                throw new Exception("One or more shapes have empty event formulas. Save operation aborted.");

            // All shapes passed validation; save the diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}