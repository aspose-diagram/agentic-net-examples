using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Retrieve the current header font style (italic flag)
        BOOL currentItalic = diagram.HeaderFooter.HeaderFooterFont.Italic;
        Console.WriteLine($"Current Header Italic: {(currentItalic == BOOL.True ? "True" : "False")}");

        // Change the header font style to italic
        diagram.HeaderFooter.HeaderFooterFont.Italic = BOOL.True;

        // Verify the change by reading back the property
        BOOL updatedItalic = diagram.HeaderFooter.HeaderFooterFont.Italic;
        Console.WriteLine($"Updated Header Italic: {(updatedItalic == BOOL.True ? "True" : "False")}");

        // Throw an exception if the change was not applied
        if (updatedItalic != BOOL.True)
        {
            throw new Exception("Failed to set header font to italic.");
        }

        // Optional: save the diagram to verify persistence (not required by task)
        // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
