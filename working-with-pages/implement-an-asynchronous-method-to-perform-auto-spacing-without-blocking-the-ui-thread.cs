using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;

public static class DiagramAutoSpaceHelper
{
    /// <summary>
    /// Performs auto‑spacing of the specified shapes on the given page asynchronously,
    /// so the UI thread remains responsive.
    /// </summary>
    /// <param name="page">The page containing the shapes to be auto‑spaced.</param>
    /// <param name="shapes">The collection of shapes to be spaced.</param>
    /// <param name="horizontalInches">Horizontal distance between shapes in inches.</param>
    /// <param name="verticalInches">Vertical distance between shapes in inches.</param>
    /// <returns>A task that completes when the auto‑spacing operation finishes.</returns>
    public static async Task AutoSpaceAsync(Page page, ShapeCollection shapes, double horizontalInches, double verticalInches)
    {
        // Prepare the options object with the desired spacing values.
        var options = new AutoSpaceOptions
        {
            DistanceInHorizontal = horizontalInches,
            DistanceInVertical = verticalInches
        };

        // Run the synchronous AutoSpaceShapes method on a background thread.
        await Task.Run(() => page.AutoSpaceShapes(shapes, options));
    }
}

class Program
{
    static void Main(string[] args)
    {
        DiagramAutoSpaceHelper.AutoSpaceAsync(null, null, 0, 0);
    }
}
