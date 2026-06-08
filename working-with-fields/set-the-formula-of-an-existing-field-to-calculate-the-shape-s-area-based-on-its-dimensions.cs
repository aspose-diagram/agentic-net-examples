using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the existing Visio file
                string inputPath = "input.vsdx";
                // Path where the modified Visio file will be saved
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has at least one field
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            // Example: modify the first field of the shape
                            Field field = shape.Fields[0];

                            // Set the formula to calculate area (Width * Height)
                            // The formula is stored in the Ufev.F property of the field's Value
                            field.Value.Ufev.F = "Width*Height";

                            // Optionally, clear any existing format strings
                            field.Format.Val = "";
                            field.Format.Ufev.F = "";
                            field.Format.Ufev.Unit = MeasureConst.Undefined;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }