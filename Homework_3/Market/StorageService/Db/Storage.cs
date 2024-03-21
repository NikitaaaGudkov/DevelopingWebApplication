namespace StorageService.Db
{
    public class Storage
    {
        public int Id { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
