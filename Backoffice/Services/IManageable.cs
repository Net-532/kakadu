using System.Collections.Generic;

namespace Kakadu.Backoffice.Services
{
    public interface IManageable<T>
    {
        List<T> LoadItems();
        void AddItem(T item);
        void DeleteItem(int Id);
        void EditItem(int itemId, T newItem);
    }
}
