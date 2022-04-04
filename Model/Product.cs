namespace Model;

public class Product
{
    // Atributos
    private String nome;
    private Double unit_price;
    private String bar_code;

    // Construtor
    public Product()
    {

    }

    // GET & SET
    public String getNome()
    {
        return nome;
    }
    public void setNome(String nome)
    {
        this.nome = nome;
    }

    public Double getUnitPrice()
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


