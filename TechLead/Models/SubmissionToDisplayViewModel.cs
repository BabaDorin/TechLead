using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechLead.Models
{
    /*
      <tr>
                    <td>@S.SubmissionID</td>
                    <td>@S.SubmissionAuthorUserName</td>
                    <td>@S.Date</td>
                    <td>@S.ScoredPoints</td>
                    <td>@Html.ActionLink("See More", "SubmissionDetails", "Problem", new { id = @S.SubmissionID }, null)</td>
                </tr>
         */
    public class SubmissionToDisplayViewModel
    {
        public int SubmissionID { get; set; }
        public string SubmissionAuthorId { get; set; }

        public string SubmissionAuthorUserName { get; set; }

        //Date when the user submitted the solution
        public DateTime Date { get; set; }

        public int ExerciseId { get; set; }

        //The name of the exercise
        public string Exercise { get; set; }

        //Total points scored by the program
        public double ScoredPoints { get; set; }
        public bool RestrictedMode { get; set; }
        public int? ClassId { get; set; }
    }
}