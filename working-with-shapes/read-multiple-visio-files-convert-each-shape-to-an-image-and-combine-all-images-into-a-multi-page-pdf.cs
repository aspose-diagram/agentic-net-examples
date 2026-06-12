using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input folder path and output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioShapeToPdf <InputFolder> <OutputPdfPath>");
                return;
            }

            string inputFolder = args[0];
            string outputPdfPath = args[1];

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Create a temporary directory to store shape images
            string tempImageDir = Path.Combine(Path.GetTempPath(), "VisioShapeImages_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempImageDir);

            try
            {
                // Collect all Visio files (common extensions)
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
                List<string> imageFiles = new List<string>();

                foreach (string visioFile in visioFiles)
                {
                    string ext = Path.GetExtension(visioFile).ToLowerInvariant();
                    if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsx" && ext != ".vtx")
                    {
                        continue; // skip non‑Visio files
                    }

                    // Load the Visio diagram
                    Diagram diagram = new Diagram(visioFile);

                    // Iterate through pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Build a unique image file name for the shape
                            string imageFileName = $"{Path.GetFileNameWithoutExtension(visioFile)}_p{page.ID}_s{shape.ID}.png";
                            string imagePath = Path.Combine(tempImageDir, imageFileName);

                            // Export shape to PNG image
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            shape.ToImage(imagePath, imgOptions);

                            imageFiles.Add(imagePath);
                        }
                    }
                }

                // Create a new PDF document using Aspose.Pdf (fully qualified to avoid namespace conflicts)
                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

                // Add each exported image as a separate PDF page
                foreach (string imgPath in imageFiles)
                {
                    // Add a new page
                    Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                    // Create an image object and set its source file
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.File = imgPath;

                    // Optionally fit the image to the page width
                    pdfImage.FixWidth = pdfPage.PageInfo.Width;

                    // Add the image to the page's paragraphs collection
                    pdfPage.Paragraphs.Add(pdfImage);
                }

                // Save the combined PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF created successfully at: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
            finally
            {
                // Clean up temporary images
                if (Directory.Exists(tempImageDir))
                {
                    try
                    {
                        Directory.Delete(tempImageDir, true);
                    }
                    catch
                    {
                        // If cleanup fails, ignore to avoid breaking the main flow
                    }
                }
            }
        }
    }