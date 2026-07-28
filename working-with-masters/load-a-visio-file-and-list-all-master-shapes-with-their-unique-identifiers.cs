using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (VDX, VSD, VSDX, etc.)
                string visioFilePath = "sample.vsdx";

                // Load the Visio diagram using the appropriate constructor.
                // The Diagram class handles format detection internally.
                Diagram diagram = new Diagram(visioFilePath);

                // Iterate through all masters in the document.
                foreach (Master master in diagram.Masters)
                {
                    // Output the master name and its unique identifier (GUID).
                    Console.WriteLine($"Master Name: {master.Name}, UniqueID: {master.UniqueID}");
                }

                // Dispose the diagram to release unmanaged resources.
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }