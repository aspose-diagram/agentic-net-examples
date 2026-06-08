using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment text
                        string commentText = annotation.Comment.Value;

                        // Check for empty or whitespace-only comments
                        if (string.IsNullOrWhiteSpace(commentText))
                        {
                            Console.WriteLine(
                                $"Warning: Empty comment found on page \"{page.Name}\" (ID {page.ID}), " +
                                $"MarkerIndex {annotation.MarkerIndex.Value}.");
                        }
                    }
                }

                // Optional: indicate completion
                Console.WriteLine("Comment validation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }