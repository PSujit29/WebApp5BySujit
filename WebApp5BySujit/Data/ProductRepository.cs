using System.Data;
using Microsoft.Data.SqlClient;
using WebApp5BySujit.Models;

namespace WebApp5BySujit.Data;

public class ProductRepository
{
    private readonly string _connectionString;
    public ProductRepository(IConfiguration configuration) =>
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    private SqlConnection GetConnection() => new(_connectionString);

    public List<Product> GetAll()
    {
        var list = new List<Product>();
        using var conn = GetConnection();
        using var cmd = new SqlCommand("SELECT * FROM Products", conn);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                Price = Convert.ToDecimal(reader["Price"])
            });
        }
        return list;
    }

    public Product? GetById(int id)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Product
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()!,
                Price = Convert.ToDecimal(reader["Price"])
            };
        }
        return null;
    }

    public void Add(Product p)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", conn);
        cmd.Parameters.AddWithValue("@Name", p.Name);
        cmd.Parameters.AddWithValue("@Price", p.Price);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void Update(Product p)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", p.Id);
        cmd.Parameters.AddWithValue("@Name", p.Name);
        cmd.Parameters.AddWithValue("@Price", p.Price);
        conn.Open();
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand("DELETE FROM Products WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}