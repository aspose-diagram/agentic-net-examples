using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

public class DiagramHelper
{
    // Asynchronously loads a Visio diagram and returns its VBA project.
    public async Task<VbaProject> LoadDiagramAndGetVbaProjectAsync(string filePath)
    {
        return await Task.Run(() =>
        {
            // Prepare load options; set format based on file extension if needed.
            var loadOptions = new LoadOptions
            {
                LoadFormat = Path.GetExtension(filePath).ToLower() switch
                {
                    ".vsdx" => LoadFileFormat.Vsdx,
                    ".vsdm" => LoadFileFormat.Vsdm,
                    ".vsd"  => LoadFileFormat.Vsd,
                    ".vdx"  => LoadFileFormat.Vdx,
                    ".vtx"  => LoadFileFormat.Vtx,
                    ".vst"  => LoadFileFormat.Vst,
                    ".vdw"  => LoadFileFormat.Vdw,
                    ".vss"  => LoadFileFormat.Vss,
                    ".vssx" => LoadFileFormat.Vssx,
                    ".vstx" => LoadFileFormat.Vstx,
                    ".vstm" => LoadFileFormat.Vstm,
                    ".vssm" => LoadFileFormat.Vssm,
                    _       => LoadFileFormat.Vsd // default fallback
                }
            };

            // Load the diagram using the constructor that accepts a file name and LoadOptions.
            var diagram = new Diagram(filePath, loadOptions);

            // Retrieve and return the VBA project associated with the loaded diagram.
            return diagram.VbaProject;
        });
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
