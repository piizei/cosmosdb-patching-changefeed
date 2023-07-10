namespace DemoApp;

class Item
{
    public String id { get; set; } = Guid.NewGuid().ToString();
    public DateTime created { get; set; } = DateTime.UtcNow;
    public DateTime modified { get; set; } = DateTime.UtcNow;
    public dynamic originalMessage { get; set; }
    
    int ttl = 60 * 60 * 24 * 30; // 30 days
    
    public Item()
    {
    }

    
    
}