using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;

public class DiagramAutoSpacer
{
    /// <summary>
    /// Performs auto‑spacing of the specified shapes on a given page asynchronously,
    /// so the UI thread remains responsive.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <param name="pageIndex">Zero‑based index of the page containing the shapes.</param>
    /// <param name="shapes">The collection of shapes to be auto‑spaced.</param>
    /// <param name="horizontalInches">Desired horizontal spacing in inches.</param>
    /// <param name="verticalInches">Desired vertical spacing in inches.</param>
    /// <returns>A task that completes when the auto‑spacing operation finishes.</returns>
    public async Task AutoSpaceAsync(Diagram diagram, int pageIndex, ShapeCollection shapes, double horizontalInches, double verticalInches)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));
        if (shapes == null) throw new ArgumentNullException(nameof(shapes));
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");

        // Configure spacing options.
        var options = new AutoSpaceOptions
        {
            DistanceInHorizontal = horizontalInches,
            DistanceInVertical = verticalInches
        };

        // Run the synchronous AutoSpaceShapes method on a background thread.
        await Task.Run(() =>
        {
            // Access the target page.
            Page page = diagram.Pages[pageIndex];

            // Perform auto‑spacing.
            page.AutoSpaceShapes(shapes, options);
        }).ConfigureAwait(false);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var obj = new DiagramAutoSpacer();
        obj.AutoSpaceAsync(null, 0, null, 0, 0);
    }
}
