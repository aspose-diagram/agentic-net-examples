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

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the annotations (comments) collection on the page sheet
                    var annotations = page.PageSheet.Annotations;

                    // Iterate through each annotation
                    foreach (Annotation annotation in annotations)
                    {
                        // Retrieve the comment text
                        string commentText = annotation.Comment.Value;

                        // Check for empty or whitespace-only comments
                        if (string.IsNullOrWhiteSpace(commentText))
                        {
                            // Report a warning with page name and comment identifier
                            Console.WriteLine(
                                $"Warning: Empty comment detected on page \"{page.Name}\" (MarkerIndex: {annotation.MarkerIndex.Value}).");
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