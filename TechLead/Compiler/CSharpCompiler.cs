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
        string FileNameWithoutExtension;
        string fileName;
        public List<int> CSharpCompilerFunction(string FilePath, List<string>Imputs, List<string>Outputs, int maxPointsForATestCase)
        {
            List<int> ScoredPoints = new List<int>();

            fileName = Path.GetFileName(FilePath);

            //We need to know the name of the source file to add the .exe or smth later. The point is that we need it.
            ////Aflam cum se numeste fisierul sursa. Acesta poate fi numit program.cs, main.cpp, main.cs etc. 
            ////Avem nevoie de acest detaliu pentru a specifica in metoda Process() -> prin adaugarea extensiei + ".exe" ->
            //// => main.exe / program.exe / prog.exe etc
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FilePath);

            IsThereAnySourceFile = System.IO.File.Exists(FilePath);

            //Here goes all the souuuce
            //Here is the compiler which converts the source file from .cpp in .exe
            Process cmd = new Process();
            if (IsThereAnySourceFile)
            {
                try
                {
                    //Open CMD, then through it we open csc.exe which will convert our .cs file to .exe
                    cmd.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                    cmd.StartInfo.WorkingDirectory = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/");
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();
                    cmd.StandardInput.WriteLine(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe " + fileName);
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.Close();
                    cmd.Close();
                }
                catch (Exception e)
                {
                    
                    Debug.WriteLine("Eroare CMD: ");
                    Debug.WriteLine(e);
                }
            }
            else
            {
                Debug.WriteLine("Eroare. Fisierul sursa nu a fost gasit.");
            }

            WaitForTheExeFile();

            int TestCases = Imputs.Count();
            
            //Here we go through each test case.
            for(int i=0; i<TestCases; i++)
            {
                ScoredPoints.Add(CompileATestCase(Imputs[i], Outputs[i], cmd, maxPointsForATestCase));
            }

            //Clean up the space.
            DeleteProgramEXE(FileNameWithoutExtension);
            DeleteSourceCode(FileNameWithoutExtension);
            DeleteProcessFromTaskManager(FileNameWithoutExtension);

            //Return the list of scored for each test case.
            return ScoredPoints;
        }

        public void WaitForTheExeFile()
        {
            if (!IsThereAnySourceFile)
            {

            }
            else
            {
                //If there was a .cs file, it means that it was already compiled. Wr are looking for .exe file now.
                string ExecutableAdress = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/");

                //The app waits for the executable. It can be created in a longer period of time. 
                bool executableExists;
                do
                {
                    executableExists = File.Exists(ExecutableAdress + @"/"+FileNameWithoutExtension+".exe");
                    if (!executableExists)
                    {
                        System.Threading.Thread.Sleep(100);
                        Debug.Write(".");
                    }
                    else break;
                } while (!executableExists);
                Debug.WriteLine("The executable has been created.");
            }
        }

        public void DeleteProgramEXE(string FileNameWithoutExtension)
        {
            try
            {
                File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/" + FileNameWithoutExtension + ".exe"));
            }
            catch (Exception)
            {
                Debug.WriteLine("! Program.exe was not deleted. ");
            }
        }

        public void DeleteSourceCode(string FileNameWithoutExtension)
        {
            File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/" + FileNameWithoutExtension + ".cs"));
        }

        public void DeleteProcessFromTaskManager(string FileNameWithoutExtension)
        {
            foreach (Process Proc in Process.GetProcesses())
                if (Proc.ProcessName.Equals(FileNameWithoutExtension+".exe"))
                    Proc.Kill();
        }

        public int CompileATestCase(string Imput, string Output, Process p, int maxPointsForATestCase)
        {
            int PointsScored = 0;
            try
            {
                //Here we take the executable and run it
                p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/"+FileNameWithoutExtension+".exe");
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
                //DeleteProcessFromTaskManager(FileNameWithoutExtension);
                //p.Close();

                //Here we have the user's program output and the correct one. We have to compare them.
                if (ActualOutput.Equals(Output))
                {
                    PointsScored += maxPointsForATestCase;
                }
                else
                {
                    Debug.WriteLine("Incorrect. Rezultatul >> \n" + ActualOutput + " | rezultatul corect: \n" + Output);
                }
                DeleteProcessFromTaskManager(FileNameWithoutExtension);
                p.WaitForExit(700);

                //p.StandardInput.Close();
                //p.WaitForInputIdle(1000);
                //p.WaitForExit();
                //p.Kill();
                //DeleteProcessFromTaskManager(FileNameWithoutExtension);
                //p.WaitForExit(1000);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Eroare Process: ");
                Debug.Write(e);
            }
            return PointsScored;
        }
    }
}