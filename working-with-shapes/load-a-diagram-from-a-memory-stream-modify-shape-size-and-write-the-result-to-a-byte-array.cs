using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Input Visio diagram as a byte array.
        byte[] inputVisioBytes = GetInputBytes();

        if (inputVisioBytes == null || inputVisioBytes.Length == 0)
        {
            Console.Error.WriteLine("Input byte array is empty.");
            return;
        }

        try
        {
            // Load the diagram from a memory stream.
            using (MemoryStream inputStream = new MemoryStream(inputVisioBytes))
            {
                Diagram diagram = new Diagram(inputStream);

                // Access the first page.
                Page page = diagram.Pages[0];

                // Modify the size of the first non‑deleted shape.
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        // Set new width and height (in inches).
                        shape.XForm.Width.Value = 2.0;   // example width
                        shape.XForm.Height.Value = 1.0;  // example height
                        break;
                    }
                }

                // Save the modified diagram to a byte array (using VDX format as an example).
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vdx);
                    byte[] resultBytes = outputStream.ToArray();

                    // Demonstrate that the result was produced.
                    Console.WriteLine($"Resulting diagram size: {resultBytes.Length} bytes");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Placeholder method to obtain the input byte array.
    static byte[] GetInputBytes()
    {
        // Replace this with actual logic to retrieve the diagram bytes.
        return new byte[0];
    }
}