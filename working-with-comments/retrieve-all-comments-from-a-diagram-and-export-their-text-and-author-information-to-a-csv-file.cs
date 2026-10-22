using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input diagram file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build a list of reviewer names in the order they appear.
                List<string> reviewerNames = new List<string>();
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Reviewer.Name is a Str2Value; safely retrieve its string value.
                    string name = reviewer.Name?.Value ?? string.Empty;
                    reviewerNames.Add(name);
                }

                // Prepare to write CSV output.
                using (var writer = new StreamWriter(outputPath))
                {
                    // Write CSV header.
                    writer.WriteLine("Comment,Author,Date");

                    // Iterate all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate annotations (comments) on the page's PageSheet.
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Retrieve comment text.
                            string commentText = annotation.Comment?.Value ?? string.Empty;

                            // Retrieve reviewer (author) index and map to name.
                            int reviewerIndex = annotation.ReviewerID?.Value ?? -1;
                            string author = "Unknown";
                            if (reviewerIndex >= 0 && reviewerIndex < reviewerNames.Count)
                            {
                                string possibleName = reviewerNames[reviewerIndex];
                                if (!string.IsNullOrWhiteSpace(possibleName))
                                    author = possibleName;
                            }

                            // Retrieve comment creation date.
                            DateTime date = annotation.Date?.Value ?? DateTime.MinValue;
                            string dateString = date == DateTime.MinValue ? string.Empty : date.ToString("o");

                            // Escape CSV fields that contain commas, quotes, or newlines.
                            string Escape(string s)
                            {
                                if (s.Contains("\""))
                                    s = s.Replace("\"", "\"\"");
                                if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
                                    s = $"\"{s}\"";
                                return s;
                            }

                            // Write the CSV line.
                            writer.WriteLine($"{Escape(commentText)},{Escape(author)},{Escape(dateString)}");
                        }
                    }
                }

                Console.WriteLine($"Comments exported successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}