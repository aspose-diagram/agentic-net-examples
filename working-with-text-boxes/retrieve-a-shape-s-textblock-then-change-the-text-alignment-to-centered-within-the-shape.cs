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
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape shape = null;
                foreach (Shape s in page.Shapes)
                {
                    shape = s;
                    break;
                }

                if (shape == null)
                {
                    throw new Exception("No shape found on the first page.");
                }

                // Ensure the shape has at least one paragraph for horizontal alignment
                if (shape.Paras.Count == 0)
                {
                    // Add a default paragraph if none exist
                    shape.Paras.Add(new Para());
                }

                // Center text horizontally within the shape
                shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                // Center text vertically within the shape's text block
                shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }