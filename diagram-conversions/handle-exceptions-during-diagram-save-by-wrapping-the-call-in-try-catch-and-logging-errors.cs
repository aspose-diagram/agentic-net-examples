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

            // Load an existing diagram (using the provided load rule)
            string inputFile = "input.vsdx";
            Diagram diagram = new Diagram(inputFile);

            // Prepare save options (using the provided save rule)
            var saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            // Example: set additional options if needed
            // saveOptions.AutoFitPageToDrawingContent = true;

            // Save the diagram with exception handling
            try
            {
                diagram.Save("output.vsdx", saveOptions);
            }
            catch (DiagramException ex)
            {
                // Log the error
                Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            }
            finally
            {
                // Ensure resources are released (using the provided lifecycle rule)
                diagram.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
