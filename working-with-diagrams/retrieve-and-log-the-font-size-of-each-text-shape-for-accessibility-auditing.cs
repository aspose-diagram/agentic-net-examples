using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape contains visible text
                            if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            {
                                // If the shape has character formatting information, log each character's font size
                                if (shape.Chars != null && shape.Chars.Count > 0)
                                {
                                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                                    {
                                        // Font size is stored in inches; convert to points for readability (1 inch = 72 points)
                                        double fontSizeInPoints = ch.Size.Value * 72.0;

                                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}, Char Index: {ch.IX}, Font Size: {fontSizeInPoints:F2} pt");
                                    }
                                }
                                else
                                {
                                    // No character formatting; report that the shape has text but no explicit font size
                                    Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Shape Name: {shape.NameU}, Font Size: (default or unspecified)");
                                }
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