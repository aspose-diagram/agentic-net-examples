using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio diagram file
        string diagramPath = "input.vsdx";
        // Verify the diagram file exists
        if (!File.Exists(diagramPath)) { Console.Error.WriteLine($"File not found: {diagramPath}"); return; }

        // Path to the output text file
        string outputPath = "authors.txt";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Build a map of reviewer indices to reviewer names (Reviewer.ID does not exist)
            var reviewerMap = new Dictionary<int, string>();
            int reviewerIndex = 0;
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                // Reviewer.Name is a Str2Value; use .Value to get the string
                string reviewerName = reviewer.Name.Value ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(reviewerName))
                {
                    reviewerMap[reviewerIndex] = reviewerName;
                }
                reviewerIndex++;
            }

            // Collect all author names from annotations (comments)
            var authorSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Annotation.ReviewerID is an IntValue referencing the reviewer index
                    int reviewerId = annotation.ReviewerID.Value;
                    if (reviewerMap.TryGetValue(reviewerId, out string author))
                    {
                        authorSet.Add(author);
                    }
                }
            }

            // Sort authors alphabetically
            var sortedAuthors = new List<string>(authorSet);
            sortedAuthors.Sort(StringComparer.OrdinalIgnoreCase);

            // Write the sorted list to the output file
            File.WriteAllLines(outputPath, sortedAuthors);

            Console.WriteLine($"Author list written to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}