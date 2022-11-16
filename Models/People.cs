using System.ComponentModel.DataAnnotations;

namespace MvcDB.Models
{
    public class People
    {
	[Key]
        public int PersonId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }
}
