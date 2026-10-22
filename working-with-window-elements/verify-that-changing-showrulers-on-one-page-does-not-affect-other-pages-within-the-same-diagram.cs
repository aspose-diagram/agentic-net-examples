using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least two pages for the verification scenario.
                if (diagram.Pages.Count < 2)
                {
                    Console.WriteLine("The diagram contains fewer than two pages; verification is not applicable.");
                    return;
                }

                // Ensure there is at least one window (view) to manipulate ShowRulers.
                if (diagram.Windows.Count == 0)
                {
                    Console.WriteLine("The diagram does not contain any windows; cannot test ShowRulers visibility.");
                    return;
                }

                // NOTE: ShowRulers is a property of Window, not Page.
                // Therefore, it is a global setting for the window view and cannot be set per page.
                // The following code toggles ShowRulers on the first window and logs the result.
                // TODO: If future API versions expose a per-page ShowRulers property, replace this logic
                // with page-specific assignments and verification that other pages remain unchanged.

                // Store the original ShowRulers value.
                BOOL originalShowRulers = diagram.Windows[0].ShowRulers;

                // Toggle the ShowRulers setting.
                diagram.Windows[0].ShowRulers = originalShowRulers == BOOL.True ? BOOL.False : BOOL.True;

                // Verify that the window's ShowRulers value has been updated.
                if (diagram.Windows[0].ShowRulers == originalShowRulers)
                {
                    throw new Exception("Verification failed: ShowRulers value did not change on the window.");
                }

                Console.WriteLine("ShowRulers toggled on the diagram's window successfully.");

                // Since ShowRulers is not page-specific, we cannot verify isolation between pages.
                // The verification step is therefore omitted.

                // Optional: save the modified diagram to demonstrate persistence of the change.
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "ModifiedDiagram.vsdx");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}