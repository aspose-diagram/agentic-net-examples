using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the diagram inside a try/catch to capture loading errors.
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram contains at least one page and one shape.
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("Error: Diagram contains no pages.");
            return;
        }

        var page = diagram.Pages[0];

        if (page.Shapes.Count == 0)
        {
            Console.Error.WriteLine("Error: First page contains no shapes.");
            return;
        }

        // Retrieve the first shape on the first page.
        Shape shape = page.Shapes[0];

        // ------------------------------
        // Insert a new text field into the shape.
        // ------------------------------
        try
        {
            // Create a new Field object.
            Field field = new Field();

            // Set the field's displayed value.
            field.Value.Val = "Sample Field";

            // Optional: clear any formatting strings.
            field.Format.Val = "";
            field.Format.Ufev.F = "";
            field.Format.Ufev.Unit = MeasureConst.Undefined;

            // Add the field to the shape's field collection.
            shape.Fields.Add(field);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error inserting field: {ex.Message}");
            // Continue execution; the diagram may still be saved without the new field.
        }

        // ------------------------------
        // Save the modified diagram.
        // ------------------------------
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}