using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Albergue.Administrator.Entities
{
    public class LanguageMapEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public string LaguageBaseId { get; set; }
        public LanguageBaseEntry LanguageBaseEntry { get; set; }

        public string CategoryTranslatableDetailsEntryId { get; set; }
        public CategoryTranslatableDetailsEntry CategoryTranslatableDetailsEntry { get; set; }

        public string ShopItemTranslatableDetailsEntryId { get; set; }
        public ShopItemTranslatableDetailsEntry ShopItemTranslatableDetailsEntry { get; set; }
    }
}