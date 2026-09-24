using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // ----- Determine input and output file paths -----
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // ----- Load the diagram inside a try/catch block -----
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

        bool allMastersPresent = true; // flag to track validation result

        // ----- Validate that every shape has a non‑null Master reference -----
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // If a shape's Master is null, report it and mark validation as failed
                    if (shape.Master == null)
                    {
                        Console.Error.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" lacks a master reference.");
                        allMastersPresent = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
            return;
        }

        // ----- If validation succeeded, perform bulk modifications -----
        if (allMastersPresent)
        {
            try
            {
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example bulk modification: set fill foreground color to red
                        shape.Fill.FillForegnd.Value = "#FF0000";

                        // Example bulk modification: replace existing text with a placeholder
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt("Modified"));
                    }
                }

                // ----- Save the modified diagram -----
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during bulk modification or saving: {ex.Message}");
            }
        }
        else
        {
            Console.Error.WriteLine("Bulk modifications aborted due to missing master references.");
        }
    }
}