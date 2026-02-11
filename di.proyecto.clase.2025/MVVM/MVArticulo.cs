
using di.proyecto.clase._2025.Backend.Modelos;

using di.proyecto.clase._2025.Backend.Servicios;

using di.proyecto.clase._2025.Frontend.MVVM.Base;
using DI.tema2.ejercicio7.Frontend.Mensajes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace di.proyecto.clase._2025.MVVM
{
    public class MVArticulo : MVBase
    {
        #region Campos y propiedades privadas
        /// <summary>
        /// Objeto que guarda el modelo de artículo actual
        /// Está vinculado a la vista para mostrar y editar los datos del artículo
        /// </summary>
        private Modeloarticulo _modeloArticulo;
        private Articulo _Articulo;
        private Espacio _espacioArticulo;
        /// <summary>
        /// Repositorio para gestionar las operaciones de datos relacionadas con los modelos de artículo
        /// </summary>
        private ModeloArticuloRepository _modeloArticuloRepository;
        private ArticuloRepository _articuloRepository;

        /// <summary>
        /// Repositorio para gestionar las operaciones de datos relacionadas con los tipos de artículo
        /// </summary>
        private TipoArticuloRepository _tipoArticuloRepository;
        private UsuarioRepository _usuarioRepository;
        private DepartamentoRepository _departamentoRepository;
        private EspacioRepository _espacioRepository;
        /// <summary>
        /// lista de tipos de artículos disponibles
        /// </summary>
        private List<Tipoarticulo> _listaTipoArticulos;
        private List<Usuario> _listaUsuarios;
        private List<Departamento> _listaDepartamentos;
        private List<Espacio> _listaEspacios;
        private List<Modeloarticulo> _listaModelosArticulos;
        private List<Articulo> _listaArticulos;
        private List<Predicate<Articulo>> criteriosArticulos;
        private Predicate<Articulo> criterioModelo;
        private Predicate<Articulo> criterioEspacio;
        private Predicate<Articulo> criterioUsuarioAlta;

        #endregion
        #region Getters y Setters
        public List<Tipoarticulo> listaTiposArticulos => _listaTipoArticulos;
        public List<Usuario> listaUsuarios => _listaUsuarios;
        public List<Departamento> listaDepartamentos => _listaDepartamentos;
        public List<Espacio> listaEspacios => _listaEspacios;

        public ListCollectionView listaArticulos { get; set; }

        public List<Modeloarticulo> listaModelosArticulos => _listaModelosArticulos;

        public Modeloarticulo modeloArticulo
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }
        public Articulo articulo

        {
            get => _Articulo;
            set => SetProperty(ref _Articulo, value);
        }
        public Modeloarticulo modeloArticuloSeleccionado
        {
            get => _modeloArticulo;
            set => SetProperty(ref _modeloArticulo, value);
        }
        public Espacio espacioArticuloSeleccionado { 
            get=>_espacioArticulo; 
            set=> SetProperty(ref _espacioArticulo, value); }
        public ListCollectionView listaArticulosFiltro => listaArticulos;
        public Predicate<object> predicadorFiltro;

        #endregion
        // Aquí puedes añadir propiedades y métodos específicos para el ViewModel de Artículo
        public MVArticulo(ModeloArticuloRepository modeloArticuloRepository, TipoArticuloRepository tipoArticuloRepository, ArticuloRepository articuloRepository, UsuarioRepository usuarioRepository, DepartamentoRepository departamentoRepository, EspacioRepository espacioRepository)
        {
            _modeloArticuloRepository = modeloArticuloRepository;
            _tipoArticuloRepository = tipoArticuloRepository;
            _modeloArticulo = new Modeloarticulo();

            _articuloRepository = articuloRepository;
            _usuarioRepository = usuarioRepository;
            _departamentoRepository = departamentoRepository;
            _espacioRepository = espacioRepository;
            //_Articulo = new Articulo();

        }

        public async Task Inicializa()
        {
            try
            {

                await InicializaListas();
                InicializaFiltros();

                predicadorFiltro = new Predicate<object>(FiltroCriterios);

            }
            catch (Exception ex)
            {
                MensajeError.Mostrar("GESTIÓN ARTÍCULOS", "Error al cargar los tipos de artículos\n" +
                    "No puedo conectar con la base de datos", 0);
            }
        }

        public async Task<bool> GuardarModeloArticuloAsync()
        {
            bool correcto = true;
            try
            {
                if (modeloArticulo.Idmodeloarticulo == 0)
                {
                    // Nuevo modelo de artículo
                    await _modeloArticuloRepository.AddAsync(modeloArticulo);
                }
                else
                {
                    // Actualizar modelo de artículo existente
                    await _modeloArticuloRepository.UpdateAsync(modeloArticulo);
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y la registramos en el log
                correcto = false;
            }
            return correcto;
        }

        public void Filtrar()
        {
            AddCriterios();
            listaArticulosFiltro.Filter = predicadorFiltro;
        }
        public void LimpiarFiltro()
        {

            modeloArticuloSeleccionado = null;
            espacioArticuloSeleccionado = null;
            //articulo.Usuarioalta = null;
            listaArticulosFiltro.Filter = null;
        }
        public async Task<bool> GuardarArticuloAsync()
        {
            bool correcto = true;
            
            try
            {
                if (articulo.Idarticulo == 0)
                {

                    bool repiteNumSerie = await _articuloRepository.EsNumSerieUnicoAsync(articulo.Numserie);
                    if (repiteNumSerie)
                    {
                        MensajeError.Mostrar("GESTIÓN ARTÍCULOS", "El número de serie ya existe en otro artículo.\n" +
                            "Por favor, introduce un número de serie único.", 0);
                        return false;
                    }

                    // Nuevo modelo de artículo
                    int? ultimoId = await _articuloRepository.GetLastIdAsync(a => a.Idarticulo);
                    articulo.Idarticulo = (ultimoId ?? 0) + 1;
                    await _articuloRepository.AddAsync(articulo);
                }
                else
                {
                    // Actualizar modelo de artículo existente
                    await _articuloRepository.UpdateAsync(articulo);
                }
            }
            catch (Exception ex)
            {
                // Capturamos la excepción y la registramos en el log
                correcto = false;
            }
            return correcto;
        }

        #region Metodos privados
        private void InicializaFiltros()
        {
            criterioModelo = new Predicate<Articulo>(a => a.ModeloNavigation != null && a.ModeloNavigation.Equals(modeloArticuloSeleccionado));
            //criterioUsuarioAlta = new Predicate<Articulo>(a => a.Usuarioalta != null && a.Usuarioalta.Equals(articulo.Usuarioalta));
            criterioEspacio = new Predicate<Articulo>(a => a.EspacioNavigation != null && a.EspacioNavigation.Equals(espacioArticuloSeleccionado));
        }

        private async Task InicializaListas()
        {
            _listaModelosArticulos = await GetAllAsync<Modeloarticulo>(_modeloArticuloRepository);
            _listaTipoArticulos = await GetAllAsync<Tipoarticulo>(_tipoArticuloRepository);
            _listaDepartamentos = await GetAllAsync<Departamento>(_departamentoRepository);
            _listaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
            _listaEspacios = await GetAllAsync<Espacio>(_espacioRepository);
            _listaArticulos = await GetAllAsync<Articulo>(_articuloRepository);
            listaArticulos = new ListCollectionView(_listaArticulos.ToList());
            criteriosArticulos = new List<Predicate<Articulo>>();
        }

        private void AddCriterios()
        {
            criteriosArticulos.Clear();
            if (modeloArticuloSeleccionado != null) { criteriosArticulos.Add(criterioModelo); }
            //if (articulo.Usuarioalta != null) { criteriosArticulos.Add(criterioUsuarioAlta); }
            if (espacioArticuloSeleccionado != null) { criteriosArticulos.Add(criterioEspacio); }

        }

        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Articulo art = (Articulo)item;
            if (criteriosArticulos != null)
            {
                correcto = criteriosArticulos.TrueForAll(x => x(art));
            }
            return correcto;
        }


        #endregion

    }
}
