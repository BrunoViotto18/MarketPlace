using System.Collections.Generic;

namespace Interfaces;

public interface IDataController<T>
{
    public T findById();
    public List<T> getAll();
    public int save();
    public void update();
    public void delete();
    public T convertModelToDTO();
}
