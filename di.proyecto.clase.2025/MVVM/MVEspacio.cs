using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Frontend.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.MVVM
{

    public class MVEspacio : MVBase
    {
        EspacioRepository espacioRepository;
        private GenericRepository<Espacio> _espacioRepository;
        private List<Espacio> _espacios;
        public List<Espacio> Espacios => _espacios ; 
        public MVEspacio(EspacioRepository espacioRepository)
        {

            _espacioRepository = espacioRepository;
        }



        public async Task Inicializa()
        {
            _espacios = await GetAllAsync<Espacio>(_espacioRepository);
        }

    }
}
