namespace Model;

public class Stocks
{
    private int quantity;
    private Product product;
    private Store store;

    private Stocks(){
        
    }
    public Store GetStore(){
        return store;
    }
    public int GetQuantity(){
        return quantity;
    }
    public Product GetProduct(){
        return product;
    }
    public void SetQuantity(int quantity){
        if(quantity >0){
            this.quantity = quantity;
            return;
        }
    }
    public void setStore(Store store){
        this.store = store;
    }
    public void setProduct(Product product){
        this.product = product;
    }
}
