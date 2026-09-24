using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string filePath = "input.vsdx";
            Diagram diagram = new Diagram(filePath);

            // Access the first page (you can change the index to target a different page)
            Page page = diagram.Pages[0];

            // List to store the paragraph texts from each shape
            List<string> paragraphTexts = new List<string>();

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the plain text of the shape
                string text = shape.Text.Value.Text;

                // Add non‑empty text entries to the list
                if (!string.IsNullOrWhiteSpace(text))
                {
                    paragraphTexts.Add(text);
                }
            }

            // Output the collected texts for reporting
            Console.WriteLine("Collected paragraph texts:");
            foreach (string txt in paragraphTexts)
            {
                Console.WriteLine(txt);
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
