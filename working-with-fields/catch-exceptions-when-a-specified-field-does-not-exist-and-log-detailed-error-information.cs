using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Indices of the page, shape, and field to access
            int pageIndex = 0;   // first page
            int shapeIndex = 0;  // first shape on the page
            int fieldIndex = 5;  // the field we expect to exist

            try
            {
                // Attempt to retrieve the specified field
                Field field = diagram.Pages[pageIndex].Shapes[shapeIndex].Fields[fieldIndex];

                // Use the field (e.g., display its value)
                Console.WriteLine($"Field Value: {field.Value}");
            }
            catch (DiagramException dex)
            {
                // Detailed logging for Aspose.Diagram specific errors
                Console.WriteLine("DiagramException caught while accessing the field:");
                Console.WriteLine($"Message   : {dex.Message}");
                Console.WriteLine($"StackTrace: {dex.StackTrace}");
            }
            catch (Exception ex)
            {
                // Logging for any other unexpected errors
                Console.WriteLine("Unexpected exception caught:");
                Console.WriteLine($"Message   : {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }

            // Save the diagram (if any changes were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
