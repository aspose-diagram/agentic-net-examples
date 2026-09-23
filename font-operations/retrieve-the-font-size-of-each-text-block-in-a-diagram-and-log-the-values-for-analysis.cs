using System;
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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains any text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // Iterate through each character formatting run
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font size is stored in inches; convert to points (1 inch = 72 points)
                                double sizeInInches = ch.Size.Value;
                                double sizeInPoints = sizeInInches * 72.0;

                                // Log the font size information
                                Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Char Index: {ch.IX}, Font Size: {sizeInPoints} pt");
                            }
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