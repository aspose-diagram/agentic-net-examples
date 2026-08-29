using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (provide via command line or use default)
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                int redLineCount = 0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the line and its color are defined
                        if (shape.Line?.LineColor?.Value != null)
                        {
                            // Compare the line color to red (hex #FF0000, case‑insensitive)
                            if (string.Equals(shape.Line.LineColor.Value, "#FF0000", StringComparison.OrdinalIgnoreCase))
                            {
                                redLineCount++;
                            }
                        }
                    }
                }

                Console.WriteLine($"Number of shapes with a red line: {redLineCount}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }