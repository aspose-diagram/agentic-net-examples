using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BatchProcessReadOnlyShapes <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder existence
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process all Visio files in the input folder (VSDX format)
        string[] files = Directory.GetFiles(inputFolder, "*.vsdx");
        foreach (string filePath in files)
        {
            // Guard against missing files (should not happen with GetFiles, but added per rule)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify read‑only shapes by checking if Delete protection is locked
                        if (shape.Protection != null && shape.Protection.LockDelete != null &&
                            shape.Protection.LockDelete.Value == BOOL.True)
                        {
                            // Disable an event cell (EventDrop used as a representative example,
                            // since EventMouseDown is not a valid cell in the Aspose.Diagram API)
                            if (shape.Event != null && shape.Event.EventDrop != null)
                            {
                                shape.Event.EventDrop.Ufe.F = "";
                            }
                        }
                    }
                }

                // Save the modified diagram to the output folder with the same file name
                string fileName = Path.GetFileName(filePath);
                string outputPath = Path.Combine(outputFolder, fileName);
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}