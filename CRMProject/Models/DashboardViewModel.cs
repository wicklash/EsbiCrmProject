using System.Collections.Generic;
using CRMProject.Data.Entities;

namespace CRMProject.Models;

public class DashboardViewModel
{
    public int ToplamCari { get; set; }
    public int ToplamMalzeme { get; set; }
    public List<Malzeme> KritikStoklar { get; set; } = new();
    public List<Cari> SonCariler { get; set; } = new();
}
