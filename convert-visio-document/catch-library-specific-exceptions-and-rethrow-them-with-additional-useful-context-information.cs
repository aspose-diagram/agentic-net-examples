using System;
using System.IO;
using Aspose.Diagram;

public class DiagramProcessor
{
    public void ProcessDiagram(string filePath)
    {
        try
        {
            // Load the diagram (lifecycle operation)
            Diagram diagram = new Diagram(filePath);

            // TODO: Insert diagram manipulation logic here
            // Example: diagram.Validate();
        }
        catch (DiagramException ex)
        {
            // Capture Aspose.Diagram specific exception,
            // enrich it with additional context, and rethrow.
            throw new DiagramException($"Error processing diagram '{filePath}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramProcessor();
            obj.ProcessDiagram("");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
