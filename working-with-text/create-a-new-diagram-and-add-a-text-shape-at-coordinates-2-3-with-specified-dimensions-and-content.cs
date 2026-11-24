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
            // Create a new empty diagram inside a using block
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has a window; add one if none exist
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowType = WindowTypeValue.Drawing;
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 1100;
                    window.WindowHeight = 700;
                    diagram.Windows.Add(window);
                }

                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a text shape at PinX=2, PinY=3 with width=2, height=1 and sample content
                double pinX = 2.0;
                double pinY = 3.0;
                double width = 2.0;
                double height = 1.0;
                string textContent = "Sample Text";

                // AddText returns a Shape object representing the created text shape
                Shape textShape = page.AddText(pinX, pinY, width, height, textContent);

                // (Optional) Additional configuration of the text shape can be done here
                // For example, set the text rotation angle if needed:
                // textShape.TextXForm.TxtAngle.Value = 0.0;

                // Save the diagram to the specified file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram created and saved successfully to " + outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}