using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Task3SubdCars
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string ProductionCountry { get; set; }
        public int Count { get; set; }
        public Car(string Name, decimal Cost, string ProductionCountry, int Count)
        {
            this.Name = Name;
            this.Cost = Cost;
            this.ProductionCountry = ProductionCountry;
            this.Count = Count;
        }
        public Car(int Id,string Name, decimal Cost, string ProductionCountry, int Count)
        {
            this.Id = Id;
            this.Name = Name;
            this.Cost = Cost;
            this.ProductionCountry = ProductionCountry;
            this.Count = Count;
        }
    }
    
    public class DbActions : IDbActions
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQL"].ConnectionString;
        public async Task CreateCar(Car car)
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            string query = "INSERT INTO cars (name, cost, production_country, count) VALUES (@p1, @p2, @p3, @p4);";
            await using NpgsqlCommand command = new NpgsqlCommand(query, connection)
            {
                Parameters =
                {
                    new("p1",car.Name),
                    new("p2",car.Cost),
                    new("p3",car.ProductionCountry),
                    new("p4",car.Count)
                }
            };
            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateCar(int carId, Car car)
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            string query = "UPDATE cars SET name = @p1, cost = @p2, production_country = @p3, count = @p4 WHERE id = @p5;";
            await using NpgsqlCommand command = new NpgsqlCommand(query, connection)
            {
                Parameters =
                {
                    new("p1",car.Name),
                    new("p2",car.Cost),
                    new("p3",car.ProductionCountry),
                    new("p4",car.Count),
                    new("p5", carId)
                }
            };
            await command.ExecuteNonQueryAsync();
        }
        public async Task DeleteCar(int carId)
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("DELETE FROM cars WHERE id = @p1;", connection)
            {
                Parameters =
                {
                    new("p1",carId)
                }
            };
            await command.ExecuteNonQueryAsync();
        }
        public async Task<Car> GetCar(int carId)
        {
            Car result;
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("SELECT * FROM cars WHERE id = @p1;", connection)
            {
                Parameters =
                {
                    new("p1",carId)
                }
            };
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int id = Convert.ToInt32(reader[0]);
                string carName = Convert.ToString(reader[1]);
                decimal cost = Convert.ToDecimal(reader[2]);
                string productionCountry = Convert.ToString(reader[3]);
                int count = Convert.ToInt32(reader[4]);

                result = new Car(id, carName, cost, productionCountry, count);
                return result;
            }
            return null;
        }
        public async Task<Car[]> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("SELECT * FROM cars;", connection);
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int id = Convert.ToInt32(reader[0]);
                string carName = Convert.ToString(reader[1]);
                decimal cost = Convert.ToDecimal(reader[2]);
                string productionCountry = Convert.ToString(reader[3]);
                int count = Convert.ToInt32(reader[4]);

                cars.Add(new Car(id, carName, cost, productionCountry, count));
            }
            return cars.ToArray();
        }
    }
}
