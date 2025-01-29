namespace DinDin.Domain.Acconts
{
    public interface IAccontRepository
    {
        Accont GetById(int id);
        void Add(Accont accont);
        Accont Update(Accont accont);
        void Delete(int id);
    }
}