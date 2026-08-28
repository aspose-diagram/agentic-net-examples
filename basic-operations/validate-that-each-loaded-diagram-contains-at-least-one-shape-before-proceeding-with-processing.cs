using System.IO;
using System;
using Aspose.Diagram;

class DiagramProcessor
{
    public static void ProcessDiagram(string inputPath, string outputPath)
    {
        // Load the diagram from the specified file (lifecycle rule: load)
        Diagram diagram = new Diagram(inputPath);

        // Validate that the diagram contains at least one shape on any page
        bool containsShape = false;
        foreach (Page page in diagram.Pages)
        {
            if (page.Shapes.Count > 0)
            {
                containsShape = true;
                break;
            }
        }

        if (!containsShape)
        {
            // No shapes found – abort processing
            throw new InvalidOperationException("The loaded diagram does not contain any shapes.");
        }

        // At this point the diagram is valid; continue with processing
        // Example operation: refresh all data record sets
        diagram.Refresh();

        // Save the (potentially modified) diagram (lifecycle rule: save)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramProcessor.ProcessDiagram("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
