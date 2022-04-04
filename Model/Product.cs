namespace Model;

public class Product
{
    // Atributos
    private String name;
    private Double unit_price;
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

    public Double getUnitprice()
    {
        return unit_price;
    }
    public void setUnitPrice(Double unit_price)
    {
        this.unit_price = unit_price;
    }

    public String getBarCode()
    {
        return bar_code;
    }
    public void setBarCode(String bar_code)
    {
        this.bar_code = bar_code;
    }

    
    
    
}


