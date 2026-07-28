using System.IO;
using System;
using System.Threading.Tasks;
using Aspose.Diagram;

public static class DiagramAutoSpaceHelper
{
    /// <summary>
    /// Performs auto‑spacing of the specified shapes on the given page asynchronously.
    /// The operation is executed on a background thread to avoid blocking the UI thread.
    /// </summary>
    /// <param name="diagram">The Aspose.Diagram.Diagram instance containing the page.</param>
    /// <param name="pageIndex">Zero‑based index of the page on which to auto‑space shapes.</param>
    /// <param name="shapes">The collection of shapes to be auto‑spaced.</param>
    /// <param name="options">Auto‑spacing options (horizontal and vertical distances).</param>
    /// <returns>A task that completes when the auto‑spacing operation finishes.</returns>
    public static async Task AutoSpaceAsync(Diagram diagram, int pageIndex, ShapeCollection shapes, AutoSpaceOptions options)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));
        if (shapes == null) throw new ArgumentNullException(nameof(shapes));
        if (options == null) throw new ArgumentNullException(nameof(options));
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index is out of range.");

        // Run the synchronous AutoSpaceShapes method on a thread‑pool thread.
        await Task.Run(() =>
        {
            // Access the target page.
            Page page = diagram.Pages[pageIndex];

            // Perform auto‑spacing using the provided shapes and options.
            page.AutoSpaceShapes(shapes, options);
        }).ConfigureAwait(false);
    }
}

// Example usage (e.g., from a UI event handler):
// async void OnAutoSpaceButtonClick(object sender, EventArgs e)
// {
//     // Assume diagram is already loaded.
//     var diagram = new Diagram("input.vsdx");
//
//     // Prepare shapes to auto‑space (e.g., all shapes on the first page).
//     ShapeCollection shapes = diagram.Pages[0].Shapes;
//
//     // Configure spacing options.
//     var options = new AutoSpaceOptions
//     {
//         DistanceInHorizontal = 0.5, // inches
//         DistanceInVertical = 0.5    // inches
//     };
//
//     // Perform auto‑spacing without blocking the UI.
//     await DiagramAutoSpaceHelper.AutoSpaceAsync(diagram, 0, shapes, options);
//
//     // Save the modified diagram.
//     diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
// }

class Program
{
    static void Main(string[] args)
    {
        DiagramAutoSpaceHelper.AutoSpaceAsync(null, 0, null, null);
    }
}
