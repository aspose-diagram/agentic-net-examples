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
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through all custom properties defined at the document level
                foreach (CustomProp customProp in diagram.DocumentProps.CustomProps)
                {
                    // Output the property name and its value to the console
                    Console.WriteLine($"{customProp.Name}: {customProp.CustomValue}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
