using Castle.Core.Logging;
using di.proyecto.clase._2025.Backend.Modelos;
using di.proyecto.clase._2025.Backend.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace di.proyecto.clase._2025.Backend.Servicios
{
    public class ArticuloRepository : GenericRepository<Articulo>
    {
        
        public ArticuloRepository(DiinventarioexamenContext context, ILogger<GenericRepository<Articulo>> logger)
            : base(context, logger)
        {
            
        }


            /// <summary>
            /// Devuelve el último valor de la propiedad id indicada para el tipo de entidad T.
            /// Útil cuando la tabla no usa id autoincremental y necesitas calcular el siguiente id a partir del máximo actual.
            /// Uso: await repo.GetLastIdAsync(x => x.Idmodeloarticulo);
            /// </summary>
            /// <typeparam name="TKey">Tipo de la propiedad id (por ejemplo int, long, string).</typeparam>
            /// <param name="idSelector">Expresión que selecciona la propiedad id (ej: x => x.Idarticulo).</param>
            /// <param name="cancellationToken">Token de cancelación.</param>
            /// <returns>El valor máximo de la propiedad id. Si no hay registros devuelve default(TKey).</returns>
        public async Task<int?> GetLastIdAsync<TKey>(Expression<Func<Articulo, int>> idSelector, CancellationToken cancellationToken = default)
        {
            if (idSelector == null) throw new ArgumentNullException(nameof(idSelector));

            try
            {
                return await _dbSet
            .AsNoTracking()
            .MaxAsync(idSelector, cancellationToken)
            .ConfigureAwait(false);
            }
            catch (InvalidOperationException)
            {
                // Tabla vacía
                return null;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(
                    $"Error al obtener el último id del tipo {typeof(Articulo).FullName}", ex);
            }
        }

        public async Task<bool> EsNumSerieUnicoAsync(string? numserie, int? idArticuloActual = null)
        {
            if (string.IsNullOrWhiteSpace(numserie))
                throw new ArgumentException("El número de serie no puede ser nulo o vacío.", nameof(numserie));

            try
            {
                // Verifica si existe algún artículo con el mismo número de serie, excluyendo el artículo actual si se proporciona
                return !await _dbSet.AsNoTracking()
                    .AnyAsync(a => a.Numserie == numserie && (!idArticuloActual.HasValue || a.Idarticulo != idArticuloActual.Value));
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Error al comprobar la unicidad del número de serie.", ex);
            }
        }

    }

        
    }


    

