using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

public class DiagramProcessor
{
    // Cached AutoSpaceOptions instance reused for all diagrams
    private static readonly AutoSpaceOptions CachedOptions = CreateAutoSpaceOptions();

    private static AutoSpaceOptions CreateAutoSpaceOptions()
    {
        var options = new AutoSpaceOptions();
        // Configure spacing (in inches) as needed
        options.DistanceInHorizontal = 0.5;
        options.DistanceInVertical = 0.5;
        return options;
    }

    public static void ProcessDiagram(string inputPath, string outputPath)
    {
        // Load the diagram from the specified file
        var diagram = new Diagram(inputPath);

        // Apply auto‑spacing to each page using the cached options
        foreach (Page page in diagram.Pages)
        {
            page.AutoSpaceShapes(page.Shapes, CachedOptions);
        }

        // Save the modified diagram as VSDX
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }

    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramProcessor <input.vsdx> <output.vsdx>");
            return;
        }

        string inputFile = args[0];
        string outputFile = args[1];

        try
        {
            ProcessDiagram(inputFile, outputFile);
            Console.WriteLine($"Diagram processed and saved to '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}
