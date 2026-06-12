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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // List to store paragraph texts from all shapes
            List<string> paragraphTexts = new List<string>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape (includes all paragraphs)
                    string text = shape.GetPureText();

                    // Add non‑empty texts to the collection
                    if (!string.IsNullOrEmpty(text))
                    {
                        paragraphTexts.Add(text);
                    }
                }
            }

            // Example reporting: write collected texts to console
            foreach (string txt in paragraphTexts)
            {
                Console.WriteLine(txt);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
