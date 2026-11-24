using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Ensure at least one file path is provided.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: Program <VisioFile1> <VisioFile2> ...");
            return;
        }

        // Iterate over each supplied file path.
        foreach (string filePath in args)
        {
            // Guard: verify the file exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram from the file.
                Diagram diagram = new Diagram(filePath);

                // Retrieve the document title from the built‑in properties.
                string title = diagram.DocumentProps.Title;

                // Set the left header text to the document title.
                diagram.HeaderFooter.HeaderLeft = title;

                // Save the updated diagram, overwriting the original file.
                diagram.Save(filePath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }
    }
}