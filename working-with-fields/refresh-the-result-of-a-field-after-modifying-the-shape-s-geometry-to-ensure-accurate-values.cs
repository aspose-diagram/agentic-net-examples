using System.IO;
using System;
using Aspose.Diagram;

class RefreshFieldAfterGeometryChange
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape you want to modify (replace with actual shape ID)
            int shapeId = 1; // example shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // ---- Modify the shape's geometry ----
            // Example: change the shape's width (value is in inches)
            shape.XForm.Width.Value = 2.0; // set new width

            // ---- Refresh shape data ----
            // This recalculates the shape's position, connections, geometry,
            // and updates any fields/formulas that depend on the geometry.
            shape.RefreshData();

            // ---- Retrieve updated field values (if any) ----
            // Fields are stored in the shape's Fields collection.
            foreach (Field field in shape.Fields)
            {
                // Display the raw value of the field
                Console.WriteLine($"Field IX={field.IX}, Value={field.Value}");
                // Optionally, display the formatted value
                Console.WriteLine($"Formatted: {field.DisplayValue}");
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
