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
            Console.Error.WriteLine("Usage: CommentAuthorsLister <inputVisioPath> <outputTextPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
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
                // Prepare a set for unique author names (case‑insensitive).
                HashSet<string> uniqueAuthors = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Cache reviewers for quick lookup by index.
                List<string> reviewerNames = new List<string>();
                foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
                {
                    // Reviewer.Name is a Str2Value; use .Value to get the string.
                    string name = reviewer.Name?.Value ?? string.Empty;
                    reviewerNames.Add(name);
                }

                // Iterate all pages and their annotations (comments).
                foreach (Page page in diagram.Pages)
                {
                    // Annotations are stored in the page's PageSheet.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // ReviewerID points to the reviewer index.
                        int reviewerId = annotation.ReviewerID?.Value ?? -1;
                        if (reviewerId >= 0 && reviewerId < reviewerNames.Count)
                        {
                            string author = reviewerNames[reviewerId];
                            if (!string.IsNullOrWhiteSpace(author))
                            {
                                uniqueAuthors.Add(author);
                            }
                        }
                    }
                }

                // Transfer to a list and sort alphabetically (case‑insensitive).
                List<string> sortedAuthors = new List<string>(uniqueAuthors);
                sortedAuthors.Sort(StringComparer.OrdinalIgnoreCase);

                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the sorted author list to the output text file.
                File.WriteAllLines(outputPath, sortedAuthors);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}