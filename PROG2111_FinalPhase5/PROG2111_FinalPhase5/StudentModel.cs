using System;

namespace PROG2111_FinalPhase5
{
    /*
     * FILE : StudentModel.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     */
    internal class StudentModel
    {
        public int StudentId { get; set; }          // studentID (PK)
        public int ProgramId { get; set; }          // programID (FK)
        public string FirstName { get; set; }       // firstName
        public string LastName { get; set; }        // lastName
        public string EmailAddress { get; set; }    // emailAddress
        public DateTime? DateOfBirth { get; set; }  // dateOfBirth (nullable)
        public DateTime DateEnrolled { get; set; }  // dateEnrolled
    }
}
