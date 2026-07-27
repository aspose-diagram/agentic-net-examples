using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find pentagon shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is not deleted and has a master named "Pentagon"
                        if (shape.Del == BOOL.False && shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            // Clear any existing text
                            shape.Text.Value.Clear();

                            // Add the annotation text
                            shape.Text.Value.Add(new Txt("Annotation"));

                            // Center the text block within the shape
                            shape.TextXForm.TxtPinX.Value = 0.5; // 50% of shape width
                            shape.TextXForm.TxtPinY.Value = 0.5; // 50% of shape height

                            // Optional: set vertical alignment to middle
                            shape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                            // Optional: set horizontal alignment to center if a paragraph exists
                            if (shape.Paras.Count > 0)
                            {
                                shape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;
                            }
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