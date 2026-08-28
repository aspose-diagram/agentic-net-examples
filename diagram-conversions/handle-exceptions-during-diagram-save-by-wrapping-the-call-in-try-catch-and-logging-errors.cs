using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramSaveExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // (Assumes the file exists at the specified path)
            Diagram diagram = new Diagram("input.vsdx");

            // Define save options (optional, can be omitted if not needed)
            SaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);

            // Attempt to save the diagram and handle any exceptions
            try
            {
                // Save the diagram to a new file using the specified format and options
                diagram.Save("output.vsdx", saveOptions);
                Console.WriteLine("Diagram saved successfully.");
            }
            catch (DiagramException dex)
            {
                // Log Aspose.Diagram specific errors
                Console.Error.WriteLine($"DiagramException occurred while saving: {dex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors
                Console.Error.WriteLine($"Unexpected error occurred while saving: {ex.Message}");
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
