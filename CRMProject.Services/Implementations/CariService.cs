using CRMProject.Data.Context;
using CRMProject.Data.Entities;
using CRMProject.Services.Interfaces;
using CRMProject.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMProject.Services.Implementations;

public class CariService : ICariService
{
    private readonly AppDbContext _context;

    public CariService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cari>> ListeAsync(string aramaMetni = "")
    {
        var query = _context.Caris.Where(c => c.AktifMi);

        if (!string.IsNullOrWhiteSpace(aramaMetni))
        {
            var text = aramaMetni.Trim().ToLower();
            query = query.Where(c =>
                c.CariKodu.ToLower().Contains(text) ||
                c.Ünvan.ToLower().Contains(text) ||
                (c.VergiNo != null && c.VergiNo.ToLower().Contains(text)) ||
                (c.Telefon != null && c.Telefon.ToLower().Contains(text)) ||
                (c.E_posta != null && c.E_posta.ToLower().Contains(text)) ||
                (c.İl != null && c.İl.ToLower().Contains(text)) ||
                (c.İlçe != null && c.İlçe.ToLower().Contains(text))
            );
        }

        return await query.OrderByDescending(c => c.OluşturmaTarihi).ToListAsync();
    }

    public async Task<Cari?> GetirAsync(int id)
    {
        return await _context.Caris.FirstOrDefaultAsync(c => c.Id == id && c.AktifMi);
    }

    public async Task<CariViewModel?> GetirViewModelAsync(int id)
    {
        var cari = await GetirAsync(id);
        if (cari == null) return null;

        return new CariViewModel
        {
            Id = cari.Id,
            CariKodu = cari.CariKodu,
            Ünvan = cari.Ünvan,
            CariTipi = cari.CariTipi,
            VergiDairesi = cari.VergiDairesi,
            VergiNo = cari.VergiNo,
            Telefon = cari.Telefon,
            EPosta = cari.E_posta,
            Adres = cari.Adres,
            İl = cari.İl,
            İlçe = cari.İlçe
        };
    }

    public async Task<bool> KodMevcutMuAsync(string cariKodu, int? haricId = null)
    {
        return await _context.Caris.AnyAsync(c =>
            c.CariKodu.ToLower() == cariKodu.Trim().ToLower() &&
            (haricId == null || c.Id != haricId.Value));
    }

    public async Task EkleAsync(CariViewModel model)
    {
        var cari = new Cari
        {
            CariKodu = model.CariKodu.Trim(),
            Ünvan = model.Ünvan.Trim(),
            CariTipi = model.CariTipi,
            VergiDairesi = model.VergiDairesi?.Trim(),
            VergiNo = model.VergiNo?.Trim(),
            Telefon = model.Telefon?.Trim(),
            E_posta = model.EPosta?.Trim(),
            Adres = model.Adres?.Trim(),
            İl = model.İl?.Trim(),
            İlçe = model.İlçe?.Trim(),
            AktifMi = true,
            OluşturmaTarihi = DateTime.Now
        };

        _context.Caris.Add(cari);
        await _context.SaveChangesAsync();
    }

    public async Task GüncelleAsync(int id, CariViewModel model)
    {
        var cari = await _context.Caris.FirstOrDefaultAsync(c => c.Id == id);
        if (cari == null) return;

        cari.CariKodu = model.CariKodu.Trim();
        cari.Ünvan = model.Ünvan.Trim();
        cari.CariTipi = model.CariTipi;
        cari.VergiDairesi = model.VergiDairesi?.Trim();
        cari.VergiNo = model.VergiNo?.Trim();
        cari.Telefon = model.Telefon?.Trim();
        cari.E_posta = model.EPosta?.Trim();
        cari.Adres = model.Adres?.Trim();
        cari.İl = model.İl?.Trim();
        cari.İlçe = model.İlçe?.Trim();
        cari.GüncellemeTarihi = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task PasifYapAsync(int id)
    {
        var cari = await _context.Caris.FirstOrDefaultAsync(c => c.Id == id);
        if (cari == null) return;

        cari.AktifMi = false;
        cari.GüncellemeTarihi = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    public async Task<int> ToplamSayıAsync()
    {
        return await _context.Caris.CountAsync(c => c.AktifMi);
    }

    public async Task<List<Cari>> SonEklenenlerAsync(int adet = 5)
    {
        return await _context.Caris
            .Where(c => c.AktifMi)
            .OrderByDescending(c => c.OluşturmaTarihi)
            .Take(adet)
            .ToListAsync();
    }
}
