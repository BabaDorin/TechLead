using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    public class Judge0_SubmissionViewModel
    {
        //For more detailed information, follow https://api.judge0.com/#submission-submission-get 

        public int Id { get; set; }

        //Program's source code
        [Required]
        public string source_code { get; set; }

        //Code for language.
        //15 for c++
        //17 for C#
        //28 for Java
        //33 for Pascal
        //36 for Python
        //Other languages => https://api.judge0.com/languages
        [Required]
        public int language_id { get; set; }

        /////Options for the compiler (i.e. compiler flags)
        public string compiler_options { get; set; }

        //Command line arguments for the program
        public string command_line_arguments { get; set; }

        //Input for program.
        public string stdin { get; set; }

        //Expected output of program.Used when you want to compare with stdout
        public string expected_output { get; set; }

        //Default runtime limit for every program. 
        //Time in which the OS assigns the processor to different tasks is not counted.
        //Unit - Second
        public float cpu_time_limit { get; set; }

        //When a time limit is exceeded, wait for extra time, before killing the program. 
        //This has the advantage that the real execution time is reported, even though it
        //slightly exceeds the limit.
        //Unit - Second
        public float cpu_extra_time { get; set; }

        //Limit wall-clock time in seconds. Decimal numbers are allowed. This clock measures
        //the time from the start of the program to its exit, so it does not stop when the 
        //program has lost the CPU or when it is waiting for an external event. We recommend to 
        //use cpu_time_limit as the main limit, but set wall_time_limit to a much higher value 
        //as a precaution against sleeping programs.
        //Unit - Second
        public float wall_time_limit { get; set; }

        //Limit address space of the program.
        //Unit - kiloByte
        public float memory_limit { get; set; }

        //Limit process stack.
        //Unit - kiloByte
        public int stack_limit { get; set; }

        //Maximum number of processes and/or threads program can create.
        public int max_processes_and_or_threads { get; set; }

        //If true then cpu_time_limit will be used as per process and thread.
        public bool enable_per_process_and_thread_time_limit { get; set; }

        //If true then memory_limit will be used as per process and thread.
        public bool enable_per_process_and_thread_memory_limi { get; set; }

        //Limit file size created or modified by the program.
        //Unit - kiloByte
        public int max_file_size { get; set; }

        //Run each program number_of_runs times and take average of time and memory.
        public int number_of_runs { get; set; }

        //Standard output of the program after execution.
        public string stdout { get; set; }

        //Standard error of the program after execution.
        public string stderr { get; set; }

        //Compiler output after compilation.
        public string compile_output { get; set; }

        //If submission status is Internal Error then 
        //this message comes from Judge0 API itself, 
        //otherwise this is status message from Isolate.
        public string message { get; set; }

        //The program’s exit code.
        public int exit_code { get; set; }

        //Signal code that the program recieved before exiting
        public int exit_signal { get; set; }

        //Submission status
        public object status { get; set; }

        //Date and time when submission was created.
        public DateTime created_at { get; set; }

        //Date and time when submission was processed.
        public DateTime finished_at { get; set; }

        //Unique submission token used for getting specific submission.
        public string token { get; set; }

        //Program’s runtime.
        //Unit - seconds
        public float time { get; set; }

        //Program’s wall time. Will be greater or equal to time.
        //Unit - second
        public float wall_time { get; set; }

        //Memory used by the program after execution.
        public float memory { get; set; }
    }
}