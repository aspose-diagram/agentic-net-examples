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

                // Flag to track validation status
                bool hasInvalidHyperlink = false;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Hyperlinks collection exists
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            // Iterate through each hyperlink of the shape
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Check if the Description cell is empty or whitespace
                                if (string.IsNullOrWhiteSpace(link.Description.Value))
                                {
                                    hasInvalidHyperlink = true;
                                    Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has a hyperlink with an empty description.");
                                }
                            }
                        }
                    }
                }

                // Report overall result
                if (hasInvalidHyperlink)
                {
                    throw new Exception("Validation failed: one or more hyperlinks have empty descriptions.");
                }
                else
                {
                    Console.WriteLine("All hyperlinks have non‑empty descriptions.");
                }

                // Optionally save the diagram (unchanged) to a new file
                // diagram.Save("validated_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }