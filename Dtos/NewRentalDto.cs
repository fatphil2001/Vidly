using System.Collections.Generic;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        public int ClientId { get; set; }
        public IEnumerable<int> MovieIds { get; set; }
    }
}
