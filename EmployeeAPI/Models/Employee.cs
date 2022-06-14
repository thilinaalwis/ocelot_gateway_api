using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required]
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("jobtitle")]
        public string JobTitle { get; set; }
        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }
        [JsonPropertyName("hireddate")]
        public DateTime HiredDate { get; set; }
        [JsonPropertyName("dtstamp")]
        public DateTime DtStamp { get; set; }
    }
}
