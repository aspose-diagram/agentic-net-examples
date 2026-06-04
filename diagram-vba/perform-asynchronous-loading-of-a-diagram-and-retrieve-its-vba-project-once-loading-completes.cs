using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

public class DiagramVbaLoader
{
    // Asynchronously loads a Visio diagram and returns its VBA project
    public async Task<VbaProject> LoadVbaProjectAsync(string filePath)
    {
        // Perform the loading on a background thread to avoid blocking the caller
        Diagram diagram = await Task.Run(() =>
        {
            // Prepare load options; you can set the format based on the file extension
            var loadOptions = new LoadOptions();

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            switch (ext)
            {
                case ".vsdx":
                    loadOptions.LoadFormat = LoadFileFormat.Vsdx;
                    break;
                case ".vsd":
                    loadOptions.LoadFormat = LoadFileFormat.Vsd;
                    break;
                case ".vsdm":
                    loadOptions.LoadFormat = LoadFileFormat.Vsdm;
                    break;
                // Add other formats as needed
                default:
                    // Keep default (VSD) if format is unknown
                    break;
            }

            // Load the diagram using the file path and the configured options
            return new Diagram(filePath, loadOptions);
        });

        // Retrieve the VBA project from the loaded diagram
        VbaProject vbaProject = diagram.VbaProject;

        // Clean up the diagram object
        diagram.Dispose();

        return vbaProject;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
