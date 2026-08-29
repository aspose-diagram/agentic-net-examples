using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the path to your diagram file
                Diagram diagram = new Diagram("input.vsdx");

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape firstShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    firstShape = shape;
                    break;
                }

                if (firstShape == null)
                {
                    throw new Exception("No shapes found on the first page.");
                }

                // Ensure the shape contains text and at least one paragraph
                if (firstShape.Text == null || firstShape.Paras.Count == 0)
                {
                    throw new Exception("The first shape does not contain any text paragraphs.");
                }

                // Align the first paragraph to center
                // HorzAlignValue.Center is the enum value for center alignment
                firstShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                // Set the font size of all characters in the shape to 12 points (12/72 inches)
                double fontSizeInInches = 12.0 / 72.0;
                foreach (Aspose.Diagram.Char ch in firstShape.Chars)
                {
                    ch.Size.Value = fontSizeInInches;
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Paragraph formatting applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }