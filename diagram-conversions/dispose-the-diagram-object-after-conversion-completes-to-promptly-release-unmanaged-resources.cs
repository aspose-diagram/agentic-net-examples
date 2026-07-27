using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramConversion
{
    public static void Convert(string inputFile, string outputFile)
    {
        // Load the Visio diagram from the specified file
        Diagram diagram = new Diagram(inputFile);
        try
        {
            // Example conversion step: remove any macros from the diagram
            diagram.RemoveMacro();

            // Save the diagram in VDX format to the output file
            diagram.Save(outputFile, SaveFileFormat.Vdx);
        }
        finally
        {
            // Dispose the Diagram object to release unmanaged resources promptly
            diagram.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramConversion.Convert("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
