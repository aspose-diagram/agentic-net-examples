using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;

public class VisioHelper
{
    // Asynchronously loads a Visio file into a Diagram object.
    public static async Task<Diagram> LoadDiagramAsync(string filePath)
    {
        // Open the file stream for asynchronous reading.
        await using var fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 81920,
            useAsync: true);

        // Copy the file stream into a memory stream asynchronously.
        var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);
        memoryStream.Position = 0; // Reset position for reading.

        // Load the diagram from the memory stream.
        // Using the constructor that accepts a Stream.
        var diagram = new Diagram(memoryStream);
        return diagram;
    }

    // Asynchronously updates window properties of a Diagram.
    public static async Task UpdateWindowPropertiesAsync(Diagram diagram, double viewScale, double viewCenterX, double viewCenterY)
    {
        // The Windows collection may contain multiple windows.
        // Updating each window is a quick operation, but we wrap it in Task.Run
        // to keep the method asynchronous and non‑blocking.
        await Task.Run(() =>
        {
            foreach (Window window in diagram.Windows)
            {
                // Example properties to update.
                window.ViewScale = viewScale;
                window.ViewCenterX = viewCenterX;
                window.ViewCenterY = viewCenterY;

                // Additional optional updates can be added here, e.g.:
                // window.ShowGrid = true;
                // window.WindowState = 1; // Normal state.
            }
        }).ConfigureAwait(false);
    }

    // Asynchronously saves the modified Diagram to a file.
    public static async Task SaveDiagramAsync(Diagram diagram, string outputPath)
    {
        // Save to a memory stream first to avoid blocking the file system.
        await Task.Run(() =>
        {
            using var outStream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            // Use the Save method that accepts a Stream.
            diagram.Save(outStream, SaveFileFormat.Vsdx);
        }).ConfigureAwait(false);
    }

    // Example usage combining the above methods.
    public static async Task ProcessVisioFileAsync(string inputPath, string outputPath)
    {
        Diagram diagram = await LoadDiagramAsync(inputPath);
        await UpdateWindowPropertiesAsync(diagram, viewScale: 1.5, viewCenterX: 5.0, viewCenterY: 5.0);
        await SaveDiagramAsync(diagram, outputPath);
        diagram.Dispose();
    }
}

class Program
{
    static void Main(string[] args)
    {
        VisioHelper.UpdateWindowPropertiesAsync(null, 0, 0, 0);
    }
}
