using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape – for demonstration we use the first shape on the page
                // Ensure the page contains at least one shape
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("No shapes found on the first page.");
                }

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Verify the shape has at least one field (e.g., a date, page number, or custom formula field)
                if (shape.Fields.Count == 0)
                {
                    throw new Exception("The selected shape does not contain any fields to modify.");
                }

                // Access the first field in the collection
                Field field = shape.Fields[0];

                // Set the formula to calculate the area (Width * Height)
                // The formula is stored in the Ufev.F property of the field's Value object
                field.Value.Ufev.F = "Width*Height";

                // Optionally, clear any existing format strings to avoid conflicts
                field.Format.Val = "";
                field.Format.Ufev.F = "";
                field.Format.Ufev.Unit = MeasureConst.Undefined;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }