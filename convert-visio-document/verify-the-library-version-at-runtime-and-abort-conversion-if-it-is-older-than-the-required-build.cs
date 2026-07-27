using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // Verify Aspose.Diagram library version at runtime.
            // -----------------------------------------------------------------
            // Required minimum assembly version (adjust as needed).
            const string requiredAssemblyVersion = "23.5.0";

            // BuildVersionInfo provides static read‑only version information.
            // AssemblyVersion is a string like "23.5.0.0".
            string currentVersionString = BuildVersionInfo.AssemblyVersion;

            // Parse the version strings.
            Version currentVersion = new Version(currentVersionString);
            Version minimumVersion = new Version(requiredAssemblyVersion);

            // Abort if the current library version is older than required.
            if (currentVersion < minimumVersion)
            {
                throw new InvalidOperationException(
                    $"Aspose.Diagram version {currentVersion} is older than the required version {minimumVersion}. Conversion aborted.");
            }

            // -----------------------------------------------------------------
            // Load the Visio diagram (use existing load rule – placeholder here).
            // -----------------------------------------------------------------
            // Diagram diagram = LoadYourDiagramSomehow();

            // -----------------------------------------------------------------
            // Perform conversion operations here.
            // -----------------------------------------------------------------
            // ...

            // -----------------------------------------------------------------
            // Save the result (use existing save rule – placeholder here).
            // -----------------------------------------------------------------
            // SaveYourDiagramSomehow(diagram);
        }
    }