using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albergue.Administrator.Entities
{
    public class ShopItemEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }
        public List<ImageEntry> Images { get; set; }

        public List<ShopItemTranslatableDetailsEntry> TranslatableDetails { get; set; }
        public string CategoryId { get; set; }
        public CategoryEntry Category { get; set; }
    }
}
