using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ---------- Global diagram protection ----------
                // Example: protect backgrounds.
                diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                LogProtectionChange("Document", "ProtectBkgnds", BOOL.True);

                // ---------- Shape‑level protection ----------
                // Ensure there is at least one page and one shape.
                if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                {
                    // Retrieve the first shape on the first page.
                    Shape shape = diagram.Pages[0].Shapes.GetShape(0);
                    long shapeId = shape.ID;

                    // Lock the aspect ratio of the shape.
                    shape.Protection.LockAspect.Value = BOOL.True;
                    LogProtectionChange($"Shape ID {shapeId}", "LockAspect", BOOL.True);

                    // Lock movement on the X axis.
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    LogProtectionChange($"Shape ID {shapeId}", "LockMoveX", BOOL.True);
                }
                else
                {
                    Console.Error.WriteLine("The diagram does not contain any pages or shapes to modify.");
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Logs a protection change with a timestamp, the affected element, the property name, and the new value.
    /// </summary>
    static void LogProtectionChange(string elementIdentifier, string propertyName, BOOL newValue)
    {
        string timestamp = DateTime.Now.ToString("u"); // Universal sortable format.
        Console.WriteLine($"{timestamp} - Protection changed: {elementIdentifier}, {propertyName} = {newValue}");
    }
}