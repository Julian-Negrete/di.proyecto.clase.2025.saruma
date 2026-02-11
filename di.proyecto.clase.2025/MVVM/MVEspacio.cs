using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using di.proyecto.clase._2025.Frontend.MVVM.Base;
using DI.tema2.ejercicio7.Frontend.Mensajes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.MVVM
{

    public class MVEspacio : MVBase
    {
        //repositorios para acceder a los datos de la base de datos
        //EspacioRepository espacioRepository;
        private GenericRepository<Espacio> _espacioRepository;
        private UsuarioRepository _usuarioRepository;
        private ModeloArticuloRepository _modeloArticuloRepository;
        private DepartamentoRepository _departamentoRepository;
        private Articulo _articuloSeleccionado;
        private Articulo _Articulo;
        private ArticuloRepository _articuloRepository;
        public Articulo articulo

        {
            get => _Articulo;
            set => SetProperty(ref _Articulo, value);
        }

        // Listas para almacenar los datos obtenidos de la base de datos
        private List<Espacio> _espacios;
        private List<Departamento> _listaDepartamentos;
        private List<Usuario> _listaUsuarios;
        private List<Modeloarticulo> _listaModelosArticulos;
        private ObservableCollection<string> _listaEstado;

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

        public ObservableCollection<string> ListaEstado
        {
            get => _listaEstado;
            set
            {
                _listaEstado = value;
                OnPropertyChanged(nameof(ListaEstado));
            }
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

            ListaEstado = new ObservableCollection<string>
                {
                    "operativo",
                    "no operativo"
                };
        }

        public async Task<bool> GuardarArticuloAsync()
        {
            if (ArticuloSeleccionado == null)
                return false;

            bool correcto = true;

            

            try
            {
                if (ArticuloSeleccionado.Idarticulo == 0)
                {

                    bool repiteNumSerie = await _articuloRepository
                        .EsNumSerieUnicoAsync(ArticuloSeleccionado.Numserie);

                    if (repiteNumSerie)
                    {
                        MensajeError.Mostrar("GESTIÓN ARTÍCULOS",
                            "El número de serie ya existe en otro artículo.\n" +
                            "Por favor, introduce un número de serie único.", 0);
                        return false;
                    }

                    int? ultimoId = await _articuloRepository
                        .GetLastIdAsync(a => a.Idarticulo);

                    ArticuloSeleccionado.Idarticulo = (ultimoId ?? 0) + 1;

                    await _articuloRepository.AddAsync(ArticuloSeleccionado);
                }
                else
                {
                    await _articuloRepository.UpdateAsync(ArticuloSeleccionado);
                }
            }
            catch
            {
                correcto = false;
            }

            return correcto;
        }

    }
}
