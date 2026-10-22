using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a backup of the original diagram state in memory.
                using (MemoryStream backupStream = new MemoryStream())
                {
                    diagram.Save(backupStream, SaveFileFormat.Vsdx);
                    backupStream.Position = 0; // Reset stream for later reading.

                    try
                    {
                        // ----- Begin geometry update transaction -----
                        // For demonstration, modify the first shape on the first page.
                        if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                        {
                            Page page = diagram.Pages[0];
                            Shape shape = page.Shapes[0];

                            // Update geometry: change width and height.
                            shape.XForm.Width.Value = 5.0;   // 5 inches width
                            shape.XForm.Height.Value = 3.0; // 3 inches height

                            // Refresh shape data to apply changes.
                            shape.RefreshData();

                            // Simulate an error occurring during the transaction.
                            // Comment out the line below to allow successful save.
                            // throw new InvalidOperationException("Simulated geometry error");
                        }
                        else
                        {
                            Console.Error.WriteLine("Diagram does not contain any pages or shapes to modify.");
                        }
                        // ----- End geometry update transaction -----
                    }
                    catch (Exception ex)
                    {
                        // Log the exception.
                        Console.Error.WriteLine($"Error during geometry update: {ex.Message}");
                        Console.Error.WriteLine("Rolling back to original diagram state.");

                        // Roll back by loading the diagram from the backup stream.
                        // Note: we need a new Diagram instance for the rolled‑back state.
                        using (Diagram rolledBackDiagram = new Diagram(backupStream))
                        {
                            // Save the rolled‑back diagram to the output path.
                            rolledBackDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                        }

                        // Exit after rollback.
                        return;
                    }

                    // If no exception occurred, save the updated diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., loading failures).
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}