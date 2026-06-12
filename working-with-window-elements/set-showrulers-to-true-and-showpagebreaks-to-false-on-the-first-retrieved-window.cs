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
            if (diagram.Windows.Count > 0)
            {
                // Retrieve the first window in the collection
                Window firstWindow = diagram.Windows[0];

                // Set the required visibility options
                firstWindow.ShowRulers = BOOL.True;        // Show rulers
                firstWindow.ShowPageBreaks = BOOL.False;   // Hide page breaks

                // Save the diagram to persist the changes (optional)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
            else
            {
                Console.WriteLine("The diagram does not contain any windows.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
