using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the second page (zero‑based index, so index 1 is page two)
            Page pageTwo = diagram.Pages[1];

            // Locate a window that is linked to page two
            Window targetWindow = null;
            foreach (Window win in diagram.Windows)
            {
                // Window.Page returns a Page object; compare its ID with pageTwo.ID
                if (win.Page != null && win.Page.ID == pageTwo.ID)
                {
                    targetWindow = win;
                    break;
                }
            }

            if (targetWindow != null)
            {
                // Enable grid visibility on the existing window
                targetWindow.ShowGrid = BOOL.True;
            }
            else
            {
                // No window linked to page two – create a new one and enable the grid
                Window newWindow = new Window
                {
                    Page = pageTwo,          // Associate the window with page two
                    ShowGrid = BOOL.True     // Turn on grid visibility
                };
                diagram.Windows.Add(newWindow);
            }

            // Save the modified diagram to a new file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}