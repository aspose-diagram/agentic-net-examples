using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram that contains the desired theme
            using (Diagram sourceDiagram = new Diagram("source.vsdx"))
            {
                // Create a new diagram (or load another one) to which the theme will be applied
                using (Diagram targetDiagram = new Diagram())
                {
                    // Apply the theme from the source diagram
                    targetDiagram.CopyTheme(sourceDiagram);

                    // Prepare a memory stream for saving the diagram without touching the disk
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Save the diagram into the memory stream in VDX format
                        targetDiagram.Save(memoryStream, SaveFileFormat.Vdx);

                        // Reset the stream position if it will be read afterwards
                        memoryStream.Position = 0;

                        // The diagram data is now available in memoryStream (e.g., as a byte array)
                        byte[] diagramBytes = memoryStream.ToArray();
                        // Further processing can be done with diagramBytes here
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
