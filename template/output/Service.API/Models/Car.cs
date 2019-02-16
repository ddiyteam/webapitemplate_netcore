using System;
using System.ComponentModel.DataAnnotations;

namespace $ext_safeprojectname$.API.Models
{
    /// <summary>
    /// Car type
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Car id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Car model name
        /// </summary>
        [Required]
        [StringLength(45, MinimumLength = 1)]
        public string ModelName { get; set; }        
        /// <summary>
        /// Car type
        /// </summary>
        [Required]
        public CarType CarType { get; set; }
        /// <summary>
        /// CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// ModifiedOn
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
