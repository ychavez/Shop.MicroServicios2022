using Existence.Grpc.Protos;
using Grpc.Core;
using System.Threading.Tasks;

namespace Existence.Grpc.Services
{
    public class ProductService : ExistenceService.ExistenceServiceBase
    {
        public override async Task<ProductReply> CheckExistence(ProductRequest request, ServerCallContext context)
        {
            //logica de existencia
            await Task.Delay(1);

            return new ProductReply { ProductQty = 90 };
        }
    }
}
