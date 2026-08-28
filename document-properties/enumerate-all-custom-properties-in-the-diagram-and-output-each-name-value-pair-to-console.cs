using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (replace with your actual file path)
            string filePath = "input.vsdx";
            using (Diagram diagram = new Diagram(filePath))
            {
                // Get the collection of custom properties defined at the document level
                var customProps = diagram.DocumentProps.CustomProps;

                // Enumerate each custom property and output its name and value
                foreach (CustomProp prop in customProps)
                {
                    Console.WriteLine($"Name: {prop.Name}, Value: {prop.CustomValue}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
