using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files.
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                try
                {
                    // Load the diagram from the specified file.
                    Diagram diagram = new Diagram(inputPath);

                    // Ensure there is at least one window; if not, create a default drawing window.
                    if (diagram.Windows.Count == 0)
                    {
                        Window defaultWindow = new Window
                        {
                            // Set the window type to a drawing window.
                            WindowType = WindowTypeValue.Drawing,
                            // Maximize the window for a typical default view.
                            WindowState = WindowStateValue.Maximized,
                            // Provide a reasonable size.
                            WindowWidth = 1100,
                            WindowHeight = 700
                        };
                        diagram.Windows.Add(defaultWindow);
                    }

                    // Restore default visibility settings for each window.
                    foreach (Window window in diagram.Windows)
                    {
                        // Show grid, guides, rulers, and page breaks.
                        window.ShowGrid = BOOL.True;
                        window.ShowGuides = BOOL.True;
                        window.ShowRulers = BOOL.True;
                        window.ShowPageBreaks = BOOL.True;
                    }

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved with default window settings to '{outputPath}'.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during processing.
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }