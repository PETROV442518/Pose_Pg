namespace POSE.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using POSE.Data;
    using POSE.Services.Dtos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ReceptServices" />
    /// </summary>
    public class ReceptServices : IReceiptServices
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly POSEDbContext _context;

        /// <summary>
        /// Defines the _mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceptServices"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="POSEDbContext"/></param>
        /// <param name="mapper">The mapper<see cref="IMapper"/></param>
        public ReceptServices(POSEDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// The ReturnReceiptsByUser
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{ReceiptDto}}"/></returns>
        public async Task<List<ReceiptDto>> ReturnReceiptsByUser(string id)
        {
            var result = new List<ReceiptDto>();

            await Task.Run(() =>
            {
                var receipts = this._context.Receipts.Where(a => a.ClientId == id)
                .Include(a => a.DrugStore)
                .Include(a => a.Drugs)
                .ToList();

                foreach (var receipt in receipts)
                {
                    var drugsDto = new List<DrugDto>();
                    foreach (var drug in receipt.Drugs)
                    {
                        drugsDto.Add(_mapper.Map<DrugDto>(drug));
                    }
                    var store = _mapper.Map<DrugStoreDto>(receipt.DrugStore);

                    var dto = _mapper.Map<ReceiptDto>(receipt);
                    dto.Drugs = drugsDto;
                    dto.DrugStore = store;
                    result.Add(dto);
                }
            });
            return result;
        }

        /// <summary>
        /// The ReturnReceiptsByStore
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <returns>The <see cref="Task{List{ReceiptDto}}"/></returns>
        public async Task<List<ReceiptDto>> ReturnReceiptsByStore(string id)
        {
            var result = new List<ReceiptDto>();

            await Task.Run(() =>
            {
                var receipts = this._context.Receipts.Where(a => a.DrugStoreId == id)
                .Include(a => a.DrugStore)
                .Include(a => a.Drugs)
                .ToList();

                foreach (var receipt in receipts)
                {
                    var drugsDto = new List<DrugDto>();
                    foreach (var drug in receipt.Drugs)
                    {
                        drugsDto.Add(_mapper.Map<DrugDto>(drug));
                    }
                    var store = _mapper.Map<DrugStoreDto>(receipt.DrugStore);

                    var dto = _mapper.Map<ReceiptDto>(receipt);
                    dto.Drugs = drugsDto;
                    dto.DrugStore = store;
                    result.Add(dto);
                }
            });
            return result;
        }
    }
}
