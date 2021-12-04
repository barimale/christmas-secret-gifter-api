using System.ComponentModel.DataAnnotations.Schema;

namespace Albergue.Administrator.Entities
{
    public class ImageEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageData { get; set; }

        public string ShopItemId { get; set; }
        public ShopItemEntry ShopItem { get; set; }
    }
}
