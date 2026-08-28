using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    // Entry point – async to avoid blocking the thread.
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram asynchronously.
        Diagram diagram = await LoadDiagramAsync(inputPath);

        // Modify text fields in the diagram.
        ModifyFields(diagram);

        // Save the modified diagram asynchronously.
        await SaveDiagramAsync(diagram, outputPath);

        Console.WriteLine("Diagram processing completed.");
    }

    // Asynchronously creates a Diagram instance from a file.
    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        return Task.Run(() => new Diagram(path));
    }

    // Synchronous field modification – quick operation, no need for async.
    private static void ModifyFields(Diagram diagram)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has at least one field to modify.
                if (shape.Fields.Count > 0)
                {
                    // Update the first field's value.
                    Field field = shape.Fields[0];
                    field.Value.Val = "Updated Value";

                    // Clear any formula or unit information.
                    field.Value.Ufev.F = "";
                    field.Value.Ufev.Unit = MeasureConst.Undefined;
                }
            }
        }
    }

    // Asynchronously saves the diagram to a file using a specific format.
    private static Task SaveDiagramAsync(Diagram diagram, string outputPath)
    {
        return Task.Run(() => diagram.Save(outputPath, SaveFileFormat.Vsdx));
    }
}
