using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        byte[] visioData = File.ReadAllBytes(visioPath);
        if (visioData.Length == 0)
        {
            Console.Error.WriteLine("Visio file is empty.");
            return;
        }

        try
        {
            using (MemoryStream diagramStream = new MemoryStream(visioData))
            {
                Diagram diagram = new Diagram(diagramStream);

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                        {
                            if (shape.ForeignData.ForeignType == ForeignType.Object && shape.ForeignData.ObjectData != null)
                            {
                                using (MemoryStream oleStream = new MemoryStream(shape.ForeignData.ObjectData))
                                {
                                    Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, OLE Data Size: {oleStream.Length} bytes");
                                    // Additional OLE processing can be added here.
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }
}