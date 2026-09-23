using System;
using System.IO;
using Aspose.Diagram;

public class VisioProcessor
{
    public void SaveChanges(string originalFilePath)
    {
        // Load the Visio diagram from the original file path
        Diagram diagram = new Diagram(originalFilePath);

        // Geometry modifications should be applied to 'diagram' before this point

        // Save the diagram back to the original file, persisting all changes
        diagram.Save(originalFilePath, SaveFileFormat.Vdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new VisioProcessor();
            obj.SaveChanges("");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
