using System.Collections.Generic;
using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.Dtos
{
    public class CompetitionBizDto
    {
        public IList<Movie> SelectedMovies { get; set; }
        public IList<GroupDto> GroupPhase { get; set; }
        public IList<GroupDto> QuarterFinals { get; set; }
        public IList<GroupDto> SemiFinals { get; set; }
        public GroupDto Finals { get; set; }
        public CompetitionResultDto CompetitionResult { get; set; }

        public CompetitionBizDto()
        {
            SelectedMovies = new List<Movie>();
        }
    }
}