using DDDPratica.Core.DomainObjects;
using DDDPratica.Core.DomainObjects.Entidades;

namespace DDDPratica.Core.Data;

public interface IRepository<TEntidade> : IDisposable 
    where TEntidade : IRaizAgregacao
{
    IUnitOfWork UnitOfWork { get; }
}