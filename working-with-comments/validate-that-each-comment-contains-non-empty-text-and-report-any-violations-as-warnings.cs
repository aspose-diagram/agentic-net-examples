using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // List to collect warning messages for empty comments
                List<string> warnings = new List<string>();

                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all annotations (comments) on the current page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment text
                        string commentText = annotation.Comment.Value;

                        // Check for empty or whitespace-only comment text
                        if (string.IsNullOrWhiteSpace(commentText))
                        {
                            warnings.Add(
                                $"Warning: Empty comment detected on page {pageIndex + 1}, marker index {annotation.MarkerIndex.Value}.");
                        }
                    }
                }

                // Output the collected warnings
                if (warnings.Count > 0)
                {
                    foreach (string warning in warnings)
                    {
                        Console.WriteLine(warning);
                    }
                }
                else
                {
                    Console.WriteLine("No empty comments found.");
                }

                // Save the diagram (optional, can be omitted if no changes are made)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }