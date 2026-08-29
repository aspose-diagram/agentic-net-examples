using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (VSDX, VSD, etc.)
            string visioPath = "input.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate through all pages in the document
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");

                        // Retrieve all user‑defined cells (User section) of the shape
                        foreach (User userCell in shape.Users)
                        {
                            // Output the name of the user cell and its evaluated value
                            Console.WriteLine($"    User Cell - Name: {userCell.Name}, Value: {userCell.Value}");
                        }
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
