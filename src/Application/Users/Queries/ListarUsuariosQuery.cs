using Application.Common;
using Application.Users.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class ListarUsuariosQuery : IRequest<PagedResult<UsuarioDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ListarUsuariosQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
