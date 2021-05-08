using System;
using System.Runtime.Serialization;

namespace Heisln.Api.Models
{
    public class EditUser
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Birthday
        /// </summary>
        [DataMember(Name = "birthday")]
        public DateTime Birthday { get; set; }
    }
}