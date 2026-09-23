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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains any hyperlinks
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            // Iterate through each hyperlink attached to the shape
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Validate that the Description cell is not empty or whitespace
                                if (string.IsNullOrWhiteSpace(link.Description.Value))
                                {
                                    // Throw an exception with details about the offending shape
                                    throw new Exception(
                                        $"Shape ID {shape.ID} on page '{page.Name}' has a hyperlink with an empty description.");
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("Validation completed successfully. All hyperlinks have non‑empty descriptions.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }