using System;
using System.Globalization;

namespace WildTerraDashboard
{
    public class PlayerStats
    {
        // Agora são float (números) e não string (texto)
        public int HP { get; set; }
        public int SP { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public void Atualizar(string[] partes)
        {
            // O pacote vem assim: STATS;HP;SP;X;Y;Z
            // Exemplo: STATS;100;100;120.5;10.0;300.2

            if (partes.Length >= 6)
            {
                // Parse Inteiros (HP e SP)
                int.TryParse(partes[1], out int hp);
                int.TryParse(partes[2], out int sp);

                HP = hp;
                SP = sp;

                // Parse Floats (Coordenadas)
                // Usamos CultureInfo.InvariantCulture para garantir que o ponto (.) funcione
                float.TryParse(partes[3].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float x);
                float.TryParse(partes[4].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float y);
                float.TryParse(partes[5].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out float z);

                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}