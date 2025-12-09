namespace PROG2111_FinalPhase5
{
    /*
     * FILE : ProgramModel.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     */
    internal class ProgramModel
    {
        public int ProgramId { get; set; }           // programID (PK)
        public string ProgramName { get; set; }      // programName
        public string CredentialType { get; set; }   // credentialType
        public byte DurationInTerms { get; set; }    // durationInTerms (TINYINT)
        public bool IsAvailable { get; set; }        // isAvailable
    }
}
