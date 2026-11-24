using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path after processing
        string outputPath = "output.vsdx";

        // Passwords to apply to confidential PDF OLE objects
        const string userPassword = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape is a foreign (OLE) shape and has foreign data
                        if (shape.Type != TypeValue.Foreign || shape.ForeignData == null)
                            continue;

                        // Ensure there is embedded object data
                        byte[] objectData = shape.ForeignData.ObjectData;
                        if (objectData == null || objectData.Length == 0)
                            continue;

                        // Simple detection of PDF content
                        bool isPdf = false;

                        // Check file extension from source name if available
                        string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                        if (sourceName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            isPdf = true;
                        }
                        else if (objectData.Length >= 5)
                        {
                            // Check PDF header "%PDF-"
                            string header = System.Text.Encoding.ASCII.GetString(objectData, 0, 5);
                            if (header == "%PDF-")
                                isPdf = true;
                        }

                        if (!isPdf)
                            continue;

                        // Process the embedded PDF: apply password protection
                        using (MemoryStream pdfInput = new MemoryStream(objectData))
                        {
                            // Load PDF document using fully qualified Aspose.Pdf type
                            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfInput);

                            // Encrypt the PDF with the specified passwords
                            pdfDoc.Encrypt(
                                userPassword,
                                ownerPassword,
                                Aspose.Pdf.Facades.DocumentPrivilege.AllowAll,
                                Aspose.Pdf.CryptoAlgorithm.RC4x128,
                                false);

                            // Save the protected PDF back to a memory stream
                            using (MemoryStream pdfOutput = new MemoryStream())
                            {
                                pdfDoc.Save(pdfOutput);
                                // Replace the OLE object's data with the protected PDF bytes
                                shape.ForeignData.ObjectData = pdfOutput.ToArray();
                            }
                        }

                        Console.WriteLine($"Applied password to PDF OLE object in shape ID {shape.ID} on page {page.Name}.");
                    }
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}