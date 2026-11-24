using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input folder path from arguments or prompt the user.
        string inputFolder;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputFolder = args[0];
        }
        else
        {
            Console.Write("Enter the folder path containing Visio files: ");
            inputFolder = Console.ReadLine();
        }

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(inputFolder) || !Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine("The specified folder does not exist.");
            return;
        }

        // Prepare the output folder.
        string outputFolder = Path.Combine(inputFolder, "output");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Define supported Visio extensions.
        string[] supportedExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vssx", ".vstx", ".vss", ".vst" };

        // Get all files with supported extensions.
        string[] allFiles = Directory.GetFiles(inputFolder);
        foreach (string filePath in allFiles)
        {
            string ext = Path.GetExtension(filePath);
            if (Array.IndexOf(supportedExtensions, ext, 0, supportedExtensions.Length) < 0)
                continue; // Skip unsupported files.

            // Guard: ensure the file exists (redundant but per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through each page.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape contains any text.
                        string shapeText = shape.Text.Value.ToString();
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue; // No text to rotate.

                        // Apply a 30-degree rotation to the shape's text.
                        // TextXForm.TxtAngle expects radians.
                        double angleDeg = 30.0;
                        double angleRad = (Math.PI / 180.0) * angleDeg;
                        shape.TextXForm.TxtAngle.Value = angleRad;
                    }
                }

                // Determine output file path (same name, saved as VSDX).
                string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(filePath) + ".vsdx");

                // Save the modified diagram in VSDX format.
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