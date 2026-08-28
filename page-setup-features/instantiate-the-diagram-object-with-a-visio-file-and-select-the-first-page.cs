using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx"); // replace with your file path

            // Select the first page in the document
            var firstPage = diagram.Pages[0];

            // Example usage: output the name of the first page
            Console.WriteLine("First page name: " + firstPage.Name);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
