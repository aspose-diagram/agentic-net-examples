using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine whether protection should be enabled.
                // Accept a command‑line argument like "true" or "false".
                bool protect = false;
                if (args.Length > 0 && bool.TryParse(args[0], out bool parsed))
                    protect = parsed;

                // Paths to the source diagram and the output file.
                // Adjust these paths as needed for your environment.
                string inputPath = "input.vsdx";
                string outputPath = protect ? "protected.vsdx" : "unprotected.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Toggle global document protection flags.
                if (protect)
                {
                    diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
                    diagram.DocumentSettings.ProtectMasters = BOOL.True;
                    diagram.DocumentSettings.ProtectShapes = BOOL.True;
                    diagram.DocumentSettings.ProtectStyles = BOOL.True;
                }
                else
                {
                    diagram.DocumentSettings.ProtectBkgnds = BOOL.False;
                    diagram.DocumentSettings.ProtectMasters = BOOL.False;
                    diagram.DocumentSettings.ProtectShapes = BOOL.False;
                    diagram.DocumentSettings.ProtectStyles = BOOL.False;
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }