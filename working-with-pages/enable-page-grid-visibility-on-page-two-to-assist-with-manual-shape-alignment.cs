using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the source Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the source file.
            Diagram diagram = new Diagram(inputPath);

            // Verify that a second page (index 1) is present.
            if (diagram.Pages.Count < 2)
            {
                Console.Error.WriteLine("The diagram does not contain a second page.");
                return;
            }

            // Retrieve the second page (zero‑based index).
            Page pageTwo = diagram.Pages[1];

            // Locate the window associated with the second page.
            Window pageWindow = null;
            foreach (Window win in diagram.Windows)
            {
                // Window.Page returns a Page object; compare its ID with pageTwo.ID.
                if (win.Page != null && win.Page.ID == pageTwo.ID)
                {
                    pageWindow = win;
                    break;
                }
            }

            if (pageWindow == null)
            {
                Console.Error.WriteLine("No window found for page two.");
                return;
            }

            // Enable grid visibility for the identified window.
            pageWindow.ShowGrid = BOOL.True;

            // Save the modified diagram to the output file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}