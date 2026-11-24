using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> <daysThreshold>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        if (!int.TryParse(args[2], out int daysThreshold) || daysThreshold < 0)
        {
            Console.Error.WriteLine("The days threshold must be a non‑negative integer.");
            return;
        }

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Access the page‑level annotations collection.
                    AnnotationCollection annotations = page.PageSheet.Annotations;

                    // Iterate backwards so that removal does not affect the index order.
                    for (int i = annotations.Count - 1; i >= 0; i--)
                    {
                        Annotation ann = annotations[i];

                        // Retrieve the comment creation date safely.
                        DateTime commentDate = ann.Date?.Value ?? DateTime.MinValue;

                        // Determine if the comment is older than the specified threshold.
                        if ((DateTime.Now - commentDate).TotalDays > daysThreshold)
                        {
                            // Remove the outdated comment.
                            annotations.RemoveAt(i);
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Comments older than {0} days have been removed.", daysThreshold);
            Console.WriteLine("Modified diagram saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine("An error occurred: " + ex.Message);
        }
    }
}