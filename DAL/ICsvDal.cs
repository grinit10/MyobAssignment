using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DAL
{
    public interface ICsvDal<T> where T : class
    {
        IEnumerable<T> ReadCsv(IFormFile file);

        byte[] WriteCsv(IEnumerable<T> recs);

    }
}