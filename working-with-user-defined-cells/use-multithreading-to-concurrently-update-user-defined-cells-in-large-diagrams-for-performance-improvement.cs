using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a try/catch to handle any loading errors.
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

        // Define the name of the user‑defined cell to update and the new value.
        const string targetCellName = "MyCustomCell";
        const string newCellValue = "UpdatedValue";

        // Use Parallel.ForEach to update shapes concurrently for better performance.
        try
        {
            Parallel.ForEach(diagram.Pages, (Aspose.Diagram.Page page) =>
            {
                // Iterate over all shapes on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Iterate over user‑defined cells of the shape.
                    foreach (Aspose.Diagram.User userCell in shape.Users)
                    {
                        // Update only the targeted cell.
                        if (userCell.Name == targetCellName)
                        {
                            userCell.Value.Val = newCellValue;
                        }
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during parallel update: {ex.Message}");
            return;
        }

        // Save the modified diagram to the specified output path.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}