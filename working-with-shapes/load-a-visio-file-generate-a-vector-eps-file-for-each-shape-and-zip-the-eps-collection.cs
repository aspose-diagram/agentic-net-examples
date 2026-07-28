using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Folder where individual EPS files will be saved
            string epsFolder = "eps_output";

            // Path for the final ZIP archive
            string zipPath = "shapes_eps.zip";

            // Ensure the EPS output folder exists
            if (!Directory.Exists(epsFolder))
                Directory.CreateDirectory(epsFolder);

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options to use a vector format (EMF)
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Emf);

            int shapeIndex = 0;

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique file name for each shape
                    string epsFile = Path.Combine(
                        epsFolder,
                        $"shape_page{page.ID}_id{shape.ID}_{shapeIndex++}.eps");

                    // Export the shape to an EPS file (using EMF format)
                    shape.ToImage(epsFile, imgOptions);
                }
            }

            // Remove existing ZIP if it exists
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            // Create a ZIP archive containing all EPS files
            ZipFile.CreateFromDirectory(epsFolder, zipPath);

            Console.WriteLine($"Exported {shapeIndex} shapes to EPS files and created archive '{zipPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
