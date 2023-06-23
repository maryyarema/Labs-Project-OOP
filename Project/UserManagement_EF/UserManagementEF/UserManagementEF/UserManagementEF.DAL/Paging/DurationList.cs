namespace UserManagementEF.UserManagementEF.DAL.Paging
{

    public class DurationList<TEntity> : List<TEntity>
    {
        public int CurrentFilm { get; private set; }
        public int TotalFilms { get; private set; }
        public int FilmSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentFilm > 1;
        public bool HasNext => CurrentFilm < TotalFilms;

        public DurationList(List<TEntity> entities, int count, int filmNumber, int filmSize)
        {
            TotalFilms = count;
            FilmSize = filmSize;
            CurrentFilm = filmNumber;
            TotalFilms = (int)Math.Ceiling(count / (double)filmSize);

            AddRange(entities);
        }

        public static DurationList<TEntity> ToDurationList(IQueryable<TEntity> source, int filmNumber, int filmSize)
        {
            var count = source.Count();
            var items = source
                .Skip((filmNumber - 1) * filmSize)
                .Take(filmSize)
                .ToList();

            return new DurationList<TEntity>(items, count, filmNumber, filmSize);
        }
    }
}
