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

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Retrieve diagram‑level creation date (if needed for audit).
            DateTime diagramCreated = diagram.DocumentProps.TimeCreated;
            Console.WriteLine($"Diagram created on: {diagramCreated:u}");
            Console.WriteLine();

            // Iterate through all pages and shapes to find OLE (foreign) objects.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // An OLE object is represented by a non‑null ForeignData instance.
                    if (shape.ForeignData != null)
                    {
                        // Source file name/path of the embedded OLE object.
                        string sourceFile = shape.ForeignData.ObjectSourceFullName ?? "(unknown)";

                        // ShowAsIcon flag indicates whether the OLE is displayed as an icon.
                        BOOL showAsIcon = shape.ForeignData.ShowAsIcon;

                        // Output metadata for audit.
                        Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}");
                        Console.WriteLine($"  OLE Source File : {sourceFile}");
                        Console.WriteLine($"  Show As Icon   : {(showAsIcon == BOOL.True ? "True" : "False")}");

                        // Note: Per‑object creation date is not exposed by Aspose.Diagram.
                        // The diagram creation date is reported above for overall audit context.
                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}