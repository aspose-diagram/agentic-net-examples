using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Create a new empty diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one window; add one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowType = WindowTypeValue.Drawing;
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 1100;
                    window.WindowHeight = 700;
                    diagram.Windows.Add(window);
                }

                // Get the active page (a new diagram contains a default page)
                Page page = diagram.ActivePage;

                // Add a text shape at PinX=2, PinY=3 with width=2, height=1 and sample content
                // The AddText method returns a Shape instance representing the created text shape
                Shape textShape = page.AddText(2.0, 3.0, 2.0, 1.0, "Sample Text");

                // (Optional) Additional shape configuration can be done here, e.g., setting fill or line styles

                // Save the diagram to the specified file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console as required
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}