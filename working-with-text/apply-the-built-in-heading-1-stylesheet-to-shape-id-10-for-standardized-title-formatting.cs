using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";   // replace with actual file path
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (avoid using ActivePage)
                Page page = diagram.Pages[0];

                // Retrieve the shape with ID 10
                Shape shape = page.Shapes.GetShape(10L);
                if (shape == null)
                {
                    throw new Exception("Shape with ID 10 not found.");
                }

                // Find the built‑in stylesheet named "Heading 1"
                StyleSheet headingStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Heading 1")
                    {
                        headingStyle = ss;
                        break;
                    }
                }

                if (headingStyle == null)
                {
                    throw new Exception("StyleSheet 'Heading 1' not found in the diagram.");
                }

                // Apply the stylesheet to the shape (text, fill, and line styles)
                shape.TextStyle = headingStyle;
                shape.FillStyle = headingStyle;
                shape.LineStyle = headingStyle;

                // Save the modified diagram
                string outputPath = "output.vsdx"; // replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }