using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace TechLead.Compiler
{
    public class Compiler
    {

        //Method to get the file extension. It will be used later for choosing a compiler (Java, CSharp, CPP, Pascal)
        public string GetFileExtension(string fileName)
        {
            try
            {
                string Extension = Path.GetExtension(fileName).ToLower();
                if (Extension == ".cpp" || Extension == ".cs" || Extension == ".java" || Extension == ".pas")
                {
                    return Extension;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void KillPorocess(string sourceFile)
        {
            foreach (Process Proc in Process.GetProcesses())
                if (Proc.ProcessName.Equals(sourceFile + ".exe"))
                    Proc.Kill();
        }

        public void DeleteProgramEXE(string fileName)
        {
            try
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\" + Path.GetFileNameWithoutExtension(fileName) + ".exe");
                Console.WriteLine("File path of Program2.exe: " + Directory.GetCurrentDirectory() + @"\" + Path.GetFileNameWithoutExtension(fileName) + ".exe");
            }
            catch (Exception)
            {
                Console.WriteLine("! Program.exe was not deleted. ");
            }
        }

        //Call the specific compiler for the language the program was written in.
        //The function will return a list with all the scored points for the problem.
        public List<int> Compilation(string FilePath)
        {
            string fileName = Path.GetFileName(FilePath);

            //The points for each test case will be stored in a List of integers.
            switch (GetFileExtension(fileName))
            {
                case ".cpp":
                    CPPCompiler CPP = new CPPCompiler();
                    return CPP.CPPCompilerFunction(FilePath);

                case ".cs":
                    CSharpCompiler CSharp = new CSharpCompiler();
                    return CSharp.CSharpCompilerFunction(FilePath);

                case ".pas":
                    PascalCompiler Pas = new PascalCompiler();
                    return Pas.PascalCompilerFunction(FilePath);

                case ".java":
                    JavaCompiler Java = new JavaCompiler();
                    return Java.JavaCompilerFunction(FilePath);
            }

            return null;
        }
        

    }
}