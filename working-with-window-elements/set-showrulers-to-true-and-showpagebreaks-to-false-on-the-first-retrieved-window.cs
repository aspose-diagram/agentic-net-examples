using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

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

                // Retrieve the first window
                Window firstWindow = diagram.Windows[0];

                // Set ShowRulers to true and ShowPageBreaks to false
                firstWindow.ShowRulers = BOOL.True;
                firstWindow.ShowPageBreaks = BOOL.False;

                // Optionally save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }