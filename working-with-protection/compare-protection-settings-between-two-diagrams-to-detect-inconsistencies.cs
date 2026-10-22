using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath1> <DiagramPath2>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard against missing files
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        try
        {
            // Load first diagram inside a using block
            using (Diagram diagram1 = new Diagram(diagramPath1))
            // Load second diagram inside a using block
            using (Diagram diagram2 = new Diagram(diagramPath2))
            {
                // Retrieve global protection settings from both diagrams
                BOOL protectBkgnds1 = diagram1.DocumentSettings.ProtectBkgnds;
                BOOL protectBkgnds2 = diagram2.DocumentSettings.ProtectBkgnds;

                BOOL protectMasters1 = diagram1.DocumentSettings.ProtectMasters;
                BOOL protectMasters2 = diagram2.DocumentSettings.ProtectMasters;

                BOOL protectShapes1 = diagram1.DocumentSettings.ProtectShapes;
                BOOL protectShapes2 = diagram2.DocumentSettings.ProtectShapes;

                BOOL protectStyles1 = diagram1.DocumentSettings.ProtectStyles;
                BOOL protectStyles2 = diagram2.DocumentSettings.ProtectStyles;

                bool anyDifference = false;

                // Compare each protection flag and report inconsistencies
                if (protectBkgnds1 != protectBkgnds2)
                {
                    anyDifference = true;
                    Console.WriteLine($"ProtectBkgnds differs: Diagram1 = {protectBkgnds1}, Diagram2 = {protectBkgnds2}");
                }

                if (protectMasters1 != protectMasters2)
                {
                    anyDifference = true;
                    Console.WriteLine($"ProtectMasters differs: Diagram1 = {protectMasters1}, Diagram2 = {protectMasters2}");
                }

                if (protectShapes1 != protectShapes2)
                {
                    anyDifference = true;
                    Console.WriteLine($"ProtectShapes differs: Diagram1 = {protectShapes1}, Diagram2 = {protectShapes2}");
                }

                if (protectStyles1 != protectStyles2)
                {
                    anyDifference = true;
                    Console.WriteLine($"ProtectStyles differs: Diagram1 = {protectStyles1}, Diagram2 = {protectStyles2}");
                }

                if (!anyDifference)
                {
                    Console.WriteLine("All global protection settings are consistent between the two diagrams.");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}