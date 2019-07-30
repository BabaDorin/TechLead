using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using TechLead.Models;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

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
            

            //If the .exe file wont be created in 10 seconds, it means that something is wrong with the file submitted by user. 
            
            Thread WaitForExe = new Thread(WaitForTheExeFile);

            int TestCases = Imputs.Count();
            Debug.WriteLine("Starting the LookingForEXE THREAD");
            WaitForExe.Start();
            Debug.WriteLine("thread has been started");

            if (!WaitForExe.Join(10000))
            {
                for (int i = 0; i < TestCases; i++)
                {
                    ScoredPoints.Add(0);
                }

                DeleteSourceCode(FileNameWithoutExtension);

                try
                {
                    Debug.WriteLine("Abuse detected. Abort thread");
                    WaitForExe.Abort();
                    cmd.WaitForExit(700);
                    return ScoredPoints;
                }
                catch (Exception)
                {
                    return ScoredPoints;
                }
            }

            //Here we go through each test case.
            for (int i=0; i<TestCases; i++)
            {
                int Error = 0;
                int Score = 0;
                CompileATestCase(Imputs[i], Outputs[i], cmd, maxPointsForATestCase, out Score,out Error);
                if (Error == -1)
                {
                    //Abuse detected. There is no reason to pass the solution to other tests.
                    for(int j=i; j<TestCases; j++)
                    {
                        ScoredPoints.Add(0);
                    }
                    Debug.WriteLine("Abuse detected. Test " + i);
                    break;
                }
                else
                {
                    Debug.WriteLine("Score for test " + i + " = " + Score);
                    ScoredPoints.Add(Score);
                }
            }

            Debug.WriteLine("Going to DeleteProgramEXE");
            //Clean up the space.
            DeleteProgramEXE(FileNameWithoutExtension);
            Debug.WriteLine("Going to DeleteSourceCode");
            DeleteSourceCode(FileNameWithoutExtension);
            Debug.WriteLine("Going to DeleteFromTaskManager");
            DeleteProcessFromTaskManager(FileNameWithoutExtension);

            Debug.WriteLine("Going to return the results");
            return ScoredPoints;
        }

        public void MeasureTime(Thread T)
        {

            Thread.Sleep(5000);
            Debug.WriteLine("Gone 5 seconds");
            if (T.IsAlive)
            {
                T.Abort();
                Debug.WriteLine("Thread was aborted");
            }
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
                    executableExists = File.Exists(ExecutableAdress + @"/" + FileNameWithoutExtension + ".exe");
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
                if (Proc.ProcessName.Equals(FileNameWithoutExtension))
                    Proc.Kill();
        }

        public void CompileATestCase(string Imput, string Output, Process p, int maxPointsForATestCase, out int Score, out int ErrorCode)
        {
            ErrorCode = 0; //No error
            Score = 0;
            try
            {
                //Here we take the executable and run it
                p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Solutions/"+FileNameWithoutExtension+".exe");
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                string ActualOutput = "";
                Thread Test = new Thread(() => SetImputGetOutput(p, Imput, out ActualOutput));
                Debug.WriteLine("Starting the TEST Thread");
                Test.Start();
                Debug.WriteLine("Test thread has been started");

                //Check is the solution is not an abuse (Ex: Infinite loop)
                //If it runs the test in 8 seconds, it means that it's at least legit. Otherwise, it is an abuse and 
                //It won't be sent to any other tests.
                if (!Test.Join(5000))
                {
                    Test.Abort();
                    p.StandardInput.Close();
                    p.WaitForExit(100);
                    Debug.WriteLine("Process killed");

                    ErrorCode = -1; //The code for 'Abuse'
                    return;
                }
                //SetImputGetOutput(p, Imput, out ActualOutput);
                //Write the Imput to the program
                

                //Here we have the user's program output and the correct one. We have to compare them.
                if (ActualOutput.Equals(Output))
                {
                    Score += maxPointsForATestCase;
                }
                else
                {
                    Debug.WriteLine("Incorrect. Rezultatul >> \n" + ActualOutput + " | rezultatul corect: \n" + Output);
                }
                Debug.WriteLine("Went through a test");
                p.WaitForExit();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Eroare Process: ");
                Debug.Write(e);
            }
        }
        public void SetImputGetOutput(Process p, string imput, out string output)
        {
            var streamWriter = p.StandardInput;
            streamWriter.WriteLine(imput);
            p.StandardInput.Close();

            //Read the output from the program
            output = "";
            output = p.StandardOutput.ReadToEnd();
        }
    }
}