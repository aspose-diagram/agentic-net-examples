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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Select the page to process (e.g., the first page)
            Page page = diagram.Pages[0];

            // List to store individual paragraph texts from all shapes
            List<string> paragraphTexts = new List<string>();

            // Iterate through each shape on the selected page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the combined text of the shape (all paragraphs)
                string pureText = shape.GetPureText();

                // If the shape contains text, split it into separate paragraphs
                if (!string.IsNullOrEmpty(pureText))
                {
                    // Visio paragraphs are typically separated by line breaks
                    string[] paragraphs = pureText.Split(
                        new[] { "\r\n", "\n" },
                        StringSplitOptions.RemoveEmptyEntries);

                    // Add each paragraph to the collection
                    paragraphTexts.AddRange(paragraphs);
                }
            }

            // Example reporting: output collected paragraphs to the console
            foreach (string paragraph in paragraphTexts)
            {
                Console.WriteLine(paragraph);
            }

            // Optionally save the diagram after processing
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
