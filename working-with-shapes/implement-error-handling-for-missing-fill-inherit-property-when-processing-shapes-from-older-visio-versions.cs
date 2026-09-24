using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a try/catch to capture loading errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through each page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Attempt to access the inherited fill properties.
                try
                {
                    // Access the inherited foreground color (may not exist in older files).
                    string inheritForegnd = shape.InheritFill.FillForegnd.Value;

                    // Access the inherited background color (may not exist in older files).
                    string inheritBkgnd = shape.InheritFill.FillBkgnd.Value;

                    // Output the retrieved inherited fill values for diagnostic purposes.
                    Console.WriteLine($"Shape ID {shape.ID}: Inherited Foreground = {inheritForegnd}, Background = {inheritBkgnd}");
                }
                catch (MissingMemberException mme)
                {
                    // Specific handling when the InheritFill property or its sub‑properties are missing.
                    Console.Error.WriteLine($"Shape ID {shape.ID} missing InheritFill member: {mme.Message}");
                }
                catch (Exception ex)
                {
                    // General fallback for any other unexpected errors while accessing fill data.
                    Console.Error.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
                }
            }
        }

        // Define output file path for the processed diagram.
        string outputPath = "output.vsdx";

        // Save the diagram (even if unchanged) with error handling.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}