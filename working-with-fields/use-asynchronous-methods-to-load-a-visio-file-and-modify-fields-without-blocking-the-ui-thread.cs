using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    // Asynchronously loads a Visio diagram from the specified file path.
    private static Task<Diagram> LoadDiagramAsync(string filePath)
    {
        return Task.Run(() => new Diagram(filePath));
    }

    // Updates the first text field of the first shape on the first page, if present.
    private static void UpdateFirstField(Diagram diagram)
    {
        // Ensure there is at least one page.
        if (diagram.Pages.Count == 0)
        {
            Console.WriteLine("No pages found in the diagram.");
            return;
        }

        Page page = diagram.Pages[0];

        // Ensure there is at least one shape.
        if (page.Shapes.Count == 0)
        {
            Console.WriteLine("No shapes found on the first page.");
            return;
        }

        Shape shape = page.Shapes[0];

        // Ensure the shape has at least one field.
        if (shape.Fields.Count == 0)
        {
            Console.WriteLine("The shape does not contain any fields.");
            return;
        }

        // Modify the first field's value.
        Field field = shape.Fields[0];
        field.Value.Val = "Updated Value";
        // Clear any formula or unit information.
        field.Value.Ufev.F = "";
        field.Value.Ufev.Unit = MeasureConst.Undefined;

        Console.WriteLine($"Field updated for shape ID {shape.ID} on page '{page.Name}'.");
    }

    // Asynchronously saves the diagram to the specified output path.
    private static Task SaveDiagramAsync(Diagram diagram, string outputPath)
    {
        return Task.Run(() => diagram.Save(outputPath, SaveFileFormat.Vsdx));
    }

    // Entry point.
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            // Load the diagram without blocking the UI thread.
            Diagram diagram = await LoadDiagramAsync(inputPath);
            Console.WriteLine("Diagram loaded successfully.");

            // Perform field modifications.
            UpdateFirstField(diagram);

            // Save the modified diagram asynchronously.
            await SaveDiagramAsync(diagram, outputPath);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
