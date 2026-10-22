using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Passwords to apply to confidential PDF OLE objects.
        const string userPassword = "UserPassword123";
        const string ownerPassword = "OwnerPassword123";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape is a foreign (OLE) shape.
                    if (shape.Type != TypeValue.Foreign)
                        continue;

                    // Ensure the shape contains foreign data.
                    if (shape.ForeignData == null)
                        continue;

                    // Verify the foreign data represents an embedded object.
                    if (shape.ForeignData.ForeignType != ForeignType.Object)
                        continue;

                    // Ensure there is binary OLE data.
                    byte[] oleData = shape.ForeignData.ObjectData;
                    if (oleData == null || oleData.Length == 0)
                        continue;

                    // Identify PDF OLE objects via the source file name.
                    string sourceName = shape.ForeignData.ObjectSourceFullName;
                    if (string.IsNullOrEmpty(sourceName) ||
                        !sourceName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        continue; // Not a PDF, skip.
                    }

                    // Load the embedded PDF using Aspose.Pdf (fully qualified).
                    using (MemoryStream pdfInputStream = new MemoryStream(oleData))
                    {
                        // Create the PDF document.
                        Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfInputStream);

                        // Apply password protection.
                        pdfDoc.Encrypt(
                            userPassword,
                            ownerPassword,
                            Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                            Aspose.Pdf.CryptoAlgorithm.RC4x128,
                            false);

                        // Save the encrypted PDF back to a memory stream.
                        using (MemoryStream encryptedPdfStream = new MemoryStream())
                        {
                            pdfDoc.Save(encryptedPdfStream);
                            // Replace the OLE object's binary data with the encrypted version.
                            shape.ForeignData.ObjectData = encryptedPdfStream.ToArray();
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path (VSDX format).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}