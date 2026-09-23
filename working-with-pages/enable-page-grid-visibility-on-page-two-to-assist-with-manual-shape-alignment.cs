using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Verify that the diagram has at least two pages (page index is zero‑based)
                if (diagram.Pages.Count < 2)
                    throw new Exception("The diagram does not contain a second page.");

                // Retrieve the second page (page two)
                Page pageTwo = diagram.Pages[1];

                // Ensure there is at least one window; if not, create a default drawing window
                if (diagram.Windows.Count == 0)
                {
                    Window newWindow = new Window
                    {
                        WindowType = WindowTypeValue.Drawing,
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 1100,
                        WindowHeight = 700
                    };
                    diagram.Windows.Add(newWindow);
                }

                // Enable the grid visibility for the diagram UI.
                // This setting is applied via the first window; it affects all pages,
                // including the second page where manual alignment will be performed.
                Window window = diagram.Windows[0];
                window.ShowGrid = BOOL.True;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }