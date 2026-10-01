using CRMProject.Data.Context;
using CRMProject.Data.Entities;
using CRMProject.Services.Interfaces;
using CRMProject.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMProject.Services.Implementations;

public class MalzemeService : IMalzemeService
{
    private readonly AppDbContext _context;

    public MalzemeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Malzeme>> ListeAsync(string aramaMetni = "", string kategori = "")
    {
        var query = _context.Malzemes.Where(m => m.AktifMi);

        if (!string.IsNullOrWhiteSpace(aramaMetni))
        {
            var text = aramaMetni.Trim().ToLower();
            query = query.Where(m =>
                m.MalzemeKodu.ToLower().Contains(text) ||
                m.MalzemeAdı.ToLower().Contains(text) ||
                (m.Açıklama != null && m.Açıklama.ToLower().Contains(text))
            );
        }

        if (!string.IsNullOrWhiteSpace(kategori))
        {
            query = query.Where(m => m.Kategori == kategori);
        }

        return await query.OrderBy(m => m.MalzemeAdı).ToListAsync();
    }

    public async Task<List<string>> KategoriListesiAsync()
    {
        return await _context.Malzemes
            .Where(m => m.AktifMi && !string.IsNullOrWhiteSpace(m.Kategori))
            .Select(m => m.Kategori!)
            .Distinct()
            .OrderBy(k => k)
            .ToListAsync();
    }

    public List<string> KategoriListesi()
    {
        return _context.Malzemes
            .Where(m => m.AktifMi && !string.IsNullOrWhiteSpace(m.Kategori))
            .Select(m => m.Kategori!)
            .Distinct()
            .OrderBy(k => k)
            .ToList();
    }

    public async Task<Malzeme?> GetirAsync(int id)
    {
        return await _context.Malzemes.FirstOrDefaultAsync(m => m.Id == id && m.AktifMi);
    }

    public async Task<MalzemeViewModel?> GetirViewModelAsync(int id)
    {
        var malzeme = await GetirAsync(id);
        if (malzeme == null) return null;

        return new MalzemeViewModel
        {
            Id = malzeme.Id,
            MalzemeKodu = malzeme.MalzemeKodu,
            MalzemeAdı = malzeme.MalzemeAdı,
            Kategori = malzeme.Kategori,
            Birim = malzeme.Birim,
            SatışFiyatı = malzeme.SatışFiyatı,
            AlışFiyatı = malzeme.AlışFiyatı,
            StokMiktarı = malzeme.StokMiktarı,
            KritikStok = malzeme.KritikStok,
            Açıklama = malzeme.Açıklama
        };
    }

    public async Task<bool> KodMevcutMuAsync(string malzemeKodu, int? haricId = null)
    {
        return await _context.Malzemes.AnyAsync(m =>
            m.MalzemeKodu.ToLower() == malzemeKodu.Trim().ToLower() &&
            (haricId == null || m.Id != haricId.Value));
    }

    public async Task EkleAsync(MalzemeViewModel model)
    {
        var malzeme = new Malzeme
        {
            MalzemeKodu = model.MalzemeKodu.Trim(),
            MalzemeAdı = model.MalzemeAdı.Trim(),
            Kategori = model.Kategori?.Trim(),
            Birim = model.Birim.Trim(),
            SatışFiyatı = model.SatışFiyatı,
            AlışFiyatı = model.AlışFiyatı,
            StokMiktarı = model.StokMiktarı,
            KritikStok = model.KritikStok,
            Açıklama = model.Açıklama?.Trim(),
            AktifMi = true,
            OluşturmaTarihi = DateTime.Now
        };

        _context.Malzemes.Add(malzeme);
        await _context.SaveChangesAsync();
    }

    public async Task GüncelleAsync(int id, MalzemeViewModel model)
    {
        var malzeme = await _context.Malzemes.FirstOrDefaultAsync(m => m.Id == id);
        if (malzeme == null) return;

        malzeme.MalzemeKodu = model.MalzemeKodu.Trim();
        malzeme.MalzemeAdı = model.MalzemeAdı.Trim();
        malzeme.Kategori = model.Kategori?.Trim();
        malzeme.Birim = model.Birim.Trim();
        malzeme.SatışFiyatı = model.SatışFiyatı;
        malzeme.AlışFiyatı = model.AlışFiyatı;
        malzeme.StokMiktarı = model.StokMiktarı;
        malzeme.KritikStok = model.KritikStok;
        malzeme.Açıklama = model.Açıklama?.Trim();
        malzeme.GüncellemeTarihi = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task PasifYapAsync(int id)
    {
        var malzeme = await _context.Malzemes.FirstOrDefaultAsync(m => m.Id == id);
        if (malzeme == null) return;

        malzeme.AktifMi = false;
        malzeme.GüncellemeTarihi = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    public async Task<int> ToplamSayıAsync()
    {
        return await _context.Malzemes.CountAsync(m => m.AktifMi);
    }

    public async Task<List<Malzeme>> KritikStoklarAsync()
    {
        return await _context.Malzemes
            .Where(m => m.AktifMi && m.StokMiktarı <= m.KritikStok)
            .OrderBy(m => m.StokMiktarı)
            .ToListAsync();
    }
}
