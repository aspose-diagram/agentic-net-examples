using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the built‑in Title property from the document properties
            string title = diagram.DocumentProps.Title;

            // Define the expected title template for comparison
            const string templateTitle = "My Template Title";

            // Compare the actual title with the template and handle the result
            if (title == templateTitle)
            {
                Console.WriteLine("Title matches the template.");
            }
            else
            {
                Console.WriteLine($"Title mismatch. Diagram title: \"{title}\"");
                throw new Exception("Diagram title does not match the expected template.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
