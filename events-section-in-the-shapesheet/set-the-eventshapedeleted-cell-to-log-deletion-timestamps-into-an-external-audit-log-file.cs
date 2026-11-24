using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments: input Visio, output Visio, audit log file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <auditLogPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string auditLogPath = args[2];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure directory for audit log exists (create if necessary)
        try
        {
            string auditDir = Path.GetDirectoryName(auditLogPath);
            if (!string.IsNullOrEmpty(auditDir) && !Directory.Exists(auditDir))
            {
                Directory.CreateDirectory(auditDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to prepare audit log directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Set an event formula that would be evaluated on shape deletion.
                        // The Visio EventShapeDeleted cell does not exist in Aspose.Diagram;
                        // EventDrop is used as the closest substitute.
                        shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"LogDeletion\")";

                        // If the shape is already marked as deleted, log the timestamp now.
                        if (shape.Del == BOOL.True)
                        {
                            string logEntry = $"{DateTime.UtcNow:O} - Shape ID {shape.ID} was deleted.";
                            File.AppendAllText(auditLogPath, logEntry + Environment.NewLine);
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}