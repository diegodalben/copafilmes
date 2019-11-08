using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.Dtos
{
    public class GroupDto
    {
        public string Name { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}