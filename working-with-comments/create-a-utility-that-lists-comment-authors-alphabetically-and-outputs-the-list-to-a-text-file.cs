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
            Diagram diagram = new Diagram(inputPath);

            // Collect reviewer IDs that actually have comments
            HashSet<int> reviewerIdsWithComments = new HashSet<int>();

            foreach (Page page in diagram.Pages)
            {
                // Annotations are stored in the page's DocumentSheet
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // ReviewerID is a Str2Value; retrieve its integer value
                    int reviewerId = annotation.ReviewerID.Value;
                    reviewerIdsWithComments.Add(reviewerId);
                }
            }

            // Map reviewer IDs to their names
            List<string> authorNames = new List<string>();
            int currentId = 0;
            foreach (Reviewer reviewer in diagram.DocumentSheet.Reviewers)
            {
                if (reviewerIdsWithComments.Contains(currentId))
                {
                    // Reviewer.Name is a Str2Value; extract the string
                    string name = reviewer.Name.Value;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        authorNames.Add(name);
                    }
                }
                currentId++;
            }

            // Sort author names alphabetically (case‑insensitive)
            authorNames.Sort(StringComparer.OrdinalIgnoreCase);

            // Write the sorted list to the output text file
            try
            {
                File.WriteAllLines(outputPath, authorNames);
                Console.WriteLine($"Successfully wrote {authorNames.Count} author(s) to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }