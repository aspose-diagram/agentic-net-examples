using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_adjusted.vsdx");

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define a small distance threshold (in inches) to consider comments overlapping
                const double epsilon = 0.01; // Adjust as needed
                const double offsetStep = 0.2; // Shift overlapping comments by this amount

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect annotations (comments) from the page
                    List<Annotation> annotations = new List<Annotation>(page.PageSheet.Annotations);

                    // Iterate through annotations to detect and resolve overlaps
                    for (int i = 0; i < annotations.Count; i++)
                    {
                        Annotation annA = annotations[i];
                        double xA = annA.X?.Value ?? 0;
                        double yA = annA.Y?.Value ?? 0;

                        // Compare with subsequent annotations
                        for (int j = i + 1; j < annotations.Count; j++)
                        {
                            Annotation annB = annotations[j];
                            double xB = annB.X?.Value ?? 0;
                            double yB = annB.Y?.Value ?? 0;

                            // Check if positions are within the overlap threshold
                            if (Math.Abs(xA - xB) < epsilon && Math.Abs(yA - yB) < epsilon)
                            {
                                // Adjust the second annotation's position to avoid overlap
                                double newX = xB + offsetStep;
                                double newY = yB + offsetStep;

                                // Update annotation coordinates
                                if (annB.X != null) annB.X.Value = newX;
                                if (annB.Y != null) annB.Y.Value = newY;

                                // Update local variables for further comparisons
                                xB = newX;
                                yB = newY;
                            }
                        }
                    }
                }

                // Save the adjusted diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with adjusted comments to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}