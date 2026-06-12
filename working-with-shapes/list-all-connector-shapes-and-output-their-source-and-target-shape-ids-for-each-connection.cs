using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD)
                    {
                        long sourceId = -1;
                        long targetId = -1;

                        foreach (Connect conn in page.Connects)
                        {
                            if (conn.ToSheet == shape.ID)
                                sourceId = conn.FromSheet;
                            else if (conn.FromSheet == shape.ID)
                                targetId = conn.ToSheet;
                        }

                        Console.WriteLine($"Connector ID: {shape.ID}, Source Shape ID: {sourceId}, Target Shape ID: {targetId}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}