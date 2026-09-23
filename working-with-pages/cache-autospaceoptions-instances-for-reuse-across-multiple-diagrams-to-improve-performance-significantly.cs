using System;
using Aspose.Diagram;

class AutoSpaceOptionsCache
{
    // Single shared instance of AutoSpaceOptions for reuse
    private static readonly AutoSpaceOptions _sharedOptions = CreateOptions();

    private static AutoSpaceOptions CreateOptions()
    {
        var options = new AutoSpaceOptions();
        // Configure desired spacing distances (in inches)
        options.DistanceInHorizontal = 2;
        options.DistanceInVertical = 2;
        return options;
    }

    // Public accessor to retrieve the cached options
    public static AutoSpaceOptions Get()
    {
        return _sharedOptions;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            // Paths to source Visio files
            string diagramPath1 = "diagram1.vsdx";
            string diagramPath2 = "diagram2.vsdx";

            // Load diagrams
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Apply AutoSpaceShapes using the cached AutoSpaceOptions instance
            foreach (Page page in diagram1.Pages)
            {
                page.AutoSpaceShapes(page.Shapes, AutoSpaceOptionsCache.Get());
            }

            foreach (Page page in diagram2.Pages)
            {
                page.AutoSpaceShapes(page.Shapes, AutoSpaceOptionsCache.Get());
            }

            // Save the modified diagrams
            diagram1.Save("output1.vsdx", SaveFileFormat.Vsdx);
            diagram2.Save("output2.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}