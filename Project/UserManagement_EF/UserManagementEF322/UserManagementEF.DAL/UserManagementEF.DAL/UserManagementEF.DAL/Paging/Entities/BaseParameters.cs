namespace UserManagementEF.DAL.Paging.Entities
{
    public abstract class BaseParameters
    {
        private const int MaxFilmSize = 400;
        public int Film { get; set; } = 1;
        //MovieDuration
        private int _filmSize = 10;
        public int FilmSize
        {
            get => _filmSize;
            set => _filmSize = value > MaxFilmSize ? MaxFilmSize : value;
        }

        public string? OrderBy { get; set; } = default!; // for sorting
        public string? Fields { get; set; } = default!; // for data shaping
    }
}
