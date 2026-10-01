using CRMProject.Data.Entities;
using CRMProject.Services.Models;

namespace CRMProject.Services.Interfaces;

public interface ICariService
{
    Task<List<Cari>> ListeAsync(string aramaMetni = "");
    Task<Cari?> GetirAsync(int id);
    Task<CariViewModel?> GetirViewModelAsync(int id);
    Task<bool> KodMevcutMuAsync(string cariKodu, int? haricId = null);
    Task EkleAsync(CariViewModel model);
    Task GüncelleAsync(int id, CariViewModel model);
    Task PasifYapAsync(int id);
    Task<int> ToplamSayıAsync();
    Task<List<Cari>> SonEklenenlerAsync(int adet = 5);
}
