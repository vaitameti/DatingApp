using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]//attribute of a table which will create a table called Photos by entity framework
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public AppUser AppUser { get; set; } //defining that there is a reationship between the AppUser class 
        public int AppUserId { get; set; }

    }
}