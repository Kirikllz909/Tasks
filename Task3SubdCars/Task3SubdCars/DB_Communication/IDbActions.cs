using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Task3SubdCars
{
    public interface IDbActions
    {
        public Task CreateCar(Car car);
        public Task UpdateCar(int id, Car car);
        public Task<Car> GetCar(int id);
        public Task<Car[]> GetAllCars();
        public Task DeleteCar(int id);
    }
}
