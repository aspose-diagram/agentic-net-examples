using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, output file, size limit in MB
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath> <sizeLimitInMB>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        double sizeLimitMb;

        // Parse size limit argument
        if (!double.TryParse(args[2], out sizeLimitMb) || sizeLimitMb < 0)
        {
            Console.Error.WriteLine("Invalid size limit. Provide a positive numeric value representing megabytes.");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate shapes backwards to allow safe removal
                for (int i = page.Shapes.Count - 1; i >= 0; i--)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes that are already marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        long sizeBytes = shape.ForeignData.ObjectData.Length;
                        double sizeMb = sizeBytes / (1024.0 * 1024.0);

                        // If the OLE object exceeds the size threshold, remove the shape
                        if (sizeMb > sizeLimitMb)
                        {
                            page.Shapes.Remove(shape);
                            Console.WriteLine($"Removed OLE shape ID {shape.ID} (size: {sizeMb:F2} MB).");
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}