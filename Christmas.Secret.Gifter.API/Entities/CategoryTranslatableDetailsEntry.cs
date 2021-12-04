using System.ComponentModel.DataAnnotations.Schema;

namespace Albergue.Administrator.Entities
{
    public class CategoryTranslatableDetailsEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }

        public string LanguageId { get; set; }
        public LanguageMapEntry Language { get; set; }

        public string CategoryId { get; set; }
        public CategoryEntry Category { get; set; }
    }
}
