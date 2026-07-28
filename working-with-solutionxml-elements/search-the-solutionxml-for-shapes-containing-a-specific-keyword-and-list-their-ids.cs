using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (VSDX, VSD, etc.)
                string diagramPath = @"C:\Path\To\YourDiagram.vsdx";

                // Keyword to search for (case‑insensitive)
                string keyword = "YourKeyword";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // List to hold IDs of matching shapes
                List<long> matchingShapeIds = new List<long>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the plain text of the shape
                        string shapeText = shape.GetPureText();

                        // Check if the text contains the keyword
                        if (!string.IsNullOrEmpty(shapeText) &&
                            shapeText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchingShapeIds.Add(shape.ID);
                        }
                    }
                }

                // Output the results
                Console.WriteLine($"Shapes containing the keyword \"{keyword}\":");
                foreach (long id in matchingShapeIds)
                {
                    Console.WriteLine($"Shape ID: {id}");
                }

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }