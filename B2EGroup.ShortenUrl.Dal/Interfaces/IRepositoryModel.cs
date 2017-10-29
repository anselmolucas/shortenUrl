using System.Collections.Generic;

namespace B2EGroup.ShortenUrl.Dal.Interfaces
{
    public interface IRepositoryModel<T> where T : class
    {
        T Save(T objectToSave);
        T Add(T objectToAdd);
        T Update(T objectToUpdate);
        void Delete(T objectToDelete);
        void Delete(params object[] idToDelete);
        List<T> SelectAll();
        T SelectPK(params object[] idToSelect);        
        void SaveChanges();
    }
}
