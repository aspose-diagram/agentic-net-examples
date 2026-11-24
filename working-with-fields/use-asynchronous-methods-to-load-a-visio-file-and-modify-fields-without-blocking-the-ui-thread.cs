using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram asynchronously to avoid blocking the UI thread (simulated in console).
            Task<Diagram> loadTask = LoadDiagramAsync(inputPath);
            loadTask.Wait(); // Wait for the async load to complete.
            Diagram diagram = loadTask.Result;

            // Modify custom shape properties (Props) in the diagram.
            ModifyShapeProperties(diagram);

            // Save the modified diagram back to disk.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading, modification, or saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Asynchronously loads a Visio diagram from the specified file path.
    /// </summary>
    private static Task<Diagram> LoadDiagramAsync(string path)
    {
        // Use Task.Run to perform the potentially time‑consuming load operation on a background thread.
        return Task.Run(() => new Diagram(path));
    }

    /// <summary>
    /// Iterates through all pages and shapes, updating the first custom property (Prop) of each shape if present.
    /// </summary>
    private static void ModifyShapeProperties(Diagram diagram)
    {
        // Loop over each page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // Loop over each shape on the current page.
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a Props collection and at least one property.
                if (shape.Props != null && shape.Props.Count > 0)
                {
                    // Access the first custom property.
                    Prop prop = shape.Props[0];

                    // Update the property's value.
                    prop.Value.Val = "UpdatedValue";

                    // Optionally, you could also update the label or other attributes here.
                }
            }
        }
    }
}