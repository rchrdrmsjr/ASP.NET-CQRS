using Npgsql;
using System;

namespace DatabaseTest
{
    {
        static void Main(string[] args)
        {
            // Try direct connection
            string directConnection = "Host=db.ssgokdfpahmagcfednst.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=OJXrmrZj9Gq28m8k;SSL Mode=Require;Trust Server Certificate=true;";
            
            // Try pooler connection
            string poolerConnection = "User Id=postgres.ssgokdfpahmagcfednst;Password=OJXrmrZj9Gq28m8k;Server=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;";
            
            TestConnection(directConnection, "Direct Connection");
            TestConnection(poolerConnection, "Pooler Connection");
        }
        
        static void TestConnection(string connectionString, string type)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                Console.WriteLine($"Attempting {type}...");
                conn.Open();
                Console.WriteLine($"{type} successful!");
                
                // Test a simple query
                using var cmd = new NpgsqlCommand("SELECT 1", conn);
                var result = cmd.ExecuteScalar();
                Console.WriteLine($"Query result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{type} failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}