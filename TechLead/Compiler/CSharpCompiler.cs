using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using TechLead.Models;
using System.Data;

namespace TechLead.Compiler
{
    public class CSharpCompiler
    {
        bool IsThereAnySourceFile;
        string SourceFileName;
        string fileName;
        public List<int> CSharpCompilerFunction(string FilePath, List<string>Imputs, List<string>Outputs, int  maxPointsForATestCase)
        {
            List<int> ScoredPoints = new List<int>();
            fileName = Path.GetFileName(FilePath);

            //We need to know the name of the source file to add the .exe or smth later. The point is that we need it.
            ////Aflam cum se numeste fisierul sursa. Acesta poate fi numit program.cs, main.cpp, main.cs etc. 
            ////Avem nevoie de acest detaliu pentru a specifica in metoda Process() -> prin adaugarea extensiei + ".exe" ->
            //// => main.exe / program.exe / prog.exe etc
            SourceFileName = Path.GetFileNameWithoutExtension(FilePath);

            //C:\Users\Maria Baba\Desktop\git\techlead\TechLead\bin\roslyn\csc.exe

            IsThereAnySourceFile = System.IO.File.Exists(FilePath);

            //Here goes all the souuuce
            //Here is the compiler which converts the source file from .cpp in .exe
            Process cmd = new Process();
            if (IsThereAnySourceFile)
            {
                try
                {

                    //THIS THING IS NOT WORKING

                    //var process = new Process
                    //{
                    //    StartInfo =
                    //            {
                    //                CreateNoWindow = true,
                    //                UseShellExecute = false,
                    //                RedirectStandardOutput = true,
                    //                RedirectStandardInput = true,
                    //                RedirectStandardError = true,
                    //                Verb = "runas",
                    //                FileName = @"C:\Windows\System32\cmd.exe",
                    //                Domain = "dir",
                    //            }
                    //};
                    //process.Start();

                    
                    cmd.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                    cmd.StartInfo.WorkingDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/");
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();
                    //cmd.StandardInput.WriteLine(@"~/bin/roslyn/csc.exe " + fileName);
                    cmd.StandardInput.WriteLine(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe " + fileName);
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.Close();
                    cmd.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Eroare CMD: ");
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine("Eroare. Fisierul sursa nu a fost gasit.");
            }
            Procesare(cmd);

            int TestCases = Imputs.Count();
            
            for(int i=0; i<TestCases; i++)
            {
                ScoredPoints.Add(CompileATestCase(Imputs[1], Outputs[2], cmd, maxPointsForATestCase));
            }

            return ScoredPoints;
        }

        public void Procesare(Process p)
        {

            if (!IsThereAnySourceFile)
            {

            }
            else
            {
                //Daca exista fisierul sursa inseamna ca acesta deja a trecut prin procesul de compilare
                string ExecutableAdress = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/");

                //The app waits for the executable. It can be created in a longer period of time. 
                ////Programul asteapta pana cand apare executabilul. Compilarea poate lua mai mult timp pentru un program mai complex
                bool executableExists;
                do
                {
                    executableExists = File.Exists(ExecutableAdress + @"/"+SourceFileName+".exe");
                    if (!executableExists)
                    {
                        System.Threading.Thread.Sleep(100);
                        Console.Write(".");
                    }
                    else break;
                } while (!executableExists);

                Console.WriteLine();
            }
        }

        public void DeleteProgramEXE(string fileName)
        {
            try
            {
                File.Delete(Directory.GetCurrentDirectory() + @"/" + Path.GetFileNameWithoutExtension(fileName) + ".exe");
                Console.WriteLine("File path of Program2.exe: " + Directory.GetCurrentDirectory() + @"\" + Path.GetFileNameWithoutExtension(fileName) + ".exe");
            }
            catch (Exception)
            {
                Console.WriteLine("! Program.exe was not deleted. ");
            }
        }

        public void DeleteSourceCode(string path)
        {
            //Remove the process from the task manager
            foreach (Process Proc in Process.GetProcesses())
                if (Proc.ProcessName.Equals(fileName))
                    Proc.Kill();

            //Delete the source code (main.cpp)
            File.Delete(path);

        }
        public int CompileATestCase(string Imput, string Output, Process p, int maxPointsForATestCase)
        {
            int PointsScored = 0;
            try
            {
                //Here we take the executable and run it
                p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/"+SourceFileName+".exe");
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                //Write the Imput to the program
                var streamWriter = p.StandardInput;
                streamWriter.WriteLine(Imput);
                p.StandardInput.Close();

                //Read the output from the program
                string ActualOutput = "";
                ActualOutput = p.StandardOutput.ReadToEnd();

                //Here we have the user's program output and the correct one. We have to compare them.
                if (ActualOutput.Equals(Output))
                {
                    PointsScored += maxPointsForATestCase;
                }
                else
                {
                    Console.WriteLine("Incorrect. Rezultatul >> \n" + ActualOutput + " | rezultatul corect: \n" + Output);
                }

                p.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Eroare Process: ");
                Console.Write(e);
            }
            return PointsScored;
        }
    }
}