using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be validated
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the comment text
                        string commentText = annotation.Comment.Value;

                        // Check for empty or whitespace-only comments
                        if (string.IsNullOrWhiteSpace(commentText))
                        {
                            // Report a warning with page name and comment identifier
                            Console.WriteLine(
                                $"Warning: Empty comment found on page \"{page.Name}\" (Comment ID: {annotation.MarkerIndex.Value}).");
                        }
                    }
                }

                // Optional: indicate validation completed
                Console.WriteLine("Comment validation completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }