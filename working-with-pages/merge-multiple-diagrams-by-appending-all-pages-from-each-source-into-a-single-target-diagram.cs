using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class DiagramMerger
{
    /// <summary>
    /// Merges multiple Visio diagrams into a single diagram by appending all pages.
    /// </summary>
    /// <param name="sourceFiles">Array of file paths to source diagrams.</param>
    /// <param name="targetFile">File path where the merged diagram will be saved.</param>
    public void Merge(string[] sourceFiles, string targetFile)
    {
        // Create an empty target diagram using the default constructor.
        Diagram targetDiagram = new Diagram();

        try
        {
            // Iterate over each source file, load it, and combine it with the target.
            foreach (string srcPath in sourceFiles)
            {
                // Load the source diagram from file.
                Diagram srcDiagram = new Diagram(srcPath);

                try
                {
                    // Append all pages from srcDiagram into targetDiagram.
                    targetDiagram.Combine(srcDiagram);
                }
                finally
                {
                    // Release resources of the source diagram.
                    srcDiagram.Dispose();
                }
            }

            // Save the merged diagram to the specified file in VDX format.
            targetDiagram.Save(targetFile, SaveFileFormat.Vdx);
        }
        finally
        {
            // Ensure the target diagram is properly disposed.
            targetDiagram.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramMerger();
            obj.Merge(null, "");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
