using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToEmfMultiPage
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = @"C:\Input\diagram.vsdx";

            // Load the source Visio diagram
            Diagram sourceDiagram = new Diagram(sourceFile, LoadFileFormat.Vsdx);

            // Create a new empty diagram that will hold the combined pages
            Diagram combinedDiagram = new Diagram();

            // Combine the source diagram into the new diagram (all pages are merged)
            combinedDiagram.Combine(sourceDiagram);

            // Iterate through all pages and shapes in the source diagram
            foreach (Page page in sourceDiagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Export each shape to EMF format using a memory stream
                    using (MemoryStream emfStream = new MemoryStream())
                    {
                        // ImageSaveOptions with EMF format
                        ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Emf);
                        shape.ToImage(emfStream, options);

                        // At this point emfStream contains the EMF image of the shape.
                        // The stream can be saved to a file or processed further as needed.
                        // Example: save to a temporary file (optional)
                        // string tempEmfPath = Path.Combine(@"C:\Temp", $"{Guid.NewGuid()}.emf");
                        // File.WriteAllBytes(tempEmfPath, emfStream.ToArray());
                    }
                }
            }

            // Save the combined multi‑page diagram to a new Visio file
            string outputFile = @"C:\Output\combinedDiagram.vsdx";
            combinedDiagram.Save(outputFile, SaveFileFormat.Vsdx);

            // Clean up
            sourceDiagram.Dispose();
            combinedDiagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
