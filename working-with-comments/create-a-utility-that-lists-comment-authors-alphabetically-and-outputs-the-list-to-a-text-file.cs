using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output text file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: CommentAuthorLister <inputVisioFile> <outputTextFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Collect author names (reviewer names) from annotations
            HashSet<string> authorSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Annotations are stored in the PageSheet
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // ReviewerID links to a reviewer in the document's Reviewer collection
                    int reviewerId = annotation.ReviewerID.Value;

                    // Ensure the reviewer index is within bounds
                    if (reviewerId >= 0 && reviewerId < diagram.DocumentSheet.Reviewers.Count)
                    {
                        var reviewer = diagram.DocumentSheet.Reviewers[reviewerId];
                        // Reviewer.Name is a Str2Value; retrieve the actual string via .Value
                        string authorName = reviewer.Name.Value ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(authorName))
                        {
                            authorSet.Add(authorName.Trim());
                        }
                    }
                }
            }

            // Sort the distinct author names alphabetically
            List<string> sortedAuthors = new List<string>(authorSet);
            sortedAuthors.Sort(StringComparer.OrdinalIgnoreCase);

            // Write the sorted list to the output text file
            try
            {
                File.WriteAllLines(outputPath, sortedAuthors);
                Console.WriteLine($"Author list written to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write output file: {ex.Message}");
            }
        }
    }