using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Build the footer text using the document title and version number
                string title = diagram.DocumentProps.Title;
                string version = diagram.Version; // Version is a string
                diagram.HeaderFooter.FooterCenter = $"{title} v{version}";

                // Optionally, save the updated diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }