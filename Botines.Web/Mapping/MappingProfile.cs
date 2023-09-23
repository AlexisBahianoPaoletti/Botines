using AutoMapper;
using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Dtos.Localidad;
using Botines.Entidades.Dtos.Modelo;
using Botines.Entidades.Dtos.Proveedor;
using Botines.Entidades.Dtos.Provincia;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Dtos.Venta;
using Botines.Entidades.Dtos.VentaBotin;
using Botines.Entidades.Dtos.VentaCarrito;
using Botines.Entidades.Entidades;
using Botines.Web.ViewModels.Botin;
using Botines.Web.ViewModels.Carrito;
using Botines.Web.ViewModels.Cliente;
using Botines.Web.ViewModels.Localidad;
using Botines.Web.ViewModels.Marca;
using Botines.Web.ViewModels.Modelo;
using Botines.Web.ViewModels.Pais;
using Botines.Web.ViewModels.Proveedor;
using Botines.Web.ViewModels.Provincia;
using Botines.Web.ViewModels.Talle;
using Botines.Web.ViewModels.Venta;
using Botines.Web.ViewModels.VentaBotin;
using Botines.Web.ViewModels.VentaCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botines.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadPaisesMapping();
            LoadProvinciasMapping();
            LoadLocalidadesMapping();
            LoadTallesMapping();
            LoadMarcasMapping();
            LoadModelosMapping();
            LoadProveedoresMapping();
            LoadClientesMapping();
            LoadBotinesMapping();
            LoadCarritoMapping();
            LoadVentasMapping();
            LoadVentaBotinMapping();
            LoadVentaCarritoMapping();
        }

        private void LoadVentaCarritoMapping()
        {
            CreateMap<VentaCarritoListDto, VentaCarritoListVm>()
            .ForMember(dest => dest.Botin, opt => opt.MapFrom(src => src.NombreBotin));
        }

        private void LoadVentaBotinMapping()
        {
            CreateMap<VentaBotinListDto, VentaBotinListVm>();
        }

        private void LoadVentasMapping()
        {
            CreateMap<VentaListDto, VentaListVm>();
        }

        private void LoadCarritoMapping()
        {
            CreateMap<ItemCarrito, ItemCarritoVm>()
            .ForMember(dest => dest.ItemCarritoId, opt => opt.MapFrom(src => src.ItemCarritoId))
            .ForMember(dest => dest.PrecioVenta, opt => opt.MapFrom(src => src.PrecioUnitario))
                .ForMember(dest => dest.Talle, opt => opt.MapFrom(src => src.TalleBotin.Talle.NumeroTalle));
        }

        private void LoadBotinesMapping()
        {
            CreateMap<BotinListDto, BotinListVm>();
            CreateMap<BotinEditVm, Botin>().ReverseMap();
            CreateMap<Botin, BotinListVm>()
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca.NombreMarca))
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo.NombreModelo));
                //.ForMember(dest => dest.Proveedor, opt => opt.MapFrom(src => src.Localidad.NombreLocalidad));
        }

        private void LoadClientesMapping()
        {
            CreateMap<ClienteListDto, ClienteListVm>();
            CreateMap<ClienteEditVm, Cliente>().ReverseMap();
            CreateMap<Cliente, ClienteListVm>()
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia.NombreProvincia))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Localidad.NombreLocalidad));
        }

        private void LoadProveedoresMapping()
        {
            CreateMap<ProveedorListDto, ProveedorListVm>();
            CreateMap<ProveedorEditVm, Proveedor>().ReverseMap();
            CreateMap<Proveedor, ProveedorListVm>()
                .ForMember(dest => dest.NombrePais, opt => opt.MapFrom(src => src.Pais.NombrePais))
                .ForMember(dest => dest.NombreProvincia, opt => opt.MapFrom(src => src.Provincia.NombreProvincia))
                .ForMember(dest => dest.NombreLocalidad, opt => opt.MapFrom(src => src.Localidad.NombreLocalidad));

        }

        private void LoadModelosMapping()
        {
            CreateMap<ModeloListDto, ModeloListVm>();
            CreateMap<ModeloEditVm, Modelo>().ReverseMap();
            CreateMap<Modelo, ModeloListVm>()
                .ForMember(dest => dest.NombreMarca,
                opt => opt.MapFrom(src => src.Marca.NombreMarca));
        }

        private void LoadMarcasMapping()
        {
            CreateMap<Marca, MarcaListVm>();
            CreateMap<Marca, MarcaEditVm>().ReverseMap();
        }

        private void LoadTallesMapping()
        {
            CreateMap<Talle, TalleListVm>();
            CreateMap<Talle, TalleEditVm>().ReverseMap();
            CreateMap<TalleListDto, TalleListVm>().ReverseMap();
        }

        private void LoadLocalidadesMapping()
        {
            CreateMap<LocalidadListDto, LocalidadListVm>();
            CreateMap<LocalidadEditVm, Localidad>().ReverseMap();
            CreateMap<Localidad, LocalidadListVm>()
                .ForMember(dest => dest.NombreProvincia,
                opt => opt.MapFrom(src => src.Provincia.NombreProvincia));
        }

        private void LoadProvinciasMapping()
        {
            CreateMap<ProvinciaListDto, ProvinciaListVm>();
            CreateMap<ProvinciaEditVm, Provincia>().ReverseMap();
            CreateMap<Provincia, ProvinciaListVm>()
                .ForMember(dest => dest.NombrePais,
                opt => opt.MapFrom(src => src.Pais.NombrePais));
        }

        private void LoadPaisesMapping()
        {
            CreateMap<Pais, PaisListVm>();
            CreateMap<Pais, PaisEditVm>().ReverseMap();
        }
    }
}