using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing diagram files; use first argument or prompt the user.
        string folderPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.Write("Enter the full path to the folder with diagram files: ");
            folderPath = Console.ReadLine()?.Trim() ?? "";
        }

        // Guard: ensure the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all Visio files (VSDX) in the specified folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No .vsdx files found in the specified folder.");
            return;
        }

        // Process each diagram file individually.
        foreach (string filePath in diagramFiles)
        {
            // Guard: verify the file still exists (in case of race conditions).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found (skipped): {filePath}");
                continue;
            }

            try
            {
                // Load the diagram from the file.
                Diagram diagram = new Diagram(filePath);

                // Iterate through every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Explicitly set line properties to their current values.
                        // This forces the shape to use its own line cells rather than inheriting.
                        shape.Line.LineColor.Value = shape.Line.LineColor.Value;
                        shape.Line.LineWeight.Value = shape.Line.LineWeight.Value;
                        shape.Line.LinePattern.Value = shape.Line.LinePattern.Value;
                        shape.Line.BeginArrow.Value = shape.Line.BeginArrow.Value;
                        shape.Line.EndArrow.Value = shape.Line.EndArrow.Value;
                        shape.Line.BeginArrowSize.Value = shape.Line.BeginArrowSize.Value;
                        shape.Line.EndArrowSize.Value = shape.Line.EndArrowSize.Value;
                        shape.Line.LineCap.Value = shape.Line.LineCap.Value;
                        shape.Line.Rounding.Value = shape.Line.Rounding.Value;
                    }
                }

                // Construct a new file name to avoid overwriting the original (optional).
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(filePath) ?? "",
                    Path.GetFileNameWithoutExtension(filePath) + "_updated.vsdx");

                // Save the modified diagram using the VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}