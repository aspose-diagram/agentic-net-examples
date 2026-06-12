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

            try
            {
                // Create a new Window instance
                Window newWindow = new Window();

                // Set required properties (example: a drawing window)
                newWindow.WindowType = WindowTypeValue.Drawing;

                // Attempt to add the window to the diagram's WindowCollection
                // This may throw a DiagramException if the collection is null or invalid
                diagram.Windows.Add(newWindow);

                Console.WriteLine("Window added successfully.");
            }
            catch (DiagramException dex)
            {
                // Handle Aspose.Diagram specific errors
                Console.WriteLine($"DiagramException caught: {dex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
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
