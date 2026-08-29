using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the folder containing Visio files if not provided as an argument.
        string folderPath = args.Length > 0 ? args[0] : "";
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            Console.Write("Enter the full path to the folder with Visio files: ");
            folderPath = Console.ReadLine() ?? "";
        }

        // Guard: ensure the folder exists before proceeding.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Prepare a list to hold paths of temporary PNG images generated from shapes.
        List<string> tempImagePaths = new List<string>();

        // Enumerate supported Visio file extensions.
        string[] visioExtensions = new[] { ".vsdx", ".vsd", ".vdx", ".vsx", ".vtx", ".vssx", ".vstx", ".vsdm", ".vssm", ".vstm" };

        // Iterate over each Visio file in the specified folder.
        foreach (string visioFilePath in Directory.GetFiles(folderPath))
        {
            // Guard: verify each discovered file actually exists (redundant but follows the rule).
            if (!File.Exists(visioFilePath))
            {
                Console.Error.WriteLine($"File not found: {visioFilePath}");
                continue;
            }

            // Process only files with supported Visio extensions.
            if (Array.IndexOf(visioExtensions, Path.GetExtension(visioFilePath).ToLower()) < 0)
                continue;

            try
            {
                // Load the Visio diagram from the file.
                Diagram diagram = new Diagram(visioFilePath);

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create a unique temporary file name for the shape image.
                        string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");

                        // Guard: ensure the temporary path is valid (file does not exist yet).
                        if (File.Exists(tempImagePath))
                        {
                            Console.Error.WriteLine($"Unexpected existing temp file: {tempImagePath}");
                            continue;
                        }

                        // Configure PNG export options.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                        // Export the shape to a PNG file.
                        shape.ToImage(tempImagePath, imgOptions);

                        // Guard: verify the image was created successfully.
                        if (!File.Exists(tempImagePath))
                        {
                            Console.Error.WriteLine($"Failed to create image for shape ID {shape.ID} in file {visioFilePath}");
                            continue;
                        }

                        // Store the temporary image path for later PDF assembly.
                        tempImagePaths.Add(tempImagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing a Visio file.
                Console.Error.WriteLine($"Error processing '{visioFilePath}': {ex.Message}");
            }
        }

        // If no images were generated, exit early.
        if (tempImagePaths.Count == 0)
        {
            Console.WriteLine("No shapes were exported; PDF will not be created.");
            return;
        }

        // Define the output PDF file path in the current working directory.
        string outputPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "CombinedShapes.pdf");

        try
        {
            // Create a new Aspose.Pdf document (fully qualified to avoid namespace conflicts).
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            // Add each PNG image as a separate page in the PDF.
            foreach (string imgPath in tempImagePaths)
            {
                // Guard: ensure the image file still exists before adding.
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image file missing before PDF insertion: {imgPath}");
                    continue;
                }

                // Create a new page in the PDF document.
                Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                // Load the image bytes into a memory stream.
                using (FileStream imgStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    // Create an Aspose.Pdf image object and assign the stream.
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imgStream
                    };

                    // Add the image to the page's paragraph collection.
                    pdfPage.Paragraphs.Add(pdfImage);
                }
            }

            // Save the assembled PDF to the designated output path.
            pdfDoc.Save(outputPdfPath, Aspose.Pdf.SaveFormat.Pdf);

            Console.WriteLine($"PDF successfully created at: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during PDF creation.
            Console.Error.WriteLine($"Error creating PDF: {ex.Message}");
        }
        finally
        {
            // Clean up all temporary image files.
            foreach (string imgPath in tempImagePaths)
            {
                try
                {
                    if (File.Exists(imgPath))
                        File.Delete(imgPath);
                }
                catch
                {
                    // Suppress any cleanup errors to avoid breaking the main flow.
                }
            }
        }
    }
}