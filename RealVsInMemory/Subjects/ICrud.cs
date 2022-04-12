using DummyShop.Persistence;

namespace RealVsInMemory.Subjects;

public interface ICrud
{
    Task<CustomerEntity> Create();

    Task<CustomerEntity> Read();

    Task<CustomerEntity> Update();

    Task<CustomerEntity> Delete();
}