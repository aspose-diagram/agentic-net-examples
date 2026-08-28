using System.IO;
using System;
using Aspose.Diagram;

class RemoveVbaPassword
{
    // Placeholder method for user credential verification.
    // Replace with actual authentication logic as needed.
    static bool VerifyUser(string userName, string password)
    {
        // Example: simple check against hard‑coded credentials.
        // In production, integrate with your authentication system.
        return userName == "admin" && password == "secret";
    }

    static void Main()
    {
        try
        {

            // Path to the source Visio diagram.
            string sourcePath = "ProtectedDiagram.vsdx";

            // Load the diagram. Aspose.Diagram handles encrypted files internally
            // when the correct passwords are supplied via PdfEncryptionDetails
            // (if the diagram is saved as PDF). For a Visio file, the load method
            // does not require additional parameters.
            Diagram diagram = new Diagram(sourcePath);

            // Prompt (or otherwise obtain) user credentials.
            // Here we use hard‑coded values for illustration.
            string userName = "admin";
            string password = "secret";

            // Verify the credentials before proceeding.
            if (!VerifyUser(userName, password))
            {
                Console.WriteLine("Authentication failed. Operation aborted.");
                return;
            }

            // At this point the user is authenticated.
            // Remove the VBA project data which contains the password protection.
            // Setting VbProjectData to null effectively strips the VBA project
            // (including any password) from the diagram.
            diagram.VbProjectData = null;

            // Optionally, you can also remove any remaining macros completely.
            // diagram.RemoveMacro();

            // Save the modified diagram to a new file.
            string outputPath = "DiagramWithoutVbaPassword.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Password protection removed and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
