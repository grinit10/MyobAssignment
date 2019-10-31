using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Http;

namespace DAL
{
    public class CsvDal<T> : ICsvDal<T> where T: class
    {
        public IEnumerable<T> ReadCsv(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            return new CsvReader(reader).GetRecords<T>().ToList();;
        }
        
        public byte[] WriteCsv(IEnumerable<T> recs)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords<T>(recs);
            }
            return memoryStream.ToArray();
        }
    }
}