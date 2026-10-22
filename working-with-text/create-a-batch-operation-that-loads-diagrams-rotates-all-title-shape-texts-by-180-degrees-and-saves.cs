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

        // Validate the folder path.
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
            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Identify title shapes by their universal name.
                            if (string.Equals(shape.NameU, "Title", StringComparison.OrdinalIgnoreCase))
                            {
                                // Rotate the text within the shape by 180 degrees.
                                // TxtAngle is a DoubleValue expressed in radians.
                                double angleRadians = Math.PI; // 180 degrees = π radians
                                shape.TextXForm.TxtAngle.Value = angleRadians;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}