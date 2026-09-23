using System;
using System.IO;
using Aspose.Diagram;

public class DiagramProcessor
{
    /// <summary>
    /// Loads a Visio diagram from a byte array, modifies the first page, and returns the updated diagram as a byte array.
    /// </summary>
    /// <param name="inputBytes">The input diagram in byte array form.</param>
    /// <returns>The modified diagram as a byte array.</returns>
    public static byte[] ProcessDiagram(byte[] inputBytes)
    {
        // Load the diagram from the input byte array using a MemoryStream (load rule)
        using (var inputStream = new MemoryStream(inputBytes))
        {
            // The Diagram constructor accepts a stream containing the Visio file.
            var diagram = new Diagram(inputStream);

            // ----- Apply changes to a page (example: rename the first page) -----
            if (diagram.Pages.Count > 0)
            {
                // Access the first page (index 0)
                var page = diagram.Pages[0];

                // Change the page name
                page.Name = "ModifiedPage";

                // Optionally, you can modify other page properties here.
                // e.g., page.Width = 10.0; page.Height = 8.5;
            }

            // Save the modified diagram to a new MemoryStream (save rule)
            using (var outputStream = new MemoryStream())
            {
                // Save in the same format as the original (VDX). Adjust SaveFileFormat if needed.
                diagram.Save(outputStream, SaveFileFormat.Vdx);

                // Return the resulting byte array
                return outputStream.ToArray();
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
