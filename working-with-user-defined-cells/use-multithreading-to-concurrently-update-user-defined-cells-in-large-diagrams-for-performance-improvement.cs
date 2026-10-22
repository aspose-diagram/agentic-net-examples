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

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define the name of the user‑defined cell to update.
            const string targetCellName = "MyCell";

            // Use Parallel.ForEach to process pages concurrently.
            Parallel.ForEach(diagram.Pages, (Aspose.Diagram.Page page) =>
            {
                // Lock the page object while modifying its shapes to avoid race conditions.
                lock (page)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Lock the shape while updating its user‑defined cells.
                        lock (shape)
                        {
                            bool cellFound = false;

                            // Search for an existing user‑defined cell with the target name.
                            foreach (Aspose.Diagram.User userCell in shape.Users)
                            {
                                if (string.Equals(userCell.Name, targetCellName, StringComparison.OrdinalIgnoreCase))
                                {
                                    // Update the cell value.
                                    userCell.Value.Val = "Updated";
                                    cellFound = true;
                                    break;
                                }
                            }

                            // If the cell does not exist, create and add it.
                            if (!cellFound)
                            {
                                Aspose.Diagram.User newUser = new Aspose.Diagram.User();
                                newUser.Name = targetCellName;
                                newUser.Value.Val = "Updated";
                                shape.Users.Add(newUser);
                            }
                        }
                    }
                }
            });

            // Save the updated diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}