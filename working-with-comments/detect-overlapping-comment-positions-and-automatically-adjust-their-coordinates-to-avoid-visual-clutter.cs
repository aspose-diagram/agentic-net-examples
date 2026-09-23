using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";
                // Path to save the adjusted diagram
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Threshold distance (in inches) to consider comments overlapping
                const double overlapThreshold = 0.2;
                // Offset to apply when moving a comment to avoid overlap
                const double offsetY = 0.3;

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect all annotations (comments) on the page
                    List<Annotation> annotations = new List<Annotation>();
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        annotations.Add(ann);
                    }

                    // Compare each pair of annotations for overlap
                    for (int i = 0; i < annotations.Count; i++)
                    {
                        Annotation a1 = annotations[i];
                        // Ensure the annotation has valid X and Y values
                        if (a1.X == null || a1.Y == null) continue;

                        for (int j = i + 1; j < annotations.Count; j++)
                        {
                            Annotation a2 = annotations[j];
                            if (a2.X == null || a2.Y == null) continue;

                            double deltaX = Math.Abs(a1.X.Value - a2.X.Value);
                            double deltaY = Math.Abs(a1.Y.Value - a2.Y.Value);

                            // If both X and Y are within the threshold, consider them overlapping
                            if (deltaX < overlapThreshold && deltaY < overlapThreshold)
                            {
                                // Move the second comment downwards to separate them
                                a2.Y.Value += offsetY;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }