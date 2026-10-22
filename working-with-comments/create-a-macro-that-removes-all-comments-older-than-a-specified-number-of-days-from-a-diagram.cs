using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <daysThreshold> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string daysArg = args[1];
        string outputPath = args.Length >= 3 ? args[2] : inputPath;

        // Guard: check input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Parse days threshold.
        if (!int.TryParse(daysArg, out int daysThreshold) || daysThreshold < 0)
        {
            Console.Error.WriteLine("Error: Days threshold must be a non‑negative integer.");
            return;
        }

        // Compute cutoff date.
        DateTime cutoffDate = DateTime.Now.AddDays(-daysThreshold);

        try
        {
            // Load diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Access the page‑level annotations collection.
                    AnnotationCollection annotations = page.PageSheet.Annotations;

                    // Iterate backwards to safely remove items while iterating.
                    for (int i = annotations.Count - 1; i >= 0; i--)
                    {
                        Annotation ann = annotations[i];

                        // Retrieve the comment creation date safely.
                        DateTime commentDate = ann.Date?.Value ?? DateTime.MinValue;

                        // If the comment is older than the cutoff, remove it.
                        if (commentDate < cutoffDate)
                        {
                            // Remove the annotation from the collection.
                            // If RemoveAt is not available, use Remove(ann) instead.
                            annotations.RemoveAt(i);
                        }
                    }
                }

                // Save the modified diagram. Overwrite input if no separate output path provided.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Comments older than {0} days have been removed.", daysThreshold);
            Console.WriteLine("Diagram saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine("An error occurred: " + ex.Message);
        }
    }
}