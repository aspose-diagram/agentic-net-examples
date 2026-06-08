using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure the shape contains text
                            if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            {
                                // Iterate through each character formatting run in the shape
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    // Font size is stored in inches; convert to points (1 inch = 72 points)
                                    double sizeInInches = ch.Size.Value;
                                    double sizeInPoints = sizeInInches * 72.0;

                                    Console.WriteLine(
                                        $"Page: {page.Name}, Shape ID: {shape.ID}, Char Index: {ch.IX}, " +
                                        $"Font: {ch.FontName.Value}, Size: {sizeInPoints:F2} pt");
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