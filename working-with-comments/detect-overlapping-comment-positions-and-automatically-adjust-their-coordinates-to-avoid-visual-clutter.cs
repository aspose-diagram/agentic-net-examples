using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Work with the first page (adjust as needed)
                Page page = diagram.Pages[0];

                // Threshold distance (in inches) to consider comments overlapping
                const double overlapThreshold = 0.5;
                // Offset to apply when moving a comment to avoid overlap
                const double offset = 0.6;

                // Collect existing annotations with their (simulated) positions
                // Note: Aspose.Diagram.Annotation does not expose PinX/PinY directly.
                // For demonstration, we treat MarkerIndex.Value as a placeholder for X coordinate
                // and use a fixed Y coordinate. In a real scenario, retrieve actual positions via
                // the appropriate cells or custom storage.
                var annotations = new System.Collections.Generic.List<(Annotation annotation, double x, double y)>();
                foreach (Annotation ann in page.PageSheet.Annotations)
                {
                    double x = ann.MarkerIndex.Value; // Placeholder for X coordinate
                    double y = 1.0; // Fixed Y coordinate placeholder
                    annotations.Add((ann, x, y));
                }

                // Adjust positions to resolve overlaps
                for (int i = 0; i < annotations.Count; i++)
                {
                    var (currentAnn, curX, curY) = annotations[i];

                    for (int j = i + 1; j < annotations.Count; j++)
                    {
                        var (otherAnn, otherX, otherY) = annotations[j];

                        double deltaX = curX - otherX;
                        double deltaY = curY - otherY;
                        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                        if (distance < overlapThreshold)
                        {
                            // Move the later comment by the offset to the right
                            otherX += offset;
                            // Update the stored position
                            annotations[j] = (otherAnn, otherX, otherY);
                        }
                    }
                }

                // Re‑add comments at the adjusted positions.
                // Since Aspose.Diagram does not provide a direct way to modify an existing annotation's
                // coordinates, we add new comments with the corrected positions.
                // (In practice, you might delete the original annotation first if needed.)
                foreach (var (annotation, x, y) in annotations)
                {
                    // Add a new comment at the resolved position with the same text
                    page.AddComment(x, y, annotation.Comment.Value);
                }

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }