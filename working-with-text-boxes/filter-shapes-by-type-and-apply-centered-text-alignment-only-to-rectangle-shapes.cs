using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace the path with the actual file location.
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify rectangle shapes via their master name.
                        if (shape.Master != null && shape.Master.Name == "Rectangle")
                        {
                            // Ensure the shape has at least one paragraph for text formatting.
                            if (shape.Paras.Count == 0)
                            {
                                // Add a default paragraph if none exist.
                                shape.Paras.Add(new Para());
                            }

                            // Apply horizontal center alignment.
                            shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                            // Apply vertical middle alignment.
                            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
                        }
                    }
                }

                // Save the modified diagram.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }