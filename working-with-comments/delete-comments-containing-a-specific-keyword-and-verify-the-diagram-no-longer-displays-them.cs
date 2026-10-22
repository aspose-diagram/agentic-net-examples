using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input diagram path and keyword to delete.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <keyword> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string keyword = args[1];
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

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
                    // Collect annotations that contain the keyword.
                    List<Annotation> annotationsToRemove = new List<Annotation>();
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        string commentText = annotation.Comment?.Value ?? string.Empty;
                        if (commentText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            annotationsToRemove.Add(annotation);
                        }
                    }

                    // Remove the collected annotations from the page.
                    foreach (Annotation annotation in annotationsToRemove)
                    {
                        page.PageSheet.Annotations.Remove(annotation);
                    }

                    // Verify that no remaining annotation contains the keyword.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        string commentText = annotation.Comment?.Value ?? string.Empty;
                        if (commentText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            throw new Exception($"Comment containing keyword '{keyword}' still present after deletion.");
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Comments containing '{keyword}' have been removed. Saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}