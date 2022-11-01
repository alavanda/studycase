using System.Collections.Generic;
using System.Linq;

namespace Catalog.Application.Features
{
    public class ApiResponse<TData>
    {
        private readonly List<string> _validationErrors;

        public ApiResponse(List<string> validationErrors = null)
        {
            _validationErrors ??= new List<string>();

            if (validationErrors != null)
            {
                _validationErrors.AddRange(validationErrors);
            }
        }

        public TData Data { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsValidResponse => !_validationErrors.Any();
        public List<string> ValidationErrors => _validationErrors;
    }
}
