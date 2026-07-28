using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    // Threshold distance (in inches) to consider two comments overlapping
    private const double OverlapThreshold = 0.2;

    // Offset applied to a comment when an overlap is detected
    private const double OffsetStep = 0.3;

    public static void Main()
    {
        try
        {

            // Load the diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect annotations (comments) on the page
                var annotations = page.PageSheet.Annotations;

                // Simple O(N^2) overlap detection and adjustment
                for (int i = 0; i < annotations.Count; i++)
                {
                    Annotation annA = annotations[i];

                    // Retrieve current coordinates (assumed to be stored in X and Y cells)
                    double xA = annA.X.Value;
                    double yA = annA.Y.Value;

                    for (int j = i + 1; j < annotations.Count; j++)
                    {
                        Annotation annB = annotations[j];
                        double xB = annB.X.Value;
                        double yB = annB.Y.Value;

                        // Compute Euclidean distance between the two comments
                        double distance = Math.Sqrt(Math.Pow(xA - xB, 2) + Math.Pow(yA - yB, 2));

                        // If the comments are too close, shift the second one
                        if (distance < OverlapThreshold)
                        {
                            // Apply an offset to the second comment's position
                            annB.X.Value = xB + OffsetStep;
                            annB.Y.Value = yB + OffsetStep;

                            // Update local variables for further comparisons
                            xB = annB.X.Value;
                            yB = annB.Y.Value;
                        }
                    }
                }
            }

            // Save the adjusted diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
