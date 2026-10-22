using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Define the list of required user‑defined cell names.
        List<string> requiredCellNames = new List<string>
        {
            "MyCell1",
            "MyCell2"
        };

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool validationFailed = false;

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check each required user‑defined cell.
                        foreach (string requiredName in requiredCellNames)
                        {
                            bool cellFound = false;

                            // Iterate over the Users collection of the shape.
                            foreach (User userCell in shape.Users)
                            {
                                // Compare the unique name of the user‑defined cell.
                                if (userCell.NameU == requiredName)
                                {
                                    cellFound = true;
                                    break;
                                }
                            }

                            // If a required cell is missing, report it.
                            if (!cellFound)
                            {
                                Console.Error.WriteLine($"Error: Shape ID {shape.ID} on page \"{page.Name}\" is missing required user‑defined cell \"{requiredName}\".");
                                validationFailed = true;
                            }
                        }
                    }
                }

                // Abort export if any validation errors were found.
                if (validationFailed)
                {
                    Console.Error.WriteLine("Validation failed. Diagram will not be exported.");
                    return;
                }

                // Export the validated diagram to PDF.
                diagram.Save(outputPath, SaveFileFormat.Pdf);
                Console.WriteLine($"Diagram successfully exported to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}