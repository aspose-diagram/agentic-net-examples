using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the target sub‑shape was found
            bool subShapeFound = false;

            // Iterate through all pages to locate the sub‑shape inside a group
            foreach (Page page in diagram.Pages)
            {
                // Attempt to retrieve the sub‑shape by its name (including inside groups)
                // Replace "SubShapeName" with the actual name of the sub‑shape you want to modify
                Shape subShape = page.Shapes.GetShapeIncludingChild("SubShapeName");
                if (subShape != null)
                {
                    // Adjust the width of the sub‑shape to 5.0 inches using SetWidth(double)
                    subShape.SetWidth(5.0);
                    subShapeFound = true;
                    break; // Exit after modifying the first matching shape
                }
            }

            if (!subShapeFound)
            {
                Console.Error.WriteLine("Sub‑shape not found in the diagram.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}