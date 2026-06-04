using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (uses the provided load rule)
            var diagram = new Diagram("input.vsdx");

            // Validate that the diagram contains at least one shape
            bool hasShape = false;
            foreach (Page page in diagram.Pages)
            {
                if (page.Shapes.Count > 0)
                {
                    hasShape = true;
                    break;
                }
            }

            if (!hasShape)
            {
                Console.WriteLine("The diagram does not contain any shapes. Processing stopped.");
                diagram.Dispose();
                return;
            }

            // Proceed with further processing
            ProcessDiagram(diagram);

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ProcessDiagram(Diagram diagram)
    {
        // Add processing logic here
    }
}
