using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");

                    // Retrieve all user‑defined cells (User collection) for the shape
                    foreach (User userCell in shape.Users)
                    {
                        // Output the cell name and its value
                        Console.WriteLine($"    User Cell: {userCell.NameU} = {userCell.Value}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
