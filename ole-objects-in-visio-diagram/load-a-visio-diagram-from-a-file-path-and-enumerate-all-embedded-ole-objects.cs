using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(visioPath))
            {
                Console.WriteLine($"Loaded diagram: {visioPath}");
                Console.WriteLine("Enumerating embedded OLE objects...");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // An OLE object is represented by non‑null ForeignData with binary content.
                        if (shape.ForeignData != null &&
                            shape.ForeignData.ObjectData != null &&
                            shape.ForeignData.ObjectData.Length > 0)
                        {
                            // Output basic information about the OLE object.
                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine($"Page ID   : {page.ID}");
                            Console.WriteLine($"Shape ID  : {shape.ID}");
                            Console.WriteLine($"Shape Name: {shape.NameU}");
                            Console.WriteLine($"OLE Source: {shape.ForeignData.ObjectSourceFullName}");
                            Console.WriteLine($"OLE Size  : {shape.ForeignData.ObjectData.Length} bytes");
                        }
                    }
                }

                Console.WriteLine("Enumeration completed.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}