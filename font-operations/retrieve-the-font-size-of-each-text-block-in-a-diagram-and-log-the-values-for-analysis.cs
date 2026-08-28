using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the diagram file path as the first command‑line argument
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: DiagramFontSizeLogger <path-to-visio-file>");
                    return;
                }

                string diagramPath = args[0];

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Verify the shape actually contains text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // Iterate over character formatting runs within the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Font size is stored in inches; convert to points (1 inch = 72 points)
                                double sizeInInches = ch.Size.Value;
                                double sizeInPoints = sizeInInches * 72.0;

                                Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Char Index: {ch.IX}, Font Size: {sizeInPoints:F2} pt");
                            }
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }