using CopaFilmes.BizLogic.Entities;

namespace CopaFilmes.BizLogic.Dtos
{
    public class CompetitionResultDto
    {
        public Movie FirstPlace { get; set; }
        public Movie SecondPlace { get; set; }
        public Movie ThirdPlace { get; set; }
    }
}