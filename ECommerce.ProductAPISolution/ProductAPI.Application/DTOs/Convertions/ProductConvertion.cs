using ProductAPI.Domain.Entities;

namespace ProductAPI.Application.DTOs.Convertions;

public static class ProductConvertion
{
    public static Product ToEntity(ProductDTO productDTO)
    {
        return new Product
        {
            Description = productDTO.Description,
            Id = productDTO.Id,
            Name = productDTO.Name,
            Price = productDTO.Price,
            Quantity = productDTO.Quantity,
        };
    } 

    public static (ProductDTO?, IEnumerable<ProductDTO>?) FromEntity(Product product, IEnumerable<Product> products)
    {
        if (product is not null || products is null) {
            var singleProduct = new ProductDTO
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
            };
            return (singleProduct, null);
        }
        else if(products is not null || product is null)
        {
            var _product = products.Select(p => new ProductDTO
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
            }).ToList();
            return (null, _product);
        }
        else
        {
            var singleProduct = new ProductDTO
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
            };
            var _product = products.Select(p => new ProductDTO
            {
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
            }).ToList();
            return (singleProduct, _product);
        }
    }
}
