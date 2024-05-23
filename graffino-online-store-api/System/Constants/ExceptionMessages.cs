namespace graffino_online_store_api.System.Constants;

public static class ExceptionMessages
{
    public const string NO_PRODUCTS_FOR_FILTERS = "There are no products matching your search and filter crietrias.";
    public const string PRODUCT_DOES_NOT_EXIST = "The product does not exist.";
    public const string PRODUCTS_DO_NOT_EXIST = "There are no products.";
    public const string CATEGORY_DOES_NOT_EXIST = "The category does not exist.";
    public const string CATEGORIES_DO_NOT_EXIST = "There are no categories.";
    public const string CATEGORY_ALREADY_EXISTS = "There already exists a category with this name.";
    public const string INVALID_PRODUCT_PRICE = "Price is negative or invalid.";
    
    public const string INVALID_ORDER_STATUS = "Order status is invalid.";
    public const string CUSTOMER_HAS_NO_ORDERS = "This customer has no orders.";

    public const string ORDER_DOES_NOT_EXIST = "This order does not exist.";
    public const string ORDERS_DO_NOT_EXIST = "There are no orders.";

    public const string USER_DOES_NOT_EXIST = "This user does not exist.";
}