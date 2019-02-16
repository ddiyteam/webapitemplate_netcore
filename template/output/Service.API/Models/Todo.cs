namespace $ext_safeprojectname$.API.Models
{
    /// <summary>
    /// Todo item
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// Todo id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Todo title
        /// </summary> 
        public string Title { get; set; }
        /// <summary>
        /// Todo status
        /// </summary>        
        public bool Completed { get; set; }        
       
    }
}
