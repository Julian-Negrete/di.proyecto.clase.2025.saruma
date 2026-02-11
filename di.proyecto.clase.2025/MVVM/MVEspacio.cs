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
        //repositorios para acceder a los datos de la base de datos
        EspacioRepository espacioRepository;
        private GenericRepository<Espacio> _espacioRepository;
        private UsuarioRepository _usuarioRepository;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private DepartamentoRepository _departamentoRepository;
        private Articulo _articuloSeleccionado;
        private ArticuloRepository _articuloRepository;

        // Listas para almacenar los datos obtenidos de la base de datos
        private List<Espacio> _espacios;
        private List<Departamento> _listaDepartamentos;
        private List<Usuario> _listaUsuarios;
        private List<Modeloarticulo> _listaModelosArticulos;
        private List<String> _listaEstado;

        public List<Espacio> Espacios { 
            get => _espacios; 
            set { _espacios = value; OnPropertyChanged(nameof(Espacios)); } 
        }

        public Articulo ArticuloSeleccionado { 
            get => _articuloSeleccionado; 
            set { _articuloSeleccionado = value; OnPropertyChanged(nameof(ArticuloSeleccionado)); } 
        }
        public List<Departamento> ListaDepartamentos { 
            get => _listaDepartamentos; 
            set { _listaDepartamentos = value; OnPropertyChanged(nameof(ListaDepartamentos)); }
        }

        public List<Usuario> ListaUsuarios { 
            get => _listaUsuarios; 
            set { _listaUsuarios = value; OnPropertyChanged(nameof(ListaUsuarios)); }
        }

        public List<Modeloarticulo> ListaModelosArticulos { 
            get => _listaModelosArticulos; 
            set { _listaModelosArticulos = value; OnPropertyChanged(nameof(ListaModelosArticulos)); }
        }

        public List<String> ListaEstado { 
            get => _listaEstado; 
            set { _listaEstado = value; OnPropertyChanged(nameof(ListaEstado)); }
        }



        public MVEspacio(EspacioRepository espacioRepository, DepartamentoRepository departamentoRepository, ArticuloRepository articuloRepository, UsuarioRepository usuarioRepository, ModeloArticuloRepository modeloArticuloRepository)
        {

            _espacioRepository = espacioRepository;
            _departamentoRepository = departamentoRepository;
            _articuloRepository = articuloRepository;
            _usuarioRepository = usuarioRepository;
            _modeloArticuloRepository = modeloArticuloRepository;
        }



        public async Task Inicializa()
        {
            _espacios = await GetAllAsync<Espacio>(_espacioRepository);
            //_listaEstado = await _articuloRepository.GetEstado();
            _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
            _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
            _listaModelosArticulos = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
        }

    }
}
