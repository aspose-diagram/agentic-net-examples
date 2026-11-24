using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <fieldIndex>");
            return;
        }

        string inputPath = args[0];
        string fieldIndexArg = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Parse the field index argument
        if (!int.TryParse(fieldIndexArg, out int fieldIndex) || fieldIndex < 0)
        {
            Console.Error.WriteLine($"Error: Invalid field index '{fieldIndexArg}'. Must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Guard: ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Guard: ensure the first page has at least one shape
            Page firstPage = diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the first page
            Shape shape = firstPage.Shapes[0];

            // Attempt to access the specified field index
            try
            {
                // Validate that the field index exists in the collection
                if (fieldIndex >= shape.Fields.Count)
                {
                    throw new IndexOutOfRangeException($"Field index {fieldIndex} is out of range. Shape contains {shape.Fields.Count} field(s).");
                }

                // Access the field
                Field targetField = shape.Fields[fieldIndex];

                // Example operation: update the field's value
                targetField.Value.Val = "UpdatedValue";

                Console.WriteLine($"Field at index {fieldIndex} updated successfully.");
            }
            catch (Exception ex) when (ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
            {
                // Log detailed error information for missing field
                Console.Error.WriteLine("Error: Specified field does not exist.");
                Console.Error.WriteLine($"Exception Message: {ex.Message}");
                Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
                // Optionally, continue without aborting the program
            }

            // Define output path
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related exceptions and log details
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine($"Exception Type: {ex.GetType().FullName}");
            Console.Error.WriteLine($"Message: {ex.Message}");
            Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}