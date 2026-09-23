using System;
using System.IO;
using Aspose.Diagram;

class DiagramConversion
{
    static void Main()
    {
        // Initialize a FileStream for the source Visio file
        FileStream fileStream = null;
        try
        {
            fileStream = new FileStream("source.vsdx", FileMode.Open, FileAccess.Read);
            
            // Load the Visio diagram from the FileStream (using the load rule)
            Diagram diagram = new Diagram(fileStream);
            
            // Perform the conversion (e.g., save as PDF)
            diagram.Save("output.pdf", SaveFileFormat.Pdf);
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or conversion
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        finally
        {
            // Ensure the FileStream is properly closed regardless of success or failure
            if (fileStream != null)
            {
                fileStream.Close();
            }
        }
    }
}
