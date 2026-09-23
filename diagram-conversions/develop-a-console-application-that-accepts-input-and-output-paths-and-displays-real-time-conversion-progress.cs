using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that input and output paths are provided
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramConverter <inputPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            // Load the source Visio diagram
            Console.WriteLine("Loading diagram...");
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine("Diagram loaded.");

            // Determine the desired output format from the file extension
            string ext = System.IO.Path.GetExtension(outputPath).ToLowerInvariant();

            // Display conversion progress (simulated)
            Console.WriteLine("Converting...");

            // Save the diagram in the requested format
            switch (ext)
            {
                case ".pdf":
                    diagram.Save(outputPath, SaveFileFormat.Pdf);
                    break;
                case ".png":
                    diagram.Save(outputPath, SaveFileFormat.Png);
                    break;
                case ".jpg":
                case ".jpeg":
                    diagram.Save(outputPath, SaveFileFormat.Jpeg);
                    break;
                case ".svg":
                    diagram.Save(outputPath, SaveFileFormat.Svg);
                    break;
                case ".vsdx":
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    break;
                default:
                    Console.WriteLine("Unsupported output format.");
                    return;
            }

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
