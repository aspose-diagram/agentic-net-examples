using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing Visio diagrams: ");
            folderPath = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Get all Visio files (VSDX) in the folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");
        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No .vsdx files found in the specified folder.");
            return;
        }

        foreach (string filePath in diagramFiles)
        {
            // Guard: ensure the file exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify title shapes by their NameU property.
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                        {
                            // Rotate the text of the title shape by 180 degrees.
                            // TextXForm.TxtAngle controls the orientation of the shape's text.
                            shape.TextXForm.TxtAngle.Value = 180.0;
                        }
                    }
                }

                // Prepare output file name (original name with "_rotated" suffix).
                string directory = Path.GetDirectoryName(filePath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                string outputPath = Path.Combine(directory, fileNameWithoutExt + "_rotated.vsdx");

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}