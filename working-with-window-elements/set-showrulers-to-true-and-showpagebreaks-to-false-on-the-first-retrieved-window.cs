using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Ensure there is at least one window in the document
                if (diagram.Windows.Count > 0)
                {
                    // Retrieve the first window
                    Window firstWindow = diagram.Windows[0];

                    // Set ShowRulers to true and ShowPageBreaks to false
                    firstWindow.ShowRulers = BOOL.True;
                    firstWindow.ShowPageBreaks = BOOL.False;
                }
                else
                {
                    Console.WriteLine("No windows found in the diagram.");
                }

                // Save the modified diagram (replace with your desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }