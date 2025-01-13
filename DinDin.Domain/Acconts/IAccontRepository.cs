namespace DinDin.Domain.Acconts
{
    public interface IAccontRepository
    {
        Accont GetById(int id);
        void Add(Accont accont);
        Accont UpdateAccont(Accont accont);
        void DeleteAccont(int id);
    }
}