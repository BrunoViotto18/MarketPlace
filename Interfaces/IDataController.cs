using System.Collections.Generic;

namespace Interfaces;

public interface IDataController<T, O>
{
    public T findById();
    public List<T> getAll();
    public void update();
    public void delete();
    public T convertModelToDTO();
}
