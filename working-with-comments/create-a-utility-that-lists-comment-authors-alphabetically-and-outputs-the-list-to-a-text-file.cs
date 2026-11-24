using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputTextPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect distinct author identifiers from all annotations.
                HashSet<string> authors = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Annotations (comments) are stored at the page level.
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the reviewer ID for this comment.
                        int reviewerId = annotation.ReviewerID.Value;
                        // Use the reviewer ID as the author identifier.
                        authors.Add(reviewerId.ToString());
                    }
                }

                // Convert the set to a list and sort alphabetically.
                List<string> sortedAuthors = new List<string>(authors);
                sortedAuthors.Sort(StringComparer.OrdinalIgnoreCase);

                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the sorted author list to the output text file.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    foreach (string author in sortedAuthors)
                    {
                        writer.WriteLine(author);
                    }
                }

                Console.WriteLine($"Author list written to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}