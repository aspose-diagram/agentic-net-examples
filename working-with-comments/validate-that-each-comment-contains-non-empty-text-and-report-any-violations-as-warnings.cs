using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int totalWarnings = 0;

                // Iterate through each page in the diagram.
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate through all annotations (comments) on the page.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment text safely.
                        string commentText = annotation.Comment?.Value ?? string.Empty;

                        // Check for empty or whitespace‑only comments.
                        if (string.IsNullOrWhiteSpace(commentText))
                        {
                            totalWarnings++;
                            Console.WriteLine(
                                $"Warning: Empty comment detected on page {pageIdx + 1}, marker ID {annotation.MarkerIndex?.Value ?? 0}.");
                        }
                    }
                }

                // Summarize the result.
                if (totalWarnings == 0)
                {
                    Console.WriteLine("No empty comments were found.");
                }
                else
                {
                    Console.WriteLine($"Validation completed with {totalWarnings} warning(s).");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}