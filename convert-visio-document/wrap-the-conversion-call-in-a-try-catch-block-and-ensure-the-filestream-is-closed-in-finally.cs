using System;
using System.IO;
using Aspose.Diagram;

class DiagramConversion
{
    static void Main()
    {
        // Paths to the source VSD file and the target VDW file
        string inputPath = @"C:\Docs\source.vsd";
        string outputPath = @"C:\Docs\target.vdw";

        // Declare the streams outside the try block so they are visible in finally
        FileStream inputStream = null;
        FileStream outputStream = null;

        try
        {
            // Open the input and output streams
            inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

            // Perform the conversion using Aspose.Diagram
            Diagram.Export(inputStream, outputStream);
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during conversion
            Console.WriteLine("An error occurred during diagram conversion:");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // Ensure both streams are closed even if an exception occurs
            if (outputStream != null)
            {
                try
                {
                    outputStream.Close();
                }
                catch { /* ignore secondary errors */ }
            }

            if (inputStream != null)
            {
                try
                {
                    inputStream.Close();
                }
                catch { /* ignore secondary errors */ }
            }
        }
    }
}
