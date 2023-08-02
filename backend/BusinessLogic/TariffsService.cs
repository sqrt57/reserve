using backend.DbStores;
using backend.Models;

namespace backend.BusinessLogic;

public class TariffsService
{
    private readonly TariffsStore _tariffsStore;

    public TariffsService(TariffsStore tariffsStore)
    {
        _tariffsStore = tariffsStore;
    }

    public async Task<IReadOnlyCollection<DbTariff>> GetTariffs()
    {
        return await _tariffsStore.GetTariffs();
    }
}
