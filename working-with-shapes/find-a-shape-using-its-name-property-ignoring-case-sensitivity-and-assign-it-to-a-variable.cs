using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "Drawing1.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
            return;
        }

        // Define the shape name to search for (case‑insensitive).
        string targetShapeName = "TargetShapeName"; // TODO: replace with the actual shape name.

        // Variable to hold the found shape.
        Shape foundShape = null;

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare the shape's Name property ignoring case.
                        if (!string.IsNullOrEmpty(shape.Name) &&
                            string.Equals(shape.Name, targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Assign the matching shape to the variable and exit loops.
                            foundShape = shape;
                            break;
                        }
                    }

                    // If the shape was found, stop searching further pages.
                    if (foundShape != null)
                        break;
                }
            }

            // Output the result.
            if (foundShape != null)
            {
                Console.WriteLine($"Shape found: ID = {foundShape.ID}, Name = {foundShape.Name}");
            }
            else
            {
                Console.WriteLine($"Shape with name \"{targetShapeName}\" was not found.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}