namespace todoApi
{
    public class Todo
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("id")]
        public int Id { get; set; }
        
        [System.ComponentModel.DataAnnotations.Schema.Column("name")]
        public string? Name { get; set; }
        
        // Add column name attribute to match Supabase schema
        [System.ComponentModel.DataAnnotations.Schema.Column("isComplete")]
        public bool IsComplete { get; set; }
    }
}
