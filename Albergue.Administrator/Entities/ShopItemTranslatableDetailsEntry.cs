using System.ComponentModel.DataAnnotations.Schema;

namespace Albergue.Administrator.Entities
{
    public class ShopItemTranslatableDetailsEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        public string LanguageId { get; set; }
        public LanguageMapEntry Language { get; set; }

        public string ShopItemId { get; set; }
        public ShopItemEntry ShopItem { get; set; }
    }
}
