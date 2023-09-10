using Core.Entites.Common;

namespace Core.Entites
{
	public class BasketItem : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public float Price { get; set; }
		public string PictureUrl { get; set; }
		public int Quantity { get; set; }
		public string Type { get; set; }
		public string Brand { get; set; }
	}
}