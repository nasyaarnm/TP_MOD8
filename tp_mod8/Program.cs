// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using tpmod8_1302223148;

public class Program
{
    public static void Main(string[] args)
    {
        double suhuTubuh;
        int hariDemam;

        CovidData dataCovid = new CovidData();

        Console.Write("Berapa suhu badan Anda saat ini: ");
        suhuTubuh = Convert.ToDouble(Console.ReadLine());

        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam: ");
        hariDemam = Convert.ToInt32(Console.ReadLine());

        bool dalamWaktu = hariDemam < dataCovid.Konfigurasi.batas_hari_demam;
        bool terimaFahrenheit = (dataCovid.Konfigurasi.satuan_suhu == "fahrenheit") &&
                                (suhuTubuh >= 97.7 && suhuTubuh <= 99.5);
        bool terimaCelcius = (dataCovid.Konfigurasi.satuan_suhu == "celcius") &&
                             (suhuTubuh >= 36.5 && suhuTubuh <= 37.5);

        if (dalamWaktu && (terimaCelcius || terimaFahrenheit))
        {
            Console.WriteLine(dataCovid.Konfigurasi.pesan_diterima);
        }
        else
        {
            Console.WriteLine(dataCovid.Konfigurasi.pesan_ditolak);
        }

    }
}
