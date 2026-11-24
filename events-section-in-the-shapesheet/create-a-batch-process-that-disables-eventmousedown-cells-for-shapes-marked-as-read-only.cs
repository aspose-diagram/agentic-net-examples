using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is the input folder, second optional is the output folder.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> [outputFolder]");
            return;
        }

        string inputFolder = args[0];
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(inputFolder, "Modified");
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        // Process each Visio file in the input folder (common extensions).
        string[] extensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vss", ".vstx", ".vst", ".vtx" };
        foreach (string ext in extensions)
        {
            foreach (string filePath in Directory.GetFiles(inputFolder, "*" + ext, SearchOption.TopDirectoryOnly))
            {
                // Guard: ensure the file exists.
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"File not found: {filePath}");
                    continue;
                }

                try
                {
                    // Load the diagram inside a using block as required.
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate all pages.
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate all shapes on the page.
                            foreach (Shape shape in page.Shapes)
                            {
                                // Determine if the shape is marked as read‑only via a user‑defined cell named "ReadOnly".
                                bool isReadOnly = false;
                                foreach (User userCell in shape.Users)
                                {
                                    if (string.Equals(userCell.NameU, "ReadOnly", StringComparison.OrdinalIgnoreCase) &&
                                        string.Equals(userCell.Value.Val, "TRUE", StringComparison.OrdinalIgnoreCase))
                                    {
                                        isReadOnly = true;
                                        break;
                                    }
                                }

                                if (isReadOnly)
                                {
                                    // Disable the EventMouseDown cell. In Aspose.Diagram the closest equivalent is EventDrop.
                                    if (shape.Event != null && shape.Event.EventDrop != null && shape.Event.EventDrop.Ufe != null)
                                    {
                                        shape.Event.EventDrop.Ufe.F = string.Empty; // Clear the formula to disable the event.
                                    }
                                }
                            }
                        }

                        // Prepare output file path.
                        string fileName = Path.GetFileNameWithoutExtension(filePath);
                        string outputPath = Path.Combine(outputFolder, fileName + "_modified.vsdx");

                        // Save the modified diagram as VSDX.
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Processed and saved: {outputPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
    }
}