using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        FileStream inputStream = null;
        FileStream outputStream = null;

        try
        {
            // Open the source VSD file for reading
            inputStream = new FileStream("input.vsd", FileMode.Open, FileAccess.Read);
            // Create the destination VDW file for writing
            outputStream = new FileStream("output.vdw", FileMode.Create, FileAccess.Write);

            // Perform the export conversion
            Diagram.Export(inputStream, outputStream);
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the export
            Console.WriteLine($"Error during export: {ex.Message}");
        }
        finally
        {
            // Ensure both streams are closed regardless of success or failure
            if (outputStream != null)
                outputStream.Close();

            if (inputStream != null)
                inputStream.Close();
        }
    }
}
