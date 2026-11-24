using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least two pages for the test
                if (diagram.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The diagram must contain at least two pages for this verification.");
                    return;
                }

                // Ensure there are at least two windows; if not, create additional windows
                while (diagram.Windows.Count < 2)
                {
                    Window newWindow = new Window
                    {
                        // Initialize the new window with default visibility settings
                        ShowRulers = BOOL.False,
                        ShowGuides = BOOL.False,
                        ShowGrid = BOOL.False,
                        ShowPageBreaks = BOOL.False,
                        // Assign a default window type and state
                        WindowType = WindowTypeValue.Drawing,
                        WindowState = WindowStateValue.Maximized,
                        WindowWidth = 800,
                        WindowHeight = 600
                    };
                    diagram.Windows.Add(newWindow);
                }

                // Store original ShowRulers values for all windows
                BOOL[] originalShowRulers = new BOOL[diagram.Windows.Count];
                for (int i = 0; i < diagram.Windows.Count; i++)
                {
                    originalShowRulers[i] = diagram.Windows[i].ShowRulers;
                }

                // Toggle ShowRulers on the first window (which typically displays the first page)
                Window targetWindow = diagram.Windows[0];
                targetWindow.ShowRulers = (targetWindow.ShowRulers == BOOL.True) ? BOOL.False : BOOL.True;

                // Verify that other windows retain their original ShowRulers values
                bool verificationPassed = true;
                for (int i = 0; i < diagram.Windows.Count; i++)
                {
                    if (i == 0)
                    {
                        // Ensure the target window's value was actually toggled
                        if (diagram.Windows[i].ShowRulers == originalShowRulers[i])
                        {
                            Console.Error.WriteLine($"Failed to toggle ShowRulers on window index {i}.");
                            verificationPassed = false;
                        }
                    }
                    else
                    {
                        // For all other windows, the value should remain unchanged
                        if (diagram.Windows[i].ShowRulers != originalShowRulers[i])
                        {
                            Console.Error.WriteLine($"ShowRulers changed on window index {i} unexpectedly.");
                            verificationPassed = false;
                        }
                    }
                }

                // Output the verification result
                if (verificationPassed)
                {
                    Console.WriteLine("Verification succeeded: Changing ShowRulers on one window does not affect other windows (and thus other pages).");
                }
                else
                {
                    throw new Exception("Verification failed: ShowRulers property inconsistency detected across windows.");
                }

                // Optional: Save the modified diagram to a new file to persist the change
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? "",
                    Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}