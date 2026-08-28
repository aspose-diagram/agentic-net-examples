using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio diagram (required argument or default file name)
        string diagramPath = args.Length > 0 ? args[0] : "diagram.vsdx";
        // Verify that the diagram file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Load the diagram inside a try/catch to capture any Aspose.Diagram errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram contains at least one page to work with
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram does not contain any pages.");
            return;
        }

        // Use the first page for the auto‑spacing benchmark
        Page page = diagram.Pages[0];

        // Configure auto‑spacing options (horizontal and vertical gaps in inches)
        AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
        {
            DistanceInHorizontal = 0.5, // 0.5 inches between shapes horizontally
            DistanceInVertical = 0.5    // 0.5 inches between shapes vertically
        };

        // Warm‑up run to mitigate JIT overhead before measuring
        try
        {
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Warm‑up auto‑spacing failed: {ex.Message}");
            return;
        }

        // Measure the time taken for the auto‑spacing operation
        Stopwatch sw = Stopwatch.StartNew();
        try
        {
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Auto‑spacing failed: {ex.Message}");
            return;
        }
        sw.Stop();

        // Output the elapsed time in milliseconds
        Console.WriteLine($"Auto‑spacing completed in {sw.ElapsedMilliseconds} ms.");

        // Optional: save the auto‑spaced diagram to verify the result
        string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "auto_spaced.vsdx");
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Auto‑spaced diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}