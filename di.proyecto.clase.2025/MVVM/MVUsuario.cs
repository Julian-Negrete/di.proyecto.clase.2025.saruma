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
    public class MVUsuario :MVBase
    {
        UsuarioRepository _usuarioRepository;
        private GenericRepository<Rol> _rolRepository;
        private GenericRepository<Grupo> _grupoRepository;


        private List<Usuario> _listaUsuarios;
        private List<Rol> _listaRoles;
        private List<Grupo> _listaGrupos;
        private Usuario _usuario;

        public Usuario Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        public List<Usuario> ListaUsuarios { 
            get => _listaUsuarios; 
            set { _listaUsuarios = value; OnPropertyChanged(nameof(ListaUsuarios)); }
        }

        public List<Rol> ListaRoles { 
            get => _listaRoles; 
            set { _listaRoles = value; OnPropertyChanged(nameof(ListaRoles)); }
        }

        public List<Grupo> ListaGrupos { 
            get => _listaGrupos; 
            set { _listaGrupos = value; OnPropertyChanged(nameof(ListaGrupos)); }
        }

        public MVUsuario(UsuarioRepository usuarioRepository, GenericRepository<Rol> rolRepository, GenericRepository<Grupo> grupoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _grupoRepository = grupoRepository;
        }

        public async Task Inicializa() {
            
            ListaUsuarios = await GetAllAsync<Usuario>(_usuarioRepository);
            _listaRoles = await GetAllAsync<Rol>(_rolRepository);
            _listaGrupos = await GetAllAsync<Grupo>(_grupoRepository);
        }
    }
}
