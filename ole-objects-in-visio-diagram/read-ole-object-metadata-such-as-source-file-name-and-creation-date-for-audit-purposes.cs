using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is an OLE object
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Source file name (full path) of the embedded OLE object
                        string sourceFile = shape.ForeignData.ObjectSourceFullName ?? "N/A";

                        // Attempt to retrieve creation date if the source file exists on disk
                        string creationInfo;
                        if (!string.IsNullOrEmpty(sourceFile) && File.Exists(sourceFile))
                        {
                            DateTime creationTime = File.GetCreationTime(sourceFile);
                            creationInfo = creationTime.ToString("u");
                        }
                        else
                        {
                            creationInfo = "Creation date unavailable (source file not found)";
                        }

                        // Output audit information
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}");
                        Console.WriteLine($"  OLE Source File: {sourceFile}");
                        Console.WriteLine($"  Creation Date : {creationInfo}");
                    }
                }
            }

            // Optionally save the diagram after any processing (not required for audit)
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
