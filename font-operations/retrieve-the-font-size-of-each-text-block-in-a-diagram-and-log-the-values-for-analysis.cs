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

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // Iterate through each character formatting entry in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font size is stored in inches; convert to points (1 inch = 72 points)
                                double fontSizeInPoints = ch.Size.Value * 72.0;

                                // Log the shape ID, character index, and font size
                                Console.WriteLine($"Shape ID: {shape.ID}, Char Index: {ch.IX}, Font Size: {fontSizeInPoints:F2} pt");
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