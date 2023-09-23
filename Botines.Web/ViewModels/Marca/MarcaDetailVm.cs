using Botines.Web.ViewModels.Modelo;
using Botines.Web.ViewModels.Pais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Marca
{
    public class MarcaDetailVm
    {
        public MarcaListVm Marca { get; set; }
        public List<ModeloListVm> Modelos { get; set; }
    }
}