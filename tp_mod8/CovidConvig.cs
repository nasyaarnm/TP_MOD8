using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace tpmod8_1302223148
{
    internal class CovidConfig
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_demam { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        public void UbahSatuan()
        {
            if (satuan_suhu == "celcius")
            {
                satuan_suhu = "fahrenheit";
            }
            else
            {
                satuan_suhu = "celcius";
            }
        }
    }

    internal class CovidData
    {
        public CovidConfig Konfigurasi;

        public CovidData()
        {
            bacaData();
        }

        private void bacaData()
        {
            const string filePath = @"covid_config.json";

            if (!File.Exists(filePath))
            {
                Config();
                SaveConfig();
            }
            else
            {
                string jsonData = File.ReadAllText(filePath);
                Konfigurasi = JsonSerializer.Deserialize<CovidConfig>(jsonData);
            }
        }

        private void Config()
        {
            Konfigurasi = new CovidConfig
            {
                satuan_suhu = "celcius",
                batas_hari_demam = 14,
                pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini",
                pesan_diterima = "Anda dipersilakan untuk masuk ke dalam gedung ini"
            };
        }

        private void SaveConfig()
        {
            const string filePath = @"covid_config.json";

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string jsonData = JsonSerializer.Serialize(Konfigurasi, options);

            File.WriteAllText(filePath, jsonData);
        }

        public string CheckStatus(double suhu, int hariDemam)
        {
            string pesan;

            if (Konfigurasi.satuan_suhu == "celcius")
            {
                if (suhu >= 36.5 && suhu <= 37.5 && hariDemam < Konfigurasi.batas_hari_demam)
                {
                    pesan = Konfigurasi.pesan_diterima;
                }
                else
                {
                    pesan = Konfigurasi.pesan_ditolak;
                }
            }
            else
            {
                double suhuCelsius = (suhu - 32) * 5 / 9;

                if (suhuCelsius >= 36.5 && suhuCelsius <= 37.5 && hariDemam < Konfigurasi.batas_hari_demam)
                {
                    pesan = Konfigurasi.pesan_diterima;
                }
                else
                {
                    pesan = Konfigurasi.pesan_ditolak;
                }
            }

            return pesan;
        }
    }
}
