using Newtonsoft.Json;

namespace Server.Models.Auth
{
    public enum StudentStatus
    {
        NotStudent,
        InactiveStudent,
        ActiveStudent
    }

    public enum StaffStatus
    {
        NotStaff,
        NotTeacherStaff,
        Lecturer
    }


    public class UsosUser
    {
        [JsonProperty("id")]
        public int UsosId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("student_status")]
        public StudentStatus StudentStatus { get; set; }
        [JsonProperty("staff_status")]
        public StaffStatus StaffStatus { get; set; }
    }
}