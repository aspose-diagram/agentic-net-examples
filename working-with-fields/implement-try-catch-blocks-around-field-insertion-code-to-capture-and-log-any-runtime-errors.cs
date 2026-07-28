using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page and the first shape as an example
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            try
            {
                // Create a new Field object
                Field field = new Field();

                // (Optional) Set additional properties on the field here
                // e.g., field.Value = "Some text"; // Value is read‑only, use appropriate properties as needed

                // Insert the field into the shape's field collection
                shape.Fields.Add(field);
            }
            catch (DiagramException dex)
            {
                // Capture Aspose.Diagram specific errors
                Console.WriteLine($"DiagramException caught: {dex.Message}");
            }
            catch (Exception ex)
            {
                // Capture any other runtime errors
                Console.WriteLine($"Unexpected error caught: {ex.Message}");
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
