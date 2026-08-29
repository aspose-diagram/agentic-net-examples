using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the VSDX file to load
                string filePath = "input.vsdx";

                // Load the diagram from the specified file
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Output the page ID and the shape ID
                            Console.WriteLine($"Page {page.ID}, Shape ID: {shape.ID}");
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }