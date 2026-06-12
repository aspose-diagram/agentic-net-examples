using System.IO;
using System;
using Aspose.Diagram;

class ReplacePlaceholder
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (use the provided load rule)
            var diagram = new Diagram("input.vsdx");

            // Define the actual user name to replace the placeholder with
            string userName = "John Doe";

            // Iterate through all shapes on the active page
            foreach (Shape shape in diagram.ActivePage.Shapes)
            {
                // Replace the placeholder text "[Name]" with the actual user name
                shape.ReplaceText("[Name]", userName);
                // Refresh shape data to update its layout after text change
                shape.RefreshData();
            }

            // Save the modified diagram to a new file (use the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
