using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file (replace with actual file path)
                string diagramPath = "input.vsdx";

                // Load the diagram. Diagram implements IDisposable, so we use a using block.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape contains text.
                            if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            {
                                // Iterate through each character formatting run in the shape.
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    // Font size is stored in inches. Convert to points (1 inch = 72 points).
                                    double fontSizePoints = ch.Size.Value * 72.0;

                                    // Log the shape ID, character index, and font size.
                                    Console.WriteLine(
                                        $"Page: {page.Name}, Shape ID: {shape.ID}, Char Index: {ch.IX}, Font Size: {fontSizePoints:F2} pt");
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