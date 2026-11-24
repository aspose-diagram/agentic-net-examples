using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for input Visio file path
        Console.Write("Enter the path to the Visio file (e.g., diagram.vsdx): ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the input file exists
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file does not exist.");
            return;
        }

        // Prompt for the shape ID to modify
        Console.Write("Enter the Shape ID (numeric) whose area formula you want to set: ");
        string shapeIdText = Console.ReadLine();
        if (!long.TryParse(shapeIdText, out long shapeId))
        {
            Console.Error.WriteLine("Invalid Shape ID.");
            return;
        }

        // Prompt for output file path
        Console.Write("Enter the output file path (e.g., diagram_modified.vsdx): ");
        string outputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Output path cannot be empty.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page; adjust as needed
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                    return;
                }

                // Create a new field that calculates area = Width * Height
                Field areaField = new Field();

                // Set the formula for the field. Visio formula syntax uses cell names.
                // Width and Height refer to the shape's XForm.Width and XForm.Height cells.
                areaField.Value.Ufev.F = "Width*Height";

                // Optionally set the unit to undefined (default)
                areaField.Value.Ufev.Unit = MeasureConst.Undefined;

                // Add the field to the shape's Fields collection
                shape.Fields.Add(areaField);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}