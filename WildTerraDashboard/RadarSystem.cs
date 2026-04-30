using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace WildTerraDashboard
{
    public class RadarEntity
    {
        public string Tipo { get; set; } // M, R, D, C, P
        public string Nome { get; set; }
        public int Nivel { get; set; } // Variável do Level
        public float Distancia { get; set; }
        public float RelX { get; set; }
        public float RelY { get; set; }
        public bool IsAggressive { get; set; }
        public int WorldId { get; set; }
        public Color Cor { get; set; }
    }

    public static class RadarSystem
    {
        public static List<RadarEntity> ParsePacote(string dadosBrutos)
        {
            List<RadarEntity> lista = new List<RadarEntity>();
            string[] itens = dadosBrutos.Split('~');

            foreach (string item in itens)
            {
                // Formato Esperado: TIPO:NOME:DIST:DX:DZ:AGGRO:LEVEL
                string[] p = item.Split(':');

                if (p.Length >= 3)
                {
                    RadarEntity ent = new RadarEntity();
                    ent.Tipo = p[0];
                    ent.Nome = p[1];

                    bool isHideoutEnter = !string.IsNullOrEmpty(ent.Nome) && ent.Nome.IndexOf("HideoutEnter", StringComparison.OrdinalIgnoreCase) >= 0;

                    // Parse Distância
                    string distStr = p[2].Replace(',', '.');
                    float.TryParse(distStr, NumberStyles.Any, CultureInfo.InvariantCulture, out float dist);
                    ent.Distancia = dist;

                    // Parse Coordenadas
                    if (p.Length >= 5)
                    {
                        string dxStr = p[3].Replace(',', '.');
                        string dyStr = p[4].Replace(',', '.');
                        float.TryParse(dxStr, NumberStyles.Any, CultureInfo.InvariantCulture, out float dx);
                        float.TryParse(dyStr, NumberStyles.Any, CultureInfo.InvariantCulture, out float dy);
                        ent.RelX = dx;
                        ent.RelY = dy;
                    }

                    // Parse Level (Posição 6 no array, que é o 7º item)
                    if (p.Length >= 7)
                    {
                        int.TryParse(p[6], out int lvl);
                        ent.Nivel = lvl;
                    }

                    // Parse WorldId opcional (8º item)
                    if (p.Length >= 8)
                    {
                        int.TryParse(p[7], out int worldId);
                        ent.WorldId = worldId;
                    }

                    // --- CORES ---
                    switch (ent.Tipo)
                    {
                        case "P": ent.Cor = Color.Cyan; break;     // Player
                        case "M":
                            // Aggro check
                            if (p.Length >= 6 && p[5] == "1") { ent.IsAggressive = true; ent.Cor = Color.Red; }
                            else { ent.IsAggressive = false; ent.Cor = Color.Yellow; }
                            break;
                        case "D": ent.Cor = Color.Blue; break;     // Drop
                        case "C": ent.Cor = Color.Magenta; break;  // Loot/Corpo
                        default:
                            ent.Cor = isHideoutEnter ? Color.DeepPink : Color.LimeGreen;
                            break; // Recurso
                    }
                    lista.Add(ent);
                }
            }
            lista.Sort((a, b) => a.Distancia.CompareTo(b.Distancia));
            return lista;
        }

        public static List<ListViewItem> GerarItensLista(List<RadarEntity> entidades)
        {
            List<ListViewItem> listaVisual = new List<ListViewItem>();

            foreach (var ent in entidades)
            {
                // 1. COLUNA TIPO
                string tipoTexto = ent.Tipo;
                if (ent.Tipo == "M") tipoTexto = ent.IsAggressive ? "Monstro *" : "Monstro";
                if (ent.Tipo == "R") tipoTexto = "Recurso";
                if (ent.Tipo == "D") tipoTexto = "Drop";
                if (ent.Tipo == "C") tipoTexto = "Loot";
                if (ent.Tipo == "P") tipoTexto = "Player";

                ListViewItem lvi = new ListViewItem(tipoTexto);

                // 2. COLUNA LEVEL (AQUI ESTAVA O ERRO, AGORA ESTÁ CERTO)
                // Se for maior que 0 mostra o número, se não mostra "-"
                string textoLevel = ent.Nivel > 0 ? ent.Nivel.ToString() : "-";
                lvi.SubItems.Add(textoLevel);

                // 3. COLUNA NOME
                lvi.SubItems.Add(ent.Nome);

                // 4. COLUNA DISTÂNCIA
                lvi.SubItems.Add(ent.Distancia.ToString("F1") + "m");

                // Cores da Lista
                if (ent.Tipo == "M") lvi.ForeColor = ent.IsAggressive ? Color.Red : Color.Orange;
                else if (ent.Tipo == "P") lvi.ForeColor = Color.DarkBlue;
                else if (ent.Tipo == "C") lvi.ForeColor = Color.Purple;
                else if (ent.Tipo == "D") lvi.ForeColor = Color.Blue;
                else if (ent.Tipo == "R" && !string.IsNullOrEmpty(ent.Nome) && ent.Nome.IndexOf("HideoutEnter", StringComparison.OrdinalIgnoreCase) >= 0) lvi.ForeColor = Color.DeepPink;
                else lvi.ForeColor = Color.DarkGreen;

                listaVisual.Add(lvi);
            }
            return listaVisual;
        }
    }
}