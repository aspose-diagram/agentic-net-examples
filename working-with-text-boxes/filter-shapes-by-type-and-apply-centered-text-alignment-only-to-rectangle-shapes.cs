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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify rectangle shapes by master name
                        if (shape.Master != null && shape.Master.Name == "Rectangle")
                        {
                            // Ensure the shape has at least one paragraph for horizontal alignment
                            if (shape.Paras.Count > 0)
                            {
                                // Center horizontally
                                shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                            }

                            // Center vertically using the text block alignment
                            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
                        }
                    }
                }

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