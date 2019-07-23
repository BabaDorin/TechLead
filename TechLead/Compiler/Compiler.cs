﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using TechLead.Models;

namespace TechLead.Compiler
{
    public class Compiler
    {
        //In these lists will be stored the test cases (Imputs, Outputs)
        static List<string> Imputs = new List<string>();
        static List<string> Outputs = new List<string>();

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
        public List<int> Compilation(string FilePath, Exercise E)
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
                    int MaxPointsPerTestCase = E.Points / Imputs.Count();
                    return CSharp.CSharpCompilerFunction(FilePath,Imputs,Outputs,MaxPointsPerTestCase);

                case ".pas":
                    PascalCompiler Pas = new PascalCompiler();
                    return Pas.PascalCompilerFunction(FilePath);

                case ".java":
                    JavaCompiler Java = new JavaCompiler();
                    return Java.JavaCompilerFunction(FilePath);
            }

            return null;
        }
        
        //Creating 2 lists. One for Imputs and one for outputs.
        public void CreatingImputOutputLists(Exercise E)
        {
            //Go througn each test case and insert data in those lists
            //The first 2 are mandatory. For the rest we should check their existence
            Imputs.Add(E.TestImput1);
            Imputs.Add(E.TestImput2);
            Outputs.Add(E.TestOutput1);
            Outputs.Add(E.TestOutput2);

            if (E.TestImput3 != null)
            {
                Imputs.Add(E.TestImput3);
                Outputs.Add(E.TestOutput3);
            }

            if (E.TestImput4 != null)
            {
                Imputs.Add(E.TestImput4);
                Outputs.Add(E.TestOutput4);
            }

            if (E.TestImput5 != null)
            {
                Imputs.Add(E.TestImput5);
                Outputs.Add(E.TestOutput5);
            }

            if (E.TestImput6 != null)
            {
                Imputs.Add(E.TestImput6);
                Outputs.Add(E.TestOutput6);
            }

            if (E.TestImput7 != null)
            {
                Imputs.Add(E.TestImput7);
                Outputs.Add(E.TestOutput7);
            }

            if (E.TestImput8 != null)
            {
                Imputs.Add(E.TestImput8);
                Outputs.Add(E.TestOutput8);
            }

            if (E.TestImput9 != null)
            {
                Imputs.Add(E.TestImput9);
                Outputs.Add(E.TestOutput9);
            }

            if (E.TestImput10 != null)
            {
                Imputs.Add(E.TestImput10);
                Outputs.Add(E.TestOutput10);
            }
        }
    }
}