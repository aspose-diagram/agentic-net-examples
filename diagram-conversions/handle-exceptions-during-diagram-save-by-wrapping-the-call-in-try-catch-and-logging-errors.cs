using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define save options if needed (optional)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
            // Example: enable auto fit page
            saveOptions.AutoFitPageToDrawingContent = true;

            try
            {
                // Attempt to save the diagram to a file using the specified format
                diagram.Save("output.vdx", saveOptions);
                Console.WriteLine("Diagram saved successfully.");
            }
            catch (DiagramException dex)
            {
                // Handle Aspose.Diagram specific exceptions
                Console.Error.WriteLine($"DiagramException occurred while saving: {dex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                Console.Error.WriteLine($"Unexpected error while saving diagram: {ex.Message}");
            }
            finally
            {
                // Ensure resources are released
                diagram.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
