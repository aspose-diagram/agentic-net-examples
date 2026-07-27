using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists.
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to validate an event cell.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // NOTE: Aspose.Diagram does not expose an EventValidate cell.
                    // As a substitute, validate the EventDblClick cell which is available.
                    var eventCell = shape.Event.EventDblClick;

                    // Check that the formula is present and not empty.
                    if (eventCell == null || string.IsNullOrWhiteSpace(eventCell.Ufe.F))
                    {
                        throw new Exception(
                            $"Shape ID {shape.ID} on page \"{page.Name}\" has an empty EventDblClick formula.");
                    }
                }
            }

            // Save the diagram after successful validation.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}