namespace Model;

using Interfaces;

public class Product : IValidateDataObject<Product>, IDataController<Product>
{
    // Atributos
    private String name;
    private String bar_code;


    // Construtor
    public Product()
    {

    }


    // GET & SET
    public String getName()
    {
        return name;
    }
    public void setName(String name)
    {
        this.name = name;
    }

    public String getBarCode()
    {
        return bar_code;
    }
    public void setBarCode(String bar_code)
    {
        this.bar_code = bar_code;
    }


    // M�todos

    public Boolean validateObject( Product product)
    {
        if (name == null)
            return false;

        if (bar_code == null)
            return false;

        return true;
    }
}


