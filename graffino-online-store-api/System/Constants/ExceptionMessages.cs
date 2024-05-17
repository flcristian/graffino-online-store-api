namespace graffino_online_store_api.System.Constants;

public static class ExceptionMessages
{
    public const string PRODUCT_DOES_NOT_EXIST = "The product does not exist.";
    public const string PRODUCTS_DO_NOT_EXIST = "There are no products.";
    public const string INVALID_PRODUCT_PRICE = "Price is negative or invalid.";

    public const string ORDER_DETAIL_DOES_NOT_EXIST = "This order detail does not exist.";
    public const string ORDER_DETAILS_DO_NOT_EXIST = "There are no order details.";
    public const string ORDER_DETAIL_ALREADY_EXISTS = "This product has already been added to the order.";
    public const string INVALID_ORDER_DETAIL_PRODUCT_COUNT = "Product count is null, negative or invalid.";
    public const string CART_DOES_NOT_EXIST = "The customer has no cart.";
    public const string CART_ALREADY_EXISTS = "The cart already exists.";
    public const string INVALID_ORDER_STATUS = "Order status is invalid.";
    public const string CUSTOMER_HAS_NO_ORDERS = "This customer has no orders.";

    public const string ORDER_DOES_NOT_EXIST = "This order does not exist.";
    public const string ORDERS_DO_NOT_EXIST = "There are no orders.";

    public const string USER_DOES_NOT_EXIST = "This user does not exist.";
}