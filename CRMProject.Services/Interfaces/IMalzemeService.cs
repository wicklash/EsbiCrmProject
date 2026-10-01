using CRMProject.Data.Entities;
using CRMProject.Services.Models;

namespace CRMProject.Services.Interfaces;

public interface IMalzemeService
{
    Task<List<Malzeme>> ListeAsync(string aramaMetni = "", string kategori = "");
    Task<List<string>> KategoriListesiAsync();
    List<string> KategoriListesi();
    Task<Malzeme?> GetirAsync(int id);
    Task<MalzemeViewModel?> GetirViewModelAsync(int id);
    Task<bool> KodMevcutMuAsync(string malzemeKodu, int? haricId = null);
    Task EkleAsync(MalzemeViewModel model);
    Task GüncelleAsync(int id, MalzemeViewModel model);
    Task PasifYapAsync(int id);
    Task<int> ToplamSayıAsync();
    Task<List<Malzeme>> KritikStoklarAsync();
}
