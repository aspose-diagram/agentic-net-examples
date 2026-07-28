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

            // Create a new Window instance and set basic properties
            Window newWindow = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowWidth = 800,
                WindowHeight = 600
            };

            try
            {
                // Verify that the diagram actually has a WindowCollection
                if (diagram.Windows == null)
                    throw new DiagramException("The diagram does not contain a WindowCollection.");

                // Add the window to the collection
                diagram.Windows.Add(newWindow);
            }
            catch (DiagramException dex)
            {
                // Handle Aspose.Diagram specific errors
                Console.WriteLine($"DiagramException caught: {dex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                Console.WriteLine($"Unexpected exception: {ex.Message}");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
