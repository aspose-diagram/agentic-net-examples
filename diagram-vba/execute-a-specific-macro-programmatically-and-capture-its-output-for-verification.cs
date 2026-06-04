using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsd");

            // Access the VBA project modules collection
            VbaModuleCollection modules = diagram.VbaProject.Modules;

            // Retrieve the macro module (replace "Module1" with the actual module name)
            VbaModule macroModule = modules["Module1"]; // or modules[0] for the first module

            // Get the VBA code of the macro
            string macroCode = macroModule.Codes;

            // Write the macro code to a temporary VBScript file
            string tempVbsPath = Path.Combine(Path.GetTempPath(), "tempMacro.vbs");
            File.WriteAllText(tempVbsPath, macroCode);

            // Execute the VBScript using cscript and capture its standard output
            Process proc = new Process();
            proc.StartInfo.FileName = "cscript";
            proc.StartInfo.Arguments = $"//NoLogo \"{tempVbsPath}\"";
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();

            string macroOutput = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            // Output the captured macro result
            Console.WriteLine("Macro Output:");
            Console.WriteLine(macroOutput);

            // Example verification (replace with actual expected output)
            string expectedOutput = "Hello World";
            bool isVerified = macroOutput.Trim() == expectedOutput;
            Console.WriteLine($"Verification: {(isVerified ? "Passed" : "Failed")}");

            // Clean up the temporary file
            File.Delete(tempVbsPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
