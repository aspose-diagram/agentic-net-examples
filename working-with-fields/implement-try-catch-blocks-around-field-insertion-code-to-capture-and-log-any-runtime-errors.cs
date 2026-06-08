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

            // Create a new Field object that will be inserted into the shape
            Field field = new Field();

            // Example: you could set additional properties on the field here
            // (e.g., field.Type, field.Format, etc.)

            // Insert the field into the shape with error handling
            try
            {
                // The Fld class represents the insertion point for the field.
                // Index 0 means the field will be inserted at the beginning of the shape's text.
                Fld insertionPoint = new Fld(0, shape);

                // Add the field to the shape's Fields collection.
                // The Add method returns the index at which the field was inserted.
                shape.Fields.Add(field);
            }
            catch (DiagramException dex)
            {
                // Capture and log Aspose.Diagram specific exceptions
                Console.WriteLine($"DiagramException occurred: {dex.Message}");
            }
            catch (Exception ex)
            {
                // Capture and log any other unexpected exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
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
