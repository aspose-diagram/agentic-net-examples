using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define a minimal distance (in inches) to consider comments overlapping
                const double overlapThreshold = 0.5; // 0.5 inches
                // Define offset to apply when moving a comment to resolve overlap
                const double offset = 0.2; // 0.2 inches

                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    // Retrieve the current page
                    Page page = diagram.Pages[pageIndex];

                    // Collect annotations (comments) from the page
                    List<Annotation> annotations = new List<Annotation>();
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        annotations.Add(ann);
                    }

                    // Simple O(n^2) overlap detection and adjustment
                    for (int i = 0; i < annotations.Count; i++)
                    {
                        Annotation a1 = annotations[i];

                        // Ensure the annotation has X and Y coordinates; if not, skip adjustment
                        if (a1.X == null || a1.Y == null)
                            continue;

                        double x1 = a1.X.Value;
                        double y1 = a1.Y.Value;

                        for (int j = i + 1; j < annotations.Count; j++)
                        {
                            Annotation a2 = annotations[j];

                            if (a2.X == null || a2.Y == null)
                                continue;

                            double x2 = a2.X.Value;
                            double y2 = a2.Y.Value;

                            // Compute Euclidean distance between the two comment positions
                            double distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

                            // If the distance is less than the threshold, adjust the second comment
                            if (distance < overlapThreshold)
                            {
                                // Apply offset to the second comment's coordinates
                                a2.X.Value = x2 + offset;
                                a2.Y.Value = y2 + offset;

                                // Update local variables for further comparisons
                                x2 = a2.X.Value;
                                y2 = a2.Y.Value;
                            }
                        }
                    }
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}