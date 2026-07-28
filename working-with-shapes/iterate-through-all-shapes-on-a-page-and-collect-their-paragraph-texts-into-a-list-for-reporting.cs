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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Select the first page (or any specific page you need)
            Page page = diagram.Pages[0];

            // List to store paragraph texts from all shapes on the page
            List<string> paragraphTexts = new List<string>();

            // Iterate through each shape on the page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the plain text of the shape (includes all paragraphs)
                string pureText = shape.GetPureText();

                // If the shape contains text, add it to the collection
                if (!string.IsNullOrEmpty(pureText))
                {
                    paragraphTexts.Add(pureText);
                }
            }

            // Example reporting: output the collected paragraph texts
            Console.WriteLine("Collected Paragraph Texts:");
            foreach (string text in paragraphTexts)
            {
                Console.WriteLine("- " + text);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
