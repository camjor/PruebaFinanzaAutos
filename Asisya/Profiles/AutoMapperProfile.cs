using AutoMapper;
using Asisya.Models;

// CATEGORY
using Asisya.Dtos.CategoryDtos;

// PRODUCT
using Asisya.Dtos.ProductDtos;

// CUSTOMER
using Asisya.Dtos.CustomerDtos;

// ORDER + ORDER DETAIL
using Asisya.Dtos.OrderDtos;
using Asisya.Dtos.OrderDetailDtos;

// SUPPLIER
using Asisya.Dtos.SupplierDtos;

// EMPLOYEE
using Asisya.Dtos.EmployeeDtos;

// SHIPPER
using Asisya.Dtos.ShipperDtos;

namespace Asisya.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // ------------------------------------------
        // CATEGORY
        // ------------------------------------------
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<CategoryRequestDto, Category>();

        // ------------------------------------------
        // SUPPLIER
        // ------------------------------------------
        CreateMap<Supplier, SupplierResponseDto>();
        CreateMap<SupplierRequestDto, Supplier>();

        // ------------------------------------------
        // PRODUCT
        // ------------------------------------------
        CreateMap<ProductRequestDto, Product>();

        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
            .ForMember(dest => dest.SupplierName,
                opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.CompanyName : null));

        // ------------------------------------------
        // CUSTOMER
        // ------------------------------------------
        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<CustomerRequestDto, Customer>();

        // ------------------------------------------
        // EMPLOYEE
        // ------------------------------------------
        CreateMap<EmployeeRequestDto, Employee>();

        CreateMap<Employee, EmployeeResponseDto>()
            .ForMember(dest => dest.ManagerName,
                opt => opt.MapFrom(src =>
                    src.Manager != null
                        ? $"{src.Manager.FirstName} {src.Manager.LastName}"
                        : null
                ));

        // ------------------------------------------
        // SHIPPER
        // ------------------------------------------
        CreateMap<ShipperRequestDto, Shipper>();
        CreateMap<Shipper, ShipperResponseDto>();

        // ------------------------------------------
        // ORDER
        // ------------------------------------------
        CreateMap<OrderRequestDto, Order>()
            .ForMember(dest => dest.OrderDate,
                opt => opt.MapFrom(src => DateTime.Now)); // asignación automática

        CreateMap<Order, OrderResponseDto>()
            .ForMember(dest => dest.OrderDetails,
                opt => opt.MapFrom(src => src.OrderDetails));

        // ------------------------------------------
        // ORDER DETAIL
        // ------------------------------------------
        CreateMap<OrderDetailRequestDto, OrderDetail>();

        CreateMap<OrderDetail, OrderDetailResponseDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.ProductName : null
                ));
    }
}
