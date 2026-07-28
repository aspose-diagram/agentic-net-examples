using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Use a HashSet to collect unique author names
                HashSet<string> authorSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Get the reviewer index for this comment
                        int reviewerIndex = annotation.ReviewerID.Value;

                        // Retrieve the reviewer object from the document's reviewer collection
                        Reviewer reviewer = diagram.DocumentSheet.Reviewers[reviewerIndex];

                        // Extract the reviewer name (Str2Value) and add to the set
                        if (reviewer != null && reviewer.Name != null)
                        {
                            string authorName = reviewer.Name.Value;
                            if (!string.IsNullOrWhiteSpace(authorName))
                            {
                                authorSet.Add(authorName.Trim());
                            }
                        }
                    }
                }

                // Transfer the set to a list for sorting
                List<string> authorList = new List<string>(authorSet);
                authorList.Sort(StringComparer.OrdinalIgnoreCase);

                // Output file path
                string outputPath = "CommentAuthors.txt";

                // Write the sorted author names to the text file
                File.WriteAllLines(outputPath, authorList);

                Console.WriteLine($"Author list written to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }