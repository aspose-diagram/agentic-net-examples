using System;
using System.IO;
using Aspose.Diagram;

class OleSizeFilter
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path to the output Visio file
            string outputPath = "output_filtered.vsdx";

            // Size limit in megabytes (e.g., 5 MB)
            const double sizeLimitMb = 5.0;
            long sizeLimitBytes = (long)(sizeLimitMb * 1024 * 1024);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate backwards through shapes to allow safe removal
                for (int i = page.Shapes.Count - 1; i >= 0; i--)
                {
                    Shape shape = page.Shapes[i];

                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Size of the embedded OLE object in bytes
                        long oleSize = shape.ForeignData.ObjectData.Length;

                        // If the OLE object exceeds the size limit, remove the shape
                        if (oleSize > sizeLimitBytes)
                        {
                            // Optionally, log the removal
                            Console.WriteLine($"Removing shape ID {shape.ID} (OLE size: {oleSize / (1024 * 1024.0):F2} MB) from page \"{page.Name}\".");

                            // Remove the shape from the page
                            page.Shapes.RemoveAt(i);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Processing complete. Filtered file saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
