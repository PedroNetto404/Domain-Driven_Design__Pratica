namespace DDDPratica.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit(); 
}