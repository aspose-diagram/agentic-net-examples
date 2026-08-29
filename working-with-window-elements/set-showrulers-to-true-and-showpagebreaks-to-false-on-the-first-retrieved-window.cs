using System.IO;
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

            // Verify that the diagram contains at least one window
            if (diagram.Windows.Count == 0)
            {
                Console.WriteLine("The diagram does not contain any windows.");
                return;
            }

            // Retrieve the first window in the collection
            Window window = diagram.Windows[0];

            // Set the required visibility options
            window.ShowRulers = BOOL.True;        // Show rulers
            window.ShowPageBreaks = BOOL.False;   // Hide page breaks

            // Save the modified diagram (optional)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Window settings have been updated and the diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
